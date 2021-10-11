using System;

namespace TestApi.Core.Services
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public string Error { get; set; }
        public bool Success { get; set; }
        public Exception Exception { get; set; }
        public ServiceResponse<T> Failed(string message,Exception e=null)
        {
            Success = false;
            Error = message;
            Exception = e;
            return this;
        }

        public ServiceResponse<T> Successful(T data=default(T))
        {
            Success = true;
            Data = data;
            return this;
        }

        public static implicit operator bool(ServiceResponse<T> response) => response.Success;
    }
    
}