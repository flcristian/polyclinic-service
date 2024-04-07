using System.Net;
using System.Text;
using Newtonsoft.Json;
using polyclinic_service.Users.Models;
using polyclinic_service_api_tests.Infrastructure;
using polyclinic_service_api_tests.Users.Helpers;

namespace polyclinic_service_api_tests.Users
{
    public class UserIntegrationTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public UserIntegrationTests(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_GetAllUsers_UsersFound_ReturnsOkStatusCode_ValidUsersContentResponse()
        {
            var response = await _client.GetAsync("/api/v1/User/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_GetUserById_UserFound_ReturnsOkStatusCode_ValidUserContentResponse()
        {
            var userId = 1;
            var response = await _client.GetAsync($"/api/v1/User/user/{userId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_GetUserById_UserNotFound_ReturnsNotFoundStatusCode()
        {
            var userId = 9999;
            var response = await _client.GetAsync($"/api/v1/User/user/{userId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_CreateUser_ValidRequest_ReturnsCreatedStatusCode_ValidUserContentResponse()
        {
            var user = TestsUsersHelper.CreateTestDoctor(1);
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/User/create", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Post_CreateUser_UserAlreadyExists_ReturnsBadRequestStatusCode()
        {
            var user = TestsUsersHelper.CreateTestDoctor(2);
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            await _client.PostAsync("/api/v1/User/create", content);

            var response = await _client.PostAsync("/api/v1/User/create", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Put_UpdateUser_ValidRequest_ReturnsAcceptedStatusCode_ValidUserContentResponse()
        {
            var user = TestsUsersHelper.CreateTestCreateUserRequest(3);
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/User/create", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var createdUser = JsonConvert.DeserializeObject<User>(responseString);

            var updateUser = TestsUsersHelper.CreateTestUpdateUserRequest(createdUser.Id);
            content = new StringContent(JsonConvert.SerializeObject(updateUser), Encoding.UTF8, "application/json");

            response = await _client.PutAsync("/api/v1/User/update", content);

            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        }

        [Fact]
        public async Task Put_UpdateUser_InvalidUserAge_ReturnsBadRequestStatusCode()
        {
            var user = TestsUsersHelper.CreateTestCreateUserRequest(4);
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/User/create", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var createdUser = JsonConvert.DeserializeObject<User>(responseString);

            var updateUser = TestsUsersHelper.CreateTestUpdateUserRequest(4);
            updateUser.Age = -28;
            content = new StringContent(JsonConvert.SerializeObject(updateUser), Encoding.UTF8, "application/json");

            response = await _client.PutAsync("/api/v1/User/update", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Put_UpdateUser_UserDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var updateUser = TestsUsersHelper.CreateTestUpdateUserRequest(5);
            var content = new StringContent(JsonConvert.SerializeObject(updateUser), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/v1/User/update", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_DeleteUser_UserExists_ReturnsDeletedUser()
        {
            var user = TestsUsersHelper.CreateTestCreateUserRequest(6);
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/User/create", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var createdUser = JsonConvert.DeserializeObject<User>(responseString);

            response = await _client.DeleteAsync($"/api/v1/User/delete/{createdUser.Id}");

            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        }

        [Fact]
        public async Task Delete_DeleteUser_UserDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var response = await _client.DeleteAsync("/api/v1/User/delete/9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
