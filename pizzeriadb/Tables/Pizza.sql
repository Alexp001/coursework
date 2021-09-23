CREATE TABLE [dbo].[Pizza]
(
	id int identity primary key,
	nameOfPizza NVARCHAR(20) not null,
    price decimal(18, 2) not null,
    [image] nvarchar(500),
	onSale bit default 1 not null,
)
