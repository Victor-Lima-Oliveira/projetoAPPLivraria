using System.ComponentModel;

namespace projetoAPPLivraria.Models
{
    public class Autor
    {
        public int id { get; set; }
        [DisplayName("Nome do autor")]
        public String nomeAutor { get; set; }
        [DisplayName("Status do autor")]
        public int status { get; set; }
    }
}
