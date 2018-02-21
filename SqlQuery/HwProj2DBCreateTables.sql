use HwProj2DB

--drop table StudentsHomework, Tasks, CourseMates, Courses, Users

create table Users (
Id uniqueidentifier,
Name nvarchar(30),
Surname nvarchar(30),
Gender char,
UserType char,
Email varchar(30) not null,
constraint UQ_Users_NameAndSurname unique (Name, Surname),
constraint UQ_Users_Email unique (Email),
constraint PK_Users primary key (Id),
constraint CK_Users_Gender check (Gender = 'M' or Gender = 'F'),
constraint CK_Users_UserType check (UserType = 'S' or UserType = 'T')
);

go

create table Courses (
Id int identity(1,1) not null,
Name nvarchar(15) not null,
GroupName nvarchar(15),
IsComplete bit not null,
constraint PK_Courses primary key (Id),
constraint UQ_Courses_Name unique (Name) 
);

go

create table CourseMates (
CourseId int,
UserId uniqueidentifier,
constraint FK_CourseMates_CourseId foreign key (CourseId) references Courses (Id) on delete cascade,
constraint FK_CourseMates_UserId foreign key (UserId) references Users (Id) on delete cascade
);

go 

create table Tasks (
Id int identity(1,1) not null,
CourseId int,
Title nvarchar(20),
Description nvarchar(100),
constraint PK_Tasks primary key (Id),
constraint FK_Tasks_CourseId foreign key (CourseId) references Courses (Id) on delete cascade
);

go

create table StudentsHomework (
TaskId int,
StudentId uniqueidentifier,
IsComplete bit not null,
constraint FK_StudentsHomework_TaskId foreign key (TaskId) references Tasks (Id) on delete cascade,
constraint FK_StudentsHomework_StudentId foreign key (StudentId) references Users (Id) on delete cascade
);