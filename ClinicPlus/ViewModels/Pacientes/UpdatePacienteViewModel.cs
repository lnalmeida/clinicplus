using System.ComponentModel.DataAnnotations;

namespace ClinicPlus.ViewModels.Pacientes;

public class UpdatePacienteViewModel
{
    public int Id { get; set; }
    
    public string Cpf { get; set; } = string.Empty;
    
    public string Nome { get; set; } = string.Empty;
    
    [Display(Name = "Data de Nasimento")]
    [DataType(DataType.Date)]
    public DateTime DataNascimento { get; set; }
    
    public string? Alergias { get; set; }
    
    [Display(Name = "Medicamentos em uso")]
    
    public string? MedicamentosEmUso { get; set; }
    
    [Display(Name = "Cirurgias realizadas")]
    public string? CirurgiasRealizadas { get; set; }
}