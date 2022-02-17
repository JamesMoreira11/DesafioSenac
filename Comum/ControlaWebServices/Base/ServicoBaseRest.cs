using Senac.Fecomercio.Common;
using Senac.Fecomercio.Entities.Servico.WebResponse;
using Senac.Fecomercio.Exceptions.Servico;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Senac.Fecomercio.ControlaWebServices.Base
{
    public abstract class ServicoBaseRest : IDisposable
    {
        #region Propriedades
        private WebHeaderCollection HeadersAdd { get; set; }
        private WebProxy ProxyRequest { get; set; }
        private bool DisableValidateSSL { get; set; }
        private bool IsCredencialCache { get; set; }
        private string CredencialCacheUsuario { get; set; }
        private string CredencialCacheSenha { get; set; }
        private string CredencialCacheAuthType { get; set; }
        #endregion

        #region MetodosAbstratos
        protected abstract string GetUrl();
        protected abstract string GetUserAgent();
        protected abstract string GetContentType();
        protected abstract string GetMethod();

        protected bool GetKeepAlive()
        {
            return false;
        }

        protected virtual bool RemoverSujeitaJson()
        {
            return true;
        }
        #endregion

        #region Metodos
        #region CallPostPatchEtc
        public string CallService(string entrada = null)
        {
            HttpStatusCode[] codeSucesso = new HttpStatusCode[1];
            codeSucesso[0] = HttpStatusCode.OK;

            return CallService(codeSucesso, entrada);
        }

        public string CallService(HttpStatusCode[] httpCodesSucesso, string entrada = null)
        {
            int? responseCode = null;
            return CallService(httpCodesSucesso, out responseCode, entrada);
        }

        public string CallService(HttpStatusCode[] httpCodesSucesso, out int? statusCodeResponse, string entrada = null)
        {
            string ret = string.Empty;
            HttpWebRequest request = null;
            StringBuilder sbSource = null;
            try
            {
                statusCodeResponse = null;

                string urlServico = GetUrl();

                string habilitaDumb = Extension.GetValueConfig("GTEC_DUMP_OBJECT", false);

                if (habilitaDumb.IsNull())
                {
                    habilitaDumb = "0";
                }

                if (urlServico.IsNull())
                {
                    throw new ArgumentNullException("Não foi possível recuperar a URL do serviço");
                }

                Uri addr = new Uri(urlServico);

                ServicePointManager.ServerCertificateValidationCallback = (object a, System.Security.Cryptography.X509Certificates.X509Certificate b, System.Security.Cryptography.X509Certificates.X509Chain c, System.Net.Security.SslPolicyErrors d) => { return true; };

                byte[] bytes = null;

                if (entrada.IsNotNull())
                {
                    if (habilitaDumb.Equals("1"))
                    {
                        Logger.LogInfo("CallService - Dump Request > '{0}'".ToFormat(entrada));
                    }

                    bytes = Encoding.UTF8.GetBytes(entrada);
                }

                //Create and initialize the web request  
                request = WebRequest.Create(addr) as HttpWebRequest;

                if (HeadersAdd.IsNotNull() && HeadersAdd.Count > 0)
                {
                    request.Headers = HeadersAdd;
                }

                if (bytes.IsNotNull())
                {
                    request.ContentLength = bytes.Length;
                    request.SendChunked = true;
                }

                request.UserAgent = GetUserAgent();
                request.ContentType = GetContentType();

                request.Method = GetMethod();
                request.KeepAlive = GetKeepAlive();

                if (DisableValidateSSL)
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.DefaultConnectionLimit = 9999;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                }

                if (ExisteProxy())
                {
                    Logger.LogDebug("Adicionando proxy - BaseRest");

                    //ICredentials credentiasl = new NetworkCredential("t2411msl", "", "VISANET");
                    //this.ProxyRequest.Credentials = credentiasl;

                    request.Proxy = this.ProxyRequest;
                }

                if (IsCredencialCache)
                {
                    NetworkCredential myNetworkCredential = new NetworkCredential(this.CredencialCacheUsuario, this.CredencialCacheSenha);
                    CredentialCache myCredentialCache = new CredentialCache();
                    myCredentialCache.Add(addr, this.CredencialCacheAuthType, myNetworkCredential);

                    request.PreAuthenticate = true;
                    request.Credentials = myCredentialCache;
                }

                try
                {
                    Logger.LogDebug("Begin - BaseRest - GetRequestStream");
                    using (Stream requestStream2 = request.GetRequestStream())
                    {
                        requestStream2.Write(bytes, 0, bytes.Length);
                        requestStream2.Flush();
                        requestStream2.Close();
                    }
                    Logger.LogDebug("End - BaseRest - GetRequestStream");

                    //Get response  
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        if (request.HaveResponse == true && response.IsNotNull())
                        {
                            statusCodeResponse = (int)response.StatusCode;

                            if (httpCodesSucesso.Contains(response.StatusCode))
                            {
                                using (Stream objStream = response.GetResponseStream())
                                {
                                    using (StreamReader objReader = new StreamReader(objStream))
                                    {
                                        sbSource = new StringBuilder(objReader.ReadToEnd());
                                        objReader.Close();
                                    }
                                    objStream.Flush();
                                    objStream.Close();
                                }

                                ret = sbSource.ToString();

                                if (habilitaDumb.Equals("1"))
                                {
                                    Logger.LogInfo("CallService - Dump Response > '{0}'".ToFormat(ret));
                                }

                                if (RemoverSujeitaJson())
                                {
                                    ret = ret.Replace("s:", "").Replace(":a", "").Replace("a:", "").Replace("i:", "").Replace("b:", "");
                                }
                            }
                            else
                            {
                                response.Close();
                                throw new Exception("Houve uma falha na comunicação do serviço Rest - Status Code: '{0}'".ToFormat(response.StatusCode));
                            }
                        }
                        else
                        {
                            Logger.LogDebug("CallService - Não houve resposta na chamada do serviço");
                        }

                        response.Close();
                    }
                }
                catch (WebException subWex)
                {
                    if (subWex.Status == WebExceptionStatus.ProtocolError)
                    {
                        using (HttpWebResponse responseEx = subWex.Response as HttpWebResponse)
                        {
                            if (responseEx.IsNotNull())
                            {
                                statusCodeResponse = (int)responseEx.StatusCode;

                                if (httpCodesSucesso.Contains(responseEx.StatusCode))
                                {
                                    using (Stream objStream = responseEx.GetResponseStream())
                                    {
                                        using (StreamReader objReader = new StreamReader(objStream))
                                        {
                                            sbSource = new StringBuilder(objReader.ReadToEnd());
                                            objReader.Close();
                                        }
                                        objStream.Flush();
                                        objStream.Close();
                                    }

                                    ret = sbSource.ToString();

                                    if (habilitaDumb.Equals("1"))
                                    {
                                        Logger.LogInfo("CallService - Dump Response > '{0}'".ToFormat(ret));
                                    }

                                    if (RemoverSujeitaJson())
                                    {
                                        ret = ret.Replace("s:", "").Replace(":a", "").Replace("a:", "").Replace("i:", "").Replace("b:", "");
                                    }
                                }
                                else
                                {
                                    responseEx.Close();
                                    throw new Exception("Houve uma falha na comunicação do serviço Rest - Status Code: '{0}'".ToFormat(responseEx.StatusCode));
                                }
                            }
                            else
                            {
                                responseEx.Close();
                                throw subWex;
                            }

                            responseEx.Close();
                        }
                    }
                    else
                    {
                        throw subWex;
                    }
                }
                catch (Exception subex)
                {
                    throw subex;
                }

                return ret;
            }
            catch (WebException wex)
            {
                Logger.LogError("Erro ao comunicar Rest - {0}".ToFormat(wex.GetAllErrorDetail()), wex);

                ErroComunicacaoException erroCom = null;
                try
                {
                    using (WebResponse errResp = wex.Response)
                    {
                        using (Stream respStream = errResp.GetResponseStream())
                        {
                            string text = null;
                            using (StreamReader readerError = new StreamReader(respStream))
                            {
                                text = readerError.ReadToEnd();
                                text = text.Left(text.Length - 1);
                                text = text.Right(text.Length - 1);

                                readerError.Close();
                            }
                            respStream.Flush();
                            respStream.Close();

                            if (text.IsNotNull())
                            {
                                WebRespondeError retServico = Extension.JsonDeserializeObject<WebRespondeError>(text);

                                erroCom = new ErroComunicacaoException(retServico.code, retServico.message);
                            }
                        }

                        errResp.Close();
                    }
                }
                catch
                { }

                if (erroCom.IsNotNull())
                {
                    throw erroCom;
                }
                else
                {
                    throw wex;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Erro ao comunicar Rest - {0}".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
            finally
            {
                if (request.IsNotNull())
                {
                    request = null;
                }

                if (sbSource.IsNotNull())
                {
                    sbSource = null;
                }
            }
        }
        #endregion

        #region GET
        public string CallServiceGet(string[] entrada = null)
        {
            HttpStatusCode[] codeSucesso = new HttpStatusCode[1];
            codeSucesso[0] = HttpStatusCode.OK;

            int? responseCode = null;

            return CallServiceGet(codeSucesso, out responseCode, entrada);
        }

        public string CallServiceGet(HttpStatusCode[] httpCodesSucesso, string[] entrada = null)
        {
            /*HttpStatusCode[] codeSucesso = new HttpStatusCode[1];
            codeSucesso[0] = HttpStatusCode.OK;*/

            int? responseCode = null;

            return CallServiceGet(httpCodesSucesso, out responseCode, entrada);
        }

        public string CallServiceGet(HttpStatusCode[] httpCodesSucesso, out int? statusCodeResponse, string[] entrada = null)
        {
            string ret = string.Empty;
            HttpWebRequest request = null;
            StringBuilder sbSource = null;
            try
            {
                statusCodeResponse = null;

                string urlServico = GetUrl();

                string habilitaDumb = Extension.GetValueConfig("GTEC_DUMP_OBJECT", false);

                if (habilitaDumb.IsNull())
                {
                    habilitaDumb = "0";
                }

                if (urlServico.IsNull())
                {
                    throw new ArgumentNullException("Não foi possível recuperar a URL do serviço");
                }

                string urlServicoWithParam = "{0}".ToFormat(urlServico);

                if (entrada.IsNotNull() && entrada.Length > 0)
                {
                    for (int i = 0; i < entrada.Length; i++)
                    {
                        urlServicoWithParam += "/{0}".ToFormat(entrada[i]);
                    }
                }

                Uri addr = new Uri(urlServicoWithParam);

                request = WebRequest.Create(addr) as HttpWebRequest;

                ServicePointManager.ServerCertificateValidationCallback = (object a, System.Security.Cryptography.X509Certificates.X509Certificate b, System.Security.Cryptography.X509Certificates.X509Chain c, System.Net.Security.SslPolicyErrors d) => { return true; };

                //Create and initialize the web request  

                request.UserAgent = GetUserAgent();
                request.Method = GetMethod();

                if (HeadersAdd.IsNotNull() && HeadersAdd.Count > 0)
                {
                    request.Headers = HeadersAdd;
                }

                if (ExisteProxy())
                {
                    //ICredentials credentials = new NetworkCredential("t2411msl", "", "VISANET");
                    //this.ProxyRequest.Credentials = credentials;

                    request.Proxy = this.ProxyRequest;
                }

                if (DisableValidateSSL)
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    ServicePointManager.ServerCertificateValidationCallback = (snder, cert, chain, error) => true;
                }

                try
                {
                    //Get response  
                    using (HttpWebResponse response2 = request.GetResponse() as HttpWebResponse)
                    {
                        if (request.HaveResponse == true && response2.IsNotNull())
                        {
                            statusCodeResponse = (int)response2.StatusCode;

                            if (httpCodesSucesso.Contains(response2.StatusCode))
                            {
                                // Get the response stream  
                                using (Stream reader2 = response2.GetResponseStream())
                                {
                                    using (StreamReader objReader = new StreamReader(reader2))
                                    {
                                        // Read it into a StringBuilder  
                                        sbSource = new StringBuilder(objReader.ReadToEnd());
                                        objReader.Close();
                                    }
                                    reader2.Flush();
                                    reader2.Close();
                                }
                                response2.Close();

                                ret = sbSource.ToString();

                                if (habilitaDumb.Equals("1"))
                                {
                                    Logger.LogInfo("CallService - Dump Response > '{0}'".ToFormat(ret));
                                }

                                if (RemoverSujeitaJson())
                                {
                                    ret = ret.Replace("s:", "").Replace(":a", "").Replace("a:", "").Replace("i:", "").Replace("b:", "");
                                }
                            }
                            else
                            {
                                throw new Exception("Houve uma falha na comunicação do serviço Rest - Status Code: '{0}'".ToFormat(statusCodeResponse));
                            }
                        }
                    }
                }
                catch (WebException subWex)
                {
                    if (subWex.Status == WebExceptionStatus.ProtocolError)
                    {
                        using (HttpWebResponse responseEx = subWex.Response as HttpWebResponse)
                        {
                            if (responseEx.IsNotNull())
                            {
                                statusCodeResponse = (int)responseEx.StatusCode;

                                if (httpCodesSucesso.Contains(responseEx.StatusCode))
                                {
                                    using (Stream objStream = responseEx.GetResponseStream())
                                    {
                                        using (StreamReader reader = new StreamReader(objStream))
                                        {
                                            sbSource = new StringBuilder(reader.ReadToEnd());
                                            reader.Close();
                                        }
                                        objStream.Flush();
                                        objStream.Close();
                                    }

                                    ret = sbSource.ToString();

                                    if (habilitaDumb.Equals("1"))
                                    {
                                        Logger.LogInfo("CallServiceGet - Dump Response > '{0}'".ToFormat(ret));
                                    }

                                    if (RemoverSujeitaJson())
                                    {
                                        ret = ret.Replace("s:", "").Replace(":a", "").Replace("a:", "").Replace("i:", "").Replace("b:", "");
                                    }
                                }
                                else
                                {
                                    throw new Exception("Houve uma falha na comunicação do serviço Rest - Status Code: '{0}'".ToFormat(responseEx.StatusCode));
                                }
                            }
                            else
                            {
                                throw subWex;
                            }

                            responseEx.Close();
                        }
                    }
                    else
                    {
                        throw subWex;
                    }
                }
                catch (Exception subex)
                {
                    throw subex;
                }

                return ret;
            }
            catch (WebException wex)
            {
                ErroComunicacaoException erroCom = null;
                try
                {
                    using (WebResponse errResp = wex.Response)
                    {
                        using (Stream respStream = errResp.GetResponseStream())
                        {
                            string text = null;
                            using (StreamReader readerError = new StreamReader(respStream))
                            {
                                text = readerError.ReadToEnd();
                                text = text.Left(text.Length - 1);
                                text = text.Right(text.Length - 1);

                                readerError.Close();
                            }
                            respStream.Flush();
                            respStream.Close();

                            if (text.IsNotNull())
                            {
                                WebRespondeError retServico = Extension.JsonDeserializeObject<WebRespondeError>(text);

                                erroCom = new ErroComunicacaoException(retServico.code, retServico.message);
                            }
                        }

                        errResp.Close();
                    }
                }
                catch
                { }

                if (erroCom.IsNotNull())
                {
                    throw erroCom;
                }
                else
                {
                    throw wex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (request.IsNotNull())
                {
                    request = null;
                }

                if (sbSource.IsNotNull())
                {
                    sbSource = null;
                }
            }
        }
        #endregion

        public async Task<string> CallServiceAsync(HttpStatusCode[] httpCodesSucesso, string entrada = null)
        {
            string ret = string.Empty;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader reader = null;
            StringBuilder sbSource = null;
            try
            {
                //HttpClient x = new HttpClient();

                string urlServico = GetUrl();

                string habilitaDumb = Extension.GetValueConfig("GTEC_DUMP_OBJECT", false);

                if (habilitaDumb.IsNull())
                {
                    habilitaDumb = "0";
                }

                if (urlServico.IsNull())
                {
                    throw new ArgumentNullException("Não foi possível recuperar a URL do serviço");
                }

                Uri addr = new Uri(urlServico);

                ServicePointManager.ServerCertificateValidationCallback = (object a, System.Security.Cryptography.X509Certificates.X509Certificate b, System.Security.Cryptography.X509Certificates.X509Chain c, System.Net.Security.SslPolicyErrors d) => { return true; };

                byte[] bytes = null;

                if (entrada.IsNotNull())
                {
                    if (habilitaDumb.Equals("1"))
                    {
                        Logger.LogInfo("CallServiceAsync - Dump Request > '{0}'".ToFormat(entrada));
                    }

                    bytes = Encoding.UTF8.GetBytes(entrada);
                }

                //Create and initialize the web request  
                request = WebRequest.Create(addr) as HttpWebRequest;
                request.UserAgent = GetUserAgent();
                request.ContentType = GetContentType();

                if (HeadersAdd.IsNotNull() && HeadersAdd.Count > 0)
                {
                    request.Headers = HeadersAdd;
                }

                if (bytes.IsNotNull())
                {
                    request.ContentLength = bytes.Length;
                    request.SendChunked = true;
                }

                request.Method = GetMethod();
                request.KeepAlive = GetKeepAlive();

                if (DisableValidateSSL)
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    ServicePointManager.ServerCertificateValidationCallback = (snder, cert, chain, error) => true;
                }

                if (ExisteProxy())
                {
                    Logger.LogDebug("CallServiceAsync - Adicionando proxy - BaseRest");
                    request.Proxy = this.ProxyRequest;
                }

                Logger.LogDebug("Begin - BaseRest - CallServiceAsync - GetRequestStream");
                Stream requestStream = request.GetRequestStream();
                Logger.LogDebug("End - BaseRest - CallServiceAsync - GetRequestStream");

                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();

                //Get response  
                response = await GetResponseAsync(request);

                if (request.HaveResponse == true && response.IsNotNull())
                {
                    if (httpCodesSucesso.Contains(response.StatusCode))
                    {
                        // Get the response stream  
                        reader = new StreamReader(response.GetResponseStream());

                        // Read it into a StringBuilder  
                        sbSource = new StringBuilder(reader.ReadToEnd());

                        ret = sbSource.ToString();

                        if (habilitaDumb.Equals("1"))
                        {
                            Logger.LogInfo("CallServiceAsync - Dump Response > '{0}'".ToFormat(ret));
                        }

                        if (RemoverSujeitaJson())
                        {
                            ret = ret.Replace("s:", "").Replace(":a", "").Replace("a:", "").Replace("i:", "").Replace("b:", "");
                        }
                    }
                    else
                    {
                        throw new Exception("Houve uma falha na comunicação do serviço Rest - CallServiceAsync - Status Code: '{0}'".ToFormat(response.StatusCode));
                    }
                }

                return ret;
            }
            catch (WebException wex)
            {
                Logger.LogError("Erro ao comunicar Rest - CallServiceAsync - {0}".ToFormat(wex.GetAllErrorDetail()), wex);

                ErroComunicacaoException erroCom = null;
                try
                {
                    WebResponse errResp = wex.Response;
                    using (Stream respStream = errResp.GetResponseStream())
                    {
                        StreamReader readerError = new StreamReader(respStream);
                        string text = readerError.ReadToEnd();
                        text = text.Left(text.Length - 1);
                        text = text.Right(text.Length - 1);
                        WebRespondeError retServico = Extension.JsonDeserializeObject<WebRespondeError>(text);

                        erroCom = new ErroComunicacaoException(retServico.code, retServico.message);
                    }
                }
                catch
                { }

                if (erroCom.IsNotNull())
                {
                    throw erroCom;
                }
                else
                {
                    throw wex;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Erro ao comunicar Rest - CallServiceAsync - {0}".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
            finally
            {
                if (request.IsNotNull())
                {
                    request = null;
                }

                if (response.IsNotNull())
                {
                    response.Close();
                    response.Dispose();
                    response = null;
                }

                if (reader.IsNotNull())
                {
                    reader.Close();
                    reader.Dispose();
                    reader = null;
                }

                if (sbSource.IsNotNull())
                {
                    sbSource = null;
                }
            }
        }

        private async Task<HttpWebResponse> GetResponseAsync(HttpWebRequest request)
        {
            try
            {
                var resp = await request.GetResponseAsync();
                return (HttpWebResponse)resp;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        protected void AddProxy(string host, int port)
        {
            ProxyRequest = new WebProxy(host, port);
        }

        protected void HabilitarValidacaoSSL()
        {
            this.DisableValidateSSL = true;
        }

        protected void DesabilitarValidacaoSSL()
        {
            this.DisableValidateSSL = false;
        }

        protected void AddHeader(string key, string value)
        {
            if (HeadersAdd.IsNull())
            {
                HeadersAdd = new WebHeaderCollection();
            }

            HeadersAdd.Add(key, value);
        }

        private bool ExisteProxy()
        {
            bool ret = false;
            if (ProxyRequest.IsNotNull())
            {
                ret = true;
            }
            return ret;
        }

        protected void AddCredentialCache(string usuario, string senha, string authType)
        {
            this.IsCredencialCache = true;
            this.CredencialCacheUsuario = usuario;
            this.CredencialCacheSenha = senha;
            this.CredencialCacheAuthType = authType;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {

        }
        #endregion
    }
}