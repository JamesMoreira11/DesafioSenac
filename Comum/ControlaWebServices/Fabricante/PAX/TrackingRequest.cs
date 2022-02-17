using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.Base;
using System;
using System.Xml;

namespace Senac.Fecomercio.ControlaWebServices.Fabricante.PAX
{
    public class TrackingRequest : ServicoBaseRest
    {
        #region MetodosOverrides
        protected override string GetUrl()
        {
            return Extension.GetValueConfig("pax_tracking_request_url", true);
        }

        protected override string GetUserAgent()
        {
            return Extension.GetValueConfig("pax_tracking_request_user_agent", true);
        }

        protected override string GetContentType()
        {
            return Extension.GetValueConfig("pax_tracking_request_content_type", true);
        }

        protected override string GetMethod()
        {
            return "POST";
        }
        #endregion

        #region Metodos
        public TrackingRequestResponse ObterTrackingPedido(string numeroPedido)
        {
            TrackingRequestResponse ret = null;
            XmlDocument xDocResponse = null;

            try
            {
                string xmlSend = System.IO.File.ReadAllText(@"{0}\Fabricante\PAX\PAX_Request_Tracking.xml".ToFormat(typeof(TrackingRequest).Assembly.GetDirectoryPath()));

                string urlServicePax = Extension.GetValueConfig("pax_tracking_request_url_pax", true);
                string paxIdentificacao = Extension.GetValueConfig("pax_tag_identificacao", true);
                string paxCredencial = Extension.GetValueConfig("pax_tag_credencial", true);
                string paxSenha = Extension.GetValueConfig("pax_tag_senha", true);

                xmlSend = xmlSend.ToFormat(urlServicePax, numeroPedido, paxIdentificacao, paxCredencial, paxSenha);

                string xmlRetorno = CallService(xmlSend);

                if (xmlRetorno.IsNull() || (xmlRetorno.IsNotNull() && !xmlRetorno.IsXML()))
                {
                    throw new XmlException("O XML de retorno não é válido. '{0}'".ToFormat(xmlRetorno));
                }

                xmlRetorno = xmlRetorno.RemoveAllNamespaces();

                xDocResponse = new XmlDocument();
                xDocResponse.LoadXml(xmlRetorno);

                ret = new TrackingRequestResponse();

                XmlNode root = xDocResponse.DocumentElement;

                XmlNode nodeCodigoRetorno = root.SelectSingleNode("//Envelope/Body/TrackingRequestResponse/TrackingRequestResult/Retorno/CodigoRetorno");

                if (nodeCodigoRetorno.IsNull())
                {
                    throw new ArgumentNullException("Não foi possível encontrar o Node 'CodigoRetorno' no retorno do tracking request PAX");
                }

                ret.CodigoRetorno = nodeCodigoRetorno.InnerText;

                XmlNode nodeDescricaoRetorno = xDocResponse.SelectSingleNode("//Envelope/Body/TrackingRequestResponse/TrackingRequestResult/Retorno/Descricao");

                if (nodeDescricaoRetorno.IsNull())
                {
                    throw new ArgumentNullException("Não foi possível encontrar o Node 'Descricao' no retorno do tracking request PAX");
                }

                ret.DescricaoRetorno = nodeDescricaoRetorno.InnerText;

                XmlNode nodeTracking = xDocResponse.SelectSingleNode("//Envelope/Body/TrackingRequestResponse/TrackingRequestResult/Tracking");

                if (nodeTracking.IsNull())
                {
                    throw new ArgumentNullException("Não foi possível encontrar o Node 'a:Tracking' no retorno do tracking request PAX");
                }

                ret.XmlTracking = nodeTracking;

                return ret;
            }
            catch (Exception ex)
            {
                Logger.LogError("TrackingRequest() > Error > '{0}'".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
            finally
            {
                if (xDocResponse.IsNotNull())
                {
                    xDocResponse = null;
                }
            }
        }
        #endregion
    }
}