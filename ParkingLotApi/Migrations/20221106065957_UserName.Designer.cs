﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkingLotApi.Repository;

#nullable disable

namespace ParkingLotApi.Migrations
{
    [DbContext(typeof(ParkingLotContext))]
    [Migration("20221106065957_UserName")]
    partial class UserName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ParkingLotApi.Model.OrderEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CloseTime")
                        .HasColumnType("longtext");

                    b.Property<string>("CreationTime")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsClose")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("ParkingLotEntityId")
                        .HasColumnType("int");

                    b.Property<string>("ParkingLotName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PlateNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ParkingLotEntityId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ParkingLotApi.Model.ParkingLotEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ParkingLots");
                });

            modelBuilder.Entity("ParkingLotApi.Model.OrderEntity", b =>
                {
                    b.HasOne("ParkingLotApi.Model.ParkingLotEntity", null)
                        .WithMany("Orders")
                        .HasForeignKey("ParkingLotEntityId");
                });

            modelBuilder.Entity("ParkingLotApi.Model.ParkingLotEntity", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
