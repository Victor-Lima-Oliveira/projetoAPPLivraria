using System.ComponentModel;

namespace projetoAPPLivraria.Models
{
    public class Livro
    {
        [DisplayName ("Código")]
        public int codLivro { get; set; }

        [DisplayName ("Livro")]
        public String nomeLivro { get; set;}

        public Autor RefAutor { get; set; }

        [DisplayName("Autor")]
        public List<Autor> listaAutor { get; set;}
    }
}
