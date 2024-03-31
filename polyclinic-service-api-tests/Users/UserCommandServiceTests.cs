using Moq;
using polyclinic_service_api_tests.Users.Helpers;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.Users.DTOs;
using polyclinic_service.Users.Models;
using polyclinic_service.Users.Models.Comparers;
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
    
    [Fact]
    public async Task CreateUser_ValidRequest_ReturnsCreatedUser()
    {
        CreateUserRequest request = TestsUsersHelper.CreateTestCreateUserRequest(1);
        User expectedUser = TestsUsersHelper.CreateTestUserFromCreateRequest(1, request);
        
        _mockRepository.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null!);
        _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<CreateUserRequest>())).ReturnsAsync(expectedUser);

        var result = await _service.CreateUser(request);
        
        Assert.NotNull(result);
        Assert.Equal(expectedUser, result, new UserEqualityComparer());
    }
    
    [Fact]
    public async Task UpdateUser_EmailAlreadyUser_ThrowsItemAlreadyExistsException()
    {
        User user = TestsUsersHelper.CreateTestDoctor(1);

        _mockRepository.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);

        UpdateUserRequest request = TestsUsersHelper.CreateTestUpdateUserRequest(1);
        var exception = await Assert.ThrowsAsync<ItemAlreadyExists>(() => _service.UpdateUser(request));
        
        Assert.Equal(Constants.USER_EMAIL_ALREADY_USED, exception.Message);
    }
    
    [Fact]
    public async Task UpdateUser_UserDoesNotExist_ThrowsItemDoesNotExistException()
    {
        _mockRepository.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null!);
        _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((User)null!);

        UpdateUserRequest request = TestsUsersHelper.CreateTestUpdateUserRequest(1);
        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateUser(request));
        
        Assert.Equal(Constants.USER_DOES_NOT_EXIST, exception.Message);
    }
    
    [Fact]
    public async Task UpdateUser_ValidRequest_ReturnsUpdatedUser()
    {
        UpdateUserRequest request = TestsUsersHelper.CreateTestUpdateUserRequest(1);
        User expectedUser = TestsUsersHelper.CreateTestUserFromUpdateRequest(request);
        User oldUser = TestsUsersHelper.CreateTestDoctor(1);
        
        _mockRepository.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null!);
        _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(oldUser);
        _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<UpdateUserRequest>())).ReturnsAsync(expectedUser);

        var result = await _service.UpdateUser(request);
        
        Assert.NotNull(result);
        Assert.Equal(expectedUser, result, new UserEqualityComparer());
    }
    
    [Fact]
    public async Task DeleteUser_UserDoesNotExist_ThrowsItemDoesNotExistException()
    {
        _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((User)null!);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeleteUser(1));
        
        Assert.Equal(Constants.USER_DOES_NOT_EXIST, exception.Message);
    }
    
    [Fact]
    public async Task DeleteUser_ValidRequest_ReturnsUpdatedUser()
    {
        User expectedUser = TestsUsersHelper.CreateTestDoctor(1);
        
        _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(expectedUser);
        _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<UpdateUserRequest>())).ReturnsAsync(expectedUser);

        var result = await _service.DeleteUser(1);
        
        Assert.NotNull(result);
        Assert.Equal(expectedUser, result, new UserEqualityComparer());
    }
}