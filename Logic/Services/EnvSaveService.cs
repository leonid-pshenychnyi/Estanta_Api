using Data.Data;
using Data.Models;
using Logic.Interfaces;

namespace Logic.Services;

public class EnvSaveService : IEnvSaveService
{
    public async Task SaveDataFileAsync(UnitOfWork unitOfWork, string json, Guid? userId = null)
    {
        var id = Guid.NewGuid();
        var path = "";
        var filePath = $"{path}/{id}";
        await File.WriteAllTextAsync(filePath, json);

        var envSave = new EnvSave
        {
            Id = id,
            Path = filePath
        };
        if (userId.HasValue)
            envSave.UserId = userId.Value;

        await unitOfWork.EnvSaves.Add(envSave);
    }

    public async Task<string> GetDataFromFileAsync(UnitOfWork unitOfWork, Guid id)
    {
        var path = "";
        var filePath = $"{path}/{id}";

        var envSave = (await unitOfWork.EnvSaves.GetAll()).FirstOrDefault(w => w.Id == id)?.Path;
        return envSave != null 
            ? await File.ReadAllTextAsync(envSave) : File.Exists(filePath) 
                ? await File.ReadAllTextAsync(filePath) : throw new FileNotFoundException();
        // TODO: Custom Exception
    }
}