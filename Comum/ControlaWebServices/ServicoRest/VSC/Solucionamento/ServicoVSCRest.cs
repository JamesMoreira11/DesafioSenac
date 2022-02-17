using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.Base;

namespace Senac.Fecomercio.ControlaWebServices.ServicoRest.VSC.Solucionamento
{
    public class ServicoVSCRest : ServicoBaseRest
    {
        protected override string GetContentType()
        {
            return "application/json";
        }

        protected override string GetUrl()
        {
            return Extension.GetValueConfig("VSC_SERVICO_SOLUCIONAMENTO_URL", true);
        }

        protected override string GetUserAgent()
        {
            return "Apache-HttpClient/4.1.1 (java 1.5)";
        }

        protected override string GetMethod()
        {
            return Extension.GetValueConfig("VSC_SERVICO_SOLUCIONAMENTO_METHOD", true);
        }
    }
}