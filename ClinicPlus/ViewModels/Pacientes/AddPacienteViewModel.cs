namespace ClinicPlus.ViewModels.Pacientes;

public class AddPacienteViewModel
{
    public string Cpf { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
}