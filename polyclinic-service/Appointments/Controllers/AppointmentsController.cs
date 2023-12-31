﻿using Microsoft.AspNetCore.Mvc;
using polyclinic_service.Appointments.Controllers.Interfaces;
using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;
using polyclinic_service.Appointments.Services.Interfaces;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;

namespace polyclinic_service.Appointments.Controllers;

public class AppointmentsController : AppointmentsApiController
{ 
    private IAppointmentQueryService _queryService;
    private IAppointmentCommandService _commandService;

    private ILogger<AppointmentsController> _logger;
    
    public AppointmentsController(IAppointmentQueryService queryService, IAppointmentCommandService commandService, ILogger<AppointmentsController> logger)
    {
        _queryService = queryService;
        _commandService = commandService;
        _logger = logger;
    }
    
    public override async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
    {
        _logger.LogInformation("Rest request: Get all appointments.");
        try
        {
            IEnumerable<Appointment> result = await _queryService.GetAllAppointments();
            
            return Ok(result);
        }
        catch (ItemsDoNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<Appointment>> GetAppointmentById(int id)
    {
        _logger.LogInformation($"Rest request: Get appointment with id {id}.");
        try
        {
            Appointment result = await _queryService.GetAppointmentById(id);

            return Ok(result);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<Appointment>> CreateAppointment(CreateAppointmentRequest appointmentRequest)
    {
        _logger.LogInformation($"Rest request: Create appointment with DTO:\n{appointmentRequest}");
        Appointment response = await _commandService.CreateAppointment(appointmentRequest);

        return Created(Constants.APPOINTMENT_CREATED, response);
    }

    public override async Task<ActionResult<Appointment>> UpdateAppointment(UpdateAppointmentRequest appointmentRequest)
    {
        _logger.LogInformation($"Rest request: Create appointment with DTO:\n{appointmentRequest}");
        try
        {
            Appointment response = await _commandService.UpdateAppointment(appointmentRequest);

            return Accepted(Constants.APPOINTMENT_UPDATED, response);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult> DeleteAppointment(int id)
    {
        _logger.LogInformation($"Rest request: Delete appointment with id {id}");
        try
        {
            await _commandService.DeleteAppointment(id);

            return Accepted(Constants.APPOINTMENT_DELETED, Constants.APPOINTMENT_DELETED);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<IEnumerable<FreeTimeSlotResponse>>> CheckAvailabilityForDay(int userId, int day, int month, int year)
    {
        DateTime startDay = new DateTime(year, month, day);
        DateTime endDay = startDay.AddDays(1);
        _logger.LogInformation($"Rest request: Get free slots for user {userId} in day {day}-{month}-{year}");
        
        try
        {
            IEnumerable<FreeTimeSlotResponse> response =
                await _queryService.GetFreeSlotsForInterval(userId, startDay, endDay);
            return Ok(response);
        }
        catch (ItemsDoNotExist ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    public override async Task<ActionResult<IEnumerable<FreeTimeSlotResponse>>> CheckAvailabilityForWeek(int userId, int weekNumber, int year)
    {
        DateTime startDay = new DateTime(year, 1, 1); // First day of the year
        DateTime startWeek = startDay.AddDays((weekNumber - 1) * 7 - (int)startDay.DayOfWeek + 1);
        DateTime endWeek = startWeek.AddDays(7);
        _logger.LogInformation($"Rest request: Get free slots for user {userId} in week {weekNumber} of year {year}");
        
        try
        {
            IEnumerable<FreeTimeSlotResponse> response =
                await _queryService.GetFreeSlotsForInterval(userId, startWeek, endWeek);
            return Ok(response);
        }
        catch (ItemsDoNotExist ex)
        {
            return NotFound(ex.Message);
        }    
    }

    public override async Task<ActionResult<IEnumerable<FreeTimeSlotResponse>>> CheckAvailabilityForMonth(int userId, int month, int year)
    {
        DateTime startMonth = new DateTime(year, month, 1);
        DateTime endMonth = startMonth.AddMonths(1);
        _logger.LogInformation($"Rest request: Get free slots for user {userId} in month {month}-{year}");
                                                                                                        
        try
        {
            IEnumerable<FreeTimeSlotResponse> response =
                await _queryService.GetFreeSlotsForInterval(userId, startMonth, endMonth);
            return Ok(response);
        }
        catch (ItemsDoNotExist ex)
        {
            return NotFound(ex.Message);
        }        
    }

    public override async Task<ActionResult<IEnumerable<FreeTimeSlotResponse>>> CheckAvailabilityForInterval(int userId, int startDateDay, int startDateMonth, int startDateYear, int endDateDay, int endDateMonth, int endDateYear)
    {
        DateTime startDate = new DateTime(startDateYear, startDateMonth, startDateDay);
        DateTime endDate = new DateTime(endDateYear, endDateMonth, endDateDay);
        _logger.LogInformation($"Rest request: Get free slots for user {userId} in interval {startDate} - {endDate}.");
        try
        {
            IEnumerable<FreeTimeSlotResponse> response =
                await _queryService.GetFreeSlotsForInterval(userId, startDate, endDate);

            return Ok(response);
        }
        catch (ItemsDoNotExist ex)
        {
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<DateResponse>> GetDayWithMostAppointmentsFromMonth(int month, int year)
    {
        DateTime startMonth = new DateTime(year, month, 1);
        DateTime endMonth = startMonth.AddMonths(1);
        _logger.LogInformation($"Rest request: Get day with most appointments in month {month}-{year}");
        try
        {
            DateResponse response =
                await _queryService.DayWithMostAppointmentsInInterval(startMonth, endMonth);

            return Ok(response);
        }
        catch (ItemsDoNotExist ex)
        {
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<DateResponse>> GetDayWithMostAppointmentsFromWeek(int weekNumber, int year)
    {
        DateTime startDay = new DateTime(year, 1, 1); // First day of the year
        DateTime startWeek = startDay.AddDays((weekNumber - 1) * 7 - (int)startDay.DayOfWeek + 1);
        DateTime endWeek = startWeek.AddDays(7);
        _logger.LogInformation($"Rest request: Get day with most appointments in week {weekNumber} of year {year}");
        try
        {
            DateResponse response =
                await _queryService.DayWithMostAppointmentsInInterval(startWeek, endWeek);

            return Ok(response);
        }
        catch (ItemsDoNotExist ex)
        {
            return NotFound(ex.Message);
        }
    }
}