using ClinicPlus.Models.Entities;
using ClinicPlus.Models.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace ClinicPlus.Contexts;

public class ClinicPlusContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public DbSet<Medico> Medicos => Set<Medico>();
    public DbSet<Paciente> Pacientes => Set<Paciente>();

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
    }
}