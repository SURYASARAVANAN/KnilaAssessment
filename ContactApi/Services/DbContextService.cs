using ContactApi.Modals;
using Microsoft.EntityFrameworkCore;

namespace ContactApi.Services
{
    public class DbContextService : DbContext
    {
       private string _connectionString; 
       public DbSet<Contact> Contacts { get; set; }

       //public DbContextService(string connectionString)
       //{
       //     _connectionString = connectionString;
       //}

       //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       //{
       //     optionsBuilder.UseSqlServer(_connectionString);
       //}

       public DbContextService( DbContextOptions<DbContextService> options): base(options)
       {
       }
    }
}
