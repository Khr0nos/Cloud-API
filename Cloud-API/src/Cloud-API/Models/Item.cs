using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Cloud_API.Models {
    public class ItemContext : DbContext {
        public ItemContext(DbContextOptions<ItemContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>().HasKey(k => k.ID);
        }

        public DbSet<Item> Items { get; set; }
    }

    public class Item {
        public int ID { get; set; }
        public DateTime Data { get; set; }
        public string Equip { get; set; }
        public double Value { get; set; }
        public string Aux { get; set; }

        public void Update(Item nouItem) {
            Data = DateTime.Now;
            Equip = nouItem.Equip;
            Value = nouItem.Value;
            Aux = nouItem.Aux;
        }
    }
}
