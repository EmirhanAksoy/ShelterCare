



using Dapper;
using ShelterCare.Core.Abstractions.Repository;
using ShelterCare.Core.Domain;
using ShelterCare.Infrastructure.Repository.Queries;
using System.Data;

namespace ShelterCare.Infrastructure.Repository;

public class ShelterRepository : IShelterRepository
{
    private readonly IDbConnection _dbConnection;

    public ShelterRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Shelter> Create(Shelter entity)
    {
        string createQuery = SqlQueries.ShelterRepositoryQueries.Create
            .Replace($"@{nameof(Shelter.Id).ToLower()}", entity.Id.ToString())
            .Replace($"@{nameof(Shelter.Name)}", entity.Name)
            .Replace($"@{nameof(Shelter.OwnerFullName)}", entity.OwnerFullName)
            .Replace($"@{nameof(Shelter.Address)}", entity.Address)
            .Replace($"@{nameof(Shelter.Website)}", entity.Website)
            .Replace($"@{nameof(Shelter.FoundationDate)}", entity.FoundationDate.ToShortDateString())
            .Replace($"@{nameof(Shelter.CreateDate)}", DateTime.UtcNow.ToString())
            .Replace($"@{nameof(Shelter.CreateUserId)}", Guid.NewGuid().ToString())
            .Replace($"@{nameof(Shelter.IsActive)}", bool.TrueString)
            .Replace($"@{nameof(Shelter.Website)}", entity.Website);
        return await _dbConnection.QuerySingleAsync<Shelter>(createQuery);
    }

    public async Task<bool> Delete(string id)
    {
        int effectedRows = await _dbConnection.ExecuteAsync(SqlQueries.ShelterRepositoryQueries.Get.Replace($"@{nameof(Shelter.Id).ToLower()}", id));
        return effectedRows > 0;
    }

    public async Task<Shelter> Get(string id)
    {
        return await _dbConnection.QueryFirstAsync<Shelter>(SqlQueries.ShelterRepositoryQueries.Get.Replace($"@{nameof(Shelter.Id).ToLower()}", id));
    }

    public async Task<List<Shelter>> GetAll()
    {
        IEnumerable<Shelter> shelters = await _dbConnection.QueryAsync<Shelter>(SqlQueries.ShelterRepositoryQueries.GetAll);
        return shelters.ToList();
    }

    public async Task<Shelter> Update(Shelter entity)
    {
        string updateQuery = SqlQueries.ShelterRepositoryQueries.Update
            .Replace($"@{nameof(Shelter.Id).ToLower()}", entity.Id.ToString())
            .Replace($"@{nameof(Shelter.Name)}", entity.Name)
            .Replace($"@{nameof(Shelter.OwnerFullName)}", entity.OwnerFullName)
            .Replace($"@{nameof(Shelter.Address)}", entity.Address)
            .Replace($"@{nameof(Shelter.Website)}", entity.Website)
            .Replace($"@{nameof(Shelter.UpdateDate)}", DateTime.UtcNow.ToString())
            .Replace($"@{nameof(Shelter.UpdateUserId)}", "admin")
            .Replace($"@{nameof(Shelter.Website)}", entity.Website);
        return await _dbConnection.QuerySingleAsync<Shelter>(updateQuery);
    }
}