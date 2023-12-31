﻿using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;

namespace polyclinic_service.Appointments.Services.Interfaces;

public interface IAppointmentQueryService
{
    Task<IEnumerable<Appointment>> GetAllAppointments();
    Task<Appointment> GetAppointmentById(int id);
    Task<IEnumerable<FreeTimeSlotResponse>> GetFreeSlotsForInterval(int userId, DateTime startDate, DateTime endDate);
    Task<DateResponse> DayWithMostAppointmentsInInterval(DateTime startDate, DateTime endDate);
}