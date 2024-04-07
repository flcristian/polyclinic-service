using AutoMapper;
using Moq;
using polyclinic_service.Schedules.DTOs;
using polyclinic_service.Schedules.Models;
using polyclinic_service.Schedules.Repository.Interfaces;
using polyclinic_service.Schedules.Services;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;

namespace polyclinic_service_api_tests.Schedules
{
    public class ScheduleQueryServiceTests
    {
        private readonly Mock<IScheduleRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ScheduleQueryService _service;

        public ScheduleQueryServiceTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockRepo = new Mock<IScheduleRepository>();
            _service = new ScheduleQueryService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllSchedules_NoSchedulesExist_ThrowItemsDoNotExistException()
        {
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Schedule>());

            var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAllSchedules());

            Assert.Equal(Constants.SCHEDULES_DO_NOT_EXIST, exception.Message);
        }

        [Fact]
        public async Task GetAllSchedules_SchedulesExist_ReturnsAllSchedules()
        {
            var schedules = new List<Schedule> { new Schedule(), new Schedule() };
            var mappedSchedules = schedules.Select(schedule => new GetScheduleRequest()).ToList();

            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(schedules);
            _mockMapper.Setup(mapper => mapper.Map<List<GetScheduleRequest>>(schedules)).Returns(mappedSchedules);

            var result = await _service.GetAllSchedules();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetSchedulesByDoctorId_NoSchedulesExist_ThrowItemsDoNotExistException()
        {
            int doctorId = 1;
            _mockRepo.Setup(repo => repo.GetSchedulesByDoctorIdAsync(doctorId)).ReturnsAsync(new List<Schedule>());

            var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetSchedulesByDoctorId(doctorId));

            Assert.Equal(Constants.SCHEDULES_DO_NOT_EXIST, exception.Message);
        }

        [Fact]
        public async Task GetSchedulesByDoctorId_SchedulesExist_ReturnsSchedules()
        {
            int doctorId = 1;
            var schedules = new List<Schedule> { new Schedule(), new Schedule() };
            var mappedSchedules = schedules.Select(schedule => new GetScheduleRequest()).ToList();

            _mockRepo.Setup(repo => repo.GetSchedulesByDoctorIdAsync(doctorId)).ReturnsAsync(schedules);
            _mockMapper.Setup(mapper => mapper.Map<List<GetScheduleRequest>>(schedules)).Returns(mappedSchedules);

            var result = await _service.GetSchedulesByDoctorId(doctorId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetScheduleByDoctorIdAndWeekIdentity_ScheduleDoesNotExist_ThrowsItemDoesNotExistException()
        {
            var request = new GetByDoctorIdAndWeekIdentityRequest
            {
                DoctorId = 1,
                Year = 2024,
                WeekNumber = 14
            };

            _mockRepo.Setup(repo => repo.GetByDoctorIdAndWeekIdentityAsync(request)).ReturnsAsync((Schedule)null);

            await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetScheduleByDoctorIdAndWeekIdentity(request));
        }

        [Fact]
        public async Task GetScheduleByDoctorIdAndWeekIdentity_ScheduleExists_ReturnsGetScheduleRequest()
        {
            var request = new GetByDoctorIdAndWeekIdentityRequest
            {
                DoctorId = 1,
                Year = 2024,
                WeekNumber = 14
            };

            var schedule = new Schedule
            {
                DoctorId = 1,
                Year = 2024,
                WeekNumber = 14,
                MondaySchedule = new ScheduleSlot { StartTime = "09:00", EndTime = "17:00" },
                TuesdaySchedule = new ScheduleSlot { StartTime = "09:00", EndTime = "17:00" },
                WednesdaySchedule = new ScheduleSlot { StartTime = "09:00", EndTime = "17:00" },
                ThursdaySchedule = new ScheduleSlot { StartTime = "09:00", EndTime = "17:00" },
                FridaySchedule = new ScheduleSlot { StartTime = "09:00", EndTime = "17:00" }
            };

            var expectedRequest = new GetScheduleRequest
            {
                DoctorId = 1,
                Year = 2024,
                WeekNumber = 14,
                MondaySchedule = new GetScheduleSlotRequest(),
                TuesdaySchedule = new GetScheduleSlotRequest(),
                WednesdaySchedule = new GetScheduleSlotRequest(),
                ThursdaySchedule = new GetScheduleSlotRequest(),
                FridaySchedule = new GetScheduleSlotRequest()
            };

            _mockRepo.Setup(repo => repo.GetByDoctorIdAndWeekIdentityAsync(request)).ReturnsAsync(schedule);
            _mockMapper.Setup(m => m.Map<GetScheduleSlotRequest>(It.IsAny<ScheduleSlot>())).Returns(new GetScheduleSlotRequest());

            var result = await _service.GetScheduleByDoctorIdAndWeekIdentity(request);

            Assert.NotNull(result);
            Assert.Equal(expectedRequest.DoctorId, result.DoctorId);
            Assert.Equal(expectedRequest.Year, result.Year);
            Assert.Equal(expectedRequest.WeekNumber, result.WeekNumber);
            Assert.NotNull(result.MondaySchedule);
            Assert.NotNull(result.TuesdaySchedule);
            Assert.NotNull(result.WednesdaySchedule);
            Assert.NotNull(result.ThursdaySchedule);
            Assert.NotNull(result.FridaySchedule);
        }

        private static ScheduleSlot CreateMockScheduleSlot()
        {
            return new ScheduleSlot
            {
                StartTime = "08:00",
                EndTime = "16:00"
            };
        }
    }
}
