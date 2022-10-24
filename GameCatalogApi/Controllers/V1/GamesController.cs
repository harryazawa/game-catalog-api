using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GameCatalogApi.Exceptions;
using GameCatalogApi.InputModel;
using GameCatalogApi.Services;
using GameCatalogApi.ViewModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GameCatalogApi.Controllers.V1;

[Route("api/V1/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
    {
        var games = await _gameService.Get(page, quantity);

        if (games.Count() == 0)
            return NoContent();
        
        return Ok(games);
    }
    
    [HttpGet("{gameId:guid}")]
    public async Task<ActionResult<GameViewModel>> Get([FromRoute] Guid gameId)
    {
        var game = await _gameService.Get(gameId);

        if (game == null)
            return NoContent();
        
        return Ok(game);
    }

    [HttpPost]
    public async Task<ActionResult<GameViewModel>> PostGame([FromBody] GameInputModel gameInputModel)
    {
        try
        {
            var game = await _gameService.Post(gameInputModel);

            return Ok(game);
        }
        catch (GameAlreadyRegisteredException ex)
        {
            return UnprocessableEntity("A game with this name has already been registered under this Developer.");
        }
    }

    [HttpPut("{gameId:guid}")]
    public async Task<ActionResult> UpdateGame([FromRoute] Guid gameId, [FromBody] GameInputModel gameInputModel)
    {
        try
        {
            await _gameService.Update(gameId, gameInputModel);

            return Ok();
        }
        catch (GameNotRegisteredException ex)
        {
            return NotFound("Game not found");
        }
    }

    [HttpPatch("{gameId:guid}/price/{price:double)")]
    public async Task<ActionResult> UpdateGame([FromRoute] Guid gameId, [FromRoute] double price)
    {
        try
        {
            await _gameService.Update(gameId, price);

            return Ok();
        }
        catch (GameNotRegisteredException ex)
        {
            return NotFound("Game not found");
        }
    }

    [HttpDelete("{gameId:guid}")]
    public async Task<ActionResult> DeleteGame([FromRoute] Guid gameId)
    {
        try
        {
            await _gameService.Delete(gameId);

            return Ok();
        }
        catch (GameNotRegisteredException ex)
        {
            return NotFound("Game not found");
        }
    }
}