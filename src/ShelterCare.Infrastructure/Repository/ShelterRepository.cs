



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
        return await _dbConnection.QuerySingleAsync<Shelter>(SqlQueries.ShelterRepositoryQueries.Create,new
        {
            entity.Name, 
            entity.OwnerFullName, 
            entity.Address,
            entity.Website, 
            entity.FoundationDate, 
            entity.TotalAreaInSquareMeters,
            IsActive = true,
            CreateDate = DateTime.UtcNow,
            CreateUserId = Guid.NewGuid()
        });
    }

    public async Task<bool> Delete(Guid id)
    {
        int effectedRows = await _dbConnection.ExecuteAsync(SqlQueries.ShelterRepositoryQueries.Delete,new { id });
        return effectedRows > 0;
    }

    public async Task<Shelter> Get(Guid id)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Shelter>(SqlQueries.ShelterRepositoryQueries.Get,new { id });
    }

    public async Task<List<Shelter>> GetAll()
    {
        IEnumerable<Shelter> shelters = await _dbConnection.QueryAsync<Shelter>(SqlQueries.ShelterRepositoryQueries.GetAll);
        return shelters.ToList();
    }

    public async Task<Shelter> Update(Shelter entity)
    {
        return await _dbConnection.QuerySingleAsync<Shelter>(SqlQueries.ShelterRepositoryQueries.Update, new
        {
            entity.Id,
            entity.Name,
            entity.OwnerFullName,
            entity.Address,
            entity.Website,
            entity.FoundationDate,
            entity.TotalAreaInSquareMeters,
            IsActive = true,
            UpdateDate = DateTime.UtcNow,
            UpdateUserId = Guid.NewGuid()
        });
    }

    public async Task<bool> CheckIfShelterNameExists(string shelterName)
    {
        IEnumerable<int> response = await _dbConnection.QueryAsync<int>(SqlQueries.ShelterRepositoryQueries.CheckIfShelterNameExists,new { Name = shelterName });
        return response?.Count() > 0 && response.FirstOrDefault() == 1;
    }
}