using Moq;
using polyclinic_service_api_tests.Users.Helpers;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.Users.DTOs;
using polyclinic_service.Users.Models;
using polyclinic_service.Users.Repository.Interfaces;
using polyclinic_service.Users.Services;
using polyclinic_service.Users.Services.Interfaces;

namespace polyclinic_service_api_tests.Users;

public class UserCommandServiceTests
{
    private readonly Mock<IUserRepository> _mockRepository;
    private readonly IUserCommandService _service;

    public UserCommandServiceTests()
    {
        _mockRepository = new Mock<IUserRepository>();
        _service = new UserCommandService(_mockRepository.Object);
    }

    [Fact]
    public async Task CreateUser_EmailAlreadyUser_ThrowsItemAlreadyExistsException()
    {
        User user = TestsUsersHelper.CreateTestDoctor(1);

        _mockRepository.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);

        CreateUserRequest request = TestsUsersHelper.CreateTestCreateUserRequest(1);
        var exception = await Assert.ThrowsAsync<ItemAlreadyExists>(() => _service.CreateUser(request));
        
        Assert.Equal(Constants.USER_EMAIL_ALREADY_USED, exception.Message);
    }
}