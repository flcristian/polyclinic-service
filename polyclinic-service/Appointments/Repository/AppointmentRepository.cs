﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Data;
using polyclinic_service.Appointments.Models;
using polyclinic_service.Appointments.Repository.Interfaces;

namespace polyclinic_service.Appointments.Repository;

public class AppointmentRepository : IAppointmentRepository
{
    private AppDbContext _context;
    private IMapper _mapper;

    public AppointmentRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return await _context.Appointments
            .Include(appointment => appointment.UserAppointments)
                .ThenInclude(userAppointment => userAppointment.User)
            .ToListAsync();
    }

    public async Task<Appointment> GetByIdAsync(int id)
    {
        return (await _context.Appointments.FirstOrDefaultAsync(appointment => appointment.Id == id))!;
    }

    public async Task<Appointment> CreateAsync(CreateAppointmentRequest appointmentRequest)
    {
        Appointment appointment = _mapper.Map<Appointment>(appointmentRequest);
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<Appointment> UpdateAsync(UpdateAppointmentRequest appointmentRequest)
    {
        Appointment appointment = (await _context.Appointments.FindAsync(appointmentRequest.Id))!;

        appointment.StartDate = appointmentRequest.StartDate;
        appointment.EndDate = appointmentRequest.EndDate;

        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<IEnumerable<FreeTimeSlotResponse>> GetFreeSlotsAsync(int userId, DateTime startDate, DateTime endDate)
    {
        var appointments = await _context.Appointments
            .Where(appointment =>
                _context.UserAppointments.Any(userAppointment => userAppointment.UserId == userId && userAppointment.AppointmentId == appointment.Id)
                &&
                (appointment.StartDate >= startDate && appointment.StartDate <= endDate ||
                 appointment.EndDate >= startDate && appointment.EndDate <= endDate))
            .OrderBy(appointment => appointment.StartDate)
            .ToListAsync();

        var freeSlots = new List<FreeTimeSlotResponse>();
        
        DateTime lastEndTime = startDate;
        appointments.ForEach(appointment =>
        {
            freeSlots.Add(new FreeTimeSlotResponse { StartDate = lastEndTime, EndDate = appointment.StartDate });
            lastEndTime = appointment.EndDate;
        });
        freeSlots.Add(new FreeTimeSlotResponse { StartDate = lastEndTime, EndDate = endDate });
        
        return freeSlots;
    }

    public async Task DeleteAsync(int id)
    {
        Appointment appointment = (await _context.Appointments.FindAsync(id))!;
        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
    }
}