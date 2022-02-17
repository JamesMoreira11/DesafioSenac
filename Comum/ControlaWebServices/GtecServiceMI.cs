using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.Base;
using Senac.Fecomercio.ControlaWebServices.svcSAP;
using System;
using System.Configuration;

namespace Senac.Fecomercio.ControlaWebServices
{
    public class GtecServiceMI : ServicoBase
    {
        #region Construtor
        public GtecServiceMI()
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
                    ServicoCall = new svcSAP.Gtec_HBS_GtecService_MI();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("GtecServiceMI > Construtor Error  > {0}".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
        }

        protected override void ConfiguraServico()
        {
            try
            {
                if (ServicoCall.IsNotNull())
                {
                    string sProtocolo = Extension.GetValueConfig("xi_protocolo", true);
                    string sServidor = Extension.GetValueConfig("xi_servidor", true);
                    string sPorta = Extension.GetValueConfig("xi_porta", true);
                    string sBusinessSystem = Extension.GetValueConfig("xi_businesssystem", true);

                    string urlServico = sProtocolo.Trim() + "://" + sServidor.Trim() + ":" + sPorta.Trim() + "/XISOAPAdapter/MessageServlet?channel=:Gtec_" + sBusinessSystem + ":Gtec_Soap_" +
                        "OL_Sender&version=3.0&Sender.Service=Gtec_" + sBusinessSystem + "&Interface=urn%3Avisanet%3Alegancy%3" +
                        "Agtec%3Aopl%5EGtecService_MI";

                    ServicoCall.Url = urlServico;
                    var cripto = new Crypt();
                    string passCredential = cripto.Decrypt(ConfigurationManager.AppSettings["xi_passcredential"]);

                    ServicoCall.Credentials = new System.Net.NetworkCredential(cripto.Decrypt(ConfigurationManager.AppSettings["xi_usercredential"]), passCredential);
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
            svcSAP.Return_DT retorno = null;
            try
            {
                if (ServicoCall.IsNotNull())
                {
                    retorno = ((svcSAP.Gtec_HBS_GtecService_MI)ServicoCall).GtecService_MI((msgGtec_DT)entrada);
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