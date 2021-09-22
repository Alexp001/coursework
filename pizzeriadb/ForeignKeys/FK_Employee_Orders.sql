ALTER TABLE dbo.Orders
ADD CONSTRAINT FK_Employee_Orders FOREIGN KEY (employeeId)
    REFERENCES dbo.Employee (id)