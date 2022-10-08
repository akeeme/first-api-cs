using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using myAPI.Models;
using MySql.EntityFrameworkCore;

namespace myAPI.Models{
    public class myAPIDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public myAPIDBContext(DbContextOptions<myAPIDBContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options){
            var connectionString = Configuration.GetConnectionString("CustomerDataService");
            options.UseMySQL(connectionString);
        }

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Email> Emails { get; set; } = null!;
    }
}