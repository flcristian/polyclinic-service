using System.Data;
using FluentMigrator;
using Microsoft.EntityFrameworkCore;

namespace polyclinic_service.Data.Migrations;

[Migration(108112023)]
public class AddOnDeleteCascadeToTables : Migration
{
    public override void Up()
    {
        Delete.ForeignKey("FK_UserAppointments_Appointment").OnTable("UserAppointments");
        Delete.ForeignKey("FK_UserAppointments_Doctor").OnTable("UserAppointments");
        Delete.ForeignKey("FK_UserAppointments_Patient").OnTable("UserAppointments");
        
        Create.ForeignKey("FK_UserAppointments_Patient")
            .FromTable("UserAppointments")
            .ForeignColumn("PatientId")
            .ToTable("Users")
            .PrimaryColumn("Id")
            .OnDelete(Rule.Cascade);

        Create.ForeignKey("FK_UserAppointments_Doctor")
            .FromTable("UserAppointments")
            .ForeignColumn("DoctorId")
            .ToTable("Users")
            .PrimaryColumn("Id")
            .OnDelete(Rule.Cascade);

        Create.ForeignKey("FK_UserAppointments_Appointment")
            .FromTable("UserAppointments")
            .ForeignColumn("AppointmentId")
            .ToTable("Appointments")
            .PrimaryColumn("Id")
            .OnDelete(Rule.Cascade);
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_UserAppointments_Appointment").OnTable("UserAppointments");
        Delete.ForeignKey("FK_UserAppointments_Doctor").OnTable("UserAppointments");
        Delete.ForeignKey("FK_UserAppointments_Patient").OnTable("UserAppointments");
    }
}