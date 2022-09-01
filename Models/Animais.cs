using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DesafioAPIConsultasVeterinarias.Models
{
    public class Animais
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome do animal é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Raça do animal é obrigatória")]
        public string Raca { get; set; }

        [Required(ErrorMessage = "Nome do dono é obrigatório")]
        public string NomeDono { get; set; }

        [Required(ErrorMessage = "Contato do dono é obrigatório")]
        [MinLength(11, ErrorMessage = "Insira um número de contato válido. DDD + Número, sem traços (Min. 11 dígitos)")]
        public string ContatoDono { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Imagem { get; set; }
    }
}
