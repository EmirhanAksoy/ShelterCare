using ShelterCare.Core.Domain;

namespace ShelterCare.Infrastructure.Repository.Queries;

public static class SqlQueries
{
    public static class ShelterRepositoryQueries
    {
        public const string GetAll = "SELECT * FROM Shelters";
        public const string Get = "SELECT * FROM Shelters WHERE id = '@id'";
        public const string Delete = "DELETE FROM Shelters WHERE id = '@id'";
        public const string Update = $"""
            UPDATE Shelters
            SET
            {nameof(Shelter.Name)} = '@Name',
            {nameof(Shelter.OwnerFullName)} = '@OwnerFullName',
            {nameof(Shelter.Address)} = '@Address',
            {nameof(Shelter.FoundationDate)} = '@FoundationDate',
            {nameof(Shelter.UpdateDate)} = '@UpdateDate',
            {nameof(Shelter.UpdateUserId)} = '@UpdateUserId',
            {nameof(Shelter.Website)} = '@Website',
            {nameof(Shelter.TotalAreaInSquareMeters)} = '@TotalAreaInSquareMeters'
            WHERE id = '@id'

            RETURNING *
            """;
        public const string Create = $"""
            INSERT INTO Shelters
            (
            {nameof(Shelter.Name)},
            {nameof(Shelter.IsActive)},
            {nameof(Shelter.OwnerFullName)},
            {nameof(Shelter.Address)},
            {nameof(Shelter.FoundationDate)},
            {nameof(Shelter.CreateDate)},
            {nameof(Shelter.CreateUserId)},
            {nameof(Shelter.Website)},
            {nameof(Shelter.TotalAreaInSquareMeters)}
            )
            VALUES
            (
             '@Name',
             '@IsActive',
             '@OwnerFullName',
             '@Address',
             '@FoundationDate',
             '@CreateDate',
             '@CreateUserId',
             '@Website',
             '@TotalAreaInSquareMeters'
            )

            RETURNING *
            """;
        public const string CheckIfShelterNameExists = "SELECT 1 FROM Shelters WHERE UPPER(name)=UPPER('@Name')";
    }

    public static class AnimalSpecieRepositoryQueries
    {
        public const string GetAll = "SELECT * FROM AnimalSpecies";
        public const string Get = "SELECT * FROM AnimalSpecies WHERE id = '@id'";
        public const string Delete = "DELETE FROM AnimalSpecies WHERE id = '@id'";
        public const string Update = $"""
            UPDATE AnimalSpecies
            SET
            {nameof(AnimalSpecie.Name)} = '@Name',
            {nameof(AnimalSpecie.UpdateDate)} = '@UpdateDate',
            {nameof(AnimalSpecie.UpdateUserId)} = '@UpdateUserId'
            WHERE id = '@id'

            RETURNING *
            """;
        public const string Create = $"""
            INSERT INTO AnimalSpecies
            (
            {nameof(AnimalSpecie.Name)},
            {nameof(AnimalSpecie.IsActive)},
            {nameof(AnimalSpecie.CreateDate)},
            {nameof(AnimalSpecie.CreateUserId)}
            )
            VALUES
            (
             '@Name',
             '@IsActive',
             '@CreateDate',
             '@CreateUserId'
            )

            RETURNING *
            """;
        public const string CheckIfAnimalSpecieNameExists = "SELECT 1 FROM AnimalSpecies WHERE  UPPER(name)=UPPER('@Name')";
    }

    public static class AnimalOwnerRepositoryQueries
    {
        public const string GetAll = "SELECT * FROM AnimalOwners";
        public const string Get = "SELECT * FROM AnimalOwners WHERE id = '@id'";
        public const string Delete = "DELETE FROM AnimalOwners WHERE id = '@id'";
        public const string Update = $"""
            UPDATE AnimalOwners
            SET
            {nameof(AnimalOwner.Fullname)} = '@Fullname',
            {nameof(AnimalOwner.PhoneNumber)} = '@PhoneNumber',
            {nameof(AnimalOwner.EmailAddress)} = '@EmailAddress',
            {nameof(AnimalOwner.NationalId)} = '@NationalId',
            {nameof(AnimalOwner.UpdateDate)} = '@UpdateDate',
            {nameof(AnimalOwner.UpdateUserId)} = '@UpdateUserId'
            WHERE id = '@id'

            RETURNING *
            """;
        public const string Create = $"""
            INSERT INTO AnimalOwners
            (
            {nameof(AnimalOwner.Fullname)},
            {nameof(AnimalOwner.PhoneNumber)},
            {nameof(AnimalOwner.EmailAddress)},
            {nameof(AnimalOwner.NationalId)},
            {nameof(AnimalOwner.IsActive)},
            {nameof(AnimalOwner.CreateDate)},
            {nameof(AnimalOwner.CreateUserId)}
            )
            VALUES
            (
             '@Fullname',
             '@PhoneNumber',
             '@EmailAddress',
             '@NationalId',
             '@IsActive',
             '@CreateDate',
             '@CreateUserId'
            )

            RETURNING *
            """;
        public const string CheckIfAnimalOwnerExists = "SELECT 1 FROM AnimalOwners WHERE  UPPER(nationalId)=UPPER('@NationalId')";
    }
}
