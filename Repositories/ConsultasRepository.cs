using DesafioAPIConsultasVeterinarias.Interfaces;
using DesafioAPIConsultasVeterinarias.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace DesafioAPIConsultasVeterinarias.Repositories
{
    public class ConsultasRepository : IConsultasRepository
    {
        // string para habilitar a conesão com o Database do projeto

        readonly string connectionString = "Data Source = NB033786\\SQLEXPRESS2; Integrated Security = true; Initial Catalog = Consultas_Veterinarias";

        /// <summary>
        /// Método que possibilita a deleção de uma linha da respectiva tabela
        /// </summary>
        /// <param name="id">Id do item a ser deletado</param>
        /// <returns>Retorna booleano indicando se houve a exclusão</returns>
        public bool Delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                // Comando SQL a ser executada no DB
                string script = "DELETE FROM Consultas WHERE Id=@id";

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

        /// <summary>
        /// Método responsável por gerar uma lista com todos os itens da respectiva tabela
        /// </summary>
        /// <returns>Lista de itens da tabela</returns>
        public ICollection<Consultas> GetAll()
        {
            var consultas = new List<Consultas>();

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                // Seleciona as colunas referente às informações da tabela Consultas e realiza
                // um JOIN com as tabelas Animais e Veterinarios, para puxar as informações de cada uma
                // em relação ao Id em comum das Foreign Keys de Consultas
                string query = @"SELECT
                                        C.Id AS 'Id_Consulta',
                                        C.Descricao AS 'Descricao_Consulta',
                                        C.AnimalId AS 'Id_Animal',
                                        Animais.Nome AS 'Nome_Animal',
                                        Animais.Raca AS 'Raca_Animal',
                                        Animais.NomeDono AS 'Dono',
                                        Animais.ContatoDono AS 'Contato_Dono',
                                        C.VeterinarioId AS 'Id_Veterinario',
                                        Veterinarios.Nome AS 'Nome_Veterinario',
                                        Veterinarios.Email AS 'Email_Veterinario',
                                        Veterinarios.Contato AS 'Contato_Veterinario'
                                    FROM Consultas AS C
                                    JOIN Animais ON C.AnimalId = Animais.Id
                                    JOIN Veterinarios ON C.VeterinarioId = Veterinarios.Id";



                using (SqlCommand cmd = new SqlCommand(query, conexao))
                {
                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Exibe as informãções das tabelas das respectivas Foreign Keys associadas aos itens em
                            // Animais e Veterinários
                            consultas.Add(new Consultas
                            {
                                Id = (int)reader["Id_Consulta"],
                                Descricao = (string)reader["Descricao_Consulta"],
                                AnimalId = (int)reader["Id_Animal"],
                                Animal = new Animais
                                {
                                    Nome = (string)reader["Nome_Animal"],
                                    Raca = (string)reader["Raca_Animal"],
                                    NomeDono = (string)reader["Dono"],
                                    ContatoDono = (string)reader["Contato_Dono"],
                                    Imagem = null
                                },
                                VeterinarioId = (int)reader["Id_Veterinario"],
                                Veterinario = new Veterinarios
                                {
                                    Nome = (string)reader["Nome_Veterinario"],
                                    Email = (string)reader["Email_Veterinario"],
                                    Contato = (string)reader["Contato_Veterinario"],
                                    Imagem = null
                                }
                            });
                        }
                    }
                }
            }

            return consultas;
        }

        /// <summary>
        /// Método resonsável por selecionar 1 item da respectiva tabela com base no seu Id
        /// </summary>
        /// <param name="id">Id do item a ser selecionado</param>
        /// <returns>Retorna um objeto do tipo referente ao repository</returns>
        public Consultas GetById(int id)
        {
            Consultas consulta = null;

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string query = "SELECT * FROM Consultas WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(query, conexao))
                {
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            consulta = new Consultas
                            {
                                Id = (int)reader[0],
                                Descricao = (string)reader[1],
                                AnimalId = (int)reader[2],
                                VeterinarioId = (int)reader[3]
                            };
                        }
                    }

                }
            }   

            return consulta;
        }

        /// <summary>
        /// Método responsável por inserir um novo item na respectiva tabela
        /// </summary>
        /// <param name="consulta">Objeto a ser inserido na tabela dentro do DB</param>
        /// <returns>Retorna um objeto do tipo referente ao repository</returns>
        public Consultas Insert(Consultas consulta)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                // Abre uma conexão
                conexao.Open();

                string script = "INSERT INTO Consultas (Descricao, AnimalId, VeterinarioId) VALUES (@Descricao, @AnimalId, @VeterinarioId)";

                // Criamos um comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetro
                    cmd.Parameters.Add("@Descricao", System.Data.SqlDbType.NVarChar).Value = consulta.Descricao;
                    cmd.Parameters.Add("@AnimalId", System.Data.SqlDbType.Int).Value = consulta.AnimalId;
                    cmd.Parameters.Add("@VeterinarioId", System.Data.SqlDbType.Int).Value = consulta.VeterinarioId;

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return consulta;
        }

        /// <summary>
        /// Método responsável por alterar os dados de algum item da respectiva tabela, através do Id
        /// </summary>
        /// <param name="id">Id do item a ser alterado</param>
        /// <param name="consulta">Objeto que irá substituir os objeto presente no item a ser substituído</param>
        /// <returns>Retorna um objeto do tipo referente ao repository</returns>
        public Consultas Update(int id, Consultas consulta)
        {            
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                string script = "UPDATE Consultas SET Descricao=@Descricao, AnimalId=@AnimalId, VeterinarioId=@VeterinarioId WHERE Id=@id";

                // Criamos um comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    // Fazemos as declarações das variáveis por parâmetro
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@Descricao", System.Data.SqlDbType.NVarChar).Value = consulta.Descricao;
                    cmd.Parameters.Add("@AnimalId", System.Data.SqlDbType.Int).Value = consulta.AnimalId;
                    cmd.Parameters.Add("@VeterinarioId", System.Data.SqlDbType.Int).Value = consulta.VeterinarioId;

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    consulta.Id = id;
                }
            }

            return consulta;
        }
    }
}
