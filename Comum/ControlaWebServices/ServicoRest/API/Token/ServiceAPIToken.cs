using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.Base;
using Senac.Fecomercio.Entities.API.Token;
using System;
using System.Net;

namespace Senac.Fecomercio.ControlaWebServices.ServicoRest.API.Token
{
    public class ServiceAPIToken : ServicoBaseRest
    {
        #region Propriedades
        private DateTime DataExpiracao { get; set; }
        private string Token { get; set; }
        private string TokenType { get; set; }
        private string APIName { get; set; }
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
            return Extension.GetValueConfig("SERVICO_API_{0}_TOKEN_URL".ToFormat(APIName), true);
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
                this.TokenType = string.Empty;

                string hashAutorization = Extension.GetValueConfig("SERVICO_API_{0}_HASH_TOKEN".ToFormat(APIName), true);
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

                    Logger.LogInfo("ServiceAPIToken - Adicionando proxy - '{0}' - '{1}'".ToFormat(hostProxy, portaConv));
                    AddProxy(hostProxy, portaConv);
                }

                AddHeader("Authorization", hashAutorization);

                HttpStatusCode[] codeSucesso = new HttpStatusCode[3];
                codeSucesso[0] = HttpStatusCode.OK;
                codeSucesso[1] = HttpStatusCode.Created;
                codeSucesso[2] = HttpStatusCode.Accepted;

                HabilitarValidacaoSSL();

                Logger.LogInfo("ServiceAPIToken - Gerando Token");

                string jsonRetorno = CallService(codeSucesso, "{\"grant_type\": \"client_credentials\"}");

                Logger.LogInfo("ServiceAPIToken - GerarToken - JSON Retorno ({0})".ToFormat(jsonRetorno));

                if (jsonRetorno.IsNotNull())
                {
                    AccessTokenResponse retToken = Extension.JsonDeserializeObject<AccessTokenResponse>(jsonRetorno);

                    if (retToken.IsNotNull() && retToken.access_token.IsNotNull())
                    {
                        this.Token = retToken.access_token;
                        this.TokenType = retToken.token_type;
                        this.DataExpiracao = new DateTime();
                        this.DataExpiracao = DateTime.Now;

                        if (retToken.expires_in > 0)
                        {
                            this.DataExpiracao = DataExpiracao.AddMinutes(retToken.expires_in);
                        }
                        else
                        {
                            this.DataExpiracao = DataExpiracao.AddMinutes(60);
                        }
                    }
                    else
                    {
                        throw new Exception("Houve um erro ao gerar o Token da API Ambev - Objeto retorno inválido");
                    }
                }
                else
                {
                    throw new Exception("Houve um erro ao gerar o Token da API Ambev - Objeto retorno vazio");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Erro ao gerar o Token - '{0}'".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
        }

        public TokenResponse GetToken(EAPITokenCollection apiName)
        {
            TokenResponse retorno = null;
            try
            {
                this.APIName = Enum.GetName(typeof(EAPITokenCollection), apiName).ToString();

                GerarToken();

                retorno = new TokenResponse();
                retorno.Token = this.Token;
                retorno.TokenType = this.TokenType;
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