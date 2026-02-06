using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.Exceptions
{
    public class ValidationException: Exception
    {
        public List<string> Errors { get; }
        public ValidationException(List<string> errors) : base("Validation Failed")
        {
            Errors = errors;
        }
        public ValidationException(string error) : base("Validation Failed")
        {
            Errors = new List<string> { error };
        }
     
    }
}



