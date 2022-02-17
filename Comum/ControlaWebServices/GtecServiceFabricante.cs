using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.Base;
using System;
using System.Configuration;

namespace Senac.Fecomercio.ControlaWebServices
{
    public abstract class GtecServiceFabricante : ServicoBase
    {
        #region Construtor
        public GtecServiceFabricante()
            : base()
        { }

        public GtecServiceFabricante(bool criaInstancia)
            : base(criaInstancia)
        { }
        #endregion

        #region MetodosAbstratos
        protected abstract string GetInterfaceService();
        #endregion

        #region MetodosOverride
        protected override void ConfiguraServico()
        {
            try
            {
                if (ServicoCall.IsNotNull())
                {
                    string sProtocolo = Extension.GetValueConfig("pax_xi_protocolo", true);
                    string sServidor = Extension.GetValueConfig("pax_xi_servidor", true);
                    string sPorta = Extension.GetValueConfig("pax_xi_porta", true);
                    string sBusSystem = Extension.GetValueConfig("pax_xi_businesssystem", true);

                    string urlServico = "{0}://{1}:{2}/XISOAPAdapter/MessageServlet?senderParty=&senderService={3}&receiverParty=&receiverService=&interface={4}&interfaceNamespace=urn:cielo:gtec:pax:VendaTerminal".ToFormat(sProtocolo, sServidor, sPorta, sBusSystem, GetInterfaceService());

                    ServicoCall.Url = urlServico;
                    var cripto = new Crypt();
                    string passCredential = cripto.Decrypt(ConfigurationManager.AppSettings["pax_xi_passcredential"]);

                    ServicoCall.Credentials = new System.Net.NetworkCredential(cripto.Decrypt(ConfigurationManager.AppSettings["pax_xi_usercredential"]), passCredential);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("GtecServiceFabricante - ConfiguraServico > Error > Erro: '{0}'".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
        }
        #endregion
    }
}