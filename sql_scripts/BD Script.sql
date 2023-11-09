USE [HakunaMatata]
GO
/****** Object:  Table [dbo].[Amistades]    Script Date: 8/11/2023 11:56:29 ******/
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
/****** Object:  Table [dbo].[Juegos]    Script Date: 8/11/2023 11:56:29 ******/
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
/****** Object:  Table [dbo].[Puntajes]    Script Date: 8/11/2023 11:56:29 ******/
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
/****** Object:  Table [dbo].[Usuarios]    Script Date: 8/11/2023 11:56:29 ******/
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
SET IDENTITY_INSERT [dbo].[Amistades] ON 

INSERT [dbo].[Amistades] ([idAmistad], [idUsuario1], [idUsuario2], [Estado]) VALUES (1, 1, 1, 2)
INSERT [dbo].[Amistades] ([idAmistad], [idUsuario1], [idUsuario2], [Estado]) VALUES (2, 1, 1, 2)
INSERT [dbo].[Amistades] ([idAmistad], [idUsuario1], [idUsuario2], [Estado]) VALUES (3, 1, 1, 2)
SET IDENTITY_INSERT [dbo].[Amistades] OFF
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
