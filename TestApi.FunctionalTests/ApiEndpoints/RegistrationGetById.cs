using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TestApi.DTO;
using TestApi.FunctionalTests.Data;
using Xunit;

namespace TestApi.FunctionalTests.ApiEndpoints
{
    [Collection("Sequential")]
    public class RegistrationGetById : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient http;

        public RegistrationGetById(CustomWebApplicationFactory<Startup> factory)
        {
            http = factory.CreateClient();
        }
        [Fact]
        public async Task ReturnsNotFound()
        {
            var result = await http.PostAsJsonAsync("/api/v1/registrations", 
                SeedData.TestRegistration());
            Guid id = Guid.NewGuid();
            result = await http.GetAsync("/api/v1/registrations/" + id);
            Assert.Equal(HttpStatusCode.NotFound,result.StatusCode);
            var content = await result.Content.ReadFromJsonAsync<ErrorResponse>();
            Assert.NotNull(content);
            Assert.Equal(ErrorCodes.InternalServerError.ToString("G"),content.Error.Code);
            Assert.Equal("Registration not found",content.Error.Message);
        }
        
        [Fact]
        public async Task ReturnsRegistrationById()
        {
            Guid id = Guid.NewGuid();
            var data = SeedData.TestRegistration();
            data.Id = id;
            var result = await http.PostAsJsonAsync("/api/v1/registrations", data);

            result =  await http.GetAsync("/api/v1/registrations/" + id);
            Assert.Equal(HttpStatusCode.OK,result.StatusCode);
            var content = await result.Content.ReadFromJsonAsync<GetRegistrationResponse>();
            Assert.NotNull(content);
            Assert.Equal(id,content.Id);
            Assert.NotNull(content.Person);
            Assert.NotEqual(default,content.RegistrationDate);
        }
        
    }
}