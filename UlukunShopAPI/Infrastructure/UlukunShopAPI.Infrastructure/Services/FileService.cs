using Microsoft.AspNetCore.Hosting;
using UlukunShopAPI.Infrastructure.Operations;
namespace UlukunShopAPI.Infrastructure.Services;

public class FileService
{
    readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }


}

