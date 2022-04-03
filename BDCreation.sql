USE [master]
GO

/****** Object:  Database [ObligatorioP3]    Script Date: 03/04/2022 10:42:05 ******/
CREATE DATABASE [ObligatorioP3]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ObligatorioP^3', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ObligatorioP^3.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ObligatorioP^3_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ObligatorioP^3_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ObligatorioP3].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

USE [ObligatorioP3]
GO

/****** Object:  Table [dbo].[Usuarios]    Script Date: 02/04/2022 20:53:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Usuarios](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Mail] [varchar](40) NOT NULL,
	[Pass] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Ambientes](
	[idAmbiente] [int] IDENTITY(1,1) NOT NULL,
	[TipoAmbiente] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Ambientes] PRIMARY KEY CLUSTERED 
(
	[idAmbiente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TiposDePlanta](
	[idTipo] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](30) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Tipo] PRIMARY KEY CLUSTERED 
(
	[idTipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TiposDeIluminacion](
	[idIluminacion] [int] IDENTITY(1,1) NOT NULL,
	[TipoIluminacion] [varchar](20) NOT NULL,
 CONSTRAINT [PK_TiposDeIluminacion] PRIMARY KEY CLUSTERED 
(
	[idIluminacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Plantas](
	[idPlnata] [int] IDENTITY(1,1) NOT NULL,
	[TipoPlanta] [int] NOT NULL,
	[NombreCientifico] [varchar](40) NOT NULL,
	[NombreVulgar] [varchar](200) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
	[TipoAmbiente] [int] NOT NULL,
	[Altura] [decimal](5, 2) NOT NULL,
	[Foto] [varchar](200) NOT NULL,
	[FrecuenciaDeRiego] [varchar](20) NOT NULL,
	[TipoIluminacion] [int] NOT NULL,
	[TempMantenimiento] [int] NOT NULL,
 CONSTRAINT [PK_Plantas] PRIMARY KEY CLUSTERED 
(
	[idPlnata] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Plantas]  WITH CHECK ADD  CONSTRAINT [FK_Plantas_Ambientes] FOREIGN KEY([TipoAmbiente])
REFERENCES [dbo].[Ambientes] ([idAmbiente])
GO

ALTER TABLE [dbo].[Plantas] CHECK CONSTRAINT [FK_Plantas_Ambientes]
GO

ALTER TABLE [dbo].[Plantas]  WITH CHECK ADD  CONSTRAINT [FK_Plantas_TiposDeIluminacion] FOREIGN KEY([TipoIluminacion])
REFERENCES [dbo].[TiposDeIluminacion] ([idIluminacion])
GO

ALTER TABLE [dbo].[Plantas] CHECK CONSTRAINT [FK_Plantas_TiposDeIluminacion]
GO

ALTER TABLE [dbo].[Plantas]  WITH CHECK ADD  CONSTRAINT [FK_Plantas_TiposDePlanta] FOREIGN KEY([TipoPlanta])
REFERENCES [dbo].[TiposDePlanta] ([idTipo])
GO

ALTER TABLE [dbo].[Plantas] CHECK CONSTRAINT [FK_Plantas_TiposDePlanta]
GO



