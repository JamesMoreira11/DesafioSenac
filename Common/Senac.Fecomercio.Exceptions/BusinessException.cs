using System;

namespace Senac.Fecomercio.Exceptions
{
    public class BusinessException: Exception
    {
        public BusinessException(string message)
            :base(message)
        {
           
        }
    }
}