using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using UT = Cielo.Gtec.BLL.Utilities.GtecAspUtil;
using CieloGtecDAL.DAL;

namespace Cielo.Gtec.BLL.Model
{
    public class ST_REQUEST
    {
        #region Propriedades
        public string usuario;
        public string dominio;

        public string reqHISTORICO;
        public string reqCHAMADO_STATUS;
        public string reqCHAMADO_STATUS_OLD;
        public string reqSERVICO_EXECUTADO;
        public string reqMOD_SOL;
        public string reqAT_DESIGNADO;
        public int? reqATSERVCONTR;
        public string reqCONTATOEC;
        public string reqUSUARIO_CO;
        public string reqDOMINIO_CO;
        public string reqTECNICO;
        public int reqNum_TENTATIVA;
        public string reqATFilial;
        public bool reqoptShowFormTranf;
        public string reqMODPREF;
        public int? reqOCORRENCIA;
        public DateTime? reqDH_RETORNO_POS_BASE;
        public DateTime? reqDH_AGENDAMENTO;
        public DateTime? reqDH_ACOMPANHAMENTO;
        public DateTime? reqDH_BXLOGISTICA;
        public DateTime? reqDH_RETENTATIVA;
        public DateTime? reqDH_PROX_RETENTATIVA;
        public int? reqMotivo_RETENTATIVA;
        public string reqObs_RETENTATIVA;
        //public DateTime? reqDH_BAIXA_Date;
        //public DateTime? reqDH_BAIXA_Time;
        public DateTime? reqDH_BAIXA;
        public string reqDC_MENSAGEM_TRANSF;
        public string reqCD_DEPENDENCIA_TRANSF;
        public string reqONTHEFLY_CANCEL_SOLUC;
        public string reqCHAMADO_STATUS_REATIV;

        //public object reqMaquineta;
        //public object reqNLogico;
        //public object reqNChamado;
        //public object reqNChamadoSeq;
        //public object reqNChamadoEDS;
        //public object reqTServ;
        //public object reqTServFilter;
        //public object reqAT;
        //public object reqAT_DESC;
        //public object reqCHStatus;
        //public DateTime? reqDtOcorrenciaBegin;
        //public DateTime? reqDtOcorrenciaEnd;
        //public DateTime? reqDtEnvioBegin;
        //public DateTime? reqDtEnvioEnd;
        //public DateTime? reqDtBaixaBegin;
        //public DateTime? reqDtBaixaEnd;
        //public object IN_OPERADOR_LOGISTICO;
        //public DateTime? reqDH_ENTRADA;
        //public DateTime? reqDH_CAPTURA;
        //public DateTime? reqDH_ENVIO;
        //public object reqEVENTO;
        //public object reqDEPENDENCIA;
        //public object reqARQUIVO_ENVIADO;
        //public object reqATENDTEMPO;
        //public DateTime? reqDH_CONTRAORDEM;
        ////public object reqCONTATOEC_CO;
        //public object reqAT_FILIAL_DESIGNA;
        //public object reqCONTRA_ORDEM;
        //public object reqSERVICO_EXECUTADO_DESC;
        //public object reqSERVICO_TIPO;
        //public object reqSE_Cancelamento;
        //public object reqCHAMADO_STATUS_NEW;
        //public object reqIN_ACAO;
        //public object reqDC_DEPEND_TRANF;
        //public object reqERRO;
        //public object reqERROEvento;
        //public object reqERROProcesso;
        //public object reqbitSOLUCAO_DEF;
        //public object reqSOLUCAO_DEF;
        //public object reqNU_CEP;
        //public object reqUF;
        //public object reqLocalidade;
        //public object reqSG_UF;
        //public object reqCD_LOCAL;
        //public object reqCD_BAIRRO;
        //public object reqClasse;
        //public object reqSegmento;
        //public object reqPacote;
        //public object reqMCC;
        //public object reqAgendado;
        //public object reqCHStatus2N;
        //public object reqEVENTOFILTER;
        #endregion

