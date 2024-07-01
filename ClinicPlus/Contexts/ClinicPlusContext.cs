using ClinicPlus.Models.Entities;
using ClinicPlus.Models.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace ClinicPlus.Contexts;

public class ClinicPlusContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public DbSet<Medico> Medicos => Set<Medico>();
    public DbSet<Paciente> Pacientes => Set<Paciente>();
    public DbSet<InformacoesComplementaresPaciente> InformacoesComplementaresPacientes =>
        Set<InformacoesComplementaresPaciente>();
    public DbSet<MonitoramentoPaciente> MonitoramentosPacientes => Set<MonitoramentoPaciente>();
    public DbSet<Especialidade> Especialidades => Set<Especialidade>();
    public DbSet<Consulta> Consultas => Set<Consulta>();
    
    public ClinicPlusContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ClinicPlus"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MedicoConfiguration());
        modelBuilder.ApplyConfiguration(new PacienteConfiguration());
        modelBuilder.ApplyConfiguration(new InformacoesComplementaresPacienteConfiguration());
        modelBuilder.ApplyConfiguration(new MonitoramentoPacienteConfiguration());
        modelBuilder.ApplyConfiguration(new EspecialidadeConfiguration());
        modelBuilder.ApplyConfiguration(new ConsultaConfiguration());

    }
}