using Senac.Fecomercio.Common;
using Senac.Fecomercio.ControlaWebServices.Base;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml;

namespace Senac.Fecomercio.ControlaWebServices.FaturamentoSAP
{
    public class ServicoFaturamentoSAPWithRest : ServicoBaseRest
    {
        #region MetodosOverrides
        protected override string GetUrl()
        {
            string protocolo = Extension.GetValueConfig("SAP_FATURAMENTO_XI_PROTOCOLO", true);
            string servidor = Extension.GetValueConfig("SAP_FATURAMENTO_XI_SERVIDOR", true);
            string porta = Extension.GetValueConfig("SAP_FATURAMENTO_XI_SERVIDOR_PORTA", true);
            string senderService = Extension.GetValueConfig("SAP_FATURAMENTO_XI_SENDER_SERVICE", true);
            string interfaceService = Extension.GetValueConfig("SAP_FATURAMENTO_XI_INTERFACE_SERVICE", true);

            string urlServico = "{0}://{1}:{2}/XISOAPAdapter/MessageServlet?senderParty=&senderService={3}&receiverParty=&receiverService=&interface={4}&interfaceNamespace=urn:cielo:sap:gtec:faturamentoLogistico".ToFormat(protocolo, servidor, porta, senderService, interfaceService);

            return urlServico;
        }

        protected override string GetUserAgent()
        {
            return Extension.GetValueConfig("SAP_FATURAMENTO_USER_AGENT", true);
        }

        protected override string GetContentType()
        {
            return Extension.GetValueConfig("SAP_FATURAMENTO_CONTENT_TYPE", true);
        }

        protected override string GetMethod()
        {
            return "POST";
        }
        #endregion

