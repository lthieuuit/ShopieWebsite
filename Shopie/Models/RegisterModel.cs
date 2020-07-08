using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopie.Models
{
    public class RegisterModel
    {     
        [Key]
        public long ID { set; get; }

        [Required(ErrorMessage = "Invalid username")]
        [StringLength(32, MinimumLength=6, ErrorMessage = "Username must be between 6 and 32 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Invalid password")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 32 characters")]
        public string Password { get; set; }

        [StringLength(32)]
        [Compare("Password", ErrorMessage = "Password not match")]
        public string ConfirmPassword { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Enter your full name ")]
        public string Name { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Enter your email")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Enter your phone number")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }
   
    }
}