using Microsoft.AspNetCore.Mvc;
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
    
    [HttpGet("userAppointment/{id}")]
    [ProducesResponseType(statusCode:200,type:typeof(UserAppointment))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<UserAppointment>> GetUserAppointmentById([FromRoute]int id);
}