        #region Metodos
        public void EnviarChamado(List<Entities.FaturamentoSAP.FaturamentoSAP> entrada)
        {
            StringBuilder sbEnvelope = null;
            StringBuilder sbDados = null;
            try
            {
                string formatDateTime = Extension.GetValueConfig("SAP_FATURAMENTO_DATE_TIME_FORMAT", true);
                string xmlSendHeaderFooter = System.IO.File.ReadAllText(@"{0}\FaturamentoSAP\FaturamentoSAPRequest.xml".ToFormat(typeof(ServicoFaturamentoSAPWithRest).Assembly.GetDirectoryPath()));
                string xmlSendDadosBase = System.IO.File.ReadAllText(@"{0}\FaturamentoSAP\FaturamentoSAPDados.xml".ToFormat(typeof(ServicoFaturamentoSAPWithRest).Assembly.GetDirectoryPath()));

                sbEnvelope = new StringBuilder();

                sbDados = new StringBuilder();

                string dataContraOrdem = string.Empty;
                string dataBaixaSistema = string.Empty;
                string dataBaixa = string.Empty;
                string baixaSLA = string.Empty;
                string baixaOS = string.Empty;
                string dhPrevista2Tentativa = string.Empty;
                string dhPrevistaUltimaTentativa = string.Empty;
                string codManutencao = string.Empty;
                string dh1Tentativa = string.Empty;
                string dh2Tentativa = string.Empty;
                string novaLinha = Environment.NewLine;

                foreach (var item in entrada)
                {
                    dataContraOrdem = string.Empty;
                    dataBaixaSistema = string.Empty;
                    dataBaixa = string.Empty;
                    baixaSLA = string.Empty;
                    baixaOS = string.Empty;
                    dhPrevista2Tentativa = string.Empty;
                    dhPrevistaUltimaTentativa = string.Empty;
                    codManutencao = string.Empty;
                    dh1Tentativa = string.Empty;
                    dh2Tentativa = string.Empty;

                    if (item.DataContraOrdem.HasValue)
                    {
                        dataContraOrdem = item.DataContraOrdem.Value.ToString(formatDateTime);
                    }

                    if (item.DataBaixaSistema.HasValue)
                    {
                        dataBaixaSistema = item.DataBaixaSistema.Value.ToString(formatDateTime);
                    }

                    if (item.DataBaixa.HasValue)
                    {
                        dataBaixa = item.DataBaixa.Value.ToString(formatDateTime);
                    }

                    if (item.DataSLA.HasValue)
                    {
                        baixaSLA = item.DataSLA.Value.ToString(formatDateTime);
                    }

                    if (item.DataOS.HasValue)
                    {
                        baixaOS = item.DataOS.Value.ToString(formatDateTime);
                    }

                    if (item.DataPrevista2Tentativa.HasValue)
                    {
                        dhPrevista2Tentativa = item.DataPrevista2Tentativa.Value.ToString(formatDateTime);
                    }

                    if (item.DataPrevistaUltimaTentativa.HasValue)
                    {
                        dhPrevistaUltimaTentativa = item.DataPrevistaUltimaTentativa.Value.ToString(formatDateTime);
                    }

                    if (item.CodigoOcorrenciaManutencao.HasValue)
                    {
                        codManutencao = item.CodigoOcorrenciaManutencao.Value.ToString();
                    }

                    if (item.Data1Tentativa.HasValue)
                    {
                        dh1Tentativa = item.Data1Tentativa.Value.ToString(formatDateTime);
                    }

                    if (item.Data2Tentativa.HasValue)
                    {
                        dh2Tentativa = item.Data2Tentativa.Value.ToString(formatDateTime);
                    }

                    sbDados.AppendFormat(xmlSendDadosBase
                        , item.CodigoServicoTipo
                        , item.DescricaoServicoTipo.ToStringHtmlEncode()
                        , item.CodigoEvento
                        , item.CodigoOperador
                        , item.CodigoFilial
                        , item.StatusChamado
                        , item.CodigoOcorrenciaCategoria
                        , item.DescricaoOcorrenciaCategoria.ToStringHtmlEncode()
                        , item.CodigoOcorrencia
                        , item.DescricaoOcorrencia.ToStringHtmlEncode()
                        , item.NumeroChamado
                        , item.NumeroSeqChamado
                        , item.ChamadoEDS.Left(7)
                        , item.CodigoAtServicoContraTipo
                        , item.DescricaoAtServicoContraTipo.ToStringHtmlEncode()
                        , item.ModSol.ToStringHtmlEncode()
                        , item.Cidade.ToStringHtmlEncode()
                        , item.UF.ToStringHtmlEncode()
                        , item.TempoAtendimento.ToStringHtmlEncode()
                        , dataContraOrdem
                        , dataBaixaSistema
                        , dataBaixa
                        , baixaSLA
                        , baixaOS
                        , item.DataEnvio.ToString(formatDateTime)
                        , item.DataPrevista.ToString(formatDateTime)
                        , item.TempoRealAtendimento.ToStringHtmlEncode()
                        , dhPrevista2Tentativa
                        , dhPrevistaUltimaTentativa
                        , item.DescricaoMotivoMemo.ToStringHtmlEncode()
                        , item.NumeroLogico
                        , item.NumeroEstabelecimento
                        , item.Capital
                        , item.Atraso.ToStringHtmlEncode()
                        , item.CodigoMotivoRetorno
                        , item.DescricaoMotivoRetorno.ToStringHtmlEncode()
                        , codManutencao
                        , item.Observacao.ToStringHtmlEncode()
                        , item.QuantidadeReincidencia.ToStringHtmlEncode()
                        , dh1Tentativa
                        , dh2Tentativa
                        , item.Quantidade
                        , novaLinha
                        );

                    Logger.LogInfo("FaturamentoSAP > Item: '{0}'".ToFormat(item.JsonSerializeObject()));
                }

                sbEnvelope.AppendFormat(xmlSendHeaderFooter, sbDados.ToString());

                string usuario = Extension.GetValueConfig("SAP_FATURAMENTO_XI_CREDENTIAL_USER", true);
                string senha = Extension.GetValueConfig("SAP_FATURAMENTO_XI_CREDENTIAL_PASS", true);
                string authType = Extension.GetValueConfig("SAP_FATURAMENTO_XI_CREDENTIAL_AUTH_TYPE", true);

                var crypt = new Crypt();
                usuario = crypt.Decrypt(usuario);
                senha = crypt.Decrypt(senha);

                HabilitarValidacaoSSL();

                AddCredentialCache(usuario, senha, authType);

                HttpStatusCode[] codeSucesso = new HttpStatusCode[2];
                codeSucesso[0] = HttpStatusCode.OK;
                codeSucesso[1] = HttpStatusCode.InternalServerError;

                int? statusCodeResponse = null;
                string xmlRetorno = CallService(codeSucesso, out statusCodeResponse, sbEnvelope.ToString());

                if (statusCodeResponse.HasValue && statusCodeResponse.Value.Equals((int)HttpStatusCode.InternalServerError))
                {
                    xmlRetorno = xmlRetorno.Replace("SOAP:", "");

                    xmlRetorno = xmlRetorno.RemoveAllNamespaces();

                    if (xmlRetorno.IsNull() || (xmlRetorno.IsNotNull() && !xmlRetorno.IsXML()))
                    {
                        throw new XmlException("O XML de retorno não é válido. '{0}'".ToFormat(xmlRetorno));
                    }
                    else
                    {
                        XmlDocument xDocResponse = null;

                        xDocResponse = new XmlDocument();
                        xDocResponse.LoadXml(xmlRetorno);

                        XmlNode root = xDocResponse.DocumentElement;
                        XmlNode nodeCodigoRetorno = root.SelectSingleNode("//Envelope/Body/Fault/detail");

                        if (nodeCodigoRetorno.IsNotNull())
                        {
                            throw new XmlException("Erro ao chamar serviço Faturamento SAP. '{0}'".ToFormat(nodeCodigoRetorno.InnerText));
                        }
                        else
                        {
                            throw new XmlException("O XML de retorno não é válido. '{0}'".ToFormat(xmlRetorno));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("ServicoFaturamentoSAPWithRest > Error > '{0}'".ToFormat(ex.GetAllErrorDetail()), ex);
                throw ex;
            }
            finally
            {
                if (sbEnvelope.IsNotNull())
                {
                    sbEnvelope.Clear();
                    sbEnvelope = null;
                }

                if (sbDados.IsNotNull())
                {
                    sbDados.Clear();
                    sbDados = null;
                }
            }
        }
        #endregion
    }
}