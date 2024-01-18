namespace polyclinic_service.System.Constants;

public static class Constants
{
    // EMAIL SENDER 
    public const string EMAIL_SMTP_SERVER = "smtp-mail.outlook.com";
    public const int EMAIL_SMTP_PORT = 587;
    public const string EMAIL_SENDER_ADDRESS = "pspsmtp@outlook.com";
    public const string EMAIL_SENDER_PASSWORD = "pa-ro/la";
    
    // EMAIL CONTROLLER
    public const string EMAIL_SENT = "Email sent successfully.";
    
    // DATE FORMATS
    public const string SQL_DATE_FORMAT = "yyyy-MM-dd HH:mm";
    public const string STANDARD_DATE_FORMAT = "dd.MM.yyyy HH:mm";
    public const string STANDARD_DATE_HOUR_AND_MINUTE_ONLY = "HH:mm";
    public const string STANDARD_DATE_CALENDAR_DATE_ONLY = "dd.MM.yyyy";
    
    // APPOINTMENTS
    public const string APPOINTMENT_CREATED = "Appointment successfully created.";
    public const string APPOINTMENT_UPDATED = "Appointment successfully updated.";
    public const string APPOINTMENT_DELETED = "Appointment successfully deleted.";
    public const string APPOINTMENT_DOES_NOT_EXIST = "This appointment does not exist.";
    public const string APPOINTMENTS_DO_NOT_EXIST = "There are no appointments.";
    
    // USER APPOINTMENTS
    public const string USER_APPOINTMENT_DOES_NOT_EXIST = "This user appointment does not exist.";
    public const string USER_APPOINTMENTS_DO_NOT_EXIST = "There are no user appointments.";
    public const string USER_HAS_NO_APPOINTMENTS = "This user has no appointments.";
    public const string USER_APPOINTMENT_CREATED = "User appointment successfully created.";
    public const string USER_APPOINTMENT_UPDATED = "User appointment successfully updated.";
    public const string USER_APPOINTMENT_DELETED = "User appointment successfully deleted.";
    
    // USERS
    public const string USER_CREATED = "User successfully created.";
    public const string USER_UPDATED = "User successfully updated.";
    public const string USER_DELETED = "User successfully deleted.";
    public const string USER_DOES_NOT_EXIST = "This user does not exist.";
    public const string USERS_DO_NOT_EXIST = "There are no users.";
    public const string USER_NOT_DOCTOR_OR_PATIENT = "This user isn't a doctor nor a pacient.";
    public const string NO_DOCTORS_HAVE_APPOINTMENTS = "There are no doctors with appointments.";
    public const string NO_PATIENTS_HAVE_APPOINTMENTS = "There are no patients with appointments.";
    public const string USER_NOT_DOCTOR = "This user is not a doctor.";
    
    // TIME SLOTS
    public const string NO_FREE_TIME_SLOTS = "There are no free time slots.";
    public const string NO_OCCUPIED_TIME_SLOTS = "There are no occupied time slots.";
    public const string NO_APPOINTMENTS_IN_INTERVAL = "There are no appointments in that interval.";
    public const string NO_APPOINTMENTS_IN_MONTH = "There are no appointments in that month.";
    public const string NO_APPOINTMENTS_IN_WEEK = "There are no appointments in that week.";
    
    // SCHEDULES
    public const string SCHEDULE_CREATED = "Schedule successfully created.";
    public const string SCHEDULE_UPDATED = "Schedule successfully updated.";
    public const string SCHEDULE_DELETED = "Schedule successfully deleted.";
    public const string SCHEDULE_DOES_NOT_EXIST = "This schedule does not exist";
    public const string SCHEDULES_DO_NOT_EXIST = "There are no schedules.";
    
    // USER ACTIONS
    public const string APPOINTMENT_SCHEDULED = "Appointment successfully scheduled. Emails with details were sent!";
    public const string APPOINTMENT_NOT_IN_SCHEDULE = "The doctor is not available at that time.";
}