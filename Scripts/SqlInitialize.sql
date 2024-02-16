create table Companies (
       Id int PRIMARY KEY IDENTITY,
       Name varchar(250) not null
);

create table Passports (
       Id int PRIMARY KEY IDENTITY,
       Type varchar(250),
       Number varchar(250)
);

create table Departments (
         Id int PRIMARY KEY IDENTITY,
         Name varchar(250),
         Phone varchar(250)
);

create table Employees (
Id int PRIMARY KEY IDENTITY,
Name varchar(250) not null,
Surname varchar(250) not null,
Phone varchar(20) unique,
CompanyId int references Companies (Id),
PassportId int references Passports (Id),
DepartmentId int references Departments (Id)
);


insert into Companies values ('smartway');
insert into Passports values ('ID', '28-12-121212');
insert into Departments values ('AI', '89881231212');