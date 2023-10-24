using Dapper;
using ShelterCare.Core.Abstractions.Repository;
using ShelterCare.Core.Domain;
using ShelterCare.Infrastructure.Repository.Queries;
using System.Data;

namespace ShelterCare.Infrastructure.Repository;

public class AnimalSpecieRepository : IAnimalSpecieRepository
{
    private readonly IDbConnection _dbConnection;

    public AnimalSpecieRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<AnimalSpecie> Create(AnimalSpecie entity)
    {
        string createQuery = SqlQueries.AnimalSpecieRepositoryQueries.Create
            .Replace($"@{nameof(AnimalSpecie.Name)}", entity.Name)
            .Replace($"@{nameof(AnimalSpecie.IsActive)}", bool.TrueString)
            .Replace($"@{nameof(AnimalSpecie.CreateDate)}", DateTime.UtcNow.ToString())
            .Replace($"@{nameof(AnimalSpecie.CreateUserId)}", Guid.NewGuid().ToString());
        return await _dbConnection.QuerySingleAsync<AnimalSpecie>(createQuery);
    }

    public async Task<bool> Delete(Guid id)
    {
        int effectedRows = await _dbConnection.ExecuteAsync(SqlQueries.AnimalSpecieRepositoryQueries.Delete.Replace($"@{nameof(AnimalSpecie.Id).ToLower()}", id.ToString()));
        return effectedRows > 0;
    }

    public async Task<AnimalSpecie> Get(Guid id)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<AnimalSpecie>(SqlQueries.AnimalSpecieRepositoryQueries.Get.Replace($"@{nameof(AnimalSpecie.Id).ToLower()}", id.ToString()));
    }

    public async Task<List<AnimalSpecie>> GetAll()
    {
        IEnumerable<AnimalSpecie> animalSpecies = await _dbConnection.QueryAsync<AnimalSpecie>(SqlQueries.AnimalSpecieRepositoryQueries.GetAll);
        return animalSpecies.ToList();
    }

    public async Task<AnimalSpecie> Update(AnimalSpecie entity)
    {
        string updateQuery = SqlQueries.AnimalSpecieRepositoryQueries.Update
            .Replace($"@{nameof(AnimalSpecie.Id).ToLower()}", entity.Id.ToString())
            .Replace($"@{nameof(AnimalSpecie.Name)}", entity.Name)
            .Replace($"@{nameof(AnimalSpecie.UpdateDate)}", DateTime.UtcNow.ToString())
            .Replace($"@{nameof(AnimalSpecie.UpdateUserId)}", Guid.NewGuid().ToString());
        return await _dbConnection.QuerySingleAsync<AnimalSpecie>(updateQuery);
    }

    public async Task<bool> CheckIfAnimalSpecieNameExists(string animaSpecieName)
    {
        string query = SqlQueries.AnimalSpecieRepositoryQueries.CheckIfAnimalSpecieNameExists.Replace($"@{nameof(AnimalSpecie.Name)}", animaSpecieName);
        IEnumerable<int> response = await _dbConnection.QueryAsync<int>(query);
        return response?.Count() > 0 && response.FirstOrDefault() == 1;
    }
}
