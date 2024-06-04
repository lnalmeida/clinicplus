using System.ComponentModel.DataAnnotations;

namespace ClinicPlus.ViewModels.Monitoramentos;

public class AddMonitoramentoPacienteViewModel
{
    [Display(Name = "Pressão arterial")]
    public string PressaoArterial { get; set; } = String.Empty;
    [Display(Name = "Temperatura corporal")]
    public decimal Temperatura { get; set; }
    [Display(Name = "Saturação do oxigênio")]
    public int Saturacao { get; set; }
    [Display(Name = "Frequência cardíaca")]
    public int FrequenciaCardiaca { get; set; }
    [Display(Name = "Data da aferição")]
    public DateTime DataAfericao { get; set; }
}