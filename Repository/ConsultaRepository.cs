using Microsoft.EntityFrameworkCore;
using Office.Context;
using Office.Models.Entities;
using Office.Models.Params;
using Office.Repository.Interfaces;

namespace Office.Repository;

public class ConsultaRepository : BaseRepository, IConsultaRepository
{
    private readonly OfficeContext _context;
    public ConsultaRepository(OfficeContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Consulta> GetConsultaById(int id)
    => await _context.Consultas
            .Include(x => x.Paciente)
            .Include(x => x.Profissional)
            .Include(x => x.Especialidade)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<Consulta>> GetConsultas()
        => await _context.Consultas
            .Include(x => x.Paciente)
            .Include(x => x.Profissional)
            .Include(x => x.Especialidade)
            .ToListAsync();

    public async Task<IEnumerable<Consulta>> GetConsultas(ConsultaParams parametro)
    {
        var consultas = _context.Consultas
            .Include(x => x.Paciente)
            .Include(x => x.Profissional)
            .Include(x => x.Especialidade)
            .AsQueryable();

        DateTime dataVazia = new DateTime();

        if (parametro.DataInicio != dataVazia)
        {
            var dataInicioUtc = parametro.DataInicio.ToUniversalTime();
            consultas = consultas.Where(x => x.DataHorario >= dataInicioUtc);
        }

        if (parametro.DataFim != dataVazia)
        {
            var dataFimUtc = parametro.DataFim.ToUniversalTime();
            consultas = consultas.Where(x => x.DataHorario <= dataFimUtc);
        }

        if (!string.IsNullOrWhiteSpace(parametro.NomeEspecialidade))
        {
            string nomeEspecialidade = parametro.NomeEspecialidade.ToLower();
            consultas = consultas.Where(x => x.Especialidade.Nome.ToLower().Contains(nomeEspecialidade));
        }
        return await consultas.ToListAsync();
    }
}
