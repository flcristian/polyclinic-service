using Microsoft.AspNetCore.Mvc;
using polyclinic_service.Schedules.DTOs;
using polyclinic_service.Schedules.Models;

namespace polyclinic_service.Schedules.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class SchedulesApiController : ControllerBase
{
    [HttpGet("all")]
    [ProducesResponseType(statusCode:200,type:typeof(IEnumerable<Schedule>))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<Schedule>>> GetAllSchedules();
    
    [HttpGet("schedule/{id}")]
    [ProducesResponseType(statusCode:200,type:typeof(Schedule))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Schedule>> GetScheduleByDoctorId([FromRoute]int doctorId);
    
    [HttpPost("create")]
    [ProducesResponseType(statusCode:201,type:typeof(Schedule))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Schedule>> CreateSchedule([FromBody]CreateScheduleRequest scheduleRequest);
    
    [HttpPut("update")]
    [ProducesResponseType(statusCode:202,type:typeof(Schedule))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Schedule>> UpdateSchedule([FromBody]UpdateScheduleRequest scheduleRequest);
    
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(statusCode:202,type:typeof(String))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult> DeleteSchedule([FromRoute]int doctorId);
}