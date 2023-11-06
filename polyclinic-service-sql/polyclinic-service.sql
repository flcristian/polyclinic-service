create table Appointments(
    Id integer primary key auto_increment,
    StartDate datetime not null,
    EndDate datetime not null
);

drop table versioninfo;
drop table users;

select * from Appointments;

insert into Appointments(id, startdate, enddate) values(1, "2023-11-12 12:00:00", "2023-11-12 14:00:00");
insert into Appointments(id, startdate, enddate) values(2, "2023-11-08 19:00:00", "2023-11-08 21:00:00");
insert into Appointments(id, startdate, enddate) values(3, "2023-12-01 04:00:00", "2023-12-01 06:00:00");
insert into Appointments(id, startdate, enddate) values(4, "2023-11-19 10:00:00", "2023-11-19 12:00:00");
insert into Appointments(id, startdate, enddate) values(5, "2023-11-21 15:00:00", "2023-11-21 17:00:00");
insert into Appointments(id, startdate, enddate) values(6, "2023-11-15 08:00:00", "2023-11-15 10:00:00");
insert into Appointments(id, startdate, enddate) values(7, "2023-11-23 07:30:00", "2023-11-23 09:30:00");
insert into Appointments(id, startdate, enddate) values(8, "2023-11-29 16:45:00", "2023-11-29 18:45:00");
insert into Appointments(id, startdate, enddate) values(9, "2023-11-09 03:30:00", "2023-11-09 05:30:00");
insert into Appointments(id, startdate, enddate) values(10, "2023-11-14 12:15:00", "2023-11-14 14:15:00");
insert into Appointments(id, startdate, enddate) values(11, "2023-11-18 11:45:00", "2023-11-18 13:45:00");
insert into Appointments(id, startdate, enddate) values(12, "2023-11-25 02:20:00", "2023-11-25 04:20:00");
insert into Appointments(id, startdate, enddate) values(13, "2023-11-07 14:10:00", "2023-11-07 16:10:00");
insert into Appointments(id, startdate, enddate) values(14, "2023-11-30 09:00:00", "2023-11-30 11:00:00");
insert into Appointments(id, startdate, enddate) values(15, "2023-11-16 18:30:00", "2023-11-16 20:30:00");
insert into Appointments(id, startdate, enddate) values(16, "2023-11-05 07:50:00", "2023-11-05 09:50:00");
insert into Appointments(id, startdate, enddate) values(17, "2023-11-26 17:25:00", "2023-11-26 19:25:00");
insert into Appointments(id, startdate, enddate) values(18, "2023-11-28 21:40:00", "2023-11-28 23:40:00");
insert into Appointments(id, startdate, enddate) values(19, "2023-11-12 04:55:00", "2023-11-12 06:55:00");
insert into Appointments(id, startdate, enddate) values(20, "2023-11-22 13:20:00", "2023-11-22 15:20:00");
insert into Appointments(id, startdate, enddate) values(21, "2023-11-08 22:00:00", "2023-11-08 00:00:00");
insert into Appointments(id, startdate, enddate) values(22, "2023-11-11 11:30:00", "2023-11-11 13:30:00");
insert into Appointments(id, startdate, enddate) values(23, "2023-11-17 09:15:00", "2023-11-17 11:15:00");
insert into Appointments(id, startdate, enddate) values(24, "2023-11-27 16:35:00", "2023-11-27 18:35:00");
insert into Appointments(id, startdate, enddate) values(25, "2023-11-19 06:05:00", "2023-11-19 08:05:00");
insert into Appointments(id, startdate, enddate) values(26, "2023-11-13 20:25:00", "2023-11-13 22:25:00");
insert into Appointments(id, startdate, enddate) values(27, "2023-11-24 15:45:00", "2023-11-24 17:45:00");
insert into Appointments(id, startdate, enddate) values(28, "2023-11-10 01:00:00", "2023-11-10 03:00:00");
insert into Appointments(id, startdate, enddate) values(29, "2023-11-20 08:45:00", "2023-11-20 10:45:00");
insert into Appointments(id, startdate, enddate) values(30, "2023-11-30 18:15:00", "2023-11-30 20:15:00");



select * from Users;