ALTER TABLE dbo.CommentOfClient
ADD CONSTRAINT FK_Clients_CommentOfClient FOREIGN KEY (clientId)
    REFERENCES dbo.Clients (id)
    ON DELETE SET NULL
