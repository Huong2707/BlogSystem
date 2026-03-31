using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.Helpers
{
    public static class SlugHelper
    {
        public static string GenerateSlug(string text)
        {
            return text.Trim().ToLower()
                .Replace(" ", "-")
                .Replace(".", "");

        }
    }
}
