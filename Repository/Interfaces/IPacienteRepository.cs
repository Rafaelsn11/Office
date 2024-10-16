using Office.Models.Dtos;
using Office.Models.Entities;

namespace Office.Repository.Interfaces;

public interface IPacienteRepository : IBaseRepository
{
    Task<IEnumerable<PacienteDto>> GetPacientesAsync();
    Task<Paciente> GetPacientesByIdAsync(int id);
}
