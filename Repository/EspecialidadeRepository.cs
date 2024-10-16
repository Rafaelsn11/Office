using Microsoft.EntityFrameworkCore;
using Office.Context;
using Office.Models.Dtos;
using Office.Models.Entities;
using Office.Repository.Interfaces;

namespace Office.Repository;

public class EspecialidadeRepository : BaseRepository, IEspecialidadeRepository
{
    private readonly OfficeContext _context;

    public EspecialidadeRepository(OfficeContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Especialidade> GetEspecialidadeById(int id)
        => await _context.Especialidades
            .Include(x => x.Profissionais)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<EspecialidadeDto>> GetEspecialidades()
        => await _context.Especialidades
            .Select(x => new EspecialidadeDto
            {
                Id = x.Id,
                Nome = x.Nome,
                Ativa = x.Ativa
            }).ToListAsync();
}
