using System;

namespace RegistroVisitantes.DTOs
{
    public class VisitaDTO
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public int VisitanteId { get; set; }
    }
}
