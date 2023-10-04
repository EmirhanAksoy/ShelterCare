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

public class GetShelterByIdQueryFailed
{
    public const int EventId = 1001;
    public const string Code = "SHELTER-QUERY-BY-ID-FAIL";
    public const string Message = "Error occured while retrieving shelter by id";
}

public class CreateShelterCommandFailed
{
    public const int EventId = 1002;
    public const string Code = "SHELTER-COMMAND-CREATE-FAILED";
    public const string Message = "Error occured while creating shelter";
}

public class DeletehelterByIdCommandFailed
{
    public const int EventId = 1003;
    public const string Code = "SHELTER-DELETE-BY-ID-FAIL";
    public const string Message = "Error occured while deleting shelter by id";
}

public class ShelterNotFound
{
    public const int EventId = 1004;
    public const string Code = "SHELTER-NOT-FOUND";
    public const string Message = "Shelter is not found";
}



