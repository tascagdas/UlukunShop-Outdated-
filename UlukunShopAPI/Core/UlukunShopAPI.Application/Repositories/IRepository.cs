using Microsoft.EntityFrameworkCore;
using UlukunShopAPI.Domain.Entities.Common;

namespace UlukunShopAPI.Application.Repositories;

public interface IRepository<T> where T:BaseEntity
{
    DbSet<T> Table { get; }
}