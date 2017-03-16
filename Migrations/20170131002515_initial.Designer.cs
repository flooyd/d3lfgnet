using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using d3lfg.Models;

namespace d3lfg.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20170131002515_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("d3lfg.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Activity");

                    b.Property<string>("BNet");

                    b.Property<string>("Description");

                    b.Property<int>("GRLevel");

                    b.Property<int>("MinParagon");

                    b.Property<string>("Name");

                    b.Property<string>("Region");

                    b.Property<bool>("WillCarry");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });
        }
    }
}
