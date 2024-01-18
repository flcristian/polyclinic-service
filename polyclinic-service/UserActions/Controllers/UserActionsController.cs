using Microsoft.AspNetCore.Mvc;
using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;
using polyclinic_service.Appointments.Services.Interfaces;
using polyclinic_service.Emails.DTOs;
using polyclinic_service.Emails.Services.Interfaces;
using polyclinic_service.Schedules.Services.Interfaces;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.UserActions.Controllers.Interfaces;
using polyclinic_service.UserActions.DTOs;
using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service.UserAppointments.Services.Interfaces;
using polyclinic_service.Users.Models;
using polyclinic_service.Users.Services.Interfaces;

namespace polyclinic_service.UserActions.Controllers;

public class UserActionsController : UserActionsApiController
{
    private IUserQueryService _userQueryService;
    private IUserCommandService _userCommandService;
    private IAppointmentQueryService _appointmentQueryService;
    private IAppointmentCommandService _appointmentCommandService;
    private IUserAppointmentQueryService _userAppointmentQueryService;
    private IUserAppointmentCommandService _userAppointmentCommandService;
    private IScheduleQueryService _scheduleQueryService;
    private IScheduleCommandService _scheduleCommandService;
    private IEmailSenderService _emailSenderService;

    public UserActionsController(IUserQueryService userQueryService, IUserCommandService userCommandService, IAppointmentQueryService appointmentQueryService, IAppointmentCommandService appointmentCommandService, IUserAppointmentQueryService userAppointmentQueryService, IUserAppointmentCommandService userAppointmentCommandService, IScheduleQueryService scheduleQueryService, IScheduleCommandService scheduleCommandService, IEmailSenderService emailSenderService)
    {
        _userQueryService = userQueryService;
        _userCommandService = userCommandService;
        _appointmentQueryService = appointmentQueryService;
        _appointmentCommandService = appointmentCommandService;
        _userAppointmentQueryService = userAppointmentQueryService;
        _userAppointmentCommandService = userAppointmentCommandService;
        _scheduleQueryService = scheduleQueryService;
        _scheduleCommandService = scheduleCommandService;
        _emailSenderService = emailSenderService;
    }

    // SUCCESS: Appointment successfully scheduled. Emails with details were sent!
    // EXCEPTII: 
    // Pacientul nu exista
    // Doctorul nu exista
    // Programarea nu este in programul doctorului
    public override async Task<ActionResult<string>> CreateSchedule(CreateAppointmentActionRequest request)
    {
        // Obtaining the patient.
        User patient = null!;
        try
        {
            patient = await _userQueryService.GetUserById(request.PatientId);
        }
        catch (ItemDoesNotExist ex)
        {
            return NotFound(ex.Message);
        }
        
        // Obtaining the doctor.
        User doctor = null!;
        try
        {
            doctor = await _userQueryService.GetUserById(request.DoctorId);
        }
        catch (ItemDoesNotExist ex)
        {
            return NotFound(ex.Message);
        }
        
        if (doctor.Type != UserType.Doctor) 
            return BadRequest(Constants.USER_NOT_DOCTOR);
        
        // Checking if the appointment is in the doctor's schedule.
        DateTime appointmentStartDate = request.AppointmentDate;
        DateTime appointmentEndDate = appointmentStartDate + TimeSpan.FromMinutes(request.Minutes);

        if (!await _scheduleQueryService.CheckIfAppointmentInDoctorSchedule(doctor.Id, appointmentStartDate,
                appointmentEndDate))
            return Conflict(Constants.APPOINTMENT_NOT_IN_SCHEDULE);

        Appointment appointment = await _appointmentCommandService.CreateAppointment(new CreateAppointmentRequest
        {
            StartDate = appointmentStartDate,
            EndDate = appointmentEndDate
        });

        UserAppointment patientAppointment = await _userAppointmentCommandService.CreateUserAppointment(
            new CreateUserAppointmentRequest
            {
                AppointmentId = appointment.Id,
                UserId = patient.Id
            });
        
        UserAppointment doctorAppointment = await _userAppointmentCommandService.CreateUserAppointment(
            new CreateUserAppointmentRequest
            {
                AppointmentId = appointment.Id,
                UserId = doctor.Id
            });

        await _emailSenderService.SendAppointmentDetailsAsync(new SendAppointmentDetailsRequest
        {
            AppointmentId = appointment.Id,
            UserId = patient.Id
        });
        
        await _emailSenderService.SendAppointmentDetailsAsync(new SendAppointmentDetailsRequest
        {
            AppointmentId = appointment.Id,
            UserId = doctor.Id
        });

        return Ok(Constants.APPOINTMENT_SCHEDULED);
    }
}