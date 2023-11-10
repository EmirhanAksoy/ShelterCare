using ShelterCare.Core.Abstractions.Repository;
using ShelterCare.Core.Domain;
using ShelterCare.Infrastructure.Repository.Queries;
using Dapper;
using System.Data;

namespace ShelterCare.Infrastructure.Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly IDbConnection _dbConnection;

        public AnimalRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Animal> Create(Animal entity)
        {
            return await _dbConnection.QuerySingleAsync<Animal>(SqlQueries.AnimalRepositoryQueries.Create, new
            {
                entity.ShelterId,
                entity.OwnerId,
                entity.Name,
                entity.UniqueIdentifier,
                entity.DateOfBirth,
                entity.JoiningDate,
                entity.IsNeutered,
                entity.IsDisabled,
                IsActive = true,
                CreateDate = DateTime.UtcNow,
                CreateUserId = Guid.NewGuid()
            });
        }

        public async Task<bool> Delete(Guid id)
        {
            int affectedRows = await _dbConnection.ExecuteAsync(SqlQueries.AnimalRepositoryQueries.Delete, new { id });
            return affectedRows > 0;
        }

        public async Task<Animal> Get(Guid id)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<Animal>(SqlQueries.AnimalRepositoryQueries.Get, new { id });
        }

        public async Task<List<Animal>> GetAll()
        {
            IEnumerable<Animal> animals = await _dbConnection.QueryAsync<Animal>(SqlQueries.AnimalRepositoryQueries.GetAll);
            return animals.ToList();
        }

        public async Task<Animal> Update(Animal entity)
        {
            return await _dbConnection.QuerySingleAsync<Animal>(SqlQueries.AnimalRepositoryQueries.Update, new
            {
                entity.Id,
                entity.ShelterId,
                entity.OwnerId,
                entity.Name,
                entity.UniqueIdentifier,
                entity.DateOfBirth,
                entity.JoiningDate,
                entity.IsNeutered,
                entity.IsDisabled,
                IsActive = true,
                UpdateDate = DateTime.UtcNow,
                UpdateUserId = Guid.NewGuid()
            });
        }

        public async Task<bool> CheckIfAnimalNameExists(string animalName)
        {
            IEnumerable<int> response = await _dbConnection.QueryAsync<int>(SqlQueries.AnimalRepositoryQueries.CheckIfAnimalNameExists, new { Name = animalName });
            return response?.Count() > 0 && response.FirstOrDefault() == 1;
        }
    }
}
