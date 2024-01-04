using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using polyclinic_service.Appointments.Repository;
using polyclinic_service.Appointments.Repository.Interfaces;
using polyclinic_service.Appointments.Services;
using polyclinic_service.Appointments.Services.Interfaces;
using polyclinic_service.Data;
using polyclinic_service.Schedules.Repository;
using polyclinic_service.Schedules.Repository.Interfaces;
using polyclinic_service.Schedules.Services;
using polyclinic_service.Schedules.Services.Interfaces;
using polyclinic_service.UserAppointments.Repository;
using polyclinic_service.UserAppointments.Repository.Interfaces;
using polyclinic_service.UserAppointments.Services;
using polyclinic_service.UserAppointments.Services.Interfaces;
using polyclinic_service.Users.Repository;
using polyclinic_service.Users.Repository.Interfaces;
using polyclinic_service.Users.Services;
using polyclinic_service.Users.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region BASE

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("Default")!,
        new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddMySql5()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("Default"))
        .ScanIn(typeof(Program).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole());

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#endregion

#region REPOSITORIES

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IUserAppointmentRepository, UserAppointmentRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();

#endregion

#region SERVICES

builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IAppointmentQueryService, AppointmentQueryService>();
builder.Services.AddScoped<IAppointmentCommandService, AppointmentCommandService>();
builder.Services.AddScoped<IUserAppointmentQueryService, UserAppointmentQueryService>();
builder.Services.AddScoped<IUserAppointmentCommandService, UserAppointmentCommandService>();
builder.Services.AddScoped<IScheduleQueryService, ScheduleQueryService>();
builder.Services.AddScoped<IScheduleCommandService, ScheduleCommandService>();

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler("/Home/Error");
app.UseDeveloperExceptionPage();    

using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

app.Run();
