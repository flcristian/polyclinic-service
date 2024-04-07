using System.Collections;
using Microsoft.AspNetCore.Mvc;
using polyclinic_service.Schedules.DTOs;
using polyclinic_service.Schedules.Models;

namespace polyclinic_service.Schedules.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class SchedulesApiController : ControllerBase
{
    [HttpGet("all")]
    [ProducesResponseType(statusCode:200,type:typeof(IEnumerable<GetScheduleRequest>))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<GetScheduleRequest>>> GetAllSchedules();
    
    [HttpGet("schedules/{doctorId}")]
    [ProducesResponseType(statusCode:200,type:typeof(IEnumerable<GetScheduleRequest>))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<IEnumerable<GetScheduleRequest>>> GetSchedulesByDoctorId([FromRoute]int doctorId);

    [HttpGet("schedule/{doctorId}")]
    [ProducesResponseType(statusCode: 200, type: typeof(GetScheduleRequest))]
    [ProducesResponseType(statusCode: 404, type: typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<GetScheduleRequest>> GetScheduleByDoctorIdAndWeekIdentity([FromRoute]int doctorId, [FromQuery]int year, [FromQuery]int weekNumber);
    
    [HttpPost("create")]
    [ProducesResponseType(statusCode:201,type:typeof(Schedule))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Schedule>> CreateSchedule([FromBody]CreateScheduleRequest scheduleRequest);
    
    [HttpPut("update")]
    [ProducesResponseType(statusCode:202,type:typeof(Schedule))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Schedule>> UpdateSchedule([FromBody]UpdateScheduleRequest scheduleRequest);
    
    [HttpPut("delete")]
    [ProducesResponseType(statusCode:202,type:typeof(String))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Schedule>> DeleteSchedule(DeleteScheduleRequest scheduleRequest);
}