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

        return await _dbConnection.QuerySingleAsync<AnimalSpecie>(SqlQueries.AnimalSpecieRepositoryQueries.Create, new
        {
            entity.Name,
            IsActive = true,
            CreateDate = DateTime.UtcNow,
            CreateUserId = Guid.NewGuid()
        });
    }

    public async Task<bool> Delete(Guid id)
    {
        int effectedRows = await _dbConnection.ExecuteAsync(SqlQueries.AnimalSpecieRepositoryQueries.Delete, new { id });
        return effectedRows > 0;
    }

    public async Task<AnimalSpecie> Get(Guid id)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<AnimalSpecie>(SqlQueries.AnimalSpecieRepositoryQueries.Get, new { id });
    }

    public async Task<List<AnimalSpecie>> GetAll()
    {
        IEnumerable<AnimalSpecie> animalSpecies = await _dbConnection.QueryAsync<AnimalSpecie>(SqlQueries.AnimalSpecieRepositoryQueries.GetAll);
        return animalSpecies.ToList();
    }

    public async Task<AnimalSpecie> Update(AnimalSpecie entity)
    {
        return await _dbConnection.QuerySingleAsync<AnimalSpecie>(SqlQueries.AnimalSpecieRepositoryQueries.Update, new
        {
            entity.Id,
            entity.Name,
            IsActive = true,
            UpdateDate = DateTime.UtcNow,
            UpdateUserId = Guid.NewGuid()
        });
    }

    public async Task<bool> CheckIfAnimalSpecieNameExists(string animaSpecieName)
    {
        IEnumerable<int> response = await _dbConnection.QueryAsync<int>(SqlQueries.AnimalSpecieRepositoryQueries.CheckIfAnimalSpecieNameExists, new { Name = animaSpecieName });
        return response?.Count() > 0 && response.FirstOrDefault() == 1;
    }
}
