using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using UT = Cielo.Gtec.BLL.Utilities.GtecAspUtil;

namespace Cielo.Gtec.BLL.Model
{
    public class ModelChamadoItem
    {
        #region Propriedades
        public int NU_CHAMADO; // [NU_CHAMADO] [int] NOT NULL,
        public int NU_SEQCHAMADO; // [NU_SEQCHAMADO] [int] NOT NULL,
        public string CD_CHAMADO_EDS; // [CD_CHAMADO_EDS] [varchar](8) NULL,
        public int? CD_ATENDIMENTO; // [CD_ATENDIMENTO] [int] NULL,
        public int? NU_CONTRAORDEM; // [NU_CONTRAORDEM] [int] NULL,
        public DateTime? DH_CONTRAORDEM; // [DH_CONTRAORDEM] [datetime] NULL,
        public string NM_USUARIO_CO; // [NM_USUARIO_CO] [varchar](20) NULL,
        public string NM_DOMINIO_CO; // [NM_DOMINIO_CO] [varchar](20) NULL,
        public string NM_CONTATOEC_CO; // [NM_CONTATOEC_CO] [varchar](50) NULL,
        public decimal? NU_CPF_TECNICO; // [NU_CPF_TECNICO] [numeric](14, 0) NULL,
        public string NM_TECNICO_CO; // [NM_TECNICO_CO] [varchar](100) NULL,
        public int? CD_OCORRENCIA; // [CD_OCORRENCIA] [int] NULL,
        public string DC_OCORRENCIA; // [DC_OCORRENCIA] [varchar](60) NOT NULL,
        public bool IN_GERA_NU_LOGICO; // [IN_GERA_NU_LOGICO] [bit] NOT NULL,
        public int CD_SERVICO_TIPO; // [CD_SERVICO_TIPO] [int] NOT NULL,
        public string DC_SERVICO_TIPO; // [DC_SERVICO_TIPO] [varchar](30) NOT NULL,
        public int? CD_EVENTO; // [CD_EVENTO] [int] NOT NULL,
        public int? CD_DEPENDENCIA; // [CD_DEPENDENCIA] [int] NOT NULL,
        public string CD_CHAMADO_STATUS; // [CD_CHAMADO_STATUS] [varchar](3) NULL,
        public string DC_CHAMADO_STATUS; // [DC_CHAMADO_STATUS] [varchar](50) NULL,
        public int? NU_PESO; // [NU_PESO] [int] NULL,
        public bool IN_AGENDAMENTO; // [IN_AGENDAMENTO] [bit] NULL,
        public bool IN_TRATAMENTO; // [IN_TRATAMENTO] [bit] NULL,
        public bool IN_TRATAMENTO_ERRO; // [IN_TRATAMENTO_ERRO] [bit] NULL,
        public bool IN_ENVIA_EMAIL; // [IN_ENVIA_EMAIL] [bit] NULL,
        public bool IN_BAIXA_CHAMADO; // [IN_BAIXA_CHAMADO] [bit] NULL,
        public bool IN_CANCELAMENTO; // [IN_CANCELAMENTO] [bit] NULL,
        public bool IN_VISITA; // [IN_VISITA] [bit] NULL,
        public string CD_AT_DESIGNADO; // [CD_AT_DESIGNADO] [varchar](2) NULL,
        public string DC_AT; // [DC_AT] [varchar](30) NULL,
        public int? CD_ATSERVCONTR_DESIG; // [CD_ATSERVCONTR_DESIG] [int] NULL,
        public string DC_ATSERVCONTRATTIPO; // [DC_ATSERVCONTRATTIPO] [varchar](32) NULL,
        public string NM_ARQUIVO_ENVIADO; // [NM_ARQUIVO_ENVIADO] [varchar](30) NULL,
        public string CD_AT_FILIAL_DESIGNA; // [CD_AT_FILIAL_DESIGNA] [varchar](4) NULL,
        public string DC_AT_FILIAL; // [DC_AT_FILIAL] [varchar](30) NULL,
        public int? CD_ATENDTEMPO; // [CD_ATENDTEMPO] [int] NULL,
        public bool IN_EQUIPAMENTO_NOVO; // [IN_EQUIPAMENTO_NOVO] [bit] NOT NULL,
        public int? NU_LOGICO; // [NU_LOGICO] [int] NULL,
        public string CD_MOD_SOL_DESIGNADO; // [CD_MOD_SOL_DESIGNADO] [varchar](2) NULL,
        public string DC_MOD_SOL_DESIGNADO; // [DC_MOD_SOL_DESIGNADO] [varchar](50) NULL,
        public string CD_MOD_SOL; // [CD_MOD_SOL] [varchar](2) NULL,
        public string DC_MOD_SOL; // [DC_MOD_SOL] [varchar](50) NULL,
        public string CD_VERSAO_APLICATIVO; // [CD_VERSAO_APLICATIVO] [varchar](12) NULL,
        public bool IN_LEITOR_CB; // [IN_LEITOR_CB] [bit] NULL,
        public string NU_SERIE_LEITOR_CB; // [NU_SERIE_LEITOR_CB] [varchar](30) NULL,
        public string CD_NAC; // [CD_NAC] [varchar](5) NULL,
        public string HR_ATENDTEMPO_REAL; // [HR_ATENDTEMPO_REAL] [varchar](8000) NULL,
        public string HR_ATENDTEMPO_REAL_2; // [HR_ATENDTEMPO_REAL_2] [varchar](8000) NULL,
        public string DC_SLA_ENVIO; // [DC_SLA_ENVIO] [varchar](15) NOT NULL,
        public string DC_SLA_ENTRADA; // [DC_SLA_ENTRADA] [varchar](15) NOT NULL,
        public string CD_SERVICO_EXECUTADO; // [CD_SERVICO_EXECUTADO] [varchar](3) NULL,
        public string DC_SERVICO_EXECUTADO; // [DC_SERVICO_EXECUTADO] [varchar](50) NULL,
        public string NM_ARQUIVO_BXTECNICA; // [NM_ARQUIVO_BXTECNICA] [varchar](80) NULL,
        public string NM_USUARIO; // [NM_USUARIO] [varchar](20) NULL,
        public string NM_DOMINIO; // [NM_DOMINIO] [varchar](20) NULL,
        public DateTime? DH_BLOQUEIO; // [DH_BLOQUEIO] [datetime] NULL,
        public byte? IN_MP_STATUS_IB; // [IN_MP_STATUS_IB] [tinyint] NOT NULL,
        public string DC_MP_STATUS_IB; // [DC_MP_STATUS_IB] [varchar](30) NULL,
        public DateTime? DH_MP_IB; // [DH_MP_IB] [datetime] NULL,
        public bool IN_MP_ERRO_IB; // [IN_MP_ERRO_IB] [bit] NOT NULL,
        public byte? IN_MP_STATUS_ED; // [IN_MP_STATUS_ED] [tinyint] NOT NULL,
        public string DC_MP_STATUS_ED; // [DC_MP_STATUS_ED] [varchar](30) NULL,
        public DateTime? DH_MP_ED; // [DH_MP_ED] [datetime] NULL,
        public bool IN_MP_ERRO_ED; // [IN_MP_ERRO_ED] [bit] NOT NULL,
        public int? IN_MP_STATUS_OL; // [IN_MP_STATUS_OL] [int] NULL,
        public string DC_MP_STATUS_OL; // [DC_MP_STATUS_OL] [varchar](30) NULL,
        public DateTime? DH_MP_OL; // [DH_MP_OL] [datetime] NULL,
        public byte? IN_MP_STATUS_MNT; // [IN_MP_STATUS_MNT] [tinyint] NULL,
        public string DC_MP_STATUS_MNT; // [DC_MP_STATUS_MNT] [varchar](30) NULL,
        public DateTime? DH_MP_MNT; // [DH_MP_MNT] [datetime] NULL,
        public int? CD_ERRO_WS; // [CD_ERRO_WS] [int] NULL,
        public string DC_ERRO_WS; // [DC_ERRO_WS] [varchar](255) NULL,
        public DateTime? DH_ERRO_WS; // [DH_ERRO_WS] [datetime] NULL,
        public string CD_GRUPO_MODSOL; // [CD_GRUPO_MODSOL] [varchar](3) NULL,
        public int? CD_USO_PRODUTO; // [CD_USO_PRODUTO] [int] NULL,
        public string CD_SOLUCAO_DEF; // [CD_SOLUCAO_DEF] [varchar](1) NULL,
        public DateTime? DH_PROCESS_EDS_ST; // [DH_PROCESS_EDS_ST] [datetime] NULL,
        public DateTime? DH_PREVISTA; // [DH_PREVISTA] [datetime] NULL,
        public DateTime? DH_ENVIO; // [DH_ENVIO] [datetime] NULL,
        public DateTime? DH_AGENDAMENTO; // [DH_AGENDAMENTO] [datetime] NULL,
        public DateTime? DH_BXLOGISTICA; // [DH_BXLOGISTICA] [datetime] NULL,
        public DateTime? DH_ACOMPANHAMENTO; // [DH_ACOMPANHAMENTO] [datetime] NULL,
        public DateTime? DH_BAIXA; // [DH_BAIXA] [datetime] NULL,
        public DateTime? DH_ENTRADA; // [DH_ENTRADA] [datetime] NULL,
        public DateTime? DH_CAPTURA; // [DH_CAPTURA] [datetime] NULL,
        public DateTime? DH_RETORNO_POS_BASE; // [DH_RETORNO_POS_BASE] [datetime] NULL,
        public string NU_ESTAB; // [NU_ESTAB] [varchar](10) NULL,
        public int? NU_LOJA; // [NU_LOJA] [int] NULL,
        public string NM_FANTASIA; // [NM_FANTASIA] [varchar](74) NULL,
        public string NM_RAZAO_SOCIAL; // [NM_RAZAO_SOCIAL] [varchar](74) NULL,
        public string NU_CEP; // [NU_CEP] [varchar](30) NULL,
        public string SG_UF; // [SG_UF] [varchar](2) NULL,
        public string NU_BANCO_CB; // [NU_BANCO_CB] [varchar](4) NULL,
        public int? CD_LOCAL; // [CD_LOCAL] [int] NULL,
        public string NM_LOCAL; // [NM_LOCAL] [varchar](147) NULL,
        public string NM_CIDADE; // [NM_CIDADE] [varchar](74) NULL,
        public int? CD_BAIRRO; // [CD_BAIRRO] [int] NULL,
        public string NM_BAIRRO; // [NM_BAIRRO] [varchar](74) NULL,
        public int? CD_TP_LOGR; // [CD_TP_LOGR] [int] NULL,
        public string NM_TP_LOGR; // [NM_TP_LOGR] [varchar](72) NULL,
        public string NM_ENDERECO; // [NM_ENDERECO] [varchar](40) NULL,
        public string NU_LOTE; // [NU_LOTE] [varchar](11) NULL,
        public string NM_COMPLEMENTO; // [NM_COMPLEMENTO] [varchar](40) NULL,
        public string NM_CONTATO; // [NM_CONTATO] [varchar](30) NULL,
        public string NU_DDD; // [NU_DDD] [varchar](4) NULL,
        public string NU_FONE; // [NU_FONE] [varchar](30) NULL,
        public string CD_CLASSE_FAT; // [CD_CLASSE_FAT] [varchar](1) NULL,
        public int? CD_MCC; // [CD_MCC] [int] NULL,
        public string DC_MCC; // [DC_MCC] [varchar](150) NULL,
        public bool IN_OPERADOR_LOGISTICO; // [IN_OPERADOR_LOGISTICO] [bit] NULL,
        public int? QT_TENTATIVA_VISITA; // [QT_TENTATIVA_VISITA] [int] NULL,
        public int? QT_COBRANCA_CHAMADO; // [QT_COBRANCA_CHAMADO] [int] NULL,
        public int? IN_LOGICO; // [IN_LOGICO] [int] NULL,
        public bool IN_PRIORIZADO; // [IN_PRIORIZADO] [bit] NULL,
        public bool IN_ED_TROCA_MANUT; // [IN_ED_TROCA_MANUT] [bit] NOT NULL,
        public int? NU_ACESSO_TERMINAL; // [NU_ACESSO_TERMINAL] [int] NULL,
        public string NM_ARQUIVO_BX_OS; // [NM_ARQUIVO_BX_OS] [varchar](80) NULL,
        public int? CD_SEGMENTO; // [CD_SEGMENTO] [int] NULL,
        public string DC_SEGMENTO; // [DC_SEGMENTO] [varchar](50) NULL,
        public int? CD_PACOTE; // [CD_PACOTE] [int] NULL,
        public string DC_PACOTE; // [DC_PACOTE] [varchar](50) NULL 

