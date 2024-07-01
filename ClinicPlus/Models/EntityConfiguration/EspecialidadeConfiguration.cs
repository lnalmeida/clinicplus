using ClinicPlus.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicPlus.Models.EntityConfiguration;

public class EspecialidadeConfiguration : IEntityTypeConfiguration<Especialidade>
{
    public void Configure(EntityTypeBuilder<Especialidade> builder)
    {
        builder.ToTable("Especialidades");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Titulo)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Cor)
            .HasMaxLength(7);
        
    }
}