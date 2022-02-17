using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.Base;
using Senac.Fecomercio.Entities.EnvioWpp;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Senac.Fecomercio.ControlaWebServices.ServicoRest.EnvioWhatsApp
{
    public class ServicoCadastrarWhatsApp : ServicoBaseRest
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
            return Extension.GetValueConfig("WHATSAPP_SERVICO_API_CADASTRO", true);
        }

        protected override string GetUserAgent()
        {
            return "Apache-HttpClient/4.1.1 (java 1.5)";
        }
        #endregion

        #region Metodos
        public int? SendCadastrarWhatsApp(CadastrarWhatsApp entrada, string token, string tipoToken)
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

                    Logger.LogInfo("ServicoCadastrarWhatsApp - Adicionando proxy - '{0}' - '{1}'".ToFormat(hostProxy, portaConv));
                    AddProxy(hostProxy, portaConv);
                }

                Logger.LogInfo("ServicoCadastrarWhatsApp - Incluindo Authorization");

                AddHeader("Authorization", "{0} {1}".ToFormat(tipoToken, token));

                HttpStatusCode[] codeSucesso = new HttpStatusCode[3];
                codeSucesso[0] = HttpStatusCode.OK;
                codeSucesso[1] = HttpStatusCode.Unauthorized;
                codeSucesso[2] = HttpStatusCode.Created;

                JsonSerializerSettings jsonSett = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddThh:mm:ssZ" };

                string jsonEntrada = entrada.JsonSerializeObject(jsonSett);

                //Logger.LogInfo("ServicoCadastrarWhatsApp - jsonEntrada - '{0}'".ToFormat(jsonEntrada));

                HabilitarValidacaoSSL();

                int? statusCodeResponse = null;

                string result = CallService(codeSucesso, out statusCodeResponse, jsonEntrada);

                Logger.LogInfo("ServicoCadastrarWhatsApp - Envio - Retorno '{0}'".ToFormat(statusCodeResponse.Value));

                if (statusCodeResponse.HasValue)
                {
                    ret = statusCodeResponse.Value;
                }

                return ret;
            }
            catch (Exception ex)
            {
                Logger.LogInfo("ServicoCadastrarWhatsApp - Erro ao Enviar o Delivery - '{0}'".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
        }
        #endregion
    }
}
