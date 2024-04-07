using Moq;
using polyclinic_service.Schedules.Services;
using polyclinic_service.Schedules.Repository.Interfaces;
using polyclinic_service.Schedules.Models;
using polyclinic_service.System.Exceptions;
using polyclinic_service.Schedules.DTOs;

namespace polyclinic_service_api_tests.Schedules
{
    public class ScheduleCommandServiceTests
    {
        private readonly Mock<IScheduleRepository> _mockRepository;
        private readonly ScheduleCommandService _service;

        public ScheduleCommandServiceTests()
        {
            _mockRepository = new Mock<IScheduleRepository>();
            _service = new ScheduleCommandService(_mockRepository.Object);
        }

        [Fact]
        public async Task CreateSchedule_ValidRequest_ReturnsCreatedSchedule()
        {
            var request = new CreateScheduleRequest
            {
                DoctorId = 1,
                Year = 2024,
                WeekNumber = 14
            };

            var expectedSchedule = new Schedule
            {
                DoctorId = request.DoctorId,
                Year = request.Year,
                WeekNumber = request.WeekNumber
            };

            _mockRepository.Setup(repo => repo.CreateAsync(request)).ReturnsAsync(expectedSchedule);

            var result = await _service.CreateSchedule(request);

            Assert.NotNull(result);
            Assert.Equal(expectedSchedule, result);
        }

        [Fact]
        public async Task UpdateSchedule_ScheduleDoesNotExist_ThrowsItemDoesNotExistException()
        {
            var request = new UpdateScheduleRequest
            {
                DoctorId = 1,
                Year = 2024,
                WeekNumber = 14
            };

            _mockRepository.Setup(repo => repo.GetByDoctorIdAndWeekIdentityAsync(It.IsAny<GetByDoctorIdAndWeekIdentityRequest>())).ReturnsAsync((Schedule)null);

            await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateSchedule(request));
        }

        [Fact]
        public async Task UpdateSchedule_ValidRequest_ReturnsUpdatedSchedule()
        {
            var request = new UpdateScheduleRequest
            {
                DoctorId = 1,
                Year = 2024,
                WeekNumber = 14
            };

            var schedule = new Schedule
            {
                DoctorId = request.DoctorId,
                Year = request.Year,
                WeekNumber = request.WeekNumber
            };

            _mockRepository.Setup(repo => repo.GetByDoctorIdAndWeekIdentityAsync(It.IsAny<GetByDoctorIdAndWeekIdentityRequest>())).ReturnsAsync(schedule);
            _mockRepository.Setup(repo => repo.UpdateAsync(request)).ReturnsAsync(schedule);

            var result = await _service.UpdateSchedule(request);

            Assert.NotNull(result);
            Assert.Equal(schedule, result);
        }

        [Fact]
        public async Task DeleteSchedule_ScheduleDoesNotExist_ThrowsItemDoesNotExistException()
        {
            var request = new DeleteScheduleRequest
            {
                DoctorId = 1,
                Year = 2024,
                WeekNumber = 14
            };

            _mockRepository.Setup(repo => repo.GetByDoctorIdAndWeekIdentityAsync(It.IsAny<GetByDoctorIdAndWeekIdentityRequest>())).ReturnsAsync((Schedule)null);

            await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeleteSchedule(request));
        }

        [Fact]
        public async Task DeleteSchedule_ValidRequest_ReturnsDeletedSchedule()
        {
            var request = new DeleteScheduleRequest
            {
                DoctorId = 1,
                Year = 2024,
                WeekNumber = 14
            };

            var schedule = new Schedule
            {
                DoctorId = request.DoctorId,
                Year = request.Year,
                WeekNumber = request.WeekNumber
            };

            _mockRepository.Setup(repo => repo.GetByDoctorIdAndWeekIdentityAsync(It.IsAny<GetByDoctorIdAndWeekIdentityRequest>())).ReturnsAsync(schedule);
            _mockRepository.Setup(repo => repo.DeleteAsync(request)).ReturnsAsync(schedule);

            var result = await _service.DeleteSchedule(request);

            Assert.NotNull(result);
            Assert.Equal(schedule, result);
        }
    }
}
