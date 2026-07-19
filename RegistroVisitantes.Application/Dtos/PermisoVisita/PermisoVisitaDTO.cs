namespace RegistroVisitantes.Application.Dtos.PermisoVisita
{
    public class PermisoVisitaDTO : DtoBase
    {
        public DateTime FechaEmision { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public int RegistroVisitaId { get; set; }
    }
}
