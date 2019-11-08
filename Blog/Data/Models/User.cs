using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Morate uneti korisničko ime", AllowEmptyStrings = false)]
        [Display(Name="Korisnicko ime")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Morate uneti lozinku", AllowEmptyStrings = false)]
        [Display(Name="Lozinka")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Morate uneti potvrdu lozinke", AllowEmptyStrings = false)]
        [Compare("Password", ErrorMessage = "Lozinke se ne podudaraju")]
        [DataType(DataType.Password)]
        [Display(Name="Potvrdite lozinku")]
        public string ConfirmPassword { get; set; }

        public bool IsAdmin { get; set; }

        public string LoginErrorMessage { get; set; }
    }
}