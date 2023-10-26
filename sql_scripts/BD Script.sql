USE [HakunaMatata]
GO
/****** Object:  Table [dbo].[Juegos]    Script Date: 26/10/2023 15:50:12 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Puntajes]    Script Date: 26/10/2023 15:50:12 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 26/10/2023 15:50:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](max) NOT NULL,
	[Contrasena] [varchar](max) NOT NULL,
	[Mail] [varchar](max) NOT NULL,
	[Token] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Juegos] ON 

INSERT [dbo].[Juegos] ([idJuego], [Nombre]) VALUES (1, N'Tetris')
INSERT [dbo].[Juegos] ([idJuego], [Nombre]) VALUES (2, N'Snake')
INSERT [dbo].[Juegos] ([idJuego], [Nombre]) VALUES (3, N'2048')
INSERT [dbo].[Juegos] ([idJuego], [Nombre]) VALUES (4, N'Buscaminas')
SET IDENTITY_INSERT [dbo].[Juegos] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([idUsuario], [Nombre], [Contrasena], [Mail], [Token]) VALUES (1, N'Juan', N'$2a$11$fM9jHSC9XrvMW0iuX5jfjeswtGpbRPOjEhWx1jTcfr6kfmZH7qrnu', N'Juan@gmail.com', N'token')
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
