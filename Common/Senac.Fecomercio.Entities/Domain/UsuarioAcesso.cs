using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senac.Fecomercio.Entities.Domain
{
    public class UsuarioAcesso
    {
        public string NomeUsuario { get; set; }
        public string DominioUsuario { get; set; }
        public int CodigoPerfil { get; set; }
        public bool Ativo { get; set; }
    }
}
