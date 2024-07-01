using System.ComponentModel.DataAnnotations;

namespace ClinicPlus.ViewModels.Consultas;

public class AddConsultaViewModel
{
    public DateTime Data { get; set; }
    [Display(Name = "Especialidade")]
    public int EspecialidadeId { get; set; }
    [Display(Name = "Médico")]
    public int MedicoId { get; set; }
    [Display(Name = "Paciente")]
    public int PacienteId { get; set; }
}