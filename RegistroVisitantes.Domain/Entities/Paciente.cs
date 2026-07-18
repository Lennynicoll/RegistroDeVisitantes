using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class Paciente : BaseEntity
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

        public List<Cita> Citas { get; set; } = new List<Cita>();
        public List<HistorialClinico> Historiales { get; set; } = new List<HistorialClinico>();
        public List<PacienteSeguro> PacienteSeguros { get; set; } = new List<PacienteSeguro>();
    }
}
