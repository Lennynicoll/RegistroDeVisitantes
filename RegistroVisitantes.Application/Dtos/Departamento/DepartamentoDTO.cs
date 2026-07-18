namespace RegistroVisitantes.Application.Dtos.Departamento
{
    public class DepartamentoDTO : DtoBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
    }
}
