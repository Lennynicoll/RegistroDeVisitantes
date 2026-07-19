using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class VisitanteSeguro : BaseEntity
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string NumeroPoliza { get; set; } = string.Empty;

        public int RegistroVisitanteId { get; set; }
        public RegistroVisitante? RegistroVisitante { get; set; }

        public int SeguridadEdificioId { get; set; }
        public SeguridadEdificio? SeguridadEdificio { get; set; }
    }
}
