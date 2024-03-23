using System.ComponentModel;

namespace projetoAPPLivraria.Models
{
    public class Autor
    {
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("Nome do autor")]
        public String nomeAutor { get; set; }
        [DisplayName("Status do autor")]
        public Status Refstatus { get; set; }
        public List<Status> listaStatus { get; set; }   
    }
}
