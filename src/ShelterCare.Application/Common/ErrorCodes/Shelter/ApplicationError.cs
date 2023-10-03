using ShelterCare.Application.Common.ErrorCodes.Base;

namespace ShelterCare.Application;

public class ValidationError
{
    public const int EventId = 100;
    public const string Code = "VALIDATION-ERROR";
    public const string Message = "Validation failed";
}


public class GetAllSheltersQueryFailed 
{
    public const int EventId = 1000;
    public const string Code = "SHELTER-QUERY-ALL-FAIL";
    public const string Message = "Error occured while fetching shelters";
}

public class CreateShelterCommandFailed
{
    public const int EventId = 1001;
    public const string Code = "SHELTER-COMMAND-CREATE-FAILED";
    public const string Message = "Error occured while creating shelter";
}

