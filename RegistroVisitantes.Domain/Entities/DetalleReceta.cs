using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class DetalleReceta : BaseEntity
    {
        public string Dosis { get; set; } = string.Empty;
        public string Frecuencia { get; set; } = string.Empty;
        public int DuracionDias { get; set; }
        public string Indicaciones { get; set; } = string.Empty;

        public int RecetaMedicaId { get; set; }
        public RecetaMedica? RecetaMedica { get; set; }

        public int MedicamentoId { get; set; }
        public Medicamento? Medicamento { get; set; }
    }
}
