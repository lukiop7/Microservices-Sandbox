using CommandService.AsyncDataServices;
using CommandService.EventProcessing;
using CommandService.Persistence;
using CommandService.Persistence.Repositories;
using CommandService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddScoped<IPlatformDataClient, PlatformDataClient>();

builder.Services.AddHostedService<MessageBusSubscriber>();

AddDb(builder.Services, builder.Environment, builder.Configuration);
AddRepositories(builder.Services);

var app = builder.Build();

PrepDb.PrepPopulation(app);

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


void AddDb(IServiceCollection services, IWebHostEnvironment environment, ConfigurationManager configuration)
{
    Console.WriteLine("--> Using InMem Db");
    services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
}

void AddRepositories(IServiceCollection services)
{
    services.AddScoped<ICommandRepo, CommandRepo>();
}