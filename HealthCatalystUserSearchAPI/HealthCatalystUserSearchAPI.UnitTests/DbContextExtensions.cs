using System;
using HealthCatalystUserSearchAPI.Context;

namespace HealthCatalystUserSearchAPI.UnitTests
{
    public static class DbContextExtensions
    {
        public static void Seed(this UserDbContext dbContext)
        {
            // Add entities for DbContext instance
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

            var userOne = new Users
            {
                Id = new Guid("e04a1983-be52-428c-bd3c-e6004e3a5992"),
                FirstName = "John",
                LastName = "James",
                MyAddress = addressOne
            };

            var userTwo = new Users
            {
                Id = new Guid("2517d71a-0152-48b1-bf16-2ab75be91a6f"),
                FirstName = "Chris",
                LastName = "Handle",
                MyAddress = addressTwo
            };

            var userThree = new Users
            {
                Id = new Guid("feaf87e5-5bf7-4b7a-a8fc-22b0b4c06aeb"),
                FirstName = "Mukesh",
                LastName = "Mukesh",
                MyAddress = addressTwo
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

            dbContext.Addresses.Add(addressOne);
            dbContext.Addresses.Add(addressTwo);

            dbContext.Interests.Add(interestOne);
            dbContext.Interests.Add(interestTwo);
            dbContext.Interests.Add(interestThree);
            dbContext.Interests.Add(interestFour);

            dbContext.Users.Add(userOne);
            dbContext.Users.Add(userTwo);
            dbContext.Users.Add(userThree);

            dbContext.UserToInterest.Add(
                new UserToInterest { UserId = userOne.Id, InterestId = interestTwo.Id });

            dbContext.UserToInterest.Add(
                new UserToInterest { UserId = userThree.Id, InterestId = interestTwo.Id });

            dbContext.UserToInterest.Add(
                new UserToInterest { UserId = userThree.Id, InterestId = interestFour.Id });

            dbContext.UserToInterest.Add(
                new UserToInterest { UserId = userTwo.Id, InterestId = interestOne.Id });

            dbContext.UserToInterest.Add(
                new UserToInterest { UserId = userTwo.Id, InterestId = interestThree.Id });
        }
    }
}
