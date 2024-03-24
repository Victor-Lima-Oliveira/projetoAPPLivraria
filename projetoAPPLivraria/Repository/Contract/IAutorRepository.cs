using projetoAPPLivraria.Models;

namespace projetoAPPLivraria.Repository.Contract
{
    public interface IAutorRepository
    {


       
        void cadastrar (Autor autor);

        void atualizar(Autor autor);
        
        String excluir(int id);
        Autor obterAutor(int id);
        IEnumerable<Autor> obterTodosOsAutores();



    }
}
