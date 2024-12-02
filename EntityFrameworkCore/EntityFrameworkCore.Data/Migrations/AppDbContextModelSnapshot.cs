﻿// <auto-generated />
using System;
using EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EntityFrameworkCore.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("EntityFrameworkCore.Domain.Coach", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Coaches");
                });

            modelBuilder.Entity("EntityFrameworkCore.Domain.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("TeamId"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("TeamId");

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            TeamId = 1,
                            CreatedDate = new DateTime(2024, 12, 2, 1, 43, 35, 285, DateTimeKind.Unspecified).AddTicks(3667),
                            Name = "測試隊伍1"
                        },
                        new
                        {
                            TeamId = 2,
                            CreatedDate = new DateTime(2024, 12, 2, 1, 43, 35, 285, DateTimeKind.Unspecified).AddTicks(3675),
                            Name = "測試隊伍2"
                        },
                        new
                        {
                            TeamId = 3,
                            CreatedDate = new DateTime(2024, 12, 2, 1, 43, 35, 285, DateTimeKind.Unspecified).AddTicks(3676),
                            Name = "測試隊伍3"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}