CREATE TABLE [dbo].[part_vendor_relational]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [part_id] INT NULL, 
    [vendor_id] INT NULL, 
    CONSTRAINT [part_id_FK] FOREIGN KEY ([part_id]) REFERENCES [Parts]([Id]), 
    CONSTRAINT [vendor_id_FK] FOREIGN KEY ([vendor_id]) REFERENCES [vendor]([Id])
)
