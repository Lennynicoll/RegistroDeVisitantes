using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class RegistroVisita : BaseEntity
    {
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Estado { get; set; } = "Pendiente";
        public string Observaciones { get; set; } = string.Empty;

        public int RegistroVisitanteId { get; set; }
        public RegistroVisitante? RegistroVisitante { get; set; }

        public int AnfitrionId { get; set; }
        public Anfitrion? Anfitrion { get; set; }
    }
}
