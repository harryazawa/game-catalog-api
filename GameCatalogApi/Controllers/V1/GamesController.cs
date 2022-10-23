using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameCatalogApi.InputModel;
using GameCatalogApi.ViewModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GameCatalogApi.Controllers.V1;

[Route("api/V1/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<GameViewModel>>> Get()
    {
        return Ok();
    }
    
    [HttpGet("{gameId:guid}")]
    public async Task<ActionResult<GameViewModel>> Get(Guid gameId)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<GameViewModel>> PostGame(GameInputModel game)
    {
        return Ok();
    }

    [HttpPut("{gameId:guid}")]
    public async Task<ActionResult> UpdateGame(Guid gameId, GameInputModel game)
    {
        return Ok();
    }
    
    [HttpPatch("{gameId:guid}/price/{price:double)")]
    public async Task<ActionResult> UpdateGame(Guid gameId, double price)
    {
        return Ok();
    }

    [HttpDelete("{gameId:guid}")]
    public async Task<ActionResult> DeleteGame(Guid gameId)
    {
        return Ok();
    }
}