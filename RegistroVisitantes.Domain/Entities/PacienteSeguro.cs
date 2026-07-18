using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class PacienteSeguro : BaseEntity
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string NumeroPoliza { get; set; } = string.Empty;

        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }

        public int SeguroMedicoId { get; set; }
        public SeguroMedico? SeguroMedico { get; set; }
    }
}
