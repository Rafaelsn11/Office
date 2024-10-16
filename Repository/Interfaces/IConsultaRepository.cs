using Office.Models.Entities;
using Office.Models.Params;

namespace Office.Repository.Interfaces;

public interface IConsultaRepository : IBaseRepository
{
    Task<IEnumerable<Consulta>> GetConsultas();
    Task<IEnumerable<Consulta>> GetConsultas(ConsultaParams parametro);
    Task<Consulta> GetConsultaById(int id);
}
