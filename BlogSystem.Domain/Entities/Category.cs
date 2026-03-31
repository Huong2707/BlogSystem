using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Domain.Entities
{
    public class Category : BaseEntity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
       public string CateSlug { get; set; }
        public string? CategoryDescription { get; set; }
        public byte IsActive { get; set; } = 1;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateAt { get; set; }
    }
}
