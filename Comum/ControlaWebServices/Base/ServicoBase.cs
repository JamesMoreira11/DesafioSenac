using Senac.Fecomercio.Common;
using System;

namespace Senac.Fecomercio.ControlaWebServices.Base
{
    public abstract class ServicoBase : IDisposable
    {
        #region Construtor
        public ServicoBase()
        {
            try
            {
                CriarInstancia();
                ConfiguraServico();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ServicoBase(bool criaInstancia)
        {
            try
            {
                if (criaInstancia)
                {
                    CriarInstancia();
                    ConfiguraServico();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Propriedades
        protected ServiceReferenceBase ServicoCall = null;
        #endregion

        #region MetodosAbstratos
        protected abstract void CriarInstancia();

        protected abstract void ConfiguraServico();

        protected abstract ISaidaServico CallMetodoServico(IEntradaServico entrada);
        #endregion

        #region Metodos
        public ISaidaServico Enviar(IEntradaServico entrada)
        {
            ISaidaServico retorno = null;
            try
            {
                retorno = CallMetodoServico(entrada);
                return retorno;
            }
            catch (Exception ex)
            {
                Logger.LogError("ServicoBase > Enviar Error  > {0}".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
        }

        public void Dispose()
        {
            try
            {
                if (ServicoCall.IsNotNull())
                {
                    ServicoCall.Dispose();
                    ServicoCall = null;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("ServicoBase > Error Dispose > Erro: '{0}'".ToFormat(ex.GetAllErrorDetail()), ex);
            }
        }
        #endregion
    }
}