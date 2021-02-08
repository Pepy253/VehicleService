using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Vehicle.Service.Models;

namespace Vehicle.Service.Data
{
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext() : base("name=DefaultConnection")
        {             
        }

        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
