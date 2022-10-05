create database PayrollService;
use PayrollService;

create table EmployeePayroll
(
	EmployeeId int identity(1,1) primary key,
	EmployeeName varchar (50),
	EmployeeSalary float,
	StartDate date
);

insert into EmployeePayroll values ('Abc', 25.36, cast ('2022-01-04' as date));
insert into EmployeePayroll values ('Def', 35.36, cast ('2022-04-01' as date));
insert into EmployeePayroll values ('Ghi', 45.36, cast ('2022-06-04' as date));
insert into EmployeePayroll values ('Jkl', 55.36, cast ('2022-07-04' as date));
insert into EmployeePayroll values ('Mno', 65.36, cast ('2022-08-04' as date));
insert into EmployeePayroll values ('Pqr', 75.36, cast ('2022-09-04' as date));

select * from EmployeePayroll;

select EmployeeSalary from EmployeePayroll where EmployeeName = 'Pqr';
select employeeSalary from EmployeePayroll where StartDate between cast ('2022-05-01' as date) and GETDATE();

alter table employeepayroll add Gender varchar(1);

update EmployeePayroll set gender = 'M' where EmployeeId between 2 and 4; 
update EmployeePayroll set gender = 'M' where gender is null;

update EmployeePayroll set gender = 'F' where EmployeeId < 2;
update EmployeePayroll set gender = 'F' where EmployeeId >= 4; 

select gender, SUM(employeesalary) as TotalSalary from EmployeePayroll group by gender;
select gender, Avg(employeesalary) as AverageSalary from EmployeePayroll group by gender;
select gender, Min(employeesalary) as MinimumSalary from EmployeePayroll group by gender;
select gender, Max(employeesalary) as MaximumSalary from EmployeePayroll group by gender;
select gender, Count(EmployeeId) as HeadCount from EmployeePayroll group by gender;

create table EmployeeDetails
(
	EmployeeId int foreign key references EmployeePayroll(EmployeeId),
	Contact varchar (10),
	City varchar (20),
	States varchar(20),
	Zip varchar (6)
);

create table TaxDetails
(
	EmployeeId int foreign key references EmployeePayroll(EmployeeId),
	Deductions float,
	TaxPercent float
);

exec sp_rename 'EmployeePayroll.employeesalary','BasicPay','column';

insert into EmployeeDetails values (1,'9876543210','Pune','Maharashtra','400000');
insert into EmployeeDetails values (2,'1234567890','Mumbai','Maharashtra','400000');
insert into EmployeeDetails values (3,'8888999912','Nasik','Maharashtra','400000');
insert into EmployeeDetails values (4,'8877992255','New Delhi','Delhi','400000');
insert into EmployeeDetails values (5,'1122334455','Bangalore','Karnataka','400000');
insert into EmployeeDetails values (6,'7744552244','Goa','Goa','400000');

insert into TaxDetails values (1,2.5,0.02);
insert into TaxDetails values (2,2.1,0.04);
insert into TaxDetails values (3,3.0,0.05);
insert into TaxDetails values (4,1.2,0.06);
insert into TaxDetails values (5,0.4,0.06);
insert into TaxDetails values (6,7.8,0.09);

create procedure GetCompleteDetails
as
select employeedetails.EmployeeId,Gender,EmployeeName,Contact,City,States,Zip,BasicPay,StartDate,Deductions,TaxPercent,(BasicPay - Deductions) * TaxPercent as Taxpaid, (BasicPay - Deductions - (BasicPay - Deductions) * TaxPercent) as NetPay from EmployeePayroll,EmployeeDetails,TaxDetails where EmployeeDetails.EmployeeId = EmployeePayroll.EmployeeId and EmployeeDetails.EmployeeId = TaxDetails.EmployeeId;

exec GetCompleteDetails;

create procedure AddEmployee 
@name varchar(50), @gender varchar (1), @contact varchar(10), @city varchar(20), @state varchar(20), @zip varchar(6), @startdate date, @basicpay float, @deductions float, @taxpercent float
as
insert into EmployeePayroll values (@name, @basicpay, @startdate, @gender)
declare @id int
select @id = SCOPE_IDENTITY()
insert into EmployeeDetails values (@id,@contact,@city,@state,@zip)
insert into TaxDetails values (@id,@deductions,@taxpercent)

exec AddEmployee 'Xyz','M','9887766550','someCity','someState','123456','2022-09-01',1000,100,0.01

exec GetCompleteDetails;

create procedure DeleteEmployee 
@name varchar(60)
as
declare @id as int
select @id = EmployeeId from EmployeePayroll where EmployeeName = @name 
delete from TaxDetails where EmployeeId = @id
delete from EmployeeDetails where EmployeeId = @id
delete from EmployeePayroll where EmployeeId = @id