namespace RegistroVisitantes.Application.Dtos.VisitanteSeguro
{
    public class VisitanteSeguroDTO : DtoBase
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string NumeroPoliza { get; set; } = string.Empty;
        public int RegistroVisitanteId { get; set; }
        public int SeguridadEdificioId { get; set; }
    }
}
