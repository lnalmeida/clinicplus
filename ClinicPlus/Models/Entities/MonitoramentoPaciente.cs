namespace ClinicPlus.Models.Entities;

public class MonitoramentoPaciente
{
    public int Id { get; set; }
    public string PressaoArterial { get; set; } = String.Empty;
    public decimal Temperatura { get; set; }
    public int Saturacao { get; set; }
    public int FrequenciaCardiaca { get; set; }
    public DateTime DataAfericao { get; set; }
    public int IdPaciente { get; set; }
    public Paciente Paciente { get; set; } = null!;
}