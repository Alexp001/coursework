CREATE TABLE [dbo].[Clients]
(
    id int identity primary key,
	nameOfClient NVARCHAR(20) NOT NULL,
    email NVARCHAR(30),
    phone NVARCHAR(20) NOT NULL,
    address NVARCHAR(20),
    userId INT not null
)
