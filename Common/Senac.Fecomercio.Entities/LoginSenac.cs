using System;

namespace Senac.Fecomercio.Entities.Domain
{
    /// <summary>
    /// Representa uma A��o realizada para recuperar parametro do GTeC - PR_GT_GTEC_PARAMETROS
    /// </summary>
    public class LoginSenac
    {
        /// <summary>
        /// Datafield: NM_USUARIO_NT
        /// nome do usu�rio
        /// </summary>
        public string NM_USUARIO_NT { get; set; }

        /// <summary>
        /// Datafield: NM_DOMINIO_NT
        /// Nome do dom�nio
        /// </summary>
        public string NM_DOMINIO_NT { get; set; }

        /// <summary>
        /// Datafield: CD_PERFIL
        /// C�digo do perfil
        /// </summary>
        public int CD_PERFIL { get; set; }

        /// <summary>
        /// Datafield: NU_ID
        /// N�mero do ID
        /// </summary>
        public int NU_ID { get; set; }

        /// <summary>
        /// Datafield: NU_TIMESTAMP
        /// timestamp
        /// </summary>
        public string NU_TIMESTAMP { get; set; }

        /// <summary>
        /// Datafield: IN_ATIVO
        /// usu�rio Ativo/Inativo
        /// </summary>
        public bool IN_ATIVO { get; set; }

       
    }
}
