using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.Base;
using Senac.Fecomercio.Entities.GCOM.ConsultaGestorComercial;
using System;
using System.Net;

namespace Senac.Fecomercio.ControlaWebServices.ServicoRest.API.GCOM
{
    public class GestorComercial : ServicoBaseRest
    {
        #region Construtor
        public GestorComercial(string numeroEstabelecimento)
            : base()
        {
            this.NumeroEstabelecimento = numeroEstabelecimento;
        }
        #endregion

        #region Propriedades
        private string NumeroEstabelecimento { get; set; }
        #endregion

        #region MetodosOverrides
        protected override string GetContentType()
        {
            return "application/json";
        }

        protected override string GetMethod()
        {
            return "GET";
        }

        protected override string GetUrl()
        {
            return Extension.GetValueConfig("SERVICO_REST_API_GCOM_GESTOR_COMERCIAL_URL", true).ToFormat(this.NumeroEstabelecimento);
        }

        protected override string GetUserAgent()
        {
            return "";
        }
        #endregion

        #region Metodos
        public RetornoGestorComercial ObterGestorComercial()
        {
            RetornoGestorComercial ret = null;
            try
            {
                string habilitaProxy = Extension.GetValueConfig("SERVICO_REST_API_GCOM_GESTOR_COMERCIAL_PROXY_HABILITA", false);

                if (habilitaProxy.IsNotNull() && habilitaProxy.Equals("1"))
                {
                    string hostProxy = Extension.GetValueConfig("SERVICO_REST_API_GCOM_GESTOR_COMERCIAL_PROXY_HOST", true);
                    string porta = Extension.GetValueConfig("SERVICO_REST_API_GCOM_GESTOR_COMERCIAL_PROXY_PORTA", true);
                    int portaConv = 8080;

                    if (!porta.IsInt())
                    {
                        throw new FormatException("O parâmetro 'SERVICO_REST_API_GCOM_GESTOR_COMERCIAL_PROXY_PORTA' está inválido, não é numérico");
                    }
                    else
                    {
                        portaConv = porta.ToInt();
                    }

                    Logger.LogInfo("GestorComercial - Adicionando proxy - '{0}' - '{1}'".ToFormat(hostProxy, portaConv));
                    AddProxy(hostProxy, portaConv);
                }

                //HabilitarValidacaoSSL();

                HttpStatusCode[] codeSucesso = new HttpStatusCode[2];
                codeSucesso[0] = HttpStatusCode.OK;
                codeSucesso[1] = HttpStatusCode.NotFound;

                int? statusCodeResponse = null;

                string jsonResponse = CallServiceGet(codeSucesso, out statusCodeResponse, null);

                Logger.LogInfo("GestorComercial - jsonResponse: '{0}' - statusCodeResponse: '{1}'".ToFormat(jsonResponse, statusCodeResponse));

                if (statusCodeResponse.HasValue && statusCodeResponse.Value.Equals((int)HttpStatusCode.OK))
                {
                    ret = new RetornoGestorComercial();

                    if (jsonResponse.IsNotNull())
                    {
                        ret = jsonResponse.JsonDeserializeObject<RetornoGestorComercial>();
                    }
                }

                return ret;
            }
            catch (Exception ex)
            {
                Logger.LogError("GestorComercial - Erro: '{0}'".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
        }
        #endregion
    }
}