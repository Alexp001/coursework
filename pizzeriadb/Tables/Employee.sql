CREATE TABLE [dbo].[Employee]
(
	id int identity primary key,
    nameOfEmployee NVARCHAR(20) NOT NULL,
    email NVARCHAR(30),
    phone NVARCHAR(20) NOT NULL,
    salary decimal(18, 2) NOT NULL,
    employeePosition NVARCHAR(20),
    [address] NVARCHAR(20),
    userId INT not null,
)
