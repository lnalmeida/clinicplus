namespace ClinicPlus.ViewModels.Consultss;

public class ListarConsultasViewModel
{
    public int  Id { get; set; }
    public DateTime Data { get; set; }
    public string Especialidade { get; set; } = String.Empty;
    public string CorEspecialidade { get; set; } = String.Empty;
    public string Medico { get; set; } = String.Empty;
    public string Paciente { get; set; } = String.Empty;
}