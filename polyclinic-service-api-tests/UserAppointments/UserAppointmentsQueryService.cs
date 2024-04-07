using Moq;
using polyclinic_service_api_tests.UserAppointments.Helpers;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service.UserAppointments.Repository.Interfaces;
using polyclinic_service.UserAppointments.Services;
using polyclinic_service.UserAppointments.Services.Interfaces;

namespace polyclinic_service_api_tests.UserAppointments 
{
    public class UserAppointmentQueryServiceTests
    {
        private readonly Mock<IUserAppointmentRepository> _mockRepository;
        private readonly IUserAppointmentQueryService _service;

        public UserAppointmentQueryServiceTests()
        {
            _mockRepository = new Mock<IUserAppointmentRepository>();
            _service = new UserAppointmentQueryService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllUserAppointments_NoUserAppointments_ThrowsItemsDoNotExistException()
        {
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<UserAppointment>());

            var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAllUserAppointments());
            
            Assert.Equal(Constants.USER_APPOINTMENTS_DO_NOT_EXIST, exception.Message);
        }

        [Fact]
        public async Task GetAllUserAppointments_UserAppointmentsExist_ReturnsUserAppointmentList()
        {
            List<UserAppointment> appointments = new List<UserAppointment>
            {
                new UserAppointment(),
                new UserAppointment()
            };
            
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(appointments);

            var result = await _service.GetAllUserAppointments();
            
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
        
        [Fact]
        public async Task GetUserAppointmentById_UserAppointmentNotFound_ThrowsItemDoesNotExistException()
        {
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((UserAppointment)null!);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetUserAppointmentById(1));
            
            Assert.Equal(Constants.USER_APPOINTMENT_DOES_NOT_EXIST, exception.Message);
        }

        [Fact]
        public async Task GetUserAppointmentById_UserAppointmentExists_ReturnsUserAppointment()
        {
            UserAppointment userAppointment = new UserAppointment();
            
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(userAppointment);

            var result = await _service.GetUserAppointmentById(1);
            
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAppointmentsByUserId_NoAppointmentsForUser_ThrowsItemsDoNotExistException()
        {
            int userId = 1;
            _mockRepository.Setup(repo => repo.GetUserAppointmentsByUserId(userId)).ReturnsAsync(new List<UserAppointment>());

            var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAppointmentsByUserId(userId));
            
            Assert.Equal(Constants.APPOINTMENTS_DO_NOT_EXIST, exception.Message);
        }
        
        [Fact]
        public async Task GetAppointmentsByUserId_AppointmentsFoundForUser_ReturnsAppointmentList()
        {
            int userId = 1;
            List<UserAppointment> userAppointments = new List<UserAppointment>
            {
                TestsUserAppointmentsHelpers.CreateUserAppointment(1),
                TestsUserAppointmentsHelpers.CreateUserAppointment(2)
            };
            
            _mockRepository.Setup(repo => repo.GetUserAppointmentsByUserId(userId)).ReturnsAsync(userAppointments);

            var result = await _service.GetAppointmentsByUserId(userId);
            
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
        
        [Fact]
        public async Task GetAppointmentHistoryByUserId_NoAppointmentHistoryForUser_ThrowsItemsDoNotExistException()
        {
            int userId = 1;
            _mockRepository.Setup(repo => repo.GetUserAppointmentsByUserId(userId)).ReturnsAsync(new List<UserAppointment>());

            var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAppointmentHistoryByUserId(userId));
            
            Assert.Equal(Constants.APPOINTMENTS_DO_NOT_EXIST, exception.Message);
        }
        
        [Fact]
        public async Task GetAppointmentHistoryByUserId_AppointmentHistoryFoundForUser_ReturnsAppointmentList()
        {
            int userId = 1;
            List<UserAppointment> userAppointments = new List<UserAppointment>
            {
                TestsUserAppointmentsHelpers.CreateUserAppointment(1),
                TestsUserAppointmentsHelpers.CreateUserAppointment(2)
            };

            userAppointments[0].Appointment.StartDate = DateTime.UtcNow.AddDays(-10);
            userAppointments[0].Appointment.EndDate = DateTime.UtcNow.AddDays(-10).AddHours(1);
            userAppointments[1].Appointment.StartDate = DateTime.UtcNow.AddDays(-10);
            userAppointments[1].Appointment.EndDate = DateTime.UtcNow.AddDays(-10).AddHours(1);
            
            _mockRepository.Setup(repo => repo.GetUserAppointmentsByUserId(userId)).ReturnsAsync(userAppointments);

            var result = await _service.GetAppointmentHistoryByUserId(userId);
            
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
