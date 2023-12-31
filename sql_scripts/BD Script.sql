USE [HakunaMatata]
GO
/****** Object:  Table [dbo].[Amistades]    Script Date: 16/11/2023 14:49:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Amistades](
	[idAmistad] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario1] [int] NOT NULL,
	[idUsuario2] [int] NOT NULL,
	[Estado] [int] NOT NULL,
 CONSTRAINT [PK_Amistades] PRIMARY KEY CLUSTERED 
(
	[idAmistad] ASC
)
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Juegos]    Script Date: 16/11/2023 14:49:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Juegos](
	[idJuego] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Juegos] PRIMARY KEY CLUSTERED 
(
	[idJuego] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notificaciones]    Script Date: 16/11/2023 14:49:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notificaciones](
	[idNotificacion] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario1] [int] NOT NULL,
	[idUsuario2] [int] NOT NULL,
	[idAmistad] [int] NULL,
	[Titulo] [text] NOT NULL,
	[Descripcion] [text] NOT NULL,
	[Estado] [int] NOT NULL,
 CONSTRAINT [PK_Notificaciones] PRIMARY KEY CLUSTERED 
(
	[idNotificacion] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Puntajes]    Script Date: 16/11/2023 14:49:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Puntajes](
	[idPuntaje] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NOT NULL,
	[idJuego] [int] NOT NULL,
	[Puntos] [int] NOT NULL,
 CONSTRAINT [PK_Puntajes] PRIMARY KEY CLUSTERED 
(
	[idPuntaje] ASC
)
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 16/11/2023 14:49:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](max) NOT NULL,
	[Contrasena] [varchar](max) NOT NULL,
	[Mail] [varchar](max) NOT NULL,
	[FechaNacimiento] [date] NOT NULL,
	[FotoPerfil] [varchar](max) NULL,
	[Token] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Juegos] ON 

INSERT [dbo].[Juegos] ([idJuego], [Nombre]) VALUES (1, N'Tetris')
INSERT [dbo].[Juegos] ([idJuego], [Nombre]) VALUES (2, N'Snake')
INSERT [dbo].[Juegos] ([idJuego], [Nombre]) VALUES (3, N'2048')
INSERT [dbo].[Juegos] ([idJuego], [Nombre]) VALUES (4, N'Buscaminas')
INSERT [dbo].[Juegos] ([idJuego], [Nombre]) VALUES (5, N'Wordle')
SET IDENTITY_INSERT [dbo].[Juegos] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([idUsuario], [Nombre], [Contrasena], [Mail], [FechaNacimiento], [FotoPerfil], [Token]) VALUES (1, N'Juan', N'$2a$11$fM9jHSC9XrvMW0iuX5jfjeswtGpbRPOjEhWx1jTcfr6kfmZH7qrnu', N'Juan@gmail.com', CAST(N'1111-11-11' AS Date), N'/assets/fotosPerfil/foto-anonimo.jpg', N'token')
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
ALTER TABLE [dbo].[Notificaciones]  WITH CHECK ADD  CONSTRAINT [FK_Notificaciones_Amistades] FOREIGN KEY([idAmistad])
REFERENCES [dbo].[Amistades] ([idAmistad])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Notificaciones] CHECK CONSTRAINT [FK_Notificaciones_Amistades]
GO
ALTER TABLE [dbo].[Notificaciones]  WITH CHECK ADD  CONSTRAINT [FK_Notificaciones_Usuarios] FOREIGN KEY([idUsuario1])
REFERENCES [dbo].[Usuarios] ([idUsuario])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Notificaciones] CHECK CONSTRAINT [FK_Notificaciones_Usuarios]
GO
ALTER TABLE [dbo].[Puntajes]  WITH CHECK ADD  CONSTRAINT [FK_Puntajes_Juegos] FOREIGN KEY([idJuego])
REFERENCES [dbo].[Juegos] ([idJuego])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Puntajes] CHECK CONSTRAINT [FK_Puntajes_Juegos]
GO
ALTER TABLE [dbo].[Puntajes]  WITH CHECK ADD  CONSTRAINT [FK_Puntajes_Usuarios] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuarios] ([idUsuario])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Puntajes] CHECK CONSTRAINT [FK_Puntajes_Usuarios]
GO
/****** Object:  StoredProcedure [dbo].[sp_AceptarAmistad]    Script Date: 16/11/2023 14:49:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_AceptarAmistad]
	@id int
AS
BEGIN
	UPDATE Amistades SET Estado = 0 WHERE idAmistad = @id
	DELETE FROM Notificaciones WHERE idAmistad = @id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_EnviarSolicitud]    Script Date: 16/11/2023 14:49:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_EnviarSolicitud]
    @idUsuario1 INT,
    @idUsuario2 INT
AS
BEGIN
    INSERT INTO Amistades (idUsuario1, idUsuario2, Estado) VALUES (@idUsuario1, @idUsuario2, 2)

    DECLARE @idAmistad INT
    SET @idAmistad = SCOPE_IDENTITY()

    DECLARE @nombreUsuario1 NVARCHAR(MAX)
    SET @nombreUsuario1 = (SELECT Nombre FROM Usuarios WHERE idUsuario = @idUsuario1)

    INSERT INTO Notificaciones (idUsuario1, idUsuario2, idAmistad, Titulo, Descripcion, Estado)
    VALUES (@idUsuario1, @idUsuario2, @idAmistad, 'Solicitud de amistad', @nombreUsuario1 + ' quiere ser tu amigo!', 0)

END
GO
/****** Object:  StoredProcedure [dbo].[sp_RechazarAmistad]    Script Date: 16/11/2023 14:49:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_RechazarAmistad]
	@id int
AS
BEGIN
	DELETE FROM Amistades WHERE idAmistad = @id
	DELETE FROM Notificaciones WHERE idAmistad = @id
END
GO
