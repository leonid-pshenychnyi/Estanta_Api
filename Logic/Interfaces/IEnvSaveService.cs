using Data.Data;

namespace Logic.Interfaces;

public interface IEnvSaveService
{
    Task SaveDataFileAsync(UnitOfWork unitOfWork, string json, Guid? userId = null);
    Task<string> GetDataFromFileAsync(UnitOfWork unitOfWork, Guid id);
}