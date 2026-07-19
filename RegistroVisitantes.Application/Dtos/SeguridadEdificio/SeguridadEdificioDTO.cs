namespace RegistroVisitantes.Application.Dtos.SeguridadEdificio
{
    public class SeguridadEdificioDTO : DtoBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Cobertura { get; set; } = string.Empty;
    }
}
