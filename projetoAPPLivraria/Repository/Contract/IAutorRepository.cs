using projetoAPPLivraria.Models;

namespace projetoAPPLivraria.Repository.Contract
{
    public interface IAutorRepository
    {


        IEnumerable<Autor> obterTodosOsAutores();
        void cadastrar (Autor autor);

        void atualizar(Autor autor);
        Autor obterAutor(int id);
        void excluir(int id);



    }
}
