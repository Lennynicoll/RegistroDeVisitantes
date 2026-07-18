namespace RegistroVisitantes.Application.Dtos.DetalleReceta
{
    public class DetalleRecetaDTO : DtoBase
    {
        public string Dosis { get; set; } = string.Empty;
        public string Frecuencia { get; set; } = string.Empty;
        public int DuracionDias { get; set; }
        public string Indicaciones { get; set; } = string.Empty;
        public int RecetaMedicaId { get; set; }
        public int MedicamentoId { get; set; }
    }
}
