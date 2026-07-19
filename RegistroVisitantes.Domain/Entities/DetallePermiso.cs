using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class DetallePermiso : BaseEntity
    {
        public string Dosis { get; set; } = string.Empty;
        public string Frecuencia { get; set; } = string.Empty;
        public int DuracionDias { get; set; }
        public string Indicaciones { get; set; } = string.Empty;

        public int PermisoVisitaId { get; set; }
        public PermisoVisita? PermisoVisita { get; set; }

        public int OficinaId { get; set; }
        public Oficina? Oficina { get; set; }
    }
}
