using Microsoft.AspNetCore.Http;
using UlukunShopAPI.Application.Abstractions.Storage;

namespace UlukunShopAPI.Infrastructure.Services.Storage;

public class StorageService : IStorageService
{
    readonly private IStorage _storage;

    public StorageService(IStorage storage)
    {
        _storage = storage;
    }

    public Task<List<(string fileName, string pathOrContainer)>> UploadAsync(string pathOrContainerName,
        IFormFileCollection files)
        => _storage.UploadAsync(pathOrContainerName, files);

    public async Task DeleteAsync(string pathOrContainerName, string fileName)
        => await _storage.DeleteAsync(pathOrContainerName, fileName);

    public List<string> GetFiles(string pathOrContainerName)
        => _storage.GetFiles(pathOrContainerName);

    public bool HasFile(string pathOrContainerName, string fileName)
        => _storage.HasFile(pathOrContainerName, fileName);

    public string storageName
    {
        get => _storage.GetType().Name;
    }
}