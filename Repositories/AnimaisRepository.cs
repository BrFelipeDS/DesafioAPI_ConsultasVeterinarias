using DesafioAPIConsultasVeterinarias.Interfaces;
using DesafioAPIConsultasVeterinarias.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace DesafioAPIConsultasVeterinarias.Repositories
{
    public class AnimaisRepository : IAnimaisRepository
    {

        readonly string connectionString = "Data Source = NB033786\\SQLEXPRESS2; Integrated Security = true; Initial Catalog = Consultas_Veterinarias";

        public bool Delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "DELETE FROM Animais WHERE Id=@id";

                // Criamos um comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetro
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                    cmd.CommandType = CommandType.Text;
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    if (linhasAfetadas == 0)
                    {
                        return false;
                    }

                }
            }
            return true;
        }

        public ICollection<Animais> GetAll()
        {
            var animais = new List<Animais>();

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string consulta = "SELECT * FROM Animais";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            animais.Add(new Animais
                            {
                                Id = (int)reader[0],
                                Nome = (string)reader[1],
                                Raca = (string)reader[2],
                                NomeDono = (string)reader[3],
                                ContatoDono = (string)reader[4],
                                Imagem = (string)reader[5].ToString()
                            });
                        }
                    }
                }
            }

            return animais;
        }

        public Animais GetById(int id)
        {
            Animais animal = null;

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string consulta = "SELECT * FROM Animais WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            animal = new Animais
                            {
                                Id = (int)reader[0],
                                Nome = (string)reader[1],
                                Raca = (string)reader[2],
                                NomeDono = (string)reader[3],
                                ContatoDono = (string)reader[4],
                                Imagem = (string)reader[5].ToString()
                            };                            

                        }
                    }
                }
            }

            return animal;
        }

        public Animais Insert(Animais animal)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre uma conexão
                conexao.Open();

                string script = "INSERT INTO Animais (Nome, Raca, NomeDono, ContatoDono, Imagem) VALUES (@Nome, @Raca, @NomeDono, @ContatoDono, @Imagem)";

                // Criamos um comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetro
                    cmd.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar).Value = animal.Nome;
                    cmd.Parameters.Add("@Raca", System.Data.SqlDbType.NVarChar).Value = animal.Raca;
                    cmd.Parameters.Add("@NomeDono", System.Data.SqlDbType.NVarChar).Value =animal.NomeDono;
                    cmd.Parameters.Add("@ContatoDono", System.Data.SqlDbType.NVarChar).Value = animal.ContatoDono;
                    cmd.Parameters.Add("@Imagem", System.Data.SqlDbType.NVarChar).Value = animal.Imagem;

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return animal;
        }

        public Animais Update(int id, Animais animal)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "UPDATE Animais SET Nome=@Nome, Raca=@Raca, NomeDono=@NomeDono, ContatoDono=@ContatoDono, Imagem=@Imagem WHERE Id=@id";

                // Criamos um comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetro
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar).Value = animal.Nome;
                    cmd.Parameters.Add("@Raca", System.Data.SqlDbType.NVarChar).Value = animal.Raca;
                    cmd.Parameters.Add("@NomeDono", System.Data.SqlDbType.NVarChar).Value = animal.NomeDono;
                    cmd.Parameters.Add("@ContatoDono", System.Data.SqlDbType.NVarChar).Value = animal.ContatoDono;
                    cmd.Parameters.Add("@Imagem", System.Data.SqlDbType.NVarChar).Value = animal.Imagem;

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    animal.Id = id;
                }
            }

            return animal;
        }
    }
}
