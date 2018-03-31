using System.Data.Entity;
using ApiUserControl.Domain.Models;
using ApiUserControl.Infraestructure.Data.Map;

namespace ApiUserControl.Infraestructure.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext() : base("EntityContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
