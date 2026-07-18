namespace RegistroVisitantes.Application.Dtos.Diagnostico
{
    public class DiagnosticoDTO : DtoBase
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }
}
