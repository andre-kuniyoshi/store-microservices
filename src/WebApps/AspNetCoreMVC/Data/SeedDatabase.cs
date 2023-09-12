namespace AspNetCoreMVC.Data
{
    public class SeedDatabase
    {
        public static async Task MigrateDatabase(WebApplication webApp)
        {
            var scopedFactory = webApp.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopedFactory.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await context.Database.EnsureCreatedAsync();
            }
        }
    }
}
