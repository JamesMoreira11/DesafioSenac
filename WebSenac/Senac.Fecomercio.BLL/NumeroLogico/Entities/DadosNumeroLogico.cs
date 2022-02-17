using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cielo.Gtec.BLL.Business.NumeroLogico.Entities
{
    public class DadosNumeroLogico
    {
        public int NumeroLogico { get; set; }
        public int NumeroLoja { get; set; }
        public string NumeroEstabelecimento { get; set; }
        public string CodigoModeloSolucao { get; set; }        
        public string CodigoModeloSolucaoDefinido { get; set; }
        public bool IndicadorLeitorCodigoBarras { get; set; }
        public string NumeroNAC { get; set; }
        public DadosNumeroLogicoMobile DadosSolucaoMobile { get; set; }
    }
}
