using Microsoft.Data.Entity;

namespace Dorothy.Models
{
    public sealed class Db : DbContext
    {
        public Db()
        {
                Database.Migrate();
        }

        public DbSet<Guest> Guests { get; set; }
        public DbSet<Rsvp> Rsvps { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Guest>().HasKey(v => v.Id);
            builder.Entity<Rsvp>().HasKey(v => v.Id);

            base.OnModelCreating(builder);
        }
    }
}
