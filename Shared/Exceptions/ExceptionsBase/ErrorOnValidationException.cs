using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : BaseException
    {
        public IList<string> ErrorMessages { get; set; }

        public ErrorOnValidationException(IList<string> erros) : base(string.Empty)
        {
            ErrorMessages = erros;
        }
    }
}
