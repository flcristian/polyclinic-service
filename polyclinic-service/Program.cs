using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using polyclinic_service.Appointments.Repository;
using polyclinic_service.Appointments.Repository.Interfaces;
using polyclinic_service.Data;
using polyclinic_service.UserAppointments.Repository;
using polyclinic_service.UserAppointments.Repository.Interfaces;
using polyclinic_service.Users.Repository;
using polyclinic_service.Users.Repository.Interfaces;

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

#endregion

#region SERVICES

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
