using Microsoft.AspNetCore.Mvc;
using polyclinic_service.Appointments.Controllers.Interfaces;
using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;
using polyclinic_service.Appointments.Services.Interfaces;

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
            IEnumerable<Appointment> result = await
        }
    }

    public override Task<ActionResult<Appointment>> GetAppointmentById(int id)
    {
        throw new NotImplementedException();
    }

    public override Task<ActionResult<Appointment>> CreateAppointment(CreateAppointmentRequest appointmentRequest)
    {
        throw new NotImplementedException();
    }

    public override Task<ActionResult<Appointment>> UpdateAppointment(int id, UpdateAppointmentRequest appointmentRequest)
    {
        throw new NotImplementedException();
    }

    public override Task<ActionResult> DeleteAppointment(int id)
    {
        throw new NotImplementedException();
    }
}