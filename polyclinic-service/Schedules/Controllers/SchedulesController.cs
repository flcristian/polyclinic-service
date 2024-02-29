using System.Collections;
using Microsoft.AspNetCore.Mvc;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.Schedules.Controllers.Interfaces;
using polyclinic_service.Schedules.DTOs;
using polyclinic_service.Schedules.Models;
using polyclinic_service.Schedules.Services.Interfaces;

namespace polyclinic_service.Schedules.Controllers;

public class SchedulesController : SchedulesApiController
{
    private IScheduleQueryService _queryService;
    private IScheduleCommandService _commandService;

    private ILogger<SchedulesController> _logger;
    
    public SchedulesController(IScheduleQueryService queryService, IScheduleCommandService commandService, ILogger<SchedulesController> logger)
    {
        _queryService = queryService;
        _commandService = commandService;
        _logger = logger;
    }
    
    public override async Task<ActionResult<IEnumerable<GetScheduleRequest>>> GetAllSchedules()
    {
        _logger.LogInformation("Rest request: Get all schedules.");
        try
        {
            IEnumerable<GetScheduleRequest> result = await _queryService.GetAllSchedules();
            
            return Ok(result);
        }
        catch (ItemsDoNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<IEnumerable<GetScheduleRequest>>> GetSchedulesByDoctorId(int doctorId)
    {
        _logger.LogInformation($"Rest request: Get schedules with doctor id {doctorId}.");
        try
        {
            IEnumerable<GetScheduleRequest> result = await _queryService.GetSchedulesByDoctorId(doctorId);

            return Ok(result);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<GetScheduleRequest>> GetScheduleByDoctorIdAndWeekIdentity(int doctorId, int year, int weekNumber)
    {
        _logger.LogInformation($"Rest request: Get schedule with doctor id and week identity {doctorId} - {year} : Week {weekNumber}.");
        try
        {
            GetScheduleRequest result = await _queryService.GetScheduleByDoctorIdAndWeekIdentity(new GetByDoctorIdAndWeekIdentityRequest
            {
                DoctorId = doctorId,
                Year = year,
                WeekNumber = weekNumber
            });

            return Ok(result);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
    
    public override async Task<ActionResult<Schedule>> CreateSchedule(CreateScheduleRequest scheduleRequest)
    {
        _logger.LogInformation($"Rest request: Create schedule with DTO:\n{scheduleRequest}");
        Schedule response = await _commandService.CreateSchedule(scheduleRequest);

        return Created(Constants.SCHEDULE_CREATED, response);
    }

    public override async Task<ActionResult<Schedule>> UpdateSchedule(UpdateScheduleRequest scheduleRequest)
    {
        _logger.LogInformation($"Rest request: Create schedule with DTO:\n{scheduleRequest}");
        try
        {
            Schedule response = await _commandService.UpdateSchedule(scheduleRequest);

            return Accepted(Constants.SCHEDULE_UPDATED, response);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult> DeleteSchedule(DeleteScheduleRequest scheduleRequest)
    {
        _logger.LogInformation($"Rest request: Delete schedule by DTO {scheduleRequest}");
        try
        {
            await _commandService.DeleteSchedule(scheduleRequest);

            return Accepted(Constants.SCHEDULE_DELETED, Constants.SCHEDULE_DELETED);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
}