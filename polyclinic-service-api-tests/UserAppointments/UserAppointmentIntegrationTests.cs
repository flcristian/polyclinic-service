using System.Net;
using System.Text;
using Newtonsoft.Json;
using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service_api_tests.UserAppointments.Helpers;
using polyclinic_service_api_tests.Infrastructure;
using polyclinic_service.UserAppointments.Models;

namespace polyclinic_service_api_tests.UserAppointments
{
    public class UserAppointmentIntegrationTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public UserAppointmentIntegrationTests(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllUserAppointments_UsersFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createUserAppointmentRequest = TestsUserAppointmentsHelpers.CreateUserAppointmentRequest(1, 1);
            var createUserAppointmentContent = new StringContent(JsonConvert.SerializeObject(createUserAppointmentRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/UserAppointments/create", createUserAppointmentContent);

            var response = await _client.GetAsync("/api/v1/UserAppointments/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetUserAppointmentById_UserAppointmentFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createUserAppointmentRequest = TestsUserAppointmentsHelpers.CreateUserAppointmentRequest(1, 1);
            var createUserAppointmentContent = new StringContent(JsonConvert.SerializeObject(createUserAppointmentRequest), Encoding.UTF8, "application/json");
            var createUserAppointmentResponse = await _client.PostAsync("/api/v1/UserAppointments/create", createUserAppointmentContent);
            var responseString = await createUserAppointmentResponse.Content.ReadAsStringAsync();
            var createdUserAppointment = JsonConvert.DeserializeObject<UserAppointment>(responseString);

            var response = await _client.GetAsync($"/api/v1/UserAppointments/userAppointment/{createdUserAppointment.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetUserAppointmentById_UserAppointmentNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/UserAppointments/userAppointment/9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateUserAppointment_ValidRequest_ReturnsCreatedStatusCode_ValidResponse()
        {
            var userAppointmentRequest = TestsUserAppointmentsHelpers.CreateUserAppointmentRequest(1, 1);
            var content = new StringContent(JsonConvert.SerializeObject(userAppointmentRequest), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/UserAppointments/create", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task CreateUserAppointment_InvalidRequest_ReturnsBadRequestStatusCode()
        {
            var userAppointmentRequest = TestsUserAppointmentsHelpers.CreateUserAppointmentRequest(-1, 1);
            var content = new StringContent(JsonConvert.SerializeObject(userAppointmentRequest), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/UserAppointments/create", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateUserAppointment_ValidRequest_ReturnsAcceptedStatusCode_ValidResponse()
        {
            var createUserAppointmentRequest = TestsUserAppointmentsHelpers.CreateUserAppointmentRequest(1, 1);
            var createUserAppointmentContent = new StringContent(JsonConvert.SerializeObject(createUserAppointmentRequest), Encoding.UTF8, "application/json");
            var createUserAppointmentResponse = await _client.PostAsync("/api/v1/UserAppointments/create", createUserAppointmentContent);
            var responseString = await createUserAppointmentResponse.Content.ReadAsStringAsync();
            var createdUserAppointment = JsonConvert.DeserializeObject<UserAppointment>(responseString);

            var updateUserAppointmentRequest = TestsUserAppointmentsHelpers.UpdateUserAppointmentRequest(createdUserAppointment.Id, 1, 2);
            var content = new StringContent(JsonConvert.SerializeObject(updateUserAppointmentRequest), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/v1/UserAppointments/update", content);

            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        }

        [Fact]
        public async Task UpdateUserAppointment_InvalidUserAppointment_ReturnsNotFoundStatusCode()
        {
            var updateUserAppointmentRequest = TestsUserAppointmentsHelpers.UpdateUserAppointmentRequest(9999, 1, 2);
            var content = new StringContent(JsonConvert.SerializeObject(updateUserAppointmentRequest), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/v1/UserAppointments/update", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteUserAppointment_UserAppointmentExists_ReturnsAcceptedStatusCode_ValidResponse()
        {
            var createUserAppointmentRequest = TestsUserAppointmentsHelpers.CreateUserAppointmentRequest(1, 1);
            var createUserAppointmentContent = new StringContent(JsonConvert.SerializeObject(createUserAppointmentRequest), Encoding.UTF8, "application/json");
            var createUserAppointmentResponse = await _client.PostAsync("/api/v1/UserAppointments/create", createUserAppointmentContent);
            var responseString = await createUserAppointmentResponse.Content.ReadAsStringAsync();
            var createdUserAppointment = JsonConvert.DeserializeObject<UserAppointment>(responseString);

            var response = await _client.DeleteAsync($"/api/v1/UserAppointments/delete/{createdUserAppointment.Id}");

            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        }

        [Fact]
        public async Task DeleteUserAppointment_UserAppointmentDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var response = await _client.DeleteAsync("/api/v1/UserAppointments/delete/9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
