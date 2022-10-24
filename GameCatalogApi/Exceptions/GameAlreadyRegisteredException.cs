namespace GameCatalogApi.Exceptions;

public class GameAlreadyRegisteredException : Exception
{
    public GameAlreadyRegisteredException() : base("This game has already been registered!")
    {
    }
}