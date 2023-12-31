﻿using System.Data;
using FluentMigrator;

namespace polyclinic_service.Data.Migrations;

[Migration(103112023)]
public class InitializationCreateTables : Migration
{
    public override void Up()
    {
        CreateUsersTable();
        CreateAppointmentsTable();
        CreateUserAppointmentsTable();
        CreateIndexes();
        CreateForeignKeys();
        SeedInitialData();
    }

    private void CreateUsersTable()
    {
        Create.Table("Users")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(128).NotNullable()
            .WithColumn("Email").AsString(128).NotNullable().Unique()
            .WithColumn("Password").AsString(128).NotNullable()
            .WithColumn("Gender").AsString(16).NotNullable()
            .WithColumn("Age").AsInt32().NotNullable()
            .WithColumn("Phone").AsString(32).NotNullable()
            .WithColumn("Type").AsInt32().NotNullable();
    }

    private void CreateAppointmentsTable()
    {
        Create.Table("Appointments")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("StartDate").AsDateTime().NotNullable()
            .WithColumn("EndDate").AsDateTime().NotNullable();
    }

    private void CreateUserAppointmentsTable()
    {
        Create.Table("UserAppointments")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserId").AsInt32().NotNullable()
            .WithColumn("AppointmentId").AsInt32().NotNullable();
    }

    private void CreateIndexes()
    {
        Create.Index("IX_UserAppointments_UserId").OnTable("UserAppointments").OnColumn("UserId").Ascending().WithOptions().NonClustered();
        Create.Index("IX_UserAppointments_AppointmentId").OnTable("UserAppointments").OnColumn("AppointmentId").Ascending().WithOptions().NonClustered();
    }

    private void CreateForeignKeys()
    {
        Create.ForeignKey("FK_UserAppointments_User").FromTable("UserAppointments").ForeignColumn("UserId").ToTable("Users").PrimaryColumn("Id").OnDelete(Rule.Cascade);
        Create.ForeignKey("FK_UserAppointments_Appointment").FromTable("UserAppointments").ForeignColumn("AppointmentId").ToTable("Appointments").PrimaryColumn("Id").OnDelete(Rule.Cascade);
    }

    private void SeedInitialData()
    {
        Execute.Script(@"./Data/Scripts/start-users.sql");
        Execute.Script(@"./Data/Scripts/start-appointments.sql");
        Execute.Script(@"./Data/Scripts/start-user-appointments.sql");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_UserAppointments_Appointment").OnTable("UserAppointments");
        Delete.ForeignKey("FK_UserAppointments_User").OnTable("UserAppointments");
        Delete.Index("IX_UserAppointments_AppointmentId").OnTable("UserAppointments");
        Delete.Index("IX_UserAppointments_UserId").OnTable("UserAppointments");
        Delete.Table("UserAppointments");
        Delete.Table("Appointments");
        Delete.Table("Users");
    }
}
