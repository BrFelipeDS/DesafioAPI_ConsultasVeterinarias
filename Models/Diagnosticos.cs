using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DesafioAPIConsultasVeterinarias.Models
{
    public class Diagnosticos
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Diagnóstico é obrigatório")]
        public string Diagnostico { get; set; }

        [Required(ErrorMessage = "Id da consulta é obrigatório")]
        public int ConsultaId { get; set; } // FK da tabela Diagnósticos
    }
}
