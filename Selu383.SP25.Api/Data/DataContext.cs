
using Microsoft.EntityFrameworkCore;
using Selu383.SP25.Api.Entities;
using System.Diagnostics.Metrics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Selu383.SP25.Api.Data
{
    public class DataContext : DbContext
    {

        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // connect to sql server with connection string from app settings
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("TheaterDB"));
        }

        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Theater> Theater { get; set; }
               
    }



}

