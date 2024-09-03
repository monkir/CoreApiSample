using CoreApiSample.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreApiSample.Db
{
    public class ShopSolutionDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var currenctDirectory = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder()
                .SetBasePath(currenctDirectory)
                .AddJsonFile("appsettings.json");

            var dbConn = builder
                .Build()
                .GetConnectionString("ShopSolutionDbConn");

            optionsBuilder.UseSqlServer(dbConn);

            //base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Product> Products { get; set; }
    }
}
