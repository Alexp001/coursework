ALTER TABLE dbo.Orders
ADD CONSTRAINT FK_Clients_Orders FOREIGN KEY (clientId)
    REFERENCES dbo.Clients (id)