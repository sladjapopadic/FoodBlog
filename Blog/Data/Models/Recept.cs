using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Models
{
    public class Recept
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Morate uneti naziv", AllowEmptyStrings = false)]
        [MaxLength(100)]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Morate uneti pripremu", AllowEmptyStrings = false)]
        public string Priprema { get; set; }

        [Display(Name = "Vreme pripreme")]
        public string VremePripreme { get; set; }

        [Required(ErrorMessage = "Morate uneti broj porcija", AllowEmptyStrings = false)]
        [Range(1, 50)]
        [Display(Name = "Broj porcija")]
        public int BrojPorcija { get; set; }
        
        [MaxLength(50)]
        public string Autor { get; set; }

        [Required(ErrorMessage = "Morate uneti sastojke", AllowEmptyStrings = false)]
        public string Sastojci { get; set; }

        [Required(ErrorMessage = "Morate uneti vrstu recepta", AllowEmptyStrings = false)]
        [Display(Name = "Vrsta recepta")]
        public string VrstaRecepta { get; set; }      

    }
}
