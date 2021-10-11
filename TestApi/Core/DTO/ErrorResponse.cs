namespace TestApi.DTO
{
    public enum  ErrorCodes
    {
         InternalServerError,
         ValidationFailed
    }
    public enum FieldErrorCodes{
        IsRequired
    }
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            
        }

        public ErrorResponse(string error,ErrorCodes code)
        {
            Error = new Error {Code = code.ToString("G"), Message = error};
        }
        public Error Error { get; set; }
        public FieldError[] FieldErrors { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }

    public class FieldError
    {
        public string Code { get; set; }
        public string Field { get; set; }
        public string Message { get; set; }
    }
}