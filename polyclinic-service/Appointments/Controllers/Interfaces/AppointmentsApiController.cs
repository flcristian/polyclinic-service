using Microsoft.AspNetCore.Mvc;
using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;

namespace polyclinic_service.Appointments.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class AppointmentsApiController : ControllerBase
{
    [HttpGet("all")]
    [ProducesResponseType(statusCode:200,type:typeof(IEnumerable<Appointment>))]
    [ProducesResponseType(statusCode:400,type:typeof(String))]
    public abstract Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments();
    
    [HttpGet("appointment/{id}")]
    [ProducesResponseType(statusCode:200,type:typeof(Appointment))]
    [ProducesResponseType(statusCode:400,type:typeof(String))]
    public abstract Task<ActionResult<Appointment>> GetAppointmentById([FromRoute]int id);
    
    [HttpPost("create")]
    [ProducesResponseType(statusCode:200,type:typeof(Appointment))]
    [ProducesResponseType(statusCode:400,type:typeof(String))]
    public abstract Task<ActionResult<Appointment>> CreateAppointment([FromBody]CreateAppointmentRequest appointmentRequest);
    
    [HttpPut("update")]
    [ProducesResponseType(statusCode:200,type:typeof(Appointment))]
    [ProducesResponseType(statusCode:400,type:typeof(String))]
    public abstract Task<ActionResult<Appointment>> UpdateAppointment([FromQuery]int id, [FromBody]UpdateAppointmentRequest appointmentRequest);
    
    [HttpDelete("delete")]
    [ProducesResponseType(statusCode:200,type:typeof(String))]
    [ProducesResponseType(statusCode:400,type:typeof(String))]
    public abstract Task<ActionResult> DeleteAppointment([FromQuery]int id);
}