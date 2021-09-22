ALTER TABLE dbo.Accounting
ADD CONSTRAINT FK_Orders_Accounting FOREIGN KEY (orderId)
    REFERENCES dbo.Orders (id)