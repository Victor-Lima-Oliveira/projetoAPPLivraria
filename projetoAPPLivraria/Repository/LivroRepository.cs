using projetoAPPLivraria.Models;
using MySql.Data.MySqlClient;
using projetoAPPLivraria.Repository.Contract;
using System.Data;
using System.Net.NetworkInformation;

namespace projetoAPPLivraria.Repository
{
    public class LivroRepository : ILivroRepository
    {
        private readonly string _conexaoMySql;

        public LivroRepository(IConfiguration config)
        {
            _conexaoMySql = config.GetConnectionString("Conexao");
        }

        public void atualizar(Livro livro)
        {
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE tblivro SET nomelivro=@nomelivro, codAutor=@codAutor" +
                    " where codLivro = @codlivro", conexao);

                cmd.Parameters.Add("@codLivro", MySqlDbType.VarChar).Value = livro.codLivro;
                cmd.Parameters.Add("@nomeLivro", MySqlDbType.VarChar).Value = livro.nomeLivro;
                cmd.Parameters.Add("@codAutor", MySqlDbType.VarChar).Value = livro.RefAutor.id; 

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
            
        }   

        public void cadastrar(Livro livro)
        {
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO tblivro (nomelivro, codautor) VALUES (@nomeLivro, @codAutor)", conexao);

                cmd.Parameters.Add("@nomeLivro", MySqlDbType.VarChar).Value = livro.nomeLivro;
                cmd.Parameters.Add("@codAutor", MySqlDbType.VarChar).Value = livro.RefAutor.id;

                cmd.ExecuteNonQuery();
                conexao.Close();
            } 
        }

        public void excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySql)) 
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM tblivro where codlivro = @codlivro", conexao);
                cmd.Parameters.AddWithValue("@codLivro", id);
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Livro obterLivro(int id)
        {
            using(var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT  t1.codLivro,  t1.nomeLivro,  t2.codAutor,  t2.nomeAutor,  t3.sta, t3.codStatus " + 
                    "FROM tblivro t1 INNER JOIN  tbautor t2 on  t1.codAutor = t2.codAutor " +   
                    "INNER JOIN tbstatus t3 on t3.codStatus = t2.sta "+
                    "where t1.codLivro = @codLivro; ", conexao);

                cmd.Parameters.AddWithValue("@codLivro", id);

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                MySqlDataReader dataReader;

                Livro livro = new Livro();
                dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while(dataReader.Read()) 
                {
                    livro.codLivro = Convert.ToInt32(dataReader["codLivro"]);
                    livro.nomeLivro = (string)(dataReader["nomelivro"]);
                    livro.RefAutor = new Autor()
                    {
                        id = Convert.ToInt32(dataReader["codautor"]),
                        nomeAutor = (string)(dataReader["nomeAutor"]),
                        Refstatus = new Status() {
                            codStatus = (int) dataReader["codStatus"],
                            nomeStatus = (string) dataReader["sta"]
                        }
                    };
                }
                return livro;
            }
        }

        public IEnumerable<Livro> obterTodosOsLivros()
        {
            List<Livro> livros = new List<Livro>();
            using(var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT  t1.codLivro,  t1.nomeLivro,  t2.codAutor,  t2.nomeAutor,  t3.sta, t3.codStatus " +
                    "FROM tblivro t1 INNER JOIN  tbautor t2 on  t1.codAutor = t2.codAutor " +
                    "INNER JOIN tbstatus t3 on t3.codStatus = t2.sta ", conexao);

                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                mySqlDataAdapter.Fill(dataTable);

                conexao.Close();

                foreach (DataRow dr in dataTable.Rows)
                {
                    livros.Add(
                        new Livro
                        {
                            codLivro = Convert.ToInt32(dr["codlivro"]),
                            nomeLivro = (string)dr["nomelivro"],
                            RefAutor = new Autor()
                            {
                                id = Convert.ToInt32(dr["codAutor"]),
                                nomeAutor = (string)(dr["nomeAutor"]),
                                Refstatus = new Status()
                                {
                                    codStatus = (int)dr["codStatus"],
                                    nomeStatus = (String) dr["sta"]
                                }
                            },
                        }); ;
               };
                return livros;
            }
        }
    }
}
