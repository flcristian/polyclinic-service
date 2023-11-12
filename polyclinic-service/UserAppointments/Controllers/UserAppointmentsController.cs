using Microsoft.AspNetCore.Mvc;
using polyclinic_service.System.Exceptions;
using polyclinic_service.UserAppointments.Controllers.Interfaces;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service.UserAppointments.Services.Interfaces;

namespace polyclinic_service.UserAppointments.Controllers;

public class UserAppointmentsController : UserAppointmentsApiController
{
    private IUserAppointmentQueryService _queryService;

    private ILogger<UserAppointmentsController> _logger;

    public UserAppointmentsController(IUserAppointmentQueryService queryService,
        ILogger<UserAppointmentsController> logger)
    {
        _queryService = queryService;
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
}