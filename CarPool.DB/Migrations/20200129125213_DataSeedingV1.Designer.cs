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
    [Migration("20200129125213_DataSeedingV1")]
    partial class DataSeedingV1
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

                    b.Property<string>("Boarding")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dropping")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RideID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Seats")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("RideID");

                    b.HasIndex("UserID");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("CarPool.DB.Models.Ride", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Boarding")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BoardingDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Dropping")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DroppingDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumOfStopOvers")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Seats")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Rides");
                });

            modelBuilder.Entity("CarPool.DB.Models.StopOver", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("int");

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RideID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("RideID");

                    b.ToTable("StopOvers");
                });

            modelBuilder.Entity("CarPool.DB.Models.User", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CarName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MobileNum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberPlate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasName("Email");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = "1",
                            CarName = "Hyundai",
                            Email = "sam@gmail.com",
                            MobileNum = "0987654321",
                            Name = "SamYoung",
                            NumberPlate = "MH0237",
                            Password = "qazwsxedc"
                        });
                });

            modelBuilder.Entity("CarPool.DB.Models.Booking", b =>
                {
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

            modelBuilder.Entity("CarPool.DB.Models.Ride", b =>
                {
                    b.HasOne("CarPool.DB.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarPool.DB.Models.StopOver", b =>
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
