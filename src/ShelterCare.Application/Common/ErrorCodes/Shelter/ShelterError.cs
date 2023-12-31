﻿namespace ShelterCare.Application;


public class GetAllSheltersQueryFailed
{
    public const int EventId = 1000;
    public const string Code = "SHELTER-QUERY-ALL-FAILED";
    public const string Message = "Error occurred while fetching shelters";
}

public class GetShelterByIdQueryFailed
{
    public const int EventId = 1001;
    public const string Code = "SHELTER-QUERY-BY-ID-FAILED";
    public const string Message = "Error occurred while retrieving shelter by id";
}

public class CreateShelterCommandFailed
{
    public const int EventId = 1002;
    public const string Code = "SHELTER-CREATE-FAILED";
    public const string Message = "Error occurred while creating shelter";
}

public class DeleteShelterByIdCommandFailed
{
    public const int EventId = 1003;
    public const string Code = "SHELTER-DELETE-BY-ID-FAIL";
    public const string Message = "Error occurred while deleting shelter by id";
}

public class ShelterNotFound
{
    public const int EventId = 1004;
    public const string Code = "SHELTER-NOT-FOUND";
    public const string Message = "Shelter is not found";
}

public class UpdateShelterCommandFailed
{
    public const int EventId = 1005;
    public const string Code = "SHELTER-UPDATE-FAILED";
    public const string Message = "Error occurred while updating shelter";
}

public class ShelterNameAlreadyExists
{
    public const int EventId = 1006;
    public const string Code = "SHELTER-NAME-ALREADY-EXISTS";
    public const string Message = "Shelter name is already exists";
}



