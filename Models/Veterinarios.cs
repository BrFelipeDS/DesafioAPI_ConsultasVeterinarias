using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DesafioAPIConsultasVeterinarias.Models
{
    public class Veterinarios
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome do veterinário é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email do veterinário é obrigatório")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contato do veterinário é obrigatório")]
        [MinLength(11, ErrorMessage = "Insira um número de contato válido. DDD + Número, sem traços (Min. 11 dígitos)")]
        public string Contato { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Imagem { get; set; }
    }
}
