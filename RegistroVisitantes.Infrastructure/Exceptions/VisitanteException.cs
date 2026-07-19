namespace RegistroVisitantes.Infrastructure.Exceptions
{
    public class VisitanteException : Exception
    {
        public VisitanteException(string message) : base(message)
        {
        }

        public VisitanteException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public static VisitanteException NotFound(int id)
        {
            return new VisitanteException($"Visitante con ID {id} no encontrado.");
        }

        public static VisitanteException AlreadyExists(string cedula)
        {
            return new VisitanteException($"Ya existe un visitante con cédula {cedula}.");
        }
    }
}
