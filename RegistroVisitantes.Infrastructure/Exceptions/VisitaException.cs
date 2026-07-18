namespace RegistroVisitantes.Infrastructure.Exceptions
{
    public class VisitaException : Exception
    {
        public VisitaException(string message) : base(message)
        {
        }

        public VisitaException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public static VisitaException NotFound(int id)
        {
            return new VisitaException($"Visita con ID {id} no encontrada.");
        }

        public static VisitaException VisitanteNotFound(int visitanteId)
        {
            return new VisitaException($"Visitante con ID {visitanteId} no existe.");
        }
    }
}
