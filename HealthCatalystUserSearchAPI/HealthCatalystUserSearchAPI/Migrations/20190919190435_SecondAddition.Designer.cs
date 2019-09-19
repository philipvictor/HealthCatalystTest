﻿// <auto-generated />
using System;
using HealthCatalystUserSearchAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HealthCatalystUserSearchAPI.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20190919190435_SecondAddition")]
    partial class SecondAddition
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

                    b.HasData(
                        new { Id = new Guid("9e2a29f7-9fa7-4cfa-b90c-6aacc2019729"), City = "ThatCity", Country = "TheCountry", State = "ThatState", Street1 = "1 Tree Place", ZipCode = "010123" },
                        new { Id = new Guid("1869874b-3a8a-4800-a247-21187815c407"), City = "TheCity", Country = "ThatCountry", State = "TheState", Street1 = "45 Pike Street", ZipCode = "010321" }
                    );
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

                    b.HasData(
                        new { Id = new Guid("09bd236a-673c-4d06-a259-549008f22b03"), InterestName = "Soccer", InterestType = "Sport" },
                        new { Id = new Guid("e13dc427-55ec-4f13-932c-ab8d5b1d08f4"), InterestName = "Cooking" }
                    );
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

                    b.HasData(
                        new { Id = new Guid("e04a1983-be52-428c-bd3c-e6004e3a5992"), FirstName = "John", LastName = "James", MyAddressId = new Guid("9e2a29f7-9fa7-4cfa-b90c-6aacc2019729") },
                        new { Id = new Guid("2517d71a-0152-48b1-bf16-2ab75be91a6f"), FirstName = "Chris", LastName = "Handle", MyAddressId = new Guid("1869874b-3a8a-4800-a247-21187815c407") },
                        new { Id = new Guid("feaf87e5-5bf7-4b7a-a8fc-22b0b4c06aeb"), FirstName = "Mukesh", LastName = "Mukesh", MyAddressId = new Guid("1869874b-3a8a-4800-a247-21187815c407") }
                    );
                });

            modelBuilder.Entity("HealthCatalystUserSearchAPI.Context.UserToInterest", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("InterestId");

                    b.HasKey("UserId", "InterestId");

                    b.HasIndex("InterestId");

                    b.ToTable("UserToInterest");

                    b.HasData(
                        new { UserId = new Guid("e04a1983-be52-428c-bd3c-e6004e3a5992"), InterestId = new Guid("e13dc427-55ec-4f13-932c-ab8d5b1d08f4") },
                        new { UserId = new Guid("feaf87e5-5bf7-4b7a-a8fc-22b0b4c06aeb"), InterestId = new Guid("e13dc427-55ec-4f13-932c-ab8d5b1d08f4") },
                        new { UserId = new Guid("2517d71a-0152-48b1-bf16-2ab75be91a6f"), InterestId = new Guid("09bd236a-673c-4d06-a259-549008f22b03") }
                    );
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
