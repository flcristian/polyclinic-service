using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using polyclinic_service.Appointments.Controllers.Interfaces;
using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;
using polyclinic_service.Appointments.Services.Interfaces;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;

namespace polyclinic_service.Appointments.Controllers;

public class AppointmentsController : AppointmentsApiController
{ 
    private IAppointmentQueryService _queryService;
    private IAppointmentCommandService _commandService;

    private ILogger<AppointmentsController> _logger;
    
    public AppointmentsController(IAppointmentQueryService queryService, IAppointmentCommandService commandService, ILogger<AppointmentsController> logger)
    {
        _queryService = queryService;
        _commandService = commandService;
        _logger = logger;
    }
    
    public override async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
    {
        _logger.LogInformation("Rest request: Get all appointments.");
        try
        {
            IEnumerable<Appointment> result = await _queryService.GetAllAppointments();
            
            return Ok(result);
        }
        catch (ItemsDoNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<Appointment>> GetAppointmentById(int id)
    {
        _logger.LogInformation($"Rest request: Get appointment with id {id}.");
        try
        {
            Appointment result = await _queryService.GetAppointmentById(id);

            return Ok(result);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<Appointment>> CreateAppointment(CreateAppointmentRequest appointmentRequest)
    {
        _logger.LogInformation($"Rest request: Create appointment with DTO:\n{appointmentRequest}");
        Appointment response = await _commandService.CreateAppointment(appointmentRequest);

        return Created(Constants.APPOINTMENT_CREATED, response);
    }

    public override async Task<ActionResult<Appointment>> UpdateAppointment(UpdateAppointmentRequest appointmentRequest)
    {
        _logger.LogInformation($"Rest request: Create appointment with DTO:\n{appointmentRequest}");
        try
        {
            Appointment response = await _commandService.UpdateAppointment(appointmentRequest);

            return Accepted(Constants.APPOINTMENT_UPDATED, response);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult> DeleteAppointment(int id)
    {
        _logger.LogInformation($"Rest request: Delete appointment with id {id}");
        try
        {
            await _commandService.DeleteAppointment(id);

            return Accepted(Constants.APPOINTMENT_DELETED, Constants.APPOINTMENT_DELETED);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
}