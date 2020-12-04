using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GameLoanManager.Data
{
    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
         public IConfiguration Configuration { get; }
        public ContextFactory(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public ContextFactory()
        {
        }

        public Context CreateDbContext(string[] args)
        {
            var connectionString = "Server=localhost;Port=5432;Database=GameLoanManager;User Id=postgres;Password=root;";//Configuration.GetConnectionString("WebApiConnection");
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseNpgsql(connectionString);
            
            return new Context(optionsBuilder.Options);
        }
    }
}
