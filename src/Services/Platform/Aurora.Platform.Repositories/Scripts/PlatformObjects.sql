--------------------------------------------------
-- ESQUEMAS
--------------------------------------------------
-- Schema [APP]
CREATE SCHEMA [APP]
GO

-- Schema [SEG]
CREATE SCHEMA [SEG]
GO

--------------------------------------------------
-- TABLAS
--------------------------------------------------
-- Table [APP].[Aplicacion]
CREATE TABLE [APP].[Aplicacion](
	[IdAplicacion] [smallint] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](36) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Aplicacion] PRIMARY KEY CLUSTERED 
(
	[IdAplicacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_Aplicacion] UNIQUE NONCLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Table [APP].[Componente]
CREATE TABLE [APP].[Componente](
	[IdComponente] [int] IDENTITY(1,1) NOT NULL,
	[IdAplicacion] [smallint] NOT NULL,
	[Codigo] [varchar](40) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Componente] PRIMARY KEY CLUSTERED 
(
	[IdComponente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_Componente] UNIQUE NONCLUSTERED 
(
	[IdAplicacion] ASC,
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Table [APP].[Repositorio]
CREATE TABLE [APP].[Repositorio](
	[IdRepositorio] [int] IDENTITY(1,1) NOT NULL,
	[IdAplicacion] [smallint] NOT NULL,
	[Codigo] [varchar](36) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Repositorio] PRIMARY KEY CLUSTERED 
(
	[IdRepositorio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_Repositorio] UNIQUE NONCLUSTERED 
(
	[IdAplicacion] ASC,
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Table [APP].[RepositorioDetalle]
CREATE TABLE [APP].[RepositorioDetalle](
	[IdRepositorioDetalle] [int] IDENTITY(1,1) NOT NULL,
	[IdRepositorio] [int] NOT NULL,
	[IdComponente] [int] NOT NULL,
	[Cadena] [varchar](1000) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaModificacion] [datetime] NOT NULL,
 CONSTRAINT [PK_RepositorioDetalle] PRIMARY KEY CLUSTERED 
(
	[IdRepositorioDetalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_RepositorioDetalle] UNIQUE NONCLUSTERED 
(
	[IdRepositorio] ASC,
	[IdComponente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Table [SEG].[Rol]
CREATE TABLE [SEG].[Rol](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[IdRepositorio] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
	[EsPredeterminado] [bit] NOT NULL,
	[EsActivo] [bit] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaModificacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_Rol] UNIQUE NONCLUSTERED 
(
	[IdRepositorio] ASC,
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Table [SEG].[Usuario]
CREATE TABLE [SEG].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[NombreUsuario] [varchar](35) NOT NULL,
	[Descripcion] [nvarchar](100) NOT NULL,
	[CorreoElectronico] [varchar](100) NOT NULL,
	[EsPredeterminado] [bit] NOT NULL,
	[EsActivo] [bit] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaModificacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_Usuario] UNIQUE NONCLUSTERED 
(
	[NombreUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Table [SEG].[UsuarioCredencial]
CREATE TABLE [SEG].[UsuarioCredencial](
	[IdUsuario] [int] NOT NULL,
	[Contrasena] [varchar](200) NOT NULL,
	[ContrasenaControl] [varchar](500) NOT NULL,
	[DebeCambiar] [bit] NOT NULL,
	[FechaExpiracion] [datetime] NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaModificacion] [datetime] NOT NULL,
 CONSTRAINT [PK_UsuarioCredencial] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Table [SEG].[UsuarioCredencialHistorial]
CREATE TABLE [SEG].[UsuarioCredencialHistorial](
	[IdHistorial] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[NumeroCambio] [int] NOT NULL,
	[Contrasena] [varchar](200) NOT NULL,
	[ContrasenaControl] [varchar](500) NOT NULL,
	[DebeCambiar] [bit] NOT NULL,
	[FechaExpiracion] [datetime] NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaFinalizacion] [datetime] NULL,
 CONSTRAINT [PK_UsuarioCredencialHistorial] PRIMARY KEY CLUSTERED 
(
	[IdHistorial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Table [SEG].[UsuarioPertenencia]
CREATE TABLE [SEG].[UsuarioPertenencia](
	[IdPertenencia] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IdRol] [int] NOT NULL,
	[EsPredeterminado] [bit] NOT NULL,
	[EsActivo] [bit] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaModificacion] [datetime] NOT NULL,
 CONSTRAINT [PK_UsuarioPertenencia] PRIMARY KEY CLUSTERED 
(
	[IdPertenencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_UsuarioPertenencia] UNIQUE NONCLUSTERED 
(
	[IdUsuario] ASC,
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--------------------------------------------------
-- DEFAULTS
--------------------------------------------------
ALTER TABLE [APP].[Aplicacion] ADD  CONSTRAINT [DF_Aplicacion_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO

ALTER TABLE [APP].[Componente] ADD  CONSTRAINT [DF_Componente_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO

ALTER TABLE [APP].[Repositorio] ADD  CONSTRAINT [DF_Repositorio_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO

ALTER TABLE [APP].[RepositorioDetalle] ADD  CONSTRAINT [DF_RepositorioDetalle_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO

ALTER TABLE [APP].[RepositorioDetalle] ADD  CONSTRAINT [DF_RepositorioDetalle_FechaModificacion]  DEFAULT (getdate()) FOR [FechaModificacion]
GO

--------------------------------------------------
-- FOREIGN KEYS
--------------------------------------------------
ALTER TABLE [APP].[Componente]  WITH CHECK ADD  CONSTRAINT [FK_Componente_Aplicacion] FOREIGN KEY([IdAplicacion])
REFERENCES [APP].[Aplicacion] ([IdAplicacion])
GO

ALTER TABLE [APP].[Componente] CHECK CONSTRAINT [FK_Componente_Aplicacion]
GO

ALTER TABLE [APP].[Repositorio]  WITH CHECK ADD  CONSTRAINT [FK_Repositorio_Aplicacion] FOREIGN KEY([IdAplicacion])
REFERENCES [APP].[Aplicacion] ([IdAplicacion])
GO

ALTER TABLE [APP].[Repositorio] CHECK CONSTRAINT [FK_Repositorio_Aplicacion]
GO

ALTER TABLE [APP].[RepositorioDetalle]  WITH CHECK ADD  CONSTRAINT [FK_RepositorioDetalle_Repositorio] FOREIGN KEY([IdRepositorio])
REFERENCES [APP].[Repositorio] ([IdRepositorio])
GO

ALTER TABLE [APP].[RepositorioDetalle] CHECK CONSTRAINT [FK_RepositorioDetalle_Repositorio]
GO

ALTER TABLE [SEG].[UsuarioCredencial]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioCredencial_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [SEG].[Usuario] ([IdUsuario])
GO

ALTER TABLE [SEG].[UsuarioCredencial] CHECK CONSTRAINT [FK_UsuarioCredencial_Usuario]
GO

ALTER TABLE [SEG].[UsuarioCredencialHistorial]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioCredencialHistorial_UsuarioCredencial] FOREIGN KEY([IdUsuario])
REFERENCES [SEG].[UsuarioCredencial] ([IdUsuario])
GO

ALTER TABLE [SEG].[UsuarioCredencialHistorial] CHECK CONSTRAINT [FK_UsuarioCredencialHistorial_UsuarioCredencial]
GO

ALTER TABLE [SEG].[UsuarioPertenencia]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioPertenencia_Rol] FOREIGN KEY([IdRol])
REFERENCES [SEG].[Rol] ([IdRol])
GO

ALTER TABLE [SEG].[UsuarioPertenencia] CHECK CONSTRAINT [FK_UsuarioPertenencia_Rol]
GO

ALTER TABLE [SEG].[UsuarioPertenencia]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioPertenencia_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [SEG].[Usuario] ([IdUsuario])
GO

ALTER TABLE [SEG].[UsuarioPertenencia] CHECK CONSTRAINT [FK_UsuarioPertenencia_Usuario]
GO
