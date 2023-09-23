﻿using ShelterCare.Core.Domain;

namespace ShelterCare.Infrastructure.Repository.Queries;

public static class SqlQueries
{
    public static class ShelterRepositoryQueries
    {
        public const string GetAll = "SELECT * FROM Shelters";
        public const string Get = "SELECT * FROM Shelters WHERE id = '@id'";
        public const string Delete = "DELETE Shelters WHERE id = '@id'";
        public const string Update = $"""
            UPDATE Shelters
            SET
            {nameof(Shelter.Name)} = '@Name',
            {nameof(Shelter.OwnerFullName)} = '@OwnerFullName',
            {nameof(Shelter.Address)} = '@Address',
            {nameof(Shelter.FoundationDate)} = '@FoundationDate',
            {nameof(Shelter.UpdateDate)} = '@UpdateDate',
            {nameof(Shelter.UpdateUserId)} = '@UpdateUserId',
            {nameof(Shelter.Website)} = '@Website'
            WHERE id = '@id'
            """;
        public const string Create = $"""
            INSERT INTO Shelters
            {nameof(Shelter.Name)},
            {nameof(Shelter.OwnerFullName)},
            {nameof(Shelter.Address)},
            {nameof(Shelter.FoundationDate)},
            {nameof(Shelter.CreateDate)},
            {nameof(Shelter.CreateUserId)},
            {nameof(Shelter.Website)}
            VALUES
            (
             '@Name',
             '@OwnerFullName',
             '@Address',
             '@FoundationDate',
             '@CreateDate',
             '@CreateUserId',
             '@Website'
            )
            """;
    }
}