using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class Anfitrion : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string HorarioAtencion { get; set; } = string.Empty;

        public int MotivoVisitaId { get; set; }
        public MotivoVisita? MotivoVisita { get; set; }

        public int DepartamentoId { get; set; }
        public Departamento? Departamento { get; set; }

        public List<RegistroVisita> RegistroVisitas { get; set; } = new List<RegistroVisita>();
    }
}
