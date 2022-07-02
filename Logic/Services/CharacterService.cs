using Data.Data;
using Data.Models;
using Logic.Interfaces;

namespace Logic.Services;

public class CharacterService : ICharacterService
{
    public async Task<Guid> CheckCharacterByElementsAsync(UnitOfWork unitOfWork, string elements)
    {
        var characters = await unitOfWork.Characters.GetAll();
        var character = characters.FirstOrDefault(character => character.Elements == elements);
        return character?.Id ?? Guid.Empty;
    }

    public async Task<Character> GetCharacterByKeyAsync(UnitOfWork unitOfWork, Guid id, string key)
    {
        var characters = await unitOfWork.Characters.GetAll();
        var character = characters.FirstOrDefault(character => character.Id == id);
        return character != null && character.Key == key ? character : null;
    }

    public async Task AddNewCharacterAsync(UnitOfWork unitOfWork, Character character)
    {
        // TODO: check for element exists + key validate
        if (string.IsNullOrEmpty(character.Elements))
            throw new NullReferenceException();

        character.Id = Guid.NewGuid();
        await unitOfWork.Characters.Add(character);
    }

    public Task UpdateCharacter(UnitOfWork unitOfWork, Character character)
    {
        throw new NotImplementedException();
    }
}