        #region Construtor
        public ST_REQUEST()
        {
            //reqHISTORICO = null;
            //reqCHAMADO_STATUS = null;
            //reqCHAMADO_STATUS_OLD = null;

            //reqMaquineta = null;
            //reqNLogico = null;
            //reqNChamado = null;
            //reqNChamadoSeq = null;
            //reqNChamadoEDS = null;
            //reqTServ = null;
            //reqTServFilter = null;
            //reqAT = null;
            //reqAT_DESC = null;
            //reqOCORRENCIA = null;
            //reqCHStatus = null;
            //reqDtOcorrenciaBegin = null;
            //reqDtOcorrenciaEnd = null;
            //reqDtEnvioBegin = null;
            //reqDtEnvioEnd = null;
            //reqDtBaixaBegin = null;
            //reqDtBaixaEnd = null;
            //IN_OPERADOR_LOGISTICO = null;
            //reqDH_BAIXA_Date = null;
            //reqDH_BAIXA_Time = null;
            //reqDH_BAIXA = null;
            //reqDH_RETENTATIVA = null;
            //reqDH_PROX_RETENTATIVA = null;
            //reqDH_RETORNO_POS_BASE = null;
            //reqMotivo_RETENTATIVA = null;
            //reqObs_RETENTATIVA = null;
            //reqNum_TENTATIVA = null;
            //reqDH_AGENDAMENTO = null;
            //reqDH_BXLOGISTICA = null;
            //reqDH_ACOMPANHAMENTO = null;
            //reqDH_ENTRADA = null;
            //reqDH_CAPTURA = null;
            //reqDH_ENVIO = null;
            //reqEVENTO = null;
            //reqDEPENDENCIA = null;
            //reqARQUIVO_ENVIADO = null;
            //reqATENDTEMPO = null;
            //reqDH_CONTRAORDEM = null;
            //reqUSUARIO_CO = null;
            //reqDOMINIO_CO = null;
            ////reqCONTATOEC_CO = null;
            //reqTECNICO = null;
            //reqCONTATOEC = null;
            //reqAT_DESIGNADO = null;
            //reqATSERVCONTR = null;
            //reqAT_FILIAL_DESIGNA = null;
            //reqATFilial = null;
            //reqMOD_SOL = null;
            //reqCONTRA_ORDEM = null;
            //reqSERVICO_EXECUTADO = null;
            //reqSERVICO_EXECUTADO_DESC = null;
            //reqSERVICO_TIPO = null;
            //reqSE_Cancelamento = null;
            //reqCHAMADO_STATUS_NEW = null;
            //reqoptShowFormTranf = null;
            //reqIN_ACAO = null;
            //reqCD_DEPENDENCIA_TRANSF = null;
            //reqDC_DEPEND_TRANF = null;
            //reqDC_MENSAGEM_TRANSF = null;
            //reqMODPREF = null;
            //reqERRO = null;
            //reqERROEvento = null;
            //reqERROProcesso = null;
            //reqbitSOLUCAO_DEF = null;
            //reqSOLUCAO_DEF = null;
            //reqNU_CEP = null;
            //reqUF = null;
            //reqLocalidade = null;
            //reqSG_UF = null;
            //reqCD_LOCAL = null;
            //reqCD_BAIRRO = null;
            //reqClasse = null;
            //reqSegmento = null;
            //reqPacote = null;
            //reqMCC = null;
            //reqAgendado = null;
            //reqCHStatus2N = null;
            //reqEVENTOFILTER = null;
        }

