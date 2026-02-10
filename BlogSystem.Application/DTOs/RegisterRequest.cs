using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.DTOs
{
    public  class RegisterRequest
    {
        [Required(ErrorMessage = "FullName is required")]
        [StringLength(100, ErrorMessage = "Full name must be less than 100 charaters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 charaters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
            ErrorMessage = "Password not strength")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }

        [StringLength(255, ErrorMessage = "Bio must be at least 255 charaters")]
        public string? Bio { get; set; }

    }
}
