namespace UlukunShopAPI.Application.Abstractions.Storage;

public interface IStorageService:IStorage
{
    public string storageName { get; }
}