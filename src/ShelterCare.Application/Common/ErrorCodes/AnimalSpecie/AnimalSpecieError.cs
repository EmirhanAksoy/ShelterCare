namespace ShelterCare.Application;

public class GetAllAnimalSpeciesQueryFailed
{
    public const int EventId = 2000;
    public const string Code = "ANIMAL-SPECIE-QUERY-ALL-FAILED";
    public const string Message = "Error occurred while fetching animal species";
}

public class GetAnimalSpecieByIdQueryFailed
{
    public const int EventId = 2001;
    public const string Code = "ANIMAL-SPECIE-QUERY-BY-ID-FAILED";
    public const string Message = "Error occurred while retrieving animal specie by id";
}

public class CreateAnimalSpecieCommandFailed
{
    public const int EventId = 2002;
    public const string Code = "ANIMAL-SPECIE-CREATE-FAILED";
    public const string Message = "Error occurred while creating animal specie";
}

public class DeleteAnimalSpecieByIdCommandFailed
{
    public const int EventId = 2003;
    public const string Code = "ANIMAL-SPECIE-DELETE-BY-ID-FAIL";
    public const string Message = "Error occurred while deleting animal specie by id";
}

public class AnimalSpecieNotFound
{
    public const int EventId = 2004;
    public const string Code = "ANIMAL-SPECIE-NOT-FOUND";
    public const string Message = "Animal specie is not found";
}

public class AnimalSpecieNameAlreadyExists
{
    public const int EventId = 2006;
    public const string Code = "ANIMAL-SPECIE-NAME-ALREADY-EXISTS";
    public const string Message = "Animal specie name is already exists";
}