using System.ComponentModel.DataAnnotations;

namespace ClinicPlus.ViewModels.Consultss;

public class UpdateConsultaViewModel
{
    public int  Id { get; set; }
    public DateTime Data { get; set; }
    
    [Display(Name = "Especialidade")]
    public int EspecialidadeId { get; set; }
    public string Especialidade { get; set; } = string.Empty;
    
    [Display(Name = "Médico")]
    public int MedicoId { get; set; }
    public string Medico { get; set; } = string.Empty;
    
    [Display(Name = "Paciente")]
    public int PacienteId { get; set; }
    public string Paciente { get; set; } = String.Empty;
}