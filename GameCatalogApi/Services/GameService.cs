using GameCatalogApi.Entities;
using GameCatalogApi.Exceptions;
using GameCatalogApi.InputModel;
using GameCatalogApi.Repositories;
using GameCatalogApi.ViewModel;

namespace GameCatalogApi.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<List<GameViewModel>> Get(int page, int quantity)
    {
        var games = await _gameRepository.Get(page, quantity);

        return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Developer = game.Developer,
                Price = game.Price
            })
            .ToList();
    }

    public async Task<GameViewModel> Get(Guid id)
    {
        var game = await _gameRepository.Get(id);

        if (game == null)
            return null;

        return new GameViewModel
        {
            Id = game.Id,
            Name = game.Name,
            Developer = game.Developer,
            Price = game.Price
        };
    }

    public async Task<GameViewModel> Post(GameInputModel game)
    {
        var gameEntity = await _gameRepository.Get(game.Name, game.Developer);

        if (gameEntity.Count > 0)
            throw new GameAlreadyRegisteredException();

        var gamePost = new Game
        {
            Id = Guid.NewGuid(),
            Name = game.Name,
            Developer = game.Developer,
            Price = game.Price
        };

        await _gameRepository.Post(gamePost);

        return new GameViewModel
        {
            Id = gamePost.Id,
            Name = game.Name,
            Developer = game.Developer,
            Price = game.Price
        };
    }

    public async Task Update(Guid id, GameInputModel game)
    {
        var gameEntity = await _gameRepository.Get(id);

        if (gameEntity == null)
            throw new GameNotRegisteredException();

        gameEntity.Name = game.Name;
        gameEntity.Developer = game.Developer;
        gameEntity.Price = game.Price;

        await _gameRepository.Update(gameEntity);
    }

    public async Task Update(Guid id, double price)
    {
        var gameEntity = await _gameRepository.Get(id);

        if (gameEntity == null)
            throw new GameNotRegisteredException();

        gameEntity.Price = price;

        await _gameRepository.Update(gameEntity);
    }

    public async Task Delete(Guid id)
    {
        var game = _gameRepository.Get(id);

        if (game == null)
            throw new GameNotRegisteredException();

        await _gameRepository.Delete(id);
    }

    public void Dispose()
    {
        _gameRepository?.Dispose();
    }
}