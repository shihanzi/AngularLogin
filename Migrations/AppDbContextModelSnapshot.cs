﻿// <auto-generated />
using System;
using AngularLogin.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AngularLogin.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("AngularLogin.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("AngularLogin.Models.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nickname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Perches")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("LocationId");

                    b.ToTable("locations");
                });

            modelBuilder.Entity("AngularLogin.Models.Lot", b =>
                {
                    b.Property<int>("LotId")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<decimal>("Perches")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ReceivedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("RepId1")
                        .HasColumnType("int");

                    b.Property<DateTime>("SoldDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("LotId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RepId1");

                    b.ToTable("Lots");
                });

            modelBuilder.Entity("AngularLogin.Models.Rep", b =>
                {
                    b.Property<int>("RepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepPhone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RepId");

                    b.ToTable("Reps");
                });

            modelBuilder.Entity("AngularLogin.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("AngularLogin.Models.Lot", b =>
                {
                    b.HasOne("AngularLogin.Models.Customer", null)
                        .WithMany("Lots")
                        .HasForeignKey("CustomerId");

                    b.HasOne("AngularLogin.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("LotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AngularLogin.Models.Location", "Location")
                        .WithMany("Lots")
                        .HasForeignKey("LotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AngularLogin.Models.Rep", "Rep")
                        .WithMany()
                        .HasForeignKey("LotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AngularLogin.Models.Rep", null)
                        .WithMany("Lots")
                        .HasForeignKey("RepId1");

                    b.Navigation("Customer");

                    b.Navigation("Location");

                    b.Navigation("Rep");
                });

            modelBuilder.Entity("AngularLogin.Models.Customer", b =>
                {
                    b.Navigation("Lots");
                });

            modelBuilder.Entity("AngularLogin.Models.Location", b =>
                {
                    b.Navigation("Lots");
                });

            modelBuilder.Entity("AngularLogin.Models.Rep", b =>
                {
                    b.Navigation("Lots");
                });
#pragma warning restore 612, 618
        }
    }
}
