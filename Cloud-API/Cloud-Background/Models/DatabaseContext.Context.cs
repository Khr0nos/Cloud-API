﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cloud_Background.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AuxDataType> AuxDataType { get; set; }
        public virtual DbSet<AuxDeviceActions> AuxDeviceActions { get; set; }
        public virtual DbSet<AuxDeviceProtocols> AuxDeviceProtocols { get; set; }
        public virtual DbSet<AuxDeviceType> AuxDeviceType { get; set; }
        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<HistoricData> HistoricData { get; set; }
        public virtual DbSet<HistoricDevices> HistoricDevices { get; set; }
    }
}