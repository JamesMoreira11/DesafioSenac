using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.Base;
using Senac.Fecomercio.Entities.Ambev.WebHook;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Senac.Fecomercio.ControlaWebServices.ServicoRest.API.Ambev.WebHook
{
    public class ServicoAPIWebHook : ServicoBaseRest
    {
        #region MetodosOverrides
        protected override string GetContentType()
        {
            return "application/json;charset=utf-8";
        }

        protected override string GetMethod()
        {
            return "POST";
        }

        protected override string GetUrl()
        {
            return Extension.GetValueConfig("AMBEV_SERVICO_API_WEBHOOK_URL", true);
        }

        protected override string GetUserAgent()
        {
            return "Apache-HttpClient/4.1.1 (java 1.5)";
        }
        #endregion

        #region Metodos
        public int? SendWebHookDelivery(DeliveryRequest entrada, string token, string tipoToken)
        {
            int? ret = null;
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

                    Logger.LogInfo("ServicoAPIWebHook - Adicionando proxy - '{0}' - '{1}'".ToFormat(hostProxy, portaConv));
                    AddProxy(hostProxy, portaConv);
                }

                Logger.LogInfo("ServicoAPIWebHook - Incluindo Authorization - '{0} {1}'".ToFormat(tipoToken, token));

                AddHeader("Authorization", "{0} {1}".ToFormat(tipoToken, token));

                HttpStatusCode[] codeSucesso = new HttpStatusCode[2];
                codeSucesso[0] = HttpStatusCode.Created;
                codeSucesso[1] = HttpStatusCode.Unauthorized;

                JsonSerializerSettings jsonSett = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddThh:mm:ssZ" };

                string jsonEntrada = entrada.JsonSerializeObject(jsonSett);

                Logger.LogInfo("ServicoAPIWebHook - jsonEntrada - '{0}'".ToFormat(jsonEntrada));

                HabilitarValidacaoSSL();

                int? statusCodeResponse = null;

                string result = CallService(codeSucesso, out statusCodeResponse, jsonEntrada);

                Logger.LogInfo("ServicoAPIWebHook - OK - Retorno '{0}'".ToFormat(result));

                if (statusCodeResponse.HasValue)
                {
                    ret = statusCodeResponse.Value;
                }

                return ret;
            }
            catch (Exception ex)
            {
                Logger.LogInfo("ServicoAPIWebHook - Erro ao Enviar o Delivery - '{0}'".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
        }
        #endregion
    }
}