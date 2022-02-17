using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using UT = Cielo.Gtec.BLL.Utilities.GtecAspUtil;

namespace Cielo.Gtec.BLL.Model
{
    // retorno da procedure PR_GT_P20_CHAMADO_PESQUISA_CHECK
    public class ModelPesquisaCheck
    {
        #region Propriedades
        public int NU_CHAMADO;
        public int NU_SEQCHAMADO;
        public string CD_CHAMADO_STATUS;	// CD_CHAMADO_STATUS	varchar(3)
        public int CD_SERVICO_TIPO;   // O.CD_SERVICO_TIPO
        public string CD_MOD_SOL_DESIGNADO; // CD_MOD_SOL_DESIGNADO	varchar(2)
        public string CD_AT_DESIGNADO; // CD_AT_DESIGNADO varchar(2)
        public string CD_AT_FILIAL_DESIGNA; // CD_AT_FILIAL_DESIGNA	varchar(4)
        public int? CD_ATSERVCONTR_DESIG; // CD_ATSERVCONTR_DESIG	int
        public DateTime? DH_ENVIO;
        public DateTime? DH_BAIXA;
        public DateTime? DH_AGENDAMENTO;
        public DateTime? DH_BXLOGISTICA;

        // Campos inseridos
        public int? CD_OCORRENCIA;
        public string CD_SERVICO_EXECUTADO; // varchar(3)
        public string NM_ARQUIVO_ENVIADO; // varchar(30)
        public int? CD_ATENDTEMPO;
        public bool IN_EQUIPAMENTO_NOVO;
        public bool IN_GERA_NU_LOGICO;
        #endregion

        #region Construtor
        public ModelPesquisaCheck(DataRow dr)
        {
            NU_CHAMADO = (int)dr["NU_CHAMADO"];
            NU_SEQCHAMADO = (int)dr["NU_SEQCHAMADO"];
            CD_SERVICO_TIPO = (int)dr["CD_SERVICO_TIPO"];

            // Nullable
            CD_CHAMADO_STATUS = dr["CD_CHAMADO_STATUS"] == DBNull.Value ? "" : (string)dr["CD_CHAMADO_STATUS"];
            CD_MOD_SOL_DESIGNADO = dr["CD_MOD_SOL_DESIGNADO"] == DBNull.Value ? "" : (string)dr["CD_MOD_SOL_DESIGNADO"];
            CD_AT_DESIGNADO = dr["CD_AT_DESIGNADO"] == DBNull.Value ? "" : (string)dr["CD_AT_DESIGNADO"];
            CD_AT_FILIAL_DESIGNA = dr["CD_AT_FILIAL_DESIGNA"] == DBNull.Value ? "" : (string)dr["CD_AT_FILIAL_DESIGNA"];
            CD_ATSERVCONTR_DESIG = dr["CD_ATSERVCONTR_DESIG"] == DBNull.Value ? null : (int?)dr["CD_ATSERVCONTR_DESIG"];
            DH_ENVIO = dr["DH_ENVIO"] == DBNull.Value ? null : (DateTime?)dr["DH_ENVIO"];
            DH_BAIXA = dr["DH_BAIXA"] == DBNull.Value ? null : (DateTime?)dr["DH_BAIXA"];
            DH_AGENDAMENTO = dr["DH_AGENDAMENTO"] == DBNull.Value ? null : (DateTime?)dr["DH_AGENDAMENTO"];
            DH_BXLOGISTICA = dr["DH_BXLOGISTICA"] == DBNull.Value ? null : (DateTime?)dr["DH_BXLOGISTICA"];

            CD_OCORRENCIA = dr["CD_OCORRENCIA"] == DBNull.Value ? null : (int?)dr["CD_OCORRENCIA"];
            CD_SERVICO_EXECUTADO = dr["CD_SERVICO_EXECUTADO"] == DBNull.Value ? "" : (string)dr["CD_SERVICO_EXECUTADO"];
            NM_ARQUIVO_ENVIADO = dr["NM_ARQUIVO_ENVIADO"] == DBNull.Value ? "" : (string)dr["NM_ARQUIVO_ENVIADO"];
            CD_ATENDTEMPO = dr["CD_ATENDTEMPO"] == DBNull.Value ? null : (int?)dr["CD_ATENDTEMPO"];
            IN_EQUIPAMENTO_NOVO = dr["IN_EQUIPAMENTO_NOVO"] == DBNull.Value ? false : (bool)dr["IN_EQUIPAMENTO_NOVO"];
            IN_GERA_NU_LOGICO = dr["IN_GERA_NU_LOGICO"] == DBNull.Value ? false : (bool)dr["IN_GERA_NU_LOGICO"];
        }

        #endregion
    }
}
