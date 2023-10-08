using System.Text.Json.Serialization;

namespace ShelterCare.Application;

public class Response<T>
{
    public bool Success { get; private set; }
    public T Data { get; private set; }
    public IReadOnlyList<string> Errors => errors;
    public string ErrorCode { get; private set; }

    private readonly List<string> errors = new();

    [JsonConstructor]
    public Response()
    {
        Success = true;
    }

    private Response(T data) : this()
    {
        Data = data;
    }

    private Response(string error, string errorCode) : this()
    {
        Success = false;
        errors.Add(error);
        ErrorCode = errorCode;
    }

    private Response(List<string> errors, string errorCode) : this()
    {
        Success = false;
        this.errors.AddRange(errors);
        ErrorCode = errorCode;
    }

    public static Response<T> SuccessResult(T data)
    {
        return new Response<T>(data);
    }

    public static Response<T> ErrorResult(string errorCode,string errorMessage)
    {
        return new Response<T>(errorMessage, errorCode);
    }

    public static Response<T> ErrorResult(string errorCode,List<string> errorMessages)
    {
        return new Response<T>(errorMessages, errorCode);
    }
}