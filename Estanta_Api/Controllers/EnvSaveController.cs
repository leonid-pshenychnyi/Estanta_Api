using System.Net;
using Data.Data;
using Logic.Interfaces;
using Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Estanta_Api.Controllers;

[ApiController]
[Route("api/{controller}")]
public class EnvSaveController
{
    private readonly IEnvSaveService _envSaveService;

    public EnvSaveController()
    {
        _envSaveService = new EnvSaveService();
    }

    [HttpGet]
    [Route("SaveDataFile")]
    public async Task<IActionResult> SaveDataFile(string json, Guid? userId)
    {
        using (var unitOfWork = new UnitOfWork())
        {
            await _envSaveService.SaveDataFileAsync(unitOfWork, json, userId);
            return new JsonResult(HttpStatusCode.OK);
        }
    }

    [HttpGet]
    [Route("GetDataFromFile")]
    public async Task<IActionResult> GetDataFromFile(Guid id)
    {
        using (var unitOfWork = new UnitOfWork())
        {
            var json = await _envSaveService.GetDataFromFileAsync(unitOfWork, id);
            return new JsonResult(json);
        }
    }
}