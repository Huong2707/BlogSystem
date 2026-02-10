using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.DTOs
{
    public class LoginRequest
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid Email Format")]
        public string Email { get; set; }



        [Required(ErrorMessage ="Password is required")]
       
        public string Password { get; set; }


       


    }
}
