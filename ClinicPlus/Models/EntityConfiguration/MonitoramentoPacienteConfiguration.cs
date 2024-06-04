using ClinicPlus.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicPlus.Models.EntityConfiguration;

public class MonitoramentoPacienteConfiguration : IEntityTypeConfiguration<MonitoramentoPaciente>
{
    public void Configure(EntityTypeBuilder<MonitoramentoPaciente> builder)
    {
        builder.ToTable("MonitoramentosPacientes");

        builder.HasKey(mp => mp.Id);

        builder.Property(mp => mp.PressaoArterial)
            .IsRequired()
            .HasMaxLength(7);

        builder.Property(mp => mp.Temperatura)
            .HasColumnType("DECIMAL(3,1)");

        builder.Property(mp => mp.Saturacao)
            .HasColumnType("TINYINT");
        
        builder.Property(mp => mp.FrequenciaCardiaca)
            .HasColumnType("TINYINT");
        
        builder.Property(mp => mp.DataAfericao)
            .IsRequired();
    }
}