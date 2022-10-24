using GameCatalogApi.InputModel;
using GameCatalogApi.ViewModel;

namespace GameCatalogApi.Services;

public interface IGameService
{
    Task<List<GameViewModel>> Get(int page, int quantity);
    Task<GameViewModel> Get(Guid id);
    Task<GameViewModel> Post(GameInputModel game);
    Task Update(Guid id, GameInputModel game);
    Task Update(Guid id, double price);
    Task Delete(Guid id);
}