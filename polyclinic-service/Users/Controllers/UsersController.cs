using Microsoft.AspNetCore.Mvc;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.Users.Controllers.Interfaces;
using polyclinic_service.Users.DTOs;
using polyclinic_service.Users.Models;
using polyclinic_service.Users.Services.Interfaces;

namespace polyclinic_service.Users.Controllers;

public class UsersController : UsersApiController
{
    private IUserQueryService _queryService;
    private IUserCommandService _commandService;

    private ILogger<UsersController> _logger;
    
    public UsersController(IUserQueryService queryService, IUserCommandService commandService, ILogger<UsersController> logger)
    {
        _queryService = queryService;
        _commandService = commandService;
        _logger = logger;
    }
    
    public override async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        _logger.LogInformation("Rest request: Get all users.");
        try
        {
            IEnumerable<User> result = await _queryService.GetAllUsers();
            
            return Ok(result);
        }
        catch (ItemsDoNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<User>> GetUserById(int id)
    {
        _logger.LogInformation($"Rest request: Get user with id {id}.");
        try
        {
            User result = await _queryService.GetUserById(id);

            return Ok(result);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<User>> CreateUser(CreateUserRequest userRequest)
    {
        _logger.LogInformation($"Rest request: Create user with DTO:\n{userRequest}");
        User response = await _commandService.CreateUser(userRequest);

        return Created(Constants.USER_CREATED, response);
    }

    public override async Task<ActionResult<User>> UpdateUser(UpdateUserRequest userRequest)
    {
        _logger.LogInformation($"Rest request: Create user with DTO:\n{userRequest}");
        try
        {
            User response = await _commandService.UpdateUser(userRequest);

            return Accepted(Constants.USER_UPDATED, response);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult> DeleteUser(int id)
    {
        _logger.LogInformation($"Rest request: Delete user with id {id}");
        try
        {
            await _commandService.DeleteUser(id);

            return Accepted(Constants.USER_DELETED, Constants.USER_DELETED);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
}