using GameCatalogApi.Entities;

namespace GameCatalogApi.Repositories;

public interface IGameRepository : IDisposable
{
    Task<List<Game>> Get(int page, int quantity);
    Task<Game> Get(Guid id);
    Task<List<Game>> Get(string name, string developer);
    Task Post(Game game);
    Task Update(Game game);
    Task Delete(Guid id);
}