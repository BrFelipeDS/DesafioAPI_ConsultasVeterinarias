using DesafioAPIConsultasVeterinarias.Models;
using System.Collections.Generic;

namespace DesafioAPIConsultasVeterinarias.Interfaces
{
    public interface IAnimaisRepository
    {
        // CRUD

        // Read
        ICollection<Animais> GetAll();
        Animais GetById(int id);

        // Create
        Animais Insert(Animais animal);

        // Update
        Animais Update(int id, Animais animal);

        // Delete
        bool Delete(int id);
    }
}
