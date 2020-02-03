USE [aeim]
GO

/****** Object:  Table [dbo].[Demande_Absence]    Script Date: 20/03/2018 09:28:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Demande_Absence](
	[Matricule] [varchar](15) NOT NULL,
	[Date debut] [datetime] NOT NULL,
	[Date fin] [datetime] NOT NULL,
	[Type Jour] [varchar](4) NOT NULL,
    [Commentaire agent] [varchar](250) NULL,
    [Commentaire gestionnaire] [varchar](250) NULL,	
    [Etat] [varchar](3) NOT NULL,    
 CONSTRAINT [PK_Demande_Absence] PRIMARY KEY CLUSTERED 
(
	[Matricule] ASC,
	[Date debut] ASC,
	[Date fin] ASC	
))



