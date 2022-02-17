using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using UT = Cielo.Gtec.BLL.Utilities.GtecAspUtil;

namespace Cielo.Gtec.BLL.Model
{
    public class ST_CONTROLE
    {
        #region Propriedades
        public int IN_TRANSICAO;
        public int IN_REATIVADO;

        //public bool IN_CANCELAMENTO;
        public int NUM_RETENTATIVAS;

        public int IN_EDITAR;
        public int IN_CONSULTAR;
        public int IN_SALVAR_EXECUTAR;
        //public int IN_SALVAR_EXECUTAR2;

        public bool PermissaoParaEdicao;
        #endregion
    }
}
