using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.Base;

namespace Senac.Fecomercio.ControlaWebServices.ServicoRest.VSC.ApiOrders.Resource
{
    public class ServicoApiOrdersRest : ServicoBaseRest
    {
        protected override string GetContentType()
        {
            return "application/json;charset=utf-8";
        }

        protected override string GetUrl()
        {
            return Extension.GetValueConfig("VSC_SERVICO_API_ORDERS_URL", true);
        }

        protected override string GetUserAgent()
        {
            return "Apache-HttpClient/4.1.1 (java 1.5)";
        }

        protected override string GetMethod()
        {
            return Extension.GetValueConfig("VSC_SERVICO_API_ORDERS_METHOD", true);
        }
    }
}
