using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DesafioAPIConsultasVeterinarias.Models
{
    public class Diagnosticos
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Diagnóstico é obrigatório")]
        public string Diagnostico { get; set; }

        [Required(ErrorMessage = "Id da consulta é obrigatório")]
        public int ConsultaId { get; set; } // FK da tabela Diagnósticos
    }
}
