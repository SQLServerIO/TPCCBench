--USE [master]
--GO
--/****** Object:  Database [tpccdb]    Script Date: 10/13/2009 21:59:24 ******/
--CREATE DATABASE [tpccdb] ON  PRIMARY
----set the location of the datafiles before creating database 
--( NAME = N'tpccdb_dat', FILENAME = N's:\tpccdb.mdf' , SIZE = 11926784KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
-- LOG ON 
--( NAME = N'tpccdb_log', FILENAME = N's:\tpccdb_log.ldf' , SIZE = 26816KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
--GO
--USE [master]
--GO
--ALTER DATABASE [tpccdb] SET RECOVERY SIMPLE WITH NO_WAIT
--GO
USE [tpccdb]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CUSTOMER4]') AND parent_object_id = OBJECT_ID(N'[dbo].[CUSTOMER]'))
ALTER TABLE [dbo].[CUSTOMER] DROP CONSTRAINT [FK_CUSTOMER4]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DISTRICT]') AND parent_object_id = OBJECT_ID(N'[dbo].[DISTRICT]'))
ALTER TABLE [dbo].[DISTRICT] DROP CONSTRAINT [FK_DISTRICT]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_HISTORY]') AND parent_object_id = OBJECT_ID(N'[dbo].[HISTORY]'))
ALTER TABLE [dbo].[HISTORY] DROP CONSTRAINT [FK_HISTORY]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_HISTORY1]') AND parent_object_id = OBJECT_ID(N'[dbo].[HISTORY]'))
ALTER TABLE [dbo].[HISTORY] DROP CONSTRAINT [FK_HISTORY1]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NEW_ORDER]') AND parent_object_id = OBJECT_ID(N'[dbo].[NEW_ORDER]'))
ALTER TABLE [dbo].[NEW_ORDER] DROP CONSTRAINT [FK_NEW_ORDER]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_O_ORDER]') AND parent_object_id = OBJECT_ID(N'[dbo].[O_ORDER]'))
ALTER TABLE [dbo].[O_ORDER] DROP CONSTRAINT [FK_O_ORDER]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__O_ORDER__SOURCE___7F60ED59]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[O_ORDER] DROP CONSTRAINT [DF__O_ORDER__SOURCE___7F60ED59]
END

GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ORDER_LINE]') AND parent_object_id = OBJECT_ID(N'[dbo].[ORDER_LINE]'))
ALTER TABLE [dbo].[ORDER_LINE] DROP CONSTRAINT [FK_ORDER_LINE]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ORDER_LINE1]') AND parent_object_id = OBJECT_ID(N'[dbo].[ORDER_LINE]'))
ALTER TABLE [dbo].[ORDER_LINE] DROP CONSTRAINT [FK_ORDER_LINE1]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_STOCK]') AND parent_object_id = OBJECT_ID(N'[dbo].[STOCK]'))
ALTER TABLE [dbo].[STOCK] DROP CONSTRAINT [FK_STOCK]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_STOCK1]') AND parent_object_id = OBJECT_ID(N'[dbo].[STOCK]'))
ALTER TABLE [dbo].[STOCK] DROP CONSTRAINT [FK_STOCK1]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__HEART__sourc__2A4B4B5E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[HEARTBEAT] DROP CONSTRAINT [DF__HEART__sourc__2A4B4B5E]
END

GO

