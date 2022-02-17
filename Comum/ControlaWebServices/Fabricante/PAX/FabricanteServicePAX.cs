using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.Base;
using Senac.Fecomercio.Entities.Enums;
using System;

namespace Senac.Fecomercio.ControlaWebServices.Fabricante.PAX
{
    public class FabricanteServicePAX : GtecServiceFabricante
    {
        #region Constructor
        public FabricanteServicePAX(EFabricantePAXOperacao operacaoPAX)
            : base(false)
        {
            OperacaoPAX = operacaoPAX;
            this.CriarInstancia();
            this.ConfiguraServico();
        }
        #endregion

        #region Propriedades
        private EFabricantePAXOperacao OperacaoPAX;
        #endregion

        #region MetodosOverride
        protected override void CriarInstancia()
        {
            try
            {
                if (ServicoCall.IsNull())
                {
                    if (OperacaoPAX == EFabricantePAXOperacao.MakeReverseLogistica)
                    {
                        ServicoCall = new svcPAXVendaTerminal.Gtec_HBS_VendaTerminal_out_SI();
                    }
                    else
                    {
                        throw new InvalidOperationException("FabricanteServicePAX - Não foi possível recuperar a operação a ser chamado");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("GtecServiceFabricante > Construtor Error  > {0}".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
        }

        protected override string GetInterfaceService()
        {
            string retorno = "VendaTerminal_out_SI";

            if (OperacaoPAX == EFabricantePAXOperacao.MakeReverseLogistica)
            {
                retorno = "VendaTerminal_out_SI";
            }

            return retorno;
        }

        protected override ISaidaServico CallMetodoServico(IEntradaServico entrada)
        {
            ISaidaServico retorno = null;
            try
            {
                if (ServicoCall.IsNotNull())
                {
                    string tagCredencial = Extension.GetValueConfig("pax_tag_credencial", true);
                    string tagIdentificacao = Extension.GetValueConfig("pax_tag_identificacao", true);
                    string tagSenha = Extension.GetValueConfig("pax_tag_senha", true);

                    if (OperacaoPAX == EFabricantePAXOperacao.MakeReverseLogistica)
                    {
                        svcPAXVendaTerminal.Header_VendaTerminal_DT entradaWithCredential = (svcPAXVendaTerminal.Header_VendaTerminal_DT)entrada;

                        entradaWithCredential.Credencial = tagCredencial;
                        entradaWithCredential.Identificacao = tagIdentificacao;
                        entradaWithCredential.senha = tagSenha;

                        retorno = ((svcPAXVendaTerminal.Gtec_HBS_VendaTerminal_out_SI)ServicoCall).VendaTerminal_out_SI(entradaWithCredential);
                    }
                }
                else
                {
                    throw new FormatException("Não foi iniciado o serviço para envio para operador PAX");
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