using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.DTOs
{
    public class ValidateTokenResponse
    {
        public bool IsValid { get; set; }
        public UserDto User { get; set; }

    }
}
