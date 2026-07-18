using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class Medico : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string HorarioAtencion { get; set; } = string.Empty;

        public int EspecialidadId { get; set; }
        public Especialidad? Especialidad { get; set; }

        public int DepartamentoId { get; set; }
        public Departamento? Departamento { get; set; }

        public List<Cita> Citas { get; set; } = new List<Cita>();
    }
}
