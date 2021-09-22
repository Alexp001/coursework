ALTER TABLE dbo.Accounting
ADD CONSTRAINT FK_Pizza_Accounting FOREIGN KEY (pizzaId)
    REFERENCES dbo.Pizza (id)
