using Senac.Fecomercio.Common;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Senac.Fecomercio.Data
{
    public class BDConexao
    {
        public SqlConnection connection = new SqlConnection();
        public SqlTransaction transaction;
        public bool transacaoAtiva;
        public int commandTimeout;
        int gcCount = 0;

        public static BDConexao ConexaoBDTGTC()
        {
            return NovaConexao("BDTGTC");
        }

        public static BDConexao ConexaoBDTCEP()
        {
            return NovaConexao("BDTCEP");
        }

        public static BDConexao NovaConexao(string dbName)
        {
            var cripto = new Crypt();
            string connectionString = cripto.Decrypt(ConfigurationManager.ConnectionStrings[dbName].ConnectionString);            

            cripto = null;
            return new BDConexao(connectionString);
        }

        public BDConexao()
        {
        }

        public BDConexao(string connectionString)
        {
            Open(connectionString, false);
        }

        public BDConexao(string connectionString, bool readOnly)
        {
            Open(connectionString, readOnly);
        }

        public void Open(string connectionString)
        {
            Open(connectionString, false);
        }

        public void Close()
        {
            if (connection.State.Equals(ConnectionState.Open))
            {
                connection.Close();
                connection.Dispose();
                gcCount++;

                if (gcCount.Equals(50))
                {
                    GC.Collect();
                    gcCount = 0;
                }
            }
        }

        public void Open(string connectionString, bool readOnly)
        {
            transacaoAtiva = false;
            //commandTimeout = 30;
            //commandTimeout = 300;
            commandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["commandTimeout"]);
            if (commandTimeout == 0)
                commandTimeout = 30;
            connection = new SqlConnection(connectionString);
            connection.Open();
            //spid = Me.ExecuteScalar("SELECT @@SPID") 
        }

        public void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
            transacaoAtiva = true;
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            transaction = connection.BeginTransaction(isolationLevel);
            //Me.ExecuteNonQuery("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE") 
            transacaoAtiva = true;
        }

        // Esta assinatura foi criada para manter compatibilidade com a implementação anterior
        public void BeginTransaction(string transactionName)
        {
            transaction = connection.BeginTransaction(transactionName);
            transacaoAtiva = true;
        }

        public void CommitTransaction()
        {
            transaction.Commit();
            transacaoAtiva = false;
        }

        public void RollbackTransaction()
        {
            transaction.Rollback();
            transacaoAtiva = false;
        }

        public SqlDataReader ExecuteReader(string sql)
        {
            Logger.LogDebug("ExecuteReader: {0}", sql);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            SqlCommand cmd = GetCommand(sql, CommandType.Text);
            var retorno = cmd.ExecuteReader();

            sw.Stop();
            Logger.LogDebug(string.Format("Tempo de processamento: {0}", sw.Elapsed));

            return retorno;

        }

        public SqlDataReader ExecuteReader(string sql, CommandType commandType, ref SqlParameter[] parameters)
        {
            SqlDataReader ret = null;
            Logger.LogDebug("ExecuteReader: {0}, {1}", sql, commandType);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            SqlCommand cmd = GetCommand(sql, commandType, ref parameters);
            ret = cmd.ExecuteReader();

            sw.Stop();
            Logger.LogDebug(string.Format("Tempo de processamento: {0}", sw.Elapsed));

            return ret;
        }

        //// Esta assinatura foi criada para manter compatibilidade com a implementação de DBManager
        //public int ExecuteNonQuery_OBSOLETO(string databasename, string sql, CommandType commandType)
        //{
        //    return ExecuteNonQuery(sql, commandType);
        //}

        //// Esta assinatura foi criada para manter compatibilidade com a implementação de DBManager
        //public int ExecuteNonQuery_OBSOLETO(string databasename, string sql, CommandType commandType, ref SqlParameter[] parameters)
        //{
        //    return ExecuteNonQuery(sql, commandType, ref parameters);
        //}

        // Esta assinatura foi criada para manter compatibilidade com a implementação de DBManager
        //public int ExecuteNonQuery(string sql, CommandType commandType, ref SqlParameter[] parameters, ref SqlTransaction externalTransaction)
        public int ExecuteNonQuery(string sql, CommandType commandType, ref SqlParameter[] parameters)
        {

            Logger.LogDebug("ExecuteNonQuery: {0}, {1}", sql, commandType);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            SqlCommand cmd = GetCommand(sql, commandType, ref parameters);
            int ret = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            var retorno = ret;

            sw.Stop();
            Logger.LogDebug(string.Format("Tempo de processamento: {0}", sw.Elapsed));

            return ret;
        }

        public int ExecuteNonQuery(string sql, CommandType commandType)
        {
            Logger.LogDebug("ExecuteNonQuery: {0}, {1}", sql, commandType);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            SqlCommand cmd = GetCommand(sql, commandType);
            var retorno = cmd.ExecuteNonQuery();

            sw.Stop();
            Logger.LogDebug(string.Format("Tempo de processamento: {0}", sw.Elapsed));

            return retorno;
        }

        public int ExecuteNonQuery(string sql)
        {
            Logger.LogDebug("ExecuteNonQuery: {0}", sql);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            SqlCommand cmd = GetCommand(sql, CommandType.Text);
            var retorno = cmd.ExecuteNonQuery();

            sw.Stop();
            Logger.LogDebug(string.Format("Tempo de processamento: {0}", sw.Elapsed));

            return retorno;
        }

        //// Esta assinatura foi criada para manter compatibilidade com a implementação de DBManager
        //public object ExecuteScalar_OBSOLETO(string databasename, string sql, CommandType commandType)
        //{
        //    return ExecuteScalar(sql, commandType);
        //}

        //// Esta assinatura foi criada para manter compatibilidade com a implementação de DBManager
        //public object ExecuteScalar_OBSOLETO(string databasename, string sql, CommandType commandType, ref SqlParameter[] parameters)
        //{
        //    return ExecuteScalar(sql, commandType, ref parameters);
        //}

        public object ExecuteScalar(string sql, CommandType commandType)
        {
            Logger.LogDebug("ExecuteScalar: {0}, {1}", sql, commandType);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            SqlCommand cmd = GetCommand(sql, commandType);
            var retorno = cmd.ExecuteScalar();

            sw.Stop();
            Logger.LogDebug(string.Format("Tempo de processamento: {0}", sw.Elapsed));

            return retorno;

        }

        public object ExecuteScalar(string sql, CommandType commandType, ref SqlParameter[] parameters)
        {

            Logger.LogDebug("ExecuteScalar: {0}, {1}", sql, commandType);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            SqlCommand cmd = GetCommand(sql, commandType, ref parameters);
            object ret = cmd.ExecuteScalar();
            cmd.Parameters.Clear();

            sw.Stop();
            Logger.LogDebug(string.Format("Tempo de processamento: {0}", sw.Elapsed));

            return ret;

        }

        public object ExecuteScalar(string sql)
        {
            Logger.LogDebug("ExecuteScalar: {0}", sql);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            SqlCommand cmd = GetCommand(sql, CommandType.Text);
            var ret = cmd.ExecuteScalar();

            sw.Stop();
            Logger.LogDebug(string.Format("Tempo de processamento: {0}", sw.Elapsed));

            return ret;
        }

        public DataRow ExecuteDataRow(string sql)
        {

            SqlParameter[] parameters = null;
            var ret = ExecuteDataRow(sql, CommandType.Text, ref parameters);

            return ret;

        }

        public DataRow ExecuteDataRow(string sql, CommandType commandType, ref SqlParameter[] parameters)
        {

            DataRow dr = null;
            DataTable dt = ExecuteDataTable(sql, commandType, ref parameters);
            if (dt.Rows.Count > 0)
                dr = dt.Rows[0];

            return dr;
        }

        // Esta assinatura foi criada para manter compatibilidade com a implementação de DBManager
        //public DataTable ExecuteDataTable(string dataBaseName, string commandSQL, CommandType commandType)
        //{
        //    SqlParameter[] p = null;
        //    return ExecuteDataTable(dataBaseName, commandSQL, commandType, ref p);
        //}

        //// Esta assinatura foi criada para manter compatibilidade com a implementação de DBManager
        //public DataTable ExecuteDataTable_OBSOLETO(string dataBaseName, string sql, CommandType commandType, ref SqlParameter[] parameters)
        //{
        //    return ExecuteDataTable(sql, commandType, ref parameters);
        //}

        public DataTable ExecuteDataTable(string sql)
        {

            SqlParameter[] parameters = null;
            var ret = ExecuteDataTable(sql, CommandType.Text, ref parameters);

            return ret;
        }

        public DataTable ExecuteDataTable(string sql, CommandType commandType)
        {
            SqlParameter[] parametros = null;
            var ret = ExecuteDataTable(sql, commandType, ref parametros);

            return ret;
        }

        public DataTable ExecuteDataTable(string sql, CommandType commandType, ref SqlParameter[] parameters)
        {
            Logger.LogDebug("ExecuteDataTable: {0}, {1}", sql, commandType);
            Stopwatch sw = new Stopwatch();
            sw.Start();

            SqlCommand cmd = GetCommand(sql, commandType, ref parameters);
            DataTable ret = ExecuteDataTable(cmd);
            Logger.LogDebug(string.Format("Comando Executado: {0}", cmd.CommandText));
            cmd.Parameters.Clear();

            sw.Stop();
            Logger.LogDebug(string.Format("Tempo de processamento: {0}", sw.Elapsed));

            return ret;
        }

        private DataTable ExecuteDataTable(SqlCommand cmd)
        {

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public DataSet ExecuteDataset(string sql)
        {
            Logger.LogDebug("ExecuteDataset: {0}", sql);
            Stopwatch sw = new Stopwatch();
            sw.Start();

            SqlCommand cmd = GetCommand(sql, CommandType.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            sw.Stop();
            Logger.LogDebug(string.Format("Tempo de processamento: {0}", sw.Elapsed));

            return ds;
        }

        private SqlCommand GetProcedure(string sql)
        {
            SqlCommand cmd = GetCommand(sql, CommandType.StoredProcedure);
            //if (true)
            //{
            //    SqlParameter parameter = null;
            //    parameter = new SqlParameter();
            //    parameter.ParameterName = "@ReturnValue";
            //    parameter.Direction = ParameterDirection.ReturnValue;
            //    parameter.SqlDbType = SqlDbType.Int;
            //    cmd.Parameters.Add(parameter);
            //}
            return cmd;
        }

        private SqlCommand GetProcedure(string sql, ref SqlParameter[] parameters)
        {
            return GetCommand(sql, CommandType.StoredProcedure, ref parameters);
        }

        private SqlCommand GetCommand(string sql, CommandType commandType)
        {
            SqlParameter[] parameters = null;
            return GetCommand(sql, commandType, ref parameters);
        }

        private SqlCommand GetCommand(string sql, CommandType commandType, ref SqlParameter[] parameters)
        {
            var cmd = new SqlCommand(sql, connection)
            {
                CommandType = commandType,
                CommandTimeout = commandTimeout,
                Transaction = transaction
            };

            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            return cmd;
        }

        #region Implement IDisposable (Finalize and Dispose)
        ~BDConexao()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    if (transaction != null)
                    {
                        if (transacaoAtiva)
                            RollbackTransaction();

                        transaction.Dispose();
                        transaction = null;
                    }

                    if (connection != null)
                    {
                        //if (connection.State != ConnectionState.Closed)
                        if (connection.State == ConnectionState.Open)
                            connection.Close();

                        connection.Dispose();
                        connection = null;
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("BDConexao.Dispose(true)", ex);
                }
            }
        }
        #endregion
    }
}