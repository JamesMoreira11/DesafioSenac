using System;

namespace Senac.Fecomercio.Exceptions.Servico
{
    public class ErroComunicacaoException : Exception
    {
        private string Code { get; set; }
        private string Mensagem { get; set; }

        public ErroComunicacaoException(string code, string mensagem) : base(String.Format("Code: '{0}' - Mensagem: '{1}'", code, mensagem))
        {
            this.Code = code;
            this.Mensagem = mensagem;
        }
    }
}
