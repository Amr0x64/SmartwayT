create table Companies (
       Id int PRIMARY KEY IDENTITY,
       Name varchar(250) not null
);

create table Departments (
         Id int PRIMARY KEY IDENTITY,
         Name varchar(250) unique,
         Phone varchar(250)
);

create table Employees (
        Id int PRIMARY KEY IDENTITY,    
        Name varchar(250) not null,
        Surname varchar(250) not null,
        Phone varchar(20) unique,
        CompanyId int references Companies (Id),
        DepartmentId int references Departments (Id)
);

create table Passports (
       Number varchar(250) PRIMARY KEY,
       Type varchar(250),
       EmployeeId int references Employees(Id)
);

--Данная инициализация требуется, для успешного создания сотрудника. 
insert into Companies values ('smartway');
insert into Departments values ('AI', '89881231212');