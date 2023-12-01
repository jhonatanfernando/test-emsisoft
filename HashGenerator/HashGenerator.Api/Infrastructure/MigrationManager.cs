using HashGenerator.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HashGenerator.Api.Infrastructure;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase(this WebApplication webApp)
    {
        using (var scope = webApp.Services.CreateScope())
        {
            var log = scope.ServiceProvider.GetRequiredService<ILogger<HashContext>>();

            using var appContext = scope.ServiceProvider.GetRequiredService<HashContext>();
            try
            {
                appContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                log.LogError($"Error migrating the database: {ex.Message}");
            }
        }

        return webApp;
    }
}