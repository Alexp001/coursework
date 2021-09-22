ALTER TABLE dbo.AccountingRoles
ADD CONSTRAINT FK_Roles_AccountingRoles FOREIGN KEY (roleId)
    REFERENCES dbo.Roles (id)