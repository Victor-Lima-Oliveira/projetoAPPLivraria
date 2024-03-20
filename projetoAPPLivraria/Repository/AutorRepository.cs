using MySql.Data.MySqlClient;
using projetoAPPLivraria.Models;
using projetoAPPLivraria.Repository.Contract;
using System.Data;

namespace projetoAPPLivraria.Repository
{
    public class AutorRepository : IAutorRepository
    {
        private readonly string _Conexao;

        public AutorRepository(IConfiguration configuration) {
            _Conexao = configuration.GetConnectionString("Conexao");
        }


        public void atualizar(Autor autor)
        {
            throw new NotImplementedException();
        }

        public void cadastrar(Autor autor)
        {
            using (var conexao = new MySqlConnection(_Conexao))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into tbAutor ( nomeAutor, sta) values(@nomeAutor, @sta);", conexao);

                cmd.Parameters.Add("@nomeAutor", MySqlDbType.VarChar).Value = autor.nomeAutor;
                cmd.Parameters.Add("@sta", MySqlDbType.VarChar).Value = autor.status;

                cmd.ExecuteNonQuery();
                conexao.Close();

            }
        }

        public void excluir(int id)
        {
            throw new NotImplementedException();
        }

        public Autor obterAutor(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Autor> obterTodosOsAutores()
        {
            List<Autor> autors = new List<Autor>();
            using (var conexao = new MySqlConnection(_Conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbautor", conexao);

                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd); 

                DataTable dataTable = new DataTable();

                mySqlDataAdapter.Fill(dataTable);

                conexao.Close() ;

                foreach(DataRow dr in dataTable.Rows)
                {
                    autors.Add(
                        new Autor()
                        {
                            id = Convert.ToInt32(dr["codAutor"]),
                            nomeAutor = (string)(dr["nomeAutor"]),
                            status = Convert.ToInt32(dr["sta"])
                        });
                }
                return autors;
            }
            
        }
    }
}
