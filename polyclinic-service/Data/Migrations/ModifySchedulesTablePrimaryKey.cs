using FluentMigrator;

namespace polyclinic_service.Data.Migrations
{
    [Migration(501032031)]
    public class ModifySchedulesTablePrimaryKey : Migration
    {
        public override void Up()
        {
            Delete.FromTable("Schedules").AllRows();
            Delete.FromTable("ScheduleSlots").AllRows();
            Delete.ForeignKey("FK_Schedules_DoctorId").OnTable("Schedules"); 
            
            Execute.Sql("ALTER TABLE `Schedules` DROP PRIMARY KEY, " +
                        "MODIFY `DoctorId` INT NOT NULL, " +
                        "ADD `Id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY FIRST;");

            Create.ForeignKey("FK_Schedules_DoctorId")
                .FromTable("Schedules").ForeignColumn("DoctorId")
                .ToTable("Users").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_Schedules_DoctorId").OnTable("Schedules"); 

            Execute.Sql("ALTER TABLE `Schedules` DROP PRIMARY KEY, DROP `Id`, " +
                        "MODIFY `DoctorId` INT NOT NULL AUTO_INCREMENT, " +
                        "ADD PRIMARY KEY (`DoctorId`);");

            Create.ForeignKey("FK_Schedules_DoctorId")
                .FromTable("Schedules").ForeignColumn("DoctorId")
                .ToTable("Users").PrimaryColumn("Id");
        }
    }
}