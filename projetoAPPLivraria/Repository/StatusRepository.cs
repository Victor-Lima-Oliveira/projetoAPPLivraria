using MySql.Data.MySqlClient;
using projetoAPPLivraria.Models;
using projetoAPPLivraria.Repository.Contract;
using System.Data;

namespace projetoAPPLivraria.Repository
{
    public class StatusRepository : IStatusRepository
    {

        private readonly string _Conexao;

        public StatusRepository(IConfiguration configuration)
        {
            _Conexao = configuration.GetConnectionString("Conexao");
        }

        public IEnumerable<Status> obterStatus()
        {
            List<Status> status = new List<Status>();
            using (var conexao = new MySqlConnection(_Conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbstatus", conexao);

                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                mySqlDataAdapter.Fill(dataTable);

                conexao.Close();

                foreach (DataRow dr in dataTable.Rows)
                {
                    status.Add(
                        new Status()
                        {
                            codStatus = (int)dr["codStatus"],
                            nomeStatus = (string)dr["sta"]
                        }
                    );
                }
                return status;
            }
        } // end obterStatus()
    }
}
