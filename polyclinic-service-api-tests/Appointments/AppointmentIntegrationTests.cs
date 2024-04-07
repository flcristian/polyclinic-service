using System.Net;
using System.Text;
using Newtonsoft.Json;
using polyclinic_service_api_tests.Appointments.Helpers;
using polyclinic_service_api_tests.Infrastructure;
using polyclinic_service.Appointments.Models;

namespace polyclinic_service_api_tests.Appointments
{
    public class AppointmentsIntegrationTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public AppointmentsIntegrationTests(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllAppointments_AppointmentsFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createAppointmentRequest = TestsAppointmentsHelper.CreateTestCreateAppointmentRequest(1);
            var content = new StringContent(JsonConvert.SerializeObject(createAppointmentRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/Appointments/create", content);
            
            var response = await _client.GetAsync("/api/v1/Appointments/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAppointmentById_AppointmentFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createAppointmentRequest = TestsAppointmentsHelper.CreateTestCreateAppointmentRequest(2);
            var content = new StringContent(JsonConvert.SerializeObject(createAppointmentRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/Appointments/create", content);

            var response = await _client.GetAsync("/api/v1/Appointments/appointment/2");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAppointmentById_AppointmentNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/Appointments/9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateAppointment_ValidRequest_ReturnsCreatedStatusCode_ValidResponse()
        {
            var createAppointmentRequest = TestsAppointmentsHelper.CreateTestCreateAppointmentRequest(3);
            var content = new StringContent(JsonConvert.SerializeObject(createAppointmentRequest), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/Appointments/create", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAppointment_ValidRequest_ReturnsAcceptedStatusCode_ValidResponse()
        {
            var createAppointmentRequest = TestsAppointmentsHelper.CreateTestCreateAppointmentRequest(4);
            var content = new StringContent(JsonConvert.SerializeObject(createAppointmentRequest), Encoding.UTF8, "application/json");
            var createdResponse = await _client.PostAsync("/api/v1/Appointments/create", content);
            var responseString = await createdResponse.Content.ReadAsStringAsync();
            var createdAppointment = JsonConvert.DeserializeObject<Appointment>(responseString);

            
            var updateAppointmentRequest = TestsAppointmentsHelper.CreateTestUpdateAppointmentRequest(createdAppointment.Id);
            var updateContent = new StringContent(JsonConvert.SerializeObject(updateAppointmentRequest), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/v1/Appointments/update", updateContent);

            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAppointment_AppointmentNotFound_ReturnsNotFoundStatusCode()
        {
            var updateAppointmentRequest = TestsAppointmentsHelper.CreateTestUpdateAppointmentRequest(9999);
            var content = new StringContent(JsonConvert.SerializeObject(updateAppointmentRequest), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/v1/Appointments/update", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteAppointment_ValidRequest_ReturnsAcceptedStatusCode_ValidResponse()
        {
            var createAppointmentRequest = TestsAppointmentsHelper.CreateTestCreateAppointmentRequest(5);
            var content = new StringContent(JsonConvert.SerializeObject(createAppointmentRequest), Encoding.UTF8, "application/json");
            var createdResponse = await _client.PostAsync("/api/v1/Appointments/create", content);
            var responseString = await createdResponse.Content.ReadAsStringAsync();
            var createdAppointment = JsonConvert.DeserializeObject<Appointment>(responseString);
            
            var response = await _client.DeleteAsync($"/api/v1/Appointments/delete/{createdAppointment.Id}");

            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        }

        [Fact]
        public async Task DeleteAppointment_AppointmentNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.DeleteAsync("/api/v1/Appointments/delete/9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CheckAvailabilityForDay_AvailabilityFound_ReturnsOkStatusCode_ValidResponse()
        {
            var response = await _client.GetAsync("/api/v1/Appointments/check_availability_for_day/?userId=1&year=2024&month=1&day=1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CheckAvailabilityForWeek_AvailabilityFound_ReturnsOkStatusCode_ValidResponse()
        {
            var response = await _client.GetAsync("/api/v1/Appointments/check_availability_for_week/?userId=1&year=2024&weekNumber=1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CheckAvailabilityForMonth_AvailabilityFound_ReturnsOkStatusCode_ValidResponse()
        {
            var response = await _client.GetAsync("/api/v1/Appointments/check_availability_for_month/?userId=1&year=2024&month=1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CheckAvailabilityForInterval_AvailabilityFound_ReturnsOkStatusCode_ValidResponse()
        {
            var response = await _client.GetAsync("/api/v1/Appointments/check_availability_for_interval/?userId=1&startDateYear=2024&startDateMonth=1&startDateDay=1&endDateYear=2024&endDateMonth=1&endDateDay=1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
