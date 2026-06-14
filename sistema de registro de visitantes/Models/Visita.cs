using System;

namespace RegistroVisitantes.Models
{
    public class Visita
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Comentarios { get; set; } = string.Empty;

        public int VisitanteId { get; set; }
        public Visitante? Visitante { get; set; }
    }
}
