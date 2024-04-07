using Moq;
using polyclinic_service.System.Exceptions;
using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service.UserAppointments.Repository.Interfaces;
using polyclinic_service.UserAppointments.Services;
using polyclinic_service.UserAppointments.Services.Interfaces;

namespace polyclinic_service_api_tests.UserAppointments
{
    public class UserAppointmentCommandServiceTests
    {
        private readonly Mock<IUserAppointmentRepository> _mockRepository;
        private readonly IUserAppointmentCommandService _service;

        public UserAppointmentCommandServiceTests()
        {
            _mockRepository = new Mock<IUserAppointmentRepository>();
            _service = new UserAppointmentCommandService(_mockRepository.Object);
        }

        [Fact]
        public async Task CreateUserAppointment_ValidRequest_ReturnsCreatedUserAppointment()
        {
            var request = new CreateUserAppointmentRequest();
            var expectedUserAppointment = new UserAppointment();
            _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<CreateUserAppointmentRequest>())).ReturnsAsync(expectedUserAppointment);
            var result = await _service.CreateUserAppointment(request);
            Assert.NotNull(result);
            Assert.Equal(expectedUserAppointment, result);
        }

        [Fact]
        public async Task UpdateUserAppointment_UserAppointmentDoesNotExist_ThrowsItemDoesNotExistException()
        {
            var request = new UpdateUserAppointmentRequest();
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((UserAppointment)null);
            await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateUserAppointment(request));
        }

        [Fact]
        public async Task UpdateUserAppointment_ValidRequest_ReturnsUpdatedUserAppointment()
        {
            var request = new UpdateUserAppointmentRequest();
            var expectedUserAppointment = new UserAppointment();
            var oldUserAppointment = new UserAppointment();
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(oldUserAppointment);
            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<UpdateUserAppointmentRequest>())).ReturnsAsync(expectedUserAppointment);
            var result = await _service.UpdateUserAppointment(request);
            Assert.NotNull(result);
            Assert.Equal(expectedUserAppointment, result);
        }

        [Fact]
        public async Task DeleteUserAppointment_UserAppointmentDoesNotExist_ThrowsItemDoesNotExistException()
        {
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((UserAppointment)null);
            await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeleteUserAppointment(1));
        }

        [Fact]
        public async Task DeleteUserAppointment_ValidRequest_ReturnsDeletedUserAppointment()
        {
            var expectedUserAppointment = new UserAppointment();
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(expectedUserAppointment);
            _mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>())).ReturnsAsync(expectedUserAppointment);
            var result = await _service.DeleteUserAppointment(1);
            Assert.NotNull(result);
            Assert.Equal(expectedUserAppointment, result);
        }
    }
}
