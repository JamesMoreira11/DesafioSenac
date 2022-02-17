using Senac.Fecomercio.Business.Base;
using Senac.Fecomercio.Common;
using Senac.Fecomercio.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Senac.Fecomercio.Business
{
    public partial class RegistroPonto : BaseBL
    {
        #region Propriedade

        ProcessamentoRegistroPontoDAL oProcDAL = null;

        #endregion

        #region Construtor
        public RegistroPonto()
        {
        }
        #endregion

        public DataTable ConsultaRegistroPonto(int codColaborador)
        {
            ProcessamentoRegistroPontoDAL oProcDAL = null;
            try
            {
                oProcDAL = new ProcessamentoRegistroPontoDAL();

                return oProcDAL.ConsultaFolhaPonto(codColaborador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oProcDAL.IsNotNull())
                {
                    oProcDAL.Dispose();
                    oProcDAL = null;
                }
            }
        }

        #region registrarponto
        public void CadastrarRegistroPonto(int codColaborador, string nomColaborador, DateTime datLancamento, string indLancamento)
        {
            try
            {
                if (oProcDAL.IsNull())
                {
                    oProcDAL = new ProcessamentoRegistroPontoDAL();
                }

                oProcDAL.CadastrarRegistroPonto(codColaborador, nomColaborador, datLancamento, indLancamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oProcDAL.IsNotNull())
                {
                    oProcDAL.Dispose();
                    oProcDAL = null;
                }
            }
        }
        public void Dispose()
        {
            if (oProcDAL.IsNull())
                oProcDAL = new ProcessamentoRegistroPontoDAL();

            oProcDAL.Dispose();
            oProcDAL = null;
        }

        #endregion
    }
}
