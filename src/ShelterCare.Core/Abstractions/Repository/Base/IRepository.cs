using ShelterCare.Core.Domain.Base;

namespace ShelterCare.Core.Abstractions.Repository;

public interface IRepository<T> where T : class, IEntity
{
    Task<T> Get(string id);
    Task<T> Update(T entity);
    Task<bool> Delete(string id);
    Task<T> Create(T entity);
    Task<List<T>> GetAll();
}
