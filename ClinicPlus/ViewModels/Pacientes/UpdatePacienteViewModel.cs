namespace ClinicPlus.ViewModels.Pacientes;

public class UpdatePacienteViewModel
{
    public int Id { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
}