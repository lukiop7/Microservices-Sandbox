using Microsoft.EntityFrameworkCore;
using PlatformService.AsyncDataServices;
using PlatformService.Persistence;
using PlatformService.Persistence.Repositories;
using PlatformService.SyncDataServices.Grpc;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

builder.Services.AddGrpc();

AddDb(builder.Services, builder.Environment, builder.Configuration);
AddRepositories(builder.Services);

var app = builder.Build();

PrepDb.PrepPopulation(app, app.Environment.IsProduction());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<GrpcPlatformService>();

app.MapGet("/protos/platforms.proto",
    async context => { await context.Response.WriteAsync(File.ReadAllText("Protos/platforms.proto")); });

app.Run();

void AddDb(IServiceCollection services, IWebHostEnvironment environment, ConfigurationManager configuration)
{
    if (environment.IsProduction())
    {
        Console.WriteLine("--> Using Sql Db");
        services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("PlatformsConn")));
    }
    else
    {
        Console.WriteLine("--> Using InMem Db");
        services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
    }
}

void AddRepositories(IServiceCollection services)
{
    services.AddScoped<IPlatformRepo, PlatformRepo>();
}