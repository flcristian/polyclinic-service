using Microsoft.AspNetCore.Mvc;
using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;
using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service.UserAppointments.Models;

namespace polyclinic_service.UserAppointments.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class UserAppointmentsApiController : ControllerBase
{
    [HttpGet("all")]
    [ProducesResponseType(statusCode:200,type:typeof(IEnumerable<GetUserAppointmentRequest>))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<GetUserAppointmentRequest>>> GetAllUserAppointments();
    
    [HttpGet("user_appointment/{id}")]
    [ProducesResponseType(statusCode:200,type:typeof(GetUserAppointmentRequest))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<GetUserAppointmentRequest>> GetUserAppointmentById([FromRoute]int id);
    
    [HttpPost("create")]
    [ProducesResponseType(statusCode:201,type:typeof(GetUserAppointmentRequest))]
    [Produces("application/json")]
    public abstract Task<ActionResult<GetUserAppointmentRequest>> CreateUserAppointment([FromBody]CreateUserAppointmentRequest userAppointmentRequest);
    
    [HttpPut("update")]
    [ProducesResponseType(statusCode:202,type:typeof(GetUserAppointmentRequest))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<GetUserAppointmentRequest>> UpdateUserAppointment([FromBody]UpdateUserAppointmentRequest userAppointmentRequest);
    
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(statusCode:202,type:typeof(String))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<GetUserAppointmentRequest>> DeleteUserAppointment([FromRoute]int id);

    [HttpGet("appointments_of_user/{userId}")]
    [ProducesResponseType(statusCode:200,type:typeof(IEnumerable<Appointment>))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<GetAppointmentRequest>>> GetAppointmentsByUserId(int userId);
    
    [HttpGet("appointment_history_of_user/{userId}")]
    [ProducesResponseType(statusCode:200,type:typeof(IEnumerable<Appointment>))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<GetAppointmentRequest>>> GetAppointmentHistoryOfUserByUserId(int userId);
}