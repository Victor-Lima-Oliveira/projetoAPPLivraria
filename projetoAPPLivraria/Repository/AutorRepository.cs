﻿using Microsoft.AspNetCore.Identity;
using MySql.Data.MySqlClient;
using projetoAPPLivraria.Models;
using projetoAPPLivraria.Repository.Contract;
using System.Data;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;

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
            using (var conexao = new MySqlConnection(_Conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE tbautor SET" +
                    " nomeAutor = @nomeAutor," +
                    " sta = @sta" +
                    " where codAutor = @codAutor;", conexao);

                cmd.Parameters.Add("@nomeAutor", MySqlDbType.VarChar).Value = autor.nomeAutor;
                cmd.Parameters.Add("@sta", MySqlDbType.Int64).Value = autor.Refstatus.codStatus;
                cmd.Parameters.Add("@codAutor", MySqlDbType.Int64).Value = autor.id;
                cmd.ExecuteNonQuery();
                conexao.Close();

            }
        }

        public void cadastrar(Autor autor)
        {
            using (var conexao = new MySqlConnection(_Conexao))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into tbAutor ( nomeAutor, sta) values(@nomeAutor, @sta);", conexao);

                cmd.Parameters.Add("@nomeAutor", MySqlDbType.VarChar).Value = autor.nomeAutor;
                cmd.Parameters.Add("@sta", MySqlDbType.VarChar).Value = autor.Refstatus.codStatus;

                cmd.ExecuteNonQuery();
                conexao.Close();

            }
        }

        public String excluir(int id)
        {
            using (var conexao =  new MySqlConnection(_Conexao))
            {
                conexao.Open();
                try
                {
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM tbautor WHERE codAutor = @codAutor", conexao);

                    cmd.Parameters.Add("@codAutor", MySqlDbType.Int64).Value = id;
                    cmd.ExecuteNonQuery();
                    return null;
                }catch(Exception ex)
                {
                    String menssage = ex.Message;
                    if (menssage == "Cannot delete or update a parent row: a foreign key constraint fails (`dblivraria`.`tblivro`, CONSTRAINT `tblivro_ibfk_1` FOREIGN KEY (`codautor`) REFERENCES `tbautor` (`codAutor`))")
                        return "Esse autor possui livros cadastrados, não é possível deletar";
                    else
                        return "Erro: Chame um técnico";

                }
                finally { conexao.Close(); }
            }
        }

        public Autor obterAutor(int id)
        {
            using(var conexao = new MySqlConnection(_Conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT t1.codAutor, t1.nomeAutor, t2.codStatus, t2.sta " +
                    "FROM tbautor t1 " +
                    "INNER JOIN tbstatus t2 on t1.sta = t2.codStatus " +
                    "where t1.codAutor = @codAutor ;", conexao);

               cmd.Parameters.Add("@CodAutor", MySqlDbType.UInt64).Value = id;

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                MySqlDataReader dataReader;

                Autor autor = new Autor();
                dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while(dataReader.Read())
                {
                    autor.id = (int)dataReader["codAutor"];
                    autor.nomeAutor = (string)dataReader["nomeAutor"];
                    autor.Refstatus = new Status()
                    {
                        codStatus = (int)dataReader["codStatus"],
                        nomeStatus = (string)dataReader["sta"]
                    };
                }
                return autor;
            } // end using
        } // end obterAutor()

        public IEnumerable<Autor> obterTodosOsAutores()
        {
            List<Autor> autors = new List<Autor>();
            using (var conexao = new MySqlConnection(_Conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT t1.codAutor, t1.nomeAutor, t2.codStatus, t2.sta " +
                    "FROM tbautor t1 " +
                    "INNER JOIN tbstatus t2 on t1.sta = t2.codStatus;", conexao);

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
                            Refstatus = new Status()
                            {
                                codStatus = (int)dr["codStatus"],
                                nomeStatus = (String)dr["sta"]
                            }
                        });
                }
                return autors;
            }             
        } // end obterTodosOsAutores()

    }// end autorRespository
}