/****** Object:  Table [dbo].[CUSTOMER]    Script Date: 10/13/2009 22:38:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CUSTOMER]') AND type in (N'U'))
DROP TABLE [dbo].[CUSTOMER]
GO

/****** Object:  Table [dbo].[DISTRICT]    Script Date: 10/13/2009 22:38:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DISTRICT]') AND type in (N'U'))
DROP TABLE [dbo].[DISTRICT]
GO

/****** Object:  Table [dbo].[HISTORY]    Script Date: 10/13/2009 22:38:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HISTORY]') AND type in (N'U'))
DROP TABLE [dbo].[HISTORY]
GO

/****** Object:  Table [dbo].[ITEM]    Script Date: 10/13/2009 22:38:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEM]') AND type in (N'U'))
DROP TABLE [dbo].[ITEM]
GO

/****** Object:  Table [dbo].[NEW_ORDER]    Script Date: 10/13/2009 22:38:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NEW_ORDER]') AND type in (N'U'))
DROP TABLE [dbo].[NEW_ORDER]
GO

/****** Object:  Table [dbo].[O_ORDER]    Script Date: 10/13/2009 22:38:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[O_ORDER]') AND type in (N'U'))
DROP TABLE [dbo].[O_ORDER]
GO

/****** Object:  Table [dbo].[ORDER_LINE]    Script Date: 10/13/2009 22:38:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ORDER_LINE]') AND type in (N'U'))
DROP TABLE [dbo].[ORDER_LINE]
GO

/****** Object:  Table [dbo].[STOCK]    Script Date: 10/13/2009 22:38:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCK]') AND type in (N'U'))
DROP TABLE [dbo].[STOCK]
GO

/****** Object:  Table [dbo].[WAREHOUSE]    Script Date: 10/13/2009 22:38:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WAREHOUSE]') AND type in (N'U'))
DROP TABLE [dbo].[WAREHOUSE]
GO

/****** Object:  Table [dbo].[HEARTBEAT]    Script Date: 10/13/2009 22:59:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HEARTBEAT]') AND type in (N'U'))
DROP TABLE [dbo].[HEARTBEAT]
GO

/****** Object:  StoredProcedure [dbo].[CREATE_NEW_ORDER]    Script Date: 10/13/2009 22:57:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CREATE_NEW_ORDER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CREATE_NEW_ORDER]
GO

/****** Object:  StoredProcedure [dbo].[DELIVERY]    Script Date: 10/13/2009 22:57:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DELIVERY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DELIVERY]
GO

/****** Object:  StoredProcedure [dbo].[ORDER_STATUS]    Script Date: 10/13/2009 22:57:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ORDER_STATUS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ORDER_STATUS]
GO

/****** Object:  StoredProcedure [dbo].[PAYMENT]    Script Date: 10/13/2009 22:57:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PAYMENT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PAYMENT]
GO

/****** Object:  StoredProcedure [dbo].[STOCK_LEVEL]    Script Date: 10/13/2009 22:57:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCK_LEVEL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[STOCK_LEVEL]
GO



/****** Object:  Table [dbo].[WAREHOUSE]    Script Date: 10/13/2009 21:57:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[WAREHOUSE](
	[W_ID] [varchar](20) NOT NULL,
	[W_NAME] [varchar](20) NULL,
	[W_STREET_1] [varchar](20) NULL,
	[W_STREET_2] [varchar](20) NULL,
	[W_CITY] [varchar](20) NULL,
	[W_STATE] [varchar](2) NULL,
	[W_ZIP] [varchar](9) NULL,
	[W_TAX] [numeric](5, 4) NULL,
	[W_YTD] [numeric](13, 2) NULL,
	[SEQ_ID] [varchar](50) NULL,
 CONSTRAINT [PK_WAREHOUSE] PRIMARY KEY CLUSTERED 
(
	[W_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[DISTRICT]    Script Date: 10/13/2009 21:57:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DISTRICT](
	[D_ID] [varchar](20) NOT NULL,
	[D_W_ID] [varchar](20) NOT NULL,
	[D_NAME] [varchar](20) NULL,
	[D_STREET_1] [varchar](20) NULL,
	[D_STREET_2] [varchar](20) NULL,
	[D_CITY] [varchar](20) NULL,
	[D_STATE] [varchar](10) NULL,
	[D_ZIP] [varchar](10) NULL,
	[D_TAX] [numeric](5, 4) NULL,
	[D_YTD] [numeric](13, 2) NULL,
	[D_NEXT_O_ID] [varchar](50) NULL,
	[SEQ_ID] [varchar](50) NULL,
 CONSTRAINT [PK_DISTRICT] PRIMARY KEY CLUSTERED 
(
	[D_W_ID] ASC,
	[D_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[CUSTOMER]    Script Date: 10/13/2009 21:57:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CUSTOMER](
	[C_ID] [varchar](20) NOT NULL,
	[C_D_ID] [varchar](20) NOT NULL,
	[C_W_ID] [varchar](20) NOT NULL,
	[C_FIRST] [varchar](16) NULL,
	[C_MIDDLE] [varchar](2) NULL,
	[C_LAST] [varchar](16) NULL,
	[C_STREET_1] [varchar](25) NULL,
	[C_STREET_2] [varchar](25) NULL,
	[C_CITY] [varchar](25) NULL,
	[C_STATE] [varchar](25) NULL,
	[C_ZIP] [varchar](9) NULL,
	[C_PHONE] [varchar](12) NULL,
	[C_SINCE] [datetime] NULL,
	[C_CREDIT] [varchar](2) NULL,
	[C_CREDIT_LIM] [numeric](13, 2) NULL,
	[C_DISCOUNT] [numeric](5, 4) NULL,
	[C_BALANCE] [numeric](13, 2) NULL,
	[C_YTD_PAYMENT] [numeric](13, 2) NULL,
	[C_PAYMENT_CNT] [numeric](5, 0) NULL,
	[C_DELIVERY_CNT] [numeric](5, 0) NULL,
	[C_DATA] [varchar](500) NULL,
	[SEQ_ID] [varchar](50) NULL,
 CONSTRAINT [PK_CUSTOMER] PRIMARY KEY CLUSTERED 
(
	[C_ID] ASC,
	[C_D_ID] ASC,
	[C_W_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[HEARTBEAT]    Script Date: 10/13/2009 21:57:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HEARTBEAT](
	[id] [bigint] NOT NULL,
	[source_time] [datetime] NULL,
 CONSTRAINT [PK_HEARTBEAT] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[HISTORY]    Script Date: 10/13/2009 21:57:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[HISTORY](
	[H_C_ID] [varchar](20) NOT NULL,
	[H_C_D_ID] [varchar](20) NOT NULL,
	[H_C_W_ID] [varchar](20) NOT NULL,
	[H_D_ID] [varchar](20) NOT NULL,
	[H_W_ID] [varchar](20) NOT NULL,
	[H_DATE] [datetime] NULL,
	[H_AMOUNT] [numeric](7, 2) NULL,
	[H_DATA] [varchar](24) NULL,
	[SEQ_ID] [varchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[ITEM]    Script Date: 10/13/2009 21:57:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ITEM](
	[I_ID] [varchar](20) NOT NULL,
	[I_IM_ID] [varchar](20) NULL,
	[I_NAME] [varchar](24) NULL,
	[I_PRICE] [numeric](6, 2) NULL,
	[I_DATA] [varchar](50) NULL,
 CONSTRAINT [PK_ITEM] PRIMARY KEY CLUSTERED 
(
	[I_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[O_ORDER]    Script Date: 10/13/2009 21:57:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[O_ORDER](
	[O_ID] [int] NOT NULL,
	[O_D_ID] [varchar](20) NOT NULL,
	[O_W_ID] [varchar](20) NOT NULL,
	[O_C_ID] [varchar](20) NOT NULL,
	[O_ENTRY_D] [datetime] NULL,
	[O_CARRIER_ID] [varchar](15) NULL,
	[O_OL_CNT] [numeric](11, 0) NULL,
	[O_ALL_LOCAL] [numeric](6, 1) NULL,
	[SEQ_ID] [varchar](50) NULL,
	[SOURCE_TIME] [datetime] NULL,
 CONSTRAINT [PK_O_ORDER] PRIMARY KEY CLUSTERED 
(
	[O_W_ID] ASC,
	[O_D_ID] ASC,
	[O_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[NEW_ORDER]    Script Date: 10/13/2009 21:57:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[NEW_ORDER](
	[NO_O_ID] [int] NOT NULL,
	[NO_D_ID] [varchar](20) NOT NULL,
	[NO_W_ID] [varchar](20) NOT NULL,
	[SEQ_ID] [varchar](50) NULL,
 CONSTRAINT [PK_NEW_ORDER] PRIMARY KEY CLUSTERED 
(
	[NO_O_ID] ASC,
	[NO_D_ID] ASC,
	[NO_W_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


/****** Object:  Index [IDX_NEW_ORDER__K3_K2_K1]    Script Date: 10/13/2009 21:57:25 ******/
CREATE NONCLUSTERED INDEX [IDX_NEW_ORDER__K3_K2_K1] ON [dbo].[NEW_ORDER] 
(
	[NO_W_ID] ASC,
	[NO_D_ID] ASC,
	[NO_O_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[STOCK]    Script Date: 10/13/2009 21:57:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[STOCK](
	[S_I_ID] [varchar](20) NOT NULL,
	[S_W_ID] [varchar](20) NOT NULL,
	[S_QUANTITY] [numeric](5, 0) NULL,
	[S_DIST_01] [varchar](24) NULL,
	[S_DIST_02] [varchar](24) NULL,
	[S_DIST_03] [varchar](24) NULL,
	[S_DIST_04] [varchar](24) NULL,
	[S_DIST_05] [varchar](24) NULL,
	[S_DIST_06] [varchar](24) NULL,
	[S_DIST_07] [varchar](24) NULL,
	[S_DIST_08] [varchar](24) NULL,
	[S_DIST_09] [varchar](24) NULL,
	[S_DIST_10] [varchar](24) NULL,
	[S_YTD] [numeric](8, 0) NULL,
	[S_ORDER_CNT] [numeric](4, 0) NULL,
	[S_REMOTE_CNT] [numeric](4, 0) NULL,
	[S_DATA] [varchar](50) NULL,
	[SEQ_ID] [varchar](50) NULL,
 CONSTRAINT [PK_STOCK] PRIMARY KEY CLUSTERED 
(
	[S_W_ID] ASC,
	[S_I_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


/****** Object:  Index [IDX_STOCK__K2]    Script Date: 10/13/2009 21:57:25 ******/
CREATE NONCLUSTERED INDEX [IDX_STOCK__K2] ON [dbo].[STOCK] 
(
	[S_W_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ORDER_LINE]    Script Date: 10/13/2009 21:57:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ORDER_LINE](
	[OL_O_ID] [int] NOT NULL,
	[OL_D_ID] [varchar](20) NOT NULL,
	[OL_W_ID] [varchar](20) NOT NULL,
	[OL_NUMBER] [varchar](20) NOT NULL,
	[OL_I_ID] [varchar](20) NULL,
	[OL_SUPPLY_W_ID] [varchar](20) NULL,
	[OL_DELIVERY_D] [datetime] NULL,
	[OL_QUANTITY] [numeric](3, 0) NULL,
	[OL_AMOUNT] [numeric](7, 2) NULL,
	[OL_DIST_INFO] [varchar](24) NULL,
	[SEQ_ID] [varchar](50) NULL,
 CONSTRAINT [PK_ORDER_LINE] PRIMARY KEY CLUSTERED 
(
	[OL_W_ID] ASC,
	[OL_D_ID] ASC,
	[OL_O_ID] ASC,
	[OL_NUMBER] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


/****** Object:  Index [IDX_ORDER_LINE_K3_K1_K2_K9]    Script Date: 10/13/2009 21:57:25 ******/
CREATE NONCLUSTERED INDEX [IDX_ORDER_LINE_K3_K1_K2_K9] ON [dbo].[ORDER_LINE] 
(
	[OL_W_ID] ASC,
	[OL_O_ID] ASC,
	[OL_D_ID] ASC,
	[OL_AMOUNT] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DISTRICT]  WITH CHECK ADD  CONSTRAINT [FK_DISTRICT] FOREIGN KEY([D_W_ID])
REFERENCES [dbo].[WAREHOUSE] ([W_ID])
GO

ALTER TABLE [dbo].[DISTRICT] CHECK CONSTRAINT [FK_DISTRICT]
GO

ALTER TABLE [dbo].[CUSTOMER]  WITH CHECK ADD  CONSTRAINT [FK_CUSTOMER4] FOREIGN KEY([C_W_ID], [C_D_ID])
REFERENCES [dbo].[DISTRICT] ([D_W_ID], [D_ID])
GO

ALTER TABLE [dbo].[CUSTOMER] CHECK CONSTRAINT [FK_CUSTOMER4]
GO

ALTER TABLE [dbo].[HEARTBEAT] ADD  CONSTRAINT [DF__GGS_HEART__sourc__2A4B4B5E]  DEFAULT (getdate()) FOR [source_time]
GO

ALTER TABLE [dbo].[HISTORY]  WITH CHECK ADD  CONSTRAINT [FK_HISTORY] FOREIGN KEY([H_C_ID], [H_C_D_ID], [H_C_W_ID])
REFERENCES [dbo].[CUSTOMER] ([C_ID], [C_D_ID], [C_W_ID])
GO

ALTER TABLE [dbo].[HISTORY] CHECK CONSTRAINT [FK_HISTORY]
GO

ALTER TABLE [dbo].[HISTORY]  WITH CHECK ADD  CONSTRAINT [FK_HISTORY1] FOREIGN KEY([H_W_ID], [H_D_ID])
REFERENCES [dbo].[DISTRICT] ([D_W_ID], [D_ID])
GO

ALTER TABLE [dbo].[HISTORY] CHECK CONSTRAINT [FK_HISTORY1]
GO

ALTER TABLE [dbo].[O_ORDER]  WITH CHECK ADD  CONSTRAINT [FK_O_ORDER] FOREIGN KEY([O_C_ID], [O_D_ID], [O_W_ID])
REFERENCES [dbo].[CUSTOMER] ([C_ID], [C_D_ID], [C_W_ID])
GO

ALTER TABLE [dbo].[O_ORDER] CHECK CONSTRAINT [FK_O_ORDER]
GO

ALTER TABLE [dbo].[O_ORDER] ADD  CONSTRAINT [DF__O_ORDER__SOURCE___7F60ED59]  DEFAULT (getdate()) FOR [SOURCE_TIME]
GO

ALTER TABLE [dbo].[NEW_ORDER]  WITH CHECK ADD  CONSTRAINT [FK_NEW_ORDER] FOREIGN KEY([NO_W_ID], [NO_D_ID], [NO_O_ID])
REFERENCES [dbo].[O_ORDER] ([O_W_ID], [O_D_ID], [O_ID])
GO

ALTER TABLE [dbo].[NEW_ORDER] CHECK CONSTRAINT [FK_NEW_ORDER]
GO

ALTER TABLE [dbo].[STOCK]  WITH CHECK ADD  CONSTRAINT [FK_STOCK] FOREIGN KEY([S_W_ID])
REFERENCES [dbo].[WAREHOUSE] ([W_ID])
GO

ALTER TABLE [dbo].[STOCK] CHECK CONSTRAINT [FK_STOCK]
GO

ALTER TABLE [dbo].[STOCK]  WITH CHECK ADD  CONSTRAINT [FK_STOCK1] FOREIGN KEY([S_I_ID])
REFERENCES [dbo].[ITEM] ([I_ID])
GO

ALTER TABLE [dbo].[STOCK] CHECK CONSTRAINT [FK_STOCK1]
GO

ALTER TABLE [dbo].[ORDER_LINE]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_LINE] FOREIGN KEY([OL_W_ID], [OL_D_ID], [OL_O_ID])
REFERENCES [dbo].[O_ORDER] ([O_W_ID], [O_D_ID], [O_ID])
GO

ALTER TABLE [dbo].[ORDER_LINE] CHECK CONSTRAINT [FK_ORDER_LINE]
GO

ALTER TABLE [dbo].[ORDER_LINE]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_LINE1] FOREIGN KEY([OL_SUPPLY_W_ID], [OL_I_ID])
REFERENCES [dbo].[STOCK] ([S_W_ID], [S_I_ID])
GO

ALTER TABLE [dbo].[ORDER_LINE] CHECK CONSTRAINT [FK_ORDER_LINE1]
GO


/****** Object:  StoredProcedure [dbo].[CREATE_NEW_ORDER]    Script Date: 10/13/2009 21:58:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[CREATE_NEW_ORDER]
	@CARID                                    VARCHAR(4000) ,
	@VO_ALL_LOCAL                             FLOAT ,
	@VOL_NUMBER                               VARCHAR(4000),
	@WHID										int,
	@Auto_Number				  int
    
AS 
	BEGIN

		SET NOCOUNT ON
		
		DECLARE @VS_DIST_01                               VARCHAR(20) 
		DECLARE @VS_DIST_02                               VARCHAR(20) 
		DECLARE @VS_DIST_03                               VARCHAR(20) 
		DECLARE @VS_DIST_04                               VARCHAR(20) 
		DECLARE @VS_DIST_05                               VARCHAR(20) 
		DECLARE @VS_DIST_06                               VARCHAR(20) 
		DECLARE @VS_DIST_07                               VARCHAR(20) 
		DECLARE @VS_DIST_08                               VARCHAR(20) 
		DECLARE @VS_DIST_09                               VARCHAR(20) 
		DECLARE @VS_DIST_10                               VARCHAR(20) 
		DECLARE @VS_QUANTITY                              VARCHAR(20) 
		DECLARE @VS_DATA                                  VARCHAR(20) 
		DECLARE @VS_YTD                                   FLOAT 
		DECLARE @VS_ORDER_CNT                             FLOAT 
		DECLARE @VSTO_ID                                  int 
		DECLARE @VDIS_ID                                  int 
		DECLARE @VC_DISCOUNT                              FLOAT 
		DECLARE @VC_LAST                                  VARCHAR(20) 
		DECLARE @VC_CREDIT                                VARCHAR(10) 
		DECLARE @VW_TAX                                   FLOAT 
		DECLARE @VD_TAX                                   FLOAT 
		DECLARE @VD_NEXT_O_ID                             FLOAT 
		DECLARE @VO_ID                                    FLOAT 
		DECLARE @VO_OL_CNT                                FLOAT 
		DECLARE @VOL_AMOUNT                               FLOAT 
		DECLARE @VTAX_AND_DISCOUNT                        FLOAT 
		DECLARE @VPRICE                                   FLOAT 
		DECLARE @VI_ID                                    FLOAT 
		DECLARE @VOL_QUANTITY                             FLOAT 
		DECLARE @VC_ID                                    VARCHAR(20) 
		DECLARE @VW_ID                                    VARCHAR(5) 
		DECLARE @VD_ID                                    VARCHAR(20) 
		DECLARE @VRD_ID                                   FLOAT 
		DECLARE @VRW_ID                                   FLOAT 
		DECLARE @VNEWORD_ID                               int 
		DECLARE @VOORD_ID                                 int 
		DECLARE @VORDLINE_ID                              int 
        
		/* Generating Random Values */
		DECLARE @i  FLOAT 
		DECLARE @innercounter INTEGER
		DECLARE @RandomNumber float
		DECLARE @RandomInteger int
		DECLARE @MaxValue int
		DECLARE @MinValue int
		DECLARE @innercounter1 INTEGER
		DECLARE @RandomNumber1 float
		DECLARE @RandomInteger1 int
		DECLARE @MaxValue1 int
		DECLARE @MinValue1 int
		DECLARE @innercounter2 INTEGER
		DECLARE @RandomNumber2 float
		DECLARE @RandomInteger2 int
		DECLARE @MaxValue2 int
		DECLARE @MinValue2 int
		DECLARE @innercounter3 int
		DECLARE @RandomNumber3 float
		DECLARE @RandomInteger3 int
		DECLARE @MaxValue3 int
		DECLARE @MinValue3 int

		DECLARE @innercounter4 INTEGER
		DECLARE @RandomNumber4 float
		DECLARE @RandomInteger4 int
		DECLARE @MaxValue4 int
		DECLARE @MinValue4 int
		
		--SELECT @VSTO_ID =100001
		--SELECT @VDIS_ID=100001
		--SELECT @VNEWORD_ID=100001
		--SELECT @VOORD_ID=100001
		--SELECT @VORDLINE_ID=100001

DECLARE @RandomNumber5 float
DECLARE @RandomInteger5 int
DECLARE @MaxValue5 int
DECLARE @MinValue5 int


DECLARE @RandomNumber6 float
DECLARE @RandomInteger6 int
DECLARE @MaxValue6 int
DECLARE @MinValue6 int

DECLARE @RandomNumber7 float
DECLARE @RandomInteger7 int
DECLARE @MaxValue7 int
DECLARE @MinValue7 int

DECLARE @RandomNumber8 float
DECLARE @RandomInteger8 int
DECLARE @MaxValue8 int
DECLARE @MinValue8 int

DECLARE @RandomNumber9 float
DECLARE @RandomInteger9 int
DECLARE @MaxValue9 int
DECLARE @MinValue9 int


SET @MaxValue = 500000
SET @MinValue = 100001
SELECT @RandomNumber5 = RAND()
SELECT @RandomInteger5 = ((@MaxValue + 1) - @MinValue) * @RandomNumber5 + @MinValue



SET @MaxValue = 500000
SET @MinValue = 100001
SELECT @RandomNumber6 = RAND()
SELECT @RandomInteger6 = ((@MaxValue + 1) - @MinValue) * @RandomNumber6 + @MinValue


SET @MaxValue = 500000
SET @MinValue = 100001
SELECT @RandomNumber7 = RAND()
SELECT @RandomInteger7 = ((@MaxValue + 1) - @MinValue) * @RandomNumber7 + @MinValue


SET @MaxValue = 500000
SET @MinValue = 100001
SELECT @RandomNumber8 = RAND()
SELECT @RandomInteger8 = ((@MaxValue + 1) - @MinValue) * @RandomNumber8 + @MinValue


SET @MaxValue = 500000
SET @MinValue = 100001
SELECT @RandomNumber9 = RAND()
SELECT @RandomInteger9 = ((@MaxValue + 1) - @MinValue) * @RandomNumber9 + @MinValue


	SELECT @VSTO_ID =@RandomInteger5
		SELECT @VDIS_ID=@RandomInteger6
		SELECT @VNEWORD_ID=@RandomInteger7
		SELECT @VOORD_ID=@RandomInteger8
		SELECT @VORDLINE_ID=@RandomInteger9


		
			SET @MaxValue = @WHID
			SET @MinValue = 1
			SELECT @RandomNumber = RAND()

			SELECT @RandomInteger = ((@MaxValue + 1) - @MinValue) * @RandomNumber + @MinValue

			SET @MaxValue1 = 10
			SET @MinValue1 = 1
			SELECT @RandomNumber1 = RAND()

			SELECT @RandomInteger1 = ((@MaxValue1 + 1) - @MinValue1) * @RandomNumber1 + @MinValue1

			SET @MaxValue2 = 100000
			SET @MinValue2 = 1
			SELECT @RandomNumber2 = RAND()

			SELECT @RandomInteger2 = ((@MaxValue2 + 1) - @MinValue2) * @RandomNumber2 + @MinValue2

			SET @MaxValue3 = 4
			SET @MinValue3 = 1
			SELECT @RandomNumber3 = RAND()

			SELECT @RandomInteger3 = ((@MaxValue3 + 1) - @MinValue3) * @RandomNumber3 + @MinValue3

			SET @MaxValue4 = 3000
			SET @MinValue4 = 1
			SELECT @RandomNumber4 = RAND()

			SELECT @RandomInteger4 = ((@MaxValue4 + 1) - @MinValue4) * @RandomNumber4 + @MinValue4
  
		SELECT @VRW_ID  = @RandomInteger
		SELECT @VW_ID  = 'W_' + cast(@VRW_ID  as varchar)
		SELECT @VRD_ID  = @RandomInteger1
		SELECT @VD_ID  = 'D_W' + cast(@VRW_ID as varchar) + '_' + cast(@VRD_ID  as varchar)
		SELECT @VC_ID  = 'C_W' + cast(@VRW_ID as varchar) + '_D' +cast(@VRD_ID as varchar) + '_' + cast(@RandomInteger4 as varchar)
		SELECT @VI_ID  = @RandomInteger2
		SELECT @VOL_QUANTITY  = @RandomInteger3
		SELECT @VO_OL_CNT  = 1 
		/* Selecting the Sequence Value */
		
	
		SELECT  @VS_DIST_01  =  S_DIST_01,
				 @VS_DIST_02  =  S_DIST_02,
				 @VS_DIST_03  =  S_DIST_03,
				 @VS_DIST_04  =  S_DIST_04,
				 @VS_DIST_05  =  S_DIST_05,
				 @VS_DIST_06  =  S_DIST_06,
				 @VS_DIST_07  =  S_DIST_07,
				 @VS_DIST_08  =  S_DIST_08,
				 @VS_DIST_09  =  S_DIST_09,
				 @VS_DIST_10  =  S_DIST_10,
				 @VS_QUANTITY  =  S_QUANTITY,
				 @VS_DATA  =  S_DATA,
				 @VS_YTD  =  S_YTD,
				 @VS_ORDER_CNT  =  S_ORDER_CNT
		FROM  stock 
		WHERE	 S_I_ID  = @VI_ID
		 AND	S_W_ID  = @VW_ID
		
		IF ( @VS_QUANTITY < @VOL_QUANTITY ) 
		BEGIN 
			SELECT @VS_QUANTITY  = @VS_QUANTITY - @VOL_QUANTITY + 91 
		END
		ELSE
		BEGIN 
			SELECT @VS_QUANTITY  = @VS_QUANTITY - @VOL_QUANTITY 
		END
   
		/* Updating the Sotck Table */
		UPDATE  STOCK   
		SET	S_QUANTITY = @VS_QUANTITY,	
		    S_ORDER_CNT = (@VS_ORDER_CNT + 1),	
		    SEQ_ID = @VSTO_ID  
		WHERE  S_I_ID  = @VI_ID
		 AND	S_W_ID  = @VW_ID 
		
		
		-- IMPLICIT_TRANSACTIONS is set to OFF
		/* Selecting the Custome Table Details */
		SELECT
				 @VC_LAST  =  C_LAST,
				 @VC_CREDIT  =  C_CREDIT,
				 @VC_DISCOUNT  =  C_DISCOUNT
		FROM  CUSTOMER 
		WHERE	 C_W_ID  = @VW_ID
		 AND	C_D_ID  = @VD_ID
		 AND	C_ID  = @VC_ID
		
		/* Selecting the District Table Details */
		SELECT
				 @VD_TAX  =  D_TAX,
				 @VD_NEXT_O_ID  =  D_NEXT_O_ID
		FROM  DISTRICT 
		WHERE	 D_W_ID  = @VW_ID
		 AND	D_ID  = @VD_ID
		
		/* Updating the District Table */
		UPDATE  DISTRICT   
		SET	D_NEXT_O_ID = (@VD_NEXT_O_ID + 1),	
		    SEQ_ID = @VDIS_ID  
		WHERE  D_W_ID  = @VW_ID
		 AND	D_ID  = @VD_ID 
		
		
		-- IMPLICIT_TRANSACTIONS is set to OFF
		/* Inserting into Order Table */
		INSERT INTO  O_ORDER   
				(  
				O_ID ,
				O_D_ID , 
				O_W_ID , 
				O_C_ID , 
				O_ENTRY_D , 
				O_OL_CNT , 
				O_ALL_LOCAL , 
				SEQ_ID )  
		 VALUES 		(   
				@Auto_Number,
				@VD_ID , 
				@VW_ID , 
				@VC_ID , 
				getdate() , 
				@VO_OL_CNT , 
				@VO_ALL_LOCAL , 
				@VOORD_ID )  
		
		
		-- IMPLICIT_TRANSACTIONS is set to OFF
		/* Inserting into New Order Table */
		INSERT INTO  NEW_ORDER   
				(  
				NO_O_ID ,
				NO_D_ID , 
				NO_W_ID , 
				SEQ_ID )  
		 VALUES 		(  
				@Auto_Number,
				@VD_ID , 
				@VW_ID , 
				@VNEWORD_ID )  
		
		
		-- IMPLICIT_TRANSACTIONS is set to OFF
		/* Selecting the details of Warehouse Table */
		SELECT @VW_TAX  =  W_TAX
		FROM  WAREHOUSE 
		WHERE	 W_ID  = @VW_ID
		
		/* Selecting the details of Item Table */
		SELECT @VPRICE  =  I_PRICE
		FROM  ITEM 
		WHERE	 I_ID  = @VI_ID
		
		/* Calculating the Discount Details */
		
		SELECT @VTAX_AND_DISCOUNT  = ( 1 + @VD_TAX + @VW_TAX ) * ( 1 - @VC_DISCOUNT ) 
		/* Calculating the Total Amount for the Order placed */
		
		SELECT @VOL_AMOUNT  = ( @VPRICE * @VOL_QUANTITY ) / @VTAX_AND_DISCOUNT 
		/* Inserting into Orde Line Table */
		INSERT INTO  ORDER_LINE   
				(  
				OL_O_ID ,
				OL_D_ID , 
				OL_W_ID , 
				OL_NUMBER , 
				OL_I_ID , 
				OL_SUPPLY_W_ID , 
				OL_QUANTITY , 
				OL_AMOUNT , 
				SEQ_ID )  
		 VALUES 		( 
				@Auto_Number,
				@VD_ID , 
				@VW_ID , 
				@VOL_NUMBER , 
				@VI_ID , 
				@VW_ID , 
				@VOL_QUANTITY , 
				@VOL_AMOUNT , 
				@VORDLINE_ID )  
		
		
		-- IMPLICIT_TRANSACTIONS is set to OFF

		SET NOCOUNT OFF

	END



GO

/****** Object:  StoredProcedure [dbo].[DELIVERY]    Script Date: 10/13/2009 21:58:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-----------------exec order_status 'W_1','D_W1_2','C_W1_D2_1'---------------------

CREATE PROCEDURE [dbo].[DELIVERY]
	@VW_ID                                    VARCHAR(4000) ,
	@VD_ID                                    VARCHAR(4000) 
AS 
	BEGIN
		SET NOCOUNT ON
		
		DECLARE @VO_C_ID                                  VARCHAR(20) 
		DECLARE @VNO_O_ID                                 VARCHAR(20) 
		DECLARE @VO_CARRIER_ID                            VARCHAR(20) 
		DECLARE @OL_TOTAL                                 FLOAT 
		DECLARE @VC_BALANCE                               FLOAT 
		DECLARE @VC_DELIVERY_CNT                          FLOAT 
		DECLARE @VCUS_ID                                  VARCHAR(50) 
		DECLARE @VOORD_ID                                 NUMERIC(35) 
		DECLARE @VORDLINE_ID                              NUMERIC(35) 

		DECLARE RS_NO_DET CURSOR LOCAL FOR 
		SELECT NO_O_ID
		FROM  NEW_ORDER 
		WHERE	 NO_W_ID  = @VW_ID
		 AND	NO_D_ID  = @VD_ID
		ORDER BY NO_O_ID DESC 
		
		
		OPEN RS_NO_DET 
		DECLARE @count		 INT 
		SELECT @count = 1 
		WHILE (0 = 0) 
		BEGIN --( 
			
			FETCH NEXT FROM RS_NO_DET INTO 
								@VNO_O_ID
			IF (@@FETCH_STATUS = -1) 
			BREAK

			SELECT @VORDLINE_ID  = 100001 
			SELECT @VOORD_ID  = 100001 
			SELECT @VCUS_ID  = 1 
			DELETE FROM   NEW_ORDER    
			WHERE  NO_O_ID  = @VNO_O_ID
			 AND	NO_D_ID  = @VD_ID
			 AND	NO_W_ID  = @VW_ID 
			
			SELECT
					 @VO_C_ID  =  O_C_ID,
					 @VO_CARRIER_ID  =  O_CARRIER_ID
			FROM  O_ORDER 
			WHERE	 O_ID  = @VNO_O_ID
			 AND	O_D_ID  = @VD_ID
			 AND	O_W_ID  = @VW_ID
			
			UPDATE  O_ORDER   
			SET	O_CARRIER_ID = @VO_CARRIER_ID,	
			    SEQ_ID = @VOORD_ID + 1 
			WHERE  O_ID  = @VNO_O_ID
			 AND	O_D_ID  = @VD_ID
			 AND	O_W_ID  = @VW_ID 
			
			
			-- IMPLICIT_TRANSACTIONS is set to OFF
			SELECT @OL_TOTAL  =  SUM(CONVERT(FLOAT, OL_AMOUNT))
			FROM  ORDER_LINE 
			WHERE	 OL_O_ID  = @VNO_O_ID
			 AND	OL_D_ID  = @VD_ID
			 AND	OL_W_ID  = @VW_ID
			
			UPDATE  ORDER_LINE   
			SET	OL_DELIVERY_D =getdate(),	
			    SEQ_ID = @VORDLINE_ID + 1 
			WHERE  OL_O_ID  = @VNO_O_ID
			 AND	OL_D_ID  = @VD_ID
			 AND	OL_W_ID  = @VW_ID 
			
			
			-- IMPLICIT_TRANSACTIONS is set to OFF
			SELECT
					 @VC_BALANCE  =  C_BALANCE,
					 @VC_DELIVERY_CNT  =  C_DELIVERY_CNT
			FROM  CUSTOMER 
			WHERE	 C_W_ID  = @VW_ID
			 AND	C_D_ID  = @VD_ID
			 AND	C_ID  = @VO_C_ID
			
			UPDATE  CUSTOMER   
			SET	C_BALANCE = @VC_BALANCE + @OL_TOTAL,	
			    C_DELIVERY_CNT = @VC_DELIVERY_CNT + 1,	
			    SEQ_ID = @VCUS_ID + 1 
			WHERE  C_ID  = @VO_C_ID
			 AND	C_D_ID  = @VD_ID
			 AND	C_W_ID  = @VW_ID 
			
			
			-- IMPLICIT_TRANSACTIONS is set to OFF
			SELECT @count=@count +1
		END --) 
		
 DEALLOCATE RS_NO_DET

		SET NOCOUNT OFF

	END




GO

/****** Object:  StoredProcedure [dbo].[ORDER_STATUS]    Script Date: 10/13/2009 21:58:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 

---------------------------------------------exec STOCK_LEVEL 'D_W2_4','W_2'

--------------------------------------


CREATE PROCEDURE [dbo].[ORDER_STATUS]
	@VW_ID                                    VARCHAR(4000) ,
	@VD_ID                                    VARCHAR(4000) ,
	@VC_ID                                    VARCHAR(4000) 
AS 
	BEGIN
		SET NOCOUNT ON
		
		DECLARE @VO_ID                                    VARCHAR(20) 
		DECLARE @VO_CARRIER_ID                            VARCHAR(20) 
		DECLARE @VO_ENTRY_D                               DATETIME 
		DECLARE @VOL_I_ID                                 VARCHAR(20) 
		DECLARE @VOL_SUPPLY_W_ID                          VARCHAR(20) 
		DECLARE @VOL_QUANTITY                             FLOAT 
		DECLARE @VOL_AMOUNT                               FLOAT 
		DECLARE @VOL_DELIVERY_D                           DATETIME 
		DECLARE @VC_BALANCE                               FLOAT 
		DECLARE @VC_FIRST                                 VARCHAR(20) 
		DECLARE @VC_MIDDLE                                VARCHAR(20) 
		DECLARE @VC_LAST                                  VARCHAR(20) 

		DECLARE RS_OR_DET CURSOR LOCAL FOR 
		SELECT
				 O_ID,
				 O_CARRIER_ID,
				 O_ENTRY_D
		FROM  O_ORDER 
		WHERE	 O_W_ID  = @VW_ID
		 AND	O_D_ID  = @VD_ID
		 AND	O_C_ID  = @VC_ID
		ORDER BY O_W_ID DESC,
			 O_D_ID DESC,
			 O_C_ID DESC,
			 O_ID DESC 
		
		SELECT
				 @VC_BALANCE  =  C_BALANCE,
				 @VC_FIRST  =  C_FIRST,
				 @VC_MIDDLE  =  C_MIDDLE,
				 @VC_LAST  =  C_LAST
		FROM  CUSTOMER 
		WHERE	 C_W_ID  = @VW_ID
		 AND	C_D_ID  = @VD_ID
		 AND	C_ID  = @VC_ID
		
		
		OPEN RS_OR_DET 
		DECLARE @count		 INT 
		SELECT @count = 1 
		WHILE (0 = 0) 
		BEGIN --( 
			
			FETCH NEXT FROM RS_OR_DET INTO 
								@VO_ID, @VO_CARRIER_ID, @VO_ENTRY_D
			IF (@@FETCH_STATUS = -1) 
			BREAK

			SELECT
					 @VOL_I_ID  =  OL_I_ID,
					 @VOL_SUPPLY_W_ID  =  OL_SUPPLY_W_ID,
					 @VOL_QUANTITY  =  OL_QUANTITY,
					 @VOL_AMOUNT  =  OL_AMOUNT,
					 @VOL_DELIVERY_D  =  OL_DELIVERY_D
			FROM  ORDER_LINE 
			WHERE	 OL_W_ID  = @VW_ID
			 AND	OL_D_ID  = @VD_ID
			 AND	OL_O_ID  = @VO_ID
			
			SELECT @count=@count +1
		END --) 
		
 DEALLOCATE RS_OR_DET

		SET NOCOUNT OFF

	END




GO

/****** Object:  StoredProcedure [dbo].[PAYMENT]    Script Date: 10/13/2009 21:58:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 

---------------------------------exec delivery 'W_1','D_W1_8'


CREATE PROCEDURE [dbo].[PAYMENT]
	@VW_ID                                    VARCHAR(4000) ,
	@VD_ID                                    VARCHAR(4000) ,
	@VC_ID                                    VARCHAR(4000) ,
	@VC1_LAST                                 VARCHAR(4000) ,
	@VH_AMOUNT                                FLOAT 
AS 
	BEGIN
		SET NOCOUNT ON
		
		DECLARE @VH_DATA                                  VARCHAR(50) 
		DECLARE @NAMECNT                                  FLOAT 
		DECLARE @N                                        FLOAT 
		DECLARE @VCC_ID                                   VARCHAR(20) 
		DECLARE @VCUS_ID                                  VARCHAR(20) 
		DECLARE @VDIS_ID                                  VARCHAR(20) 
		DECLARE @VWAR_ID                                  VARCHAR(20) 
		DECLARE @VHIS_ID                                  VARCHAR(20) 
		DECLARE @VW_NAME                                  VARCHAR(20) 
		DECLARE @VW_STREET_1                              VARCHAR(20) 
		DECLARE @VW_STREET_2                              VARCHAR(20) 
		DECLARE @VW_CITY                                  VARCHAR(20) 
		DECLARE @VW_STATE                                 VARCHAR(20) 
		DECLARE @VW_ZIP                                   NUMERIC(15) 
		DECLARE @VW_YTD                                   NUMERIC(15) 
		DECLARE @VD_STREET_1                              VARCHAR(20) 
		DECLARE @VD_STREET_2                              VARCHAR(20) 
		DECLARE @VD_CITY                                  VARCHAR(20) 
		DECLARE @VD_STATE                                 VARCHAR(20) 
		DECLARE @VD_ZIP                                   VARCHAR(20) 
		DECLARE @VD_NAME                                  VARCHAR(20) 
		DECLARE @VD_YTD                                   NUMERIC(15) 
		DECLARE @VC_FIRST                                 VARCHAR(20) 
		DECLARE @VC_LAST                                  VARCHAR(20) 
		DECLARE @VC_MIDDLE                                VARCHAR(20) 
		DECLARE @VC_STREET_1                              VARCHAR(20) 
		DECLARE @VC_STDDLE                                VARCHAR(20) 
		DECLARE @VC_STREET_2                              VARCHAR(20) 
		DECLARE @VC_CITY                                  VARCHAR(20) 
		DECLARE @VC_STATE                                 VARCHAR(20) 
		DECLARE @VC_ZIP                                   VARCHAR(20) 
		DECLARE @VC_PHONE                                 VARCHAR(20) 
		DECLARE @VC_SINCE                                 DATETIME 
		DECLARE @VC_CREDIT                                VARCHAR(3) 
		DECLARE @VC_CREDIT_LIM                            NUMERIC(15) 
		DECLARE @VC_DISCOUNT                              NUMERIC(15) 
		DECLARE @VC_BALANCE                               NUMERIC(38) 
		DECLARE @VC_PAYMENT_CNT                           NUMERIC(15) 
		DECLARE @VC_DATA                                  VARCHAR(500) 

		DECLARE RS_CUST_ID CURSOR LOCAL FOR 
		SELECT C_ID
		FROM  CUSTOMER 
		WHERE	 C_W_ID  = @VW_ID
		 AND	C_D_ID  = @VD_ID
		 AND	C_LAST  = @VC_LAST
		ORDER BY C_W_ID,
			 C_D_ID,
			 C_LAST,
			 C_FIRST 
		
		SELECT @VCUS_ID  = 1 
		SELECT @VDIS_ID  = 100001 
		SELECT @VWAR_ID  = 1 
		SELECT @VHIS_ID  = 1 
		IF ( @VC_ID = '0' ) 
		BEGIN 
			SELECT @NAMECNT  =  COUNT(C_ID)
			FROM  CUSTOMER 
			WHERE	 C_LAST  = @VC_LAST
			 AND	C_D_ID  = @VD_ID
			 AND	C_W_ID  = @VW_ID
			
			SELECT @N  = 0 
			
			OPEN RS_CUST_ID 
			DECLARE @count		 INT 
		SELECT @count = 1 
		WHILE (0 = 0) 
			BEGIN --( 
				
				FETCH NEXT FROM RS_CUST_ID INTO 
									@VCC_ID
				IF (@@FETCH_STATUS = -1) 
				BREAK

				SELECT @N  = @N + 1 
			SELECT @count=@count +1
			END --) 
			
		END
   
		SELECT
				 @VC_FIRST  =  C_FIRST,
				 @VC_MIDDLE  =  C_MIDDLE,
				 @VC_LAST  =  C_LAST,
				 @VC_STREET_1  =  C_STREET_1,
				 @VC_STREET_2  =  C_STREET_2,
				 @VC_CITY  =  C_CITY,
				 @VC_STATE  =  C_STATE,
				 @VC_ZIP  =  C_ZIP,
				 @VC_PHONE  =  C_PHONE,
				 @VC_SINCE  =  C_SINCE,
				 @VC_CREDIT  =  C_CREDIT,
				 @VC_CREDIT_LIM  =  C_CREDIT_LIM,
				 @VC_DISCOUNT  =  C_DISCOUNT,
				 @VC_BALANCE  =  C_BALANCE,
				 @VC_PAYMENT_CNT  =  C_PAYMENT_CNT,
				 @VC_DATA  =  C_DATA
		FROM  CUSTOMER 
		WHERE	 C_W_ID  = @VW_ID
		 AND	C_D_ID  = @VD_ID
		 AND	C_ID  = @VC_ID
		
		SELECT @VC_BALANCE  = @VC_BALANCE + @VH_AMOUNT 
		IF ( @VC_CREDIT = 'BC' ) 
		BEGIN 
			UPDATE  CUSTOMER   
			SET	C_BALANCE = @VC_BALANCE,	
			    C_DATA = (CAST(@VC_ID AS VARCHAR) + CAST(@VD_ID AS VARCHAR) + CAST(@VW_ID AS VARCHAR) + CAST(@VD_ID AS VARCHAR) + CAST(@VW_ID AS VARCHAR) + CAST(@VH_AMOUNT AS VARCHAR) + CAST(@VC_DATA AS VARCHAR)),	
			    C_PAYMENT_CNT = (@VC_PAYMENT_CNT + 1),	
			    SEQ_ID = @VCUS_ID + 1 
			WHERE  C_W_ID  = @VW_ID
			 AND	C_D_ID  = @VD_ID
			 AND	C_ID  = @VC_ID 
			
			
			-- IMPLICIT_TRANSACTIONS is set to OFF
			UPDATE  CUSTOMER   
			SET	C_BALANCE = @VC_BALANCE,	
			    C_PAYMENT_CNT = (@VC_PAYMENT_CNT + 1),	
			    SEQ_ID = @VCUS_ID + 1 
			WHERE  C_W_ID  = @VW_ID
			 AND	C_D_ID  = @VD_ID
			 AND	C_ID  = @VC_ID 
			
			
			-- IMPLICIT_TRANSACTIONS is set to OFF
		END
   
		SELECT
				 @VD_STREET_1  =  D_STREET_1,
				 @VD_STREET_2  =  D_STREET_2,
				 @VD_CITY  =  D_CITY,
				 @VD_STATE  =  D_STATE,
				 @VD_ZIP  =  D_ZIP,
				 @VD_NAME  =  D_NAME,
				 @VD_YTD  =  D_YTD
		FROM  DISTRICT 
		WHERE	 D_W_ID  = @VW_ID
		 AND	D_ID  = @VD_ID
		
		UPDATE  DISTRICT   
		SET	D_YTD = (@VD_YTD + @VH_AMOUNT),	
		    SEQ_ID = @VDIS_ID + 1 
		WHERE  D_W_ID  = @VW_ID
		 AND	D_ID  = @VD_ID 
		
		
		-- IMPLICIT_TRANSACTIONS is set to OFF
		SELECT
				 @VW_NAME  =  W_NAME,
				 @VW_STREET_1  =  W_STREET_1,
				 @VW_STREET_2  =  W_STREET_2,
				 @VW_CITY  =  W_CITY,
				 @VW_STATE  =  W_STATE,
				 @VW_ZIP  =  W_ZIP,
				 @VW_YTD  =  W_YTD
		FROM  WAREHOUSE 
		WHERE	 W_ID  = @VW_ID
		
		UPDATE  WAREHOUSE   
		SET	W_YTD = (@VW_YTD + @VH_AMOUNT),	
		    SEQ_ID = @VWAR_ID + 1 
		WHERE  W_ID  = @VW_ID 
		
		
		-- IMPLICIT_TRANSACTIONS is set to OFF
		SELECT @VH_DATA  = @VW_NAME + '' + @VD_NAME 
		INSERT INTO  HISTORY   
				( H_C_D_ID , 
				H_C_W_ID , 
				H_C_ID , 
				H_D_ID , 
				H_W_ID , 
				H_DATE , 
				H_AMOUNT , 
				H_DATA , 
				SEQ_ID )  
			VALUES 		( @VD_ID , 
				@VW_ID , 
				@VC_ID , 
				@VD_ID , 
				@VW_ID , 
				getdate(), 
				@VH_AMOUNT , 
				@VH_DATA , 
				@VHIS_ID + 1 )  
		
		
		-- IMPLICIT_TRANSACTIONS is set to OFF
 DEALLOCATE RS_CUST_ID

		SET NOCOUNT OFF

	END





GO

/****** Object:  StoredProcedure [dbo].[STOCK_LEVEL]    Script Date: 10/13/2009 21:58:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
CREATE PROCEDURE [dbo].[STOCK_LEVEL]
	@VW_ID                                    VARCHAR(4000) ,
	@VD_ID                                    VARCHAR(4000) 
AS 
	BEGIN
		SET NOCOUNT ON
		
		DECLARE @VD_NEXT_O_ID                             VARCHAR (4000)/* TABLE NOT FOUND IN METADATA OR METADATA NOT UPDATED */ 
		DECLARE @N_ITEMS                                  FLOAT 
		SELECT @VD_NEXT_O_ID  =  D_NEXT_O_ID
		FROM  DISTRICT 
		WHERE	 D_W_ID  = @VW_ID
		 AND	D_ID  = @VD_ID
		
		SELECT @N_ITEMS  =  COUNT(DISTINCT S_I_ID)
		FROM  ORDER_LINE,
			 STOCK 
		WHERE	 OL_W_ID  = @VW_ID
		 AND	OL_D_ID  = @VD_ID
		 AND	OL_O_ID  < @VD_NEXT_O_ID
		 AND	OL_O_ID  >= (@VD_NEXT_O_ID - 20)
		 AND	S_W_ID  = @VW_ID
		 AND	S_I_ID  = OL_I_ID
		

		SET NOCOUNT OFF

	END

GO


