using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.Base;
using Senac.Fecomercio.Entities.TratamentoExternoSegundoNivel;
using Senac.Fecomercio.Exceptions;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Senac.Fecomercio.ControlaWebServices.ServicoRest.API.TratamentoExternoSegundoNivel
{
    public class ServicoTratamentoExternoSegundoNivel : ServicoBaseRest
    {
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
            return Extension.GetValueConfig("INDIGO_SERVICO_URL", true);
        }

        protected override string GetUserAgent()
        {
            return "";
        }
        #endregion

        #region Metodos
        public void EnviarTratamentoExternoSegundoNivel(CreateTicketRequest entrada, string token, string tipoToken)
        {
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

                    Logger.LogInfo("EnviarTratamentoExternoSegundoNivel - Adicionando proxy - '{0}' - '{1}'".ToFormat(hostProxy, portaConv));
                    AddProxy(hostProxy, portaConv);
                }

                Logger.LogInfo("EnviarTratamentoExternoSegundoNivel - Incluindo Authorization - '{0} {1}'".ToFormat(tipoToken, token));

                AddHeader("Authorization", "{0} {1}".ToFormat(tipoToken, token));

                HttpStatusCode[] codeSucesso = new HttpStatusCode[3];
                codeSucesso[0] = HttpStatusCode.Accepted;
                codeSucesso[1] = (HttpStatusCode)409;
                codeSucesso[2] = (HttpStatusCode)422;

                JsonSerializerSettings jsonSett = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddTHH:mm:ss" };

                string jsonEntrada = entrada.JsonSerializeObject(jsonSett);

                Logger.LogInfo("EnviarTratamentoExternoSegundoNivel - jsonEntrada - '{0}'".ToFormat(jsonEntrada));

                HabilitarValidacaoSSL();

                int? statusCodeResponse = null;

                string result = CallService(codeSucesso, out statusCodeResponse, jsonEntrada);

                if (statusCodeResponse.HasValue && (statusCodeResponse.Value == 422))
                {
                    throw new BusinessException("Ocorreu um erro ao comunicar com o serviço de tratamento. Retorno: '{0}'".ToFormat(result));
                }

                Logger.LogInfo("EnviarTratamentoExternoSegundoNivel - OK - Retorno '{0}'".ToFormat(result));
            }
            catch (Exception ex)
            {
                Logger.LogInfo("EnviarTratamentoExternoSegundoNivel - Erro ao Enviar o Delivery - '{0}'".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
        }
        #endregion
    }
}