namespace RegistroVisitantes.Application.Dtos.PacienteSeguro
{
    public class PacienteSeguroDTO : DtoBase
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string NumeroPoliza { get; set; } = string.Empty;
        public int PacienteId { get; set; }
        public int SeguroMedicoId { get; set; }
    }
}
