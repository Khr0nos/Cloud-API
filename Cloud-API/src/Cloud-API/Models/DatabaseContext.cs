using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cloud_API.Models {
    public partial class DatabaseContext : DbContext {
        public virtual DbSet<AuxDataType> AuxDataType { get; set; }
        public virtual DbSet<AuxDeviceActions> AuxDeviceActions { get; set; }
        public virtual DbSet<AuxDeviceProtocols> AuxDeviceProtocols { get; set; }
        public virtual DbSet<AuxDeviceType> AuxDeviceType { get; set; }
        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<HistoricData> HistoricData { get; set; }
        public virtual DbSet<HistoricDevices> HistoricDevices { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<AuxDataType>(entity => {
                entity.HasKey(e => e.IdauxDataType)
                    .HasName("PK_AuxDataType");

                entity.Property(e => e.IdauxDataType)
                    .HasColumnName("IDAuxDataType")
                    .ValueGeneratedNever();

                entity.Property(e => e.DataType)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AuxDeviceActions>(entity => {
                entity.HasKey(e => e.IdauxDeviceAction)
                    .HasName("PK_auxDeviceActions");

                entity.Property(e => e.IdauxDeviceAction)
                    .HasColumnName("IDAuxDeviceAction")
                    .ValueGeneratedNever();

                entity.Property(e => e.DeviceAction)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AuxDeviceProtocols>(entity => {
                entity.HasKey(e => e.IddeviceProtocol)
                    .HasName("PK_AuxDeviceProtocols");

                entity.Property(e => e.IddeviceProtocol)
                    .HasColumnName("IDDeviceProtocol")
                    .ValueGeneratedNever();

                entity.Property(e => e.DeviceProtocol)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AuxDeviceType>(entity => {
                entity.HasKey(e => e.IdauxDeviceType)
                    .HasName("PK_auxType");

                entity.Property(e => e.IdauxDeviceType)
                    .HasColumnName("IDAuxDeviceType")
                    .ValueGeneratedNever();

                entity.Property(e => e.DeviceType)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Devices>(entity => {
                entity.HasKey(e => e.Iddevice)
                    .HasName("PK_Equips");

                entity.Property(e => e.Iddevice)
                    .HasColumnName("IDDevice")
                    .ValueGeneratedNever();

                entity.Property(e => e.DeviceAux).HasMaxLength(1024);

                entity.Property(e => e.DeviceConnected).HasDefaultValueSql("0");

                entity.Property(e => e.DeviceCreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.DeviceEnabled).HasDefaultValueSql("0");

                entity.Property(e => e.DeviceInterval).HasDefaultValueSql("0");

                entity.Property(e => e.DeviceName)
                    .IsRequired()
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.DeviceNeedLogin).HasDefaultValueSql("0");

                entity.Property(e => e.DevicePassword)
                    .IsRequired()
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.DeviceUsername)
                    .IsRequired()
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.IdauxDeviceType).HasColumnName("IDAuxDeviceType");

                entity.Property(e => e.IddeviceProtocol).HasColumnName("IDDeviceProtocol");

                entity.HasOne(d => d.IdauxDeviceTypeNavigation)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.IdauxDeviceType)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Devices_AuxType");

                entity.HasOne(d => d.IddeviceProtocolNavigation)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.IddeviceProtocol)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Devices_AuxDeviceProtocols");
            });

            modelBuilder.Entity<HistoricData>(entity => {
                entity.HasKey(e => e.IdhistoricData)
                    .HasName("PK_HistoricDades");

                entity.Property(e => e.IdhistoricData)
                    .HasColumnName("IDHistoricData")
                    .ValueGeneratedNever();

                entity.Property(e => e.HistDataAux).HasMaxLength(512);

                entity.Property(e => e.HistDataDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.HistDataValue)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("0");

                entity.Property(e => e.IddataType).HasColumnName("IDDataType");

                entity.Property(e => e.Iddevice).HasColumnName("IDDevice");

                entity.HasOne(d => d.IddataTypeNavigation)
                    .WithMany(p => p.HistoricData)
                    .HasForeignKey(d => d.IddataType)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_HistoricData_AuxDataType");

                entity.HasOne(d => d.IddeviceNavigation)
                    .WithMany(p => p.HistoricData)
                    .HasForeignKey(d => d.Iddevice)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_HistoricData_Devices");
            });

            modelBuilder.Entity<HistoricDevices>(entity => {
                entity.HasKey(e => e.IdhistoricDevices)
                    .HasName("PK_HistoricEquips");

                entity.Property(e => e.IdhistoricDevices)
                    .HasColumnName("IDHistoricDevices")
                    .ValueGeneratedNever();

                entity.Property(e => e.HistDeviceAux).HasMaxLength(1024);

                entity.Property(e => e.HistDeviceDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.HistDeviceIpaddress)
                    .HasColumnName("HistDeviceIPaddress")
                    .HasMaxLength(16);

                entity.Property(e => e.Iddevice).HasColumnName("IDDevice");

                entity.Property(e => e.IddeviceAction).HasColumnName("IDDeviceAction");

                entity.HasOne(d => d.IddeviceNavigation)
                    .WithMany(p => p.HistoricDevices)
                    .HasForeignKey(d => d.Iddevice)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_HistoricDevices_Devices");

                entity.HasOne(d => d.IddeviceActionNavigation)
                    .WithMany(p => p.HistoricDevices)
                    .HasForeignKey(d => d.IddeviceAction)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_HistoricDevices_AuxDeviceActions1");
            });
        }
    }
}