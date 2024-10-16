using Office.Models.Dtos;
using Office.Models.Entities;

namespace Office.Repository.Interfaces;

public interface IProfissionalRepository : IBaseRepository
{
    Task<IEnumerable<ProfissionalDto>> GetProfissionaisAsync();
    Task<Profissional> GetProfissionalByIdAsync(int id);
    Task<ProfissionalEspecialidade> GetProfissionalEspecialidade(int profissionalId, int especialidadeId);
}
