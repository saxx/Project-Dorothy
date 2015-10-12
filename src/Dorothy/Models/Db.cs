using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Migrations;

namespace Dorothy.Models
{
    public sealed class Db : DbContext
    {
        public Db()
        {
                Database.Migrate();
        }

        public DbSet<Guest> Guests { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Guest>().Key(v => v.Id);

            base.OnModelCreating(builder);
        }
    }
}
