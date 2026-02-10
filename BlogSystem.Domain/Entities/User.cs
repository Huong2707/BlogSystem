using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Domain.Entities
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }
        public byte IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateAt { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }

    }
}
