using DesafioAPIConsultasVeterinarias.Interfaces;
using DesafioAPIConsultasVeterinarias.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace DesafioAPIConsultasVeterinarias.Repositories
{
    public class VeterinariosRepository : IVeterinariosRepository
    {

        readonly string connectionString = "Data Source = NB033786\\SQLEXPRESS2; Integrated Security = true; Initial Catalog = Consultas_Veterinarias";

        public bool Delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "DELETE FROM Veterinarios WHERE Id=@id";

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

        public ICollection<Veterinarios> GetAll()
        {
            var veterinarios = new List<Veterinarios>();

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string query = "SELECT * FROM Veterinarios";

                using (SqlCommand cmd = new SqlCommand(query, conexao))
                {
                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            veterinarios.Add(new Veterinarios
                            {
                                Id = (int)reader[0],
                                Nome = (string)reader[1],
                                Email = (string)reader[2],
                                Contato = (string)reader[3],
                                Imagem = (string)reader[4].ToString()
                            });
                        }
                    }
                }
            }

            return veterinarios;
        }

        public Veterinarios GetById(int id)
        {
            Veterinarios veterinario = null;

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string query = "SELECT * FROM Veterinarios WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(query, conexao))
                {
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            veterinario = new Veterinarios
                            {
                                Id = (int)reader[0],
                                Nome = (string)reader[1],
                                Email = (string)reader[2],
                                Contato = (string)reader[3],
                                Imagem = (string)reader[4].ToString()
                            };
                        }
                    }
                }
            }

            return veterinario;
        }

        public Veterinarios Insert(Veterinarios veterinario)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre uma conexão
                conexao.Open();

                string script = "INSERT INTO Veterinarios (Nome, Email, Contato, Imagem) VALUES (@Nome, @Email, @Contato, @Imagem)";

                // Criamos um comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetro
                    cmd.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar).Value = veterinario.Nome;
                    cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar).Value = veterinario.Email;
                    cmd.Parameters.Add("@Contato", System.Data.SqlDbType.NVarChar).Value = veterinario.Contato;
                    cmd.Parameters.Add("@Imagem", System.Data.SqlDbType.NVarChar).Value = veterinario.Imagem;

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return veterinario;
        }

        public Veterinarios Update(int id, Veterinarios veterinario)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "UPDATE Veterinarios SET Nome=@Nome, Email=@Email, Contato=@Contato, Imagem=@Imagem WHERE Id=@id";

                // Criamos um comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetro
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar).Value = veterinario.Nome;
                    cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar).Value = veterinario.Email;
                    cmd.Parameters.Add("@Contato", System.Data.SqlDbType.NVarChar).Value = veterinario.Contato;
                    cmd.Parameters.Add("@Imagem", System.Data.SqlDbType.NVarChar).Value = veterinario.Imagem;

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    veterinario.Id = id;
                }
            }

            return veterinario;
        }
    }
}
