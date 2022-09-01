using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DesafioAPIConsultasVeterinarias.Models
{
    /// <summary>
    /// Definição do Model Consultas
    /// </summary>
    public class Consultas
    {

        // Definição dos atributos do model, baseados nas colunas do banco de dados

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Descrição da consulta é obrigatória")]
        public string Descricao { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [Required(ErrorMessage = "Id do animal é obrigatório")]
        public int AnimalId { get; set; } // FK da tabela Consultas

        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        //public Animais Animal { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [Required(ErrorMessage = "Id do veterinário é obrigatório")]
        public int VeterinarioId { get; set; } // FK da tabela Consultas

        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        //public Veterinarios Veterinario { get; set; }
    }
}
