namespace RegistroVisitantes.Application.Core
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public static ServiceResult Ok(object? data = null, string message = "Operación exitosa")
        {
            return new ServiceResult { Success = true, Message = message, Data = data };
        }

        public static ServiceResult Fail(string message)
        {
            return new ServiceResult { Success = false, Message = message };
        }

        public static ServiceResult Fail(List<string> errors)
        {
            return new ServiceResult { Success = false, Errors = errors };
        }
    }
}
