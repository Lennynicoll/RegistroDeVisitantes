using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class NotaVisita : BaseEntity
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public List<HistorialVisitas> Historiales { get; set; } = new List<HistorialVisitas>();
    }
}
