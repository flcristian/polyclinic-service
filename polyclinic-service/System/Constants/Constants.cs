﻿namespace polyclinic_service.System.Constants;

public static class Constants
{
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
    
    // USERS
    public const string USER_CREATED = "User successfully created.";
    public const string USER_UPDATED = "User successfully updated.";
    public const string USER_DELETED = "User successfully deleted.";
    public const string USER_DOES_NOT_EXIST = "This user does not exist.";
    public const string USERS_DO_NOT_EXIST = "There are no users.";
    public const string USER_NOT_DOCTOR_OR_PATIENT = "This user isn't a doctor nor a pacient.";
    public const string NO_DOCTORS_HAVE_APPOINTMENTS = "There are no doctors with appointments.";
    public const string NO_PATIENTS_HAVE_APPOINTMENTS = "There are no patients with appointments.";
    
    // TIME SLOTS
    public const string NO_FREE_TIME_SLOTS = "There are no free time slots.";
    public const string NO_OCCUPIED_TIME_SLOTS = "There are no occupied time slots.";
}