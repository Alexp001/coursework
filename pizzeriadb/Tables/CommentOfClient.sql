CREATE TABLE [dbo].[CommentOfClient]
(
    id int identity primary key,
	textOfComment text,
    kindOfComment nvarchar(10),
    mark int not null,
    clientId int
)
