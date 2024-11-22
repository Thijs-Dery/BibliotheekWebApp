using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BibliotheekApp.Models
{
    public class BibliotheekContextFactory : IDesignTimeDbContextFactory<BibliotheekContext>
    {
        public BibliotheekContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BibliotheekContext>();

            // Zorg ervoor dat de verbinding naar de juiste configuratiebestand wijst
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Zet het pad naar je projectmap
                .AddJsonFile("appsettings.json")
                .Build();

            // Gebruik de configuratie voor het verkrijgen van de juiste connectiestring
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new BibliotheekContext(optionsBuilder.Options);
        }
    }
}

