using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.DTOs.Categories
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategorySlug { get; set; }
        public string? Description { get; set; }

        public byte IsActive { get; set; } = 1;
        public DateTime? CreatedAt { get; set; }
    }
}
