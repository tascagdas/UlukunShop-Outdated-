using Microsoft.Extensions.Configuration;

namespace UlukunShopAPI.Persistence;

static class Configuration
{
    static public string ConnectionString
    {
        get
        {
            //connection stringi appsettings içinden alabilmek icin 2 eklenti yüklendi. burada hangi json dosyasina gidecegini gösteriyoruz.
            ConfigurationManager configurationManager = new();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),
                "../../Presentation/UlukunShopAPI.API"));
            configurationManager.AddJsonFile("appsettings.json");

            // return configurationManager.GetConnectionString("PostgreSQL");
            return configurationManager.GetConnectionString("SqliteConnection");
        }
    }
}