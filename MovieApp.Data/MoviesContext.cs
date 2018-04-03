using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using MovieApp.Domain;


namespace MovieApp.Data
{
    public class MoviesContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieActor> MovieActor { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<TomatoRating> TomatoRating { get; set; }

        public static readonly LoggerFactory MovieLoggerFactory
            = new LoggerFactory(new [] {
            new ConsoleLoggerProvider((category, level)
                => category == DbLoggerCategory.Database.Command.Name
                && level == LogLevel.Information, true)
            });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>().HasKey(m => new { m.ActorId, m.MovieId });
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("Data Source=c:\\databas\\mydb.db");
  //              .EnableSensitiveDataLogging()
  //              .UseLoggerFactory(MovieLoggerFactory)
  //              .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = MyMovieDb; Trusted_Connection = True;");
        }
    }
}
