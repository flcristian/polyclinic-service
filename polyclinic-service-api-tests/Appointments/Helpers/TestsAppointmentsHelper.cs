using polyclinic_service.Appointments.DTOs;
using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service.Appointments.Models;
using polyclinic_service.UserAppointments.Models;

namespace polyclinic_service_api_tests.Appointments.Helpers
{
    public static class TestsAppointmentsHelper
    {
        public static List<Appointment> CreateTestAppointments(int count)
        {
            List<Appointment> appointments = new List<Appointment>();

            for (int i = 1; i <= count; i++)
            {
                appointments.Add(CreateTestAppointment(i));
            }

            return appointments;
        }

        public static Appointment CreateTestAppointment(int id)
        {
            return new Appointment
            {
                Id = id,
                StartDate = DateTime.UtcNow.AddDays(10),
                EndDate = DateTime.UtcNow.AddDays(10).AddHours(1),
                UserAppointments = new List<UserAppointment>()
            };
        }

        public static CreateAppointmentRequest CreateTestCreateAppointmentRequest(int id)
        {
            return new CreateAppointmentRequest
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddHours(1),
                PatientId = 1,
                DoctorId = 2
            };
        }

        public static UpdateAppointmentRequest CreateTestUpdateAppointmentRequest(int id)
        {
            return new UpdateAppointmentRequest
            {
                Id = id,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddHours(1)
            };
        }

        public static GetAppointmentRequest CreateTestGetAppointmentRequest(int id)
        {
            return new GetAppointmentRequest
            {
                Id = id,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddHours(1),
                UserAppointments = new List<GetUserAppointmentRequest>()
            };
        }
        
        public static List<FreeTimeSlotResponse> CreateTestFreeTimeSlots(int count)
        {
            var freeSlots = new List<FreeTimeSlotResponse>();

            for (int i = 0; i < count; i++)
            {
                freeSlots.Add(new FreeTimeSlotResponse
                {
                    StartDate = DateTime.UtcNow.AddHours(i * 2),
                    EndDate = DateTime.UtcNow.AddHours(i * 2 + 1)
                });
            }

            return freeSlots;
        }

        public static DateResponse CreateTestDateResponse()
        {
            return new DateResponse
            {
                Day = DateTime.UtcNow.Day,
                Month = DateTime.UtcNow.Month,
                Year = DateTime.UtcNow.Year,
                Count = 5 
            };
        }
    }
}