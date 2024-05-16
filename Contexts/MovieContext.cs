using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using WebAPIEFCoreDemo.Configuration;
using WebAPIEFCoreDemo.Models;

namespace WebAPIEFCoreDemo.Contexts
{
    public class MovieContext: DbContext
    {
        private ConnectionStringSettings _connectionStringSettings;
        public MovieContext(DbContextOptions<MovieContext> options, ConnectionStringSettings connectionStringSettings) :base(options)
        {
            _connectionStringSettings = connectionStringSettings;
        }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionStringSettings.DB_Connection);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(
                    new Movie { Id=1, Title="DDLJ", Genre="Romantic", ReleaseDate= DateTime.Now.AddYears(-20) },
                    new Movie { Id = 2, Title = "DDLJ2", Genre = "Romantic", ReleaseDate = DateTime.Now.AddYears(-10) }

                );
        }
    }
}
