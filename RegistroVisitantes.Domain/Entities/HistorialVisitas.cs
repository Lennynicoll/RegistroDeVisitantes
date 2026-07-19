using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class HistorialVisitas : BaseEntity
    {
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;

        public int RegistroVisitanteId { get; set; }
        public RegistroVisitante? RegistroVisitante { get; set; }

        public int NotaVisitaId { get; set; }
        public NotaVisita? NotaVisita { get; set; }
    }
}
