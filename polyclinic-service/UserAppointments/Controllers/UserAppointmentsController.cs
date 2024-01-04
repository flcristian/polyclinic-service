using Microsoft.AspNetCore.Mvc;
using polyclinic_service.Appointments.Models;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.UserAppointments.Controllers.Interfaces;
using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service.UserAppointments.Services.Interfaces;

namespace polyclinic_service.UserAppointments.Controllers;

public class UserAppointmentsController : UserAppointmentsApiController
{
    private IUserAppointmentQueryService _queryService;
    private IUserAppointmentCommandService _commandService;

    private ILogger<UserAppointmentsController> _logger;

    public UserAppointmentsController(IUserAppointmentQueryService queryService,
        ILogger<UserAppointmentsController> logger, IUserAppointmentCommandService commandService)
    {
        _queryService = queryService;
        _commandService = commandService;
        _logger = logger;
    }

    public override async Task<ActionResult<IEnumerable<UserAppointment>>> GetAllUserAppointments()
    {
        _logger.LogInformation("Rest request: Get all user appointments.");
        try
        {
            IEnumerable<UserAppointment> result = await _queryService.GetAllUserAppointments();

            return Ok(result);
        }
        catch (ItemsDoNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<UserAppointment>> GetUserAppointmentById(int id)
    {
        _logger.LogInformation($"Rest request: Get user appointment with id {id}.");
        try
        {
            UserAppointment result = await _queryService.GetUserAppointmentById(id);

            return Ok(result);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
    
    public override async Task<ActionResult<UserAppointment>> CreateUserAppointment(CreateUserAppointmentRequest userAppointmentRequest)
    {
        _logger.LogInformation($"Rest request: Create userAppointment with DTO:\n{userAppointmentRequest}");
        UserAppointment response = await _commandService.CreateUserAppointment(userAppointmentRequest);

        return Created(Constants.USER_APPOINTMENT_CREATED, response);
    }

    public override async Task<ActionResult<UserAppointment>> UpdateUserAppointment(UpdateUserAppointmentRequest userAppointmentRequest)
    {
        _logger.LogInformation($"Rest request: Create userAppointment with DTO:\n{userAppointmentRequest}");
        try
        {
            UserAppointment response = await _commandService.UpdateUserAppointment(userAppointmentRequest);

            return Accepted(Constants.USER_APPOINTMENT_UPDATED, response);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult> DeleteUserAppointment(int id)
    {
        _logger.LogInformation($"Rest request: Delete userAppointment with id {id}");
        try
        {
            await _commandService.DeleteUserAppointment(id);

            return Accepted(Constants.USER_APPOINTMENT_DELETED, Constants.USER_APPOINTMENT_DELETED);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
    
    public override async Task<ActionResult<UserAppointment>> GetAppointmentsByUserId(int userId)
    {
        _logger.LogInformation($"Rest request: Get user appointments of the user with id {userId}.");
        try
        {
            IEnumerable<Appointment> result = await _queryService.GetAppointmentsByUserId(userId);

            return Ok(result);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
    
    public override async Task<ActionResult<UserAppointment>> GetAppointmentHistoryOfUserByUserId(int userId)
    {
        _logger.LogInformation($"Rest request: Get appointment history of the user with id {userId}.");
        try
        {
            IEnumerable<Appointment> result = await _queryService.GetAppointmentHistoryByUserId(userId);

            return Ok(result);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
}