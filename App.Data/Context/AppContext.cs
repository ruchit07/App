using Microsoft.EntityFrameworkCore;
using App.Data.Models;

namespace App.Data.Context
{
    public partial class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
            Database.SetCommandTimeout(150000);
            //this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public AppContext()
        {
        }

       
        public DbSet<ErrorLog> ErrorLog { get; set; }
        public DbSet<Lead> Lead { get; set; }
        public DbSet<LeadAddress> LeadAddress { get; set; }
        public DbSet<LeadNote> LeadNote { get; set; }
        public DbSet<LeadSource> LeadSource { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=ruchit\SQLEXPRESS;Initial Catalog=ZCoreApp;User id=sa;password=sa@123;");
            //optionsBuilder.UseSqlServer(@"data source=MD2GP2HC\SQLEXPRESS;initial catalog=ZCoreApp;integrated security=True;MultipleActiveResultSets=True");
            //optionsBuilder.UseSqlServer(@"data source=RUCHIT\SQLEXPRESS;initial catalog=ZCoreApp;integrated security=True;MultipleActiveResultSets=True");
            optionsBuilder.UseSqlServer(@"data source=localhost\SQLEXPRESS;initial catalog=ZCoreApp;integrated security=True;MultipleActiveResultSets=True");
            //optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

