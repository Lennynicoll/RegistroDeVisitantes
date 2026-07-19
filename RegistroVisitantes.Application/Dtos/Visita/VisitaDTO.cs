namespace RegistroVisitantes.Application.Dtos.Visita
{
    public class VisitaDTO : DtoBase
    {
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Comentarios { get; set; } = string.Empty;
        public int VisitanteId { get; set; }
    }
}
