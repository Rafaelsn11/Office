using Office.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Office.Context;

public class OfficeContext : DbContext
{
    public OfficeContext(DbContextOptions<OfficeContext> options) : base(options)
    { }

    public DbSet<Consulta> Consultas { get; set; }
    public DbSet<Especialidade> Especialidades { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Profissional> Profissionais { get; set; }
    public DbSet<ProfissionalEspecialidade> ProfissionaisEspecialidades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
