--------------------------------------------------
-- ESQUEMAS
--------------------------------------------------
-- Schema [COM]
CREATE SCHEMA [COM]
GO

--------------------------------------------------
-- TABLAS
--------------------------------------------------
-- Table [COM].[Atributo]
CREATE TABLE [COM].[Atributo](
	[IdAtributo] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](40) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [nvarchar](200) NOT NULL,
	[TipoAmbito] [varchar](20) NOT NULL,
	[TipoDato] [varchar](10) NOT NULL,
	[Configuracion] [xml] NOT NULL,
	[EsVisible] [bit] NOT NULL,
	[EsEditable] [bit] NOT NULL,
	[EsActivo] [bit] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaModificacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Atributo] PRIMARY KEY CLUSTERED 
(
	[IdAtributo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_Atributo] UNIQUE NONCLUSTERED 
(
	[Codigo] ASC,
	[TipoAmbito] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-- Table [COM].[Catalogo]
CREATE TABLE [COM].[Catalogo](
	[IdCatalogo] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](40) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [nvarchar](200) NOT NULL,
	[EsVisible] [bit] NOT NULL,
	[EsEditable] [bit] NOT NULL,
 CONSTRAINT [PK_Catalogo] PRIMARY KEY CLUSTERED 
(
	[IdCatalogo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_Catalogo] UNIQUE NONCLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Table [COM].[CatalogoItem]
CREATE TABLE [COM].[CatalogoItem](
	[IdCatalogoItem] [int] IDENTITY(1,1) NOT NULL,
	[IdCatalogo] [int] NOT NULL,
	[Codigo] [varchar](40) NOT NULL,
	[Descripcion] [nvarchar](100) NOT NULL,
	[EsEditable] [bit] NOT NULL,
	[EsActivo] [bit] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaModificacion] [datetime] NOT NULL,
 CONSTRAINT [PK_CatalogoItem] PRIMARY KEY CLUSTERED 
(
	[IdCatalogoItem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_CatalogoItem] UNIQUE NONCLUSTERED 
(
	[IdCatalogo] ASC,
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Table [COM].[Localidad]
CREATE TABLE [COM].[Localidad](
	[IdLocalidad] [int] IDENTITY(1,1) NOT NULL,
	[IdPaisDivision] [smallint] NOT NULL,
	[IdLocalidadPadre] [int] NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Codigo] [varchar](5) NULL,
	[CodigoAlterno] [varchar](10) NULL,
	[EsActivo] [bit] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaModificacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Localidad] PRIMARY KEY CLUSTERED 
(
	[IdLocalidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Table [COM].[Pais]
CREATE TABLE [COM].[Pais](
	[IdPais] [smallint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[NombreOficial] [varchar](100) NOT NULL,
	[CodigoDosLetras] [char](2) NOT NULL,
	[CodigoTresLetras] [char](3) NOT NULL,
	[CodigoTresDigitos] [char](3) NOT NULL,
	[PrefijoInternet] [char](3) NULL,
	[EsActivo] [bit] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaModificacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Pais] PRIMARY KEY CLUSTERED 
(
	[IdPais] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_Pais] UNIQUE NONCLUSTERED 
(
	[CodigoTresLetras] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Table [COM].[PaisDivision]
CREATE TABLE [COM].[PaisDivision](
	[IdPaisDivision] [smallint] IDENTITY(1,1) NOT NULL,
	[IdPais] [smallint] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Nivel] [int] NOT NULL,
	[EsNivelCiudad] [bit] NOT NULL,
	[EsActivo] [bit] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaModificacion] [datetime] NOT NULL,
 CONSTRAINT [PK_PaisDivision] PRIMARY KEY CLUSTERED 
(
	[IdPaisDivision] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Table [COM].[ValorAtributo]
CREATE TABLE [COM].[ValorAtributo](
	[IdAtributo] [int] NOT NULL,
	[IdRelacion] [int] NOT NULL,
	[Valor] [xml] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaModificacion] [datetime] NOT NULL,
 CONSTRAINT [PK_ValorAtributo] PRIMARY KEY CLUSTERED 
(
	[IdAtributo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_ValorAtributo] UNIQUE NONCLUSTERED 
(
	[IdAtributo] ASC,
	[IdRelacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

--------------------------------------------------
-- DEFAULTS
--------------------------------------------------

--------------------------------------------------
-- FOREIGN KEYS
--------------------------------------------------
ALTER TABLE [COM].[CatalogoItem]  WITH CHECK ADD  CONSTRAINT [FK_CatalogoItem_Catalogo] FOREIGN KEY([IdCatalogo])
REFERENCES [COM].[Catalogo] ([IdCatalogo])
GO

ALTER TABLE [COM].[CatalogoItem] CHECK CONSTRAINT [FK_CatalogoItem_Catalogo]
GO

ALTER TABLE [COM].[Localidad]  WITH CHECK ADD  CONSTRAINT [FK_Localidad_PaisDivision] FOREIGN KEY([IdPaisDivision])
REFERENCES [COM].[PaisDivision] ([IdPaisDivision])
GO

ALTER TABLE [COM].[Localidad] CHECK CONSTRAINT [FK_Localidad_PaisDivision]
GO

ALTER TABLE [COM].[PaisDivision]  WITH CHECK ADD  CONSTRAINT [FK_PaisDivision_Pais] FOREIGN KEY([IdPais])
REFERENCES [COM].[Pais] ([IdPais])
GO

ALTER TABLE [COM].[PaisDivision] CHECK CONSTRAINT [FK_PaisDivision_Pais]
GO

ALTER TABLE [COM].[ValorAtributo]  WITH CHECK ADD  CONSTRAINT [FK_ValorAtributo_Atributo] FOREIGN KEY([IdAtributo])
REFERENCES [COM].[Atributo] ([IdAtributo])
GO

ALTER TABLE [COM].[ValorAtributo] CHECK CONSTRAINT [FK_ValorAtributo_Atributo]
GO
