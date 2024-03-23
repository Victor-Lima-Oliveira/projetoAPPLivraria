using System.ComponentModel;

namespace projetoAPPLivraria.Models
{
    public class Livro
    {
        [DisplayName ("Código")]
        public int codLivro { get; set; }

        [DisplayName ("Livro")]
        public String nomeLivro { get; set;}
        [DisplayName("Autor")]

        public Autor RefAutor { get; set; }

        public List<Autor> listaAutor { get; set;}
    }
}
