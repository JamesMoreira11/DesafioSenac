using System;

namespace Senac.Fecomercio.Entities.Domain
{
    /// <summary>
    /// Representa uma Ação realizada para recuperar parametro do GTeC - PR_GT_GTEC_PARAMETROS
    /// </summary>
    public class LoginSenac
    {
        /// <summary>
        /// Datafield: NM_USUARIO_NT
        /// nome do usuário
        /// </summary>
        public string NM_USUARIO_NT { get; set; }

        /// <summary>
        /// Datafield: NM_DOMINIO_NT
        /// Nome do domínio
        /// </summary>
        public string NM_DOMINIO_NT { get; set; }

        /// <summary>
        /// Datafield: CD_PERFIL
        /// Código do perfil
        /// </summary>
        public int CD_PERFIL { get; set; }

        /// <summary>
        /// Datafield: NU_ID
        /// Número do ID
        /// </summary>
        public int NU_ID { get; set; }

        /// <summary>
        /// Datafield: NU_TIMESTAMP
        /// timestamp
        /// </summary>
        public string NU_TIMESTAMP { get; set; }

        /// <summary>
        /// Datafield: IN_ATIVO
        /// usuário Ativo/Inativo
        /// </summary>
        public bool IN_ATIVO { get; set; }

       
    }
}
