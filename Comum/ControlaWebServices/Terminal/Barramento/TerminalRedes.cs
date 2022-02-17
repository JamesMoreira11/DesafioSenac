using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.svcOsbConsultaTerminal;
using Senac.Fecomercio.Entities.Domain.Terminal.Barramento;
using System;

namespace Senac.Fecomercio.ControlaWebServices.Terminal.Barramento
{
    public class TerminalRedes
    {
        public TerminalDetalhe ConsultarDetalheTerminal(string numeroLogico, string tipoTecnologia = null, string sistemaSolicitante = null)
        {
            TerminalDetalhe ret = null;
            Equipamento_consultarTerminalDetalhadoServicePortTypeClient oClient = null;
            try
            {
                Logger.LogInfo("TerminalRedes > Iniciando a consulta do lógico: '{0}'".ToFormat(numeroLogico));

                oClient = new Equipamento_consultarTerminalDetalhadoServicePortTypeClient();

                consultarTerminalDetalhadoRequest oRequest = new consultarTerminalDetalhadoRequest();
                oRequest.cieloSoapHeader = new CieloSoapHeaderType();

                UsuarioType usuarioEnvio = new UsuarioType();
                var cripto = new Crypt();
				usuarioEnvio.id = cripto.Decrypt(Extension.GetValueConfig("svcOsbConsultaTerminal.usuario", true));
                usuarioEnvio.senha = cripto.Decrypt(Extension.GetValueConfig("svcOsbConsultaTerminal.senha", true));

                oRequest.cieloSoapHeader.identificacaoRequest = new IdentificacaoRequestType
                {
                    nomeSistema = "GTC"
                };

                //oRequest.cieloSoapHeader.Item
                oRequest.cieloSoapHeader.Item = usuarioEnvio;
                oRequest.consultarTerminalDetalhadoRequest1 = new ConsultarTerminalDetalhadoRequestType();

                oRequest.consultarTerminalDetalhadoRequest1.numeroLogico = numeroLogico;

                if (tipoTecnologia.IsNotNull())
                {
                    oRequest.consultarTerminalDetalhadoRequest1.descricaoTipoTecnologia = tipoTecnologia;
                }

                if (sistemaSolicitante.IsNotNull())
                {
                    oRequest.consultarTerminalDetalhadoRequest1.nomeSistemaSolicitante = sistemaSolicitante;
                }

                DadosBaixaTecnicaType dadosBaixaTecnica = null;
                DadosClienteType dadosCliente = null;
                DadosEquipamentoType dadosEquipamento = null;
                DadosGrupoSolucaoType dadosGrupoSolucao = null;
                DadosRetornoType dadosRetorno = null;
                DadosUltimaInicializacaoType dadosUltimaInicializacao = null;
                DadosProcessamentoType[] listaDadosProcessamento = null;

                DadosAtualizacaoPaginaType retServ = oClient.consultarTerminalDetalhado(oRequest.cieloSoapHeader, oRequest.consultarTerminalDetalhadoRequest1, out dadosBaixaTecnica, out dadosCliente, out dadosEquipamento, out dadosGrupoSolucao, out dadosRetorno, out dadosUltimaInicializacao, out listaDadosProcessamento);

                if (dadosRetorno.IsNotNull())
                {
                    ret = new TerminalDetalhe();

                    if (dadosRetorno.codigoRetornoMensagem == "0")
                    {
                        ret.CodigoRetorno = 0;
                        ret.MensagemRetorno = dadosRetorno.descricaoRetornoMensagem;

                        if (dadosBaixaTecnica.IsNotNull())
                        {
                            ret.BaixaTecnica = new TerminalBaixaTecnica();
                            ret.BaixaTecnica.CodigoRetornoUltimaInicializacao = dadosBaixaTecnica.codigoRetornoUltimaInicializacaoBaixaTecnica;
                            ret.BaixaTecnica.CodigoTipoBaixaTecnica = dadosBaixaTecnica.codigoTipoBaixaTecnica;
                        }

                        if (dadosCliente.IsNotNull())
                        {
                            ret.DadosCliente = new TerminalDadosCliente();
                            ret.DadosCliente.IndicadorComercioEletronico = dadosCliente.indicadorComercioEletronico;
                            ret.DadosCliente.NomeFantasia = dadosCliente.nomeFantasia;
                            ret.DadosCliente.NumeroEstabelecimentoComercial = dadosCliente.codigoCliente.ToString().PadLeft(10, '0');
                            ret.DadosCliente.NumeroLoja = dadosCliente.codigoLojaEstabelecimento.ToInt();
                            ret.DadosCliente.RazaoSocial = dadosCliente.nomeRazaoSocial;
                            ret.DadosCliente.TipoPessoa = dadosCliente.codigoTipoPessoa;
                        }

                        if (dadosEquipamento.IsNotNull())
                        {
                            ret.DadosTerminal = new TerminalDadosTerminal();
                            ret.DadosTerminal.CodigoCategoria = dadosEquipamento.codigoCategoria;
                            ret.DadosTerminal.CodigoModSol = dadosEquipamento.siglaTipoTerminal;
                            ret.DadosTerminal.CodigoNoMobile = dadosEquipamento.codigoNoMobile;
                            ret.DadosTerminal.CodigoNoPrimario = dadosEquipamento.codigoNoPrimario;
                            ret.DadosTerminal.CodigoNoSecundario = dadosEquipamento.codigoNoSecundario;
                            ret.DadosTerminal.CodigoNoServico = dadosEquipamento.codigoNoServico;
                            ret.DadosTerminal.CodigoNoTelecarga = dadosEquipamento.codigoNoTelecarga;
                            ret.DadosTerminal.CodigoOperadoraSIMCard = dadosEquipamento.codigoOperadoraSIMCard;
                            ret.DadosTerminal.CodigoOperadorLogistico = dadosEquipamento.codigoOperadorLogistico;
                            ret.DadosTerminal.CodigoUsoTerminal = dadosEquipamento.codigoUsoTerminal;
                            ret.DadosTerminal.CodigoVersaoAplicativo = dadosEquipamento.codigoVersaoAplicativo;
                            ret.DadosTerminal.DescricaoCompleta = dadosEquipamento.descricaoCompleta;
                            ret.DadosTerminal.DescricaoModelo = dadosEquipamento.descricaoModelo;
                            ret.DadosTerminal.DescricaoResumida = dadosEquipamento.descricaoResumida;
                            ret.DadosTerminal.IndicadorBloqueio = dadosEquipamento.indicadorBloqueio;
                            ret.DadosTerminal.IndicadorCanalOrigem = dadosEquipamento.indicadorCanalOrigem;
                            ret.DadosTerminal.IndicadorDescontinuado = dadosEquipamento.indicadorDescontinuado;
                            ret.DadosTerminal.NomeGrupoSolucaoCaptura = dadosEquipamento.nomeGrupoSolucaoCaptura;
                            ret.DadosTerminal.NomeTecnologia = dadosEquipamento.nomeTecnologia;
                            ret.DadosTerminal.NumeroAcesso = dadosEquipamento.numeroAcesso;
                            ret.DadosTerminal.NumeroLogico = dadosEquipamento.numeroLogico;
                            ret.DadosTerminal.NumeroSerie = dadosEquipamento.numeroSerie;
                        }
                    }
                    else
                    {
                        Logger.LogInfo("TerminalRedes > ConsultarDetalheTerminal > Ocorreu erro ao consultar o terminal. Código retorno: '{0}' - Mensagem: '{1}'".ToFormat(dadosRetorno.codigoRetornoMensagem, dadosRetorno.descricaoRetornoMensagem));

                        if (dadosRetorno.codigoRetornoMensagem.IsInt())
                        {
                            ret.CodigoRetorno = dadosRetorno.codigoRetornoMensagem.ToInt();
                        }
                        else
                        {
                            ret.CodigoRetorno = 100;
                        }
                        ret.MensagemRetorno = dadosRetorno.descricaoRetornoMensagem;
                    }
                }
                else
                {
                    Logger.LogInfo("TerminalRedes > ConsultarDetalheTerminal > Ocorreu erro ao consultar o terminal. Sem retorno do serviço");
                }

                return ret;
            }
            catch (Exception ex)
            {
                Logger.LogError("TerminalRedes > ConsultarDetalheTerminal - Ocorreu erro: '{0}'".ToFormat(ex.GetAllErrorDetail()), ex);
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
        }
    }
}