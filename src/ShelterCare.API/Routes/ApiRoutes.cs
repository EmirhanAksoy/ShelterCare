namespace ShelterCare.API.Routes;

public static class ShelterRoutes
{
    public const string GetAll = "/shelter";
    public const string Get = "/shelter/{id}";
    public const string Create = "/shelter";
    public const string Update = "/shelter";
    public const string Delete = "/shelter/{id}";
}

public static class AreaRoutes
{
    public const string GetAll = "/area";
    public const string Get = "/area/{id}";
    public const string GetShelter = "/area/shelter";
    public const string Create = "/area";
    public const string Update = "/area";
    public const string Delete = "/area/{id}";
}

public static class AnimalSpecieRoutes
{
    public const string GetAll = "/animal/specie";
    public const string Get = "/animal/specie/{id}";
    public const string Create = "/animal/specie";
    public const string Update = "/animal/specie";
    public const string Delete = "/animal/specie/{id}";
}

public static class AnimalOwnerRoutes
{
    public const string GetAll = "/animal/owner";
    public const string Get = "/animal/owner/{id}";
    public const string Create = "/animal/owner";
    public const string Update = "/animal/owner";
    public const string Delete = "/animal/owner/{id}";
    public const string GetAnimals = "/animal/owner/animals";
}

public static class AnimalRoutes
{
    public const string GetAll = "/animal";
    public const string Get = "/animal/{id}";
    public const string Create = "/animal";
    public const string Update = "/animal";
    public const string Delete = "/animal/{id}";
    public const string GetShelter = "/animal/shelter";
    public const string GetOwner = "/animal/owner";
    public const string GetArea = "/animal/area";
}

public static class EmployeeRoutes
{
    public const string GetAll = "/employee";
    public const string Get = "/employee/{id}";
    public const string Create = "/employee";
    public const string Update = "/employee";
    public const string Delete = "/employee/{id}";
    public const string GetShelter = "/employee/shelter";
}