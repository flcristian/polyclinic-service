using FluentMigrator;
using System;
using System.Data;

namespace polyclinic_service.Data.Migrations;

[Migration(203012024)]
public class CreateScheduleTables : Migration
{ 
    public override void Up()
    {
        CreateTables();
        CreateIndexes();
        CreateForeignKeys();
    }

    private void CreateTables()
    {
        Create.Table("ScheduleSlots")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("StartDate").AsDateTime().NotNullable()
            .WithColumn("EndDate").AsDateTime().NotNullable();
        
        Create.Table("Schedules")
            .WithColumn("DoctorId").AsInt32().PrimaryKey()
            .WithColumn("MondayScheduleId").AsInt32().Nullable()
            .WithColumn("TuesdayScheduleId").AsInt32().Nullable()
            .WithColumn("WednesdayScheduleId").AsInt32().Nullable()
            .WithColumn("ThursdayScheduleId").AsInt32().Nullable()
            .WithColumn("FridayScheduleId").AsInt32().Nullable();
    }

    private void CreateIndexes()
    {
        Create.Index("IX_Schedules_MondayScheduleId").OnTable("Schedules").OnColumn("MondayScheduleId").Ascending().WithOptions().NonClustered();
        Create.Index("IX_Schedules_TuesdayScheduleId").OnTable("Schedules").OnColumn("TuesdayScheduleId").Ascending().WithOptions().NonClustered();
        Create.Index("IX_Schedules_WednesdayScheduleId").OnTable("Schedules").OnColumn("WednesdayScheduleId").Ascending().WithOptions().NonClustered();
        Create.Index("IX_Schedules_ThursdayScheduleId").OnTable("Schedules").OnColumn("ThursdayScheduleId").Ascending().WithOptions().NonClustered();
        Create.Index("IX_Schedules_FridayScheduleId").OnTable("Schedules").OnColumn("FridayScheduleId").Ascending().WithOptions().NonClustered();
    }
    
    private void CreateForeignKeys()
    {
        Create.ForeignKey("FK_Schedules_DoctorId").FromTable("Schedules").ForeignColumn("DoctorId").ToTable("Users").PrimaryColumn("Id").OnDelete(Rule.Cascade);
        Create.ForeignKey("FK_Schedules_MondayScheduleId").FromTable("Schedules").ForeignColumn("MondayScheduleId").ToTable("ScheduleSlots").PrimaryColumn("Id").OnDelete(Rule.SetNull);
        Create.ForeignKey("FK_Schedules_TuesdayScheduleId").FromTable("Schedules").ForeignColumn("TuesdayScheduleId").ToTable("ScheduleSlots").PrimaryColumn("Id").OnDelete(Rule.SetNull);
        Create.ForeignKey("FK_Schedules_WednesdayScheduleId").FromTable("Schedules").ForeignColumn("WednesdayScheduleId").ToTable("ScheduleSlots").PrimaryColumn("Id").OnDelete(Rule.SetNull);
        Create.ForeignKey("FK_Schedules_ThursdayScheduleId").FromTable("Schedules").ForeignColumn("ThursdayScheduleId").ToTable("ScheduleSlots").PrimaryColumn("Id").OnDelete(Rule.SetNull);
        Create.ForeignKey("FK_Schedules_FridayScheduleId").FromTable("Schedules").ForeignColumn("FridayScheduleId").ToTable("ScheduleSlots").PrimaryColumn("Id").OnDelete(Rule.SetNull);
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_Schedules_DoctorId").OnTable("Schedules");
        Delete.ForeignKey("FK_Schedules_MondayScheduleId").OnTable("Schedules");
        Delete.ForeignKey("FK_Schedules_TuesdayScheduleId").OnTable("Schedules");
        Delete.ForeignKey("FK_Schedules_WednesdayScheduleId").OnTable("Schedules");
        Delete.ForeignKey("FK_Schedules_ThursdayScheduleId").OnTable("Schedules");
        Delete.ForeignKey("FK_Schedules_FridayScheduleId").OnTable("Schedules");
        Delete.Index("IX_Schedules_MondayScheduleId").OnTable("Schedules");
        Delete.Index("IX_Schedules_TuesdayScheduleId").OnTable("Schedules");
        Delete.Index("IX_Schedules_WednesdayScheduleId").OnTable("Schedules");
        Delete.Index("IX_Schedules_ThursdayScheduleId").OnTable("Schedules");
        Delete.Index("IX_Schedules_FridayScheduleId").OnTable("Schedules");
        Delete.Table("Schedules");
        Delete.Table("ScheduleSlots");
    }
}