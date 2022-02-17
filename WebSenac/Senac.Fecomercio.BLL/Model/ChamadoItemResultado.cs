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
    [Serializable]
    public class ChamadoItemResultado
    {
        #region Propriedades
        public string chamadoItem;
        public int chamado;
        public int seqChamado;

        public string chamadoEds;
        public int? logico;
        //public string protocoloAgendamento;

        public string contraOrdemNumero;
        public string contraOrdemDataHora;
        public bool processado;
        public bool resultado;
        public string resultadoMensagem;
        #endregion

        #region Construtor
        public ChamadoItemResultado()
        {
        }
        #endregion
    }
}
