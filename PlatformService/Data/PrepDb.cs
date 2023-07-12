using PlatformService.Models;

namespace PlatformService.Data;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder application)
    {
        using( var serviceScope = application.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }
    }

    private static void SeedData(AppDbContext context)
    {
        if (!context.Platforms.Any())
        {
            Console.WriteLine(" ->> Seeding data");

            context.Platforms.AddRange(
                new Platform() { Name = " Dot Net", Publisher = "Microsoft", Cost = " Free" },
                new Platform() { Name = " Dot Net Core", Publisher = "GoJek", Cost = " Free" },
                new Platform() { Name = " K8S", Publisher = "Bee", Cost = " Free" },
                new Platform() { Name = " SQL", Publisher = "Alibaba", Cost = " Free" }
                );
            context.SaveChanges();
        }
        else
        {
            Console.WriteLine(" ->> We already have data");
        }
    }
}
