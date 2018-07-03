CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Product_Number] varchar(25) NOT NULL, 
    [Description] VARCHAR(50) NULL, 
    [Price] MONEY NULL DEFAULT 0.00, 
    [Weight] DECIMAL NULL, 
    [Master_Units] INT NULL, 
    [Cubic_Feet] INT NULL, 
    [quantity_on_hand] INT NULL, 
    [annual_use] INT NULL, 
    [sales_last_period] INT NULL, 
    [ytd_sales] INT NULL, 
    [gross] INT NULL, 
    [assembly_time_secs] INT NULL, 
    [product_code] INT NULL, 
    [string_along] VARCHAR(25) NULL
)
