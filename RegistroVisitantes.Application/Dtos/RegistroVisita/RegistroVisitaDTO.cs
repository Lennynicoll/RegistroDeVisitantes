namespace RegistroVisitantes.Application.Dtos.RegistroVisita
{
    public class RegistroVisitaDTO : DtoBase
    {
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Estado { get; set; } = "Pendiente";
        public string Observaciones { get; set; } = string.Empty;
        public int RegistroVisitanteId { get; set; }
        public int AnfitrionId { get; set; }
    }
}
