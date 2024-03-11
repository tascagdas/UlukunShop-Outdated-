using UlukunShopAPI.Domain.Entities.Common;

namespace UlukunShopAPI.Application.Repositories;

public interface IWriteRepository<T>:IRepository<T> where T:BaseEntity
{
    Task<bool> AddAsync(T model);
    Task<bool> AddRangeAsync(List<T> datas);
    bool Remove(T model);
    bool RemoveRange(List<T> datas);
    Task<bool> Remove(string id);
    bool Update(T model);
    Task<int> SaveAsync();
}