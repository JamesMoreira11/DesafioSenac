using System;
using System.Net;

namespace Senac.Fecomercio.ControlaWebServices.Base
{
    public class WebClientWithTimeOut : WebClient
    {
        public WebClientWithTimeOut() : base()
        {
            this.SetTimeOut(120000);
        }

        private int timeOut { get; set; }

        public void SetTimeOut(int timeOut)
        {
            this.timeOut = timeOut;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var objWebRequest = base.GetWebRequest(address);
            objWebRequest.Timeout = this.timeOut;
            return objWebRequest;
        }
    }
}