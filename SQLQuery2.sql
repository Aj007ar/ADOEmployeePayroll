---UC1-----
create database EmployeePayRollServices

use EmployeePayRollServices
-----UC2----
create table employee_payroll
(
Id int IDENTITY(1,1) Primary key,
Name varchar(50) NOT NULL,
Salary float NOT NULL,
Startdate date ,
);
-----UC3----
insert into employee_payroll values('Prabhudas',25000,GETDATE())
insert into employee_payroll values('Bhanudas',35000,GETDATE())
insert into employee_payroll values('Amarendra',23000,GETDATE())
-----UC4-----
SELECT * FROM employee_payroll
-----UC5-----
select * from employee_payroll where Name='Bhanudas'
select * from employee_payroll where Startdate  between '01-01-1999' and  getdate()
select * from employee_payroll where Startdate between  CAST('01-31-1995' as date) and '2025-01-01'
-----UC6-----
alter table employee_payroll Add Gender char(1)  NOT NULL default 'M'  
update employee_payroll set Name='Priyanka' where Name= 'Prabhudas'
update employee_payroll set Gender='F' where Name= 'Priyanka'
----UC7------
SELECT SUM(salary) as TotalSalary,gender FROM employee_payroll GROUP BY gender;
SELECT Min(salary) as MinimumSalary,gender FROM employee_payroll GROUP BY gender;
SELECT Max(salary) as MaxSalary,gender FROM employee_payroll GROUP BY gender;
SELECT Count(salary) as CountIs,gender FROM employee_payroll GROUP BY gender;
-----UC8------
alter table employee_payroll add PhoneNumber bigint
alter table employee_payroll add Addresss varchar(100) default 'Bombay';
alter table employee_payroll add Department varchar(20) NOT NULL default 'IT'
------UC9-------
update employee_payroll set  PhoneNumber=9999999 where Name='Bhanudas' 
update employee_payroll set  PhoneNumber=2345123 where Name='Amarendra' 
update employee_payroll set  Addresss='Delhi' where Name='Amarendra'

EXEC sp_rename 'employee_payroll.Salary', 'BasicPay', 'COLUMN';
Alter table employee_payroll add Deductions float,Taxable_Pay float,Income_Tax float, Net_Pay float;
------UC10------
insert into employee_payroll (Name,BasicPay,StartDate,Gender,PhoneNumber,Addresss,Department) values('Terissa',12000,'01-01-2000','F',11223344,'pune','Sales'),
('Terissa',12000,'01-01-2000','F',11223344,'pune','Marketing');
------UC11------
Create Table Company
(CompanyID int identity(1,1) primary key,
CompanyName varchar(100))

--Insert Values in Company
Insert into Company values ('TCS'),('Infosys')
Select * from Company

drop table employee_payroll

--Create Employee Table

create table Employee
(EmployeeID int identity(1,1) primary key,
CompanyIdentity int,
EmployeeName varchar(50),
EmployeePhoneNumber bigInt,
EmployeeAddress varchar(100),
StartDate date,
Gender char,
Foreign key (CompanyIdentity) references Company(CompanyID)
)

--Insert Values in Employee
insert into Employee values
(1,'Ayusha Chakku',8833445566,'street 101 pune','2012-05-28','F'),
(2,'Arya cherry',7842905550,'street 503 mumbai ','2017-05-22','F'),
(1,'joy memon',8533445566,'street 101 delhi','2015-02-22','M'),
(2,'Arjun pandit',8633445566,'street 555 Dadar','2017-08-29','M')
Select * from Employee

--Create Payroll Table

create table PayrollCalculate
(BasicPay float,
Deductions float,
TaxablePay float,
IncomeTax float,
NetPay float,
EmployeeIdentity int,
Foreign key (EmployeeIdentity) references Employee(EmployeeID)
)

--Insert Values in Payroll Table
insert into PayrollCalculate(BasicPay,Deductions,IncomeTax,EmployeeIdentity) values 
(6000000,1000000,20000,1),
(6500000,200000,4000,2),
(5000000,10000,5000,3),
(8000000,399994,6784,4)
Select * from PayrollCalculate

--Update the Derived attribute values  TaxablePay and NetPay
update PayrollCalculate set TaxablePay=BasicPay-Deductions
update PayrollCalculate set NetPay=TaxablePay-IncomeTax
select * from PayrollCalculate

--Create Department Table
create table Department
(
DepartmentId int identity(1,1) primary key,
DepartName varchar(100)
)

insert into Department values
('Marketing'),
('Sales'),
('Publishing')
select * from Department

--Create table EmployeeDepartment
create table EmployeeDepartment
(
DepartmentIdentity int ,
EmployeeIdentity int,
Foreign key (EmployeeIdentity) references Employee(EmployeeID),
Foreign key (DepartmentIdentity) references Department(DepartmentID)
)

insert into EmployeeDepartment values
(3,1),
(2,2),
(1,3),
(3,4)
select * from EmployeeDepartment

-----------------UC12-------------------

--UC 4: Retrieve all Data
SELECT CompanyID,CompanyName,EmployeeID,EmployeeName,EmployeeAddress,EmployeePhoneNumber,StartDate,Gender,BasicPay,Deductions,TaxablePay,IncomeTax,NetPay,DepartName from Company
INNER JOIN Employee ON Company.CompanyID = Employee.CompanyIdentity
INNER JOIN PayrollCalculate on PayrollCalculate.EmployeeIdentity=Employee.EmployeeID
INNER JOIN EmployeeDepartment on Employee.EmployeeID=EmployeeDepartment.EmployeeIdentity
INNER JOIN Department on Department.DepartmentId=EmployeeDepartment.DepartmentIdentity 

--UC 5: Select Query using Cast() an GetDate()
SELECT CompanyID,CompanyName,EmployeeID,EmployeeName,BasicPay,Deductions,TaxablePay,IncomeTax,NetPay from Company
INNER JOIN Employee ON Company.CompanyID = Employee.CompanyIdentity and StartDate BETWEEN Cast('2012-11-12' as Date) and GetDate()
INNER JOIN PayrollCalculate on PayrollCalculate.EmployeeIdentity=Employee.EmployeeID

---UC5 Retrieve query based on Name
SELECT CompanyID,CompanyName,EmployeeID,EmployeeName,BasicPay,Deductions,TaxablePay,IncomeTax,NetPay from Company
INNER JOIN Employee ON Company.CompanyID = Employee.CompanyIdentity and Employee.EmployeeName='Arjun Pandit'
INNER JOIN PayrollCalculate on PayrollCalculate.EmployeeIdentity=Employee.EmployeeID

--UC 7: Use Aggregate Functions and Group by Gender
select Sum(BasicPay) as "TotalSalary",Gender from Employee
INNER JOIN PayrollCalculate on PayrollCalculate.EmployeeIdentity=Employee.EmployeeID group by Gender;
select Avg(BasicPay) as "AverageSalary",Gender from Employee
INNER JOIN PayrollCalculate on PayrollCalculate.EmployeeIdentity=Employee.EmployeeID group by Gender;
select Min(BasicPay) as "MinimumSalary",Gender from Employee
INNER JOIN PayrollCalculate on PayrollCalculate.EmployeeIdentity=Employee.EmployeeID group by Gender;
select Max(BasicPay)  as "MaximumSalary",Gender from Employee
INNER JOIN PayrollCalculate on PayrollCalculate.EmployeeIdentity=Employee.EmployeeID group by Gender;
select Count(BasicPay) as "CountSalary",Gender from Employee
INNER JOIN PayrollCalculate on PayrollCalculate.EmployeeIdentity=Employee.EmployeeID group by Gender;