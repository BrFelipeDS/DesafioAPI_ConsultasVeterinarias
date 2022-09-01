using DesafioAPIConsultasVeterinarias.Models;
using System.Collections.Generic;

namespace DesafioAPIConsultasVeterinarias.Interfaces
{
    public interface IDiagnosticosRepository
    {
        // CRUD

        // Read
        ICollection<Diagnosticos> GetAll();
        Diagnosticos GetById(int id);

        // Create
        Diagnosticos Insert(Diagnosticos diagnostico);

        // Update
        Diagnosticos Update(int id, Diagnosticos diagnostico);

        // Delete
        bool Delete(int id);
    }
}
