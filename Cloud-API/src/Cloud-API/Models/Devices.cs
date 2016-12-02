using Microsoft.EntityFrameworkCore;

namespace Cloud_API.Models {
    public class DevicesContext : DbContext{
        public DevicesContext(DbContextOptions<DevicesContext> options) : base(options){}

        #region Overrides of DbContext

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Devices>().HasKey(k => k.DeviceID);
        }

        #endregion

        public DbSet<Devices> Devices { get; set; }
    }

    public class Devices {
        public int DeviceID { get; set; }
    }
}
