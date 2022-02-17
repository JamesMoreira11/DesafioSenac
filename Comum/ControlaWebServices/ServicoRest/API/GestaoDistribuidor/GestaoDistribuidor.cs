using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.Base;
using Senac.Fecomercio.Entities.Domain.Terminal.Distribuidor;
using System;
using System.Net;

namespace Senac.Fecomercio.ControlaWebServices.ServicoRest.API.GestaoDistribuidor
{
    public class GestaoDistribuidor : ServicoBaseRest
    {
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
            return Extension.GetValueConfig("GD_SERVICO_DISTRIBUIDOR_URL", true);
        }

        protected override string GetUserAgent()
        {
            return "";
        }

        protected override bool RemoverSujeitaJson()
        {
            return false;
        }
        #endregion

        #region Metodos
        public DistribuidorResponse ConsultaDistribuidorPorEcLogico(string numeroEstabelecimentoComercial, int numeroLogico, string token, string tipoToken)
        {
            DistribuidorResponse ret = null;
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

                    Logger.LogInfo("Distribuidor - Adicionando proxy - '{0}' - '{1}'".ToFormat(hostProxy, portaConv));
                    AddProxy(hostProxy, portaConv);
                }

                Logger.LogInfo("Distribuidor - Incluindo Authorization - '{0} {1}'".ToFormat(tipoToken, token));

                AddHeader("Authorization", "{0} {1}".ToFormat(tipoToken, token));

                HabilitarValidacaoSSL();

                string[] entrada = new string[4];
                entrada[0] = "ec-code";
                entrada[1] = numeroEstabelecimentoComercial;
                entrada[2] = "logical-number";
                entrada[3] = numeroLogico.ToString();

                HttpStatusCode[] codeSucesso = new HttpStatusCode[3];
                codeSucesso[0] = HttpStatusCode.OK;

                string jsonRetorno = CallServiceGet(codeSucesso, entrada);

                if (jsonRetorno.IsNotNull())
                {
                    ret = Extension.JsonDeserializeObject<DistribuidorResponse>(jsonRetorno);
                }
                else
                {
                    Logger.LogDebug("Distribuidor - Não foi possível realizar a consulta do EC '{0}' e lógico '{1}' no serviço do GD".ToFormat(numeroEstabelecimentoComercial, numeroLogico));
                }

                return ret;
            }
            catch (Exception ex)
            {
                Logger.LogError("Distribuidor - Erro: '{0}'".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
        }
        #endregion
    }
}