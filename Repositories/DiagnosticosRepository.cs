using DesafioAPIConsultasVeterinarias.Interfaces;
using DesafioAPIConsultasVeterinarias.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace DesafioAPIConsultasVeterinarias.Repositories
{
    public class DiagnosticosRepository : IDiagnosticosRepository
    {

        readonly string connectionString = "Data Source = NB033786\\SQLEXPRESS2; Integrated Security = true; Initial Catalog = Consultas_Veterinarias";

        public bool Delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "DELETE FROM Diagnosticos WHERE Id=@id";

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

        public ICollection<Diagnosticos> GetAll()
        {
            var diagnosticos = new List<Diagnosticos>();

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string query = "SELECT * FROM Diagnosticos";

                using (SqlCommand cmd = new SqlCommand(query, conexao))
                {
                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            diagnosticos.Add(new Diagnosticos
                            {
                                Id = (int)reader[0],
                                Diagnostico = (string)reader[1],
                                ConsultaId = (int)reader[2]                                
                            });
                        }
                    }
                }
            }

            return diagnosticos;
        }

        public Diagnosticos GetById(int id)
        {
            Diagnosticos diagnostico = null;

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string query = "SELECT * FROM Diagnosticos WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(query, conexao))
                {
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            diagnostico = new Diagnosticos
                            {
                                Id = (int)reader[0],
                                Diagnostico = (string)reader[1],
                                ConsultaId = (int)reader[2]
                            };
                            
                        }
                    }
                }
            }

            return diagnostico;
        }

        public Diagnosticos Insert(Diagnosticos diagnostico)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre uma conexão
                conexao.Open();

                string script = "INSERT INTO Diagnosticos (Diagnostico, ConsultaId) VALUES (@Diagnostico, @ConsultaId)";

                // Criamos um comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetro
                    cmd.Parameters.Add("@Diagnostico", System.Data.SqlDbType.NVarChar).Value = diagnostico.Diagnostico;
                    cmd.Parameters.Add("@ConsultaId", System.Data.SqlDbType.Int).Value = diagnostico.ConsultaId;
                   
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return diagnostico;
        }

        public Diagnosticos Update(int id, Diagnosticos diagnostico)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "UPDATE Diagnosticos SET Diagnostico=@Diagnostico, ConsultaId=@ConsultaId WHERE Id=@id";

                // Criamos um comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetro
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@Diagnostico", System.Data.SqlDbType.NVarChar).Value = diagnostico.Diagnostico;
                    cmd.Parameters.Add("@ConsultaId", System.Data.SqlDbType.Int).Value = diagnostico.ConsultaId;
                    
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    diagnostico.Id = id;
                }
            }

            return diagnostico;
        }
    }
}
