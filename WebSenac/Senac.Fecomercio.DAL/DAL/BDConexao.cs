using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CieloGtecDAL.Crypt;
using System.Configuration;
using LogviewHelper;
//using System.Linq;

namespace CieloGtecDAL.DAL
{
    public class BDConexao
    {
        // Contador Garbage Collector
        private int gcCount = 0;
        public SqlConnection connection = new SqlConnection();
        public SqlTransaction transaction;
        public bool transacaoAtiva;
        public int commandTimeout;

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
            var cripto = new CieloGtecDAL.Crypt.Crypt();
            string connectionString = cripto.Decrypt(ConfigurationManager.ConnectionStrings[dbName].ConnectionString);
            cripto = null;
            return new BDConexao(connectionString);
        }

        public BDConexao()
        {
        }

        public BDConexao(string connectionString)
        {
            this.Open(connectionString, false);
        }

        public BDConexao(string connectionString, bool readOnly)
        {
            this.Open(connectionString, readOnly);
        }

        public void Open(string connectionString)
        {
            this.Open(connectionString, false);
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

        public void BeginTransaction(System.Data.IsolationLevel isolationLevel)
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
            SqlCommand cmd = GetCommand(sql, CommandType.Text);
            return cmd.ExecuteReader();
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
            SqlCommand cmd = GetCommand(sql, commandType, ref parameters);
            int ret = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return ret;
        }

        public int ExecuteNonQuery(string sql, CommandType commandType)
        {
            SqlCommand cmd = GetCommand(sql, commandType);
            return cmd.ExecuteNonQuery();
        }

        public int ExecuteNonQuery(string sql)
        {
            SqlCommand cmd = GetCommand(sql, CommandType.Text);
            return cmd.ExecuteNonQuery();
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
            SqlCommand cmd = GetCommand(sql, commandType);
            return cmd.ExecuteScalar();
        }

        public object ExecuteScalar(string sql, CommandType commandType, ref SqlParameter[] parameters)
        {
            SqlCommand cmd = GetCommand(sql, commandType, ref parameters);
            object ret = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return ret;
        }

        public object ExecuteScalar(string sql)
        {
            SqlCommand cmd = GetCommand(sql, CommandType.Text);
            return cmd.ExecuteScalar();
        }

        public DataRow ExecuteDataRow(string sql)
        {
            SqlParameter[] parameters = null;
            return ExecuteDataRow(sql, CommandType.Text, ref parameters);
        }

        public DataRow ExecuteDataRow(string sql, CommandType commandType, ref SqlParameter[] parameters)
        {
            DataRow dr = null;
            DataTable dt = this.ExecuteDataTable(sql, commandType, ref parameters);
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
            return ExecuteDataTable(sql, CommandType.Text, ref parameters);
        }

        public DataTable ExecuteDataTable(string sql, CommandType commandType)
        {
            SqlParameter[] parametros = null;
            return ExecuteDataTable(sql, commandType, ref parametros);
        }

        public DataTable ExecuteDataTable(string sql, CommandType commandType, ref SqlParameter[] parameters)
        {
            SqlCommand cmd = GetCommand(sql, commandType, ref parameters);
            DataTable ret = ExecuteDataTable(cmd);
            cmd.Parameters.Clear();
            return ret;
        }

        public DataTable ExecuteDataTable(SqlCommand cmd)
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataSet ExecuteDataset(string sql)
        {
            SqlCommand cmd = GetCommand(sql, CommandType.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public SqlCommand GetProcedure(string sql)
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

        public SqlCommand GetProcedure(string sql, ref SqlParameter[] parameters)
        {
            SqlCommand cmd = GetCommand(sql, CommandType.StoredProcedure, ref parameters); ;
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

        public SqlCommand GetCommand(string sql, CommandType commandType)
        {
            SqlParameter[] parameters = null;
            return GetCommand(sql, commandType, ref parameters);
        }

        public SqlCommand GetCommand(string sql, CommandType commandType, ref SqlParameter[] parameters)
        {
            SqlCommand cmd;
            cmd = new SqlCommand(sql, connection);
            cmd.CommandType = commandType;
            cmd.CommandTimeout = commandTimeout;
            cmd.Transaction = transaction;
            //cmd.Parameters.Clear();
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            //if (parameters != null && parameters.Length > 0)
            //{
            //    for (int i = 0; i < parameters.Count(); i++)
            //    {
            //        cmd.Parameters.Add(parameters[i]);
            //    }
            //}

            return cmd;
        }

        public void CloseConnection()
        {
            try
            {
                CloseTransaction();


                if (connection != null)
                {
                    ////if (connection.State != ConnectionState.Closed)
                    //if (connection.State == ConnectionState.Open)
                    //    connection.Close();

                    connection.Close();
                    connection.Dispose();
                    gcCount++;
                    if (gcCount == 50)
                    {
                        gcCount = 0;
                        GC.Collect();
                        LogClass logClass = new LogClass();
                        logClass.log(Level.INFO, ConfigurationManager.AppSettings["NomeSistema"].ToString(), "Executado GC.Collect()");                    
                    }
                }
            }
            catch (Exception)
            {
            }

            connection = null;

        }

        public void CloseTransaction()
        {
            try
            {
                if (transaction != null)
                {
                    if (transacaoAtiva)
                        RollbackTransaction();

                    transaction.Dispose();
                }
            }
            catch (Exception)
            {
            }

            transaction = null;
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
                    //if (transaction != null)
                    //{
                    //    if (transacaoAtiva)
                    //        RollbackTransaction();

                    //    transaction.Dispose();
                    //    transaction = null;
                    //}
					
                    //if (connection != null)
                    //{
                    //    //if (connection.State != ConnectionState.Closed)
                    //    if (connection.State == ConnectionState.Open)
                    //        connection.Close();

                    //    connection.Dispose();
                    //    connection = null;
                    //}
                    CloseConnection();
                }
                catch (Exception ex)
                {
                    try
                    {
                        LogClass logClass = new LogClass();
                        logClass.log(Level.ERROR, ConfigurationManager.AppSettings["NomeSistema"].ToString(), "BDConexao.Dispose(true): " + ex.Message + "   -   StackTrace: " + ex.StackTrace);
                    }
                    catch (Exception)
                    {
                    }
                }
            }

        }

        #endregion

    }

}


