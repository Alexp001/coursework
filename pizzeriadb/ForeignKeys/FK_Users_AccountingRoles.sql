ALTER TABLE dbo.AccountingRoles
ADD CONSTRAINT FK_Users_AccountingRoles FOREIGN KEY (userId)     
    REFERENCES dbo.Users (id)