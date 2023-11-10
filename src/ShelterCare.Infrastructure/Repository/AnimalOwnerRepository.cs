using Dapper;
using ShelterCare.Core.Abstractions.Repository;
using ShelterCare.Core.Domain;
using ShelterCare.Infrastructure.Repository.Queries;
using System.Data;

namespace ShelterCare.Infrastructure.Repository;

public class AnimalOwnerRepository : IAnimalOwnerRepository
{
    private readonly IDbConnection _dbConnection;

    public AnimalOwnerRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<AnimalOwner> Create(AnimalOwner entity)
    {
        return await _dbConnection.QuerySingleAsync<AnimalOwner>(SqlQueries.AnimalOwnerRepositoryQueries.Create, new
        {
            entity.Fullname,
            entity.EmailAddress,
            entity.PhoneNumber,
            entity.NationalId,
            IsActive = true,
            CreateDate = DateTime.UtcNow,
            CreateUserId = Guid.NewGuid()
        });
    }

    public async Task<bool> Delete(Guid id)
    {
        int effectedRows = await _dbConnection.ExecuteAsync(SqlQueries.AnimalOwnerRepositoryQueries.Delete, new { id });
        return effectedRows > 0;
    }

    public async Task<AnimalOwner> Get(Guid id)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<AnimalOwner>(SqlQueries.AnimalOwnerRepositoryQueries.Get, new { id });
    }

    public async Task<List<AnimalOwner>> GetAll()
    {
        IEnumerable<AnimalOwner> animalOwners = await _dbConnection.QueryAsync<AnimalOwner>(SqlQueries.AnimalOwnerRepositoryQueries.GetAll);
        return animalOwners.ToList();
    }

    public async Task<AnimalOwner> Update(AnimalOwner entity)
    {
        return await _dbConnection.QuerySingleAsync<AnimalOwner>(SqlQueries.AnimalOwnerRepositoryQueries.Update, new
        {
            entity.Id,
            entity.Fullname,
            entity.EmailAddress,
            entity.PhoneNumber,
            entity.NationalId,
            IsActive = true,
            UpdateDate = DateTime.UtcNow,
            UpdateUserId = Guid.NewGuid()
        });
    }

    public async Task<bool> CheckIfAnimalOwnerExists(string nationalId)
    {
        string query = SqlQueries.AnimalOwnerRepositoryQueries.CheckIfAnimalOwnerExists.Replace($"@{nameof(AnimalOwner.NationalId)}", nationalId);
        IEnumerable<int> response = await _dbConnection.QueryAsync<int>(SqlQueries.AnimalOwnerRepositoryQueries.CheckIfAnimalOwnerExists, new
        {
            nationalId
        });
        return response?.Count() > 0 && response.FirstOrDefault() == 1;
    }
}

