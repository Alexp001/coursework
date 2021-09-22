ALTER TABLE dbo.Employee
ADD CONSTRAINT FK_Users_Employee FOREIGN KEY (userId)
    REFERENCES dbo.Users (id)
    ON DELETE CASCADE