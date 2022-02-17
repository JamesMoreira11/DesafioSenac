using Senac.Fecomercio.Entities.Domain.Cliente.Barramento;
using System;
using System.Collections.Generic;
using Senac.Fecomercio.ControlaWebServices.svcOsbConsultarDadosCadastraisCliente;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senac.Fecomercio.Common;
using System.ServiceModel;

namespace Senac.Fecomercio.ControlaWebServices.Cliente.Barramento
{
    public class ClienteDadosCadastrais
    {
        public ClienteDetalhe ConsultaDadosCadastrais(long codigoCliente)
        {
            ClienteDetalhe retorno = null;

            Cliente_consultarDadosCadastraisClienteServicePortTypeClient oClient = null;

            try
            {
                retorno = new ClienteDetalhe();

                Crypt cripto = new Crypt();

                oClient = new Cliente_consultarDadosCadastraisClienteServicePortTypeClient();

                consultarDadosCadastraisClienteRequest oRequest = new consultarDadosCadastraisClienteRequest();
                oRequest.cieloSoapHeader = new CieloSoapHeaderType();

                UsuarioType usrEnvio = new UsuarioType();
                usrEnvio.id = cripto.Decrypt(Extension.GetValueConfig("svcOsbConsultaDadosCadastraisCliente.usuario", true));
                usrEnvio.senha = cripto.Decrypt(Extension.GetValueConfig("svcOsbConsultaDadosCadastraisCliente.senha", true));

                oRequest.cieloSoapHeader.identificacaoRequest = new IdentificacaoRequestType { nomeSistema = "GTC" };

                oRequest.cieloSoapHeader.Item = usrEnvio;

                oRequest.consultarDadosCadastraisClienteRequest1 = new ConsultarDadosCadastraisClienteRequestType();

                oRequest.consultarDadosCadastraisClienteRequest1.Item = codigoCliente;

                ConsultarDadosCadastraisClienteResponseDadosCredenciamentoType dadosAfiliacao;

                System.Nullable<long> numeroMatriz;
                string indicadorMultivan;
                bool indicadorAlteracaoDiario;
                bool indicadorAssinaturaArquivo;
                string nomeEmailContatoAdicional;
                System.Nullable<bool> indicadorBloqueioSuprimentos;
                string codigoGerenteVirtual;
                string nomeGerenteVirtual;
                string indicadorClientePrepago;
                string indicadorClienteVendas;
                bool indicadorFranquia;

                ConsultarDadosCadastraisClienteResponseDadosClienteType oResponse = oClient.consultarDadosCadastraisCliente(oRequest.cieloSoapHeader, oRequest.consultarDadosCadastraisClienteRequest1, out dadosAfiliacao, out numeroMatriz, out indicadorMultivan, out indicadorAlteracaoDiario, out indicadorAssinaturaArquivo, out nomeEmailContatoAdicional, out indicadorBloqueioSuprimentos, out codigoGerenteVirtual, out nomeGerenteVirtual, out indicadorClientePrepago, out indicadorClienteVendas, out indicadorFranquia);

                retorno = ConverteDados(oResponse);
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

            return retorno;
        }

        private ClienteDetalhe ConverteDados(ConsultarDadosCadastraisClienteResponseDadosClienteType oDados)
        {
            ClienteDetalhe oCliente = new ClienteDetalhe();

            oCliente.dadosCliente = new DadosCliente();

            oCliente.dadosCliente.codigoCliente = oDados.codigoCliente;
            oCliente.dadosCliente.dataAberturaCliente = oDados.dataAberturaCliente;
            oCliente.dadosCliente.codigoCadeia = oDados.codigoCadeia;
            oCliente.dadosCliente.descricaoPOS = oDados.descricaoPOS;
            oCliente.dadosCliente.indicadorCessao = oDados.indicadorCessao;
            oCliente.dadosCliente.indicadorCartaCircularizacao = oDados.indicadorCartaCircularizacao;
            oCliente.dadosCliente.indicadorAntecipacaoAutomatica = oDados.indicadorAntecipacaoAutomatica;
            oCliente.dadosCliente.indicadorAntecipacaoSecuritizacao = oDados.indicadorAntecipacaoSecuritizacao;

            //Lista Dados Produto Cliente
            if (oDados.listaDadosProdutoCliente.Count() > 0)
            {
                oCliente.dadosCliente.listaDadosProdutoCliente = new List<DadosProdutoCliente>();

                DadosProdutoCliente oProdutoCliente;

                for (int x = 0; x < oDados.listaDadosProdutoCliente.Count(); x++)
                {
                    oProdutoCliente = new DadosProdutoCliente();

                    oProdutoCliente.dadosBancarios = new DadosBancarios();

                    oProdutoCliente.dadosBancarios.codigoBanco = oDados.listaDadosProdutoCliente[x].dadosProdutoCliente[0].dadosBancarios.codigoBanco;
                    oProdutoCliente.dadosBancarios.numeroAgencia = oDados.listaDadosProdutoCliente[x].dadosProdutoCliente[0].dadosBancarios.numeroAgencia;
                    oProdutoCliente.dadosBancarios.numeroContaCorrente = oDados.listaDadosProdutoCliente[x].dadosProdutoCliente[0].dadosBancarios.numeroContaCorrente;

                    oProdutoCliente.codigoProduto = oDados.listaDadosProdutoCliente[x].dadosProdutoCliente[0].codigoProduto;
                    oProdutoCliente.percentualTaxa = oDados.listaDadosProdutoCliente[x].dadosProdutoCliente[0].percentualTaxa;
                    oProdutoCliente.descricaoProduto = oDados.listaDadosProdutoCliente[x].dadosProdutoCliente[0].descricaoProduto;

                    oProdutoCliente.dadosTarifa = new DadosTarifa();

                    oProdutoCliente.dadosTarifa.nomeTipoTarifa = oDados.listaDadosProdutoCliente[x].dadosProdutoCliente[0].dadosTarifa.nomeTipoTarifa;
                    oProdutoCliente.dadosTarifa.valorTarifa = oDados.listaDadosProdutoCliente[x].dadosProdutoCliente[0].dadosTarifa.valorTarifa;

                    oCliente.dadosCliente.listaDadosProdutoCliente.Add(oProdutoCliente);
                }
            }

            oCliente.dadosCliente.codigoTipoGarantia = oDados.codigoTipoGarantia;
            oCliente.dadosCliente.nomeFantasia = oDados.nomeFantasia;

            oCliente.dadosCliente.dadosContatoCliente = new DadosContatoCliente();

            oCliente.dadosCliente.dadosContatoCliente.nomeContato = oDados.dadosContatoCliente.nomeContato;
            oCliente.dadosCliente.dadosContatoCliente.numeroTelefoneContato = oDados.dadosContatoCliente.numeroTelefoneContato;
            oCliente.dadosCliente.dadosContatoCliente.numeroFax = oDados.dadosContatoCliente.numeroFax;
            oCliente.dadosCliente.dadosContatoCliente.numeroDDDFax = oDados.dadosContatoCliente.numeroDDDFax;
            oCliente.dadosCliente.dadosContatoCliente.nomeEmailContato = oDados.dadosContatoCliente.nomeEmailContato;


            oCliente.dadosCliente.indicadorEcommerce = oDados.indicadorEcommerce;
            oCliente.dadosCliente.codigoMoeda = (oDados.codigoMoeda.Length > 0) ? oDados.codigoMoeda[0].ToString() : null;
            oCliente.dadosCliente.indicadorMultiBandeira = oDados.indicadorMultiBandeira;
            oCliente.dadosCliente.codigoPCT = oDados.codigoPCT;
            oCliente.dadosCliente.indicadorSecuratizacao = oDados.indicadorSecuratizacao;
            oCliente.dadosCliente.indicadorTrava = oDados.indicadorTrava;
            oCliente.dadosCliente.indicadorMoto = oDados.indicadorMoto;

            if (oDados.dadosBancarioCliente.Length > 0)
            {

                oCliente.dadosCliente.dadosBancariosCliente = new List<DadosBancariosCliente>();

                DadosBancariosCliente oDadosBancarios;

                for (int b = 0; b < oDados.dadosBancarioCliente.Length; b++)
                {
                    oDadosBancarios = new DadosBancariosCliente();
                    oDadosBancarios.codigoBanco = oDados.dadosBancarioCliente[b].codigoBanco;
                    oDadosBancarios.numeroAgencia = oDados.dadosBancarioCliente[b].numeroAgencia;
                    oDadosBancarios.numeroContaCorrente = oDados.dadosBancarioCliente[b].numeroContaCorrente;

                    oCliente.dadosCliente.dadosBancariosCliente.Add(oDadosBancarios);
                }
            }

            oCliente.dadosCliente.dadosProprietarioMobile = new DadosProprietarioMobile();

            oCliente.dadosCliente.dadosProprietarioMobile.nomeProprietario = oDados.dadosProprietarioMobile.nomeProprietario;
            oCliente.dadosCliente.dadosProprietarioMobile.numeroCPFProprietario = oDados.dadosProprietarioMobile.numeroCPFProprietario;
            oCliente.dadosCliente.dadosProprietarioMobile.dataNascimentoProprietario = oDados.dadosProprietarioMobile.dataNascimentoProprietario;

            oCliente.dadosCliente.percentualAntecipacao = oDados.percentualAntecipacao;
            oCliente.dadosCliente.descricaoLimite = oDados.descricaoLimite;
            oCliente.dadosCliente.indicadorVisaVale = oDados.indicadorVisaVale;
            oCliente.dadosCliente.indicadorJuridico = oDados.indicadorJuridico;
            oCliente.dadosCliente.valorTarifaAgendamento = oDados.valorTarifaAgendamento;
            oCliente.dadosCliente.quantidadePOS = oDados.quantidadePOS;
            oCliente.dadosCliente.indicadorSaldoConsolidado = oDados.indicadorSaldoConsolidado;
            oCliente.dadosCliente.codigoRamoAtividade = oDados.codigoRamoAtividade;
            oCliente.dadosCliente.codigoBandeira = oDados.codigoBandeira;
            oCliente.dadosCliente.codigoSegmento = oDados.codigoSegmento;
            oCliente.dadosCliente.codigoECAssociada = oDados.codigoECAssociada;
            oCliente.dadosCliente.nomeRazaoSocial = oDados.nomeRazaoSocial;
            oCliente.dadosCliente.numeroCNPJ = oDados.numeroCNPJ;
            oCliente.dadosCliente.indicadorCadeia = oDados.indicadorCadeia;
            oCliente.dadosCliente.indicadorTransmissao = oDados.indicadorTransmissao;
            oCliente.dadosCliente.codigoTipoPagamento = oDados.codigoTipoPagamento;

            oCliente.dadosCliente.dadosEnderecoFisicoCliente = new DadosEnderecoFisicoCliente();

            oCliente.dadosCliente.dadosEnderecoFisicoCliente.nomeLogradouro = oDados.dadosEnderecoFisicoCliente.nomeLogradouro;
            oCliente.dadosCliente.dadosEnderecoFisicoCliente.descricaoComplementoEndereco = oDados.dadosEnderecoFisicoCliente.descricaoComplementoEndereco;
            oCliente.dadosCliente.dadosEnderecoFisicoCliente.nomeCidade = oDados.dadosEnderecoFisicoCliente.nomeCidade;
            oCliente.dadosCliente.dadosEnderecoFisicoCliente.siglaEstado = oDados.dadosEnderecoFisicoCliente.siglaEstado;
            oCliente.dadosCliente.dadosEnderecoFisicoCliente.numeroCEP = oDados.dadosEnderecoFisicoCliente.numeroCEP;

            oCliente.dadosCliente.descricaoRamoAtividade = oDados.descricaoRamoAtividade;
            oCliente.dadosCliente.indicadorRecebimentoSMS = oDados.indicadorRecebimentoSMS;

            if (oDados.dadosEnderecoCorrespondenciaCliente.Length > 0)
            {
                oCliente.dadosCliente.dadosEnderecoCorrespondenciaCliente = new List<DadosEnderecoCorrespondenciaCliente>();

                DadosEnderecoCorrespondenciaCliente oEndereco;

                for (int c = 0; c < oDados.dadosEnderecoCorrespondenciaCliente.Length; c++)
                {
                    oEndereco = new DadosEnderecoCorrespondenciaCliente();

                    oEndereco.nomeLogradouro = oDados.dadosEnderecoCorrespondenciaCliente[c].nomeLogradouro;
                    oEndereco.descricaoComplementoEndereco = oDados.dadosEnderecoCorrespondenciaCliente[c].descricaoComplementoEndereco;
                    oEndereco.nomeCidade = oDados.dadosEnderecoCorrespondenciaCliente[c].nomeCidade;
                    oEndereco.siglaEstado = oDados.dadosEnderecoCorrespondenciaCliente[c].siglaEstado;
                    oEndereco.numeroCEP = oDados.dadosEnderecoCorrespondenciaCliente[c].numeroCEP;

                    oCliente.dadosCliente.dadosEnderecoCorrespondenciaCliente.Add(oEndereco);
                }
            }

            oCliente.dadosCliente.codigoCategoriaAntecipacao = oDados.codigoCategoriaAntecipacao;
            oCliente.dadosCliente.codigoPeriodicidadeAntecipacaoAutomatica = oDados.codigoPeriodicidadeAntecipacaoAutomatica;
            oCliente.dadosCliente.codigoECPrincipal = oDados.codigoECPrincipal;
            oCliente.dadosCliente.indicadorParcelado = oDados.indicadorParcelado;
            oCliente.dadosCliente.indicadorMobile = oDados.indicadorMobile;
            oCliente.dadosCliente.codigoClasseFaturamento = oDados.codigoClasseFaturamento;

            if (oDados.dadosSituacaoFuncionamentoCliente.Length > 0)
            {
                oCliente.dadosCliente.dadosSituacaoFuncionamentoCliente = new List<DadosSituacaoFuncionamentoCliente>();

                DadosSituacaoFuncionamentoCliente oSituacao;

                for (int s = 0; s < oDados.dadosSituacaoFuncionamentoCliente.Length; s++)
                {
                    oSituacao = new DadosSituacaoFuncionamentoCliente();

                    oSituacao.codigoSituacaoCliente = oDados.dadosSituacaoFuncionamentoCliente[s].codigoSituacaoCliente;
                    oSituacao.codigoMotivoFechamento = oDados.dadosSituacaoFuncionamentoCliente[s].codigoMotivoFechamento;

                    oCliente.dadosCliente.dadosSituacaoFuncionamentoCliente.Add(oSituacao);
                }
            }

            oCliente.dadosCliente.dataUltimaAlteracao = oDados.dataUltimaAlteracao;
            oCliente.dadosCliente.indicadorPiloto = oDados.indicadorPiloto;

            oCliente.dadosCliente.dadosContatoAdicional = new DadosContatoAdicional();

            oCliente.dadosCliente.dadosContatoAdicional.numeroDDDTelefoneContato = oDados.dadosContatoAdicional.numeroDDDTelefoneContato;
            oCliente.dadosCliente.dadosContatoAdicional.numeroTelefoneContato = oDados.dadosContatoAdicional.numeroTelefoneContato;

            oCliente.dadosCliente.nomeSituacaoAtividadeCliente = oDados.nomeSituacaoAtividadeCliente;

            oCliente.dadosCliente.dadosEnderecoSuprimento = new DadosEnderecoSuprimento();

            oCliente.dadosCliente.dadosEnderecoSuprimento.nomeLogradouro = oDados.dadosEnderecoSuprimento.nomeLogradouro;
            oCliente.dadosCliente.dadosEnderecoSuprimento.descricaoComplementoEndereco = oDados.dadosEnderecoSuprimento.descricaoComplementoEndereco;
            oCliente.dadosCliente.dadosEnderecoSuprimento.nomeCidade = oDados.dadosEnderecoSuprimento.nomeCidade;
            oCliente.dadosCliente.dadosEnderecoSuprimento.siglaEstado = oDados.dadosEnderecoSuprimento.siglaEstado;
            oCliente.dadosCliente.dadosEnderecoSuprimento.numeroCEP = oDados.dadosEnderecoSuprimento.numeroCEP;

            oCliente.dadosCliente.nomeSituacaoAtivacaoCliente = oDados.nomeSituacaoAtivacaoCliente;

            return oCliente;
        }
    }
}
