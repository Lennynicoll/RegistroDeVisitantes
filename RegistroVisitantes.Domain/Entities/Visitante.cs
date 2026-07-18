using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class Visitante : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        public List<Visita> Visitas { get; set; } = new List<Visita>();
    }
}
