﻿using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.Base;
using Senac.Fecomercio.Entities.EnvioWpp;
using Newtonsoft.Json;
using System;
using System.Net;
using Senac.Fecomercio.Entities.Servico.WebResponse;

namespace Senac.Fecomercio.ControlaWebServices.ServicoRest.EnvioWhatsApp
{
    public class ServicoTokenWhatsApp : ServicoBaseRest
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
            return Extension.GetValueConfig("SERVICO_API_WHATSAPP_TOKEN_URL", true);
        }

        protected override string GetUserAgent()
        {
            return "Apache-HttpClient/4.1.1 (java 1.5)";
        }
        #endregion

        #region Metodos
        public string SendTokenWhatsApp(TokenWhatsApp entrada)
        {
            string ret = null;
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

                    Logger.LogInfo("ServicoTokenWhatsApp - Adicionando proxy - '{0}' - '{1}'".ToFormat(hostProxy, portaConv));
                    AddProxy(hostProxy, portaConv);
                }

                Logger.LogInfo("ServicoTokenWhatsApp - Incluindo Authorization ");

                AddHeader("Authorization", "{0} {1}".ToFormat(entrada.username, entrada.password));

                HttpStatusCode[] codeSucesso = new HttpStatusCode[2];
                codeSucesso[0] = HttpStatusCode.OK;
                codeSucesso[1] = HttpStatusCode.Unauthorized;

                JsonSerializerSettings jsonSett = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddThh:mm:ssZ" };

                string jsonEntrada = entrada.JsonSerializeObject(jsonSett);

                //Logger.LogInfo("ServicoTokenWhatsApp - jsonEntrada - '{0}'".ToFormat(jsonEntrada));

                HabilitarValidacaoSSL();

                int? statusCodeResponse = null;

                string result = CallService(codeSucesso, out statusCodeResponse, jsonEntrada);

                RetornoTokenWhatsApp retServico = Extension.JsonDeserializeObject<RetornoTokenWhatsApp>(result);

                Logger.LogInfo("ServicoTokenWhatsApp - OK");

                if (statusCodeResponse.HasValue)
                {
                    ret = retServico.access.ToString();
                }

                return ret;
            }
            catch (Exception ex)
            {
                Logger.LogInfo("ServicoTokenWhatsApp - Erro ao Enviar o Delivery - '{0}'".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
        }
        #endregion
    }
}
