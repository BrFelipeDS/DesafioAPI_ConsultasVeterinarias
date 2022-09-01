using APIMaisEventos.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System;
using DesafioAPIConsultasVeterinarias.Repositories;
using DesafioAPIConsultasVeterinarias.Models;

namespace DesafioAPIConsultasVeterinarias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {

        private ConsultasRepository repositorio = new ConsultasRepository();

        // POST - Cadastrar
        /// <summary>
        /// Cadastra consultas na aplicação
        /// </summary>
        /// <param name="consulta">Dados da consulta</param>
        /// <returns>Dados da consulta cadastrada</returns>
        [HttpPost]
        public IActionResult Cadastrar(Consultas consulta)
        {
            try
            {
                repositorio.Insert(consulta);
                return Ok(consulta);

            }
            catch (InvalidOperationException ex) //Excessão gerada por operações inválidas
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message,
                });
            }
            catch (SqlException ex) //Excessão gerada por erros relacionados aos scripts SQL
            {
                return StatusCode(500, new
                {
                    msg = "Falha na sintaxe do código SQL",
                    erro = ex.Message,
                });
            }
            catch (Exception ex) // Excessão gerada por erro genérico
            {
                return StatusCode(500, new
                {
                    msg = "Falha na definição do código",
                    erro = ex.Message
                });
            }
        }

        // GET - Listar
        /// <summary>
        /// Lista todas as consultas da apliacação
        /// </summary>
        /// <returns>Lista de consultas</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var consultas = repositorio.GetAll();
                return Ok(consultas);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message,
                });
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na sintaxe do código SQL",
                    erro = ex.Message,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na definição do código",
                    erro = ex.Message
                });
            }
        }

        // PUT - Alterar
        /// <summary>
        /// Altera os dados de uma consulta
        /// </summary>
        /// <param name="id">Id da consulta</param>
        /// <param name="consulta">Todas as informações do usuário</param>
        /// <returns>Consulta alterada</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Consultas consulta)
        {
            try
            {
                var buscarConsulta = repositorio.GetById(id);

                //Confere se há algum item da tabela com o Id especificado, se não, retorna Não Encontrado
                if (buscarConsulta is null)
                {
                    return NotFound(new
                    {
                        msg = "Id da consulta inválido"
                    });
                }
                
                var consultaAlterada = repositorio.Update(id, consulta);

                return Ok(consulta);

            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message,
                });
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na sintaxe do código SQL",
                    erro = ex.Message,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na definição do código",
                    erro = ex.Message
                });
            }
        }

        // DELETE - Excluir
        /// <summary>
        /// Exclui uma consulta da aplicação
        /// </summary>
        /// <param name="id">Id da consulta</param>
        /// <returns>Mensagem de exclusão</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                var buscarConsulta = repositorio.GetById(id);

                if (buscarConsulta is null)
                {
                    return NotFound(new
                    {
                        msg = "Id da consulta inválido"
                    });
                }

                repositorio.Delete(id);

                return Ok(new
                {
                    msg = "Consulta excluída com sucesso"
                });

            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = ex.Message,
                });
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na sintaxe do código SQL",
                    erro = ex.Message,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na definição do código",
                    erro = ex.Message
                });
            }
        }

    }
}
