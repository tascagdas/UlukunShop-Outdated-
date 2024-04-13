using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using UlukunShopAPI.Persistence.Contexts;

namespace UlukunShopAPI.Persistence;

public class DesignTimeDbContextFactory:IDesignTimeDbContextFactory<UlukunAPIDbContext>
{
    public UlukunAPIDbContext CreateDbContext(string[] args)
    {
        //bu dosyayi migrationlari yapabilmek icin yaptik.
        
        
        //connection stringi appsettings icinden alabilmek icin 2 eklenti yuklendi. burada hangi json dosyasina gidecegini gosteriyoruz.
        
        // bunlara burada gerek kalmadi. configuration icinde yazildi
        
        
        
        // ConfigurationManager configurationManager = new();
        // configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/UlukunAPI.API"));
        // configurationManager.AddJsonFile("appsettings.json");
        //
        DbContextOptionsBuilder<UlukunAPIDbContext> dbContextOptionsBuilder = new();
        
        
        
        
        
        
        // dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);  
        
        //SQLite için...
        // dbContextOptionsBuilder.UseSqlite(Configuration.ConnectionString);
        dbContextOptionsBuilder.UseSqlite(Configuration.ConnectionString);

        
        
        
        return new(dbContextOptionsBuilder.Options);
    }
}