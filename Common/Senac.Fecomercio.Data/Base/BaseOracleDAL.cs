using Senac.Fecomercio.Common;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace Senac.Fecomercio.Data.Base
{
    public class BaseOracleDAL : IDisposable
    {
        #region Propriedades
        private DbConnection conexao = null;
        private DbTransaction transacao = null;
        private bool ConexaoAberta { get; set; }
        private bool ExisteTransacaoAberta { get; set; }
        private string ServerOracle { get; set; }
        private int PortaOracle { get; set; }
        private string UserOracle { get; set; }
        private string PwdOracle { get; set; }
        private string ServiceNameOracle { get; set; }
        #endregion

        #region Construtor
        public BaseOracleDAL(string chaveServer, string chaveUser, string chavePwd, string chaveServiceName, string chavePorta = null)
        {
            var cripto = new Crypt();
            this.ServerOracle = Extension.GetValueConfig(chaveServer, true);
            this.ServiceNameOracle = Extension.GetValueConfig(chaveServiceName, true);
            this.UserOracle = cripto.Decrypt(Extension.GetValueConfig(chaveUser, true));
            this.PortaOracle = 1521;

            if (chavePorta.IsNotNull())
            {
                string portaConfig = Extension.GetValueConfig(chavePorta, true);

                if (portaConfig.IsNotNull() && portaConfig.IsInt())
                {
                    this.PortaOracle = portaConfig.ToInt();
                }
            }

            this.PwdOracle = cripto.Decrypt(Extension.GetValueConfig(chavePwd, true));
            cripto = null;

            ConexaoAberta = false;
            ExisteTransacaoAberta = false;
            AbrirConexao();
        }
        #endregion

        #region Metodos
        public void AbrirConexao()
        {
            if (conexao.IsNull())
            {
                conexao = new OracleConnection();
            }

            if (conexao.State != System.Data.ConnectionState.Open)
            {
                string stringConexao = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={2})));User Id={3};Password={4};".ToFormat(this.ServerOracle, this.PortaOracle, this.ServiceNameOracle, this.UserOracle, this.PwdOracle);
                conexao.ConnectionString = stringConexao;
                conexao.Open();
                ConexaoAberta = true;
            }
        }

        public void FecharConexao()
        {
            if (ExisteTransacaoAberta)
            {
                RollBackTransaction();
                FecharConexao();
                throw new Exception("Foi fechado a conexão com o Oracle sem finalizar a transação aberta, por segurança, foi realizado o rollback.");
            }

            if (ConexaoAberta && conexao.IsNotNull())
            {
                try
                {
                    conexao.Close();
                    conexao.Dispose();
                    conexao = null;

                }
                catch { }
                finally
                {
                    ConexaoAberta = false;
                }
            }
        }

        public void BeginTransaction()
        {
            if (!ExisteTransacaoAberta)
            {
                transacao = conexao.BeginTransaction();
                ExisteTransacaoAberta = true;
            }
            else
            {
                throw new Exception("Já existe uma transação aberta no Oracle. Server: '{0}'".ToFormat(this.ServerOracle));
            }
        }

        public void CommitTransaction()
        {
            if (ExisteTransacaoAberta && transacao.IsNotNull())
            {
                transacao.Commit();
                transacao.Dispose();
                transacao = null;
                ExisteTransacaoAberta = false;
            }
            else
            {
                throw new Exception("Não existe uma transação aberta no Oracle para realizar o commit. Server: {0}".ToFormat(this.ServerOracle));
            }
        }

        public void RollBackTransaction()
        {
            if (ExisteTransacaoAberta && transacao.IsNotNull())
            {
                transacao.Rollback();
                transacao.Dispose();
                transacao = null;
                ExisteTransacaoAberta = false;
            }
            else
            {
                throw new Exception("Não existe uma transação aberta no Oracle para realizar o rollback. Server: {0}".ToFormat(this.ServerOracle));
            }
        }

        public bool ExecuteSQLBulkCopy<T>(string sql, CommandType cmdType, ICollection<T> lista, bool bindByName = true)
        {
            bool ret = false;

            using (OracleCommand cmd = (OracleCommand)conexao.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = cmdType;
                cmd.BindByName = bindByName;
                cmd.ArrayBindCount = lista.Count();

                var entityProp = lista.FirstOrDefault();
                PropertyInfo[] allProperties = entityProp.GetProperties();

                if (allProperties.IsNotNull())
                {
                    for (int i = 0; i < allProperties.Count(); i++)
                    {
                        cmd.Parameters.Add(":{0}".ToFormat(allProperties[i].Name), GetOracleTypeFromType(allProperties[i].PropertyType), lista.Select(c => c.GetType().GetProperty(allProperties[i].Name).GetValue(c)).ToArray(), ParameterDirection.Input);
                    }

                    int execTotal = cmd.ExecuteNonQuery();

                    if (execTotal == lista.Count())
                    {
                        ret = true;
                    }
                }
            }

            return ret;
        }

        public void ExecuteNonQuery(string sql, CommandType cmdType, DbParameter[] paramCol = null)
        {
            using (OracleCommand cmd = (OracleCommand)conexao.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = cmdType;
                if (paramCol.IsNotNull())
                {
                    cmd.Parameters.AddRange(paramCol);
                }
                cmd.ExecuteNonQuery();
            }
        }

        public DbDataReader ExecuteDataReader(string sql, CommandType cmdType, DbParameter[] paramCol = null)
        {
            DbDataReader dr = null;
            using (OracleCommand cmd = (OracleCommand)conexao.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = cmdType;
                if (paramCol.IsNotNull())
                {
                    cmd.Parameters.AddRange(paramCol);
                }
                dr = cmd.ExecuteReader();
            }
            return dr;
        }

        protected OracleDbType GetOracleTypeFromType(Type tipo)
        {
            OracleDbType ret = OracleDbType.Varchar2;

            if (tipo == typeof(decimal) || tipo == typeof(Decimal))
            {
                ret = OracleDbType.Decimal;
            }
            else if (tipo == typeof(DateTime))
            {
                ret = OracleDbType.Date;
            }
            else if (tipo == typeof(Int32) || tipo == typeof(int))
            {
                ret = OracleDbType.Int32;
            }

            return ret;
        }

        #region Dispose
        public void Dispose()
        {
            try
            {
                FecharConexao();
            }
            catch { }

            if (transacao.IsNotNull())
            {
                transacao.Dispose();
                transacao = null;
            }

            if (conexao.IsNotNull())
            {
                conexao.Dispose();
                conexao = null;
            }
        }
        #endregion
        #endregion
    }
}