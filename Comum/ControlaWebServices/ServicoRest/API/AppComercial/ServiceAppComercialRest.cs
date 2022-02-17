using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.Base;
using Senac.Fecomercio.Entities.AppComercial;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Senac.Fecomercio.ControlaWebServices.ServicoRest.API.AppComercial
{
    public class ServiceAppComercialRest : ServicoBaseRest
    {
        #region MetodosOverrides
        protected override string GetContentType()
        {
            return "application/json";
        }

        protected override string GetUrl()
        {
            return Extension.GetValueConfig("APP_COMERCIAL_SERVICO_NOTIFICACAO_URL", true);
        }

        protected override string GetUserAgent()
        {
            return "Apache-HttpClient/4.1.1 (java 1.5)";
        }

        protected override string GetMethod()
        {
            return Extension.GetValueConfig("APP_COMERCIAL_SERVICO_NOTIFICACAO_METHOD", true);
        }
        #endregion

        #region Metodos
        public NotificaStatusTrackingResponse EnviarChamado(NotificaStatusTrackingRequest entrada, string token)
        {
            NotificaStatusTrackingResponse ret = null;
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

                    Logger.LogInfo("ServiceAppComercialRest - Adicionando proxy - '{0}' - '{1}'".ToFormat(hostProxy, portaConv));
                    AddProxy(hostProxy, portaConv);
                }

                Logger.LogInfo("ServiceAppComercialRest - Incluindo Authorization - '{0}'".ToFormat(token));

                AddHeader("cvd-api-token", "{0}".ToFormat(token));

                HabilitarValidacaoSSL();

                HttpStatusCode[] codeSucesso = new HttpStatusCode[6];
                codeSucesso[0] = HttpStatusCode.OK;
                codeSucesso[1] = HttpStatusCode.NotAcceptable;
                codeSucesso[2] = HttpStatusCode.Unauthorized;
                codeSucesso[3] = HttpStatusCode.Forbidden;
                codeSucesso[4] = HttpStatusCode.BadRequest;
                codeSucesso[5] = HttpStatusCode.NotFound;

                JsonSerializerSettings jsonSett = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ" };

                string jsonEntrada = entrada.JsonSerializeObject(jsonSett);

                Logger.LogInfo("ServiceAppComercialRest - jsonEntrada - '{0}'".ToFormat(jsonEntrada));
                int? statusCodeResponse = null;

                string jsonResponse = CallService(codeSucesso, out statusCodeResponse, jsonEntrada);

                Logger.LogInfo("ServiceAppComercialRest - jsonResponse: '{0}' - statusCodeResponse: '{1}'".ToFormat(jsonResponse, statusCodeResponse));

                if (statusCodeResponse.HasValue)
                {
                    ret = new NotificaStatusTrackingResponse();
                    ret.HttpStatusCode = (HttpStatusCode)statusCodeResponse.Value;

                    if (jsonResponse.IsNotNull())
                    {
                        ret.ResponseError = Extension.JsonDeserializeObject<NotificaStatusTrackingResponseError>(jsonResponse);
                    }
                }

                return ret;
            }
            catch (Exception ex)
            {
                Logger.LogError("ServiceAppComercialRest - Erro: '{0}'".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
        }
        #endregion
    }
}