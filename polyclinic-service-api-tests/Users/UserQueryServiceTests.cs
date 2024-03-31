using Moq;
using polyclinic_service_api_tests.Users.Helpers;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.Users.Models;
using polyclinic_service.Users.Models.Comparers;
using polyclinic_service.Users.Repository.Interfaces;
using polyclinic_service.Users.Services;
using polyclinic_service.Users.Services.Interfaces;

namespace polyclinic_service_api_tests.Users;

public class UserQueryServiceTests
{
    private readonly Mock<IUserRepository> _mockRepository;
    private readonly IUserQueryService _service;

    public UserQueryServiceTests()
    {
        _mockRepository = new Mock<IUserRepository>();
        _service = new UserQueryService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetAllUsers_NoUsers_ThrowsItemsDoNotExistException()
    {
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<User>());

        var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAllUsers());
        
        Assert.Equal(Constants.USERS_DO_NOT_EXIST, exception.Message);
    }

    [Fact]
    public async Task GetAllUsers_UsersExist_ReturnsUserList()
    {
        List<User> users = new List<User>
        {
            TestsUsersHelper.CreateTestDoctor(1),
            TestsUsersHelper.CreateTestPatient(2)
        };
        
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

        var result = await _service.GetAllUsers();
        
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(users, result, new UserEqualityComparer());
    }
    
    [Fact]
    public async Task GetUserById_UserNotFound_ThrowsItemDoesNotExistException()
    {
        _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((User)null!);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetUserById(1));
        
        Assert.Equal(Constants.USER_DOES_NOT_EXIST, exception.Message);
    }

    [Fact]
    public async Task GetUserById_UserExists_ReturnsUser()
    {
        User user = TestsUsersHelper.CreateTestDoctor(1);
        
        _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(user);

        var result = await _service.GetUserById(1);
        
        Assert.NotNull(result);
        Assert.Equal(user, result, new UserEqualityComparer());
    }

    [Fact]
    public async Task GetDoctorWithMostAppointments_NoDoctorsHaveAppointments_ThrowsItemDoesNotExistException()
    {
        _mockRepository.Setup(repo => repo.GetDoctorWithMostAppointmentsAsync()).ReturnsAsync((User)null!);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetDoctorWithMostAppointments());
        
        Assert.Equal(Constants.NO_DOCTORS_HAVE_APPOINTMENTS, exception.Message);
    }
    
    [Fact]
    public async Task GetDoctorWithMostAppointments_DoctorFound_ReturnsDoctor()
    {
        User user = TestsUsersHelper.CreateTestDoctor(1);
        
        _mockRepository.Setup(repo => repo.GetDoctorWithMostAppointmentsAsync()).ReturnsAsync(user);

        var result = await _service.GetDoctorWithMostAppointments();
        
        Assert.Equal(user, result, new UserEqualityComparer());
    }
    
    [Fact]
    public async Task GetPatientWithMostAppointments_NoPatientsHaveAppointments_ThrowsItemDoesNotExistException()
    {
        _mockRepository.Setup(repo => repo.GetPatientWithMostAppointmentsAsync()).ReturnsAsync((User)null!);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetPatientWithMostAppointments());
        
        Assert.Equal(Constants.NO_PATIENTS_HAVE_APPOINTMENTS, exception.Message);
    }
    
    [Fact]
    public async Task GetPatientWithMostAppointments_PatientFound_ReturnsPatient()
    {
        User user = TestsUsersHelper.CreateTestPatient(1);
        
        _mockRepository.Setup(repo => repo.GetPatientWithMostAppointmentsAsync()).ReturnsAsync(user);

        var result = await _service.GetPatientWithMostAppointments();
        
        Assert.Equal(user, result, new UserEqualityComparer());
    }

    [Fact]
    public async Task GetDoctorsByAppointmentsIncreasing_NoDoctors_ThrowsItemsDoNotExistException()
    {
        _mockRepository.Setup(repo => repo.GetDoctorsByAppointmentsIncreasingAsync()).ReturnsAsync(new List<User>());

        var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetDoctorsByAppointmentsIncreasing());
        
        Assert.Equal(Constants.NO_DOCTORS_HAVE_APPOINTMENTS, exception.Message);
    }
    
    [Fact]
    public async Task GetDoctorsByAppointmentsIncreasing_DoctorsFound_ReturnsDoctorList()
    {
        List<User> doctors = new List<User>
        {
            TestsUsersHelper.CreateTestDoctor(1),
            TestsUsersHelper.CreateTestDoctor(2)
        };
        
        _mockRepository.Setup(repo => repo.GetDoctorsByAppointmentsIncreasingAsync()).ReturnsAsync(doctors);

        var result = await _service.GetDoctorsByAppointmentsIncreasing();
        
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(doctors, result, new UserEqualityComparer());
    }
    
    [Fact]
    public async Task GetDoctorsByAppointmentsDecreasing_NoDoctors_ThrowsItemsDoNotExistException()
    {
        _mockRepository.Setup(repo => repo.GetDoctorsByAppointmentsDecreasingAsync()).ReturnsAsync(new List<User>());

        var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetDoctorsByAppointmentsDecreasing());
        
        Assert.Equal(Constants.NO_DOCTORS_HAVE_APPOINTMENTS, exception.Message);
    }
    
    [Fact]
    public async Task GetDoctorsByAppointmentsDecreasing_DoctorsFound_ReturnsDoctorList()
    {
        List<User> doctors = new List<User>
        {
            TestsUsersHelper.CreateTestDoctor(1),
            TestsUsersHelper.CreateTestDoctor(2)
        };
        
        _mockRepository.Setup(repo => repo.GetDoctorsByAppointmentsDecreasingAsync()).ReturnsAsync(doctors);

        var result = await _service.GetDoctorsByAppointmentsDecreasing();
        
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(doctors, result, new UserEqualityComparer());
    }
    
    [Fact]
    public async Task GetPatientsByAppointmentsIncreasing_NoPatients_ThrowsItemsDoNotExistException()
    {
        _mockRepository.Setup(repo => repo.GetPatientsByAppointmentsIncreasingAsync()).ReturnsAsync(new List<User>());

        var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetPatientsByAppointmentsIncreasing());
        
        Assert.Equal(Constants.NO_PATIENTS_HAVE_APPOINTMENTS, exception.Message);
    }
    
    [Fact]
    public async Task GetPatientsByAppointmentsIncreasing_PatientsFound_ReturnsPatientList()
    {
        List<User> patients = new List<User>
        {
            TestsUsersHelper.CreateTestPatient(1),
            TestsUsersHelper.CreateTestPatient(2)
        };
        
        _mockRepository.Setup(repo => repo.GetPatientsByAppointmentsIncreasingAsync()).ReturnsAsync(patients);

        var result = await _service.GetPatientsByAppointmentsIncreasing();
        
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(patients, result, new UserEqualityComparer());
    }
    
    [Fact]
    public async Task GetPatientsByAppointmentsDecreasing_NoPatients_ThrowsItemsDoNotExistException()
    {
        _mockRepository.Setup(repo => repo.GetPatientsByAppointmentsDecreasingAsync()).ReturnsAsync(new List<User>());

        var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetPatientsByAppointmentsDecreasing());
        
        Assert.Equal(Constants.NO_PATIENTS_HAVE_APPOINTMENTS, exception.Message);
    }
    
    [Fact]
    public async Task GetPatientsByAppointmentsDecreasing_PatientsFound_ReturnsPatientList()
    {
        List<User> patients = new List<User>
        {
            TestsUsersHelper.CreateTestPatient(1),
            TestsUsersHelper.CreateTestPatient(2)
        };
        
        _mockRepository.Setup(repo => repo.GetPatientsByAppointmentsDecreasingAsync()).ReturnsAsync(patients);

        var result = await _service.GetPatientsByAppointmentsDecreasing();
        
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(patients, result, new UserEqualityComparer());
    }
}