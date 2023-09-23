using ShelterCare.Application.Common.ErrorCodes.Base;

namespace ShelterCare.Application;

public class GetAllSheltersQueryFailed : IApplicationErrorBase
{
    public static string Code => "SHELTER-1";
    public static string Message => "Error occured while fetching shelters";
}