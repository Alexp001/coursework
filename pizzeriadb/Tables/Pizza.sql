CREATE TABLE [dbo].[Pizza]
(
	id int identity primary key,
	nameOfPizza NVARCHAR(20),
    price decimal(18, 2),
    [image] nvarchar(500)
)
