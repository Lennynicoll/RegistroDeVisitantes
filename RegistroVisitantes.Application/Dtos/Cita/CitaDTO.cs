namespace RegistroVisitantes.Application.Dtos.Cita
{
    public class CitaDTO : DtoBase
    {
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Estado { get; set; } = "Pendiente";
        public string Observaciones { get; set; } = string.Empty;
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
    }
}
