using System.Net;
using System.Text;
using Newtonsoft.Json;
using polyclinic_service_api_tests.Schedules.Helpers;
using polyclinic_service_api_tests.Infrastructure;

namespace polyclinic_service_api_tests.Schedules
{
    public class ScheduleIntegrationTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ScheduleIntegrationTests(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllSchedules_SchedulesFound_ReturnsOkStatusCode_ValidResponse()
        {
            var scheduleRequest = TestsSchedulesHelper.CreateCreateScheduleRequest(1, 2024, 15);
            var content = new StringContent(JsonConvert.SerializeObject(scheduleRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/Schedules/create", content);

            var response = await _client.GetAsync("/api/v1/Schedules/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetSchedulesByDoctorId_SchedulesFound_ReturnsOkStatusCode_ValidResponse()
        {
            var scheduleRequest = TestsSchedulesHelper.CreateCreateScheduleRequest(2, 2024, 15);
            var content = new StringContent(JsonConvert.SerializeObject(scheduleRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/Schedules/create", content);

            var response = await _client.GetAsync("/api/v1/Schedules/schedules/2");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetSchedulesByDoctorId_SchedulesNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/Schedules/doctor/9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetScheduleByDoctorIdAndWeekIdentity_ScheduleFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createScheduleRequest = TestsSchedulesHelper.CreateCreateScheduleRequest(3, 2024, 15);
            var content = new StringContent(JsonConvert.SerializeObject(createScheduleRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/Schedules/create", content);
            var response = await _client.GetAsync("/api/v1/Schedules/schedule/3?year=2024&weekNumber=15");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetScheduleByDoctorIdAndWeekIdentity_ScheduleNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/Schedules/schedule/999?year=2024&weekNumber=15");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateSchedule_ValidRequest_ReturnsCreatedStatusCode_ValidResponse()
        {
            var createScheduleRequest = TestsSchedulesHelper.CreateCreateScheduleRequest(4, 2024, 15);
            var content = new StringContent(JsonConvert.SerializeObject(createScheduleRequest), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/Schedules/create", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UpdateSchedule_ValidRequest_ReturnsAcceptedStatusCode_ValidResponse()
        {
            var createScheduleRequest = TestsSchedulesHelper.CreateCreateScheduleRequest(5, 2024, 15);
            var createContent = new StringContent(JsonConvert.SerializeObject(createScheduleRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/Schedules/create", createContent);

            var updateScheduleRequest = TestsSchedulesHelper.CreateUpdateScheduleRequest(5, 2024, 15);
            var content = new StringContent(JsonConvert.SerializeObject(updateScheduleRequest), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/v1/Schedules/update", content);

            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        }

        [Fact]
        public async Task UpdateSchedule_ScheduleNotFound_ReturnsNotFoundStatusCode()
        {
            var updateScheduleRequest = TestsSchedulesHelper.CreateUpdateScheduleRequest(6, 2024, 15);
            var content = new StringContent(JsonConvert.SerializeObject(updateScheduleRequest), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/v1/Schedules/update", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteSchedule_ValidRequest_ReturnsAcceptedStatusCode_ValidResponse()
        {
            var createScheduleRequest = TestsSchedulesHelper.CreateCreateScheduleRequest(7, 2024, 15);
            var createContent = new StringContent(JsonConvert.SerializeObject(createScheduleRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/Schedules/create", createContent);

            var deleteScheduleRequest = TestsSchedulesHelper.CreateDeleteScheduleRequest(7, 2024, 15);
            var content = new StringContent(JsonConvert.SerializeObject(deleteScheduleRequest), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/v1/Schedules/delete", content);

            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        }

        [Fact]
        public async Task DeleteSchedule_ScheduleNotFound_ReturnsNotFoundStatusCode()
        {
            var deleteScheduleRequest = TestsSchedulesHelper.CreateDeleteScheduleRequest(8, 2024, 15);
            var content = new StringContent(JsonConvert.SerializeObject(deleteScheduleRequest), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/v1/Schedules/delete", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
