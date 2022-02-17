using System;

namespace Senac.Fecomercio.Exceptions
{
    public class ItemNaoEncontradoException : Exception
    {
        public ItemNaoEncontradoException(string message)
            : base(message)
        {

        }
    }
}