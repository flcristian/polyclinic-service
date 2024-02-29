using AutoMapper;
using Microsoft.EntityFrameworkCore;
using polyclinic_service.Data;
using polyclinic_service.Schedules.DTOs;
using polyclinic_service.Schedules.Models;
using polyclinic_service.Schedules.Repository.Interfaces;

namespace polyclinic_service.Schedules.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ScheduleRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Schedule>> GetAllAsync()
        {
            return await _context.Schedules
                .Include(schedule => schedule.MondaySchedule)
                .Include(schedule => schedule.TuesdaySchedule)
                .Include(schedule => schedule.WednesdaySchedule)
                .Include(schedule => schedule.ThursdaySchedule)
                .Include(schedule => schedule.FridaySchedule)
                .ToListAsync();
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesByDoctorIdAsync(int doctorId)
        {
            return await _context.Schedules
                .Include(schedule => schedule.MondaySchedule)
                .Include(schedule => schedule.TuesdaySchedule)
                .Include(schedule => schedule.WednesdaySchedule)
                .Include(schedule => schedule.ThursdaySchedule)
                .Include(schedule => schedule.FridaySchedule)
                .Where(schedule => schedule.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<Schedule> GetByDoctorIdAndWeekIdentityAsync(GetByDoctorIdAndWeekIdentityRequest scheduleRequest)
        {
            return (await _context.Schedules
                .Include(schedule => schedule.MondaySchedule)
                .Include(schedule => schedule.TuesdaySchedule)
                .Include(schedule => schedule.WednesdaySchedule)
                .Include(schedule => schedule.ThursdaySchedule)
                .Include(schedule => schedule.FridaySchedule)
                .FirstOrDefaultAsync(schedule => 
                    schedule.DoctorId == scheduleRequest.DoctorId &&
                    schedule.Year == scheduleRequest.Year &&
                    schedule.WeekNumber == scheduleRequest.WeekNumber
                    ))!;
        }

        public async Task<Schedule> CreateAsync(CreateScheduleRequest scheduleRequest)
        {
            ScheduleSlot monday = _context.ScheduleSlots.Add(_mapper.Map<ScheduleSlot>(scheduleRequest.MondaySchedule)).Entity;
            ScheduleSlot tuesday = _context.ScheduleSlots.Add(_mapper.Map<ScheduleSlot>(scheduleRequest.TuesdaySchedule)).Entity;
            ScheduleSlot wednesday = _context.ScheduleSlots.Add(_mapper.Map<ScheduleSlot>(scheduleRequest.WednesdaySchedule)).Entity;
            ScheduleSlot thursday = _context.ScheduleSlots.Add(_mapper.Map<ScheduleSlot>(scheduleRequest.ThursdaySchedule)).Entity;
            ScheduleSlot friday = _context.ScheduleSlots.Add(_mapper.Map<ScheduleSlot>(scheduleRequest.FridaySchedule)).Entity;
            await _context.SaveChangesAsync();
            
            Schedule schedule = new Schedule
            {
                DoctorId = scheduleRequest.DoctorId,
                Year = scheduleRequest.Year,
                WeekNumber = scheduleRequest.WeekNumber,
                MondayScheduleId = monday.Id,
                TuesdayScheduleId = tuesday.Id,
                WednesdayScheduleId = wednesday.Id,
                ThursdayScheduleId = thursday.Id,
                FridayScheduleId = friday.Id
            };
            
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task<Schedule> UpdateAsync(UpdateScheduleRequest scheduleRequest)
        {
            Schedule schedule = (await _context.Schedules.FirstOrDefaultAsync(schedule => 
                schedule.DoctorId == scheduleRequest.DoctorId &&
                schedule.Year == scheduleRequest.Year &&
                schedule.WeekNumber == scheduleRequest.WeekNumber
            ))!;
            
            await UpdateScheduleSlotAsync(schedule.MondayScheduleId, scheduleRequest.MondaySchedule);
            await UpdateScheduleSlotAsync(schedule.TuesdayScheduleId, scheduleRequest.TuesdaySchedule);
            await UpdateScheduleSlotAsync(schedule.WednesdayScheduleId, scheduleRequest.WednesdaySchedule);
            await UpdateScheduleSlotAsync(schedule.ThursdayScheduleId, scheduleRequest.ThursdaySchedule);
            await UpdateScheduleSlotAsync(schedule.FridayScheduleId, scheduleRequest.FridaySchedule);

            _context.Schedules.Update(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }

        private async Task<ScheduleSlot> UpdateScheduleSlotAsync(int scheduleSlotId, UpdateScheduleSlotRequest scheduleSlotRequest)
        {
            ScheduleSlot scheduleSlot = (await _context.ScheduleSlots.FindAsync(scheduleSlotId))!;
            
            scheduleSlot.StartTime = _mapper.Map<String>(scheduleSlotRequest.StartTime);
            scheduleSlot.EndTime = _mapper.Map<String>(scheduleSlotRequest.EndTime);

            _context.ScheduleSlots.Update(scheduleSlot);
            await _context.SaveChangesAsync();
            return scheduleSlot;
        }

        public async Task DeleteAsync(DeleteScheduleRequest scheduleRequest)
        {
            Schedule schedule = (await _context.Schedules.FirstOrDefaultAsync(schedule => 
                schedule.DoctorId == scheduleRequest.DoctorId &&
                schedule.Year == scheduleRequest.Year &&
                schedule.WeekNumber == scheduleRequest.WeekNumber
                ))!;
            
            _context.ScheduleSlots.Remove((await _context.ScheduleSlots.FindAsync(schedule.MondayScheduleId))!);
            _context.ScheduleSlots.Remove((await _context.ScheduleSlots.FindAsync(schedule.TuesdayScheduleId))!);
            _context.ScheduleSlots.Remove((await _context.ScheduleSlots.FindAsync(schedule.WednesdayScheduleId))!);
            _context.ScheduleSlots.Remove((await _context.ScheduleSlots.FindAsync(schedule.ThursdayScheduleId))!);
            _context.ScheduleSlots.Remove((await _context.ScheduleSlots.FindAsync(schedule.FridayScheduleId))!);
            
            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
        }
    }
}