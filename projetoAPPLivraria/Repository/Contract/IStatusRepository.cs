using projetoAPPLivraria.Models;

namespace projetoAPPLivraria.Repository.Contract
{
    public interface IStatusRepository
    {
        

        void cadastrar(Status status);

        void atualizar(Status status);
        String excluir(int codStatus);
        Status obterStatus(int id);
        IEnumerable<Status> obterTodosStatus();

    }
}
