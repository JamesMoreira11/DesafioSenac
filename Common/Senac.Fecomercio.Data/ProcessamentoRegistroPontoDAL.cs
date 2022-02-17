using Senac.Fecomercio.Common;
using Senac.Fecomercio.Data.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Senac.Fecomercio.Data
{
    public class ProcessamentoRegistroPontoDAL : BaseDAL, IDisposable
    {

        public DataTable ConsultaFolhaPonto(int codColaborador)
        {
            DataTable retorno = null;
            try
            {
                SqlParameter param01 = new SqlParameter("@CD_GUID", codColaborador);
                SqlParameter param02 = new SqlParameter("@FL_TIPO ", "C");
                SqlParameter[] parametros = { param01, param02 };

                retorno = ConexaoPadrao().ExecuteDataTable("PR_ATUALIZA_REGISTRO_PONTO_001", CommandType.StoredProcedure, ref parametros);

                return retorno;
            }
            catch (Exception ex)
            {
                Logger.LogError("ConsultaFolhaPonto() - Error: '{0}'".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
            finally
            {
                if (!ConexaoPadrao().transacaoAtiva)
                {
                    EncerrarConexao();
                }
            }
        }
        public void CadastrarRegistroPonto(int codColaborador, string nomColaborador, DateTime datLancamento, string indLancamento)
        {
            try
            {
                Logger.LogInfo("CadastrarRegistroPonto()");

                SqlParameter param1 = new SqlParameter("@CD_GUID", codColaborador);
                SqlParameter param2 = new SqlParameter("@NM_COLABORADOR", nomColaborador);
                SqlParameter param3 = new SqlParameter("@DT_LANCAMENTO", datLancamento);
                SqlParameter param4 = new SqlParameter("@IN_ES", indLancamento);
                SqlParameter param5 = new SqlParameter("@FL_TIPO", "I");

                SqlParameter[] parametros = { param1, param2, param3, param4, param5 };

                ConexaoPadrao().ExecuteNonQuery("PR_GT_AT_FILIAL_I_001", CommandType.StoredProcedure, ref parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!ConexaoPadrao().transacaoAtiva)
                {
                    EncerrarConexao();
                }
            }
        }

        #region Dispose
        bool disposed = false;

        public void Dispose()
        {
            if (!ConexaoPadrao().transacaoAtiva)
            {
                EncerrarConexao();
            }

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            //if (disposed)
            return;

            if (disposing)
            {
                //EncerrarConexao();
            }

            disposed = true;
        }

        ~ProcessamentoRegistroPontoDAL()
        {
            Dispose(false);
        }
        #endregion
    }
}
