using Dorothy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dorothy.Migrations
{
    [DbContext(typeof(Db))]
    [Migration("20151109190521_Rsvps")]
    partial class Rsvps
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-beta8-15964")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dorothy.Models.Guest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdultCount");

                    b.Property<int>("ChildCount");

                    b.Property<string>("Group");

                    b.Property<bool>("HasEmail");

                    b.Property<bool>("HasInvitation");

                    b.Property<bool>("IsOptional");

                    b.Property<string>("Names");

                    b.Property<string>("Notes");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Dorothy.Models.Rsvp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdultsCount");

                    b.Property<int>("ChildCount");

                    b.Property<string>("Name");

                    b.Property<string>("Note");

                    b.Property<int>("Type");

                    b.HasKey("Id");
                });
        }
    }
}
