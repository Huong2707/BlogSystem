using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Domain.Entities
{
    public class Role : BaseEntity
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string? DescriptionRole { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }

    }
}
