using ShelterCare.Core.Domain;

namespace ShelterCare.Infrastructure.Repository.Queries;

public static class SqlQueries
{
    public static class ShelterRepositoryQueries
    {
        public const string GetAll = "SELECT * FROM Shelters";
        public const string Get = "SELECT * FROM Shelters WHERE id = @id";
        public const string Delete = "DELETE FROM Shelters WHERE id = @id";
        public const string Update = $"""
            UPDATE Shelters
            SET
            {nameof(Shelter.Name)} = @Name,
            {nameof(Shelter.OwnerFullName)} = @OwnerFullName,
            {nameof(Shelter.Address)} = @Address,
            {nameof(Shelter.FoundationDate)} = @FoundationDate,
            {nameof(Shelter.UpdateDate)} = @UpdateDate,
            {nameof(Shelter.UpdateUserId)} = @UpdateUserId,
            {nameof(Shelter.Website)} = @Website,
            {nameof(Shelter.TotalAreaInSquareMeters)} = @TotalAreaInSquareMeters
            WHERE id = @id

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
             @Name,
             @IsActive,
             @OwnerFullName,
             @Address,
             @FoundationDate,
             @CreateDate,
             @CreateUserId,
             @Website,
             @TotalAreaInSquareMeters
            )

            RETURNING *
            """;
        public const string CheckIfShelterNameExists = "SELECT 1 FROM Shelters WHERE UPPER(name)=UPPER(@Name)";
    }

    public static class AnimalSpecieRepositoryQueries
    {
        public const string GetAll = "SELECT * FROM AnimalSpecies";
        public const string Get = "SELECT * FROM AnimalSpecies WHERE id = @id";
        public const string Delete = "DELETE FROM AnimalSpecies WHERE id = @id";
        public const string Update = $"""
            UPDATE AnimalSpecies
            SET
            {nameof(AnimalSpecie.Name)} = @Name,
            {nameof(AnimalSpecie.UpdateDate)} = @UpdateDate,
            {nameof(AnimalSpecie.UpdateUserId)} = @UpdateUserId
            WHERE id = @id

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
             @Name,
             @IsActive,
             @CreateDate,
             @CreateUserId
            )

            RETURNING *
            """;
        public const string CheckIfAnimalSpecieNameExists = "SELECT 1 FROM AnimalSpecies WHERE  UPPER(name)=UPPER(@Name)";
    }

    public static class AnimalOwnerRepositoryQueries
    {
        public const string GetAll = "SELECT * FROM AnimalOwners";
        public const string Get = "SELECT * FROM AnimalOwners WHERE id = @id";
        public const string Delete = "DELETE FROM AnimalOwners WHERE id = @id";
        public const string Update = $"""
            UPDATE AnimalOwners
            SET
            {nameof(AnimalOwner.Fullname)} = @Fullname,
            {nameof(AnimalOwner.PhoneNumber)} = @PhoneNumber,
            {nameof(AnimalOwner.EmailAddress)} = @EmailAddress,
            {nameof(AnimalOwner.NationalId)} = @NationalId,
            {nameof(AnimalOwner.UpdateDate)} = @UpdateDate,
            {nameof(AnimalOwner.UpdateUserId)} = @UpdateUserId
            WHERE id = @id

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
             @Fullname,
             @PhoneNumber,
             @EmailAddress,
             @NationalId,
             @IsActive,
             @CreateDate,
             @CreateUserId
            )

            RETURNING *
            """;
        public const string CheckIfAnimalOwnerExists = "SELECT 1 FROM AnimalOwners WHERE  UPPER(nationalId)=UPPER(@NationalId)";
    }

    public static class AnimalRepositoryQueries
    {
        public const string GetAll = "SELECT * FROM Animal";
        public const string Get = "SELECT * FROM Animal WHERE id = @id";
        public const string Delete = "DELETE FROM Animal WHERE id = @id";
        public const string Update = $@"
        UPDATE Animal
        SET
        {nameof(Animal.ShelterId)} = @ShelterId,
        {nameof(Animal.OwnerId)} = @OwnerId,
        {nameof(Animal.Name)} = @Name,
        {nameof(Animal.UniqueIdentifier)} = @UniqueIdentifier,
        {nameof(Animal.DateOfBirth)} = @DateOfBirth,
        {nameof(Animal.JoiningDate)} = @JoiningDate,
        {nameof(Animal.IsNeutered)} = @IsNeutered,
        {nameof(Animal.IsDisabled)} = @IsDisabled,
        {nameof(Animal.IsActive)} = @IsActive,
        {nameof(Animal.UpdateDate)} = @UpdateDate,
        {nameof(Animal.UpdateUserId)} = @UpdateUserId
        WHERE id = @id
        RETURNING *
        ";

        public const string Create = $@"
        INSERT INTO Animal
        (
        {nameof(Animal.ShelterId)},
        {nameof(Animal.OwnerId)},
        {nameof(Animal.Name)},
        {nameof(Animal.UniqueIdentifier)},
        {nameof(Animal.DateOfBirth)},
        {nameof(Animal.JoiningDate)},
        {nameof(Animal.IsNeutered)},
        {nameof(Animal.IsDisabled)},
        {nameof(Animal.IsActive)},
        {nameof(Animal.CreateDate)},
        {nameof(Animal.CreateUserId)}
        )
        VALUES
        (
        @ShelterId,
        @OwnerId,
        @Name,
        @UniqueIdentifier,
        @DateOfBirth,
        @JoiningDate,
        @IsNeutered,
        @IsDisabled,
        @IsActive,
        @CreateDate,
        @CreateUserId
        )
        RETURNING *
        ";

        public const string CheckIfAnimalNameExists = "SELECT 1 FROM Animal WHERE UPPER(name) = UPPER(@Name)";
    }
}
