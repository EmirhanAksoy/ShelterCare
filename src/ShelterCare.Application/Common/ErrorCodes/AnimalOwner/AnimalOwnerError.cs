using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelterCare.Application;
public class GetAllAnimalOwnersQueryFailed
{
    public const int EventId = 2000;
    public const string Code = "ANIMAL-OWNER-QUERY-ALL-FAILED";
    public const string Message = "Error occured while fetching animal owners";
}

public class GetAnimalOwnerByIdQueryFailed
{
    public const int EventId = 2001;
    public const string Code = "ANIMAL-OWNER-QUERY-BY-ID-FAILED";
    public const string Message = "Error occured while retrieving animal owner by id";
}

public class CreateAnimalOwnerCommandFailed
{
    public const int EventId = 2002;
    public const string Code = "ANIMAL-OWNER-CREATE-FAILED";
    public const string Message = "Error occured while creating animal owner";
}

public class UpdateAnimalOwnerCommandFailed
{
    public const int EventId = 2002;
    public const string Code = "ANIMAL-OWNER-UPDATE-FAILED";
    public const string Message = "Error occured while updating animal owner";
}

public class DeleteAnimalOwnerByIdCommandFailed
{
    public const int EventId = 2003;
    public const string Code = "ANIMAL-OWNER-DELETE-BY-ID-FAIL";
    public const string Message = "Error occured while deleting animal owner by id";
}

public class AnimalOwnerNotFound
{
    public const int EventId = 2004;
    public const string Code = "ANIMAL-OWNER-NOT-FOUND";
    public const string Message = "Animal owner is not found";
}

public class AnimalOwnerNationalIdAlreadyExists
{
    public const int EventId = 2006;
    public const string Code = "ANIMAL-OWNER-NATIONAL-ID-ALREADY-EXISTS";
    public const string Message = "Animal owner national id is already exists";
}