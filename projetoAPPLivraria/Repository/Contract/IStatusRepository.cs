using projetoAPPLivraria.Models;

namespace projetoAPPLivraria.Repository.Contract
{
    public interface IStatusRepository
    {
        IEnumerable<Status> obterStatus();

        void cadastrar(Status status);

        void atualizar(Status status);
        void excluir(int codStatus);

    }
}
