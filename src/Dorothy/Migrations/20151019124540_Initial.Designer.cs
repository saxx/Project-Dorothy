using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Dorothy.Models;

namespace Dorothy.Migrations
{
    [DbContext(typeof(Db))]
    [Migration("20151019124540_Initial")]
    partial class Initial
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

                    b.Property<bool>("HasInvitation");

                    b.Property<bool>("IsOptional");

                    b.Property<string>("Names");

                    b.Property<string>("Notes");

                    b.HasKey("Id");
                });
        }
    }
}
