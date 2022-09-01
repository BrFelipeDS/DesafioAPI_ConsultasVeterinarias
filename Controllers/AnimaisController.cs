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
    public class AnimaisController : ControllerBase
    {
        private AnimaisRepository repositorio = new AnimaisRepository();

        // POST - Cadastrar
        /// <summary>
        /// Cadastra animais na aplicação
        /// </summary>
        /// <param name="animal">Dados do paciente animal</param>
        /// <param name="arquivo">Arquivo a ser subido</param>
        /// <returns>Dados do animal cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar([FromForm] Animais animal, IFormFile arquivo)
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
                else if(uploadResultado is null)
                {
                    return BadRequest(new
                    {
                        msg = "Foto do animal é obrigatória"
                    }); 
                }
                else
                {
                    animal.Imagem = uploadResultado;
                }                
                #endregion


                repositorio.Insert(animal);
                return Ok(animal);

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
        /// Lista todos os animais da apliacação
        /// </summary>
        /// <returns>Lista de animais</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var animais = repositorio.GetAll();
                return Ok(animais);
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
        /// Altera os dados de um animal
        /// </summary>
        /// <param name="id">Id do paciente animal</param>
        /// <param name="animal">Todas as informações do paciente animal</param>
        /// <param name="arquivo">Arquivo anexado</param>
        /// <returns>Paciente animal alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, [FromForm] Animais animal, IFormFile arquivo)
        {
            try
            {
                var buscarAnimal = repositorio.GetById(id);

                if (buscarAnimal is null)
                {
                    return NotFound(new
                    {
                        msg = "Id do animal inválido"
                    });
                }

                #region Upload de Imagem
                string[] extensoesPermitidas = { "jpeg", "jpg", "png", "svg" };
                string uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas, "Images");

                if (uploadResultado == "")
                {
                    return BadRequest("Arquivo não encontrado ou extensão não permitida");
                }
                else if (uploadResultado is null)
                {
                    return BadRequest(new
                    {
                        msg = "Foto do animal é obrigatória"
                    });
                }
                else
                {
                    animal.Imagem = uploadResultado;
                }
                #endregion

                var animalAlterado = repositorio.Update(id, animal);

                return Ok(animal);

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
        /// Exclui um animal da aplicação
        /// </summary>
        /// <param name="id">Id do paciente animal</param>
        /// <returns>Mensagem de exclusão</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                var buscarAnimal = repositorio.GetById(id);

                if (buscarAnimal is null)
                {
                    return NotFound(new
                    {
                        msg = "Id do animal inválido"
                    });
                }

                repositorio.Delete(id);

                return Ok(new
                {
                    msg = "Usuário excluído com sucesso"
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
