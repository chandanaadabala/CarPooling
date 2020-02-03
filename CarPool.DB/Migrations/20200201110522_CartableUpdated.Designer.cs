﻿// <auto-generated />
using System;
using CarPool.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarPool.DB.Migrations
{
    [DbContext(typeof(CarpoolContext))]
    [Migration("20200201110522_CartableUpdated")]
    partial class CartableUpdated
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CarPool.DB.Models.Booking", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool?>("Approval")
                        .HasColumnType("bit");

                    b.Property<string>("BoardingID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DroppingID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RideID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("RowDeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RowModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Seats")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("BoardingID");

                    b.HasIndex("DroppingID");

                    b.HasIndex("RideID")
                        .HasName("RideID");

                    b.HasIndex("UserID")
                        .HasName("UserID");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("CarPool.DB.Models.Car", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberPlate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RowDeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RowModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("UserID")
                        .HasName("UserID");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("CarPool.DB.Models.Ride", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("int");

                    b.Property<int>("NumOfStopOvers")
                        .HasColumnType("int");

                    b.Property<decimal>("PricePerKM")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("RowDeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RowModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("UserID")
                        .HasName("UserID");

                    b.ToTable("Rides");
                });

            modelBuilder.Entity("CarPool.DB.Models.Station", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderNo")
                        .HasColumnType("int");

                    b.Property<string>("RideID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("RowDeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RowModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("RideID")
                        .HasName("RideID");

                    b.ToTable("Station");
                });

            modelBuilder.Entity("CarPool.DB.Models.User", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MobileNum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RowDeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RowModifiedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasName("Email");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = "1",
                            Email = "sam@gmail.com",
                            MobileNum = "0987654321",
                            Name = "SamYoung",
                            Password = "qazwsxedc"
                        });
                });

            modelBuilder.Entity("CarPool.DB.Models.Booking", b =>
                {
                    b.HasOne("CarPool.DB.Models.Station", "BoardingStation")
                        .WithMany()
                        .HasForeignKey("BoardingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarPool.DB.Models.Station", "DroppingStation")
                        .WithMany()
                        .HasForeignKey("DroppingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarPool.DB.Models.Ride", "Ride")
                        .WithMany()
                        .HasForeignKey("RideID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarPool.DB.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarPool.DB.Models.Car", b =>
                {
                    b.HasOne("CarPool.DB.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarPool.DB.Models.Ride", b =>
                {
                    b.HasOne("CarPool.DB.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarPool.DB.Models.Station", b =>
                {
                    b.HasOne("CarPool.DB.Models.Ride", "Ride")
                        .WithMany()
                        .HasForeignKey("RideID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
