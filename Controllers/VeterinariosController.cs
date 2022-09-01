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
    public class VeterinariosController : ControllerBase
    {
        private VeterinariosRepository repositorio = new VeterinariosRepository();

        // POST - Cadastrar
        /// <summary>
        /// Cadastra veterinários na aplicação
        /// </summary>
        /// <param name="veterinario">Dados do veterinário</param>
        /// <param name="arquivo">Arquivo a ser subido</param>
        /// <returns>Dados do veterinário cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar([FromForm] Veterinarios veterinario, IFormFile arquivo)
        {
            try
            {
                #region Upload de Imagem
                string[] extensoesPermitidas = { "jpeg", "jpg", "png", "svg" };
                string uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas, "Images");

                if (uploadResultado == "")
                {
                    return BadRequest("Arquivo não encontrado ou extensão não permitida");
                }

                veterinario.Imagem = uploadResultado;
                #endregion


                repositorio.Insert(veterinario);
                return Ok(veterinario);

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
        /// Lista todos os veterinários da apliacação
        /// </summary>
        /// <returns>Lista de veterinários</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var veterionarios = repositorio.GetAll();
                return Ok(veterionarios);
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
        /// Altera os dados de um veterinário
        /// </summary>
        /// <param name="id">Id do veterinário</param>
        /// <param name="veterinario">Todas as informações do veterinário</param>
        /// <param name="arquivo">Arquivo anexado</param>
        /// <returns>Veterinário alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, [FromForm] Veterinarios veterinario, IFormFile arquivo)
        {
            try
            {
                var buscarVeterinario = repositorio.GetById(id);

                if (buscarVeterinario == null)
                {
                    return NotFound(new
                    {
                        msg = "Id do(a) veterinário(a) inválido"
                    });
                }

                #region Upload de Imagem
                string[] extensoesPermitidas = { "jpeg", "jpg", "png", "svg" };
                string uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas, "Images");

                if (uploadResultado == "")
                {
                    return BadRequest("Arquivo não encontrado ou extensão não permitida");
                }

                veterinario.Imagem = uploadResultado;
                #endregion

                var animalAlterado = repositorio.Update(id, veterinario);

                return Ok(veterinario);

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
        /// Exclui um veterinário da aplicação
        /// </summary>
        /// <param name="id">Id do veterinário</param>
        /// <returns>Mensagem de exclusão</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                var buscarVeterinario = repositorio.GetById(id);

                if (buscarVeterinario == null)
                {
                    return NotFound(new
                    {
                        msg = "Id do(a) veterinário(a) inválido"
                    });
                }

                repositorio.Delete(id);

                return Ok(new
                {
                    msg = "Veterinário excluído com sucesso"
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
