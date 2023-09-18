using ShelterCare.Core.Domain.Base;

namespace ShelterCare.Core.Abstractions.Repository;

public interface IRepository<T> where T : class, IEntity
{
    T Get(string id);
    bool Update(T entity);
    bool Delete(string id);
    List<T> GetAll();
}
