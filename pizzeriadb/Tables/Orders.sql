CREATE TABLE [dbo].[Orders]
(
	id int identity primary key,
    orderDate datetime NOT NULL,
    clientId INT,
    employeeId INT,
    accountingImplementation bit not null
)
