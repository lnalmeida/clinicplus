namespace ClinicPlus.ViewModels.Consultss;

public class CalendarioConsultaViewModel
{
    public int Id { get; set; }
    public string EventTitle { get; set; } = String.Empty;
    public DateTime StartEvent { get; set; }
    public DateTime EndEvent { get; set; }
    public string Color { get; set; } = string.Empty;
}