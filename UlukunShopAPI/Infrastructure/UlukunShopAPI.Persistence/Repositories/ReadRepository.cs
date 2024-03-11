using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Domain.Entities.Common;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly UlukunAPIDbContext _context;

    public ReadRepository(UlukunAPIDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll(bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.Where(method);
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = Table.AsNoTracking();
        return await query.FirstOrDefaultAsync(method);
    }

    public async Task<T> GetByIdAsync(string id, bool tracking = true)
    {
        //ORM'nin kendi find metodunu kullanacağız fakat bu alttaki commentte bulunan metotda find imkanı vermeyen ormler icin kullanilabilir


        // => await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));


        // 2. güncelleme bunun trackingini devre dışı bırakabilme yapıyoruz.
        // => await Table.FindAsync(Guid.Parse(id));

        var query = Table.AsQueryable();
        if (!tracking)
        {
            query = Table.AsNoTracking();
        }
        //iquerable ile çalışınca findasync metodu yok. marker Interface kullanılacak. o sebeple son hali aşağıdaki gibi oldu.
        return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
    }
}