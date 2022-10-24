using GameCatalogApi.Entities;

namespace GameCatalogApi.Repositories;

public class GameRepository : IGameRepository
{
    private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
    {
        {
            Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"),
            new Game
            {
                Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Name = "Persona 5 Royal", Developer = "Atlus",
                Price = 299.95
            }
        },
        {
            Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"),
            new Game
            {
                Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Name = "Kingdom Hearts III",
                Developer = "Square-Enix/Disney", Price = 175
            }
        },
        {
            Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"),
            new Game
            {
                Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Name = "Borderlands 3", Developer = "Gearbox",
                Price = 150
            }
        },
        {
            Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"),
            new Game
            {
                Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Name = "Marvel's Avengers",
                Developer = "Square-Enix", Price = 120
            }
        },
        {
            Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"),
            new Game
            {
                Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Name = "Banjo-Kazooie", Developer = "Rare",
                Price = 50
            }
        },
    };

    public Task<List<Game>> Get(int page, int quantity)
    {
        return Task.FromResult(games.Values.Skip((page - 1) * quantity).Take(quantity).ToList());
    }

    public Task<Game> Get(Guid id)
    {
        if (!games.ContainsKey(id))
            return null;

        return Task.FromResult(games[id]);
    }

    public Task<List<Game>> Get(string name, string developer)
    {
        return Task.FromResult(games.Values.Where(game => game.Name.Equals(name) && game.Developer.Equals(developer))
            .ToList());
    }

    public Task<List<Game>> GetWithoutLambda(string name, string developer)
    {
        var callback = new List<Game>();

        foreach (var game in games.Values)
        {
            if (game.Name.Equals(name) && game.Developer.Equals(developer))
                callback.Add(game);
        }

        return Task.FromResult(callback);
    }

    public Task Post(Game game)
    {
        games.Add(game.Id, game);
        return Task.CompletedTask;
    }

    public Task Update(Game game)
    {
        games[game.Id] = game;
        return Task.CompletedTask;
    }

    public Task Delete(Guid id)
    {
        games.Remove(id);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        // Method that ends connection to database
    }
}