namespace ClinicPlus.Models.Entities;

public class Consulta
{
    public int  Id { get; set; }
    public DateTime Data { get; set; }
    
    public int EspecialidadeId { get; set; }
    public Especialidade Especialidade { get; set; } = null!;
    
    public int MedicoId { get; set; }
    public Medico Medico { get; set; } = null!;
    
    public int PacienteId { get; set; }
    public Paciente Paciente { get; set; } = null!;
}