using Senac.Fecomercio.Common;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace Senac.Fecomercio.Data.Base
{
    public class BaseDAL
    {
        public static void BeginTransaction(string transactionName)
        {
            ConexaoPadrao().BeginTransaction(transactionName);
        }

        public static void Commit()
        {
            ConexaoPadrao().CommitTransaction();
            ConexaoPadrao().Close();
        }

        public static void Rollback()
        {
            ConexaoPadrao().RollbackTransaction();
            ConexaoPadrao().Close();
        }

        private static Dictionary<int, BDConexao> connections = new Dictionary<int, BDConexao>();

        public static BDConexao ConexaoPadrao()
        {
            int keyConn = Thread.CurrentThread.ManagedThreadId;

            Logger.LogDebug("BaseDAL - ConexaoPadrao() - Iniciando conexao padrão.");

            if (connections.IsNotNull())
            {
                Logger.LogDebug("BaseDAL - ConexaoPadrao() - Essa é uma conexão válida.");

                lock (connections)
                {
                    Logger.LogDebug("BaseDAL - ConexaoPadrao() - Conexão locada.");

                    if (connections.Count > 0 && connections.ContainsKey(keyConn))
                    {
                        if (connections[keyConn].connection.IsNull() || (connections[keyConn].connection.IsNotNull() && connections[keyConn].connection.State != ConnectionState.Open))
                        {
                            try
                            {
                                Logger.LogDebug("BaseDAL - ConexaoPadrao() - Fechando conexão.");
                                connections[keyConn].connection.Close();
                            }
                            catch { }

                            try
                            {
                                Logger.LogDebug("BaseDAL - ConexaoPadrao() - Tirando a conexão da memória.");
                                connections[keyConn].connection.Dispose();
                            }
                            catch { }

                            Logger.LogDebug("BaseDAL - ConexaoPadrao() - Removendo a conexão.");
                            connections.Remove(keyConn);
                            Logger.LogDebug("BaseDAL - ConexaoPadrao() - Adicionando a conexão.");
                            connections.Add(keyConn, BDConexao.ConexaoBDTGTC());
                        }
                    }
                    else
                    {
                        Logger.LogDebug("BaseDAL - ConexaoPadrao() - Adicionando a conexão.");
                        connections.Add(keyConn, BDConexao.ConexaoBDTGTC());
                    }
                }
            }
            else
            {
                Logger.LogDebug("BaseDAL - ConexaoPadrao() - Criando uma nova instância da conexão, quando a conexão está nula.");
                connections = new Dictionary<int, BDConexao>();
                Logger.LogDebug("BaseDAL - ConexaoPadrao() - Adicionando a conexão, quando a conexão está nula.");
                connections.Add(keyConn, BDConexao.ConexaoBDTGTC());
            }

            return connections[keyConn];
        }

        public static void EncerrarConexao(int? keyConnParam = null)
        {
            int keyConn = Thread.CurrentThread.ManagedThreadId;

            if (keyConnParam != null)
            {
                keyConn = keyConnParam.Value;
            }

            lock (connections)
            {
                if (connections != null && connections.Count > 0 && connections.ContainsKey(keyConn))
                {
                    try
                    {
                        connections[keyConn].connection.Close();
                    }
                    catch { }

                    try
                    {
                        connections[keyConn].connection.Dispose();
                    }
                    catch { }

                    try
                    {
                        connections[keyConn].connection = null;
                    }
                    catch { }

                    connections.Remove(keyConn);
                }
            }
        }

        public static void EncerrarTodasConexao()
        {
            if (connections != null && connections.Count > 0)
            {
                lock (connections)
                {
                    foreach (var itemCon in connections.Values)
                    {
                        try
                        {
                            itemCon.Close();
                        }
                        catch { }

                        try
                        {
                            itemCon.Dispose();
                        }
                        catch { }
                    }

                    connections.Clear();
                }

                connections = null;
            }
        }

        public static bool ConexaoFuncionando()
        {
            bool ret = true;

            try
            {
                BDConexao conexao = BDConexao.ConexaoBDTGTC();

                try
                {
                    conexao.Close();
                    conexao.Dispose();
                    conexao = null;
                }
                catch { }
            }
            catch
            {
                ret = false;
            }

            return ret;
        }
    }
}