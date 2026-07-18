namespace RegistroVisitantes.Application.Dtos.HistorialClinico
{
    public class HistorialClinicoDTO : DtoBase
    {
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
        public int PacienteId { get; set; }
        public int DiagnosticoId { get; set; }
    }
}
