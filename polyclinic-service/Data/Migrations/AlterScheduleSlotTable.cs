﻿using FluentMigrator;

namespace polyclinic_service.Data.Migrations;

[Migration(327022024)]
public class AlterScheduleSlotTable : Migration
{
    public override void Up()
    {
        Delete.FromTable("ScheduleSlots").AllRows();
        
        Alter.Table("ScheduleSlots")
            .AddColumn("StartTime").AsString().NotNullable()
            .AddColumn("EndTime").AsString().NotNullable();

        Delete.Column("StartDate").FromTable("ScheduleSlots");
        Delete.Column("EndDate").FromTable("ScheduleSlots");
    }

    public override void Down()
    {
        Delete.Column("StartTime").FromTable("ScheduleSlots");  
        Delete.Column("EndTime").FromTable("ScheduleSlots");

        Alter.Table("ScheduleSlots")
            .AddColumn("StartDate").AsDateTime().NotNullable()
            .AddColumn("EndDate").AsDateTime().NotNullable();
    }
}