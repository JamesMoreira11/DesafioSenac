/*using Senac.Fecomercio.Common;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Senac.Fecomercio.ControlaWebServices.FaturamentoSAP
{
    public class ServicoFaturamentoSAP : IDisposable
    {
        #region Construtor
        public ServicoFaturamentoSAP()
            : base()
        {
            this.CriarInstancia();
        }
        #endregion

        #region Propriedades
        private svcFaturamentoSAP.SI_FaturamentoLogistico_Async_OutbClient ServicoCallSAP = null;
        #endregion

        #region Metodos
        private void CriarInstancia()
        {
            try
            {
                if (ServicoCallSAP.IsNull())
                {
                    string protocolo = Extension.GetValueConfig("SAP_FATURAMENTO_XI_PROTOCOLO", true);
                    string servidor = Extension.GetValueConfig("SAP_FATURAMENTO_XI_SERVIDOR", true);
                    string porta = Extension.GetValueConfig("SAP_FATURAMENTO_XI_SERVIDOR_PORTA", true);
                    string senderService = Extension.GetValueConfig("SAP_FATURAMENTO_XI_SENDER_SERVICE", true);
                    string interfaceService = Extension.GetValueConfig("SAP_FATURAMENTO_XI_INTERFACE_SERVICE", true);
                    string usuario = Extension.GetValueConfig("SAP_FATURAMENTO_XI_CREDENTIAL_USER", true);
                    string senha = Extension.GetValueConfig("SAP_FATURAMENTO_XI_CREDENTIAL_PASS", true);

                    var crypt = new Crypt();
                    usuario = crypt.Decrypt(usuario);
                    senha = crypt.Decrypt(senha);

                    string urlServico = "{0}://{1}:{2}/XISOAPAdapter/MessageServlet?senderParty=&senderService={3}&receiverParty=&receiverService=&interface={4}&interfaceNamespace=urn:cielo:sap:gtec:faturamentoLogistico".ToFormat(protocolo, servidor, porta, senderService, interfaceService);

                    EndpointAddress address = new EndpointAddress(urlServico);
                    BasicHttpBinding binding = new BasicHttpBinding("SI_FaturamentoLogistico_Async_OutbBinding");

                    binding.AllowCookies = true;
                    binding.BypassProxyOnLocal = true;
                    binding.MessageEncoding = WSMessageEncoding.Text;
                    binding.TextEncoding = System.Text.Encoding.UTF8;
                    binding.UseDefaultWebProxy = true;
                    binding.TransferMode = TransferMode.Buffered;

                    ServicoCallSAP = new svcFaturamentoSAP.SI_FaturamentoLogistico_Async_OutbClient(binding, address);

                    ClientCredentials clientCredential = ServicoCallSAP.ClientCredentials;

                    clientCredential.UserName.UserName = usuario;
                    clientCredential.UserName.Password = senha;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("ServicoFaturamentoSAP > Construtor Error  > {0}".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
        }

        public void CallMetodoServico(List<Entities.FaturamentoSAP.FaturamentoSAP> entrada)
        {
            try
            {
                string formatDateTime = Extension.GetValueConfig("SAP_FATURAMENTO_DATE_TIME_FORMAT", true);

                if (ServicoCallSAP.IsNotNull())
                {
                    if (!entrada.IsEmpty<Entities.FaturamentoSAP.FaturamentoSAP>())
                    {
                        Logger.LogInfo("ServicoFaturamentoSAP > Enviando lote de [{0}] chamados".ToFormat(entrada.Count));

                        svcFaturamentoSAP.DT_FaturamentoLogisticoOutbDados[] arEnvio = new svcFaturamentoSAP.DT_FaturamentoLogisticoOutbDados[entrada.Count];

                        svcFaturamentoSAP.DT_FaturamentoLogisticoOutbDados item = null;
                        for (int i = 0; i < entrada.Count; i++)
                        {
                            item = new svcFaturamentoSAP.DT_FaturamentoLogisticoOutbDados();

                            item.tipoDeServicoCod = entrada[i].CodigoServicoTipo.ToString();
                            item.tipoDeServicoDesc = entrada[i].DescricaoServicoTipo;
                            item.evento = entrada[i].CodigoEvento.ToString();
                            item.assistenciaTecnica = entrada[i].CodigoOperador;
                            item.filial = entrada[i].CodigoFilial;
                            item.status = entrada[i].StatusChamado;
                            item.tipoDeOcorrenciaCod = entrada[i].CodigoOcorrenciaCategoria.ToString();
                            item.tipoDeOcorrenciaDesc = entrada[i].DescricaoOcorrenciaCategoria;
                            item.ocorrenciasCod = entrada[i].CodigoOcorrencia.ToString();
                            item.ocorrenciasDesc = entrada[i].DescricaoOcorrencia;
                            item.numDoChamadoGtec = "{0}{1}".ToFormat(entrada[i].NumeroChamado, entrada[i].NumeroSeqChamado);
                            item.servContratadoCod = entrada[i].CodigoAtServicoContraTipo.ToString();
                            item.servContratadoDesc = entrada[i].DescricaoAtServicoContraTipo;
                            item.tipo = entrada[i].ModSol;
                            item.cidade = entrada[i].Cidade;
                            item.uf = entrada[i].UF;
                            item.tempoDeAtend = entrada[i].TempoAtendimento;

                            if (entrada[i].DataContraOrdem.HasValue)
                            {
                                item.dataContraOrdem = entrada[i].DataContraOrdem.Value.ToString(formatDateTime);
                            }

                            if (entrada[i].DataBaixa.HasValue)
                            {
                                item.dataBaixa = entrada[i].DataBaixa.Value.ToString(formatDateTime);
                            }

                            if (entrada[i].DataSLA.HasValue)
                            {
                                item.baixaSLA = entrada[i].DataSLA.Value.ToString(formatDateTime);
                            }

                            if (entrada[i].DataOS.HasValue)
                            {
                                item.baixaOS = entrada[i].DataOS.Value.ToString(formatDateTime);
                            }

                            item.envio = entrada[i].DataEnvio.ToString(formatDateTime);
                            item.prevista = entrada[i].DataPrevista.ToString(formatDateTime);
                            item.tempoRealAtend = entrada[i].TempoRealAtendimento;

                            if (entrada[i].DataPrevista2Tentativa.HasValue)
                            {
                                item.dhPrevista2Tentativa = entrada[i].DataPrevista2Tentativa.Value.ToString(formatDateTime);
                            }

                            if (entrada[i].DataPrevistaUltimaTentativa.HasValue)
                            {
                                item.dhPrevistaUltimaTentativa = entrada[i].DataPrevistaUltimaTentativa.Value.ToString(formatDateTime);
                            }

                            item.dcMotivoMemo = entrada[i].DescricaoMotivoMemo;
                            item.nLogico = entrada[i].NumeroLogico.ToString();
                            item.maquineta = entrada[i].NumeroEstabelecimento;
                            item.capital = entrada[i].Capital;
                            item.atraso = entrada[i].Atraso;
                            item.motRetornoCod = entrada[i].CodigoMotivoRetorno;
                            item.motRetornoDesc = entrada[i].DescricaoMotivoRetorno;

                            if (entrada[i].CodigoOcorrenciaManutencao.HasValue)
                            {
                                item.codManutencao = entrada[i].CodigoOcorrenciaManutencao.Value.ToString();
                            }

                            item.dcObservacao = entrada[i].Observacao;
                            item.qtRncd = entrada[i].QuantidadeReincidencia;

                            if (entrada[i].Data1Tentativa.HasValue)
                            {
                                item.dh1Tentativa = entrada[i].Data1Tentativa.Value.ToString(formatDateTime);
                            }

                            if (entrada[i].Data2Tentativa.HasValue)
                            {
                                item.dh2Tentativa = entrada[i].Data2Tentativa.Value.ToString(formatDateTime);
                            }

                            arEnvio[i] = item;

                            Logger.LogInfo("FaturamentoSAP > Item: '{0}'".ToFormat(item.JsonSerializeObject()));
                        }

                        

                        ServicoCallSAP.SI_FaturamentoLogistico_Async_Outb(arEnvio);
                        ServicoCallSAP.SI_FaturamentoLogistico_Async_OutbCompleted += ServicoCallSAP_SI_FaturamentoLogistico_Async_OutbCompleted;
                        //ServicoCallSAP.SI_FaturamentoLogistico_Async_OutbAsync(arEnvio);
                        

                        Logger.LogInfo("ServicoFaturamentoSAP > Enviado com sucesso o lote de [{0}] chamados".ToFormat(entrada.Count));
                    }
                    else
                    {
                        throw new InvalidProgramException("Não existem chamados a ser enviado para o Faturamento-SAP");
                    }
                }
                else
                {
                    throw new FormatException("Não foi iniciado o serviço para envio para SAP Faturamento");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ServicoCallSAP_SI_FaturamentoLogistico_Async_OutbCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #region Dispose
        public void Dispose()
        {
            try
            {
                if (ServicoCallSAP.IsNotNull())
                {
                    try
                    {
                        ServicoCallSAP.Close();
                    }
                    catch { }

                    ServicoCallSAP = null;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Ocorreu erro no Dispose da classe ServicoFaturamentoSAP. Erro: [{0}]".ToFormat(ex.GetAllErrorDetail()), ex);
            }
        }
        #endregion
        #endregion
    }
}*/