namespace RegistroVisitantes.Application.Dtos.RecetaMedica
{
    public class RecetaMedicaDTO : DtoBase
    {
        public DateTime FechaEmision { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public int CitaId { get; set; }
    }
}
