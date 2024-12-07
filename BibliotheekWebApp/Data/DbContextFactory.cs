using BibliotheekApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BibliotheekApp.Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<BibliotheekContext>
    {
        public BibliotheekContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BibliotheekContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new BibliotheekContext(optionsBuilder.Options);
        }
    }
}
