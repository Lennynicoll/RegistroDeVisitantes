using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class RecetaMedica : BaseEntity
    {
        public DateTime FechaEmision { get; set; }
        public string Observaciones { get; set; } = string.Empty;

        public int CitaId { get; set; }
        public Cita? Cita { get; set; }

        public List<DetalleReceta> DetallesReceta { get; set; } = new List<DetalleReceta>();
    }
}
