using System.Text.Json.Serialization;

namespace ShelterCare.Application;

public class Response<T>
{
    public bool Success { get; set; } 
    public T Data { get; set; } 
    public string ErrorCode { get; set; } 
    public List<string> Errors { get; set; }

    [JsonConstructor]
    public Response()
    {
        Errors ??= new ();
    }
    public Response<T> SuccessResult(T data)
    {
        Success = true;
        Data = data;
        ErrorCode = string.Empty;
        Errors = new ();
        return this;
    }

    public Response<T> ErrorResult(string errorCode, string errorMessage)
    {
        Success = false;
        Errors = new()
        {
            errorMessage
        };
        ErrorCode = errorCode;
        return this;
    }

    public  Response<T> ErrorResult(string errorCode, List<string> errorMessages)
    {
        Success = false;
        Errors = new();
        Errors.AddRange(errorMessages);
        ErrorCode = errorCode;
        return this;
    }
}