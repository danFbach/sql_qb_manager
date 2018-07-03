CREATE TABLE [dbo].[reqd_part]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [product_Id_FK] INT NULL, 
    [part_Id_FK] INT NULL, 
    [quantity_reqd] INT NULL DEFAULT 0, 
    CONSTRAINT [product_id_FK] FOREIGN KEY ([product_Id_FK]) REFERENCES [Products]([Id]), 
    CONSTRAINT [reqd_part_id_FK] FOREIGN KEY ([part_Id_FK]) REFERENCES [Parts]([Id])
)
