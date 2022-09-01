using DesafioAPIConsultasVeterinarias.Models;
using System.Collections.Generic;

namespace DesafioAPIConsultasVeterinarias.Interfaces
{
    //Criação da interface referente aos métodos que devem ser implementados no Repository de cada Model
    public interface IConsultasRepository
    {
        // CRUD

        // Read
        ICollection<Consultas> GetAll();
        Consultas GetById(int id);

        // Create
        Consultas Insert(Consultas consulta);

        // Update
        Consultas Update(int id,Consultas consulta);

        // Delete
        bool Delete(int id);
    }
}
