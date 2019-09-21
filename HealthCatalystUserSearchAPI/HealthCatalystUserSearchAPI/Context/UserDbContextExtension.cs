using System;
using Microsoft.EntityFrameworkCore;

namespace HealthCatalystUserSearchAPI.Context
{
    public static class UserDbContextExtension
    {
        /// <summary>
        /// Add seed data to the database.
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void SeedDatabase(this ModelBuilder modelBuilder)
        {
            // Load Data
            var addressOne = new Addresses
            {
                Id = new Guid("9e2a29f7-9fa7-4cfa-b90c-6aacc2019729"),
                Street1 = "1 Tree Place",
                City = "ThatCity",
                State = "ThatState",
                Country = "TheCountry",
                ZipCode = "010123"
            };

            var addressTwo = new Addresses
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

            var interestOne = new Interests
            {
                Id = new Guid("09bd236a-673c-4d06-a259-549008f22b03"),
                InterestName = "Soccer",
                InterestType = "Sport"
            };

            var interestTwo = new Interests
            {
                Id = new Guid("e13dc427-55ec-4f13-932c-ab8d5b1d08f4"),
                InterestName = "Cooking"
            };

            var interestThree = new Interests
            {
                Id = new Guid("538afe79-3653-4061-b597-3011d79c3630"),
                InterestName = "Rugby",
                InterestType = "Sport"
            };

            var interestFour = new Interests
            {
                Id = new Guid("98d60074-3ad1-4803-807f-6922982665ae"),
                InterestName = "Baking"
            };

            modelBuilder.Entity<Addresses>().HasData(
                addressOne,
                addressTwo);


            modelBuilder.Entity<Interests>().HasData(
                interestOne,
                interestTwo,
                interestThree,
                interestFour);


            modelBuilder.Entity<Users>().HasData(
                userOne,
                userTwo,
                userThree);

            modelBuilder.Entity<UserToInterest>().HasData(
                new { UserId = userOne.Id, InterestId = interestTwo.Id },
                new { UserId = userThree.Id, InterestId = interestTwo.Id },
                new { UserId = userThree.Id, InterestId = interestFour.Id },
                new { UserId = userTwo.Id, InterestId = interestOne.Id },
                new { UserId = userTwo.Id, InterestId = interestThree.Id });

        }
    }
}
