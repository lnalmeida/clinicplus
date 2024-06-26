using System.Collections;

namespace ClinicPlus.Models.Entities;

public class Paciente
{
    public int Id { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public InformacoesComplementaresPaciente? InformacoesPaciente { get; set; } = null!;
    public ICollection<MonitoramentoPaciente> Monitoramentos { get; set; } = null!;
    public ICollection<Consulta> Consultas { get; set; } = null!;
}