using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.Base;
using Senac.Fecomercio.Entities.OperadorExterno.Distribuidor;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Senac.Fecomercio.ControlaWebServices.ServicoRest.API.Distribuidor
{
    public class DistribuidorGenerico : ServicoBaseRest
    {
        #region Construtor
        public DistribuidorGenerico(int idDistribuidor, string pathDistribuidor)
            : base()
        {
            this.PathURLDistribuidor = pathDistribuidor;
            this.IdentificacaoDistribuidor = idDistribuidor;
        }
        #endregion

        #region Propriedades
        private string PathURLDistribuidor { get; set; }
        private int IdentificacaoDistribuidor { get; set; }
        #endregion

        #region MetodosOverrides
        protected override string GetContentType()
        {
            return "application/json";
        }

        protected override string GetMethod()
        {
            return "POST";
        }

        protected override string GetUrl()
        {
            return Extension.GetValueConfig("API_SERVICO_DISTRIBUIDOR_URL", true).ToFormat(this.IdentificacaoDistribuidor);
        }

        protected override string GetUserAgent()
        {
            return "";
        }
        #endregion

        #region Metodos
        public RetornoDistribuidor EnviarChamado(AberturaChamadoDistribuidor entrada, string token, string tipoToken)
        {
            RetornoDistribuidor ret = null;
            try
            {
                string habilitaProxy = Extension.GetValueConfig("SERVICO_REST_API_PROXY_HABILITA", false);

                if (habilitaProxy.IsNotNull() && habilitaProxy.Equals("1"))
                {
                    string hostProxy = Extension.GetValueConfig("SERVICO_REST_API_PROXY_HOST", true);
                    string porta = Extension.GetValueConfig("SERVICO_REST_API_PROXY_PORTA", true);
                    int portaConv = 8080;

                    if (!porta.IsInt())
                    {
                        throw new FormatException("O parâmetro 'SERVICO_REST_API_PROXY_PORTA' está inválido, não é numérico");
                    }
                    else
                    {
                        portaConv = porta.ToInt();
                    }

                    Logger.LogInfo("DistribuidorGenerico[{0}] - Adicionando proxy - '{1}' - '{2}'".ToFormat(this.IdentificacaoDistribuidor, hostProxy, portaConv));
                    AddProxy(hostProxy, portaConv);
                }

                Logger.LogInfo("DistribuidorGenerico[{0}] - Incluindo Authorization - '{1} {2}'".ToFormat(this.IdentificacaoDistribuidor, tipoToken, token));

                AddHeader("Authorization", "{0} {1}".ToFormat(tipoToken, token));
                AddHeader("partner-host", "{0}".ToFormat(this.PathURLDistribuidor));
                //AddHeader("partner-id", "{0}".ToFormat(this.IdentificacaoDistribuidor));

                HabilitarValidacaoSSL();

                HttpStatusCode[] codeSucesso = new HttpStatusCode[5];
                codeSucesso[0] = HttpStatusCode.Created;
                codeSucesso[1] = HttpStatusCode.BadRequest;
                codeSucesso[2] = HttpStatusCode.NotAcceptable;
                codeSucesso[3] = HttpStatusCode.Unauthorized;
                codeSucesso[4] = HttpStatusCode.Forbidden;

                JsonSerializerSettings jsonSett = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddThh:mm:ssZ" };

                string jsonEntrada = entrada.JsonSerializeObject(jsonSett);

                Logger.LogInfo("DistribuidorGenerico[{0}] - jsonEntrada - '{1}'".ToFormat(this.IdentificacaoDistribuidor, jsonEntrada));
                int? statusCodeResponse = null;

                string jsonResponse = CallService(codeSucesso, out statusCodeResponse, jsonEntrada);

                Logger.LogInfo("DistribuidorGenerico[{0}] - jsonResponse: '{1}' - statusCodeResponse: '{2}'".ToFormat(this.IdentificacaoDistribuidor, jsonResponse, statusCodeResponse));

                ret = new RetornoDistribuidor();

                if (statusCodeResponse.HasValue)
                {
                    ret.StatusCodeResponse = statusCodeResponse.Value;
                }

                if (jsonResponse.IsNotNull())
                {
                    ret.BodyDistribuidor = jsonResponse.JsonDeserializeObject<DistribuidorResponse>();
                }

                return ret;
            }
            catch (Exception ex)
            {
                Logger.LogError("DistribuidorGenerico[{0}] - Erro: '{1}'".ToFormat(this.IdentificacaoDistribuidor, ex.GetAllErrorDetail()), ex);
                throw ex;
            }
        }
        #endregion
    }
}