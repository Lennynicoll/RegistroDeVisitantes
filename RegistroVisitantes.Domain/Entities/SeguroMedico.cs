using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class SeguroMedico : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Cobertura { get; set; } = string.Empty;

        public List<PacienteSeguro> PacienteSeguros { get; set; } = new List<PacienteSeguro>();
    }
}
