namespace ShelterCare.Application;

public class Response<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; } = new();
    public string ErrorCode { get; set; }

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
        Errors.Add(error);
        ErrorCode = errorCode;
    }

    private Response(List<string> errors, string errorCode) : this()
    {
        Success = false;
        Errors.AddRange(errors);
        ErrorCode = errorCode;
    }

    public static Response<T> SuccessResult(T data)
    {
        return new Response<T>(data);
    }

    public static Response<T> ErrorResult(string errorCode, string errorMessage)
    {
        return new Response<T>(errorMessage, errorCode);
    }

    public static Response<T> ErrorResult(string errorCode, List<string> errorMessages)
    {
        return new Response<T>(errorMessages, errorCode);
    }
}