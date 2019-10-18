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


INSERT INTO [dbo].[Category] (             
    [CategoryName] )
VALUES (
	'Train'
)
INSERT INTO [dbo].[Category] (             
    [CategoryName] )
VALUES (
	'Per Diem'
)
INSERT INTO [dbo].[Category] (             
    [CategoryName] )
VALUES (
	'Meals'
)
INSERT INTO [dbo].[Category] (             
    [CategoryName] )
VALUES (
	'Air Travel'
)
INSERT INTO [dbo].[Category] (             
    [CategoryName] )
VALUES (
	'Software Purchase'
)

INSERT INTO [dbo].[Transaction] (             
    [Payee],         
    [CategoryID],     
    [PurchaseAmount],
	[Memo],
    [PurchaseDate]   
     )
VALUES (
	'Boulder Express',
	1,
	56.76,
	'Train To Boulder',
	GETDATE()-3
)

INSERT INTO [dbo].[Transaction] (             
    [Payee],         
    [CategoryID],     
    [PurchaseAmount],
	[Memo],
    [PurchaseDate]  )
VALUES (
	'Huckleberry',
	3,
	105.08,
	'Dinner at Huckleberry',
	GETDATE() -2
)

INSERT INTO [dbo].[Transaction] (             
    [Payee],         
    [CategoryID],     
    [PurchaseAmount],
	[Memo],
    [PurchaseDate]  )
VALUES (
	'Tim Hanks',
	5,
	200.00,
	'Creation of Purchase Tracker',
	GETDATE() -1
)

INSERT INTO [dbo].[Transaction] (             
    [Payee],         
    [CategoryID],     
    [PurchaseAmount],
	[Memo],
    [PurchaseDate]  )
VALUES (
	'Madeup Airlines',
	4,
	99.99,
	'Flight Back to Denver',
	GETDATE() 
)

                    