using ClinicPlus.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicPlus.Models.EntityConfiguration;

public class InformacoesComplementaresPacienteConfiguration : IEntityTypeConfiguration<InformacoesComplementaresPaciente>
{
    public void Configure(EntityTypeBuilder<InformacoesComplementaresPaciente> builder)
    {
        builder.ToTable("InformacoesComplementaresPaciente");

        builder.HasKey(ip => ip.Id);

        builder.Property(ip => ip.Alergias)
            .HasMaxLength(500);
        
        builder.Property(ip => ip.MedicamentosEmUso)
            .HasMaxLength(500);
        
        builder.Property(ip => ip.CirurgiasRealizadas)
            .HasMaxLength(500);
    }
}