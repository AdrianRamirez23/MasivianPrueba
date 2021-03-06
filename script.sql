USE [APIRuleta]
GO
/****** Object:  Table [dbo].[Apuestas]    Script Date: 12/01/2021 8:06:13 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Apuestas](
	[idApuesta] [int] IDENTITY(1,1) NOT NULL,
	[idRuleta] [int] NOT NULL,
	[Usser] [varchar](15) NOT NULL,
	[MontoApuesta] [decimal](38, 2) NOT NULL,
	[Apuesta] [varchar](5) NOT NULL,
 CONSTRAINT [PK_Apuestas] PRIMARY KEY CLUSTERED 
(
	[idApuesta] ASC,
	[idRuleta] ASC,
	[Usser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CierreRuletas]    Script Date: 12/01/2021 8:06:13 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CierreRuletas](
	[idCierre] [int] IDENTITY(1,1) NOT NULL,
	[idRuleta] [int] NOT NULL,
	[Resultado] [int] NOT NULL,
	[MontoApostado] [decimal](38, 2) NOT NULL,
	[Usser] [varchar](15) NOT NULL,
	[Apuesta] [varchar](5) NOT NULL,
	[Gano] [varchar](2) NOT NULL,
	[ValorFinal] [decimal](38, 2) NOT NULL,
 CONSTRAINT [PK_CierreRuletas] PRIMARY KEY CLUSTERED 
(
	[idCierre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ruletas]    Script Date: 12/01/2021 8:06:13 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ruletas](
	[idRuleta] [int] IDENTITY(1,1) NOT NULL,
	[Resultado] [int] NULL,
	[EstadoRuleta] [bit] NOT NULL,
 CONSTRAINT [PK_Ruletas_1] PRIMARY KEY CLUSTERED 
(
	[idRuleta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 12/01/2021 8:06:13 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[NombreUsuario] [varchar](200) NOT NULL,
	[Usser] [varchar](15) NOT NULL,
	[Password] [varchar](25) NOT NULL,
	[Nit] [varchar](13) NOT NULL,
	[Email] [varchar](150) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Usser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Apuestas] ON 

INSERT [dbo].[Apuestas] ([idApuesta], [idRuleta], [Usser], [MontoApuesta], [Apuesta]) VALUES (1, 1, N'ARAMIREZ', CAST(200.00 AS Decimal(38, 2)), N'Negro')
INSERT [dbo].[Apuestas] ([idApuesta], [idRuleta], [Usser], [MontoApuesta], [Apuesta]) VALUES (2, 1, N'GURACA', CAST(700.00 AS Decimal(38, 2)), N'26')
SET IDENTITY_INSERT [dbo].[Apuestas] OFF
GO
SET IDENTITY_INSERT [dbo].[CierreRuletas] ON 

INSERT [dbo].[CierreRuletas] ([idCierre], [idRuleta], [Resultado], [MontoApostado], [Usser], [Apuesta], [Gano], [ValorFinal]) VALUES (1, 1, 1, CAST(200.00 AS Decimal(38, 2)), N'ARAMIREZ', N'Negro', N'SI', CAST(360.00 AS Decimal(38, 2)))
INSERT [dbo].[CierreRuletas] ([idCierre], [idRuleta], [Resultado], [MontoApostado], [Usser], [Apuesta], [Gano], [ValorFinal]) VALUES (2, 1, 1, CAST(700.00 AS Decimal(38, 2)), N'GURACA', N'26', N'NO', CAST(0.00 AS Decimal(38, 2)))
SET IDENTITY_INSERT [dbo].[CierreRuletas] OFF
GO
SET IDENTITY_INSERT [dbo].[Ruletas] ON 

INSERT [dbo].[Ruletas] ([idRuleta], [Resultado], [EstadoRuleta]) VALUES (1, 1, 0)
SET IDENTITY_INSERT [dbo].[Ruletas] OFF
GO
INSERT [dbo].[Usuarios] ([NombreUsuario], [Usser], [Password], [Nit], [Email], [Estado]) VALUES (N'Adrian Ramirez', N'ARAMIREZ', N'Nicolas1*', N'1036598444', N'adrian.ramirez23@hotmail.com', 1)
INSERT [dbo].[Usuarios] ([NombreUsuario], [Usser], [Password], [Nit], [Email], [Estado]) VALUES (N'Gustavo Ramirez', N'GURACA', N'guraca2*', N'70122996', N'guraca2@hotmail.com', 1)
GO
/****** Object:  StoredProcedure [dbo].[APIRuleta_ApuestasCRUD]    Script Date: 12/01/2021 8:06:14 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[APIRuleta_ApuestasCRUD](
@Opc int,
@idRuleta int,
@Usser varchar(15),
@ValorApuesta decimal(38,2),
@Apuesta varchar(5)
)
AS

IF @Opc=1 BEGIN

  IF EXISTS (SELECT '' FROM Ruletas WHERE idRuleta=@idRuleta AND EstadoRuleta=1)BEGIN
	INSERT INTO Apuestas
	SELECT @idRuleta, @Usser, @ValorApuesta, @Apuesta
  END


END

IF @Opc=2	BEGIN


   SELECT MontoApuesta, Apuesta, Usser FROM Apuestas WHERE idRuleta=@idRuleta


END

GO
/****** Object:  StoredProcedure [dbo].[APIRuleta_CierreCRUD]    Script Date: 12/01/2021 8:06:14 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[APIRuleta_CierreCRUD](
@Resultado int,
@idRuleta int,
@MontoApostado decimal(38,2),
@Apuesta varchar(5),
@Usser varchar(15)
)
	
AS
DECLARE @Gano varchar(2), @ValorFinal decimal(38,2)

IF @Apuesta='Negro' BEGIN

   IF (@Resultado%2<>0) BEGIN

   SET @Gano='SI'
   SET @ValorFinal=@MontoApostado*1.8      

   END ELSE BEGIN

   SET @Gano='NO'
   SET @ValorFinal=0     

   END


END ELSE IF @Apuesta='Rojo' BEGIN

   IF (@Resultado%2=0) BEGIN

   SET @Gano='SI'
   SET @ValorFinal=@MontoApostado*1.8      

   END ELSE BEGIN

   SET @Gano='NO'
   SET @ValorFinal=0     

   END

END ELSE IF @Apuesta=@Resultado BEGIN


   SET @Gano='SI'
   SET @ValorFinal=@MontoApostado*5

END ELSE BEGIN

   SET @Gano='NO'
   SET @ValorFinal=0

END

INSERT INTO CierreRuletas
SELECT @idRuleta, @Resultado, @MontoApostado, @Usser, @Apuesta, @Gano, @ValorFinal

UPDATE Ruletas
   SET Resultado=@Resultado, EstadoRuleta=0
   WHERE idRuleta=@idRuleta


GO
/****** Object:  StoredProcedure [dbo].[APIRuleta_RuletaCRUD]    Script Date: 12/01/2021 8:06:14 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[APIRuleta_RuletaCRUD](
	@Opc int, 
	@idRuleta int,
	@Resultado int,
	@EstadoRuleta bit
	)
AS

IF @Opc=1 BEGIN


    INSERT INTO Ruletas
	SELECT NULL,  1

	SELECT TOP 1 idRuleta FROM Ruletas ORDER BY idRuleta DESC 


END

IF @Opc=2 BEGIN

   SET @Resultado=(SELECT FLOOR(RAND()*(36-1)+1))
   SELECT @Resultado

END

IF @Opc=3 BEGIN

   SELECT * FROM Ruletas

END

IF @Opc=4 BEGIN

   SELECT * FROM CierreRuletas WHERE idRuleta=@idRuleta

END




GO
/****** Object:  StoredProcedure [dbo].[APIRuleta_UsuarioCRUD]    Script Date: 12/01/2021 8:06:14 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[APIRuleta_UsuarioCRUD] (
    @opc int,
	@NombreUsuario varchar(200),
	@Usser varchar(15),
	@Pass varchar(25),
	@Nit varchar(13),
	@Email varchar(200),
	@Estado bit
	)
AS

DECLARE @VAL BIT


IF @opc = 1 BEGIN

   IF EXISTS (SELECT '' FROM Usuarios WHERE Usser= @Usser and Password=@Pass and Estado=1)BEGIN
   SET @VAL=1
   SELECT @VAL
   END ELSE BEGIN
   SET @VAL=0
   SELECT @VAL
   END
END

IF @opc = 2 BEGIN


  INSERT INTO Usuarios
  SELECT @NombreUsuario, @Usser, @Pass, @Nit, @Email, @Estado

END
GO
