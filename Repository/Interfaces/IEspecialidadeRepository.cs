using Office.Models.Dtos;
using Office.Models.Entities;

namespace Office.Repository.Interfaces;

public interface IEspecialidadeRepository : IBaseRepository
{
    Task<IEnumerable<EspecialidadeDto>> GetEspecialidades();
    Task<Especialidade> GetEspecialidadeById(int id);
}
