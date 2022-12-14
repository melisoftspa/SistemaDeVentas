USE [master]
GO
/****** Object:  Database [SalesSystemDB]    Script Date: 24-12-2022 0:19:55 ******/
CREATE DATABASE [SalesSystemDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SalesSystemDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SalesSystemDB.mdf' , SIZE = 50176KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SalesSystemDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SalesSystemDB_log.ldf' , SIZE = 2560KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SalesSystemDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SalesSystemDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SalesSystemDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SalesSystemDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SalesSystemDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SalesSystemDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SalesSystemDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SalesSystemDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SalesSystemDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SalesSystemDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SalesSystemDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SalesSystemDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SalesSystemDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SalesSystemDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SalesSystemDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SalesSystemDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SalesSystemDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SalesSystemDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SalesSystemDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SalesSystemDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SalesSystemDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SalesSystemDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SalesSystemDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SalesSystemDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SalesSystemDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SalesSystemDB] SET  MULTI_USER 
GO
ALTER DATABASE [SalesSystemDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SalesSystemDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SalesSystemDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SalesSystemDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SalesSystemDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SalesSystemDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SalesSystemDB] SET QUERY_STORE = OFF
GO
USE [SalesSystemDB]
GO
/****** Object:  Table [dbo].[category]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[id] [uniqueidentifier] NOT NULL,
	[Value] [int] NULL,
	[Text] [nvarchar](500) NULL,
	[state] [bit] NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[decrease]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[decrease](
	[id] [uniqueidentifier] NOT NULL,
	[date] [datetime] NULL,
	[id_product] [uniqueidentifier] NULL,
	[amount] [float] NULL,
	[tax] [float] NULL,
	[total] [float] NULL,
	[note] [nvarchar](1000) NULL,
	[line] [nvarchar](4000) NULL,
 CONSTRAINT [PK_decrease] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detail]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detail](
	[id] [uniqueidentifier] NOT NULL,
	[date] [datetime] NULL,
	[id_sale] [uniqueidentifier] NULL,
	[id_product] [uniqueidentifier] NULL,
	[product_name] [nvarchar](500) NULL,
	[amount] [float] NULL,
	[price] [float] NULL,
	[tax] [float] NULL,
	[total] [float] NULL,
	[total_tax] [float] NULL,
	[state] [bit] NULL,
 CONSTRAINT [PK_detail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[entries]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[entries](
	[id] [uniqueidentifier] NOT NULL,
	[date] [datetime] NULL,
	[id_product] [uniqueidentifier] NULL,
	[name_product] [nvarchar](1000) NULL,
	[amount] [int] NULL,
	[total] [float] NULL,
	[state] [bit] NULL,
	[id_user] [int] NULL,
 CONSTRAINT [PK_entries] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[historical]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[historical](
	[id] [uniqueidentifier] NOT NULL,
	[date] [datetime] NULL,
	[id_user] [int] NULL,
	[action] [nvarchar](50) NULL,
	[before] [nvarchar](max) NULL,
	[after] [nvarchar](max) NULL,
	[type] [nchar](10) NULL,
	[description] [nchar](10) NULL,
	[id_product] [uniqueidentifier] NULL,
 CONSTRAINT [PK_historical] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[invoice]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[invoice](
	[id] [uniqueidentifier] NOT NULL,
	[date] [datetime] NULL,
	[state] [bit] NULL,
	[name] [nvarchar](1500) NULL,
	[amount] [float] NULL,
	[total] [float] NULL,
	[name_provider] [nvarchar](1500) NULL,
	[notes] [nvarchar](4000) NULL,
	[state_payment] [bit] NULL,
	[payment_check] [bit] NULL,
	[payment_check_number] [nvarchar](500) NULL,
	[payment_check_date] [date] NULL,
	[payment_check_total] [float] NULL,
	[payment_cash] [bit] NULL,
	[payment_cash_total] [float] NULL,
 CONSTRAINT [PK_invoice] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pack]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pack](
	[id] [uniqueidentifier] NULL,
	[date] [date] NULL,
	[id_pack] [uniqueidentifier] NULL,
	[id_product] [uniqueidentifier] NULL,
	[bar_code] [nvarchar](500) NULL,
	[name] [nvarchar](500) NULL,
	[amount] [float] NULL,
	[line] [nvarchar](1500) NULL,
	[state] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[parameter]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[parameter](
	[id] [uniqueidentifier] NOT NULL,
	[date] [datetime] NULL,
	[module] [nvarchar](1000) NULL,
	[name] [nvarchar](1000) NULL,
	[value] [nvarchar](4000) NULL,
	[type] [nvarchar](500) NULL,
 CONSTRAINT [PK_parameter] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[id] [uniqueidentifier] NOT NULL,
	[date] [datetime] NULL,
	[state] [bit] NULL,
	[name] [nvarchar](500) NULL,
	[amount] [int] NULL,
	[sale_price] [float] NULL,
	[minimum] [int] NULL,
	[bar_code] [nvarchar](500) NULL,
	[photo] [image] NULL,
	[stock] [int] NULL,
	[price] [float] NULL,
	[exenta] [bit] NULL,
	[id_tax] [uniqueidentifier] NULL,
	[expiration] [date] NULL,
	[id_category] [uniqueidentifier] NULL,
	[isPack] [bit] NULL,
	[id_pack] [uniqueidentifier] NULL,
	[line] [nvarchar](1500) NULL,
	[id_subcategory] [uniqueidentifier] NULL,
 CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[roles]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roles](
	[id] [int] IDENTITY(2,1) NOT NULL,
	[role_id] [int] NULL,
	[view] [text] NULL,
	[username] [text] NULL,
	[state] [int] NULL,
	[actionAdd] [tinyint] NULL,
	[actionEdit] [tinyint] NULL,
	[actionDelete] [tinyint] NULL,
 CONSTRAINT [PK_roles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sale]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sale](
	[id] [uniqueidentifier] NOT NULL,
	[date] [datetime] NULL,
	[name] [nvarchar](500) NULL,
	[amount] [float] NULL,
	[tax] [float] NULL,
	[total] [float] NULL,
	[id_user] [int] NULL,
	[note] [nvarchar](500) NULL,
	[change] [float] NULL,
	[payment_cash] [float] NULL,
	[payment_other] [float] NULL,
	[ticket] [int] NULL,
	[state] [bit] NULL,
 CONSTRAINT [PK_sale] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[subcategory]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subcategory](
	[id] [uniqueidentifier] NOT NULL,
	[date] [datetime] NULL,
	[state] [bit] NULL,
	[id_category] [uniqueidentifier] NULL,
	[value] [nvarchar](500) NULL,
	[text] [nvarchar](500) NULL,
 CONSTRAINT [PK_subcategory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tax]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tax](
	[id] [uniqueidentifier] NOT NULL,
	[Value] [nvarchar](500) NULL,
	[Text] [nvarchar](500) NULL,
	[exenta] [bit] NULL,
 CONSTRAINT [PK_tax] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date] [date] NULL,
	[username] [nvarchar](500) NULL,
	[password] [nvarchar](1000) NULL,
	[role] [nvarchar](50) NULL,
	[name] [nvarchar](1000) NULL,
	[in_invoice] [bit] NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[category] ADD  CONSTRAINT [DF_category_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[category] ADD  CONSTRAINT [DF_category_Value]  DEFAULT ((-1)) FOR [Value]
GO
ALTER TABLE [dbo].[decrease] ADD  CONSTRAINT [DF_decrease_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[decrease] ADD  CONSTRAINT [DF_decrease_date]  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [dbo].[detail] ADD  CONSTRAINT [DF_detail_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[detail] ADD  CONSTRAINT [DF_detail_date]  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [dbo].[detail] ADD  CONSTRAINT [DF_detail_state]  DEFAULT ((1)) FOR [state]
GO
ALTER TABLE [dbo].[entries] ADD  CONSTRAINT [DF_entries_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[entries] ADD  CONSTRAINT [DF_entries_date]  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [dbo].[historical] ADD  CONSTRAINT [DF_historical_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[historical] ADD  CONSTRAINT [DF_historical_date]  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [dbo].[invoice] ADD  CONSTRAINT [DF_invoice_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[invoice] ADD  CONSTRAINT [DF_invoice_date]  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [dbo].[pack] ADD  CONSTRAINT [DF_pack_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[pack] ADD  CONSTRAINT [DF_pack_date]  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [dbo].[parameter] ADD  CONSTRAINT [DF_parameter_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[parameter] ADD  CONSTRAINT [DF_parameter_date]  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_date]  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_afecta]  DEFAULT ((0)) FOR [exenta]
GO
ALTER TABLE [dbo].[roles] ADD  CONSTRAINT [DF_roles_state]  DEFAULT ((0)) FOR [state]
GO
ALTER TABLE [dbo].[roles] ADD  CONSTRAINT [DF_roles_actionAdd]  DEFAULT ((0)) FOR [actionAdd]
GO
ALTER TABLE [dbo].[roles] ADD  CONSTRAINT [DF_roles_actionEdit]  DEFAULT ((0)) FOR [actionEdit]
GO
ALTER TABLE [dbo].[roles] ADD  CONSTRAINT [DF_roles_actionDelete]  DEFAULT ((0)) FOR [actionDelete]
GO
ALTER TABLE [dbo].[sale] ADD  CONSTRAINT [DF_sale_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[sale] ADD  CONSTRAINT [DF_sale_date]  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [dbo].[sale] ADD  CONSTRAINT [DF_sale_state]  DEFAULT ((1)) FOR [state]
GO
ALTER TABLE [dbo].[subcategory] ADD  CONSTRAINT [DF_subcategory_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[subcategory] ADD  CONSTRAINT [DF_subcategory_date]  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [dbo].[subcategory] ADD  CONSTRAINT [DF_subcategory_state]  DEFAULT ((1)) FOR [state]
GO
ALTER TABLE [dbo].[tax] ADD  CONSTRAINT [DF_tax_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[tax] ADD  CONSTRAINT [DF_tax_exenta]  DEFAULT ((0)) FOR [exenta]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_date]  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_in_invoice]  DEFAULT ((0)) FOR [in_invoice]
GO
ALTER TABLE [dbo].[sale]  WITH CHECK ADD  CONSTRAINT [FK_sale_users] FOREIGN KEY([id_user])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[sale] CHECK CONSTRAINT [FK_sale_users]
GO
/****** Object:  StoredProcedure [dbo].[backupInventoryDB]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[backupInventoryDB] 
	-- Add the parameters for the stored procedure here
	@folder text
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BACKUP DATABASE SQLTestDB
	TO DISK = @folder
	   WITH FORMAT,
		  MEDIANAME = 'SQLServerBackups',
		  NAME = 'Full Backup of SalesSystemDB';
END
GO
/****** Object:  StoredProcedure [dbo].[cancel_sale]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[cancel_sale]
	-- Add the parameters for the stored procedure here
	@ticket int,
	@userid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @id uniqueidentifier, @before nvarchar(max), @after nvarchar(max), @amountproduct int
	DECLARE @id_pack uniqueidentifier, @id_product uniqueidentifier, @amount int
	DECLARE @id_pack2 uniqueidentifier, @id_product2 uniqueidentifier, @amount2 int

    IF EXISTS (select 1 from sale with(nolock) where ticket = @ticket)
	BEGIN 

		SET @id = (select id from sale with(nolock) where ticket = @ticket)

		SET @before = (select * from sale where id = @id FOR JSON PATH, ROOT('sale'))
		UPDATE sale SET state = 0 where id = @id
		SET @after = (select * from sale where id = @id FOR JSON PATH, ROOT('sale'))
		
		INSERT INTO [dbo].[historical]
				([date]
				,[id_user]
				,[action]
				,[before]
				,[after])
			VALUES
				(GETDATE()
				,@userID
				,'cancel_sale'
				,@before
				,@after)

		SET @before = (select * from detail where id_sale = @id FOR JSON PATH, ROOT('detail'))		
		UPDATE detail SET state = 0 where id_sale = @id
		SET @after = (select * from detail where id_sale = @id FOR JSON PATH, ROOT('detail'))

		INSERT INTO [dbo].[historical]
				([date]
				,[id_user]
				,[action]
				,[before]
				,[after])
			VALUES
				(GETDATE()
				,@userID
				,'cancel_detail'
				,@before
				,@after)

		-- ACTUALIZAR STOCK
		UPDATE Prod
		SET 
			Prod.amount = (Prod.amount + Det.amount)
		FROM product Prod INNER JOIN detail Det ON Prod.id = Det.id_product
		WHERE Det.id_sale = @id

		-- ACTUALIZAR STOCK DE PRODUCTOS POR EXISTENCIA DE PACK
		DECLARE pack_cursor CURSOR FOR 
		SELECT Det.amount, Prod.id_pack, Prod.id FROM product Prod INNER JOIN detail Det ON Prod.id = Det.id_product WHERE Det.id_sale = @id
		OPEN pack_cursor  
		FETCH NEXT FROM pack_cursor INTO @amount, @id_pack, @id_product
		WHILE @@FETCH_STATUS = 0  
		BEGIN  

				DECLARE product_cursor CURSOR FOR 
				SELECT amount, id_pack, id_product FROM pack with(nolock) where id_pack = @id_pack
				OPEN product_cursor  
				FETCH NEXT FROM product_cursor INTO @amount2, @id_pack2, @id_product2
				WHILE @@FETCH_STATUS = 0  
				BEGIN  
					-- Actualiza el stock de los pack
					UPDATE product SET amount = amount + @amount2 WHERE id = @id_product2

					FETCH NEXT FROM product_cursor INTO @amount2, @id_pack2, @id_product2
				END 

				CLOSE product_cursor  
				DEALLOCATE product_cursor 

			  FETCH NEXT FROM pack_cursor INTO @amount, @id_pack, @id_product
		END 

		CLOSE pack_cursor  
		DEALLOCATE pack_cursor 

	END
END
GO
/****** Object:  StoredProcedure [dbo].[create_decrease]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[create_decrease]
	-- Add the parameters for the stored procedure here
	@idProduct uniqueidentifier,
	@amount float = NULL,
	@tax float = NULL,
	@total float = NULL,
	@note nvarchar(1000) = NULL,
	@line nvarchar(4000) = NULL,
	@userID int = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @before nvarchar(max), @after nvarchar(max),@amountProduct float

	SET @amountProduct = (select amount from product where id = @idProduct)

    -- Insert statements for procedure here

	INSERT INTO [dbo].[decrease]([date],[id_product],[amount],[tax],[total],[note],[line])
	VALUES (GETDATE(),@idProduct,@amount,@tax,@total,@note,@line)

	SET @before = ''
	SET @after = (select * from [dbo].[decrease] WHERE [id_product] = @idProduct  FOR JSON PATH, ROOT('decrease'))

	INSERT INTO [dbo].[historical]
			([date]
			,[id_user]
			,[action]
			,[before]
			,[after])
		VALUES
			(GETDATE()
			,@userID
			,'create_decrease'
			,@before
			,@after)

	IF @amountProduct > 0
	BEGIN

		SET @before = (select * from [dbo].product WHERE id = @idProduct FOR JSON PATH, ROOT('product'))
		UPDATE product SET amount = (amount - @amount) WHERE id = @idProduct;
		SET @after = (select * from [dbo].product WHERE id = @idProduct FOR JSON PATH, ROOT('product'))

		INSERT INTO [dbo].[historical]
				([date]
				,[id_user]
				,[action]
				,[before]
				,[after])
			VALUES
				(GETDATE()
				,@userID
				,'create_decrease update amount in product'
				,@before
				,@after)

		SET @before = (select * from [dbo].entries WHERE id_product = @idProduct FOR JSON PATH, ROOT('entries'))
		UPDATE entries SET amount = (amount - @amount) WHERE id = @idProduct;
		SET @after = (select * from [dbo].entries WHERE id_product = @idProduct FOR JSON PATH, ROOT('entries'))
	
		INSERT INTO [dbo].[historical]
				([date]
				,[id_user]
				,[action]
				,[before]
				,[after])
			VALUES
				(GETDATE()
				,@userID
				,'create_decrease update amount in entries'
				,@before
			,@after)
	END
END
GO
/****** Object:  StoredProcedure [dbo].[create_detail]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[create_detail]
	-- Add the parameters for the stored procedure here
	@idSale uniqueidentifier,
	@idProduct uniqueidentifier = null,
	@name nvarchar(500) = NULL,
	@price float = NULL,
	@amount float = NULL,
	@tax float = NULL,
	@total float = NULL,
	@total_tax float = NULL,
	@userID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare 
		@before nvarchar(max), @after nvarchar(max), 
		@amountProduct float, @amountPack int,
		@id_pack uniqueidentifier, @id_product uniqueidentifier, 
		@amount_pack int

	SET @amountProduct = (select amount from product where id = @idProduct)
    -- Insert statements for procedure here

	INSERT INTO [dbo].[detail]([date],[id_sale],[id_product],[product_name],[amount],price,tax,[total],total_tax)
    VALUES (GETDATE(),@idSale,@idProduct,@name,@amount,@price,@tax,@total,@total_tax)

	SET @before = ''
	SET @after = (select * from [dbo].[detail] WHERE id = @idSale and id_product = @idProduct FOR JSON PATH, ROOT('sale'))

	INSERT INTO [dbo].[historical]
			([date]
			,[id_user]
			,[action]
			,[before]
			,[after])
		VALUES
			(GETDATE()
			,@userID
			,'create_detail'
			,@before
			,@after)

	IF @amountProduct > 0
	BEGIN

		SET @before = (select * from [dbo].product WHERE id = @idProduct FOR JSON PATH, ROOT('product'))
		UPDATE product SET amount = (amount - @amount) WHERE id = @idProduct;
		SET @after = (select * from [dbo].product WHERE id = @idProduct FOR JSON PATH, ROOT('product'))

		INSERT INTO [dbo].[historical]
				([date]
				,[id_user]
				,[action]
				,[before]
				,[after])
			VALUES
				(GETDATE()
				,@userID
				,'create_detail update amount in product'
				,@before
				,@after)

		SET @before = (select * from [dbo].entries WHERE id_product = @idProduct FOR JSON PATH, ROOT('entries'))
		UPDATE entries SET amount = (amount - @amount) WHERE id = @idProduct;
		SET @after = (select * from [dbo].entries WHERE id_product = @idProduct FOR JSON PATH, ROOT('entries'))
	
		INSERT INTO [dbo].[historical]
				([date]
				,[id_user]
				,[action]
				,[before]
				,[after])
			VALUES
				(GETDATE()
				,@userID
				,'create_detail update amount in entries'
				,@before
				,@after)

		-- ACTUALIZAR STOCK DE PRODUCTO DESDE UN PACK
		IF EXISTS(SELECT 1 FROM product with(nolock) where id = @idProduct AND isPack = 1)
		BEGIN
			--  Se obtiene el ID del producto a Actualizar
			SET @id_product = (SELECT pp.id_product
			FROM product p
				INNER JOIN pack pp ON p.id_pack = pp.id_pack
			WHERE
				p.id = @idProduct AND isPack = 1)

			-- Cantidad de elementos a descontar
			SET @amountProduct = (SELECT (@amount * pp.amount)
			FROM product p
				INNER JOIN pack pp ON p.id_pack = pp.id_pack
			WHERE
				p.id = @idProduct AND isPack = 1)

			UPDATE product SET amount = amount - @amountProduct WHERE id = @id_product

		END

		-- ACTUALIZAR STOCK DE PACK
		IF EXISTS(SELECT 1 FROM pack with(nolock) where id_product = @idProduct)
		BEGIN
			
			SET @amountProduct = (select amount from product where id = @idProduct)

			DECLARE pack_cursor CURSOR FOR 
			SELECT amount, id_pack, id_product FROM pack with(nolock) where id_product = @idProduct
			OPEN pack_cursor  
			FETCH NEXT FROM pack_cursor INTO @amount_pack, @id_pack, @id_product
			WHILE @@FETCH_STATUS = 0  
			BEGIN  
				-- Actualiza el stock de los pack
				UPDATE product SET amount = ROUND((@amountProduct - @amount)/@amount_pack, 0, 0) WHERE id_pack = @id_pack

				FETCH NEXT FROM pack_cursor INTO @amount_pack, @id_pack, @id_product
			END 

			CLOSE pack_cursor  
			DEALLOCATE pack_cursor 

		END

	END
END
GO
/****** Object:  StoredProcedure [dbo].[create_entry]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[create_entry]
	-- Add the parameters for the stored procedure here
	@productID uniqueidentifier,
	@productName nvarchar(50),
	@amount float,
	@total float,
	@userID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @before nvarchar(max), @after nvarchar(max)

    -- Insert statements for procedure here
	IF EXISTS (select 1 from [dbo].[entries] where id_product = @productID)
	BEGIN
		SET @before = (select * from [dbo].[entries] WHERE id_product = @productID FOR JSON PATH, ROOT('entries'))
		UPDATE [dbo].[entries] SET name_product = @productName, amount = @amount, total = @total, id_user = @userID, state = 1 WHERE id_product = @productID
	END
	ELSE
	BEGIN
		SET @before = ''
		INSERT INTO [dbo].[entries]([date],id_product,name_product,amount,[total],id_user)VALUES(GETDATE(),@productID,@productName,@amount,@total,@userID)
	END

	SET @after = (select * from [dbo].[entries] WHERE id_product = @productID FOR JSON PATH, ROOT('entries'))

	INSERT INTO [dbo].[historical]
			   ([date]
			   ,[id_user]
			   ,[action]
			   ,[before]
			   ,[after])
		 VALUES
			   (GETDATE()
			   ,@userID
			   ,'create_entry'
			   ,@before
			   ,@after)	

	-- before change value
	SET @before = (select * from [dbo].product WHERE id = @productID FOR JSON PATH, ROOT('product'))

	-- Update statements for procedure here
	if(@amount > 0)
	begin
		UPDATE dbo.product SET sale_price = @total, amount = @amount WHERE id = @productID
	end
	else 
	begin 
		UPDATE dbo.product SET sale_price = @total WHERE id = @productID
	end

	SET @after = (select * from [dbo].product WHERE id = @productID FOR JSON PATH, ROOT('product'))

	INSERT INTO [dbo].[historical]
			   ([date]
			   ,[id_user]
			   ,[action]
			   ,[before]
			   ,[after])
		 VALUES
			   (GETDATE()
			   ,@userID
			   ,'update_product'
			   ,@before
			   ,@after)	

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[create_pack]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[create_pack]
	-- Add the parameters for the stored procedure here
	@id uniqueidentifier,
	@id_product uniqueidentifier,
	@barcode nvarchar(500) = NULL,
	@name nvarchar(500) = NULL,
	@amount float = NULL,
	@line nvarchar(1500) = NULL,
	@userID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @before nvarchar(max), @after nvarchar(max)

    -- Insert statements for procedure here

	IF EXISTS (SELECT 1 FROM pack WITH(NOLOCK) WHERE [id_pack] = @id AND [id_product] = @id_product)
	BEGIN

		SET @before = (select * from [dbo].pack WHERE [id_pack] = @id AND [id_product] = @id_product FOR JSON PATH, ROOT('pack'))
		UPDATE [dbo].[pack] SET [bar_code] = @barcode, [name] = @name, [amount] = @amount, [line] = @line WHERE [id_pack] = @id AND [id_product] = @id_product
		SET @after = (select * from [dbo].pack WHERE [id_pack] = @id AND [id_product] = @id_product FOR JSON PATH, ROOT('pack'))
		INSERT INTO [dbo].[historical]
				([date]
				,[id_user]
				,[action]
				,[before]
				,[after])
			VALUES
				(GETDATE()
				,@userID
				,'update_pack'
				,@before
				,@after)

	END
	ELSE
	BEGIN

		SET @before = ''
		INSERT INTO [dbo].[pack]([id_pack],[id_product],[bar_code],[name],[amount],[line],[state])
		VALUES (@id,@id_product,@barcode,@name,@amount,@line,1)
		SET @after = ''--(select * from [dbo].pack WHERE [id_pack] = @id AND [id_product] = @id_product FOR JSON PATH, ROOT('pack'))
		INSERT INTO [dbo].[historical]
				([date]
				,[id_user]
				,[action]
				,[before]
				,[after])
			VALUES
				(GETDATE()
				,@userID
				,'create_pack'
				,@before
				,@after)

	END

	
END
GO
/****** Object:  StoredProcedure [dbo].[create_product]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[create_product]
	-- Add the parameters for the stored procedure here
	@name nvarchar(1000) = NULL, 
	@amount int = NULL,
	@minimum int = NULL,
	@stock int = NULL,
	@price float = NULL,
	@barcode nvarchar(1000) = NULL,
	@userID int = NULL,
	@line nvarchar(3000) = NULL,
	@tax uniqueidentifier = NULL,
	@expiration date = NULL,
	@exenta bit = NULL,
	@category uniqueidentifier = NULL,
	@isPack bit = NULL,
	@idPack uniqueidentifier = NULL,
	@idsubcategory uniqueidentifier = NULL,
	@res int OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @before nvarchar(max), @after nvarchar(max), @id_product uniqueidentifier, @linePack nvarchar(3000)

	IF EXISTS (SELECT 1 FROM [dbo].[product] WITH(NOLOCK) WHERE [bar_code] = @barcode) --and name like '%'+@name+'%' and price = @price)
	BEGIN
		SET @res = 0
	END
	ELSE
	BEGIN

		-- Insert statements for procedure here
		INSERT INTO [dbo].[product](date,[name],[amount],[stock],minimum,sale_price,[price],[bar_code],[line],state,id_tax,exenta,expiration,id_category,isPack,id_pack,id_subcategory)
		VALUES(GETDATE(),@name,@amount,@stock,@minimum,0,@price,@barcode,@line,1,@tax,@exenta,@expiration,@category,@isPack,@idPack,@idsubcategory)

		SET @res = @@ROWCOUNT

		SET @before = ''
		SET @after = (select * from [dbo].[product] WHERE line = @line FOR JSON PATH, ROOT('product'))

		INSERT INTO [dbo].[historical]
			   ([date]
			   ,[id_user]
			   ,[action]
			   ,[before]
			   ,[after])
		 VALUES
			   (GETDATE()
			   ,@userID
			   ,'create_product'
			   ,@before
			   ,@after)	
	END 
END
GO
/****** Object:  StoredProcedure [dbo].[create_sale]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[create_sale]
	-- Add the parameters for the stored procedure here
	@amount float,
	@tax float,
	@total float,
	@notes nvarchar(1000),
	@payment_cash float,
	@payment_other float,
	@change float,
	@userID int,
	@saleID uniqueidentifier output,
	@ticket bigint output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @before nvarchar(max), @after nvarchar(max)

	SET @saleID = NEWID();

    -- Insert statements for procedure here	
	INSERT INTO [dbo].[sale](id,[date],amount,[tax],total,id_user, note,change, payment_cash, payment_other, ticket)
	VALUES(@saleID, GETDATE(), @amount, @tax , @total, @userID, @notes, @change,@payment_cash,@payment_other, 0)

	select @ticket = (MAX(ticket) + 1) from [dbo].[sale];

	UPDATE [dbo].[sale] SET ticket = @ticket WHERE id = @saleID;

	SET @before = ''
	SET @after = (select * from [dbo].[sale] WHERE id = @saleID FOR JSON PATH, ROOT('sale'))

	INSERT INTO [dbo].[historical]
			([date]
			,[id_user]
			,[action]
			,[before]
			,[after])
		VALUES
			(GETDATE()
			,@userID
			,'create_sale'
			,@before
			,@after)	

	--SELECT @saleID
END
GO
/****** Object:  StoredProcedure [dbo].[delete_entry]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[delete_entry]
	-- Add the parameters for the stored procedure here
	@productID uniqueidentifier,
	@userID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @before nvarchar(max), @after nvarchar(max)

	SET @before = (select * from [dbo].[entries] WHERE id_product = @productID FOR JSON PATH, ROOT('entries'))
	-- update
	update [dbo].[entries] set state = 0, total = null WHERE id_product = @productID;
	SET @after = (select * from [dbo].[entries] WHERE id_product = @productID FOR JSON PATH, ROOT('entries'))

	INSERT INTO [dbo].[historical]
			   ([date]
			   ,[id_user]
			   ,[action]
			   ,[before]
			   ,[after])
		 VALUES
			   (GETDATE()
			   ,@userID
			   ,'delete_entry'
			   ,@before
			   ,@after)

	SET @before = (select * from [dbo].product WHERE id = @productID FOR JSON PATH, ROOT('product'))
	-- update
	UPDATE dbo.product SET sale_price = null, amount = null WHERE id = @productID
	SET @after = (select * from [dbo].product WHERE id = @productID FOR JSON PATH, ROOT('product'))

	INSERT INTO [dbo].[historical]
			   ([date]
			   ,[id_user]
			   ,[action]
			   ,[before]
			   ,[after])
		 VALUES
			   (GETDATE()
			   ,@userID
			   ,'delete_entry_update_product'
			   ,@before
			   ,@after)

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[delete_product]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[delete_product]
	-- Add the parameters for the stored procedure here
	@id uniqueidentifier,
	@userID int,
	@line nvarchar(10),
	@res int OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @before nvarchar(max), @after nvarchar(max)

	SET @before = (select * from [dbo].[product] WHERE id = @id FOR JSON PATH, ROOT('product'))
	UPDATE [dbo].[product] SET state = 0 WHERE id = @id;
	select @res = @@ROWCOUNT
	SET @after = (select * from [dbo].[product] WHERE id = @id FOR JSON PATH, ROOT('product'))

	INSERT INTO [dbo].[historical]
           ([date]
           ,[id_user]
           ,[action]
           ,[before]
           ,[after])
     VALUES
           (GETDATE()
           ,@userID
		   ,'delete_product'
           ,@before
           ,@after)

    -- Update statements for procedure here
END
GO
/****** Object:  StoredProcedure [dbo].[update_product]    Script Date: 24-12-2022 0:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[update_product]
	-- Add the parameters for the stored procedure here
	@id uniqueidentifier,
	@name nvarchar(1000), 
	@amount int,
	@minimum int,
	@price float,
	@barcode nvarchar(1000),
	@userID int,
	@line nvarchar(3000),
	@tax uniqueidentifier,
	@expiration date = NULL,
	@exenta bit,
	@category uniqueidentifier = NULL,
	@isPack bit = NULL,
	@id_pack uniqueidentifier = NULL,
	@idsubcategory uniqueidentifier = NULL,
	@res int OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @before nvarchar(max), @after nvarchar(max)

	SET @before = (select * from [dbo].[product] WHERE id = @id FOR JSON PATH, ROOT('product'))

    -- Update statements for procedure here
	UPDATE [dbo].[product] SET 
		[name] = @name, 
		[amount] = @amount, 
		[stock] = @amount, 
		[minimum] = @minimum,
		[price] = @price,
		[bar_code] = @barcode,
		[line] = @line,
		exenta = @exenta,
		id_tax = @tax,
		expiration = @expiration,
		id_category = @category,
		isPack = @isPack,
		id_pack = @id_pack,
		id_subcategory = @idsubcategory 
	WHERE id = @id;

	select @res = @@ROWCOUNT

	SET @after = (select * from [dbo].[product] WHERE id = @id FOR JSON PATH, ROOT('product'))

	INSERT INTO [dbo].[historical]
			([date]
			,[id_user]
			,[action]
			,[before]
			,[after])
		VALUES
			(GETDATE()
			,@userID
			,'update_product'
			,@before
			,@after)	

	declare @id_pack2 uniqueidentifier, @id_product uniqueidentifier, @amount_pack int

	DECLARE pack_cursor CURSOR FOR 
	SELECT amount, id_pack, id_product FROM pack with(nolock) where id_product = @id
	OPEN pack_cursor  
	FETCH NEXT FROM pack_cursor INTO @amount_pack, @id_pack2, @id_product
	WHILE @@FETCH_STATUS = 0  
	BEGIN  
		-- Actualiza el stock de los pack
		UPDATE product SET amount = ROUND(@amount/@amount_pack, 0, 0) WHERE id_pack = @id_pack2

		FETCH NEXT FROM pack_cursor INTO @amount_pack, @id_pack2, @id_product
	END 

	CLOSE pack_cursor  
	DEALLOCATE pack_cursor 
END
GO
USE [master]
GO
ALTER DATABASE [SalesSystemDB] SET  READ_WRITE 
GO
