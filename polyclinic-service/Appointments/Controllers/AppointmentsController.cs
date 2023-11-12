using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using polyclinic_service.Appointments.Controllers.Interfaces;
using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;
using polyclinic_service.Appointments.Services.Interfaces;
using polyclinic_service.System.Exceptions;

namespace polyclinic_service.Appointments.Controllers;

public class AppointmentsController : AppointmentsApiController
{ 
    private IAppointmentQueryService _queryService;
    private IAppointmentCommandService _commandService;
    
    public AppointmentsController(IAppointmentQueryService queryService, IAppointmentCommandService commandService)
    {
        _queryService = queryService;
        _commandService = commandService;
    }
    
    public override async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
    {
        try
        {
            IEnumerable<Appointment> result = await _queryService.GetAllAppointments();

            return Ok(result);
        }
        catch (ItemsDoNotExist ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public override async Task<ActionResult<Appointment>> GetAppointmentById(int id)
    {
        try
        {
            Appointment result = await _queryService.GetAppointmentById(id);

            return Ok(result);
        }
        catch (ItemDoesNotExist ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public override async Task<ActionResult<Appointment>> CreateAppointment(CreateAppointmentRequest appointmentRequest)
    {
        Appointment response = await _commandService.CreateAppointment(appointmentRequest);

        return Ok(response);
    }

    public override async Task<ActionResult<Appointment>> UpdateAppointment(int id, UpdateAppointmentRequest appointmentRequest)
    {
        throw new NotImplementedException();
    }

    public override async Task<ActionResult> DeleteAppointment(int id)
    {
        throw new NotImplementedException();
    }
}