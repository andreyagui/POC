using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using POC.Infrastructure.Data;
namespace POC.API.Data

{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            // Configuración de la cadena de conexión desde appsettings.json de POC.API
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../POC.API"))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            var connectionString = configuration.GetConnectionString("LocalConnection");

            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("POC.API"));

            return new DataContext(optionsBuilder.Options);
        }
    }
}
