﻿ALTER TABLE dbo.Clients
ADD CONSTRAINT FK_Users_Clients FOREIGN KEY (userId)
    REFERENCES dbo.Users (id)
    ON DELETE CASCADE