        public string CD_VERSAO;
        public string DC_SOLUCAO_DEF;
        #endregion

        #region Construtor
        public ModelChamadoItem(DataRow dr)
        {
            NU_CHAMADO = (int)dr["NU_CHAMADO"];
            NU_SEQCHAMADO = (int)dr["NU_SEQCHAMADO"];
            CD_CHAMADO_EDS = dr["CD_CHAMADO_EDS"] == DBNull.Value ? "" : (string)dr["CD_CHAMADO_EDS"];
            CD_ATENDIMENTO = dr["CD_ATENDIMENTO"] == DBNull.Value ? null : (int?)dr["CD_ATENDIMENTO"];
            NU_CONTRAORDEM = dr["NU_CONTRAORDEM"] == DBNull.Value ? null : (int?)dr["NU_CONTRAORDEM"];
            DH_CONTRAORDEM = dr["DH_CONTRAORDEM"] == DBNull.Value ? null : (DateTime?)dr["DH_CONTRAORDEM"];
            NM_USUARIO_CO = dr["NM_USUARIO_CO"] == DBNull.Value ? "" : (string)dr["NM_USUARIO_CO"];
            NM_DOMINIO_CO = dr["NM_DOMINIO_CO"] == DBNull.Value ? "" : (string)dr["NM_DOMINIO_CO"];
            NM_CONTATOEC_CO = dr["NM_CONTATOEC_CO"] == DBNull.Value ? "" : (string)dr["NM_CONTATOEC_CO"];
            NU_CPF_TECNICO = dr["NU_CPF_TECNICO"] == DBNull.Value ? null : (decimal?)dr["NU_CPF_TECNICO"];
            NM_TECNICO_CO = dr["NM_TECNICO_CO"] == DBNull.Value ? "" : (string)dr["NM_TECNICO_CO"];
            CD_OCORRENCIA = dr["CD_OCORRENCIA"] == DBNull.Value ? null : (int?)dr["CD_OCORRENCIA"];
            DC_OCORRENCIA = dr["DC_OCORRENCIA"] == DBNull.Value ? "" : (string)dr["DC_OCORRENCIA"];
            IN_GERA_NU_LOGICO = dr["IN_GERA_NU_LOGICO"] == DBNull.Value ? false : (bool)dr["IN_GERA_NU_LOGICO"];
            DC_SERVICO_TIPO = dr["DC_SERVICO_TIPO"] == DBNull.Value ? "" : (string)dr["DC_SERVICO_TIPO"];
            CD_EVENTO = dr["CD_EVENTO"] == DBNull.Value ? null : (int?)dr["CD_EVENTO"];
            CD_DEPENDENCIA = dr["CD_DEPENDENCIA"] == DBNull.Value ? null : (int?)dr["CD_DEPENDENCIA"];
            CD_CHAMADO_STATUS = dr["CD_CHAMADO_STATUS"] == DBNull.Value ? "" : (string)dr["CD_CHAMADO_STATUS"];
            DC_CHAMADO_STATUS = dr["DC_CHAMADO_STATUS"] == DBNull.Value ? "" : (string)dr["DC_CHAMADO_STATUS"];
            NU_PESO = dr["NU_PESO"] == DBNull.Value ? null : (int?)dr["NU_PESO"];
            IN_AGENDAMENTO = dr["IN_AGENDAMENTO"] == DBNull.Value ? false : (bool)dr["IN_AGENDAMENTO"];
            IN_TRATAMENTO = dr["IN_TRATAMENTO"] == DBNull.Value ? false : (bool)dr["IN_TRATAMENTO"];
            IN_TRATAMENTO_ERRO = dr["IN_TRATAMENTO_ERRO"] == DBNull.Value ? false : (bool)dr["IN_TRATAMENTO_ERRO"];
            IN_ENVIA_EMAIL = dr["IN_ENVIA_EMAIL"] == DBNull.Value ? false : (bool)dr["IN_ENVIA_EMAIL"];
            IN_BAIXA_CHAMADO = dr["IN_BAIXA_CHAMADO"] == DBNull.Value ? false : (bool)dr["IN_BAIXA_CHAMADO"];
            IN_CANCELAMENTO = dr["IN_CANCELAMENTO"] == DBNull.Value ? false : (bool)dr["IN_CANCELAMENTO"];
            IN_VISITA = dr["IN_VISITA"] == DBNull.Value ? false : (bool)dr["IN_VISITA"];
            CD_AT_DESIGNADO = dr["CD_AT_DESIGNADO"] == DBNull.Value ? "" : (string)dr["CD_AT_DESIGNADO"];
            DC_AT = dr["DC_AT"] == DBNull.Value ? "" : (string)dr["DC_AT"];
            CD_ATSERVCONTR_DESIG = dr["CD_ATSERVCONTR_DESIG"] == DBNull.Value ? null : (int?)dr["CD_ATSERVCONTR_DESIG"];
            DC_ATSERVCONTRATTIPO = dr["DC_ATSERVCONTRATTIPO"] == DBNull.Value ? "" : (string)dr["DC_ATSERVCONTRATTIPO"];
            NM_ARQUIVO_ENVIADO = dr["NM_ARQUIVO_ENVIADO"] == DBNull.Value ? "" : (string)dr["NM_ARQUIVO_ENVIADO"];
            CD_AT_FILIAL_DESIGNA = dr["CD_AT_FILIAL_DESIGNA"] == DBNull.Value ? "" : (string)dr["CD_AT_FILIAL_DESIGNA"];
            CD_ATENDTEMPO = dr["CD_ATENDTEMPO"] == DBNull.Value ? null : (int?)dr["CD_ATENDTEMPO"];
            IN_EQUIPAMENTO_NOVO = dr["IN_EQUIPAMENTO_NOVO"] == DBNull.Value ? false : (bool)dr["IN_EQUIPAMENTO_NOVO"];
            NU_LOGICO = dr["NU_LOGICO"] == DBNull.Value ? null : (int?)dr["NU_LOGICO"];
            CD_MOD_SOL_DESIGNADO = dr["CD_MOD_SOL_DESIGNADO"] == DBNull.Value ? "" : (string)dr["CD_MOD_SOL_DESIGNADO"];
            DC_MOD_SOL_DESIGNADO = dr["DC_MOD_SOL_DESIGNADO"] == DBNull.Value ? "" : (string)dr["DC_MOD_SOL_DESIGNADO"];
            CD_MOD_SOL = dr["CD_MOD_SOL"] == DBNull.Value ? "" : (string)dr["CD_MOD_SOL"];
            DC_MOD_SOL = dr["DC_MOD_SOL"] == DBNull.Value ? "" : (string)dr["DC_MOD_SOL"];
            CD_VERSAO_APLICATIVO = dr["CD_VERSAO_APLICATIVO"] == DBNull.Value ? "" : (string)dr["CD_VERSAO_APLICATIVO"];
            IN_LEITOR_CB = dr["IN_LEITOR_CB"] == DBNull.Value ? false : (bool)dr["IN_LEITOR_CB"];
            NU_SERIE_LEITOR_CB = dr["NU_SERIE_LEITOR_CB"] == DBNull.Value ? "" : (string)dr["NU_SERIE_LEITOR_CB"];
            CD_NAC = dr["CD_NAC"] == DBNull.Value ? "" : (string)dr["CD_NAC"];
            HR_ATENDTEMPO_REAL = dr["HR_ATENDTEMPO_REAL"] == DBNull.Value ? "" : (string)dr["HR_ATENDTEMPO_REAL"];
            HR_ATENDTEMPO_REAL_2 = dr["HR_ATENDTEMPO_REAL_2"] == DBNull.Value ? "" : (string)dr["HR_ATENDTEMPO_REAL_2"];
            DC_SLA_ENVIO = dr["DC_SLA_ENVIO"] == DBNull.Value ? "" : (string)dr["DC_SLA_ENVIO"];
            DC_SLA_ENTRADA = dr["DC_SLA_ENTRADA"] == DBNull.Value ? "" : (string)dr["DC_SLA_ENTRADA"];
            CD_SERVICO_EXECUTADO = dr["CD_SERVICO_EXECUTADO"] == DBNull.Value ? "" : (string)dr["CD_SERVICO_EXECUTADO"];
            DC_SERVICO_EXECUTADO = dr["DC_SERVICO_EXECUTADO"] == DBNull.Value ? "" : (string)dr["DC_SERVICO_EXECUTADO"];
            NM_ARQUIVO_BXTECNICA = dr["NM_ARQUIVO_BXTECNICA"] == DBNull.Value ? "" : (string)dr["NM_ARQUIVO_BXTECNICA"];
            NM_USUARIO = dr["NM_USUARIO"] == DBNull.Value ? "" : (string)dr["NM_USUARIO"];
            NM_DOMINIO = dr["NM_DOMINIO"] == DBNull.Value ? "" : (string)dr["NM_DOMINIO"];
            DH_BLOQUEIO = dr["DH_BLOQUEIO"] == DBNull.Value ? null : (DateTime?)dr["DH_BLOQUEIO"];
            IN_MP_STATUS_IB = dr["IN_MP_STATUS_IB"] == DBNull.Value ? null : (byte?)dr["IN_MP_STATUS_IB"];
            DC_MP_STATUS_IB = dr["DC_MP_STATUS_IB"] == DBNull.Value ? "" : (string)dr["DC_MP_STATUS_IB"];
            DH_MP_IB = dr["DH_MP_IB"] == DBNull.Value ? null : (DateTime?)dr["DH_MP_IB"];
            IN_MP_ERRO_IB = dr["IN_MP_ERRO_IB"] == DBNull.Value ? false : (bool)dr["IN_MP_ERRO_IB"];
            IN_MP_STATUS_ED = dr["IN_MP_STATUS_ED"] == DBNull.Value ? null : (byte?)dr["IN_MP_STATUS_ED"];
            DC_MP_STATUS_ED = dr["DC_MP_STATUS_ED"] == DBNull.Value ? "" : (string)dr["DC_MP_STATUS_ED"];
            DH_MP_ED = dr["DH_MP_ED"] == DBNull.Value ? null : (DateTime?)dr["DH_MP_ED"];
            IN_MP_ERRO_ED = dr["IN_MP_ERRO_ED"] == DBNull.Value ? false : (bool)dr["IN_MP_ERRO_ED"];
            IN_MP_STATUS_OL = dr["IN_MP_STATUS_OL"] == DBNull.Value ? null : (int?)dr["IN_MP_STATUS_OL"];
            DC_MP_STATUS_OL = dr["DC_MP_STATUS_OL"] == DBNull.Value ? "" : (string)dr["DC_MP_STATUS_OL"];
            DH_MP_OL = dr["DH_MP_OL"] == DBNull.Value ? null : (DateTime?)dr["DH_MP_OL"];
            IN_MP_STATUS_MNT = dr["IN_MP_STATUS_MNT"] == DBNull.Value ? null : (byte?)dr["IN_MP_STATUS_MNT"];
            DC_MP_STATUS_MNT = dr["DC_MP_STATUS_MNT"] == DBNull.Value ? "" : (string)dr["DC_MP_STATUS_MNT"];
            DH_MP_MNT = dr["DH_MP_MNT"] == DBNull.Value ? null : (DateTime?)dr["DH_MP_MNT"];
            CD_ERRO_WS = dr["CD_ERRO_WS"] == DBNull.Value ? null : (int?)dr["CD_ERRO_WS"];
            DC_ERRO_WS = dr["DC_ERRO_WS"] == DBNull.Value ? "" : (string)dr["DC_ERRO_WS"];
            DH_ERRO_WS = dr["DH_ERRO_WS"] == DBNull.Value ? null : (DateTime?)dr["DH_ERRO_WS"];
            CD_GRUPO_MODSOL = dr["CD_GRUPO_MODSOL"] == DBNull.Value ? "" : (string)dr["CD_GRUPO_MODSOL"];
            CD_USO_PRODUTO = dr["CD_USO_PRODUTO"] == DBNull.Value ? null : (int?)dr["CD_USO_PRODUTO"];
            CD_SOLUCAO_DEF = dr["CD_SOLUCAO_DEF"] == DBNull.Value ? "" : (string)dr["CD_SOLUCAO_DEF"];
            DH_PROCESS_EDS_ST = dr["DH_PROCESS_EDS_ST"] == DBNull.Value ? null : (DateTime?)dr["DH_PROCESS_EDS_ST"];
            DH_PREVISTA = dr["DH_PREVISTA"] == DBNull.Value ? null : (DateTime?)dr["DH_PREVISTA"];
            DH_ENVIO = dr["DH_ENVIO"] == DBNull.Value ? null : (DateTime?)dr["DH_ENVIO"];
            DH_AGENDAMENTO = dr["DH_AGENDAMENTO"] == DBNull.Value ? null : (DateTime?)dr["DH_AGENDAMENTO"];
            DH_BXLOGISTICA = dr["DH_BXLOGISTICA"] == DBNull.Value ? null : (DateTime?)dr["DH_BXLOGISTICA"];
            DH_ACOMPANHAMENTO = dr["DH_ACOMPANHAMENTO"] == DBNull.Value ? null : (DateTime?)dr["DH_ACOMPANHAMENTO"];
            DH_BAIXA = dr["DH_BAIXA"] == DBNull.Value ? null : (DateTime?)dr["DH_BAIXA"];
            DH_ENTRADA = dr["DH_ENTRADA"] == DBNull.Value ? null : (DateTime?)dr["DH_ENTRADA"];
            DH_CAPTURA = dr["DH_CAPTURA"] == DBNull.Value ? null : (DateTime?)dr["DH_CAPTURA"];
            DH_RETORNO_POS_BASE = dr["DH_RETORNO_POS_BASE"] == DBNull.Value ? null : (DateTime?)dr["DH_RETORNO_POS_BASE"];
            NU_ESTAB = dr["NU_ESTAB"] == DBNull.Value ? "" : (string)dr["NU_ESTAB"];
            NU_LOJA = dr["NU_LOJA"] == DBNull.Value ? null : (int?)dr["NU_LOJA"];
            NM_FANTASIA = dr["NM_FANTASIA"] == DBNull.Value ? "" : (string)dr["NM_FANTASIA"];
            NM_RAZAO_SOCIAL = dr["NM_RAZAO_SOCIAL"] == DBNull.Value ? "" : (string)dr["NM_RAZAO_SOCIAL"];
            NU_CEP = dr["NU_CEP"] == DBNull.Value ? "" : (string)dr["NU_CEP"];
            SG_UF = dr["SG_UF"] == DBNull.Value ? "" : (string)dr["SG_UF"];
            NU_BANCO_CB = dr["NU_BANCO_CB"] == DBNull.Value ? "" : (string)dr["NU_BANCO_CB"];
            CD_LOCAL = dr["CD_LOCAL"] == DBNull.Value ? null : (int?)dr["CD_LOCAL"];
            NM_LOCAL = dr["NM_LOCAL"] == DBNull.Value ? "" : (string)dr["NM_LOCAL"];
            NM_CIDADE = dr["NM_CIDADE"] == DBNull.Value ? "" : (string)dr["NM_CIDADE"];
            CD_BAIRRO = dr["CD_BAIRRO"] == DBNull.Value ? null : (int?)dr["CD_BAIRRO"];
            NM_BAIRRO = dr["NM_BAIRRO"] == DBNull.Value ? "" : (string)dr["NM_BAIRRO"];
            CD_TP_LOGR = dr["CD_TP_LOGR"] == DBNull.Value ? null : (int?)dr["CD_TP_LOGR"];
            NM_TP_LOGR = dr["NM_TP_LOGR"] == DBNull.Value ? "" : (string)dr["NM_TP_LOGR"];
            NM_ENDERECO = dr["NM_ENDERECO"] == DBNull.Value ? "" : (string)dr["NM_ENDERECO"];
            NU_LOTE = dr["NU_LOTE"] == DBNull.Value ? "" : (string)dr["NU_LOTE"];
            NM_COMPLEMENTO = dr["NM_COMPLEMENTO"] == DBNull.Value ? "" : (string)dr["NM_COMPLEMENTO"];
            NM_CONTATO = dr["NM_CONTATO"] == DBNull.Value ? "" : (string)dr["NM_CONTATO"];
            NU_DDD = dr["NU_DDD"] == DBNull.Value ? "" : (string)dr["NU_DDD"];
            NU_FONE = dr["NU_FONE"] == DBNull.Value ? "" : (string)dr["NU_FONE"];
            CD_CLASSE_FAT = dr["CD_CLASSE_FAT"] == DBNull.Value ? "" : (string)dr["CD_CLASSE_FAT"];
            CD_MCC = dr["CD_MCC"] == DBNull.Value ? null : (int?)dr["CD_MCC"];
            DC_MCC = dr["DC_MCC"] == DBNull.Value ? "" : (string)dr["DC_MCC"];
            IN_OPERADOR_LOGISTICO = dr["IN_OPERADOR_LOGISTICO"] == DBNull.Value ? false : (bool)dr["IN_OPERADOR_LOGISTICO"];
            QT_TENTATIVA_VISITA = dr["QT_TENTATIVA_VISITA"] == DBNull.Value ? null : (int?)dr["QT_TENTATIVA_VISITA"];
            QT_COBRANCA_CHAMADO = dr["QT_COBRANCA_CHAMADO"] == DBNull.Value ? null : (int?)dr["QT_COBRANCA_CHAMADO"];
            IN_LOGICO = dr["IN_LOGICO"] == DBNull.Value ? null : (int?)dr["IN_LOGICO"];
            IN_PRIORIZADO = dr["IN_PRIORIZADO"] == DBNull.Value ? false : (bool)dr["IN_PRIORIZADO"];
            IN_ED_TROCA_MANUT = dr["IN_ED_TROCA_MANUT"] == DBNull.Value ? false : (bool)dr["IN_ED_TROCA_MANUT"];
            NU_ACESSO_TERMINAL = dr["NU_ACESSO_TERMINAL"] == DBNull.Value ? null : (int?)dr["NU_ACESSO_TERMINAL"];
            NM_ARQUIVO_BX_OS = dr["NM_ARQUIVO_BX_OS"] == DBNull.Value ? "" : (string)dr["NM_ARQUIVO_BX_OS"];
            CD_SEGMENTO = dr["CD_SEGMENTO"] == DBNull.Value ? null : (int?)dr["CD_SEGMENTO"];
            DC_SEGMENTO = dr["DC_SEGMENTO"] == DBNull.Value ? "" : (string)dr["DC_SEGMENTO"];
            CD_PACOTE = dr["CD_PACOTE"] == DBNull.Value ? null : (int?)dr["CD_PACOTE"];
            DC_PACOTE = dr["DC_PACOTE"] == DBNull.Value ? "" : (string)dr["DC_PACOTE"];

            // Campos com tratamento diferenciado em relação ao padrão acima
            CD_SERVICO_TIPO = (int)dr["CD_SERVICO_TIPO"];
            DC_AT_FILIAL = dr["DC_AT_FILIAL"] == DBNull.Value ? "" : ((string)dr["DC_AT_FILIAL"]).ToUpper();
            CD_VERSAO = CD_VERSAO_APLICATIVO.Length == 0 ? "" : UT.Left(UT.Right(CD_VERSAO_APLICATIVO, 3), 2);
            DC_SOLUCAO_DEF = UT.Get_DC_SOLUCAO_DEF(CD_SOLUCAO_DEF);
        }

        #endregion
    }
}
