using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senac.Fecomercio.BLL.Utilities.Exceptions
{
    public class BusinessException : Exception
    {
        private int errorCode;

        public int ErrorCode { get { return errorCode; } }

        /// <summary>
        /// Construtor básico.
        /// </summary>
        public BusinessException()
        {

        }

        /// <summary>
        /// Construtor básico.
        /// </summary>
        public BusinessException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Construtor básico.
        /// </summary>
        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
        public BusinessException(string message, int errorCode)
            : base(message)
        {
            this.errorCode = errorCode;
        }
        public BusinessException(string message, int errorCode, Exception innerException)
            : base(message, innerException)
        {
            this.errorCode = errorCode;
        }

    }
}
