using Data.Data;
using Logic.Interfaces;
using Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Estanta_Api.Controllers;

[ApiController]
[Route("api/{controller}")]
public class CharacterController
{
    private readonly ICharacterService _characterService;

    public CharacterController()
    {
        _characterService = new CharacterService();
    }

    [HttpGet]
    [Route("GetCharacterIdByEls")]
    public async Task<IActionResult> GetCharacterIdByElements(string elements)
    {
        using (var unitOfWork = new UnitOfWork())
        {
            var characterId = await _characterService.CheckCharacterByElementsAsync(unitOfWork, elements);
            return new JsonResult(characterId);
        }
    }

    [HttpGet]
    [Route("GetCharacterByKey")]
    public async Task<IActionResult> GetCharacterByKey(Guid id, string key)
    {
        // TODO: Get exp key
        using (var unitOfWork = new UnitOfWork())
        {
            var character = await _characterService.GetCharacterByKeyAsync(unitOfWork, id, key);
            return new JsonResult(character);
        }
    }
}