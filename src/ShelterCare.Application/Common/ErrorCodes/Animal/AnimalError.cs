namespace ShelterCare.Application;
public class GetAllAnimalQueryFailed
{
    public const int EventId = 4000;
    public const string Code = "ANIMAL-QUERY-ALL-FAILED";
    public const string Message = "Error occurred while fetching animals";
}

public class GetAnimalByIdQueryFailed
{
    public const int EventId = 4001;
    public const string Code = "ANIMAL-QUERY-BY-ID-FAILED";
    public const string Message = "Error occurred while retrieving animal by id";
}

public class CreateAnimalCommandFailed
{
    public const int EventId = 4002;
    public const string Code = "ANIMAL-CREATE-FAILED";
    public const string Message = "Error occurred while creating animal";
}

public class UpdateAnimalCommandFailed
{
    public const int EventId = 4002;
    public const string Code = "ANIMAL-UPDATE-FAILED";
    public const string Message = "Error occurred while updating animal";
}

public class DeleteAnimalByIdCommandFailed
{
    public const int EventId = 4003;
    public const string Code = "ANIMAL-DELETE-BY-ID-FAIL";
    public const string Message = "Error occurred while deleting animal by id";
}

public class AnimalNotFound
{
    public const int EventId = 4004;
    public const string Code = "ANIMAL-NOT-FOUND";
    public const string Message = "Animal is not found";
}

public class AnimalUniqueIdAlreadyExists
{
    public const int EventId = 4006;
    public const string Code = "ANIMAL-UNIQUE-ID-ALREADY-EXISTS";
    public const string Message = "Animal unique id is already exists";
}

public class AnimalConfirmationFailed
{
    public const int EventId = 4007;
    public const string Code = "ANIMAL-UNIQUE-ID-NOT-EXISTS";
    public const string Message = "Animal with given unique id not exists";
}

public class OwnerOfAnimalConfirmationFailed
{
    public const int EventId = 4008;
    public const string Code = "ANIMAL-OWNER-NOT-MATHCHING";
    public const string Message = "Animal owner is not matching ";
}