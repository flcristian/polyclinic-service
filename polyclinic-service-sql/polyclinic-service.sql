delete from appointments where id = 21;
delete from userappointments where id = 41;
select * from appointments where id in (select appointmentId from userappointments where userid = 3) and StartDate >= "2023-11-1 00:00:00" and EndDate < "2023-11-2 00:00:00";
select * from appointments where id in (select appointmentId from userappointments where userid = 3) and StartDate > "2023-11-1 00:00:00" and EndDate < "2023-11-30 23:59:59";