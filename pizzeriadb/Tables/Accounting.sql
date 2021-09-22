CREATE TABLE [dbo].[Accounting]
(
	id int identity primary key,
    quantity int not null,
	pizzaId int not null,
    orderId int not null
)
