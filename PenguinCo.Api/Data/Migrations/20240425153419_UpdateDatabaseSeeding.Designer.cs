﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PenguinCo.Api.Data;

#nullable disable

namespace PenguinCo.Api.Data.Migrations
{
    [DbContext(typeof(PenguinCoContext))]
    [Migration("20240425153419_UpdateDatabaseSeeding")]
    partial class UpdateDatabaseSeeding
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PenguinCo.Api.Entities.Stock", b =>
                {
                    b.Property<int>("StockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockId"));

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("StockItemId")
                        .HasColumnType("int");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.HasKey("StockId");

                    b.HasIndex("StockItemId");

                    b.HasIndex("StoreId");

                    b.ToTable("Stock");

                    b.HasData(
                        new
                        {
                            StockId = 1,
                            Quantity = 10,
                            StockItemId = 1,
                            StoreId = 1
                        },
                        new
                        {
                            StockId = 2,
                            Quantity = 5,
                            StockItemId = 2,
                            StoreId = 1
                        },
                        new
                        {
                            StockId = 3,
                            Quantity = 15,
                            StockItemId = 8,
                            StoreId = 2
                        },
                        new
                        {
                            StockId = 4,
                            Quantity = 15,
                            StockItemId = 9,
                            StoreId = 2
                        },
                        new
                        {
                            StockId = 5,
                            Quantity = 5,
                            StockItemId = 10,
                            StoreId = 2
                        },
                        new
                        {
                            StockId = 6,
                            Quantity = 20,
                            StockItemId = 11,
                            StoreId = 2
                        });
                });

            modelBuilder.Entity("PenguinCo.Api.Entities.StockItem", b =>
                {
                    b.Property<int>("StockItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockItemId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StockItemId");

                    b.ToTable("StockItems");

                    b.HasData(
                        new
                        {
                            StockItemId = 1,
                            Name = "Pingu"
                        },
                        new
                        {
                            StockItemId = 2,
                            Name = "Pinga"
                        },
                        new
                        {
                            StockItemId = 3,
                            Name = "Tux"
                        },
                        new
                        {
                            StockItemId = 4,
                            Name = "Tuxedosam"
                        },
                        new
                        {
                            StockItemId = 5,
                            Name = "Suica"
                        },
                        new
                        {
                            StockItemId = 6,
                            Name = "Donpen"
                        },
                        new
                        {
                            StockItemId = 7,
                            Name = "Pen Pen"
                        },
                        new
                        {
                            StockItemId = 8,
                            Name = "Private"
                        },
                        new
                        {
                            StockItemId = 9,
                            Name = "Skipper"
                        },
                        new
                        {
                            StockItemId = 10,
                            Name = "Kowalski"
                        },
                        new
                        {
                            StockItemId = 11,
                            Name = "Rico"
                        });
                });

            modelBuilder.Entity("PenguinCo.Api.Entities.Store", b =>
                {
                    b.Property<int>("StoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("Updated")
                        .HasColumnType("date");

                    b.HasKey("StoreId");

                    b.ToTable("Stores");

                    b.HasData(
                        new
                        {
                            StoreId = 1,
                            Address = "Shrewsbury, West Midlands, England",
                            Name = "PenguinCo Shrewsbury",
                            Updated = new DateOnly(2024, 4, 25)
                        },
                        new
                        {
                            StoreId = 2,
                            Address = "Lüderitz, ǁKaras Region, Namibia",
                            Name = "PenguinCo Namibia",
                            Updated = new DateOnly(2024, 4, 25)
                        });
                });

            modelBuilder.Entity("PenguinCo.Api.Entities.Stock", b =>
                {
                    b.HasOne("PenguinCo.Api.Entities.StockItem", "StockItem")
                        .WithMany()
                        .HasForeignKey("StockItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PenguinCo.Api.Entities.Store", null)
                        .WithMany("Stock")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StockItem");
                });

            modelBuilder.Entity("PenguinCo.Api.Entities.Store", b =>
                {
                    b.Navigation("Stock");
                });
#pragma warning restore 612, 618
        }
    }
}
