using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cielo.Gtec.BLL.Model
{
    [Serializable]
    public class ListaGtec
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string CodigoDescricao
        {
            //get { return string.Format("{0:D3} - {1}", Codigo, Descricao); }
            get { return string.Format("{0} - {1}", Codigo, Descricao); }
        }
    }
}
