using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ShelterCare.Application;

namespace ShelterCare.API.Middlewares;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JsonSerializerOptions serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        var originalStream = context.Response.Body;
        var responseStream = new MemoryStream();
        context.Response.Body = responseStream;

        await _next(context);

        if (context.Response.StatusCode == StatusCodes.Status400BadRequest &&
            context.Response.ContentType?.Contains("application/problem+json", StringComparison.OrdinalIgnoreCase) == true)
        {
            responseStream.Seek(0, SeekOrigin.Begin);
            using var stream = new StreamReader(responseStream);
            var responseText = await stream.ReadToEndAsync();
            var validationProblemDetails = JsonSerializer.Deserialize<ValidationProblemDetails>(responseText, serializerOptions);

            var response = ConvertValidationProblemDetailsToApiResponse(validationProblemDetails);
            var json = JsonSerializer.Serialize(response, serializerOptions);
            context.Response.Body = originalStream;
            await context.Response.WriteAsync(json);
            return;
        }

        await SetOriginalBodyBack(context, originalStream, responseStream);
    }

    private static async Task SetOriginalBodyBack(HttpContext context, Stream originalStream, MemoryStream responseStream)
    {
        responseStream.Seek(0, SeekOrigin.Begin);
        await responseStream.CopyToAsync(originalStream);
        context.Response.Body = originalStream;
    }

    private static Response<object> ConvertValidationProblemDetailsToApiResponse(ValidationProblemDetails? validationProblemDetails)
    {
        if (validationProblemDetails is null)
        {
            return Response<object>.ErrorResult(ValidationError.Code, ValidationError.Message);
        }
        List<string> errorMessages = validationProblemDetails.Errors.Select(keyValuePair => keyValuePair.Value[0]).ToList();
        return Response<object>.ErrorResult(ValidationError.Code, errorMessages);
    }
}
