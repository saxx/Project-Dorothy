using Microsoft.Data.Entity;

namespace Dorothy.Models
{
    public sealed class Db : DbContext
    {
        public Db()
        {
            Database.EnsureCreated();
        }

        public DbSet<Guest> Guests { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Guest>().Key(v => v.Id);

            base.OnModelCreating(builder);
        }
    }
}
