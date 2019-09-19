using HealthCatalystUserSearchAPI.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace HealthCatalystUserSearchAPI.Data
{
    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Interests> Interests { get; set; }
        public DbSet<Addresses> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetUpModelRelationships(modelBuilder);
            SeedDatabase(modelBuilder);
        }

        /// <summary>
        /// Setup the One-to-Many and Many-To-Many relationships
        /// </summary>
        /// <param name="modelBuilder">The current modelBuilder</param>
        private void SetUpModelRelationships(ModelBuilder modelBuilder)
        {
            // Many-To-Many Setup
            modelBuilder.Entity<UserToInterest>()
                .HasKey(u => new { u.UserId, u.InterestId });

            modelBuilder.Entity<UserToInterest>()
                .HasOne(u => u.User)
                .WithMany(b => b.MyInterests)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserToInterest>()
                .HasOne(i => i.Interest)
                .WithMany(c => c.Users)
                .HasForeignKey(i => i.InterestId);

            // One-To-Many Setup
            modelBuilder.Entity<Addresses>()
                .HasMany(c => c.Users)
                .WithOne(e => e.MyAddress);
        }

        /// <summary>
        /// Add seed data to the database.
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void SeedDatabase(ModelBuilder modelBuilder)
        {
            // Load Data
            var addressOne = new Addresses()
            {
                Id = new Guid("9e2a29f7-9fa7-4cfa-b90c-6aacc2019729"),
                Street1 = "1 Tree Place",
                City = "ThatCity",
                State = "ThatState",
                Country = "TheCountry",
                ZipCode = "010123"
            };

            var addressTwo = new Addresses()
            {
                Id = new Guid("1869874b-3a8a-4800-a247-21187815c407"),
                Street1 = "45 Pike Street",
                City = "TheCity",
                State = "TheState",
                Country = "ThatCountry",
                ZipCode = "010321"
            };

            var userOne = new
            {
                Id = new Guid("e04a1983-be52-428c-bd3c-e6004e3a5992"),
                FirstName = "John",
                LastName = "James",
                MyAddressId = addressOne.Id
            };

            var userTwo = new
            {
                Id = new Guid("2517d71a-0152-48b1-bf16-2ab75be91a6f"),
                FirstName = "Chris",
                LastName = "Handle",
                MyAddressId = addressTwo.Id
            };

            var userThree = new
            {
                Id = new Guid("feaf87e5-5bf7-4b7a-a8fc-22b0b4c06aeb"),
                FirstName = "Mukesh",
                LastName = "Mukesh",
                MyAddressId = addressTwo.Id
            };

            var interestOne = new
            {
                Id = new Guid("09bd236a-673c-4d06-a259-549008f22b03"),
                InterestName = "Soccer",
                InterestType = "Sport"
            };

            var interestTwo = new
            {
                Id = new Guid("e13dc427-55ec-4f13-932c-ab8d5b1d08f4"),
                InterestName = "Cooking"
            };

            modelBuilder.Entity<Addresses>().HasData(
                addressOne,
                addressTwo);


            modelBuilder.Entity<Interests>().HasData(
                interestOne,
                interestTwo);


            modelBuilder.Entity<Users>().HasData(
                userOne,
                userTwo,
                userThree);

            modelBuilder.Entity<UserToInterest>().HasData(
                new { UserId = userOne.Id, InterestId = interestTwo.Id },
                new { UserId = userThree.Id, InterestId = interestTwo.Id },
                new { UserId = userTwo.Id, InterestId = interestOne.Id });

        }
    }
}
