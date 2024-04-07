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
    public class AppointmentCommandServiceTests
    {
        private readonly Mock<IAppointmentRepository> _mockRepo;
        private readonly IAppointmentCommandService _service;

        public AppointmentCommandServiceTests()
        {
            _mockRepo = new Mock<IAppointmentRepository>();
            _service = new AppointmentCommandService(_mockRepo.Object);
        }

        [Fact]
        public async Task CreateAppointment_ValidData_ReturnsCreatedAppointment()
        {
            var createRequest = new CreateAppointmentRequest
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddHours(1),
                PatientId = 1,
                DoctorId = 2
            };

            var expectedAppointment = new Appointment
            {
                Id = 1,
                StartDate = createRequest.StartDate,
                EndDate = createRequest.EndDate,
            };

            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<CreateAppointmentRequest>())).ReturnsAsync(expectedAppointment);

            var result = await _service.CreateAppointment(createRequest);

            Assert.NotNull(result);
            Assert.Equal(expectedAppointment, result);
        }

        [Fact]
        public async Task UpdateAppointment_ValidData_ReturnsUpdatedAppointment()
        {
            var updateRequest = new UpdateAppointmentRequest
            {
                Id = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddHours(2)
            };

            var existingAppointment = new Appointment
            {
                Id = updateRequest.Id,
                StartDate = DateTime.UtcNow.AddDays(-1),
                EndDate = DateTime.UtcNow.AddDays(-1).AddHours(1)
            };

            var expectedUpdatedAppointment = new Appointment
            {
                Id = updateRequest.Id,
                StartDate = updateRequest.StartDate,
                EndDate = updateRequest.EndDate
            };

            _mockRepo.Setup(repo => repo.GetByIdAsync(updateRequest.Id)).ReturnsAsync(existingAppointment);
            _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<UpdateAppointmentRequest>())).ReturnsAsync(expectedUpdatedAppointment);

            var result = await _service.UpdateAppointment(updateRequest);

            Assert.NotNull(result);
            Assert.Equal(expectedUpdatedAppointment, result);
        }

        [Fact]
        public async Task UpdateAppointment_AppointmentDoesNotExist_ThrowsItemDoesNotExistException()
        {
            var updateRequest = new UpdateAppointmentRequest
            {
                Id = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddHours(2)
            };

            _mockRepo.Setup(repo => repo.GetByIdAsync(updateRequest.Id)).ReturnsAsync((Appointment)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateAppointment(updateRequest));

            Assert.Equal(Constants.APPOINTMENT_DOES_NOT_EXIST, exception.Message);
        }

        [Fact]
        public async Task DeleteAppointment_AppointmentDoesNotExist_ThrowsItemDoesNotExistException()
        {
            _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Appointment)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeleteAppointment(1));

            Assert.Equal(Constants.APPOINTMENT_DOES_NOT_EXIST, exception.Message);
        }
        
        [Fact]
        public async Task DeleteAppointment_AppointmentExists_DeletesAppointment()
        {
            var existingAppointment = TestsAppointmentsHelper.CreateTestAppointment(1);

            _mockRepo.Setup(repo => repo.GetByIdAsync(existingAppointment.Id)).ReturnsAsync(existingAppointment);
            _mockRepo.Setup(repo => repo.DeleteAsync(existingAppointment.Id)).ReturnsAsync(existingAppointment);

            Appointment appointment = await _service.DeleteAppointment(existingAppointment.Id);

            Assert.Equal(existingAppointment, appointment);
        }
    }
}
