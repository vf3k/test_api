using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TestApi.Core.Entity;
using TestApi.DTO;
using TestApi.FunctionalTests.Data;
using Xunit;

namespace TestApi.FunctionalTests.ApiEndpoints
{
    [Collection("Sequential")]
    public class RegistrationCreate : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient http;

        public RegistrationCreate(CustomWebApplicationFactory<Startup> factory)
        {
            http = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsCreatedRegistrationId()
        {
            var result = await http.PostAsJsonAsync("/api/v1/registrations", 
                SeedData.TestRegistration());
            Assert.Equal(HttpStatusCode.Created,result.StatusCode);
            var content = await result.Content.ReadFromJsonAsync<RegistrationResponse>();
            Assert.NotNull(content);
            Assert.NotEqual(default(Guid),content.RegistrationId);
            
        }
        [Fact]
        public async Task ReturnsCreatedRegistrationIdWithoutOrganisation()
        {
            var result = await http.PostAsJsonAsync("/api/v1/registrations", 
                SeedData.TestRegistrationWithoutOrganisation());
            Assert.Equal(HttpStatusCode.Created,result.StatusCode);
            var content = await result.Content.ReadFromJsonAsync<RegistrationResponse>();
            Assert.NotNull(content);
            Assert.NotEqual(default(Guid),content.RegistrationId);
            
        }
        [Fact]
        public async Task FailsWithoutPerson()
        {
            var result = await http.PostAsJsonAsync("/api/v1/registrations", 
                SeedData.TestRegistrationWithoutPerson());
            Assert.Equal(HttpStatusCode.BadRequest,result.StatusCode);
            var content = await result.Content.ReadFromJsonAsync<ErrorResponse>();
            Assert.NotNull(content);
            Assert.Equal(ErrorCodes.ValidationFailed.ToString("G"),content.Error.Code);
            Assert.Equal("Person",content.FieldErrors.First().Field);
        }
        [Fact]
        public async Task FailsPersonAddressLine1Empty()
        {
            var result = await http.PostAsJsonAsync("/api/v1/registrations", 
                SeedData.TestRegistrationPersonAddressEmpty());
            Assert.Equal(HttpStatusCode.BadRequest,result.StatusCode);
            var content = await result.Content.ReadFromJsonAsync<ErrorResponse>();
            Assert.NotNull(content);
            Assert.Equal(ErrorCodes.ValidationFailed.ToString("G"),content.Error.Code);
            Assert.Equal("Person.Address.AddressLine1",content.FieldErrors.First().Field);
        }
        
    }
}