using System;
using System.Xml;

namespace Senac.Fecomercio.ControlaWebServices.Fabricante.PAX
{
    [Serializable]
    public class TrackingRequestResponse
    {
        public string CodigoRetorno { get; set; }
        public string DescricaoRetorno { get; set; }
        public XmlNode XmlTracking { get; set; }
    }
}