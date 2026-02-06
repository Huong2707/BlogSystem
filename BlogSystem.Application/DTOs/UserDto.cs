using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<string> Roles { get; set; } = new List<string>();

    }
}
