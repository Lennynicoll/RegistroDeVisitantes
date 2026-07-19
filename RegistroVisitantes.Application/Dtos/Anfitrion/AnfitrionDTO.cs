namespace RegistroVisitantes.Application.Dtos.Anfitrion
{
    public class AnfitrionDTO : DtoBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string HorarioAtencion { get; set; } = string.Empty;
        public int MotivoVisitaId { get; set; }
        public int DepartamentoId { get; set; }
    }
}
