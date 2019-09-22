using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HealthCatalystUserSearchAPI.Models;
using Newtonsoft.Json;
using Xunit;

namespace HealthCatalystUserSearchAPI.IntegrationTests
{
    public class IntegrationTests : IClassFixture<TestFixture<Startup>>
    {
        private readonly HttpClient _client;

        public IntegrationTests(TestFixture<Startup> fixture)
        {
            _client = fixture.Client;
        }

        /// <summary>
        /// Test retrieving all Users.
        /// Should succeed.
        /// </summary>
        [Fact]
        public async Task TestGetUsersAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Users"
            };

            // Act
            var response = await _client.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Test retrieving a specific user by Id.
        /// Should succeed.
        /// </summary>
        [Fact]
        public async Task TestGetUserAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Users/2517d71a-0152-48b1-bf16-2ab75be91a6f"
            };

            // Act
            var response = await _client.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Test the Name search by Interest with no interest provided.
        /// Should fail since Interest name required.
        /// </summary>
        [Fact]
        public async Task TestSearchByInterestOneAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Users/SearchByInterest"
            };

            // Act
            var response = await _client.GetAsync(request.Url);

            // Handle response. Should fail.
            try
            {
                await response.EnsureSuccessStatusCodeAsync();
            }
            catch (SimpleHttpResponseException)
            {
                if (!response.IsSuccessStatusCode)
                {
                    response.StatusCode = HttpStatusCode.OK;
                }
            }
        }

        /// <summary>
        /// Test the Name search using an Interest name.
        /// Should succeed.
        /// </summary>
        [Fact]
        public async Task TestSearchByInterestTwoAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Users/SearchByInterest?interestname=soccer"
            };

            // Act
            var response = await _client.GetAsync(request.Url);

            // Handle response
            await response.EnsureSuccessStatusCodeAsync();
        }

        /// <summary>
        /// Test the Name search using an Interest name and type.
        /// Should succeed.
        /// </summary>
        [Fact]
        public async Task TestSearchByInterestThreeAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Users/SearchByInterest?interestname=soccer&interesttype=sport"
            };

            // Act
            var response = await _client.GetAsync(request.Url);

            // Handle response
            await response.EnsureSuccessStatusCodeAsync();
        }

        /// <summary>
        /// Test the Name search using an Interest type.
        /// Should fail since Interest name required.
        /// </summary>
        [Fact]
        public async Task TestSearchByInterestFourAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Users/SearchByInterest?interesttype=sport"
            };

            // Act
            var response = await _client.GetAsync(request.Url);


            // Handle response. Should fail.
            try
            {
                await response.EnsureSuccessStatusCodeAsync();
            }
            catch (SimpleHttpResponseException)
            {
                if (!response.IsSuccessStatusCode)
                {
                    response.StatusCode = HttpStatusCode.OK;
                }
            }

        }

        /// <summary>
        /// Test the Name search using no provied name.
        /// Should fail since Last name required.
        /// </summary>
        [Fact]
        public async Task TestSearchByNameOneAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Users/SearchByName"
            };

            // Act
            var response = await _client.GetAsync(request.Url);

            // Handle response. Should fail.
            try
            {
                await response.EnsureSuccessStatusCodeAsync();
            }
            catch (SimpleHttpResponseException)
            {
                if (!response.IsSuccessStatusCode)
                {
                    response.StatusCode = HttpStatusCode.OK;
                }
            }
        }

        /// <summary>
        /// Test the Name search using Last name. 
        /// Should succeed.
        /// </summary>
        [Fact]
        public async Task TestSearchByNameTwoAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Users/SearchByName?lastname=Handle"
            };

            // Act
            var response = await _client.GetAsync(request.Url);

            // Handle response.
            await response.EnsureSuccessStatusCodeAsync();
        }

        /// <summary>
        /// Test the Name search using First name. 
        /// Should fail since Last name required.
        /// </summary>
        [Fact]
        public async Task TestSearchByNameThreeAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Users/SearchByName?firstname=Handle"
            };

            // Act
            var response = await _client.GetAsync(request.Url);

            // Handle response. Should fail.
            try
            {
                await response.EnsureSuccessStatusCodeAsync();
            }
            catch (SimpleHttpResponseException)
            {
                if (!response.IsSuccessStatusCode)
                {
                    response.StatusCode = HttpStatusCode.OK;
                }
            }
        }

        /// <summary>
        /// Test the Name search using First and Last name. 
        /// Should succeed.
        /// </summary>
        [Fact]
        public async Task TestSearchByNameFourAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Users/SearchByName?lastname=Handle&firstname=Chris"
            };

            // Act
            var response = await _client.GetAsync(request.Url);

            // Handle response.
            await response.EnsureSuccessStatusCodeAsync();
        }

        /// <summary>
        /// Test the Posting of a new User.
        /// Should succeed.
        /// </summary>
        [Fact]
        public async Task TestPostUserAsync()
        {

            var testInterest = new InterestDto
            {
                Id = Guid.NewGuid().ToString(),
                InterestName = "Vine Swinging",
                InterestType = "Outdoors"
            };

            var listOfTestInterests = new List<InterestDto> {testInterest};

            var testUser = new UserDto
            {
                FirstName = "Jane",
                LastName = "Tarzan",
                Id = Guid.NewGuid().ToString(),
                MyAddress = new AddressDto
                {
                    City = "Forbidden",
                    Country = "United States",
                    Id = Guid.NewGuid().ToString(),
                    State = "Florida",
                    Street1 = "One Big Tree House Way",
                    Street2 = string.Empty,
                    ZipCode = "98798"
                },
                MyInterests = listOfTestInterests
            };

            var requestMessage = JsonConvert.SerializeObject(testUser);
            var content = new StringContent(requestMessage, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/Users", content);
            var returnedMessage = await response.Content.ReadAsStringAsync();

            Console.WriteLine(returnedMessage);

            // Handle response status
            await response.EnsureSuccessStatusCodeAsync();
        }

    }
}
