﻿// <auto-generated />
using System;
using HealthCatalystUserSearchAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HealthCatalystUserSearchAPI.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20190919184739_FirstAddition")]
    partial class FirstAddition
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HealthCatalystUserSearchAPI.Context.Addresses", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<string>("Street1")
                        .IsRequired();

                    b.Property<string>("Street2");

                    b.Property<string>("ZipCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("HealthCatalystUserSearchAPI.Context.Interests", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("InterestName")
                        .IsRequired();

                    b.Property<string>("InterestType");

                    b.HasKey("Id");

                    b.ToTable("Interests");
                });

            modelBuilder.Entity("HealthCatalystUserSearchAPI.Context.Users", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<Guid>("MyAddressId");

                    b.HasKey("Id");

                    b.HasIndex("MyAddressId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HealthCatalystUserSearchAPI.Context.UserToInterest", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("InterestId");

                    b.HasKey("UserId", "InterestId");

                    b.HasIndex("InterestId");

                    b.ToTable("UserToInterest");
                });

            modelBuilder.Entity("HealthCatalystUserSearchAPI.Context.Users", b =>
                {
                    b.HasOne("HealthCatalystUserSearchAPI.Context.Addresses", "MyAddress")
                        .WithMany("Users")
                        .HasForeignKey("MyAddressId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HealthCatalystUserSearchAPI.Context.UserToInterest", b =>
                {
                    b.HasOne("HealthCatalystUserSearchAPI.Context.Interests", "Interest")
                        .WithMany("Users")
                        .HasForeignKey("InterestId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HealthCatalystUserSearchAPI.Context.Users", "User")
                        .WithMany("MyInterests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
