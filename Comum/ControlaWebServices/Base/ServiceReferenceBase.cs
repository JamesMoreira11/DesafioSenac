using System.Web.Services;

namespace Senac.Fecomercio.ControlaWebServices.Base
{
    [WebServiceBindingAttribute(Name = "Cielo.Services.Base", Namespace = "http://tempuri.org/")]
    public abstract class ServiceReferenceBase : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

    }
}