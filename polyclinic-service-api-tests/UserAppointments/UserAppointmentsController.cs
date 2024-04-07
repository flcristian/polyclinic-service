using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using polyclinic_service_api_tests.Appointments.Helpers;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.UserAppointments.Controllers;
using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service.UserAppointments.Services.Interfaces;
using polyclinic_service_api_tests.UserAppointments.Helpers;
using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;

namespace polyclinic_service_api_tests.UserAppointments
{
    public class UserAppointmentsControllerTests
    {
        private readonly Mock<IUserAppointmentQueryService> _mockQueryService;
        private readonly Mock<IUserAppointmentCommandService> _mockCommandService;
        private readonly Mock<ILogger<UserAppointmentsController>> _logger;
        private readonly UserAppointmentsController _controller;

        public UserAppointmentsControllerTests()
        {
            _mockQueryService = new Mock<IUserAppointmentQueryService>();
            _mockCommandService = new Mock<IUserAppointmentCommandService>();
            _logger = new Mock<ILogger<UserAppointmentsController>>();
            _controller = new UserAppointmentsController(_mockQueryService.Object, _logger.Object, _mockCommandService.Object);
        }

        [Fact]
        public async Task GetAllUserAppointments_NoUserAppointmentsExist_ReturnsNotFound()
        {
            _mockQueryService.Setup(s => s.GetAllUserAppointments())
                .ThrowsAsync(new ItemsDoNotExist(Constants.USER_APPOINTMENTS_DO_NOT_EXIST));

            var result = await _controller.GetAllUserAppointments();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(Constants.USER_APPOINTMENTS_DO_NOT_EXIST, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetAllUserAppointments_UserAppointmentsExist_ReturnsOkWithUserAppointments()
        {
            List<UserAppointment> userAppointments = TestsUserAppointmentsHelpers.CreateUserAppointments(3);

            _mockQueryService.Setup(s => s.GetAllUserAppointments())
                .ReturnsAsync(userAppointments);

            var result = await _controller.GetAllUserAppointments();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUserAppointments = Assert.IsType<List<GetUserAppointmentRequest>>(okResult.Value);
            Assert.Equal(3, returnedUserAppointments.Count);
            Assert.Equal(userAppointments.Select(ua => ua.Id), returnedUserAppointments.Select(ua => ua.Id));
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetUserAppointmentById_UserAppointmentNotFound_ReturnsNotFound()
        {
            _mockQueryService.Setup(repo => repo.GetUserAppointmentById(It.IsAny<int>()))
                .ThrowsAsync(new ItemDoesNotExist(Constants.USER_APPOINTMENT_DOES_NOT_EXIST));

            var result = await _controller.GetUserAppointmentById(1);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(Constants.USER_APPOINTMENT_DOES_NOT_EXIST, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetUserAppointmentById_UserAppointmentFound_ReturnsOkWithUserAppointment()
        {
            UserAppointment userAppointment = TestsUserAppointmentsHelpers.CreateUserAppointment(1);

            _mockQueryService.Setup(repo => repo.GetUserAppointmentById(It.IsAny<int>()))
                .ReturnsAsync(userAppointment);

            var result = await _controller.GetUserAppointmentById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUserAppointment = Assert.IsType<GetUserAppointmentRequest>(okResult.Value);
            Assert.Equal(userAppointment.Id, returnedUserAppointment.Id);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task CreateUserAppointment_ValidRequest_ReturnsCreatedWithUserAppointment()
        {
            var request = TestsUserAppointmentsHelpers.CreateUserAppointmentRequest(1, 1);
            var userAppointment = TestsUserAppointmentsHelpers.CreateUserAppointment(1);

            _mockCommandService.Setup(repo => repo.CreateUserAppointment(It.IsAny<CreateUserAppointmentRequest>()))
                .ReturnsAsync(userAppointment);

            var result = await _controller.CreateUserAppointment(request);

            var createdResult = Assert.IsType<CreatedResult>(result.Result);
            var createdUserAppointment = Assert.IsType<GetUserAppointmentRequest>(createdResult.Value);
            Assert.Equal(userAppointment.Id, createdUserAppointment.Id);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async Task UpdateUserAppointment_UserAppointmentDoesNotExist_ReturnsNotFound()
        {
            var request = TestsUserAppointmentsHelpers.UpdateUserAppointmentRequest(1, 1, 1);

            _mockCommandService.Setup(repo => repo.UpdateUserAppointment(It.IsAny<UpdateUserAppointmentRequest>()))
                .ThrowsAsync(new ItemDoesNotExist(Constants.USER_APPOINTMENT_DOES_NOT_EXIST));

            var result = await _controller.UpdateUserAppointment(request);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(Constants.USER_APPOINTMENT_DOES_NOT_EXIST, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task UpdateUserAppointment_ValidRequest_ReturnsAcceptedWithUserAppointment()
        {
            var request = TestsUserAppointmentsHelpers.UpdateUserAppointmentRequest(1,1,1);
            var userAppointment = TestsUserAppointmentsHelpers.CreateUserAppointment(1);

            _mockCommandService.Setup(repo => repo.UpdateUserAppointment(It.IsAny<UpdateUserAppointmentRequest>()))
                .ReturnsAsync(userAppointment);

            var result = await _controller.UpdateUserAppointment(request);

            var acceptedResult = Assert.IsType<AcceptedResult>(result.Result);
            var updatedUserAppointment = Assert.IsType<GetUserAppointmentRequest>(acceptedResult.Value);
            Assert.Equal(userAppointment.Id, updatedUserAppointment.Id);
            Assert.Equal(202, acceptedResult.StatusCode);
        }

        [Fact]
        public async Task DeleteUserAppointment_UserAppointmentDoesNotExist_ReturnsNotFound()
        {
            _mockCommandService.Setup(repo => repo.DeleteUserAppointment(It.IsAny<int>()))
                .ThrowsAsync(new ItemDoesNotExist(Constants.USER_APPOINTMENT_DOES_NOT_EXIST));

            var result = await _controller.DeleteUserAppointment(1);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(Constants.USER_APPOINTMENT_DOES_NOT_EXIST, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task DeleteUserAppointment_UserAppointmentExists_ReturnsAcceptedWithUserAppointment()
        {
            var userAppointment = TestsUserAppointmentsHelpers.CreateUserAppointment(1);

            _mockCommandService.Setup(repo => repo.DeleteUserAppointment(It.IsAny<int>()))
                .ReturnsAsync(userAppointment);

            var result = await _controller.DeleteUserAppointment(1);

            var acceptedResult = Assert.IsType<AcceptedResult>(result.Result);
            var deletedUserAppointment = Assert.IsType<GetUserAppointmentRequest>(acceptedResult.Value);
            Assert.Equal(userAppointment.Id, deletedUserAppointment.Id);
            Assert.Equal(202, acceptedResult.StatusCode);
        }
        
        [Fact]
        public async Task GetAppointmentsByUserId_NoAppointmentsExist_ReturnsNotFound()
        {
            _mockQueryService.Setup(s => s.GetAppointmentsByUserId(It.IsAny<int>()))
                .ThrowsAsync(new ItemDoesNotExist(Constants.APPOINTMENTS_DO_NOT_EXIST));

            var result = await _controller.GetAppointmentsByUserId(1);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(Constants.APPOINTMENTS_DO_NOT_EXIST, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetAppointmentsByUserId_AppointmentsExist_ReturnsOkWithAppointments()
        {
            List<Appointment> appointments = TestsAppointmentsHelper.CreateTestAppointments(3);

            _mockQueryService.Setup(s => s.GetAppointmentsByUserId(It.IsAny<int>()))
                .ReturnsAsync(appointments);

            var result = await _controller.GetAppointmentsByUserId(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAppointments = Assert.IsType<List<GetAppointmentRequest>>(okResult.Value);
            Assert.Equal(3, returnedAppointments.Count);
            Assert.Equal(appointments.Select(a => a.Id), returnedAppointments.Select(a => a.Id));
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAppointmentHistoryOfUserByUserId_NoAppointmentsExist_ReturnsNotFound()
        {
            _mockQueryService.Setup(s => s.GetAppointmentHistoryByUserId(It.IsAny<int>()))
                .ThrowsAsync(new ItemDoesNotExist(Constants.APPOINTMENTS_DO_NOT_EXIST));

            var result = await _controller.GetAppointmentHistoryOfUserByUserId(1);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(Constants.APPOINTMENTS_DO_NOT_EXIST, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetAppointmentHistoryOfUserByUserId_AppointmentsExist_ReturnsOkWithAppointments()
        {
            List<Appointment> appointments = TestsAppointmentsHelper.CreateTestAppointments(3);

            _mockQueryService.Setup(s => s.GetAppointmentHistoryByUserId(It.IsAny<int>()))
                .ReturnsAsync(appointments);

            var result = await _controller.GetAppointmentHistoryOfUserByUserId(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAppointments = Assert.IsType<List<GetAppointmentRequest>>(okResult.Value);
            Assert.Equal(3, returnedAppointments.Count);
            Assert.Equal(appointments.Select(a => a.Id), returnedAppointments.Select(a => a.Id));
            Assert.Equal(200, okResult.StatusCode);
        } 
    }
}
