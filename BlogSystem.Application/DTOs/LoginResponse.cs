using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.DTOs
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public UserDto User { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
