using RegistroVisitantes.Domain.Core;

namespace RegistroVisitantes.Domain.Entities
{
    public class SeguridadEdificio : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Cobertura { get; set; } = string.Empty;

        public List<VisitanteSeguro> VisitanteSeguros { get; set; } = new List<VisitanteSeguro>();
    }
}
