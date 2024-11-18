create table Users(
U_id int primary key identity(1,1),
U_name nvarchar(100) not null,
U_password nvarchar(100) not null,
U_email nvarchar(100) unique not null,
U_role nvarchar(100) not null,);

create table Tasks(
T_id int primary key identity(1,1),
T_title nvarchar(100) not null,
T_desc nvarchar(100) ,
T_status nvarchar(100) not null,
T_duedata datetime,
U_id int ,
foreign key (U_id) references Users(U_id)
);

insert into Users(U_email,U_name,U_password,U_role)
values('youssef@gmail.com', 'youssef','10','Manger');

insert into Users(U_email,U_name,U_password,U_role)
values('A@gmail.com', 'assar','10','Manger');

insert into Users(U_email,U_name,U_password,U_role)
values('gad@gmail.com', 'gad','010','Empolyee');

insert into Users(U_email,U_name,U_password,U_role)
values('ehab@gmail.com', 'ehab','010','Empolyee');

insert into Tasks(T_title,T_status,T_desc,T_duedata,U_id)
values('frontend', 'Pending','this code for web','10/2/2024',3);

insert into Tasks(T_title,T_status,T_desc,T_duedata,U_id)
values('backend', 'Completed','this code for web','10/2/2024',3);

insert into Tasks(T_title,T_status,T_desc,T_duedata,U_id)
values('desktob', 'Completed','this code for desktob','10/2/2024',4);

insert into Tasks(T_title,T_status,T_desc,T_duedata,U_id)
values('Database', 'Pending','this code for database','10/2/2024 8:50:22',4);
select * from Users;
select * from Tasks;
insert into Users(U_email,U_name,U_password,U_role)
values('mo@gmail.com', 'mo','010','Empolyee');