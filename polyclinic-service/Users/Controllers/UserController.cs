using Microsoft.AspNetCore.Mvc;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.Users.Controllers.Interfaces;
using polyclinic_service.Users.DTOs;
using polyclinic_service.Users.Models;
using polyclinic_service.Users.Services.Interfaces;

namespace polyclinic_service.Users.Controllers;

public class UserController : UserApiController
{
    private IUserQueryService _queryService;
    private IUserCommandService _commandService;
    private ILogger<UserController> _logger;
    
    public UserController(IUserQueryService queryService, IUserCommandService commandService, ILogger<UserController> logger)
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
        try
        {
            User response = await _commandService.CreateUser(userRequest);

            return Created(Constants.USER_CREATED, response);
        }
        catch (ItemAlreadyExists ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    public override async Task<ActionResult<User>> UpdateUser(UpdateUserRequest userRequest)
    {
        _logger.LogInformation($"Rest request: Create user with DTO:\n{userRequest}");
        try
        {
            User response = await _commandService.UpdateUser(userRequest);

            return Accepted(Constants.USER_UPDATED, response);
        }
        catch (ItemAlreadyExists ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return BadRequest(ex.Message);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<User>> DeleteUser(int id)
    {
        _logger.LogInformation($"Rest request: Delete user with id {id}");
        try
        {
            User user = await _commandService.DeleteUser(id);

            return Accepted(Constants.USER_DELETED, user);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<User>> GetDoctorWithMostAppointments()
    {
        _logger.LogInformation($"Rest request: Get doctor with most appointments");
        try
        {
            User doctor = await _queryService.GetDoctorWithMostAppointments();

            return Ok(doctor);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
    
    public override async Task<ActionResult<User>> GetPatientWithMostAppointments()
    {
        _logger.LogInformation($"Rest request: Get patient with most appointments");
        try
        {
            User patient = await _queryService.GetPatientWithMostAppointments();

            return Ok(patient);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<IEnumerable<User>>> GetAllDoctorsSortedByAppointmentsDecreasing()
    {
        _logger.LogInformation($"Rest request: Get all doctors sorted by appointment count in decreasing order.");
        try
        {
            IEnumerable<User> doctors = await _queryService.GetDoctorsByAppointmentsDecreasing();

            return Ok(doctors);
        }
        catch (ItemsDoNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<IEnumerable<User>>> GetAllDoctorsSortedByAppointmentsIncreasing()
    {
        _logger.LogInformation($"Rest request: Get all doctors sorted by appointment count in increasing order.");
        try
        {
            IEnumerable<User> doctors = await _queryService.GetDoctorsByAppointmentsIncreasing();

            return Ok(doctors);
        }
        catch (ItemsDoNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
    
    public override async Task<ActionResult<IEnumerable<User>>> GetAllPatientsSortedByAppointmentsDecreasing()
    {
        _logger.LogInformation($"Rest request: Get all patients sorted by appointment count in decreasing order.");
        try
        {
            IEnumerable<User> patients = await _queryService.GetPatientsByAppointmentsDecreasing();

            return Ok(patients);
        }
        catch (ItemsDoNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<IEnumerable<User>>> GetAllPatientsSortedByAppointmentsIncreasing()
    {
        _logger.LogInformation($"Rest request: Get all patients sorted by appointment count in increasing order.");
        try
        {
            IEnumerable<User> patients = await _queryService.GetPatientsByAppointmentsIncreasing();

            return Ok(patients);
        }
        catch (ItemsDoNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
}