namespace ShelterCare.Application;
public class GetAllAnimalOwnersQueryFailed
{
    public const int EventId = 3000;
    public const string Code = "ANIMAL-OWNER-QUERY-ALL-FAILED";
    public const string Message = "Error occurred while fetching animal owners";
}

public class GetAnimalOwnerByIdQueryFailed
{
    public const int EventId = 3001;
    public const string Code = "ANIMAL-OWNER-QUERY-BY-ID-FAILED";
    public const string Message = "Error occurred while retrieving animal owner by id";
}

public class CreateAnimalOwnerCommandFailed
{
    public const int EventId = 3002;
    public const string Code = "ANIMAL-OWNER-CREATE-FAILED";
    public const string Message = "Error occurred while creating animal owner";
}

public class UpdateAnimalOwnerCommandFailed
{
    public const int EventId = 3002;
    public const string Code = "ANIMAL-OWNER-UPDATE-FAILED";
    public const string Message = "Error occurred while updating animal owner";
}

public class DeleteAnimalOwnerByIdCommandFailed
{
    public const int EventId = 3003;
    public const string Code = "ANIMAL-OWNER-DELETE-BY-ID-FAIL";
    public const string Message = "Error occurred while deleting animal owner by id";
}

public class AnimalOwnerNotFound
{
    public const int EventId = 3004;
    public const string Code = "ANIMAL-OWNER-NOT-FOUND";
    public const string Message = "Animal owner is not found";
}

public class AnimalOwnerNationalIdAlreadyExists
{
    public const int EventId = 3006;
    public const string Code = "ANIMAL-OWNER-NATIONAL-ID-ALREADY-EXISTS";
    public const string Message = "Animal owner national id is already exists";
}

public class AnimalOwnerConfirmationFailed
{
    public const int EventId = 3007;
    public const string Code = "ANIMAL-OWNER-NATIONAL-ID-NOT-EXISTS";
    public const string Message = "Animal owner with given national id not exists";
}