using Microsoft.EntityFrameworkCore;

namespace Cloud_API.Models {
    public class ItemContext : DbContext {
        public ItemContext(DbContextOptions<ItemContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>().HasKey(k => k.Key);
        }

        public DbSet<Item> Items { get; set; }
    }

    public class Item {
        public int Key { get; } //no cal set si la BBDD assigna la PK
        public string Name { get; set; }
    }
}
