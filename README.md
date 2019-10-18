# LiteracyProCodingChallenge

This challenge was created in Visual Studio 2019.

To create the databases, you can update the PersonalPurchaseTrackerContext connection string in appsettings.json to use the database you want, then run "Update-Database" from the PM Console.  This will create the required tables and seed them with data.

Alternatively you can use the following script:


CREATE TABLE [Category] (
    [ID] int NOT NULL IDENTITY,
    [CategoryName] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([ID])
);

GO

CREATE TABLE [Transaction] (
    [ID] int NOT NULL IDENTITY,
    [Payee] nvarchar(100) NOT NULL,
    [CategoryID] int NOT NULL,
    [PurchaseAmount] decimal(18, 2) NOT NULL,
    [PurchaseDate] datetime2 NOT NULL,
    [Memo] nvarchar(100) NULL,
    CONSTRAINT [PK_Transaction] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Transaction_Category_CategoryID] FOREIGN KEY ([CategoryID]) REFERENCES [Category] ([ID]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Transaction_CategoryID] ON [Transaction] ([CategoryID]);

GO




Please contact Tim Hanks if you have any problems building or running the solution.
