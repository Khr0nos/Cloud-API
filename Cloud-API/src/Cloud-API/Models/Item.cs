using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Cloud_API.Models {
    public class ItemContext : DbContext {
        public ItemContext(DbContextOptions<ItemContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>().HasKey(k => k.Key);
        }

        //public Item Find() {
            
        //}

        public DbSet<Item> Items { get; set; }
    }

    public class Item {
        public int Key { get; set; }
        public string Name { get; set; }
    }
}
