using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using polyclinic_service.Appointments.Controllers;
using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;
using polyclinic_service.Appointments.Services.Interfaces;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service.UserAppointments.Services.Interfaces;

namespace polyclinic_service_api_tests.Appointments
{
    public class AppointmentsControllerTests
    {
        private readonly Mock<IAppointmentQueryService> _mockQueryService;
        private readonly Mock<IAppointmentCommandService> _mockCommandService;
        private readonly Mock<IUserAppointmentCommandService> _mockUserAppointmentCommandService;
        private readonly Mock<ILogger<AppointmentsController>> _logger;
        private readonly AppointmentsController _controller;

        public AppointmentsControllerTests()
        {
            _mockQueryService = new Mock<IAppointmentQueryService>();
            _mockCommandService = new Mock<IAppointmentCommandService>();
            _mockUserAppointmentCommandService = new Mock<IUserAppointmentCommandService>();
            _logger = new Mock<ILogger<AppointmentsController>>();
            _controller = new AppointmentsController(_mockQueryService.Object, _mockCommandService.Object, _mockUserAppointmentCommandService.Object, _logger.Object);
        }

        [Fact]
        public async Task GetAllAppointments_NoAppointmentsExist_ReturnsNotFound()
        {
            _mockQueryService.Setup(s => s.GetAllAppointments())
                .ThrowsAsync(new ItemsDoNotExist(Constants.APPOINTMENTS_DO_NOT_EXIST));

            var result = await _controller.GetAllAppointments();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(Constants.APPOINTMENTS_DO_NOT_EXIST, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetAllAppointments_AppointmentsExist_ReturnsOkWithAppointments()
        {
            var appointments = new List<Appointment>() { new Appointment(), new Appointment() };
            appointments[0].UserAppointments = new List<UserAppointment>();
            appointments[1].UserAppointments = new List<UserAppointment>();
            _mockQueryService.Setup(s => s.GetAllAppointments())
                .ReturnsAsync(appointments);

            var result = await _controller.GetAllAppointments();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAppointments = Assert.IsType<List<GetAppointmentRequest>>(okResult.Value);
            Assert.Equal(2, returnedAppointments.Count);
            Assert.Equal(appointments.Select(a => a.Id), returnedAppointments.Select(a => a.Id));
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAppointmentById_AppointmentNotFound_ReturnsNotFound()
        {
            _mockQueryService.Setup(s => s.GetAppointmentById(It.IsAny<int>()))
                .ThrowsAsync(new ItemDoesNotExist(Constants.APPOINTMENT_DOES_NOT_EXIST));

            var result = await _controller.GetAppointmentById(1);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(Constants.APPOINTMENT_DOES_NOT_EXIST, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetAppointmentById_AppointmentFound_ReturnsOkWithAppointment()
        {
            var appointment = new Appointment();
            appointment.UserAppointments = new List<UserAppointment>();
            _mockQueryService.Setup(s => s.GetAppointmentById(It.IsAny<int>()))
                .ReturnsAsync(appointment);

            var result = await _controller.GetAppointmentById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAppointment = Assert.IsType<GetAppointmentRequest>(okResult.Value);
            Assert.Equal(appointment.Id, returnedAppointment.Id);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task CreateAppointment_ValidRequest_ReturnsCreatedWithAppointment()
        {
            var request = new CreateAppointmentRequest();
            var appointment = new Appointment();
            _mockCommandService.Setup(s => s.CreateAppointment(It.IsAny<CreateAppointmentRequest>()))
                .ReturnsAsync(appointment);

            var userAppointment1 = new UserAppointment();
            var userAppointment2 = new UserAppointment();
            _mockUserAppointmentCommandService
                .Setup(s => s.CreateUserAppointment(It.IsAny<CreateUserAppointmentRequest>()))
                .ReturnsAsync(userAppointment1);

            var result = await _controller.CreateAppointment(request);

            var createdResult = Assert.IsType<CreatedResult>(result.Result);
            var createdAppointment = Assert.IsType<GetAppointmentRequest>(createdResult.Value);
            Assert.Equal(appointment.Id, createdAppointment.Id);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async Task UpdateAppointment_AppointmentDoesNotExist_ReturnsNotFound()
        {
            var request = new UpdateAppointmentRequest();
            _mockCommandService.Setup(s => s.UpdateAppointment(It.IsAny<UpdateAppointmentRequest>()))
                .ThrowsAsync(new ItemDoesNotExist(Constants.APPOINTMENT_DOES_NOT_EXIST));

            var result = await _controller.UpdateAppointment(request);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(Constants.APPOINTMENT_DOES_NOT_EXIST, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task UpdateAppointment_ValidRequest_ReturnsAcceptedWithAppointment()
        {
            var request = new UpdateAppointmentRequest();
            var appointment = new Appointment();
            appointment.UserAppointments = new List<UserAppointment>();
            _mockCommandService.Setup(s => s.UpdateAppointment(It.IsAny<UpdateAppointmentRequest>()))
                .ReturnsAsync(appointment);

            var result = await _controller.UpdateAppointment(request);

            var acceptedResult = Assert.IsType<AcceptedResult>(result.Result);
            var updatedAppointment = Assert.IsType<GetAppointmentRequest>(acceptedResult.Value);
            Assert.Equal(appointment.Id, updatedAppointment.Id);
            Assert.Equal(202, acceptedResult.StatusCode);
        }

        [Fact]
        public async Task DeleteAppointment_AppointmentDoesNotExist_ReturnsNotFound()
        {
            _mockCommandService.Setup(s => s.DeleteAppointment(It.IsAny<int>()))
                .ThrowsAsync(new ItemDoesNotExist(Constants.APPOINTMENT_DOES_NOT_EXIST));

            var result = await _controller.DeleteAppointment(1);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(Constants.APPOINTMENT_DOES_NOT_EXIST, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task DeleteAppointment_ValidRequest_ReturnsAcceptedWithAppointment()
        {
            var appointment = new Appointment();
            appointment.UserAppointments = new List<UserAppointment>();
            _mockCommandService.Setup(s => s.DeleteAppointment(It.IsAny<int>()))
                .ReturnsAsync(appointment);

            var result = await _controller.DeleteAppointment(1);

            var acceptedResult = Assert.IsType<AcceptedResult>(result.Result);
            var deletedAppointment = Assert.IsType<GetAppointmentRequest>(acceptedResult.Value);
            Assert.Equal(appointment.Id, deletedAppointment.Id);
            Assert.Equal(202, acceptedResult.StatusCode);
        }

        [Fact]
        public async Task CheckAvailabilityForDay_NoAvailability_ReturnsNotFound()
        {
            _mockQueryService.Setup(s => s.GetFreeSlotsForInterval(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new ItemsDoNotExist(Constants.NO_FREE_TIME_SLOTS));

            var result = await _controller.CheckAvailabilityForDay(1, 1, 1, 2024);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(Constants.NO_FREE_TIME_SLOTS, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task CheckAvailabilityForDay_AvailabilityExists_ReturnsOkWithAvailability()
        {
            var availability = new List<FreeTimeSlotResponse>();
            _mockQueryService.Setup(s => s.GetFreeSlotsForInterval(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(availability);

            var result = await _controller.CheckAvailabilityForDay(1, 1, 1, 2024);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAvailability = Assert.IsType<List<FreeTimeSlotResponse>>(okResult.Value);
            Assert.Equal(availability, returnedAvailability);
            Assert.Equal(200, okResult.StatusCode);
        }
        
        [Fact]
        public async Task CheckAvailabilityForWeek_NoAvailability_ReturnsNotFound()
        {
            _mockQueryService.Setup(s => s.GetFreeSlotsForInterval(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new ItemsDoNotExist(Constants.NO_FREE_TIME_SLOTS));

            var result = await _controller.CheckAvailabilityForWeek(1, 1, 2024);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(Constants.NO_FREE_TIME_SLOTS, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task CheckAvailabilityForWeek_AvailabilityExists_ReturnsOkWithAvailability()
        {
            var availability = new List<FreeTimeSlotResponse>();
            _mockQueryService.Setup(s => s.GetFreeSlotsForInterval(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(availability);

            var result = await _controller.CheckAvailabilityForWeek(1, 1, 2024);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAvailability = Assert.IsType<List<FreeTimeSlotResponse>>(okResult.Value);
            Assert.Equal(availability, returnedAvailability);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task CheckAvailabilityForMonth_NoAvailability_ReturnsNotFound()
        {
            _mockQueryService.Setup(s => s.GetFreeSlotsForInterval(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new ItemsDoNotExist(Constants.NO_FREE_TIME_SLOTS));

            var result = await _controller.CheckAvailabilityForMonth(1, 1, 2024);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(Constants.NO_FREE_TIME_SLOTS, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task CheckAvailabilityForMonth_AvailabilityExists_ReturnsOkWithAvailability()
        {
            var availability = new List<FreeTimeSlotResponse>();
            _mockQueryService.Setup(s => s.GetFreeSlotsForInterval(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(availability);

            var result = await _controller.CheckAvailabilityForMonth(1, 1, 2024);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAvailability = Assert.IsType<List<FreeTimeSlotResponse>>(okResult.Value);
            Assert.Equal(availability, returnedAvailability);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task CheckAvailabilityForInterval_NoAvailability_ReturnsNotFound()
        {
            _mockQueryService.Setup(s => s.GetFreeSlotsForInterval(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new ItemsDoNotExist(Constants.NO_FREE_TIME_SLOTS));

            var result = await _controller.CheckAvailabilityForInterval(1, 1, 1, 2024, 2, 1, 2024);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(Constants.NO_FREE_TIME_SLOTS, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task CheckAvailabilityForInterval_AvailabilityExists_ReturnsOkWithAvailability()
        {
            var availability = new List<FreeTimeSlotResponse>();
            _mockQueryService.Setup(s => s.GetFreeSlotsForInterval(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(availability);

            var result = await _controller.CheckAvailabilityForInterval(1, 1, 1, 2024, 2, 1, 2024);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAvailability = Assert.IsType<List<FreeTimeSlotResponse>>(okResult.Value);
            Assert.Equal(availability, returnedAvailability);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetDayWithMostAppointmentsFromMonth_NoAppointments_ReturnsNotFound()
        {
            _mockQueryService.Setup(s => s.DayWithMostAppointmentsInInterval(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new ItemsDoNotExist(Constants.NO_APPOINTMENTS_IN_MONTH));

            var result = await _controller.GetDayWithMostAppointmentsFromMonth(1, 2024);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(Constants.NO_APPOINTMENTS_IN_MONTH, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetDayWithMostAppointmentsFromMonth_AppointmentsExist_ReturnsOkWithDayResponse()
        {
            var response = new DateResponse();
            _mockQueryService.Setup(s => s.DayWithMostAppointmentsInInterval(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(response);

            var result = await _controller.GetDayWithMostAppointmentsFromMonth(1, 2024);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedResponse = Assert.IsType<DateResponse>(okResult.Value);
            Assert.Equal(response, returnedResponse);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetDayWithMostAppointmentsFromWeek_NoAppointments_ReturnsNotFound()
        {
            _mockQueryService.Setup(s => s.DayWithMostAppointmentsInInterval(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new ItemsDoNotExist(Constants.NO_APPOINTMENTS_IN_WEEK));

            var result = await _controller.GetDayWithMostAppointmentsFromWeek(1, 2024);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(Constants.NO_APPOINTMENTS_IN_WEEK, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetDayWithMostAppointmentsFromWeek_AppointmentsExist_ReturnsOkWithDayResponse()
        {
            var response = new DateResponse();
            _mockQueryService.Setup(s => s.DayWithMostAppointmentsInInterval(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(response);

            var result = await _controller.GetDayWithMostAppointmentsFromWeek(1, 2024);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedResponse = Assert.IsType<DateResponse>(okResult.Value);
            Assert.Equal(response, returnedResponse);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
