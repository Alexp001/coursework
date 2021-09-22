CREATE TABLE [dbo].[Users]
(
	id int identity primary key,
	userLogin NVARCHAR(20) NOT NULL unique,
    userPassword NVARCHAR(20) NOT NULL
)
