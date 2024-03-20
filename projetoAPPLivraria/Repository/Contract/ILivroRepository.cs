using projetoAPPLivraria.Models;

namespace projetoAPPLivraria.Repository.Contract
{
    public interface ILivroRepository
    {
        IEnumerable<Livro> obterTodosOsLivros();

        void cadastrar(Livro livro);

        void atualizar(Livro livro);

        Livro obterLivro(int id);

        void excluir(int id);

    }
}
