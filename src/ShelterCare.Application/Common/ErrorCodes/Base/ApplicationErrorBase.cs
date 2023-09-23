namespace ShelterCare.Application.Common.ErrorCodes.Base;

public interface IApplicationErrorBase
{
    public static string Code { get; }

    public static string Message { get; }
}
