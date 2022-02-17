using Senac.Fecomercio.Common;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace Senac.Fecomercio.ControlaWebServices.Base
{
    public class WebClientBase : IDisposable
    {
        #region Construtor
        public WebClientBase()
        {
            wc = new WebClientWithTimeOut();
        }
        #endregion

        #region Propriedades
        private WebClientWithTimeOut wc = null;
        private object retornoServico;
        #endregion

        public void SetEncoding(Encoding encoding)
        {
            wc.Encoding = encoding;
        }

        public void AddHeader(string name, string value)
        {
            wc.Headers.Set(name, value);
        }

        public void SetUrl(string url)
        {
            wc.BaseAddress = url;
        }

        public void SetCredentials(ICredentials credencial)
        {
            wc.Credentials = credencial;
        }

        public void SetTimeOut(int timeOut)
        {
            wc.SetTimeOut(timeOut);
        }

        public XDocument CallMethodXml(string xmlEnvelope, string method = "POST")
        {
            string retorno = wc.UploadString(wc.BaseAddress, method, xmlEnvelope);

            if (retorno.IsNull())
            {
                throw new WebException("Não houve retorno no serviço chamado");
            }

            retorno = retorno.Replace("SOAP:Envelope", "SOAPEnvelope");
            retorno = retorno.Replace("SOAP:Header", "SOAPHeader");
            retorno = retorno.Replace("SOAP:Body", "SOAPBody");
            retorno = retorno.Replace("SOAP:Fault", "SOAPFault");

            retorno = retorno.Replace("<a:", "<");
            retorno = retorno.Replace("</a:", "</");
            retorno = retorno.Replace("xmlns:SOAP=", "xmlns=");
            retorno = retorno.Replace("xmlns:s=", "xmlns=");
            retorno = retorno.Replace("xmlns:a=", "xmlns=");
            retorno = retorno.Replace("xmlns:i=", "xmlns=");
            retorno = retorno.Replace("i:", "");

            retorno = retorno.Replace("xmlns='http://schemas.xmlsoap.org/soap/envelope/'", "");
            retorno = retorno.Replace("xmlns='http://tempuri.org/'", "");
            retorno = retorno.Replace("xmlns='http://schemas.datacontract.org/2004/07/WSVENDA_PAX.Modelos'", "");
            retorno = retorno.Replace("xmlns='http://www.w3.org/2001/XMLSchema-instance'", "");

            XDocument doc = XDocument.Parse(retorno);

            if (doc.Descendants("soapFault").Count() > 0)
            {
                throw new WebException(doc.Descendants("faultstring").ToList()[0].Value);
            }

            return doc;
        }

        public void Dispose()
        {
            if (wc.IsNotNull())
            {
                wc.Dispose();
                wc = null;
            }
        }
    }
}