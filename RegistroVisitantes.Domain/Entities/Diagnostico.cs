using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class Diagnostico : BaseEntity
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public List<HistorialClinico> Historiales { get; set; } = new List<HistorialClinico>();
    }
}
