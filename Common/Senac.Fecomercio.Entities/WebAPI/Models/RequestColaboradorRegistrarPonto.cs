using Senac.Fecomercio.Entities.WebAPI.Models.Base;

namespace Senac.Fecomercio.Entities.WebAPI.Models
{
    public class RequestColaboradorRegistrarPonto
    {
        public string colaboradorID { get; set; }
        public string nomeColaborador { get; set; }
        public string dataRegistro { get; set; }
        public string indicadorRegistro { get; set; }
    }
}
