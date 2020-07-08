using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopie.Models
{
    public class LoginModel
    {
        [Key]
        [Required(ErrorMessage = "Enter your username")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Invalid password")]
        public string Password { set; get; }

    }
}