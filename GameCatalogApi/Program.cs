using GameCatalogApi.Controllers.V1;
using GameCatalogApi.Repositories;
using GameCatalogApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
// In case it's preferable to use SQLServer, just change the line above for the line bellow:
// builder.Services.AddScoped<IGameRepository, GameSqlServerRepository>();

#region LifeCycle

builder.Services.AddSingleton<LifeCycleIDController.IExampleSingleton, LifeCycleIDController.LifeCycleExample>();
builder.Services.AddScoped<LifeCycleIDController.IExampleScoped, LifeCycleIDController.LifeCycleExample>();
builder.Services.AddTransient<LifeCycleIDController.IExampleTransient, LifeCycleIDController.LifeCycleExample>();

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();