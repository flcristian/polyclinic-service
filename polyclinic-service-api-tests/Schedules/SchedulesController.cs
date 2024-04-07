using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using polyclinic_service.Schedules.Controllers;
using polyclinic_service.Schedules.DTOs;
using polyclinic_service.Schedules.Models;
using polyclinic_service.Schedules.Services.Interfaces;
using polyclinic_service.System.Exceptions;

namespace polyclinic_service_api_tests.Schedules
{
    public class SchedulesControllerTests
    {
        private readonly Mock<IScheduleQueryService> _mockQueryService;
        private readonly Mock<IScheduleCommandService> _mockCommandService;
        private readonly Mock<ILogger<SchedulesController>> _logger;
        private readonly SchedulesController _controller;

        public SchedulesControllerTests()
        {
            _mockQueryService = new Mock<IScheduleQueryService>();
            _mockCommandService = new Mock<IScheduleCommandService>();
            _logger = new Mock<ILogger<SchedulesController>>();
            _controller = new SchedulesController(_mockQueryService.Object, _mockCommandService.Object, _logger.Object);
        }

        [Fact]
        public async Task GetAllSchedules_NoSchedulesExist_ReturnsNotFound()
        {
            _mockQueryService.Setup(s => s.GetAllSchedules())
                .ThrowsAsync(new ItemsDoNotExist("No schedules found."));

            var result = await _controller.GetAllSchedules();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("No schedules found.", notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetAllSchedules_SchedulesExist_ReturnsOkWithSchedules()
        {
            var schedules = new List<GetScheduleRequest>() { new GetScheduleRequest(), new GetScheduleRequest() };
            _mockQueryService.Setup(s => s.GetAllSchedules())
                .ReturnsAsync(schedules);

            var result = await _controller.GetAllSchedules();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedSchedules = Assert.IsType<List<GetScheduleRequest>>(okResult.Value);
            Assert.Equal(2, returnedSchedules.Count);
            Assert.Equal(schedules, returnedSchedules);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetSchedulesByDoctorId_NoSchedulesExist_ReturnsNotFound()
        {
            _mockQueryService.Setup(s => s.GetSchedulesByDoctorId(It.IsAny<int>()))
                .ThrowsAsync(new ItemDoesNotExist("No schedules found."));

            var result = await _controller.GetSchedulesByDoctorId(1);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("No schedules found.", notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetSchedulesByDoctorId_SchedulesExist_ReturnsOkWithSchedules()
        {
            var schedules = new List<GetScheduleRequest>() { new GetScheduleRequest(), new GetScheduleRequest() };
            _mockQueryService.Setup(s => s.GetSchedulesByDoctorId(It.IsAny<int>()))
                .ReturnsAsync(schedules);

            var result = await _controller.GetSchedulesByDoctorId(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedSchedules = Assert.IsType<List<GetScheduleRequest>>(okResult.Value);
            Assert.Equal(2, returnedSchedules.Count);
            Assert.Equal(schedules, returnedSchedules);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetScheduleByDoctorIdAndWeekIdentity_NoScheduleExists_ReturnsNotFound()
        {
            _mockQueryService.Setup(s => s.GetScheduleByDoctorIdAndWeekIdentity(It.IsAny<GetByDoctorIdAndWeekIdentityRequest>()))
                .ThrowsAsync(new ItemDoesNotExist("Schedule not found."));

            var result = await _controller.GetScheduleByDoctorIdAndWeekIdentity(1, 2024, 15);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Schedule not found.", notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetScheduleByDoctorIdAndWeekIdentity_ScheduleExists_ReturnsOkWithSchedule()
        {
            var schedule = new GetScheduleRequest();
            _mockQueryService.Setup(s => s.GetScheduleByDoctorIdAndWeekIdentity(It.IsAny<GetByDoctorIdAndWeekIdentityRequest>()))
                .ReturnsAsync(schedule);

            var result = await _controller.GetScheduleByDoctorIdAndWeekIdentity(1, 2024, 15);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedSchedule = Assert.IsType<GetScheduleRequest>(okResult.Value);
            Assert.Equal(schedule, returnedSchedule);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task CreateSchedule_ValidRequest_ReturnsCreatedWithSchedule()
        {
            var request = new CreateScheduleRequest();
            var schedule = new Schedule();
            _mockCommandService.Setup(s => s.CreateSchedule(It.IsAny<CreateScheduleRequest>()))
                .ReturnsAsync(schedule);

            var result = await _controller.CreateSchedule(request);

            var createdResult = Assert.IsType<CreatedResult>(result.Result);
            var createdSchedule = Assert.IsType<Schedule>(createdResult.Value);
            Assert.Equal(schedule, createdSchedule);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public async Task UpdateSchedule_ScheduleDoesNotExist_ReturnsNotFound()
        {
            var request = new UpdateScheduleRequest();
            _mockCommandService.Setup(s => s.UpdateSchedule(It.IsAny<UpdateScheduleRequest>()))
                .ThrowsAsync(new ItemDoesNotExist("Schedule not found."));

            var result = await _controller.UpdateSchedule(request);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Schedule not found.", notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task UpdateSchedule_ValidRequest_ReturnsAcceptedWithSchedule()
        {
            var request = new UpdateScheduleRequest();
            var schedule = new Schedule();
            _mockCommandService.Setup(s => s.UpdateSchedule(It.IsAny<UpdateScheduleRequest>()))
                .ReturnsAsync(schedule);

            var result = await _controller.UpdateSchedule(request);

            var acceptedResult = Assert.IsType<AcceptedResult>(result.Result);
            var updatedSchedule = Assert.IsType<Schedule>(acceptedResult.Value);
            Assert.Equal(schedule, updatedSchedule);
            Assert.Equal(202, acceptedResult.StatusCode);
        }

        [Fact]
        public async Task DeleteSchedule_ScheduleDoesNotExist_ReturnsNotFound()
        {
            _mockCommandService.Setup(s => s.DeleteSchedule(It.IsAny<DeleteScheduleRequest>()))
                .ThrowsAsync(new ItemDoesNotExist("Schedule not found."));

            var result = await _controller.DeleteSchedule(new DeleteScheduleRequest());

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Schedule not found.", notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task DeleteSchedule_ValidRequest_ReturnsAcceptedWithSchedule()
        {
            var schedule = new Schedule();
            _mockCommandService.Setup(s => s.DeleteSchedule(It.IsAny<DeleteScheduleRequest>()))
                .ReturnsAsync(schedule);

            var result = await _controller.DeleteSchedule(new DeleteScheduleRequest());

            var acceptedResult = Assert.IsType<AcceptedResult>(result.Result);
            var deletedSchedule = Assert.IsType<Schedule>(acceptedResult.Value);
            Assert.Equal(schedule, deletedSchedule);
            Assert.Equal(202, acceptedResult.StatusCode);
        }
    }
}
