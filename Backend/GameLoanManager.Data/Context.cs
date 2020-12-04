using Microsoft.EntityFrameworkCore;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Data.Maps;

namespace GameLoanManager.Data
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<LoanedGame> LoanedGames { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(new UserMap().Configure);
            modelBuilder.Entity<Game>(new GameMap().Configure);
            modelBuilder.Entity<LoanedGame>(new LoanGameMap().Configure);
        }
    }
}
