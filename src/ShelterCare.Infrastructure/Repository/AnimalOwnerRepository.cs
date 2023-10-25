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
        string createQuery = SqlQueries.AnimalOwnerRepositoryQueries.Create
            .Replace($"@{nameof(AnimalOwner.Fullname)}", entity.Fullname)
            .Replace($"@{nameof(AnimalOwner.EmailAddress)}", entity.EmailAddress)
            .Replace($"@{nameof(AnimalOwner.PhoneNumber)}", entity.PhoneNumber)
            .Replace($"@{nameof(AnimalOwner.NationalId)}", entity.NationalId)
            .Replace($"@{nameof(AnimalOwner.IsActive)}", bool.TrueString)
            .Replace($"@{nameof(AnimalOwner.CreateDate)}", DateTime.UtcNow.ToString())
            .Replace($"@{nameof(AnimalOwner.CreateUserId)}", Guid.NewGuid().ToString());
        return await _dbConnection.QuerySingleAsync<AnimalOwner>(createQuery);
    }

    public async Task<bool> Delete(Guid id)
    {
        int effectedRows = await _dbConnection.ExecuteAsync(SqlQueries.AnimalOwnerRepositoryQueries.Delete.Replace($"@{nameof(AnimalOwner.Id).ToLower()}", id.ToString()));
        return effectedRows > 0;
    }

    public async Task<AnimalOwner> Get(Guid id)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<AnimalOwner>(SqlQueries.AnimalOwnerRepositoryQueries.Get.Replace($"@{nameof(AnimalOwner.Id).ToLower()}", id.ToString()));
    }

    public async Task<List<AnimalOwner>> GetAll()
    {
        IEnumerable<AnimalOwner> animalOwners = await _dbConnection.QueryAsync<AnimalOwner>(SqlQueries.AnimalOwnerRepositoryQueries.GetAll);
        return animalOwners.ToList();
    }

    public async Task<AnimalOwner> Update(AnimalOwner entity)
    {
        string updateQuery = SqlQueries.AnimalOwnerRepositoryQueries.Update
            .Replace($"@{nameof(AnimalOwner.Id).ToLower()}", entity.Id.ToString())
            .Replace($"@{nameof(AnimalOwner.Fullname)}", entity.Fullname)
            .Replace($"@{nameof(AnimalOwner.EmailAddress)}", entity.EmailAddress)
            .Replace($"@{nameof(AnimalOwner.PhoneNumber)}", entity.PhoneNumber)
            .Replace($"@{nameof(AnimalOwner.NationalId)}", entity.NationalId)
            .Replace($"@{nameof(AnimalOwner.IsActive)}", bool.TrueString)
            .Replace($"@{nameof(AnimalOwner.UpdateDate)}", DateTime.UtcNow.ToString())
            .Replace($"@{nameof(AnimalOwner.UpdateUserId)}", Guid.NewGuid().ToString());
        return await _dbConnection.QuerySingleAsync<AnimalOwner>(updateQuery);
    }

    public async Task<bool> CheckIfAnimalOwnerExists(string nationalId)
    {
        string query = SqlQueries.AnimalOwnerRepositoryQueries.CheckIfAnimalOwnerExists.Replace($"@{nameof(AnimalOwner.NationalId)}", nationalId);
        IEnumerable<int> response = await _dbConnection.QueryAsync<int>(query);
        return response?.Count() > 0 && response.FirstOrDefault() == 1;
    }
}

