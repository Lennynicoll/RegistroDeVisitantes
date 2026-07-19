using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class RegistroVisitante : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string TipoSangre { get; set; } = string.Empty;

        public List<RegistroVisita> RegistroVisitas { get; set; } = new List<RegistroVisita>();
        public List<HistorialVisitas> Historiales { get; set; } = new List<HistorialVisitas>();
        public List<VisitanteSeguro> VisitanteSeguros { get; set; } = new List<VisitanteSeguro>();
    }
}
