namespace RegistroVisitantes.Application.Dtos.Medicamento
{
    public class MedicamentoDTO : DtoBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string Fabricante { get; set; } = string.Empty;
        public string Forma { get; set; } = string.Empty;
        public string Concentracion { get; set; } = string.Empty;
        public string Indicaciones { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
