USE [master]
GO
/****** Object:  Database [SistemaContable]    Script Date: 26/12/2021 18:06:24 ******/
CREATE DATABASE [SistemaContable]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SistemaContable', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SistemaContable.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SistemaContable_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SistemaContable_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SistemaContable] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SistemaContable].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SistemaContable] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SistemaContable] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SistemaContable] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SistemaContable] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SistemaContable] SET ARITHABORT OFF 
GO
ALTER DATABASE [SistemaContable] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SistemaContable] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SistemaContable] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SistemaContable] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SistemaContable] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SistemaContable] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SistemaContable] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SistemaContable] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SistemaContable] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SistemaContable] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SistemaContable] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SistemaContable] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SistemaContable] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SistemaContable] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SistemaContable] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SistemaContable] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SistemaContable] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SistemaContable] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SistemaContable] SET  MULTI_USER 
GO
ALTER DATABASE [SistemaContable] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SistemaContable] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SistemaContable] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SistemaContable] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SistemaContable] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SistemaContable] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SistemaContable] SET QUERY_STORE = OFF
GO
USE [SistemaContable]
GO
/****** Object:  User [jojama]    Script Date: 26/12/2021 18:06:25 ******/
CREATE USER [jojama] FOR LOGIN [jojama] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [jojama]
GO
/****** Object:  Table [dbo].[Caja]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Caja](
	[Id_Caja] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
	[Tema] [varchar](50) NULL,
	[Serial_PC] [varchar](50) NULL,
	[Impresora_Ticket] [varchar](max) NULL,
	[Impresora_A4] [varchar](max) NULL,
	[Estado] [varchar](50) NULL,
	[Tipo] [varchar](50) NULL,
 CONSTRAINT [PK_Caja_1] PRIMARY KEY CLUSTERED 
(
	[Id_Caja] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[clientes]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[clientes](
	[idclientev] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](max) NULL,
	[Direccion_para_factura] [varchar](max) NULL,
	[Ruc] [varchar](max) NULL,
	[movil] [varchar](50) NULL,
	[Cliente] [varchar](50) NULL,
	[Proveedor] [varchar](50) NULL,
	[Estado] [varchar](50) NULL,
	[Saldo] [numeric](18, 2) NULL,
 CONSTRAINT [PK_clientes] PRIMARY KEY CLUSTERED 
(
	[idclientev] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detalle_venta]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalle_venta](
	[iddetalle_venta] [int] IDENTITY(1,1) NOT NULL,
	[idventa] [int] NULL,
	[Id_producto] [int] NULL,
	[cantidad] [numeric](18, 2) NULL,
	[preciounitario] [numeric](18, 2) NULL,
	[Moneda] [varchar](50) NULL,
	[Total_a_pagar]  AS ([preciounitario]*[cantidad]),
	[Unidad_de_medida] [varchar](50) NULL,
	[Cantidad_mostrada] [numeric](18, 2) NULL,
	[Estado] [varchar](50) NULL,
	[Descripcion] [varchar](50) NULL,
	[Codigo] [varchar](50) NULL,
	[Stock] [varchar](50) NULL,
	[Se_vende_a] [varchar](50) NULL,
	[Usa_inventarios] [varchar](50) NULL,
	[Costo] [numeric](18, 2) NULL,
	[Ganancia]  AS ([cantidad]*[preciounitario]-[cantidad]*[costo]),
 CONSTRAINT [PK_detalle_venta] PRIMARY KEY CLUSTERED 
(
	[iddetalle_venta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EMPRESA]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EMPRESA](
	[Id_empresa] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Empresa] [varchar](50) NULL,
	[Logo] [image] NULL,
	[Impuesto] [varchar](50) NULL,
	[Porcentaje_impuesto] [numeric](18, 0) NULL,
	[Moneda] [varchar](50) NULL,
	[Trabajas_con_impuestos] [varchar](50) NULL,
	[Modo_de_busqueda] [varchar](50) NULL,
	[Carpeta_para_copias_de_seguridad] [varchar](max) NULL,
	[Correo_para_envio_de_reportes] [varchar](50) NULL,
	[Ultima_fecha_de_copia_de_seguridad] [varchar](50) NULL,
	[Ultima_fecha_de_copia_date] [datetime] NULL,
	[Frecuencia_de_copias] [int] NULL,
	[Estado] [varchar](50) NULL,
	[Tipo_de_empresa] [varchar](50) NULL,
	[Pais] [varchar](max) NULL,
	[Redondeo_de_total] [varchar](50) NULL,
 CONSTRAINT [PK_EMPRESA] PRIMARY KEY CLUSTERED 
(
	[Id_empresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grupo_de_Productos]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grupo_de_Productos](
	[Idline] [int] IDENTITY(1,1) NOT NULL,
	[Linea] [varchar](50) NULL,
	[Por_defecto] [varchar](50) NULL,
 CONSTRAINT [PK_linea] PRIMARY KEY CLUSTERED 
(
	[Idline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inicios_de_sesion_por_caja]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inicios_de_sesion_por_caja](
	[Id_inicio_sesion] [int] IDENTITY(1,1) NOT NULL,
	[Id_serial_Pc] [varchar](max) NULL,
	[Id_usuario] [int] NULL,
 CONSTRAINT [PK_Inicios_de_sesion] PRIMARY KEY CLUSTERED 
(
	[Id_inicio_sesion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KARDEX]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KARDEX](
	[Id_kardex] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NULL,
	[Motivo] [varchar](200) NULL,
	[Cantidad] [numeric](18, 0) NULL,
	[Id_producto] [int] NULL,
	[Id_usuario] [int] NULL,
	[Tipo] [varchar](50) NULL,
	[Estado] [varchar](50) NULL,
	[Total]  AS ([Cantidad]*[Costo_unt]),
	[Costo_unt] [numeric](18, 2) NULL,
	[Habia] [numeric](18, 2) NULL,
	[Hay] [numeric](18, 2) NULL,
	[Id_caja] [int] NULL,
 CONSTRAINT [PK_KARDEX] PRIMARY KEY CLUSTERED 
(
	[Id_kardex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Marcan]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Marcan](
	[Id_marca] [int] IDENTITY(1,1) NOT NULL,
	[S] [varchar](max) NULL,
	[F] [varchar](max) NULL,
	[E] [varchar](max) NULL,
	[FA] [varchar](max) NULL,
 CONSTRAINT [PK_Licencias] PRIMARY KEY CLUSTERED 
(
	[Id_marca] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MOVIMIENTOCAJACIERRE]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MOVIMIENTOCAJACIERRE](
	[idcierrecaja] [int] IDENTITY(1,1) NOT NULL,
	[fechainicio] [datetime] NULL,
	[fechafin] [datetime] NULL,
	[fechacierre] [datetime] NULL,
	[ingresos] [numeric](18, 2) NULL,
	[egresos] [numeric](18, 2) NULL,
	[Saldo_queda_en_caja] [numeric](18, 2) NULL,
	[Id_usuario] [int] NULL,
	[Total_calculado] [numeric](18, 2) NULL,
	[Total_real] [numeric](18, 2) NULL,
	[Estado] [varchar](50) NULL,
	[Diferencia] [numeric](18, 2) NULL,
	[Id_caja] [int] NULL,
 CONSTRAINT [PK_MOVIMIENTOCAJACIERRE] PRIMARY KEY CLUSTERED 
(
	[idcierrecaja] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto1]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto1](
	[Id_Producto1] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
	[Imagen] [varchar](50) NULL,
	[Id_grupo] [int] NULL,
	[Usa_inventarios] [varchar](50) NULL,
	[Stock] [varchar](50) NULL,
	[Precio_de_compra] [numeric](18, 2) NULL,
	[Fecha_de_vencimiento] [varchar](50) NULL,
	[Precio_de_venta] [numeric](18, 2) NULL,
	[Codigo] [varchar](50) NULL,
	[Se_vende_a] [varchar](50) NULL,
	[Impuesto] [varchar](50) NULL,
	[Stock_minimo] [numeric](18, 2) NULL,
	[Precio_mayoreo] [numeric](18, 2) NULL,
	[Sub_total_pv]  AS ([Precio_de_venta]-([Precio_de_venta]*[Impuesto])/(100)),
	[Sub_total_pm]  AS ([Precio_mayoreo]-([Precio_mayoreo]*[Impuesto])/(100)),
	[A_partir_de] [numeric](18, 2) NULL,
 CONSTRAINT [PK_Producto1] PRIMARY KEY CLUSTERED 
(
	[Id_Producto1] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Serializacion]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Serializacion](
	[Id_serializacion] [int] IDENTITY(1,1) NOT NULL,
	[Serie] [varchar](50) NULL,
	[Cantidad_de_numeros] [varchar](50) NULL,
	[numerofin] [varchar](50) NULL,
	[Destino] [varchar](50) NULL,
	[tipodoc] [varchar](50) NULL,
	[Por_defecto] [varchar](50) NULL,
 CONSTRAINT [PK_Serializacion] PRIMARY KEY CLUSTERED 
(
	[Id_serializacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket](
	[Id_ticket] [int] IDENTITY(1,1) NOT NULL,
	[Id_Empresa] [int] NULL,
	[Identificador_fiscal] [varchar](max) NULL,
	[Direccion] [varchar](max) NULL,
	[Provincia_Departamento_Pais] [varchar](max) NULL,
	[Nombre_de_Moneda] [varchar](max) NULL,
	[Agradecimiento] [varchar](max) NULL,
	[pagina_Web_Facebook] [varchar](max) NULL,
	[Anuncio] [varchar](max) NULL,
	[Datos_fiscales_de_autorizacion] [varchar](max) NULL,
	[Por_defecto] [varchar](max) NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[Id_ticket] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIO2]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIO2](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombres_y_Apellidos] [varchar](50) NULL,
	[Login] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Icono] [image] NULL,
	[Nombre_de_icono] [varchar](max) NULL,
	[Correo] [varchar](max) NULL,
	[Rol] [varchar](max) NULL,
	[Estado] [varchar](50) NULL,
 CONSTRAINT [PK_USUARIO2] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ventas]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ventas](
	[idventa] [int] IDENTITY(1,1) NOT NULL,
	[idclientev] [int] NULL,
	[fecha_venta] [datetime] NULL,
	[Numero_de_doc] [varchar](50) NULL,
	[Monto_total] [numeric](18, 2) NULL,
	[Tipo_de_pago] [varchar](50) NULL,
	[Estado] [varchar](50) NULL,
	[IGV] [numeric](18, 1) NULL,
	[Comprobante] [varchar](50) NULL,
	[Id_usuario] [int] NULL,
	[Fecha_de_pago] [varchar](50) NULL,
	[ACCION] [varchar](50) NULL,
	[Saldo] [numeric](18, 2) NULL,
	[Pago_con] [numeric](18, 2) NULL,
	[Porcentaje_IGV] [numeric](18, 2) NULL,
	[Id_caja] [int] NULL,
	[Referencia_tarjeta] [varchar](50) NULL,
	[Vuelto] [numeric](18, 2) NULL,
	[Efectivo] [numeric](18, 2) NULL,
	[Credito] [numeric](18, 2) NULL,
	[Tarjeta] [numeric](18, 2) NULL,
 CONSTRAINT [PK_ventas] PRIMARY KEY CLUSTERED 
(
	[idventa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[detalle_venta]  WITH CHECK ADD  CONSTRAINT [FK_detalle_venta_ventas] FOREIGN KEY([idventa])
REFERENCES [dbo].[ventas] ([idventa])
GO
ALTER TABLE [dbo].[detalle_venta] CHECK CONSTRAINT [FK_detalle_venta_ventas]
GO
ALTER TABLE [dbo].[KARDEX]  WITH CHECK ADD  CONSTRAINT [FK_KARDEX_Caja] FOREIGN KEY([Id_caja])
REFERENCES [dbo].[Caja] ([Id_Caja])
GO
ALTER TABLE [dbo].[KARDEX] CHECK CONSTRAINT [FK_KARDEX_Caja]
GO
ALTER TABLE [dbo].[KARDEX]  WITH CHECK ADD  CONSTRAINT [FK_KARDEX_Producto1] FOREIGN KEY([Id_producto])
REFERENCES [dbo].[Producto1] ([Id_Producto1])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KARDEX] CHECK CONSTRAINT [FK_KARDEX_Producto1]
GO
ALTER TABLE [dbo].[KARDEX]  WITH CHECK ADD  CONSTRAINT [FK_KARDEX_USUARIO2] FOREIGN KEY([Id_usuario])
REFERENCES [dbo].[USUARIO2] ([idUsuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KARDEX] CHECK CONSTRAINT [FK_KARDEX_USUARIO2]
GO
ALTER TABLE [dbo].[MOVIMIENTOCAJACIERRE]  WITH CHECK ADD  CONSTRAINT [FK_MOVIMIENTOCAJACIERRE_Caja] FOREIGN KEY([Id_caja])
REFERENCES [dbo].[Caja] ([Id_Caja])
GO
ALTER TABLE [dbo].[MOVIMIENTOCAJACIERRE] CHECK CONSTRAINT [FK_MOVIMIENTOCAJACIERRE_Caja]
GO
ALTER TABLE [dbo].[MOVIMIENTOCAJACIERRE]  WITH CHECK ADD  CONSTRAINT [FK_MOVIMIENTOCAJACIERRE_USUARIO2] FOREIGN KEY([Id_usuario])
REFERENCES [dbo].[USUARIO2] ([idUsuario])
GO
ALTER TABLE [dbo].[MOVIMIENTOCAJACIERRE] CHECK CONSTRAINT [FK_MOVIMIENTOCAJACIERRE_USUARIO2]
GO
ALTER TABLE [dbo].[ventas]  WITH CHECK ADD  CONSTRAINT [FK_ventas_Caja] FOREIGN KEY([Id_caja])
REFERENCES [dbo].[Caja] ([Id_Caja])
GO
ALTER TABLE [dbo].[ventas] CHECK CONSTRAINT [FK_ventas_Caja]
GO
ALTER TABLE [dbo].[ventas]  WITH CHECK ADD  CONSTRAINT [FK_ventas_clientes] FOREIGN KEY([idclientev])
REFERENCES [dbo].[clientes] ([idclientev])
GO
ALTER TABLE [dbo].[ventas] CHECK CONSTRAINT [FK_ventas_clientes]
GO
ALTER TABLE [dbo].[ventas]  WITH CHECK ADD  CONSTRAINT [FK_ventas_USUARIO2] FOREIGN KEY([Id_usuario])
REFERENCES [dbo].[USUARIO2] ([idUsuario])
GO
ALTER TABLE [dbo].[ventas] CHECK CONSTRAINT [FK_ventas_USUARIO2]
GO
/****** Object:  StoredProcedure [dbo].[Buscar_id_USUARIOS]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Buscar_id_USUARIOS]
@Nombre_y_Apelllidos varchar(50)
as
select * from USUARIO2 
where Nombres_y_Apellidos =@Nombre_y_Apelllidos
order by idUsuario desc





GO
/****** Object:  StoredProcedure [dbo].[buscar_MOVIMIENTOS_DE_KARDEX]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[buscar_MOVIMIENTOS_DE_KARDEX]
@idProducto int
AS BEGIN
SELECT       KARDEX.Fecha ,Producto1.Descripcion ,KARDEX.Motivo as Movimiento, KARDEX.Habia ,KARDEX.Tipo ,KARDEX.Cantidad ,KARDEX.Hay ,USUARIO2.Nombres_y_Apellidos as Cajero ,Grupo_de_Productos.Linea as Categoria
,EMPRESA.Nombre_Empresa,EMPRESA.Logo 
FROM            dbo.KARDEX INNER JOIN Producto1 on Producto1.Id_Producto1=KARDEX.Id_producto inner join USUARIO2 on USUARIO2.idUsuario =KARDEX.Id_usuario 
               cross join EMPRESA
			INNER JOIN Grupo_de_Productos on
Grupo_de_Productos.Idline=Producto1.Id_grupo
						 
WHEre Producto1.Id_Producto1=@idProducto   order by KARDEX.Fecha Desc

END




GO
/****** Object:  StoredProcedure [dbo].[buscar_MOVIMIENTOS_DE_KARDEX_filtros ]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[buscar_MOVIMIENTOS_DE_KARDEX_filtros ]
@fecha date,
@tipo varchar(50),
@Id_usuario int 
AS BEGIN
SELECT       KARDEX.Fecha ,Producto1.Descripcion ,KARDEX.Motivo as Movimiento, KARDEX.Habia ,KARDEX.Tipo ,KARDEX.Cantidad ,KARDEX.Hay ,USUARIO2.Nombres_y_Apellidos as Usuario ,Grupo_de_Productos .Linea as Categoria
,USUARIO2.idUsuario,@fecha as Fecha_consulta, @tipo as Tipo_consulta, EMPRESA.Nombre_Empresa ,EMPRESA.Logo 
 FROM            dbo.KARDEX INNER JOIN Producto1 on Producto1.Id_Producto1=KARDEX.Id_producto inner join USUARIO2 on USUARIO2.idUsuario =KARDEX.Id_usuario 
         INNER JOIN Grupo_de_Productos on Grupo_de_Productos.Idline=Producto1.Id_grupo 
						 CROSS JOIN EMPRESA 
WHEre KARDEX.Id_usuario =@Id_usuario and (KARDEX.Tipo=@tipo or @tipo ='-Todos-') and convert(date,KARDEX.Fecha) =convert(date,@fecha )
END




GO
/****** Object:  StoredProcedure [dbo].[buscar_MOVIMIENTOS_DE_KARDEX_filtros_acumulado]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[buscar_MOVIMIENTOS_DE_KARDEX_filtros_acumulado]
@fecha date,
@tipo varchar(50),
@Id_usuario int 
AS BEGIN
SELECT     Producto1.Descripcion ,KARDEX.Tipo ,sum (KARDEX.Cantidad) Cantidad_Total ,@fecha as fecha, USUARIO2.Nombres_y_Apellidos,@tipo as Tipo_consultado
 FROM            dbo.KARDEX INNER JOIN Producto1 on Producto1.Id_Producto1=KARDEX.Id_producto inner join USUARIO2 on USUARIO2.idUsuario =KARDEX.Id_usuario 
         
				 
WHEre KARDEX.Id_usuario =@Id_usuario and (KARDEX.Tipo=@tipo or @tipo ='-Todos-') and convert(date,KARDEX.Fecha) =convert(date,@fecha )
group by Producto1.Descripcion,KARDEX.Tipo,USUARIO2.Nombres_y_Apellidos
ORDER BY sum (KARDEX.Cantidad) DESC
END




GO
/****** Object:  StoredProcedure [dbo].[buscar_producto_por_descripcion]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[buscar_producto_por_descripcion]
@letra VARCHAR(50)
AS BEGIN
select Id_Producto1,Codigo , Grupo_de_Productos.Linea as Grupo,Descripcion ,Impuesto,Usa_inventarios ,Precio_de_compra AS P_Compra,Precio_mayoreo as P_mayoreo,Se_vende_a as Se_vende_por,Stock_minimo ,Fecha_de_vencimiento as F_vencimiento ,Stock,Precio_de_venta as P_venta 
,Grupo_de_Productos.Idline,A_partir_de 

FROM            dbo.Producto1 
INNER JOIN Grupo_de_Productos on
Grupo_de_Productos.Idline=Producto1.Id_grupo
              
WHEre (dbo.Producto1.Descripcion)+Codigo +Grupo_de_Productos.Linea   LIKE  '%' + @letra+'%' ORDER BY DBO.Producto1.Descripcion  asc
 
END
GO
/****** Object:  StoredProcedure [dbo].[BUSCAR_PRODUCTOS_KARDEX]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[BUSCAR_PRODUCTOS_KARDEX]

@letrab VARCHAR(50)
AS 
SELECT       top 10 Id_Producto1, (Descripcion) AS Descripcion, Imagen, Grupo_de_Productos.Linea, Usa_inventarios, Stock, Precio_de_compra, Fecha_de_vencimiento, Precio_de_venta, Codigo, Se_vende_a, Impuesto, Stock_minimo, Precio_mayoreo, Sub_total_pv, 
                         Sub_total_pm
FROM            dbo.Producto1 
                      	INNER JOIN Grupo_de_Productos on
Grupo_de_Productos.Idline=Producto1.Id_grupo
  
						 where  (Descripcion+Grupo_de_Productos.Linea  + Codigo) LIKE  '%' + @letrab+'%' and Usa_inventarios ='SI'





GO
/****** Object:  StoredProcedure [dbo].[BUSCAR_PRODUCTOS_oka]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[BUSCAR_PRODUCTOS_oka]

@letrab VARCHAR(50)
AS 
BEGIN
SELECT      TOP 10  Id_Producto1,(Descripcion+' /Precio: '+EMPRESA.Moneda   +' '+ convert(varchar(50),Precio_de_venta)  +' /Codigo: '+ Codigo  ) AS Descripcion
, Usa_inventarios, Stock, Precio_de_compra, Precio_de_venta, Codigo, Se_vende_a
,Descripcion as Descripcion2, Codigo 
FROM            dbo.Producto1  cross join EMPRESA 
INNER JOIN Grupo_de_Productos on
Grupo_de_Productos.Idline=Producto1.Id_grupo   
              
where  (Descripcion+' /Precio: '+EMPRESA.Moneda   +' '+ convert(varchar(50),Precio_de_venta)  +' /Codigo: '+ Codigo  ) LIKE  '%' + @letrab+'%' 
end  
GO
/****** Object:  StoredProcedure [dbo].[buscar_productos_vencidos]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[buscar_productos_vencidos]
@letra as varchar(50)
as

select Id_Producto1,Codigo ,Descripcion 

,Fecha_de_vencimiento as F_vencimiento ,Stock,empresa.Nombre_Empresa,empresa.Logo
,datediff(day,Fecha_de_vencimiento,CONVERT(DATE,GETDATE ()))as [Dias Vencidos] from Producto1
cross join EMPRESA 
where   Descripcion +codigo LIKE  '%' + @letra+'%' and Fecha_de_vencimiento <>'NO APLICA' AND Fecha_de_vencimiento <= CONVERT(DATE,GETDATE ()) 
order by (datediff(day,Fecha_de_vencimiento,CONVERT(DATE,GETDATE ()))) asc





GO
/****** Object:  StoredProcedure [dbo].[buscar_usuario]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[buscar_usuario]
@letra varchar(50)
as
select  idUsuario,Nombres_y_Apellidos AS Nombres,Login,Password
,Icono ,Nombre_de_icono ,Correo ,rol  FROM USUARIO2

where Nombres_y_Apellidos + Login      LIKE '%'+ @letra +'%' AND Estado='ACTIVO'





GO
/****** Object:  StoredProcedure [dbo].[buscar_USUARIO_por_correo]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[buscar_USUARIO_por_correo]
@correo VARCHAR(max)

AS 
SELECT        Password 
FROM            dbo.USUARIO2						 
WHEre Correo =@correo





GO
/****** Object:  StoredProcedure [dbo].[CERRAR_CAJA]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[CERRAR_CAJA]
@idcaja as integer,
@fechafin datetime,
@fechacierre datetime


		
as 
update MOVIMIENTOCAJACIERRE set 
Estado ='CAJA CERRADA'
where Id_caja =@idcaja AND Estado='CAJA APERTURADA'








GO
/****** Object:  StoredProcedure [dbo].[disminuir_stock]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[disminuir_stock]
@idproducto as int,
@cantidad as numeric (18,2)
as
update Producto1 set Stock =Stock -@cantidad
where Id_Producto1=@idproducto and Usa_inventarios ='SI' and Stock >=@cantidad 









GO
/****** Object:  StoredProcedure [dbo].[disminuir_stock_en_detalle_de_venta]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[disminuir_stock_en_detalle_de_venta]
@Id_Producto1 as int,
@cantidad as numeric (18,2)

as
update detalle_venta   set Stock=Stock-@cantidad where Id_producto   =@Id_Producto1 AND Stock  <>'Ilimitado'








GO
/****** Object:  StoredProcedure [dbo].[editar_caja]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[editar_caja]
@idcaja integer,
@descripcion varchar(50)

 as       
 if EXISTS (SELECT Descripcion  FROM Caja  where (Descripcion  = @descripcion and Id_Caja  <>@idcaja ) )

RAISERROR ('YA EXISTE UN REGISTRO  CON ESTE NOMBRE, POR FAVOR INGRESE DE NUEVO', 16,1)
else          		
 
update Caja set 
Descripcion  =@descripcion 

where Id_Caja    =@idcaja   

GO
/****** Object:  StoredProcedure [dbo].[editar_detalle_venta_CANTIDAD]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[editar_detalle_venta_CANTIDAD]
@Id_producto int,
@cantidad as numeric(18,2),
@Cantidad_mostrada numeric(18,2),
@Id_venta as int
as
DECLARE @APARTIR_DE as numeric(18,2)
DECLARE @Precio_normal as numeric (18,2)
DECLARE @Precio_pormayor as numeric(18,2)
SET @APARTIR_DE= (SELECT A_partir_de  FROM Producto1  where Producto1.Id_Producto1 = @Id_producto )
SET @Precio_normal =(SELECT Precio_de_venta  FROM Producto1 where Producto1.Id_Producto1 =@Id_producto  )
SET @Precio_pormayor =(SELECT Precio_mayoreo  FROM Producto1 where Producto1.Id_Producto1 = @Id_producto )
PRINT @APARTIR_DE
PRINT @Precio_normal
PRINT @Precio_pormayor

begin
update detalle_venta set 
cantidad=@cantidad ,
Cantidad_mostrada=@Cantidad_mostrada
where detalle_venta.Id_producto =@Id_producto and detalle_venta.idventa =@Id_venta 
end

begin
if EXISTS( SELECT Descripcion,cantidad,Id_producto ,idventa  FROM detalle_venta where cantidad >=@APARTIR_DE and detalle_venta.Id_producto=@Id_producto AND  detalle_venta.idventa =@Id_venta AND detalle_venta.Codigo <>'0')
UPDATE detalle_venta set
preciounitario =@Precio_pormayor 
where detalle_venta.Id_producto =@Id_producto and detalle_venta.idventa =@Id_venta and @Precio_pormayor >0 and @APARTIR_DE >0
end

begin
if EXISTS(SELECT cantidad  FROM detalle_venta where cantidad <@APARTIR_DE and detalle_venta.Id_producto =@Id_producto and detalle_venta.idventa =@Id_venta and detalle_venta.Codigo <>'0')
update detalle_venta set
preciounitario =@Precio_normal 
where detalle_venta.Id_producto =@Id_producto and detalle_venta.idventa =@Id_venta 
end

begin
IF EXISTS(SELECT cantidad FROM detalle_venta WHERE detalle_venta .idventa =@Id_venta and detalle_venta.Codigo ='0')

update detalle_venta set 
Codigo = 0 
where detalle_venta.Codigo='0' and detalle_venta.idventa =@Id_venta 
end
















GO
/****** Object:  StoredProcedure [dbo].[editar_detalle_venta_restar]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROC [dbo].[editar_detalle_venta_restar]
@iddetalle_venta INT,
@cantidad as numeric(18, 2),
@Cantidad_mostrada  numeric(18, 2) ,
  @Id_producto varchar(50)   
    ,@Id_venta  as int

	as
	DECLARE @APARTIR_DE as numeric(18,2)
DECLARE @Precio_normal as numeric (18,2)
DECLARE @Precio_pormayor as numeric(18,2)
SET @APARTIR_DE= (SELECT A_partir_de  FROM Producto1  where Producto1.Id_Producto1 = @Id_producto )
SET @Precio_normal =(SELECT Precio_de_venta  FROM Producto1 where Producto1.Id_Producto1 =@Id_producto  )
SET @Precio_pormayor =(SELECT Precio_mayoreo  FROM Producto1 where Producto1.Id_Producto1 = @Id_producto )
PRINT @APARTIR_DE
PRINT @Precio_normal
PRINT @Precio_pormayor

BEGIN

update detalle_venta set 
cantidad=cantidad-@cantidad 
, Cantidad_mostrada=Cantidad_mostrada-@Cantidad_mostrada
where detalle_venta.Id_producto = @Id_producto and detalle_venta.idventa=@Id_venta
END

begin
delete from detalle_venta where  detalle_venta.idventa =@Id_venta and cantidad <=0
end

begin
if EXISTS( SELECT Descripcion,cantidad,Id_producto ,idventa  FROM detalle_venta where cantidad >=@APARTIR_DE and detalle_venta.Id_producto=@Id_producto AND  detalle_venta.idventa =@Id_venta AND detalle_venta.Codigo <>'0')
UPDATE detalle_venta set
preciounitario =@Precio_pormayor 
where detalle_venta.Id_producto =@Id_producto and detalle_venta.idventa =@Id_venta and @Precio_pormayor >0 and @APARTIR_DE >0
end

begin
if EXISTS(SELECT cantidad  FROM detalle_venta where cantidad <@APARTIR_DE and detalle_venta.Id_producto =@Id_producto and detalle_venta.idventa =@Id_venta and detalle_venta.Codigo <>'0')
update detalle_venta set
preciounitario =@Precio_normal 
where detalle_venta.Id_producto =@Id_producto and detalle_venta.idventa =@Id_venta 
end

begin
IF EXISTS(SELECT cantidad FROM detalle_venta WHERE detalle_venta .idventa =@Id_venta and detalle_venta.Codigo ='0')
update detalle_venta set 
Codigo = 0 
where detalle_venta.Codigo='0' and detalle_venta.idventa =@Id_venta 
end














GO
/****** Object:  StoredProcedure [dbo].[editar_detalle_venta_sumar]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[editar_detalle_venta_sumar]
@Id_producto int,
@cantidad as numeric(18,2),
@Cantidad_mostrada numeric(18,2),
@Id_venta as int
as
DECLARE @APARTIR_DE as numeric(18,2)
DECLARE @Precio_normal as numeric (18,2)
DECLARE @Precio_pormayor as numeric(18,2)
SET @APARTIR_DE= (SELECT A_partir_de  FROM Producto1  where Producto1.Id_Producto1 = @Id_producto )
SET @Precio_normal =(SELECT Precio_de_venta  FROM Producto1 where Producto1.Id_Producto1 =@Id_producto  )
SET @Precio_pormayor =(SELECT Precio_mayoreo  FROM Producto1 where Producto1.Id_Producto1 = @Id_producto )
PRINT @APARTIR_DE
PRINT @Precio_normal
PRINT @Precio_pormayor

begin
update detalle_venta set 
cantidad=cantidad + @cantidad ,
Cantidad_mostrada=Cantidad_mostrada+@Cantidad_mostrada
where detalle_venta.Id_producto =@Id_producto and detalle_venta.idventa =@Id_venta 
end

begin
if EXISTS( SELECT Descripcion,cantidad,Id_producto ,idventa  FROM detalle_venta where cantidad >=@APARTIR_DE and detalle_venta.Id_producto=@Id_producto AND  detalle_venta.idventa =@Id_venta AND detalle_venta.Codigo <>'0')
UPDATE detalle_venta set
preciounitario =@Precio_pormayor 
where detalle_venta.Id_producto =@Id_producto and detalle_venta.idventa =@Id_venta and @Precio_pormayor >0 and @APARTIR_DE >0
end

begin
if EXISTS(SELECT cantidad  FROM detalle_venta where cantidad <@APARTIR_DE and detalle_venta.Id_producto =@Id_producto and detalle_venta.idventa =@Id_venta and detalle_venta.Codigo <>'0')
update detalle_venta set
preciounitario =@Precio_normal 
where detalle_venta.Id_producto =@Id_producto and detalle_venta.idventa =@Id_venta 
end

begin
IF EXISTS(SELECT cantidad FROM detalle_venta WHERE detalle_venta .idventa =@Id_venta and detalle_venta.Codigo ='0')

update detalle_venta set 
Codigo = 0 
where detalle_venta.Codigo='0' and detalle_venta.idventa =@Id_venta 
end
















GO
/****** Object:  StoredProcedure [dbo].[editar_dinero_caja_inicial]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[editar_dinero_caja_inicial]
@Id_caja as integer,
@saldo numeric(18,2)


as
update MOVIMIENTOCAJACIERRE  set  Saldo_queda_en_caja =@saldo
where Id_caja =@Id_caja AND Estado ='CAJA APERTURADA'





GO
/****** Object:  StoredProcedure [dbo].[editar_Empresa]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[editar_Empresa]
@Nombre_Empresa  varchar(50),
@logo as image,
@Impuesto varchar(50),
@Porcentaje_impuesto numeric(18,0),
@Moneda varchar(50),
@Trabajas_con_impuestos VARCHAR(50),
@Modo_de_busqueda VARCHAR(50),
@Carpeta_para_copias_de_seguridad varchar(max),
@Correo_para_envio_de_reportes varchar(50)
as
update  EMPRESA set Nombre_Empresa=@Nombre_Empresa,Logo=@logo ,Impuesto=@Impuesto ,
Porcentaje_impuesto=@Porcentaje_impuesto,Moneda=@Moneda,Trabajas_con_impuestos=@Trabajas_con_impuestos
,Modo_de_busqueda=@Modo_de_busqueda 
,Carpeta_para_copias_de_seguridad =@Carpeta_para_copias_de_seguridad 
,Correo_para_envio_de_reportes =@Correo_para_envio_de_reportes 

GO
/****** Object:  StoredProcedure [dbo].[editar_Grupo]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[editar_Grupo]
@id int,
@Grupo varchar(50)

as
if EXISTS (SELECT Linea FROM Grupo_de_Productos  where Linea = @Grupo and Idline<>@id  )
RAISERROR ('YA EXISTE UN GRUPO CON ESTE NOMBRE, Ingrese de nuevo', 16,1)
else
update  Grupo_de_Productos set Linea=@grupo
where Idline=@id






GO
/****** Object:  StoredProcedure [dbo].[editar_inicio_De_sesion]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[editar_inicio_De_sesion]

 
 @Id_serial_Pc varchar(max),
 @id_usuario int
  as
update Inicios_de_sesion_por_caja set 
Id_usuario =@id_usuario 
where Id_serial_Pc=@Id_serial_Pc 

GO
/****** Object:  StoredProcedure [dbo].[EDITAR_marcan_a]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[EDITAR_marcan_a]

	@e varchar(max),
	@fa varchar(max),
	@f  varchar(max),
	@s varchar(max)
    as
	
    UPDATE Marcan SET E=@e, FA=@fa, F=@f 
	where S=@s 

GO
/****** Object:  StoredProcedure [dbo].[editar_Producto1]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[editar_Producto1]
@Id_Producto1 int,
 @Descripcion varchar(50),
		    @Imagen varchar(50),			         
         
          @Id_grupo INT,
		  @Usa_inventarios varchar(50),
	  @Stock varchar(50),
           @Precio_de_compra numeric(18,2),
           @Fecha_de_vencimiento varchar(50),
           @Precio_de_venta numeric(18,2),
           @Codigo varchar(50),
           @Se_vende_a varchar(50),
           @Impuesto varchar(50),
           @Stock_minimo numeric(18,2),
           @Precio_mayoreo numeric(18,2)
		 	,@A_partir_de numeric(18,2)
as 

if EXISTS (SELECT Descripcion FROM Producto1  where (Descripcion = @Descripcion and Id_Producto1 <>@Id_Producto1 ) )

RAISERROR ('YA EXISTE UN PRODUCTO  CON ESTE NOMBRE, POR FAVOR INGRESE DE NUEVO', 16,1)

else if EXISTS (SELECT Codigo  FROM Producto1  where (Codigo  = @Codigo  and Id_Producto1 <>@Id_Producto1 ))

RAISERROR ('YA EXISTE UN PRODUCTO  CON ESTE CODIGO, POR FAVOR INGRESE DE NUEVO/ SE GENERARA CODIGO AUTOMATICO', 16,1)

else if EXISTS (SELECT Descripcion,Codigo  FROM Producto1  where (Id_Producto1 =@Id_Producto1 ))

update Producto1 set  

      Descripcion =@Descripcion ,
		  Imagen =  @Imagen ,			         
         
         Id_grupo = @Id_grupo 	,
		  Usa_inventarios =@Usa_inventarios ,
		  Stock = @Stock ,
           Precio_de_compra =@Precio_de_compra ,
        Fecha_de_vencimiento =   @Fecha_de_vencimiento ,
          Precio_de_venta = @Precio_de_venta ,
         Codigo =  @Codigo ,
         Se_vende_a =  @Se_vende_a ,
           Impuesto =@Impuesto,
           Stock_minimo =@Stock_minimo ,
         Precio_mayoreo =  @Precio_mayoreo 
		 	,A_partir_de=@A_partir_de 
where id_Producto1=@Id_Producto1










GO
/****** Object:  StoredProcedure [dbo].[editar_usuario]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[editar_usuario]
@idUsuario int,
@nombres varchar(50) ,
@Login varchar(50),
@Password VARCHAR(50),
@Icono image,
@Nombre_de_icono varchar(max),
@Correo varchar(max),
@Rol varchar(max)
as
if EXISTS (SELECT Login,idUsuario FROM USUARIO2 where (Login  = @login  AND idUsuario<>@idUsuario and Estado='ACTIVO') or (Nombres_y_Apellidos   = @nombres  AND idUsuario<>@idUsuario and Estado='ACTIVO'))
raiserror('YA EXISTE UN USUARIO CON ESE LOGIN O CON ESE ICONO, POR FAVOR INGRESE DE NUEVO',16,1 )

ELSE

update USUARIO2 set Nombres_y_Apellidos=@nombres ,Password =@Password , Icono=@Icono ,Nombre_de_icono =@Nombre_de_icono
 ,Correo =@Correo , Rol=@rol , Login =@Login
 where idUsuario=@idUsuario 






GO
/****** Object:  StoredProcedure [dbo].[eliminar_caja]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[eliminar_caja]
@idcaja integer
as
update Caja set
Estado  ='Caja Eliminada'
where Id_Caja    =@idcaja

GO
/****** Object:  StoredProcedure [dbo].[eliminar_Producto1]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[eliminar_Producto1]
@id  int
as
delete from Producto1  where Id_Producto1 =@id 




GO
/****** Object:  StoredProcedure [dbo].[eliminar_usuario]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[eliminar_usuario]
@idusuario int,
@login varchar(50)
as
if exists(select login from USUARIO2 where @login ='admin')
raiserror('El Usuario *Admin* es Inborrable, si se borraria Eliminarias el Acceso al Sistema de porvida, Accion Denegada', 16,1)
else
UPDATE  USUARIO2 SET Estado='ELIMINADO'
 WHERE idUsuario =@idusuario and Login <>'admin'






GO
/****** Object:  StoredProcedure [dbo].[imprimir_inventarios_todos]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[imprimir_inventarios_todos]


AS 
SELECT    Codigo ,Descripcion,Precio_de_compra as Costo,Precio_de_venta as [Precio_Venta], Stock, Stock_minimo as [Stock_Minimo]
,Grupo_de_Productos.Linea  AS Categoria ,Precio_de_compra*Stock as Importe,EMPRESA.Nombre_Empresa,EMPRESA.Logo 
FROM         
						 dbo.Producto1 
						  cross join EMPRESA
						   inner join Grupo_de_Productos on Grupo_de_Productos.Idline=Producto1.Id_grupo 




GO
/****** Object:  StoredProcedure [dbo].[Insertar_caja]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Insertar_caja]

	
	@descripcion varchar(50),


	 @Tema varchar(50),
	  @Serial_PC varchar(50),
	   @Impresora_Ticket varchar(max),
	   @Impresora_A4 varchar(max),
	   @Tipo varchar(50)
    as

if EXISTS (SELECT  Descripcion,Serial_PC      FROM Caja    where  Descripcion=@descripcion and Serial_PC =@Serial_PC   )
RAISERROR ('Ya Existe una Caja con ese Nombre ÃƒÆ’Ã‚Â³ puede ser que ya se haya creado una Caja para Esta Pc, Ingrese un nombre diferente e intente de Nuevo', 16,1)
else

declare @Estado as varchar(50)
set @Estado ='RECIEN CREADA'

    INSERT INTO Caja values 
	(@descripcion,@Tema ,@Serial_PC ,@Impresora_Ticket,@Impresora_A4,@Estado,@Tipo )



GO
/****** Object:  StoredProcedure [dbo].[insertar_cliente]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  procedure [dbo].[insertar_cliente]
@Nombre varchar(MAX),
           @Direccion_para_factura varchar(MAX),
            @Ruc varchar(MAX),                      
            @movil varchar(50),               
            @Cliente varchar(50),
            @Proveedor varchar(50),
			@Estado as varchar(50)
		,@Saldo numeric(18,2)
as
		   BEGIN
if EXISTS (SELECT Nombre  FROM clientes  where Nombre  = @Nombre)
RAISERROR ('YA EXISTE UN REGISTRO CON ESE NOMBRE', 16,1)
else
BEGIN
insert into clientes  values 
            (@Nombre
           ,@Direccion_para_factura
           ,@Ruc
           ,@movil     
          
           ,@Cliente
           ,@Proveedor
		   ,@Estado,
		   @Saldo
            )
			end
			end


GO
/****** Object:  StoredProcedure [dbo].[insertar_DETALLE_cierre_de_caja]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[insertar_DETALLE_cierre_de_caja]
	@fechaini datetime,
	 @fechafin datetime,
	 @fechacierre datetime, 
	  @ingresos numeric(18,2), 
    @egresos numeric(18,2),
	@saldo numeric(18,2),
	@idusuario int,
	 @totalcaluclado numeric(18,2),
	  @totalreal numeric(18,2), 
	 
	 @estado as varchar(50),
	 @diferencia as numeric(18,2)	,
	 @id_caja as int   
  AS BEGIN

if EXISTS (SELECT Estado FROM MOVIMIENTOCAJACIERRE 
 where  MOVIMIENTOCAJACIERRE.Estado='CAJA APERTURADA')
RAISERROR ('Ya Fue Iniciado el Turno de esta Caja', 16,1)
else
BEGIN
    INSERT INTO MOVIMIENTOCAJACIERRE values 
	(@fechaini ,
	 @fechafin ,
	 @fechacierre , 
	  @ingresos , 
    @egresos ,
	@saldo ,
	@idusuario ,
	 @totalcaluclado ,
	  @totalreal , 
	
	 @estado ,
	 @diferencia ,
	 @id_caja )


	 end
	 end






GO
/****** Object:  StoredProcedure [dbo].[insertar_detalle_venta]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[insertar_detalle_venta]
@idventa as integer,
@Id_presentacionfraccionada as int,
@cantidad as numeric(18, 2),
@preciounitario as numeric(18, 2),
@moneda as varchar(50),

@unidades as varchar(50),
@Cantidad_mostrada as numeric(18,2),
@Estado as varchar(50),
@Descripcion varchar(50),
@Codigo varchar(50),
@Stock varchar(50),
@Se_vende_a VARCHAR(50),
@Usa_inventarios VARCHAR(50),
@Costo numeric(18,2)
as
BEGIN
if EXISTS (SELECT Id_producto,idventa   FROM detalle_venta 
inner join Producto1 on Producto1.Id_Producto1=detalle_venta.Id_producto 
  where Producto1. Id_Producto1  = @Id_presentacionfraccionada AND idventa=@idventa  ) 
update detalle_venta set 
cantidad    =cantidad +@cantidad  , 
Cantidad_mostrada=Cantidad_mostrada+@Cantidad_mostrada
where Id_producto =@Id_presentacionfraccionada 


else

BEGIN

insert into detalle_venta 

 values (@idventa,@Id_presentacionfraccionada ,@cantidad,@preciounitario,@moneda,@unidades,@Cantidad_mostrada
,@Estado,@Descripcion,@Codigo,@Stock ,@Se_vende_a ,@Usa_inventarios,@Costo)


END
END















GO
/****** Object:  StoredProcedure [dbo].[insertar_Empresa]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[insertar_Empresa]
@Nombre_Empresa  varchar(50),
@logo as image,
@Impuesto varchar(50),
@Porcentaje_impuesto numeric(18,0),
@Moneda varchar(50),

@Trabajas_con_impuestos VARCHAR(50),
@Modo_de_busqueda VARCHAR(50),
@Carpeta_para_copias_de_seguridad varchar(max),
@Correo_para_envio_de_reportes varchar(50),
@Ultima_fecha_de_copia_de_seguridad varchar(50),

@Ultima_fecha_de_copia_date datetime,
@Frecuencia_de_copias int,
@Estado varchar(50),
@Tipo_de_empresa varchar(50),
@Pais varchar(max),
@Redondeo_de_total varchar(50)

as
if EXISTS (SELECT Nombre_Empresa   FROM EMPRESA   where Nombre_Empresa  = @Nombre_Empresa   )
RAISERROR ('YA EXISTE UNA EMPRESA CON ESE NOMBRE, Ingrese un nombre diferente', 16,1)
else
insert into EMPRESA 
VALUES (@Nombre_Empresa,@logo,@Impuesto,@Porcentaje_impuesto,@Moneda ,@Trabajas_con_impuestos,@Modo_de_busqueda,
@Carpeta_para_copias_de_seguridad,@Correo_para_envio_de_reportes,@Ultima_fecha_de_copia_de_seguridad,
@Ultima_fecha_de_copia_date ,
@Frecuencia_de_copias ,
@Estado ,@Tipo_de_empresa,@Pais,@Redondeo_de_total)



GO
/****** Object:  StoredProcedure [dbo].[Insertar_FORMATO_TICKET]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Insertar_FORMATO_TICKET]

  
	  @Identificador_fiscal varchar(max),
           @Direccion varchar(max),
         
           @Provincia_Departamento_Pais varchar(max),
           @Nombre_de_Moneda varchar(max),
           @Agradecimiento varchar(max),
           @pagina_Web_Facebook varchar(max),
           @Anuncio varchar(max),
	   @Datos_fiscales_de_autorizacion varchar(max),
	   @Por_defecto as varchar(max)
	  
    as
	 DECLARE @Id_Empresa int  = (Select Id_empresa from EMPRESA )
    INSERT INTO Ticket values 

	(  
	@Id_Empresa ,
	  @Identificador_fiscal ,
           @Direccion,
         
           @Provincia_Departamento_Pais,
           @Nombre_de_Moneda ,
           @Agradecimiento ,
           @pagina_Web_Facebook ,
           @Anuncio,
	   @Datos_fiscales_de_autorizacion,
	   @Por_defecto )



GO
/****** Object:  StoredProcedure [dbo].[insertar_Grupo]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[insertar_Grupo]
@Grupo varchar(50),
@Por_defecto varchar(50)
as
if EXISTS (SELECT Linea FROM Grupo_de_Productos  where Linea = @Grupo  )
RAISERROR ('YA EXISTE UN GRUPO CON ESTE NOMBRE, Ingrese de nuevo', 16,1)
else
insert into Grupo_de_Productos  values (@Grupo, @Por_defecto)





GO
/****** Object:  StoredProcedure [dbo].[insertar_inicio_De_sesion]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  CREATE proc [dbo].[insertar_inicio_De_sesion]

 
 @Id_serial_Pc varchar(max)

  as
  declare @id_usuario_nuevo as int
  set @id_usuario_nuevo = (SELECT idUsuario  FROM USUARIO2  )

insert into Inicios_de_sesion_por_caja

values (@Id_serial_Pc,@id_usuario_nuevo )



GO
/****** Object:  StoredProcedure [dbo].[Insertar_marcan]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Insertar_marcan]

	
	@s varchar(MAX),
	@f varchar(MAX),
	@e varchar(MAX),
	@fa varchar(MAX)
    as
	
    INSERT INTO Marcan values 
	(@s,@f,@e ,@fa)



GO
/****** Object:  StoredProcedure [dbo].[insertar_Producto]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[insertar_Producto]   
           --Empezamos a declara primero los parametros para Productos
           @Descripcion varchar(50),
		    @Imagen varchar(50),			         
         
          @Id_grupo as int	,
		  @Usa_inventarios varchar(50),
		   @Stock varchar(50),
           @Precio_de_compra numeric(18,2),
           @Fecha_de_vencimiento varchar(50),
           @Precio_de_venta numeric(18,2),
           @Codigo varchar(50),
           @Se_vende_a varchar(50),
           @Impuesto varchar(50),
           @Stock_minimo numeric(18,2),
           @Precio_mayoreo numeric(18,2)
	,@A_partir_de numeric(18,2),
	--Ahora declaramos los parametros para el Ingreso a Kardex que es donde se controla el Inventario
	@Fecha datetime,
		    @Motivo varchar(200),			               
          @Cantidad as numeric(18,0)	,
	 
	   @Id_usuario as int,	
	   @Tipo as varchar(50),
	   @Estado varchar(50)	,   	   		
		@Id_caja int
		   AS 
		   --Ahora VALIDAMOS para que no se agregen productos con el mismo nombre y codigo de barras
		   BEGIN
		
if EXISTS (SELECT Descripcion,Codigo  FROM Producto1  where Descripcion = @Descripcion and Codigo=@Codigo  )
RAISERROR ('YA EXISTE UN PRODUCTO  CON ESTE NOMBRE/CODIGO, POR FAVOR INGRESE DE NUEVO/ SE GENERARA CODIGO AUTOMATICO', 16,1)
else
BEGIN
DECLARE  @Id_producto  INT
		   INSERT INTO Producto1
     VALUES
		    (
           @Descripcion        
           ,@Imagen         
		    ,@Id_grupo 
		,@Usa_inventarios	,
		@Stock ,
           @Precio_de_compra ,
           @Fecha_de_vencimiento ,
           @Precio_de_venta ,
           @Codigo ,
           @Se_vende_a ,
           @Impuesto ,
           @Stock_minimo ,
           @Precio_mayoreo,
		   @A_partir_de
		 )
		 --Ahora Obtenemos el Id del producto que se acaba de ingresar
		    SELECT  @id_producto = scope_identity()
			 --Ahora Obtenemos los datos del producto ingresado para que sean insertados en la Tabla KARDEX
			  DECLARE @Hay AS numeric(18,2)
		 DECLARE @Habia as numeric(18,2)
		 declare @Costo_unt numeric(18,2)
		
       SET @Hay = (SELECT Stock  FROM Producto1 WHERE Producto1.Id_Producto1   =@Id_producto and Producto1.Usa_inventarios ='SI' )
       SET @Costo_unt = (SELECT Producto1.Precio_de_compra   FROM Producto1 WHERE Producto1.Id_Producto1   =@Id_producto and Producto1.Usa_inventarios ='SI' )		   
       SET @Habia = 0
	   --Ahora vamos a saber si el Producto usa Inventarios o no
		  set @Usa_inventarios = (SELECT Usa_inventarios   FROM Producto1 WHERE Producto1.Id_Producto1   =@Id_producto and Producto1.Usa_inventarios ='SI' )
		 --Ahora en caso si Use inventarios Entonces Pasamos a Insertar datos en la Tabla Kardex
		   if @Usa_inventarios ='SI'
		   BEGIN	 
		   INSERT INTO KARDEX
        VALUES
		    (
        @Fecha ,
		    @Motivo ,			                  
          @Cantidad 	,

	  @Id_producto 	,
	   @Id_usuario ,	
	   @Tipo,		
		@Estado ,@Costo_unt, @Habia ,@Hay ,@Id_caja)
		END
		
END
END





GO
/****** Object:  StoredProcedure [dbo].[insertar_Serializacion]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[insertar_Serializacion]

@Serie varchar (50),
@numeroinicio as varchar (50),
@numerofin as varchar (50),
@Destino as varchar(50),
@tipodoc varchar(50)
,@Por_defecto varchar(50)
as 
if EXISTS (SELECT tipodoc  FROM Serializacion  where  tipodoc= @tipodoc )
RAISERROR ('YA EXISTE ESTE COMPROBANTE INGRESE UNO NUEVO', 16,1)
else

insert into Serializacion  values (@Serie ,
@numeroinicio ,
@numerofin,@Destino ,@tipodoc ,@Por_defecto)




GO
/****** Object:  StoredProcedure [dbo].[insertar_usuario]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[insertar_usuario]
@nombres varchar(50) ,
@Login varchar(50),
@Password VARCHAR(50),
@Icono image,
@Nombre_de_icono varchar(max),
@Correo varchar(max),
@Rol varchar(max),
@Estado varchar(50)
as
if exists (select Login FROM USUARIO2 where Login=@Login and Estado='ACTIVO')
raiserror('YA EXISTE UN USUARIO CON ESE LOGIN O CON ESE ICONO, POR FAVOR INGRESE DE NUEVO',16,1 )
ELSE

insert into USUARIO2 
values(@nombres,@Login,@Password,@Icono,@Nombre_de_icono,@Correo,@Rol,@Estado  )





GO
/****** Object:  StoredProcedure [dbo].[insertar_venta]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[insertar_venta]
@idcliente as integer,
@fecha_venta as datetime,

@nume_documento as varchar(50),
@montototal  as numeric(18,2),
@Tipo_de_pago as varchar(50),
@estado as varchar(50),
@IGV as numeric(18, 1),

@Comprobante as VARCHAR(50),
@id_usuario as int,
@Fecha_de_pago as varchar(50),
@ACCION VARCHAR(50),
@Saldo numeric(18,2),
@Pago_con numeric(18,2),
@Porcentaje_IGV numeric(18,2),
@Id_caja int,
@Referencia_tarjeta varchar(50)

as 
declare @vuelto numeric(18,2)
declare @Efectivo numeric(18,2)
declare @id_numero varchar(50)
declare @Credito numeric(18,2)
declare @Tarjeta numeric(18,2)
SET @vuelto =0
SET @Efectivo =0
SET @id_numero= (select max(idventa)+1 from ventas )
SET @Credito =0
SET @Tarjeta =0
insert into ventas 
values (@idcliente,@fecha_venta,@nume_documento ,@montototal ,@Tipo_de_pago,@estado ,@IGV 
,@Comprobante +' '+ @id_numero ,@id_usuario,@Fecha_de_pago,@ACCION ,@Saldo,@Pago_con,@Porcentaje_IGV,
@Id_caja,@Referencia_tarjeta,
@vuelto ,@Efectivo,@Credito,@Tarjeta )














GO
/****** Object:  StoredProcedure [dbo].[Mostrar_caja_principal]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Mostrar_caja_principal]
as
if Exists(Select MOVIMIENTOCAJACIERRE.Id_caja   from MOVIMIENTOCAJACIERRE inner join 
Caja on caja.Id_Caja=MOVIMIENTOCAJACIERRE.Id_caja where Caja.tipo='PRINCIPAL' )

Select Caja.*, USUARIO2.Nombres_y_Apellidos  ,MAX(MOVIMIENTOCAJACIERRE.idcierrecaja )    
from MOVIMIENTOCAJACIERRE inner join 
Caja on caja.Id_Caja=MOVIMIENTOCAJACIERRE.Id_caja 
inner join USUARIO2 on USUARIO2.idUsuario= MOVIMIENTOCAJACIERRE.Id_usuario 
where Caja.tipo='PRINCIPAL' 
GROUP BY Caja.Serial_PC, Caja.Id_Caja,Caja.Descripcion,Caja.Tema,Caja.Impresora_A4,Caja.Impresora_Ticket 
,Caja.Estado,CAJA.Tipo  , USUARIO2.Nombres_y_Apellidos

else

Select Caja.*, USUARIO2.Nombres_y_Apellidos  from Caja
cross join USUARIO2 
 where tipo='PRINCIPAL' and USUARIO2.Login  ='admin'

GO
/****** Object:  StoredProcedure [dbo].[Mostrar_caja_remota]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Mostrar_caja_remota]
as
if Exists(Select MOVIMIENTOCAJACIERRE.Id_caja   from MOVIMIENTOCAJACIERRE inner join 
Caja on caja.Id_Caja=MOVIMIENTOCAJACIERRE.Id_caja where Caja.tipo='REMOTA' )

Select Caja.*, USUARIO2.Nombres_y_Apellidos  ,MAX(MOVIMIENTOCAJACIERRE.idcierrecaja )    
from MOVIMIENTOCAJACIERRE inner join 
Caja on caja.Id_Caja=MOVIMIENTOCAJACIERRE.Id_caja 
inner join USUARIO2 on USUARIO2.idUsuario= MOVIMIENTOCAJACIERRE.Id_usuario 
where Caja.tipo='REMOTA' 
GROUP BY Caja.Serial_PC, Caja.Id_Caja,Caja.Descripcion,Caja.Tema,Caja.Impresora_A4,Caja.Impresora_Ticket 
,Caja.Estado,CAJA.Tipo  , USUARIO2.Nombres_y_Apellidos

else

Select Caja.*, USUARIO2.Nombres_y_Apellidos  from Caja
cross join USUARIO2 
 where tipo='REMOTA' and USUARIO2.Login  ='admin'

GO
/****** Object:  StoredProcedure [dbo].[mostrar_cajas_por_Serial_de_DiscoDuro]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc  [dbo].[mostrar_cajas_por_Serial_de_DiscoDuro]
@Serial as varchar(50)
as
select Id_Caja  ,Descripcion 
from Caja 
where Serial_PC =@Serial





GO
/****** Object:  StoredProcedure [dbo].[mostrar_descripcion_produco_sin_repetir]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[mostrar_descripcion_produco_sin_repetir]

@buscar varchar(50)
as begin
select TOP 10 Descripcion  from Producto1  Where Descripcion  LIKE  '%' + @buscar +'%'
end





GO
/****** Object:  StoredProcedure [dbo].[mostrar_Empresa]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[mostrar_Empresa]
as
select LOGO, Nombre_Empresa as Empresa ,(Impuesto + ' ('+  CONVERT(VARCHAR(50),Porcentaje_impuesto) + ')') Impuesto 
,Moneda  ,Id_empresa  
,Porcentaje_impuesto  ,Impuesto ,Modo_de_busqueda ,Trabajas_con_impuestos ,Trabajas_con_impuestos,
Correo_para_envio_de_reportes,
Carpeta_para_copias_de_seguridad  , Pais 
from Empresa

GO
/****** Object:  StoredProcedure [dbo].[mostrar_grupos]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[mostrar_grupos]

@buscar varchar(50)
as begin
select Idline,Linea  as Grupo from Grupo_de_Productos  Where Linea  LIKE  '%' + @buscar +'%'
end




GO
/****** Object:  StoredProcedure [dbo].[mostrar_id_venta_por_Id_caja]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[mostrar_id_venta_por_Id_caja]
@Id_caja int
as
select Max(idventa) from ventas 
where Id_caja=@Id_caja 














GO
/****** Object:  StoredProcedure [dbo].[mostrar_inicio_De_sesion]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[mostrar_inicio_De_sesion]
  @id_serial_pc as varchar(max)
  as
select Id_usuario  from Inicios_de_sesion_por_caja 
where Id_serial_Pc =@id_serial_pc 

GO
/****** Object:  StoredProcedure [dbo].[MOSTRAR_Inventarios_bajo_minimo]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[MOSTRAR_Inventarios_bajo_minimo]
AS
select  Codigo,Descripcion,Precio_de_compra AS [Precio_de_Compra],Grupo_de_Productos. linea as Categoria,
 Stock ,Stock_minimo as [Stock_Minimo],Grupo_de_Productos. linea,EMPRESA.Nombre_Empresa,EMPRESA.Logo  
 from Producto1 cross join EMPRESA
				inner join Grupo_de_Productos on Grupo_de_Productos.Idline=Producto1.Id_grupo 
				where Stock <= Stock_minimo 	and Usa_inventarios ='SI'




GO
/****** Object:  StoredProcedure [dbo].[mostrar_inventarios_todos]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROC [dbo].[mostrar_inventarios_todos]

@letra varchar(50)
AS 
SELECT    Codigo ,Descripcion,Precio_de_compra as Costo,Precio_de_venta as [Precio_Venta], Stock, Stock_minimo as [Stock_Minimo]
,Grupo_de_Productos.Linea  AS Categoria ,Precio_de_compra*Stock as Importe,EMPRESA.Nombre_Empresa,EMPRESA.Logo 
FROM         
						 dbo.Producto1 
						  cross join EMPRESA
						   inner join Grupo_de_Productos on Grupo_de_Productos.Idline=Producto1.Id_grupo 
where Descripcion+Codigo   LIKE  '%' + @letra+'%' AND Producto1.Usa_inventarios ='SI'




GO
/****** Object:  StoredProcedure [dbo].[MOSTRAR_MOVIMIENTOS_DE_CAJA_POR_SERIAL]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MOSTRAR_MOVIMIENTOS_DE_CAJA_POR_SERIAL]

@serial varchar(50)

AS
SELECT USUARIO2.Login,USUARIO2.Nombres_y_Apellidos     FROM MOVIMIENTOCAJACIERRE 
inner join USUARIO2 on USUARIO2.idUsuario=MOVIMIENTOCAJACIERRE.Id_usuario
 inner join Caja on Caja.Id_Caja =MOVIMIENTOCAJACIERRE.Id_caja 
 where Caja.Serial_PC = @serial    AND MOVIMIENTOCAJACIERRE.Estado='CAJA APERTURADA' 






GO
/****** Object:  StoredProcedure [dbo].[MOSTRAR_MOVIMIENTOS_DE_CAJA_POR_SERIAL_y_usuario]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[MOSTRAR_MOVIMIENTOS_DE_CAJA_POR_SERIAL_y_usuario]

@serial varchar(50),
@idusuario int

AS
SELECT USUARIO2.Login,USUARIO2.Nombres_y_Apellidos    FROM MOVIMIENTOCAJACIERRE inner join USUARIO2 on USUARIO2.idUsuario=MOVIMIENTOCAJACIERRE.Id_usuario
 inner join Caja on Caja.Id_Caja =MOVIMIENTOCAJACIERRE.Id_caja 
 where Caja.Serial_PC = @serial    AND MOVIMIENTOCAJACIERRE.Estado='CAJA APERTURADA' and MOVIMIENTOCAJACIERRE.Id_usuario =@idusuario and USUARIO2.Estado ='ACTIVO'






GO
/****** Object:  StoredProcedure [dbo].[MOSTRAR_MOVIMIENTOS_DE_KARDEX]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[MOSTRAR_MOVIMIENTOS_DE_KARDEX]

@idProducto int
AS
SELECT       KARDEX.Fecha ,Producto1.Descripcion ,KARDEX.Motivo as Movimiento, KARDEX.Cantidad ,KARDEX.Tipo ,KARDEX.Cantidad ,KARDEX.Hay  as Hay ,USUARIO2.Nombres_y_Apellidos  as Cajero ,Grupo_de_Productos .linea as Categoria, KARDEX.Costo_unt , KARDEX.Total 
,Grupo_de_Productos .linea ,EMPRESA.Nombre_Empresa,EMPRESA.Logo ,Producto1.Stock,convert(numeric(18,2), Producto1.Stock * KARDEX.Costo_unt) as TotalInventario
FROM            dbo.KARDEX INNER JOIN Producto1 on Producto1.Id_Producto1=KARDEX.Id_producto inner join USUARIO2 on USUARIO2.idUsuario =KARDEX.Id_usuario 
             cross join EMPRESA
			 inner join Grupo_de_Productos on Grupo_de_Productos.Idline=Producto1.Id_grupo 
			where    Producto1.Id_Producto1=@idProducto
			
			 order by KARDEX.Id_kardex  Desc 





GO
/****** Object:  StoredProcedure [dbo].[mostrar_permisos_por_usuario_ROL_UNICO]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrar_permisos_por_usuario_ROL_UNICO]
@LOGIN VARCHAR(50)
as 
select 
USUARIO2.Rol 
from USUARIO2 
where USUARIO2.LOGIN =@LOGIN and USUARIO2.Estado ='ACTIVO'





GO
/****** Object:  StoredProcedure [dbo].[mostrar_productos_agregados_a_venta]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[mostrar_productos_agregados_a_venta]
@idventa as int
as
SELECT      detalle_venta . Codigo ,( detalle_venta .Descripcion ) as Producto,
 dbo.detalle_venta.Cantidad_mostrada as Cant, 
dbo.detalle_venta.preciounitario as P_Unit ,
convert(numeric(18,2),dbo.detalle_venta.Total_a_pagar) as Importe,
						  detalle_venta .Id_producto  ,DBO.detalle_venta.iddetalle_venta ,dbo.ventas.Estado 
						 , detalle_venta .Stock ,dbo.detalle_venta.cantidad ,ventas.idclientev 
						 , detalle_venta .Stock ,detalle_venta .Stock ,Usa_inventarios ,
						 Se_vende_a ,detalle_venta.idventa 
FROM            dbo.detalle_venta INNER JOIN
                         ventas ON dbo.detalle_venta.idventa = ventas.idventa 
where dbo.detalle_venta .idventa =@idventa AND detalle_venta.cantidad >0 order by 
detalle_venta.iddetalle_venta desc














GO
/****** Object:  StoredProcedure [dbo].[mostrar_productos_vencidos]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[mostrar_productos_vencidos]
as

select Id_Producto1,Codigo ,Descripcion 

,Fecha_de_vencimiento as F_vencimiento ,Stock
,datediff(day,Fecha_de_vencimiento,CONVERT(DATE,GETDATE ()))as [Dias Vencidos] from Producto1 

where   Fecha_de_vencimiento <>'NO APLICA' AND Fecha_de_vencimiento <= CONVERT(DATE,GETDATE ()) 





GO
/****** Object:  StoredProcedure [dbo].[mostrar_productos_vencidos_en_menos_de_30_dias]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[mostrar_productos_vencidos_en_menos_de_30_dias]

as

select Id_Producto1,Codigo ,Descripcion 

,Fecha_de_vencimiento as F_vencimiento ,Stock
,(datediff(day,Fecha_de_vencimiento,CONVERT(DATE,GETDATE ())))*(-1)as [Dias a Vencer] from Producto1 

where   Fecha_de_vencimiento <>'NO APLICA' AND Fecha_de_vencimiento > CONVERT(DATE,GETDATE ()) and (datediff(day,Fecha_de_vencimiento,CONVERT(DATE,GETDATE ())))*(-1) <=30
order by (datediff(day,Fecha_de_vencimiento,CONVERT(DATE,GETDATE ())))*(-1) asc




GO
/****** Object:  StoredProcedure [dbo].[mostrar_stock_de_detalle_de_ventas]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[mostrar_stock_de_detalle_de_ventas]
@Id_producto int 
as
select detalle_venta.Stock,detalle_venta.Descripcion   from ventas 
inner join detalle_venta on detalle_venta.idventa=ventas.idventa  
where ventas.Estado ='EN ESPERA' and detalle_venta.Id_producto =@Id_producto and detalle_venta.Usa_inventarios ='SI'














GO
/****** Object:  StoredProcedure [dbo].[mostrar_usuario]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrar_usuario]
as
select  idUsuario,Nombres_y_Apellidos AS Nombres,Login,Password
,Icono ,Nombre_de_icono ,Correo ,rol  FROM USUARIO2 Where Estado='ACTIVO'



GO
/****** Object:  StoredProcedure [dbo].[restaurar_caja]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[restaurar_caja]
@idcaja integer
as
update Caja set
Estado  ='Caja Restaurada'
where Id_Caja    =@idcaja

GO
/****** Object:  StoredProcedure [dbo].[validar_usuario]    Script Date: 26/12/2021 18:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[validar_usuario]

@password varchar(50),
@login varchar(50)
as
select * from USUARIO2
where  Password   = @password and Login=@Login and Estado ='ACTIVO'





GO
USE [master]
GO
ALTER DATABASE [SistemaContable] SET  READ_WRITE 
GO
