using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.Base;
using Senac.Fecomercio.Entities.API.Token;
using System;
using System.Net;

namespace Senac.Fecomercio.ControlaWebServices.ServicoRest.API.AppComercial
{
    public class TokenAppComercial : ServicoBaseRest
    {
        #region Propriedades
        private DateTime DataExpiracao { get; set; }
        private string Token { get; set; }
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
            return Extension.GetValueConfig("APP_COMERCIAL_SERVICO_TOKEN_URL", true);
        }

        protected override string GetUserAgent()
        {
            return "Apache-HttpClient/4.1.1 (java 1.5)";
        }
        #endregion

        #region Metodos
        private void GerarToken()
        {
            try
            {
                this.Token = string.Empty;
                this.DataExpiracao = DateTime.Now;
                var crypt = new Crypt();

                string cvdApiUserName = crypt.Decrypt(Extension.GetValueConfig("APP_COMERCIAL_SERVICO_TOKEN_CVD_API_USERNAME", true));
                string cvdApiPassword = crypt.Decrypt(Extension.GetValueConfig("APP_COMERCIAL_SERVICO_TOKEN_CVD_API_PASSWORD", true));
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

                    Logger.LogInfo("TokenAppComercial - Adicionando proxy - '{0}' - '{1}'".ToFormat(hostProxy, portaConv));
                    AddProxy(hostProxy, portaConv);
                }

                Logger.LogInfo("TokenAppComercial - Inserindo username '{0}'".ToFormat(cvdApiUserName));

                AddHeader("cvd-api-username", cvdApiUserName);
                AddHeader("cvd-api-password", cvdApiPassword);

                HttpStatusCode[] codeSucesso = new HttpStatusCode[1];
                codeSucesso[0] = HttpStatusCode.OK;

                HabilitarValidacaoSSL();
                //DesabilitarValidacaoSSL();

                Logger.LogInfo("TokenAppComercial - Gerando Token");

                string jsonRetorno = CallServiceGet(codeSucesso);

                Logger.LogInfo("TokenAppComercial - GerarToken - JSON Retorno ({0})".ToFormat(jsonRetorno));

                if (jsonRetorno.IsNotNull())
                {
                    TokenAppComercialResponse retToken = Extension.JsonDeserializeObject<TokenAppComercialResponse>(jsonRetorno);

                    if (retToken.IsNotNull() && retToken.token.IsNotNull())
                    {
                        this.Token = retToken.token;
                        this.DataExpiracao = new DateTime();
                        this.DataExpiracao = retToken.expiracao;
                    }
                    else
                    {
                        throw new Exception("Houve um erro ao gerar o Token da API AppComercial - Objeto retorno inválido");
                    }
                }
                else
                {
                    throw new Exception("Houve um erro ao gerar o Token da API AppComercial - Objeto retorno vazio");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Erro ao gerar o Token - '{0}'".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
        }

        public TokenResponse GetToken()
        {
            TokenResponse retorno = null;
            try
            {
                GerarToken();

                retorno = new TokenResponse();
                retorno.Token = this.Token;
                retorno.DateExpire = this.DataExpiracao;

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}