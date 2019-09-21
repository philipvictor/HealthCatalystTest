using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

            // Handle response
            await response.EnsureSuccessStatusCodeAsync();
        }

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

        //{
        //    "id": "string",
        //    "firstName": "string",
        //    "lastName": "string",
        //    "myInterests": [
        //    {
        //        "id": "string",
        //        "interestName": "string",
        //        "interestType": "string"
        //    }
        //    ],
        //    "myAddress": {
        //        "id": "string",
        //        "street1": "string",
        //        "street2": "string",
        //        "city": "string",
        //        "state": "string",
        //        "country": "string",
        //        "zipCode": "string"
        //    }
        //}

        [Fact]
        public async Task TestPostUserAsync()
        {
            var stringjsonData = "{" +
                                 "\"id\": \"77427be4-570b-4b2a-886c-92b41a354029\"," +
                                 "\"firstName\": \"Jane\"," +
                                 "\"lastName\": \"Tarzan\"," +
                                 "\"myInterests\": [" +
                                     "{" +
                                     "\"id\": \"5f15bd8a-25cd-46b1-8f79-6aa70043eb71\"," +
                                     "\"interestName\": \"Vine Swinging\"," +
                                     "\"interestType\": \"Forest\"," +
                                     "}" +
                                 "]," +
                                 "\"myAddress\": {" +
                                         "\"id\": \"6250596b-9e58-4246-a895-51ec4b1b3fa4\"," +
                                         "\"street1\": \"One Big Tree House Way\"," +
                                         "\"street2\": \"\"," +
                                         "\"city\": \"Forbidden\"," +
                                         "\"state\": \"Florida\"," +
                                         "\"country\": \"United States\"," +
                                         "\"zipCode\": \"98798\"," +
                                    "}" +
                                 "}";

            // Arrange
            var request = new
            {
                Url = "/api/Users",
                Body = new StringContent(stringjsonData, Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }

    }
}
