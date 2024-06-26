﻿using Microsoft.AspNetCore.Mvc;
using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.UserAppointments.Controllers.Interfaces;
using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service.UserAppointments.Services.Interfaces;
using polyclinic_service.Users.Services.Interfaces;

namespace polyclinic_service.UserAppointments.Controllers;

public class UserAppointmentsController : UserAppointmentsApiController
{
    private IUserAppointmentQueryService _queryService;
    private IUserAppointmentCommandService _commandService;

    private ILogger<UserAppointmentsController> _logger;

    public UserAppointmentsController(IUserAppointmentQueryService queryService,
        ILogger<UserAppointmentsController> logger, IUserAppointmentCommandService commandService)
    {
        _queryService = queryService;
        _commandService = commandService;
        _logger = logger;
    }

    public override async Task<ActionResult<IEnumerable<GetUserAppointmentRequest>>> GetAllUserAppointments()
    {
        _logger.LogInformation("Rest request: Get all user appointments.");
        try
        {
            List<UserAppointment> userAppointments = (await _queryService.GetAllUserAppointments()).ToList();
            List<GetUserAppointmentRequest> result = new List<GetUserAppointmentRequest>();
            
            userAppointments.ForEach(userAppointment =>
            {
                _logger.LogInformation(userAppointment.Id.ToString());
                result.Add(new GetUserAppointmentRequest
                {
                    Id = userAppointment.Id,
                    User = userAppointment.User,
                    Appointment = userAppointment.Appointment
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

    public override async Task<ActionResult<GetUserAppointmentRequest>> GetUserAppointmentById(int id)
    {
        _logger.LogInformation($"Rest request: Get user appointment with id {id}.");
        try
        {
            UserAppointment userAppointment = await _queryService.GetUserAppointmentById(id);

            GetUserAppointmentRequest result = new GetUserAppointmentRequest
            {
                Id = userAppointment.Id,
                User = userAppointment.User,
                Appointment = userAppointment.Appointment
            };

            return Ok(result);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
    
    public override async Task<ActionResult<GetUserAppointmentRequest>> CreateUserAppointment(CreateUserAppointmentRequest userAppointmentRequest)
    {
        _logger.LogInformation($"Rest request: Create userAppointment with DTO:\n{userAppointmentRequest}");
        UserAppointment userAppointment = await _commandService.CreateUserAppointment(userAppointmentRequest);
        
        GetUserAppointmentRequest response = new GetUserAppointmentRequest
        {
            Id = userAppointment.Id,
            User = userAppointment.User,
            Appointment = userAppointment.Appointment
        };

        return Created(Constants.USER_APPOINTMENT_CREATED, response);
    }

    public override async Task<ActionResult<GetUserAppointmentRequest>> UpdateUserAppointment(UpdateUserAppointmentRequest userAppointmentRequest)
    {
        _logger.LogInformation($"Rest request: Create userAppointment with DTO:\n{userAppointmentRequest}");
        try
        {
            UserAppointment userAppointment = await _commandService.UpdateUserAppointment(userAppointmentRequest);
            
            GetUserAppointmentRequest response = new GetUserAppointmentRequest
            {
                Id = userAppointment.Id,
                User = userAppointment.User,
                Appointment = userAppointment.Appointment
            };

            return Accepted(Constants.USER_APPOINTMENT_UPDATED, response);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<GetUserAppointmentRequest>> DeleteUserAppointment(int id)
    {
        _logger.LogInformation($"Rest request: Delete userAppointment with id {id}");
        try
        {
            UserAppointment userAppointment = await _commandService.DeleteUserAppointment(id);
            
            GetUserAppointmentRequest response = new GetUserAppointmentRequest
            {
                Id = userAppointment.Id,
                User = userAppointment.User,
                Appointment = userAppointment.Appointment
            };

            return Accepted(Constants.USER_APPOINTMENT_DELETED, response);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
    
    public override async Task<ActionResult<IEnumerable<GetAppointmentRequest>>> GetAppointmentsByUserId(int userId)
    {
        _logger.LogInformation($"Rest request: Get user appointments of the user with id {userId}.");
        try
        {
            List<Appointment> appointments = (await _queryService.GetAppointmentsByUserId(userId)).ToList();

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
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
    
    public override async Task<ActionResult<IEnumerable<GetAppointmentRequest>>> GetAppointmentHistoryOfUserByUserId(int userId)
    {
        _logger.LogInformation($"Rest request: Get appointment history of the user with id {userId}.");
        try
        {
            List<Appointment> appointments = (await _queryService.GetAppointmentHistoryByUserId(userId)).ToList();

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
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
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