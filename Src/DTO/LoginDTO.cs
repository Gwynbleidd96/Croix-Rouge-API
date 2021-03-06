using System;
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO
{
    public class LoginDTO
    {
        [StringLength(50, MinimumLength=3)]
        [Required]
        public string UserName {get; set;}
        [StringLength(20, MinimumLength=3)]
        [Required]
        public string Password {get; set;}

        public LoginDTO ()
        {

        }
    }
}