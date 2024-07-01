﻿// <auto-generated />
using System;
using ClinicPlus.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClinicPlus.Migrations
{
    [DbContext(typeof(ClinicPlusContext))]
    [Migration("20240612123449_EntityEspecialidades")]
    partial class EntityEspecialidades
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClinicPlus.Models.Entities.Especialidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cor")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Especialidades", (string)null);
                });

            modelBuilder.Entity("ClinicPlus.Models.Entities.InformacoesComplementaresPaciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Alergias")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("CirurgiasRealizadas")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("IdPaciente")
                        .HasColumnType("int");

                    b.Property<string>("MedicamentosEmUso")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("IdPaciente")
                        .IsUnique();

                    b.ToTable("InformacoesComplementaresPaciente", (string)null);
                });

            modelBuilder.Entity("ClinicPlus.Models.Entities.Medico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CRM")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<int?>("EspecialidadeId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("CRM")
                        .IsUnique();

                    b.HasIndex("EspecialidadeId");

                    b.ToTable("Medicos", (string)null);
                });

            modelBuilder.Entity("ClinicPlus.Models.Entities.MonitoramentoPaciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataAfericao")
                        .HasColumnType("datetime2");

                    b.Property<byte>("FrequenciaCardiaca")
                        .HasColumnType("TINYINT");

                    b.Property<int>("IdPaciente")
                        .HasColumnType("int");

                    b.Property<string>("PressaoArterial")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<byte>("Saturacao")
                        .HasColumnType("TINYINT");

                    b.Property<decimal>("Temperatura")
                        .HasColumnType("DECIMAL(3,1)");

                    b.HasKey("Id");

                    b.HasIndex("IdPaciente");

                    b.ToTable("MonitoramentosPacientes", (string)null);
                });

            modelBuilder.Entity("ClinicPlus.Models.Entities.Paciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.ToTable("Pacientes", (string)null);
                });

            modelBuilder.Entity("ClinicPlus.Models.Entities.InformacoesComplementaresPaciente", b =>
                {
                    b.HasOne("ClinicPlus.Models.Entities.Paciente", "Paciente")
                        .WithOne("InformacoesPaciente")
                        .HasForeignKey("ClinicPlus.Models.Entities.InformacoesComplementaresPaciente", "IdPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("ClinicPlus.Models.Entities.Medico", b =>
                {
                    b.HasOne("ClinicPlus.Models.Entities.Especialidade", null)
                        .WithMany("Medicos")
                        .HasForeignKey("EspecialidadeId");
                });

            modelBuilder.Entity("ClinicPlus.Models.Entities.MonitoramentoPaciente", b =>
                {
                    b.HasOne("ClinicPlus.Models.Entities.Paciente", "Paciente")
                        .WithMany("Monitoramentos")
                        .HasForeignKey("IdPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("ClinicPlus.Models.Entities.Especialidade", b =>
                {
                    b.Navigation("Medicos");
                });

            modelBuilder.Entity("ClinicPlus.Models.Entities.Paciente", b =>
                {
                    b.Navigation("InformacoesPaciente");

                    b.Navigation("Monitoramentos");
                });
#pragma warning restore 612, 618
        }
    }
}
