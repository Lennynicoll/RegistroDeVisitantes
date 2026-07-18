using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class Cita : BaseEntity
    {
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Estado { get; set; } = "Pendiente";
        public string Observaciones { get; set; } = string.Empty;

        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }

        public int MedicoId { get; set; }
        public Medico? Medico { get; set; }
    }
}
