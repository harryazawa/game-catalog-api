using System.Data.SqlClient;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

using GameCatalogApi.Entities;
using GameCatalogApi.Services;

namespace GameCatalogApi.Repositories;

public class GameSqlServerRepository : IGameRepository
{
    private readonly SqlConnection sqlConnection;

    public GameSqlServerRepository(IConfiguration configuration)
    {
        sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
    }

    public async Task<List<Game>> Get(int page, int quantity)
    {
        var games = new List<Game>();

        var command =
            $"select * from Games order by id offset {((page - 1) * quantity)} rows fetch next {quantity} rows only";

        await sqlConnection.OpenAsync();
        SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
        SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

        while (sqlDataReader.Read())
        {
            games.Add(new Game
            {
                Id = (Guid)sqlDataReader["Id"],
                Name = (string)sqlDataReader["Name"],
                Developer = (string)sqlDataReader["Developer"],
                Price = (double)sqlDataReader["Price"]
            });
        }

        await sqlConnection.CloseAsync();

        return games;
    }

    public async Task<Game> Get(Guid id)
    {
        Game game = null;

        var command = $"select * from Games where Id = '{id}'";

        await sqlConnection.OpenAsync();
        SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
        SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

        while (sqlDataReader.Read())
        {
            game = new Game
            {
                Id = (Guid)sqlDataReader["Id"],
                Name = (string)sqlDataReader["Name"],
                Developer = (string)sqlDataReader["Developer"],
                Price = (double)sqlDataReader["Price"]
            };
        }

        await sqlConnection.CloseAsync();

        return game;
    }

    public async Task<List<Game>> Get(string name, string developer)
    {
        var games = new List<Game>();

        var command = $"select * from Games where Name = '{name}' and Developer = '{developer}'";

        await sqlConnection.OpenAsync();
        SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
        SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

        while (sqlDataReader.Read())
        {
            games.Add(new Game
            {
                Id = (Guid)sqlDataReader["Id"],
                Name = (string)sqlDataReader["Name"],
                Developer = (string)sqlDataReader["Developer"],
                Price = (double)sqlDataReader["Price"]
            });
        }

        await sqlConnection.CloseAsync();

        return games;
    }

    public async Task Post(Game game)
    {
        var command =
            $"insert Games (Id, Name, Developer, Price) values ('{game.Id}', '{game.Developer}', {game.Price.ToString().Replace(",", ".")})";

        await sqlConnection.OpenAsync();
        SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
        sqlCommand.ExecuteNonQuery();
        await sqlConnection.CloseAsync();
    }

    public async Task Update(Game game)
    {
        var command =
            $"update Games set Name = '{game.Name}', Developer = '{game.Developer}' Price = {game.Price.ToString().Replace(",", ".")} where Id = '{game.Id}'";

        await sqlConnection.OpenAsync();
        SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
        sqlCommand.ExecuteNonQuery();
        await sqlConnection.CloseAsync();
    }

    public async Task Delete(Guid id)
    {
        var command = $"delete from Games where Id = '{id}'";

        await sqlConnection.OpenAsync();
        SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
        await sqlCommand.ExecuteNonQueryAsync();
        await sqlConnection.CloseAsync();
    }

    public void Dispose()
    {
        sqlConnection?.Close();
        sqlConnection?.Dispose();
    }
}