using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UlukunShopAPI.Domain.Entities;
using UlukunShopAPI.Domain.Entities.Common;
using UlukunShopAPI.Domain.Entities.Identity;
using File = UlukunShopAPI.Domain.Entities.File;

namespace UlukunShopAPI.Persistence.Contexts;

public class UlukunAPIDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public UlukunAPIDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<ProductImageFile> ProductImageFiles { get; set; }
    public DbSet<InvoiceFile> InvoiceFiles { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //changetracker entityler uzerinde yapilan degisikliklerin yada yeni eklenen verinin yaskalanmasini saglayan proptir.update operasyonlarinda track edilen verileri yakalayip elde etmemizi saglar.
        var datas = ChangeTracker
            .Entries<BaseEntity>();

        foreach (var data in datas)
        {
            _ = data.State switch
            {
                EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                _ => DateTime.UtcNow
            };
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}