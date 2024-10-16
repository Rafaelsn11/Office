using Microsoft.EntityFrameworkCore;
using Office.Context;
using Office.Models.Dtos;
using Office.Models.Entities;
using Office.Repository.Interfaces;

namespace Office.Repository;

public class PacienteRepository : BaseRepository, IPacienteRepository
{
    private readonly OfficeContext _context;

    public PacienteRepository(OfficeContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PacienteDto>> GetPacientesAsync()
        => await _context.Pacientes
        .Select(x => new PacienteDto { Id = x.Id, Nome = x.Nome })
        .ToListAsync();

    public async Task<Paciente> GetPacientesByIdAsync(int id)
        => await _context.Pacientes
        .Include(x => x.Consultas)
        .ThenInclude(c => c.Especialidade)
        .ThenInclude(c => c.Profissionais)
        .Where(x => x.Id == id).FirstOrDefaultAsync();
}
