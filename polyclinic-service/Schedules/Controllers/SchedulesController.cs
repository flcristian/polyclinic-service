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
    
    public override async Task<ActionResult<IEnumerable<Schedule>>> GetAllSchedules()
    {
        _logger.LogInformation("Rest request: Get all schedules.");
        try
        {
            IEnumerable<Schedule> result = await _queryService.GetAllSchedules();
            
            return Ok(result);
        }
        catch (ItemsDoNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<Schedule>> GetScheduleByDoctorId(int doctorId)
    {
        _logger.LogInformation($"Rest request: Get schedule with doctor id {doctorId}.");
        try
        {
            Schedule result = await _queryService.GetScheduleByDoctorId(doctorId);

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

    public override async Task<ActionResult> DeleteSchedule(int doctorId)
    {
        _logger.LogInformation($"Rest request: Delete schedule with doctor id {doctorId}");
        try
        {
            await _commandService.DeleteSchedule(doctorId);

            return Accepted(Constants.SCHEDULE_DELETED, Constants.SCHEDULE_DELETED);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
}