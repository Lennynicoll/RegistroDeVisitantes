using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class Visita : BaseEntity
    {
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Comentarios { get; set; } = string.Empty;

        public int VisitanteId { get; set; }
        public Visitante? Visitante { get; set; }
    }
}
