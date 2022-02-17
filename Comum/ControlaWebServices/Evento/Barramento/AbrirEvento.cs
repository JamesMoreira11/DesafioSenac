using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senac.Fecomercio.Entities.Domain.AbreChamados;
using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.svcOsbAbrirEvento;
using System.ServiceModel;

namespace Senac.Fecomercio.ControlaWebServices.Evento.Barramento
{
    public class AbrirEvento
    {
        public long? AbrirEventoSEC(eventoSEC oEventoSEC)
        {
            long numeroRegistroEvento = 0;

            Evento_abrirEventoServicePortTypeClient oClient = null;

            try
            {
                Crypt cripto = new Crypt();

                oClient = new Evento_abrirEventoServicePortTypeClient();
                abrirEventoRequest oRequest = new abrirEventoRequest();
                oRequest.cieloSoapHeader = new CieloSoapHeaderType();

                UsuarioType usrEnvio = new UsuarioType();
                usrEnvio.id = cripto.Decrypt(Extension.GetValueConfig("svcOsbConsultaDadosCadastraisCliente.usuario", true));
                usrEnvio.senha = cripto.Decrypt(Extension.GetValueConfig("svcOsbConsultaDadosCadastraisCliente.senha", true));

                oRequest.cieloSoapHeader.identificacaoRequest = new IdentificacaoRequestType { nomeSistema = "GTC" };

                oRequest.cieloSoapHeader.Item = usrEnvio;

                oRequest.abrirEventoRequest1 = new AbrirEventoRequestType();

                oRequest.abrirEventoRequest1.codigoEvento = oEventoSEC.codigoEvento;
                oRequest.abrirEventoRequest1.codigoDependencia = oEventoSEC.codigoDependencia;
                oRequest.abrirEventoRequest1.codigoCliente = oEventoSEC.codigoCliente;
                oRequest.abrirEventoRequest1.descricaoLinha1 = oEventoSEC.descricaoLinha1;
                oRequest.abrirEventoRequest1.descricaoLinha2 = oEventoSEC.descricaoLinha2;
                oRequest.abrirEventoRequest1.descricaoLinha3 = oEventoSEC.descricaoLinha3;
                oRequest.abrirEventoRequest1.descricaoLinha4 = oEventoSEC.descricaoLinha4;
                oRequest.abrirEventoRequest1.descricaoLinha5 = oEventoSEC.descricaoLinha5;
                oRequest.abrirEventoRequest1.descricaoLinha6 = oEventoSEC.descricaoLinha6;

                oClient.abrirEvento(oRequest.cieloSoapHeader, oRequest.abrirEventoRequest1, out numeroRegistroEvento);
            }
            catch (FaultException<Fault> exWdsl)
            {
                throw exWdsl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oClient.IsNotNull())
                {
                    oClient.Close();
                    oClient = null;
                }
            }

            return (numeroRegistroEvento.Equals(0)) ? (long?)null : numeroRegistroEvento;
        }
    }
}
