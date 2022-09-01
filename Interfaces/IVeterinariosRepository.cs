using DesafioAPIConsultasVeterinarias.Models;
using System.Collections.Generic;

namespace DesafioAPIConsultasVeterinarias.Interfaces
{
    public interface IVeterinariosRepository
    {
        // CRUD

        // Read
        ICollection<Veterinarios> GetAll();
        Veterinarios GetById(int id);

        // Create
        Veterinarios Insert(Veterinarios veterinario);

        // Update
        Veterinarios Update(int id, Veterinarios veterinario);

        // Delete
        bool Delete(int id);
    }
}
