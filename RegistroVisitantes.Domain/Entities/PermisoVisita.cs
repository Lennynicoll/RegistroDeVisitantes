using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class PermisoVisita : BaseEntity
    {
        public DateTime FechaEmision { get; set; }
        public string Observaciones { get; set; } = string.Empty;

        public int RegistroVisitaId { get; set; }
        public RegistroVisita? RegistroVisita { get; set; }

        public List<DetallePermiso> DetallesPermiso { get; set; } = new List<DetallePermiso>();
    }
}
