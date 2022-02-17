using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.Base;
using System;
using System.Configuration;

namespace Senac.Fecomercio.ControlaWebServices
{
    public class GtecServiceMI_SAP_PO : ServicoBase
    {
        #region Construtor
        public GtecServiceMI_SAP_PO()
            : base()
        { }
        #endregion

        #region MetodoOverride
        protected override void CriarInstancia()
        {
            try
            {
                if (ServicoCall.IsNull())
                {
                    ServicoCall = new svcSAP_PO.GtecService_MIService();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("GtecServiceMI SAP PO > Construtor Error  > {0}".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
        }

        protected override void ConfiguraServico()
        {
            try
            {
                if (ServicoCall.IsNotNull())
                {
                    string sProtocolo = Extension.GetValueConfig("xi_protocolo_sap_po", true);
                    string sServidor = Extension.GetValueConfig("xi_servidor_sap_po", true);
                    string sPorta = Extension.GetValueConfig("xi_porta_sap_po", true);

                    string urlServico = "{0}://{1}:{2}/XISOAPAdapter/MessageServlet?senderParty=&senderService=Gtec&receiverParty=&receiverService=&interface=GtecService_MI&interfaceNamespace=urn:visanet:legancy:gtec:opl".ToFormat(sProtocolo, sServidor, sPorta);

                    ServicoCall.Url = urlServico;
                    var cripto = new Crypt();
                    string passCredential = cripto.Decrypt(ConfigurationManager.AppSettings["xi_passcredential_sap_po"]);

                    ServicoCall.Credentials = new System.Net.NetworkCredential(cripto.Decrypt(ConfigurationManager.AppSettings["xi_usercredential_sap_po"]), passCredential);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("ConfiguraServico > Error > Erro: '{0}'".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
        }

        protected override ISaidaServico CallMetodoServico(IEntradaServico entrada)
        {
            svcSAP_PO.Return_DT retorno = null;
            try
            {
                if (ServicoCall.IsNotNull())
                {
                    retorno = ((svcSAP_PO.GtecService_MIService)ServicoCall).GtecService_MI((svcSAP_PO.msgGtec_DT)entrada);
                }
                else
                {
                    throw new FormatException("Não foi iniciado o serviço para envio para operador externo");
                }

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