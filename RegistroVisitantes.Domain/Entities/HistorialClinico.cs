using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class HistorialClinico : BaseEntity
    {
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;

        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }

        public int DiagnosticoId { get; set; }
        public Diagnostico? Diagnostico { get; set; }
    }
}
