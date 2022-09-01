using APIMaisEventos.Utils;
using DesafioAPIConsultasVeterinarias.Models;
using DesafioAPIConsultasVeterinarias.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System;

namespace DesafioAPIConsultasVeterinarias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticosController : ControllerBase
    {
        private DiagnosticosRepository repositorio = new DiagnosticosRepository();

        // POST - Cadastrar
        /// <summary>
        /// Cadastra diagnóstico na aplicação
        /// </summary>
        /// <param name="diagnostico">Dados do diagnóstico</param>
        /// <returns>Dados do diagnóstico cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(Diagnosticos diagnostico)
        {
            try
            {
                repositorio.Insert(diagnostico);
                return Ok(diagnostico);

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

        // GET - Listar
        /// <summary>
        /// Lista todos os diagnósticos da apliacação
        /// </summary>
        /// <returns>Lista de diagnósticos</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var diagnosticos = repositorio.GetAll();
                return Ok(diagnosticos);
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
        /// Altera os dados de um diagnóstico
        /// </summary>
        /// <param name="id">Id do diagnóstico</param>
        /// <param name="diagnostico">Todas as informações do diagnóstico</param>
        /// <returns>Diagnóstico alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Diagnosticos diagnostico)
        {
            try
            {
                var buscarDiagnostico = repositorio.GetById(id);

                if (buscarDiagnostico == null)
                {
                    return NotFound(new
                    {
                        msg = "Id do diagnóstico inválido"
                    });
                }

                var animalAlterado = repositorio.Update(id, diagnostico);

                return Ok(diagnostico);

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
        /// Exclui um diagnostico da aplicação
        /// </summary>
        /// <param name="id">Id do diagnostico</param>
        /// <returns>Mensagem de exclusão</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                var buscarDiagnostico = repositorio.GetById(id);

                if (buscarDiagnostico == null)
                {
                    return NotFound(new
                    {
                        msg = "Id do diagnóstico inválido"
                    });
                }

                repositorio.Delete(id);

                return Ok(new
                {
                    msg = "Diagnóstico excluído com sucesso"
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
