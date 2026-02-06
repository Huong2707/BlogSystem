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
        [Required(ErrorMessage ="FullName is required")]
        [StringLength(100,ErrorMessage ="Full name must be less than 100 charaters")]
        public string FullName { get; set; }



        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }


        public string Password { get; set; }
        public string Bio { get; internal set; }
        public string ConfirmPassword { get; internal set; }
    }
}
