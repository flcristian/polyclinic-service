using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using polyclinic_service_api_tests.Users.Helpers;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.Users.Controllers;
using polyclinic_service.Users.Controllers.Interfaces;
using polyclinic_service.Users.DTOs;
using polyclinic_service.Users.Models;
using polyclinic_service.Users.Models.Comparers;
using polyclinic_service.Users.Services.Interfaces;

namespace polyclinic_service_api_tests.Users;

public class UserControllerTests
{
    private readonly Mock<IUserQueryService> _mockQueryService;
    private readonly Mock<IUserCommandService> _mockCommandService;
    private readonly Mock<ILogger<UserController>> _logger;
    private readonly UserApiController _controller;
    
    public UserControllerTests()
    {
        _mockQueryService = new Mock<IUserQueryService>();
        _mockCommandService = new Mock<IUserCommandService>();
        _logger = new Mock<ILogger<UserController>>();
        _controller = new UserController(_mockQueryService.Object, _mockCommandService.Object, _logger.Object);
    }

    [Fact]
    public async Task GetAllUsers_NoUsersExist_ReturnsNotFound()
    {
        _mockQueryService.Setup(s => s.GetAllUsers())
            .ThrowsAsync(new ItemsDoNotExist(Constants.USERS_DO_NOT_EXIST));

        var result = await _controller.GetAllUsers();

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(Constants.USERS_DO_NOT_EXIST, notFoundResult.Value);
        Assert.Equal(404, notFoundResult.StatusCode);
    }
    
