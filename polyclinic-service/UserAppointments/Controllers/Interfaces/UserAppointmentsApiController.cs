using Microsoft.AspNetCore.Mvc;
using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service.UserAppointments.Models;

namespace polyclinic_service.UserAppointments.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class UserAppointmentsApiController : ControllerBase
{
    [HttpGet("all")]
    [ProducesResponseType(statusCode:200,type:typeof(IEnumerable<UserAppointment>))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<UserAppointment>>> GetAllUserAppointments();
    
    [HttpGet("user_appointment/{id}")]
    [ProducesResponseType(statusCode:200,type:typeof(UserAppointment))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<UserAppointment>> GetUserAppointmentById([FromRoute]int id);
    
    [HttpPost("create")]
    [ProducesResponseType(statusCode:201,type:typeof(UserAppointment))]
    [Produces("application/json")]
    public abstract Task<ActionResult<UserAppointment>> CreateUserAppointment([FromBody]CreateUserAppointmentRequest userAppointmentRequest);
    
    [HttpPut("update")]
    [ProducesResponseType(statusCode:202,type:typeof(UserAppointment))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<UserAppointment>> UpdateUserAppointment([FromBody]UpdateUserAppointmentRequest userAppointmentRequest);
    
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(statusCode:202,type:typeof(String))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult> DeleteUserAppointment([FromRoute]int id);

    [HttpGet("appointments_of_user/{userId}")]
    [ProducesResponseType(statusCode:200,type:typeof(UserAppointment))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<UserAppointment>> GetAppointmentsByUserId(int userId);
    
    [HttpGet("appointment_history_of_user/{userId}")]
    [ProducesResponseType(statusCode:200,type:typeof(UserAppointment))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<UserAppointment>> GetAppointmentHistoryOfUserByUserId(int userId);
}