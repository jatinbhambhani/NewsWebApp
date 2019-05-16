CREATE TABLE [dbo].[ArticleDetails]
(
	[ArticleId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ArticleTitle] NVARCHAR(2000) NOT NULL, 
    [PublishDate] DATE NOT NULL, 
    [ArticleContent] NVARCHAR(1000) NOT NULL, 
    [AuthorId] NVARCHAR(128) NOT NULL, 
    CONSTRAINT [FK_ArticleDetails_ToUser] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[AspNetUsers]([Id])
)
