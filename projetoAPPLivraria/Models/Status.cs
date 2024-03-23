using System.ComponentModel.DataAnnotations;

namespace projetoAPPLivraria.Models
{
    public class Status
    {
        [Display(Name ="Código")]
        public int codStatus {  get; set; }
        [Display(Name = "Status")]

        public String nomeStatus { get; set; }
    }
}
