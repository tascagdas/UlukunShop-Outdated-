using System.Linq.Expressions;
using UlukunShopAPI.Domain.Entities.Common;

namespace UlukunShopAPI.Application.Repositories;

public interface IReadRepository<T>:IRepository<T> where T:BaseEntity
{
    //Read işlemleri esnasında orm'nin tracking yapmasına pek gerek yok. çünkü değişen bir veri yok.
    //bu sebeple bu repository'deki  metotların trackingini kapatmak için parametre veriyoruz içeri.
    IQueryable<T> GetAll(bool tracking = true);
    IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
    Task<T> GetByIdAsync(string id, bool tracking = true);
}