namespace RegistroVisitantes.Application.Dtos.HistorialVisitas
{
    public class HistorialVisitasDTO : DtoBase
    {
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
        public int RegistroVisitanteId { get; set; }
        public int NotaVisitaId { get; set; }
    }
}
