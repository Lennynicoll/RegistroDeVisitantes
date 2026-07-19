namespace RegistroVisitantes.Application.Dtos.NotaVisita
{
    public class NotaVisitaDTO : DtoBase
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }
}
