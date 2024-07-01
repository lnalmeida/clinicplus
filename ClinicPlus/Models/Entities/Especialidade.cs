namespace ClinicPlus.Models.Entities;

public class Especialidade
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Cor { get; set; } = "blue";

    public ICollection<Medico> Medicos { get; set; } = null!;
    public ICollection<Consulta> Consultas { get; set; } = null!;
}