using FluentMigrator;

namespace polyclinic_service.Data.Migrations;

[Migration(429022024)]
public class AddWeekIdentificationToScheduleTable : Migration
{
    public override void Up()
    {
        Delete.FromTable("Schedules").AllRows();
        Delete.FromTable("ScheduleSlots").AllRows();
        
        Alter.Table("Schedules")
            .AddColumn("Year").AsInt32().NotNullable()
            .AddColumn("WeekNumber").AsInt32().NotNullable();
    }

    public override void Down()
    {
        Delete.Column("Year").FromTable("Schedules");
        Delete.Column("WeekNumber").FromTable("Schedules");
    }
}