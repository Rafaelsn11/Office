using Microsoft.EntityFrameworkCore;
using Office.Context;
using Office.Models.Dtos;
using Office.Models.Entities;
using Office.Repository.Interfaces;

namespace Office.Repository;

public class ProfissionalRepository : BaseRepository, IProfissionalRepository
{
    private readonly OfficeContext _context;
    public ProfissionalRepository(OfficeContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<ProfissionalDto>> GetProfissionaisAsync()
        => await _context.Profissionais.Select
        (x => new ProfissionalDto { Id = x.Id, Nome = x.Nome, Ativo = x.Ativo })
        .ToListAsync();

    public async Task<Profissional> GetProfissionalByIdAsync(int id)
        => await _context.Profissionais
        .Include(x => x.Consultas)
        .Include(x => x.Especialidades)
        .Where(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<ProfissionalEspecialidade> GetProfissionalEspecialidade(int profissionalId, int especialidadeId)
        => await _context.ProfissionaisEspecialidades
            .Where(x => x.ProfissionalId == profissionalId && x.EspecialidadeId == especialidadeId)
            .FirstOrDefaultAsync();
}
