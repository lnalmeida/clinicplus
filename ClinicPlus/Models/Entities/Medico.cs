using System.Collections;

namespace ClinicPlus.Models.Entities;

public class Medico
{
    public int Id { get; set; }
    public string CRM { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
     public int EspecialidadeId { get; set; }
     public Especialidade Especialidade { get; set; } = null!;

     public ICollection<Consulta> Consultas { get; set; } = null!;
}