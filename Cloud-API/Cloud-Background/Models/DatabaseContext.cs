namespace Cloud_Background
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }

        public virtual DbSet<AuxDataType> AuxDataType { get; set; }
        public virtual DbSet<AuxDeviceActions> AuxDeviceActions { get; set; }
        public virtual DbSet<AuxDeviceProtocols> AuxDeviceProtocols { get; set; }
        public virtual DbSet<AuxDeviceType> AuxDeviceType { get; set; }
        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<HistoricData> HistoricData { get; set; }
        public virtual DbSet<HistoricDevices> HistoricDevices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuxDataType>()
                .HasMany(e => e.HistoricData)
                .WithRequired(e => e.AuxDataType)
                .HasForeignKey(e => e.IDDataType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AuxDeviceActions>()
                .HasMany(e => e.HistoricDevices)
                .WithRequired(e => e.AuxDeviceActions)
                .HasForeignKey(e => e.IDDeviceAction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AuxDeviceProtocols>()
                .HasMany(e => e.Devices)
                .WithRequired(e => e.AuxDeviceProtocols)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AuxDeviceType>()
                .HasMany(e => e.Devices)
                .WithRequired(e => e.AuxDeviceType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Devices>()
                .Property(e => e.DeviceName)
                .IsFixedLength();

            modelBuilder.Entity<Devices>()
                .Property(e => e.DeviceUsername)
                .IsFixedLength();

            modelBuilder.Entity<Devices>()
                .Property(e => e.DevicePassword)
                .IsFixedLength();

            modelBuilder.Entity<Devices>()
                .HasMany(e => e.HistoricData)
                .WithRequired(e => e.Devices)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Devices>()
                .HasMany(e => e.HistoricDevices)
                .WithRequired(e => e.Devices)
                .WillCascadeOnDelete(false);
        }
    }
}
