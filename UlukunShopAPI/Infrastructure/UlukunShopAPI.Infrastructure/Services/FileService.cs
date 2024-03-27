using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using UlukunShopAPI.Application.Services;
using UlukunShopAPI.Infrastructure.Operations;

namespace UlukunShopAPI.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
    {
        //todo try catch uygulanabilir

        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        List<(string fileName, string path)> datas = new();

        List<bool> results = new();
        foreach (IFormFile file in files)
        {
            string fileNewName = await FileRenameAsync(uploadPath, file.FileName);
            bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
            datas.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
        }

        if (results.TrueForAll(r => r.Equals(true)))
        {
            return datas;
        }

        //todo eger yukaridaki if gecerli degilse burada dosyalarin sunucuda yuklenirken hata alindigina dair bir uyarici exception yapilmasi gerekiyor.
        return null;
    }

    private async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
    {
        string regulatedFileName = await Task.Run<string>(async () =>
        {
            string extension = Path.GetExtension(fileName);
            string regulatedFileName = string.Empty;
            if (first)
            {
                string oldName = Path.GetFileNameWithoutExtension(fileName);
                regulatedFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
            }
            else
            {
                regulatedFileName = fileName;
                int indexNo1 = regulatedFileName.IndexOf("-");
                if (indexNo1 == -1)
                {
                    regulatedFileName = $"{Path.GetFileNameWithoutExtension(regulatedFileName)}-2{extension}";
                }
                else
                {
                    int lastIndex = 0;
                    while (true)
                    {
                        lastIndex = indexNo1;
                        indexNo1 = regulatedFileName.IndexOf("-", indexNo1 + 1);
                        if (indexNo1 == -1)
                        {
                            indexNo1 = lastIndex;
                            break;
                        }
                    }
                    int indexNo2 = regulatedFileName.IndexOf(".");
                    string fileNo = regulatedFileName.Substring(indexNo1 + 1, indexNo2 - indexNo1 - 1);

                    if (int.TryParse(fileNo, out int _fileNo))
                    {
                        _fileNo++;
                        regulatedFileName = regulatedFileName.Remove(indexNo1 + 1, indexNo2 - indexNo1 - 1)
                            .Insert(indexNo1 + 1, _fileNo.ToString());
                    }
                    else
                    {
                        regulatedFileName = $"{Path.GetFileNameWithoutExtension(regulatedFileName)}-2{extension}";
                    }
                }
            }
            if (File.Exists($"{path}\\{regulatedFileName}"))
            {
                return await FileRenameAsync(path, regulatedFileName, false);
            }
            else
            {
                return regulatedFileName;
            }
        });
        return regulatedFileName;
    }

    public async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        try
        {
            await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None,
                1024 * 1024, useAsync: false);

            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
        }
        catch (Exception e)
        {
            //todo log!
            throw e;
        }
    }
}