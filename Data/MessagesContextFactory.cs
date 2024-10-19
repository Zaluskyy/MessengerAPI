using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace messenger.Data
{
    public class MessagesContextFactory : IDesignTimeDbContextFactory<MessagesContext>
    {
        public MessagesContext CreateDbContext(string[] args)
        {
            // Tworzenie konfiguracji
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MessagesContext>();
            var connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlite(connectionString);  // lub UseSqlServer(connectionString) dla SQL Server

            return new MessagesContext(optionsBuilder.Options);
        }
    }
}