    [Fact]
    public async Task GetAllUsers_UsersExist_ReturnsOkWithUsers()
    {
        List<User> users = TestsUsersHelper.CreateTestUsers(3);
        
        _mockQueryService.Setup(s => s.GetAllUsers())
            .ReturnsAsync(users);

        var result = await _controller.GetAllUsers();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUsers = Assert.IsType<List<User>>(okResult.Value);
        Assert.Equal(3, returnedUsers.Count);
        Assert.Equal(users, returnedUsers, new UserEqualityComparer());
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task GetUserById_UserNotFound_ReturnsNotFound()
    {
        _mockQueryService.Setup(repo => repo.GetUserById(It.IsAny<int>()))
            .ThrowsAsync(new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST));

        var result = await _controller.GetUserById(1);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(Constants.USER_DOES_NOT_EXIST, notFoundResult.Value);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task GetUserById_UserFound_ReturnsOkWithUser()
    {
        User user = TestsUsersHelper.CreateTestDoctor(1);

        _mockQueryService.Setup(repo => repo.GetUserById(It.IsAny<int>()))
            .ReturnsAsync(user);

        var result = await _controller.GetUserById(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUser = Assert.IsType<User>(okResult.Value);
        Assert.Equal(user, returnedUser, new UserEqualityComparer());
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task CreateUser_EmailTaken_ReturnsBadRequest()
    {
        var request = TestsUsersHelper.CreateTestCreateUserRequest(1);
        
        _mockCommandService.Setup(repo => repo.CreateUser(It.IsAny<CreateUserRequest>()))
            .ThrowsAsync(new ItemAlreadyExists(Constants.USER_EMAIL_ALREADY_USED));

        var result = await _controller.CreateUser(request);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(Constants.USER_EMAIL_ALREADY_USED, badRequestResult.Value);
        Assert.Equal(400, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task CreateUser_ValidRequest_ReturnsCreatedWithUser()
    {
        var request = TestsUsersHelper.CreateTestCreateUserRequest(1);
        var user = TestsUsersHelper.CreateTestUserFromCreateRequest(1, request);

        _mockCommandService.Setup(repo => repo.CreateUser(It.IsAny<CreateUserRequest>()))
            .ReturnsAsync(user);

        var result = await _controller.CreateUser(request);

        var createdResult = Assert.IsType<CreatedResult>(result.Result);
        var createdUser = Assert.IsType<User>(createdResult.Value);
        Assert.Equal(user, createdUser, new UserEqualityComparer());
        Assert.Equal(201, createdResult.StatusCode);
    }

    [Fact]
    public async Task UpdateUser_EmailTaken_ReturnsBadRequest()
    {
        var request = TestsUsersHelper.CreateTestUpdateUserRequest(1);
        
        _mockCommandService.Setup(repo => repo.UpdateUser(It.IsAny<UpdateUserRequest>()))
            .ThrowsAsync(new ItemAlreadyExists(Constants.USER_EMAIL_ALREADY_USED));

        var result = await _controller.UpdateUser(request);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(Constants.USER_EMAIL_ALREADY_USED, badRequestResult.Value);
        Assert.Equal(400, badRequestResult.StatusCode);
    }
    
    [Fact]
    public async Task UpdateUser_UserDoesNotExist_ReturnsNotFound()
    {
        var request = TestsUsersHelper.CreateTestUpdateUserRequest(1);
        
        _mockCommandService.Setup(repo => repo.UpdateUser(It.IsAny<UpdateUserRequest>()))
            .ThrowsAsync(new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST));

        var result = await _controller.UpdateUser(request);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(Constants.USER_DOES_NOT_EXIST, notFoundResult.Value);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task UpdateUser_ValidRequest_ReturnsAcceptedWithUser()
    {
        var request = TestsUsersHelper.CreateTestUpdateUserRequest(1);
        var user = TestsUsersHelper.CreateTestUserFromUpdateRequest(request);

        _mockCommandService.Setup(repo => repo.UpdateUser(It.IsAny<UpdateUserRequest>()))
            .ReturnsAsync(user);
        
        var result = await _controller.UpdateUser(request);

        var acceptedResult = Assert.IsType<AcceptedResult>(result.Result);
        var updatedUser = Assert.IsType<User>(acceptedResult.Value);
        Assert.Equal(user, updatedUser, new UserEqualityComparer());
        Assert.Equal(202, acceptedResult.StatusCode);
    }

    [Fact]
    public async Task DeleteUser_UserDoesNotExist_ReturnsNotFound()
    {
        _mockCommandService.Setup(repo => repo.DeleteUser(It.IsAny<int>()))
            .ThrowsAsync(new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST));

        var result = await _controller.DeleteUser(1);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(Constants.USER_DOES_NOT_EXIST, notFoundResult.Value);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task DeleteUser_UserExists_ReturnsAcceptedWithUser()
    {
        var user = TestsUsersHelper.CreateTestDoctor(1);

        _mockCommandService.Setup(repo => repo.DeleteUser(It.IsAny<int>()))
            .ReturnsAsync(user);

        var result = await _controller.DeleteUser(1);
        
        var acceptedResult = Assert.IsType<AcceptedResult>(result.Result);
        var deletedUser = Assert.IsType<User>(acceptedResult.Value);
        Assert.Equal(user, deletedUser, new UserEqualityComparer());
        Assert.Equal(202, acceptedResult.StatusCode);
    }

    [Fact]
    public async Task GetDoctorWithMostAppointments_NoDoctors_ReturnsNotFound()
    {
        _mockQueryService.Setup(repo => repo.GetDoctorWithMostAppointments())
            .ThrowsAsync(new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST));

        var result = await _controller.GetDoctorWithMostAppointments();

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(Constants.USER_DOES_NOT_EXIST, notFoundResult.Value);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task GetDoctorWithMostAppointments_DoctorFound_ReturnsOkWithDoctor()
    {
        User user = TestsUsersHelper.CreateTestDoctor(1);

        _mockQueryService.Setup(repo => repo.GetDoctorWithMostAppointments())
            .ReturnsAsync(user);

        var result = await _controller.GetDoctorWithMostAppointments();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUser = Assert.IsType<User>(okResult.Value);
        Assert.Equal(user, returnedUser, new UserEqualityComparer());
        Assert.Equal(200, okResult.StatusCode);
    }
    
    [Fact]
    public async Task GetPatientWithMostAppointments_NoPatients_ReturnsNotFound()
    {
        _mockQueryService.Setup(repo => repo.GetPatientWithMostAppointments())
            .ThrowsAsync(new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST));

        var result = await _controller.GetPatientWithMostAppointments();

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(Constants.USER_DOES_NOT_EXIST, notFoundResult.Value);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task GetPatientWithMostAppointments_PatientFound_ReturnsOkWithPatient()
    {
        User user = TestsUsersHelper.CreateTestPatient(1);

        _mockQueryService.Setup(repo => repo.GetPatientWithMostAppointments())
            .ReturnsAsync(user);

        var result = await _controller.GetPatientWithMostAppointments();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUser = Assert.IsType<User>(okResult.Value);
        Assert.Equal(user, returnedUser, new UserEqualityComparer());
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task GetAllDoctorsSortedByAppointmentsDecreasing_NoDoctors_ReturnsNotFound()
    {
        _mockQueryService.Setup(repo => repo.GetDoctorsByAppointmentsDecreasing())
            .ThrowsAsync(new ItemsDoNotExist(Constants.USERS_DO_NOT_EXIST));

        var result = await _controller.GetAllDoctorsSortedByAppointmentsDecreasing();
        
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(Constants.USERS_DO_NOT_EXIST, notFoundResult.Value);
        Assert.Equal(404, notFoundResult.StatusCode);
    }
    
    [Fact]
    public async Task GetAllDoctorsSortedByAppointmentsDecreasing_DoctorsFound_ReturnsOkWithDoctors()
    {
        List<User> doctors = TestsUsersHelper.CreateTestDoctors(3);
        
        _mockQueryService.Setup(repo => repo.GetDoctorsByAppointmentsDecreasing())
            .ReturnsAsync(doctors);

        var result = await _controller.GetAllDoctorsSortedByAppointmentsDecreasing();
        
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUsers = Assert.IsType<List<User>>(okResult.Value);
        Assert.Equal(3, returnedUsers.Count);
        Assert.Equal(doctors, returnedUsers, new UserEqualityComparer());
        Assert.Equal(200, okResult.StatusCode);
    }
    
    [Fact]
    public async Task GetAllDoctorsSortedByAppointmentsIncreasing_NoDoctors_ReturnsNotFound()
    {
        _mockQueryService.Setup(repo => repo.GetDoctorsByAppointmentsIncreasing())
            .ThrowsAsync(new ItemsDoNotExist(Constants.USERS_DO_NOT_EXIST));

        var result = await _controller.GetAllDoctorsSortedByAppointmentsIncreasing();
        
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(Constants.USERS_DO_NOT_EXIST, notFoundResult.Value);
        Assert.Equal(404, notFoundResult.StatusCode);
    }
    
    [Fact]
    public async Task GetAllDoctorsSortedByAppointmentsIncreasing_DoctorsFound_ReturnsOkWithDoctors()
    {
        List<User> doctors = TestsUsersHelper.CreateTestDoctors(3);
        
        _mockQueryService.Setup(repo => repo.GetDoctorsByAppointmentsIncreasing())
            .ReturnsAsync(doctors);

        var result = await _controller.GetAllDoctorsSortedByAppointmentsIncreasing();
        
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUsers = Assert.IsType<List<User>>(okResult.Value);
        Assert.Equal(3, returnedUsers.Count);
        Assert.Equal(doctors, returnedUsers, new UserEqualityComparer());
        Assert.Equal(200, okResult.StatusCode);
    }
    
    [Fact]
    public async Task GetAllPatientsSortedByAppointmentsDecreasing_NoPatients_ReturnsNotFound()
    {
        _mockQueryService.Setup(repo => repo.GetPatientsByAppointmentsDecreasing())
            .ThrowsAsync(new ItemsDoNotExist(Constants.USERS_DO_NOT_EXIST));

        var result = await _controller.GetAllPatientsSortedByAppointmentsDecreasing();
        
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(Constants.USERS_DO_NOT_EXIST, notFoundResult.Value);
        Assert.Equal(404, notFoundResult.StatusCode);
    }
    
    [Fact]
    public async Task GetAllPatientsSortedByAppointmentsDecreasing_PatientsFound_ReturnsOkWithPatients()
    {
        List<User> patients = TestsUsersHelper.CreateTestPatients(3);
        
        _mockQueryService.Setup(repo => repo.GetPatientsByAppointmentsDecreasing())
            .ReturnsAsync(patients);

        var result = await _controller.GetAllPatientsSortedByAppointmentsDecreasing();
        
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUsers = Assert.IsType<List<User>>(okResult.Value);
        Assert.Equal(3, returnedUsers.Count);
        Assert.Equal(patients, returnedUsers, new UserEqualityComparer());
        Assert.Equal(200, okResult.StatusCode);
    }
    
    [Fact]
    public async Task GetAllPatientsSortedByAppointmentsIncreasing_NoPatients_ReturnsNotFound()
    {
        _mockQueryService.Setup(repo => repo.GetPatientsByAppointmentsIncreasing())
            .ThrowsAsync(new ItemsDoNotExist(Constants.USERS_DO_NOT_EXIST));

        var result = await _controller.GetAllPatientsSortedByAppointmentsIncreasing();
        
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(Constants.USERS_DO_NOT_EXIST, notFoundResult.Value);
        Assert.Equal(404, notFoundResult.StatusCode);
    }
    
    [Fact]
    public async Task GetAllPatientsSortedByAppointmentsIncreasing_PatientsFound_ReturnsOkWithPatients()
    {
        List<User> patients = TestsUsersHelper.CreateTestPatients(3);
        
        _mockQueryService.Setup(repo => repo.GetPatientsByAppointmentsIncreasing())
            .ReturnsAsync(patients);

        var result = await _controller.GetAllPatientsSortedByAppointmentsIncreasing();
        
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUsers = Assert.IsType<List<User>>(okResult.Value);
        Assert.Equal(3, returnedUsers.Count);
        Assert.Equal(patients, returnedUsers, new UserEqualityComparer());
        Assert.Equal(200, okResult.StatusCode);
    }
}