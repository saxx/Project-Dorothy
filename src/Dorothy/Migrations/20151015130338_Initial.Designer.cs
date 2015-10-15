using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Dorothy.Models;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace Dorothy.Migrations
{
    [DbContext(typeof(Db))]
    partial class Initial
    {
        public override string Id
        {
            get { return "20151015130338_Initial"; }
        }

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta7-15540")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn);

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

                    b.Key("Id");
                });
        }
    }
}
