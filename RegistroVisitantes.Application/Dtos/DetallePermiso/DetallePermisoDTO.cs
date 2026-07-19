namespace RegistroVisitantes.Application.Dtos.DetallePermiso
{
    public class DetallePermisoDTO : DtoBase
    {
        public string Dosis { get; set; } = string.Empty;
        public string Frecuencia { get; set; } = string.Empty;
        public int DuracionDias { get; set; }
        public string Indicaciones { get; set; } = string.Empty;
        public int PermisoVisitaId { get; set; }
        public int OficinaId { get; set; }
    }
}
