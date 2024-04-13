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
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<CompletedOrder> CompletedOrders { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Endpoint> Endpoints { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Order>().HasKey(sc => sc.Id);
        
        builder.Entity<Order>().HasIndex(o => o.OrderCode).IsUnique();
        
        builder.Entity<ShoppingCart>().HasOne(sc => sc.Order)
            .WithOne(o => o.ShoppingCart)
            .HasForeignKey<Order>(sc => sc.Id);
        
        builder.Entity<Order>()
            .HasOne(o => o.CompletedOrder)
            .WithOne(c => c.Order)
            .HasForeignKey<CompletedOrder>(c => c.OrderId);
        
        base.OnModelCreating(builder);
    }


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