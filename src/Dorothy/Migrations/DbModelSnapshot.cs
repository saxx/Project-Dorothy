using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Dorothy.Models;

namespace Dorothy.Migrations
{
    [DbContext(typeof(Db))]
    partial class DbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
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

                    b.ToTable("Guest");
                });

            modelBuilder.Entity("Dorothy.Models.Rsvp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdultsCount");

                    b.Property<int>("ChildCount");

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Name");

                    b.Property<string>("Note");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Rsvp");
                });
        }
    }
}
