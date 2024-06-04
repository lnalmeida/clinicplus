using ClinicPlus.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicPlus.Models.EntityConfiguration;

public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.ToTable("Pacientes");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Cpf)
            .IsRequired()
            .HasMaxLength(11);
        
        builder.HasIndex(p => p.Cpf)
            .IsUnique();
        
        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(150);
        
        builder.Property(p => p.DataNascimento)
            .IsRequired();

        builder.HasOne(p => p.InformacoesPaciente)
            .WithOne(ip => ip.Paciente)
            .HasForeignKey<InformacoesComplementaresPaciente>(ip => ip.IdPaciente);

        builder.HasMany(p => p.Monitoramentos)
            .WithOne(mp => mp.Paciente)
            .HasForeignKey(mp => mp.IdPaciente);
    }
}