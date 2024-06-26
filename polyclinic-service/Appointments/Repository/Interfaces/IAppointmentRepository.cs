﻿using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;

namespace polyclinic_service.Appointments.Repository.Interfaces;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAllAsync();
    Task<Appointment> GetByIdAsync(int id);
    Task<Appointment> CreateAsync(CreateAppointmentRequest appointmentRequest);
    Task<Appointment> UpdateAsync(UpdateAppointmentRequest appointmentRequest);
    Task<IEnumerable<FreeTimeSlotResponse>> GetFreeSlotsAsync(int userId, DateTime startDate, DateTime endDate);
    Task<IEnumerable<OccupiedTimeSlotResponse>> GetOccupiedSlotsAsync(int userId, DateTime startDate, DateTime endDate);
    Task<IEnumerable<FreeTimeSlotResponse>> GetFreeSlotsInIntervalAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<OccupiedTimeSlotResponse>> GetOccupiedSlotsInIntervalAsync(DateTime startDate, DateTime endDate);
    Task<Appointment> DeleteAsync(int id);
    Task<DateResponse> DayWithMostAppointmentsInIntervalAsync(DateTime startDate, DateTime endDate);
}