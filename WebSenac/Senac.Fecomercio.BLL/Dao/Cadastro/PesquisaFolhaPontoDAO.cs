using Senac.Fecomercio.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP = CieloGtecDAL.DAL.BDPadrao;
using UT = Senac.Fecomercio.BLL.Utilities.SenacAspUtil;

namespace Senac.Fecomercio.BLL.Dao.Cadastro
{
    public class PesquisaFolhaPontoDAO
    {
        #region Folha de Ponto dos Colaboradores - Carrega Lista
        public static DataTable ListaFolhaPonto(int codColaborador)
        {
            SqlParameter[] parametros =
            {
            new SqlParameter("@CD_GUID", codColaborador),
            new SqlParameter("@FL_TIPO", "C")
            };

            DataTable lista = BP.ExecuteDataTable("PR_ATUALIZA_REGISTRO_PONTO_001", CommandType.StoredProcedure, ref parametros);

            return lista;
        }

        #endregion

        #region Paginação - Parâmetros do Relatorio 
        public static int ParmRelatorioPaginacao(string parametro)
        {
            int result = 0;
            int qtdeDias = 31;
            int qtdePaginacao = 20;

            if (parametro == "DIAS")
            {
                result = qtdeDias;
            }

            if (parametro == "PAGINACAO")
            {
                result = qtdePaginacao;
            }

            return result;
        }

        #endregion
    }
}