        //public string GET_reqCHAMADO_STATUS_NEW(BDConexao db)
        //{
        //    // =>
        //    string strWAY2GO = "";
        //    switch (strWAY2GO.ToUpper())
        //    {
        //        //case "EDIT":
        //        //case "EDIT_ACTION":
        //        //    if (reqSERVICO_EXECUTADO.Length > 0)
        //        //    {
        //        //        reqSE_Cancelamento = CapturaDesc("gtec", "SELECT IN_CANCELAMENTO FROM dbo.TBGTR_SERVICO_EXECUTADO WITH (NOLOCK) WHERE CD_SERVICO_EXECUTADO = '" & reqSERVICO_EXECUTADO & "'", 0);
        //        //        if (FormataBit(reqSE_Cancelamento, "01") = "1" && Len(reqSERVICO_TIPO) > 0)
        //        //        {
        //        //            reqCHAMADO_STATUS_NEW = CapturaDesc("gtec", "SELECT CHST.CD_CHAMADO_STATUS FROM TBGTR_CHAMSTATUS CHST (NOLOCK) INNER JOIN TBGTR_SERV_EXEC_TSERV SETS (NOLOCK) ON CHST.CD_CHAMADO_STATUS = SETS.CD_CHAMADO_STATUS WHERE SETS.CD_SERVICO_TIPO =" & reqSERVICO_TIPO & " AND CD_SERVICO_EXECUTADO = '" & reqSERVICO_EXECUTADO & "'", reqSERVICO_TIPO);
        //        //        }
        //        //        else
        //        //        {
        //        //            reqCHAMADO_STATUS_NEW = reqCHAMADO_STATUS;
        //        //        }
        //        //        if (IsNumeric(reqCHAMADO_STATUS_NEW))
        //        //        {
        //        //            switch (reqCHAMADO_STATUS_NEW)
        //        //            {
        //        //                case "1":
        //        //                    reqCHAMADO_STATUS_NEW = "I";
        //        //                    break;
        //        //                case "2":
        //        //                    reqCHAMADO_STATUS_NEW = "M";
        //        //                    break;
        //        //                case "3":
        //        //                    reqCHAMADO_STATUS_NEW = "D";
        //        //                    break;
        //        //                case "4":
        //        //                    reqCHAMADO_STATUS_NEW = "T";
        //        //                    break;
        //        //                case "5":
        //        //                    reqCHAMADO_STATUS_NEW = "F";
        //        //                    break;
        //        //            }
        //        //            if (Len(reqDH_ENVIO) = 0)
        //        //            {
        //        //                reqCHAMADO_STATUS_NEW = reqCHAMADO_STATUS_NEW & "CS";
        //        //            }
        //        //            else
        //        //            {
        //        //                reqCHAMADO_STATUS_NEW = reqCHAMADO_STATUS_NEW & "CV";
        //        //            }
        //        //        }
        //        //    }
        //        //    break;

        //        default:
        //            if (reqSERVICO_EXECUTADO.Length > 0 && Len(reqTServ) > 0)
        //            {
        //                reqCHAMADO_STATUS_NEW = CapturaDesc("gtec", "SELECT CHST.CD_CHAMADO_STATUS FROM TBGTR_CHAMSTATUS CHST (NOLOCK) INNER JOIN TBGTR_SERV_EXEC_TSERV SETS (NOLOCK) ON CHST.CD_CHAMADO_STATUS = SETS.CD_CHAMADO_STATUS WHERE SETS.CD_SERVICO_TIPO =" & reqTServ & " AND CD_SERVICO_EXECUTADO = " & reqSERVICO_EXECUTADO, reqTServ);
        //            }
        //            if (IsNumeric(reqCHAMADO_STATUS_NEW))
        //            {
        //                switch (reqCHAMADO_STATUS_NEW)
        //                {
        //                    case "1":
        //                        reqCHAMADO_STATUS_NEW = "ICV";
        //                        break;
        //                    case "2":
        //                        reqCHAMADO_STATUS_NEW = "MCV";
        //                        break;
        //                    case "3":
        //                        reqCHAMADO_STATUS_NEW = "ICV";
        //                        break;
        //                    case "4":
        //                        reqCHAMADO_STATUS_NEW = "TCV";
        //                        break;
        //                    case "5":
        //                        reqCHAMADO_STATUS_NEW = "FCV";
        //                        break;
        //                }
        //            }
        //            break;
        //    }
        //}

        #endregion
    }
}
