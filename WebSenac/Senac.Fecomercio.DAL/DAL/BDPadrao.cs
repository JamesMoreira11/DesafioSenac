using System.Data;
using System.Data.SqlClient;

namespace CieloGtecDAL.DAL
{
    // Interface estática, para o banco padrão do sistema
    public static class BDPadrao
    {
        public static BDConexao ConexaoPadrao()
        {
            return BDConexao.ConexaoBDTGTC();
        }

        public static DataTable ExecuteDataTable(string sql)
        {
            SqlParameter[] parametros = null;
            return ExecuteDataTable(sql, CommandType.Text, ref parametros);
        }

        public static DataTable ExecuteDataTable(string sql, CommandType commandType)
        {
            SqlParameter[] parametros = null;
            return ExecuteDataTable(sql, commandType, ref parametros);
        }

        public static DataTable ExecuteDataTable(string sql, ref SqlParameter[] parametros)
        {
            return ExecuteDataTable(sql, CommandType.Text, ref parametros);
        }

        public static DataTable ExecuteDataTable(string sql, CommandType commandType, ref SqlParameter[] parametros)
        {
            BDConexao db = ConexaoPadrao();
            DataTable lista;

            try
            {
                lista = db.ExecuteDataTable(sql, commandType, ref parametros);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                db.CloseConnection();
                db.Dispose();
            }

            return lista;
        }

        public static DataRow ExecuteDataRow(string sql)
        {
            SqlParameter[] parametros = null;
            return ExecuteDataRow(sql, CommandType.Text, ref parametros);
        }

        public static DataRow ExecuteDataRow(string sql, CommandType commandType, ref SqlParameter[] parametros)
        {
            BDConexao db = ConexaoPadrao();
            DataRow linha;

            try
            {
                linha = db.ExecuteDataRow(sql, commandType, ref parametros);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                db.CloseConnection();
                db.Dispose();
            }

            return linha;
        }

        public static object ExecuteScalar(string sql)
        {
            SqlParameter[] parametros = null;
            return ExecuteScalar(sql, CommandType.Text, ref parametros);
        }

        public static object ExecuteScalar(string sql, CommandType commandType, ref SqlParameter[] parametros)
        {
            BDConexao db = ConexaoPadrao();
            object campo;

            try
            {
                campo = db.ExecuteScalar(sql, commandType, ref parametros);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                db.CloseConnection();
                db.Dispose();
            }

            return campo;
        }

        public static int ExecuteNonQuery(string sql)
        {
            SqlParameter[] parameters = null;
            return ExecuteNonQuery(sql, CommandType.Text, ref parameters);
        }

        public static int ExecuteNonQuery(string sql, CommandType commandType, ref SqlParameter[] parameters)
        {
            BDConexao db = ConexaoPadrao();
            int n;

            try
            {
                n = db.ExecuteNonQuery(sql, commandType, ref parameters);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                db.CloseConnection();
                db.Dispose();
            }

            return n;
        }

    }
}
