namespace GameCatalogApi.Exceptions;

public class GameNotRegisteredException : Exception
{
    public GameNotRegisteredException() : base("This Game is not registered!")
    {
    }
}