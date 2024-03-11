using Microsoft.EntityFrameworkCore;
using UlukunShopAPI.Domain.Entities;

namespace UlukunShopAPI.Persistence.Contexts;

public class UlukunAPIDbContext:DbContext
{

    public UlukunAPIDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }

}