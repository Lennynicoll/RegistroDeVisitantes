namespace RegistroVisitantes.Infrastructure.Models
{
    public class VisitaDTO
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Comentarios { get; set; } = string.Empty;
        public int VisitanteId { get; set; }
    }
}
