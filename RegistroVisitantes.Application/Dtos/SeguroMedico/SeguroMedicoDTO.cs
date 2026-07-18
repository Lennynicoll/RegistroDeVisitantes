namespace RegistroVisitantes.Application.Dtos.SeguroMedico
{
    public class SeguroMedicoDTO : DtoBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Cobertura { get; set; } = string.Empty;
    }
}
