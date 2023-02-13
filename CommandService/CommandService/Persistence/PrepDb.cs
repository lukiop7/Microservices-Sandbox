using CommandService.Models;
using CommandService.Persistence.Repositories;
using CommandService.SyncDataServices.Grpc;

namespace CommandService.Persistence;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();

        var platforms = grpcClient.ReturnAllPlatforms();

        SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>(), platforms);
    }

    private static void SeedData(ICommandRepo repo, IEnumerable<Platform> platforms)
    {
        Console.WriteLine("--> Seeding new platforms...");

        foreach (var platform in platforms)
        {
            if (!repo.ExternalPlatformExist(platform.ExternalId))
            {
                repo.CreatePlatform(platform);
            }

            repo.SaveChanges();
        }
    }
}