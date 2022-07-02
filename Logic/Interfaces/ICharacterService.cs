using Data.Data;
using Data.Models;

namespace Logic.Interfaces;

public interface ICharacterService
{
    public Task<Guid> CheckCharacterByElementsAsync(UnitOfWork unitOfWork, string elements);
    public Task<Character> GetCharacterByKeyAsync(UnitOfWork unitOfWork, Guid id, string key);
    public Task AddNewCharacterAsync(UnitOfWork unitOfWork, Character character);
    public Task UpdateCharacter(UnitOfWork unitOfWork, Character character);
}