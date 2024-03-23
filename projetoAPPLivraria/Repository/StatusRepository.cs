using Microsoft.AspNetCore.Identity;
using MySql.Data.MySqlClient;
using NuGet.Protocol.Plugins;
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

        public void atualizar(Status status)
        {
            using (var conexao = new MySqlConnection(_Conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE tbstatus SET sta = @nomeStatus WHERE codStatus = @codStatus;", conexao);

                cmd.Parameters.Add("@nomeStatus", MySqlDbType.VarChar).Value = status.nomeStatus;
                cmd.Parameters.Add("@codStatus", MySqlDbType.Int64).Value = status.codStatus;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        } // end Atualizar

        public void cadastrar(Status status)
        {
            using (var conexao = new MySqlConnection(_Conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO  tbstatus(sta) VALUES (@nomeStatus);", conexao);

                cmd.Parameters.Add("@nomeStatus", MySqlDbType.VarChar).Value = status.nomeStatus;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        } // end Cadastrar

        public String excluir(int codStatus)
        {
            
            using (var conexao = new MySqlConnection(_Conexao))
            {
                try
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM tbstatus WHERE codstatus = @codStatus", conexao);

                    cmd.Parameters.AddWithValue("@codStatus", codStatus);
                    cmd.ExecuteNonQuery();
                    return null;
                }catch(Exception ex)
                {

                    // Erro de apagar uma foreign key
                    if (ex.Message.Length > 5)
                        return "Esse status está sendo utilizado, não foi possível deletar!";
                    else
                        return "Erro: Chame um técnico";
                }
                finally { conexao.Close(); }
            }
        } // end Excluir

        public Status obterStatus(int id)
        {
            using (var conexao = new MySqlConnection(_Conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand(" SELECT * FROM tbstatus where codStatus = @codStatus;", conexao);

                cmd.Parameters.Add("@codStatus", MySqlDbType.Int64).Value = id;             

                MySqlDataReader dataReader;
                Status status = new Status();

                dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                

                while (dataReader.Read())
                {
                    status.codStatus = (int)dataReader["codStatus"];
                    status.nomeStatus = (string)dataReader["sta"];
                }
                conexao.Close();

                return status;
            }// end obterStatus()
        }

        public IEnumerable<Status> obterTodosStatus()
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
