using projetoAPPLivraria.Models;
using MySql.Data.MySqlClient;
using projetoAPPLivraria.Repository.Contract;
using System.Data;

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Livro obterLivro(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Livro> obterTodosOsLivros()
        {
            List<Livro> livros = new List<Livro>();
            using(var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tblivro as t1 INNER JOIN  tbautor as t2 on t1.codAutor = t2.codAutor", conexao);

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
                                status = Convert.ToInt32(dr["sta"])
                            },
                        });
               };
                return livros;
            }
        }
    }
}
