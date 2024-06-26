﻿using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using polyclinic_service.Appointments.Controllers.Interfaces;
using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;
using polyclinic_service.Appointments.Services.Interfaces;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service.UserAppointments.Services.Interfaces;

namespace polyclinic_service.Appointments.Controllers;

public class AppointmentsController : AppointmentsApiController
{ 
    private IAppointmentQueryService _appointmentQueryService;
    private IAppointmentCommandService _appointmentCommandService;
    private IUserAppointmentCommandService _userAppointmentCommandService;

    private ILogger<AppointmentsController> _logger;
    
    public AppointmentsController(IAppointmentQueryService appointmentQueryService, IAppointmentCommandService appointmentCommandService, IUserAppointmentCommandService userAppointmentCommandService, ILogger<AppointmentsController> logger)
    {
        _appointmentQueryService = appointmentQueryService;
        _appointmentCommandService = appointmentCommandService;
        _userAppointmentCommandService = userAppointmentCommandService;
        _logger = logger;
    }
    
    public override async Task<ActionResult<IEnumerable<GetAppointmentRequest>>> GetAllAppointments()
    {
        _logger.LogInformation("Rest request: Get all appointments.");
        try
        {
            List<Appointment> appointments = (await _appointmentQueryService.GetAllAppointments()).ToList();
            List<GetAppointmentRequest> result = new List<GetAppointmentRequest>();
            
            appointments.ForEach(appointment =>
            {
                result.Add(new GetAppointmentRequest
                {
                    Id = appointment.Id,
                    StartDate = appointment.StartDate,
                    EndDate = appointment.EndDate,
                    UserAppointments = ConvertUserAppointmentsToDTO(appointment.UserAppointments)
                });
            });
            
            return Ok(result);
        }
        catch (ItemsDoNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
    
    public override async Task<ActionResult<GetAppointmentRequest>> GetAppointmentById(int id)
    {
        _logger.LogInformation($"Rest request: Get appointment with id {id}.");
        try
        {
            Appointment appointment = await _appointmentQueryService.GetAppointmentById(id);
            
            GetAppointmentRequest result = new GetAppointmentRequest
            {
                Id = appointment.Id,
                StartDate = appointment.StartDate,
                EndDate = appointment.EndDate,
                UserAppointments = ConvertUserAppointmentsToDTO(appointment.UserAppointments)
            };

            return Ok(result);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<GetAppointmentRequest>> CreateAppointment(CreateAppointmentRequest appointmentRequest)
    {
        _logger.LogInformation($"Rest request: Create appointment with DTO:\n{appointmentRequest}");
        
        Appointment appointment = await _appointmentCommandService.CreateAppointment(appointmentRequest);

        List<CreateUserAppointmentRequest> userRequests = new List<CreateUserAppointmentRequest>
        {
            new()
            {
                UserId = appointmentRequest.PatientId,
                AppointmentId = appointment.Id
            },
            new()
            {
                UserId = appointmentRequest.DoctorId,
                AppointmentId = appointment.Id
            }
        };

        List<UserAppointment> userAppointments = new List<UserAppointment>();
        userAppointments.Add(await _userAppointmentCommandService.CreateUserAppointment(userRequests[0]));
        userAppointments.Add(await _userAppointmentCommandService.CreateUserAppointment(userRequests[1]));
        
        GetAppointmentRequest response = new GetAppointmentRequest
        {
            Id = appointment.Id,
            StartDate = appointment.StartDate,
            EndDate = appointment.EndDate,
            UserAppointments = ConvertUserAppointmentsToDTO(userAppointments)
        };

        return Created(Constants.APPOINTMENT_CREATED, response);
    }

    public override async Task<ActionResult<GetAppointmentRequest>> UpdateAppointment(UpdateAppointmentRequest appointmentRequest)
    {
        _logger.LogInformation($"Rest request: Create appointment with DTO:\n{appointmentRequest}");
        try
        {
            Appointment appointment = await _appointmentCommandService.UpdateAppointment(appointmentRequest);

            GetAppointmentRequest response = new GetAppointmentRequest
            {
                Id = appointment.Id,
                StartDate = appointment.StartDate,
                EndDate = appointment.EndDate,
                UserAppointments = ConvertUserAppointmentsToDTO(appointment.UserAppointments)
            };

            return Accepted(Constants.APPOINTMENT_UPDATED, response);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<GetAppointmentRequest>> DeleteAppointment(int id)
    {
        _logger.LogInformation($"Rest request: Delete appointment with id {id}");
        try
        {
            Appointment appointment = await _appointmentCommandService.DeleteAppointment(id);

            GetAppointmentRequest response = new GetAppointmentRequest
            {
                Id = appointment.Id,
                StartDate = appointment.StartDate,
                EndDate = appointment.EndDate,
                UserAppointments = ConvertUserAppointmentsToDTO(appointment.UserAppointments)
            };
            
            return Accepted(Constants.APPOINTMENT_DELETED, response);
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
                await _appointmentQueryService.GetFreeSlotsForInterval(userId, startDay, endDay);
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
                await _appointmentQueryService.GetFreeSlotsForInterval(userId, startWeek, endWeek);
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
                await _appointmentQueryService.GetFreeSlotsForInterval(userId, startMonth, endMonth);
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
                await _appointmentQueryService.GetFreeSlotsForInterval(userId, startDate, endDate);

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
                await _appointmentQueryService.DayWithMostAppointmentsInInterval(startMonth, endMonth);

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
                await _appointmentQueryService.DayWithMostAppointmentsInInterval(startWeek, endWeek);

            return Ok(response);
        }
        catch (ItemsDoNotExist ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    // Private methods

    private List<GetUserAppointmentRequest> ConvertUserAppointmentsToDTO(List<UserAppointment> userAppointments)
    {
        List<GetUserAppointmentRequest> result = new List<GetUserAppointmentRequest>();
        
        userAppointments.ForEach(ua =>
        {
            result.Add(new GetUserAppointmentRequest
            {
                Id = ua.Id,
                User = ua.User,
                Appointment = null
            });
        });

        return result;
    }
}