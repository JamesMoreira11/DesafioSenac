using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.Base;
using Senac.Fecomercio.Entities.OperadorExterno.Hero.Itrack;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Senac.Fecomercio.ControlaWebServices.ServicoRest.API.Hero.Itrack
{
    public class ITrackGenerico : ServicoBaseRest
    {
        #region Construtor
        public ITrackGenerico()
            : base()
        {

        }
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
            return Extension.GetValueConfig("API_SERVICO_HERO_ITRACK_URL", true);
        }

        protected override string GetUserAgent()
        {
            return "";
        }
        #endregion

        #region Metodos
        public RetornoItrack EnviarChamado(AberturaChamadoItrack entrada, string token, string tipoToken)
        {
            RetornoItrack ret = null;
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

                    Logger.LogInfo("ITrackGenerico - Adicionando proxy - '{0}' - '{1}'".ToFormat(hostProxy, portaConv));
                    AddProxy(hostProxy, portaConv);
                }

                Logger.LogInfo("ITrackGenerico - Incluindo Authorization - '{0} {1}'".ToFormat(tipoToken, token));

                AddHeader("Authorization", "{0} {1}".ToFormat(tipoToken, token));

                HabilitarValidacaoSSL();

                HttpStatusCode[] codeSucesso = new HttpStatusCode[7];
                codeSucesso[0] = HttpStatusCode.Created;
                codeSucesso[1] = HttpStatusCode.BadRequest;
                codeSucesso[2] = HttpStatusCode.BadGateway;
                codeSucesso[3] = HttpStatusCode.NotAcceptable;
                codeSucesso[4] = HttpStatusCode.Unauthorized;
                codeSucesso[5] = HttpStatusCode.Forbidden;
                codeSucesso[6] = (HttpStatusCode)422;

                JsonSerializerSettings jsonSett = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd HH:mm:ss.fff" };

                string jsonEntrada = entrada.JsonSerializeObject(jsonSett);

                Logger.LogInfo("ITrackGenerico - jsonEntrada - '{0}'".ToFormat(jsonEntrada));
                int? statusCodeResponse = null;

                string jsonResponse = CallService(codeSucesso, out statusCodeResponse, jsonEntrada);

                Logger.LogInfo("ITrackGenerico - jsonResponse: '{0}' - statusCodeResponse: '{1}'".ToFormat(jsonResponse, statusCodeResponse));

                ret = new RetornoItrack();

                if (statusCodeResponse.HasValue)
                {
                    ret.StatusCodeResponse = statusCodeResponse.Value;
                }

                if (jsonResponse.IsNotNull())
                {
                    ret.BodyHeroItrack = jsonResponse.JsonDeserializeObject<HeroItrackResponse>();
                }

                return ret;
            }
            catch (Exception ex)
            {
                Logger.LogError("ITrackGenerico - Erro: '{0}'".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
        }
        #endregion
    }
}