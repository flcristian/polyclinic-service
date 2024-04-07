using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service_api_tests.Appointments.Helpers;
using polyclinic_service_api_tests.Users.Helpers;

namespace polyclinic_service_api_tests.UserAppointments.Helpers
{
    public static class TestsUserAppointmentsHelpers
    {
        public static CreateUserAppointmentRequest CreateUserAppointmentRequest(int userId, int appointmentId)
        {
            return new CreateUserAppointmentRequest
            {
                UserId = userId,
                AppointmentId = appointmentId
            };
        }

        public static GetUserAppointmentRequest GetUserAppointmentRequest(int id)
        {
            var appointment = TestsAppointmentsHelper.CreateTestAppointment(id);
            var user = TestsUsersHelper.CreateTestDoctor(id);
            
            return new GetUserAppointmentRequest
            {
                Id = id,
                User = user,
                Appointment = appointment
            };
        }

        public static UpdateUserAppointmentRequest UpdateUserAppointmentRequest(int id, int userId, int appointmentId)
        {
            return new UpdateUserAppointmentRequest
            {
                Id = id,
                UserId = userId,
                AppointmentId = appointmentId
            };
        }
        
        public static List<UserAppointment> CreateUserAppointments(int count)
        {
            List<UserAppointment> list = new List<UserAppointment>();

            for (int i = 1; i <= count; i++)
            {
                var appointment = TestsAppointmentsHelper.CreateTestAppointment(i);
                var user = TestsUsersHelper.CreateTestDoctor(i);

                list.Add(new UserAppointment
                {
                    Id = i,
                    User = user,
                    Appointment = appointment
                });
            }

            return list;
        }

        public static UserAppointment CreateUserAppointment(int id)
        {
            var appointment = TestsAppointmentsHelper.CreateTestAppointment(id);
            var user = TestsUsersHelper.CreateTestDoctor(id);
            
            return new UserAppointment
            {
                Id = id,
                User = user,
                Appointment = appointment
            };
        }
    }
}