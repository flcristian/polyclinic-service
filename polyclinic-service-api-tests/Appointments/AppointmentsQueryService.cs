using Moq;
using polyclinic_service_api_tests.Appointments.Helpers;
using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;
using polyclinic_service.Appointments.Repository.Interfaces;
using polyclinic_service.Appointments.Services;
using polyclinic_service.Appointments.Services.Interfaces;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;

namespace polyclinic_service_api_tests.Appointments
{
    public class AppointmentQueryServiceTests
    {
        private readonly Mock<IAppointmentRepository> _mockRepo;
        private readonly IAppointmentQueryService _service;

        public AppointmentQueryServiceTests()
        {
            _mockRepo = new Mock<IAppointmentRepository>();
            _service = new AppointmentQueryService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllAppointments_NoAppointmentsExist_ThrowItemsDoNotExistException()
        {
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Appointment>());

            var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAllAppointments());

            Assert.Equal(Constants.APPOINTMENTS_DO_NOT_EXIST, exception.Message);
        }

        [Fact]
        public async Task GetAllAppointments_AppointmentsExist_ReturnsAllAppointments()
        {
            var appointments = TestsAppointmentsHelper.CreateTestAppointments(3);

            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(appointments);
            var result = await _service.GetAllAppointments();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Contains(appointments[0], result);
            Assert.Contains(appointments[1], result);
        }

        [Fact]
        public async Task GetAppointmentById_AppointmentNotFound_ThrowItemDoesNotExistException()
        {
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Appointment)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetAppointmentById(1));

            Assert.Equal(Constants.APPOINTMENT_DOES_NOT_EXIST, exception.Message);
        }

        [Fact]
        public async Task GetAppointmentById_AppointmentFound_ReturnsAppointment()
        {
            var appointment = TestsAppointmentsHelper.CreateTestAppointment(1);

            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(appointment);
            var result = await _service.GetAppointmentById(1);

            Assert.NotNull(result);
            Assert.Equal(appointment, result);
        }

        [Fact]
        public async Task GetFreeSlotsForInterval_NoFreeSlots_ThrowItemsDoNotExistException()
        {
            _mockRepo.Setup(repo => repo.GetFreeSlotsAsync(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new List<FreeTimeSlotResponse>());

            var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetFreeSlotsForInterval(1, DateTime.Now, DateTime.Now));

            Assert.Equal(Constants.NO_FREE_TIME_SLOTS, exception.Message);
        }

        [Fact]
        public async Task GetFreeSlotsForInterval_FreeSlotsExist_ReturnsFreeSlots()
        {
            var freeSlots = TestsAppointmentsHelper.CreateTestFreeTimeSlots(3);

            _mockRepo.Setup(repo => repo.GetFreeSlotsAsync(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(freeSlots);
            var result = await _service.GetFreeSlotsForInterval(1, DateTime.Now, DateTime.Now);

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Contains(freeSlots[0], result);
            Assert.Contains(freeSlots[1], result);
        }

        [Fact]
        public async Task DayWithMostAppointmentsInInterval_NoAppointments_ThrowItemDoesNotExistException()
        {
            _mockRepo.Setup(repo => repo.DayWithMostAppointmentsInIntervalAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync((DateResponse)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DayWithMostAppointmentsInInterval(DateTime.Now, DateTime.Now));

            Assert.Equal(Constants.NO_APPOINTMENTS_IN_INTERVAL, exception.Message);
        }

        [Fact]
        public async Task DayWithMostAppointmentsInInterval_AppointmentsExist_ReturnsDayWithMostAppointments()
        {
            var dateResponse = TestsAppointmentsHelper.CreateTestDateResponse();
                
            _mockRepo.Setup(repo => repo.DayWithMostAppointmentsInIntervalAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(dateResponse);
            var result = await _service.DayWithMostAppointmentsInInterval(DateTime.Now, DateTime.Now);

            Assert.NotNull(result);
            Assert.Equal(dateResponse, result);
        }
    }
}
