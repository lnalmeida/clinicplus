using ClinicPlus.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicPlus.Models.EntityConfiguration;

public class ConsultaConfiguration :  IEntityTypeConfiguration<Consulta>
{
    public void Configure(EntityTypeBuilder<Consulta> builder)
    {
        builder.ToTable("Consultas");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Data)
            .IsRequired();

        builder.HasOne(c => c.Especialidade)
            .WithMany(e => e.Consultas)
            .HasForeignKey(c => c.EspecialidadeId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(c => c.Medico)
            .WithMany(m => m.Consultas)
            .HasForeignKey(c => c.MedicoId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(c => c.Paciente)
            .WithMany(p => p.Consultas)
            .HasForeignKey(c => c.PacienteId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}