USE [master]
GO
/****** Object:  Database [SistemaContable]    Script Date: 16/10/2021 9:59:10 a. m. ******/
CREATE DATABASE [SistemaContable]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SistemaContable', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SistemaContable.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SistemaContable_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SistemaContable_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SistemaContable] SET COMPATIBILITY_LEVEL = 150
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
ALTER DATABASE [SistemaContable] SET RECOVERY FULL 
GO
ALTER DATABASE [SistemaContable] SET  MULTI_USER 
GO
ALTER DATABASE [SistemaContable] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SistemaContable] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SistemaContable] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SistemaContable] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SistemaContable] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SistemaContable] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SistemaContable', N'ON'
GO
ALTER DATABASE [SistemaContable] SET QUERY_STORE = OFF
GO
USE [SistemaContable]
GO
/****** Object:  Table [dbo].[Caja]    Script Date: 16/10/2021 9:59:15 a. m. ******/
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
 CONSTRAINT [PK_Caja] PRIMARY KEY CLUSTERED 
(
	[Id_Caja] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grupo_de_Productos]    Script Date: 16/10/2021 9:59:17 a. m. ******/
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
/****** Object:  Table [dbo].[KARDEX]    Script Date: 16/10/2021 9:59:17 a. m. ******/
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
/****** Object:  Table [dbo].[MovimientoCajaCierre]    Script Date: 16/10/2021 9:59:17 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovimientoCajaCierre](
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
	[Diferencia] [varchar](50) NULL,
	[Id_caja] [int] NULL,
 CONSTRAINT [PK_MovimientoCajaCierre] PRIMARY KEY CLUSTERED 
(
	[idcierrecaja] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto1]    Script Date: 16/10/2021 9:59:18 a. m. ******/
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
/****** Object:  Table [dbo].[usuario]    Script Date: 16/10/2021 9:59:18 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombres_y_Apellidos] [varchar](50) NULL,
	[Login] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Icono] [image] NULL,
	[Nombre_de_icono] [varchar](max) NULL,
	[Correo] [varchar](max) NULL,
	[Rol] [varchar](max) NULL,
	[Estado] [varchar](50) NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[MovimientoCajaCierre]  WITH CHECK ADD  CONSTRAINT [FK_MovimientoCajaCierre_Caja] FOREIGN KEY([Id_caja])
REFERENCES [dbo].[Caja] ([Id_Caja])
GO
ALTER TABLE [dbo].[MovimientoCajaCierre] CHECK CONSTRAINT [FK_MovimientoCajaCierre_Caja]
GO
ALTER TABLE [dbo].[MovimientoCajaCierre]  WITH CHECK ADD  CONSTRAINT [FK_MovimientoCajaCierre_usuario] FOREIGN KEY([Id_usuario])
REFERENCES [dbo].[usuario] ([idUsuario])
GO
ALTER TABLE [dbo].[MovimientoCajaCierre] CHECK CONSTRAINT [FK_MovimientoCajaCierre_usuario]
GO
/****** Object:  StoredProcedure [dbo].[buscar_usuario]    Script Date: 16/10/2021 9:59:18 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[buscar_usuario]
@letra varchar(50)
as
select idUsuario,Nombres_y_Apellidos AS [Nombre],Login,Password,Icono,Nombre_de_icono
,Correo,rol FROM usuario

where Nombres_y_Apellidos + Login LIKE '%' + @letra +'%' AND Estado='ACTIVO'
GO
/****** Object:  StoredProcedure [dbo].[buscar_usuario_por_correo]    Script Date: 16/10/2021 9:59:18 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[buscar_usuario_por_correo]
@correo varchar(max)
as
select Password 
from dbo.usuario
where Correo = @correo
GO
/****** Object:  StoredProcedure [dbo].[Cerrar_caja]    Script Date: 16/10/2021 9:59:18 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Cerrar_caja]
@idcaja as integer,
@fechafin datetime,
@fechacierre datetime
as
update MovimientoCajaCierre set Estado ='CAJA CERRADA'
where Id_caja=@idcaja and Estado='CAJA APERTURADA'
GO
/****** Object:  StoredProcedure [dbo].[editar_dinero_caja_inicial]    Script Date: 16/10/2021 9:59:18 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[editar_dinero_caja_inicial]
@id_caja as integer,
@saldo numeric(18,2)
as
update MovimientoCajaCierre set Saldo_queda_en_caja = @saldo
where Id_caja=@id_caja and Estado='CAJA APERTURADA'



delete from MovimientoCajaCierre

select *from usuario
GO
/****** Object:  StoredProcedure [dbo].[editar_Grupo]    Script Date: 16/10/2021 9:59:18 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[editar_Grupo]
@id int,
@Grupo varchar(50)
as
if EXISTS (SELECT Linea FROM Grupo_de_Productos  where Linea = @Grupo and Idline <>@id )
RAISERROR ('YA EXISTE UN GRUPO CON ESTE NOMBRE, Ingrese de nuevo', 16,1)
else
update Grupo_de_Productos  set Linea=@Grupo
where Idline=@id
GO
/****** Object:  StoredProcedure [dbo].[editar_Producto1]    Script Date: 16/10/2021 9:59:18 a. m. ******/
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
   @Precio_mayoreo numeric(18,2),
   @A_partir_de numeric(18,2)
as 

if EXISTS (SELECT Descripcion FROM Producto1  where (Descripcion = @Descripcion and Id_Producto1 <>@Id_Producto1 ) )

RAISERROR ('YA EXISTE UN PRODUCTO  CON ESTE NOMBRE, POR FAVOR INGRESE DE NUEVO', 16,1)

else if EXISTS (SELECT Codigo  FROM Producto1  where (Codigo  = @Codigo  and Id_Producto1 <>@Id_Producto1 ))

RAISERROR ('YA EXISTE UN PRODUCTO  CON ESTE CODIGO, POR FAVOR INGRESE DE NUEVO/ SE GENERARA CODIGO AUTOMATICO', 16,1)

else if EXISTS (SELECT Descripcion,Codigo  FROM Producto1  where (Id_Producto1 =@Id_Producto1 ))

update Producto1 set  

    Descripcion =@Descripcion,
    Imagen =  @Imagen,		         
    Id_grupo = @Id_grupo,
	Usa_inventarios =@Usa_inventarios,
	Stock = @Stock,
    Precio_de_compra =@Precio_de_compra,
    Fecha_de_vencimiento =   @Fecha_de_vencimiento,
    Precio_de_venta = @Precio_de_venta,
    Codigo =  @Codigo,
    Se_vende_a =  @Se_vende_a,
    Impuesto =@Impuesto,
    Stock_minimo =@Stock_minimo,
    Precio_mayoreo =  @Precio_mayoreo,
	A_partir_de=@A_partir_de 
where id_Producto1=@Id_Producto1

GO
/****** Object:  StoredProcedure [dbo].[editar_usuario]    Script Date: 16/10/2021 9:59:18 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[editar_usuario]
@idUsuario int,
@nombres varchar(50),
@Login varchar(50),
@Password varchar(50),
@Icono image,
@Nombre_de_icono varchar(max),
@Correo varchar(max),
@Rol varchar(max)
as

if EXISTS (select Login, idUsuario FROM usuario where (Login=@Login AND idUsuario <>@idUsuario AND Estado = 'ACTIVO') or (Nombres_y_Apellidos =@nombres AND idUsuario <>@idUsuario and Estado = 'ACTIVO'))
raiserror('YA EXISTE UN USUARIO CON ESE LOGIN O ESE ICONO, POR FAVOR INGRESE DE NUEV0', 16,1)
else

update usuario set Nombres_y_Apellidos=@nombres, Password=@Password, Icono=@Icono,Nombre_de_icono=@Nombre_de_icono,
Correo=@Correo,Rol=@rol, Login=@Login
where idUsuario=@idUsuario
GO
/****** Object:  StoredProcedure [dbo].[eliminar_Producto1]    Script Date: 16/10/2021 9:59:18 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[eliminar_Producto1]
@id  int
as
delete from Producto1  where Id_Producto1 =@id 
GO
/****** Object:  StoredProcedure [dbo].[eliminar_usuario]    Script Date: 16/10/2021 9:59:18 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[eliminar_usuario]
@idusuario int,
@login varchar(50)
as
if exists(select login from usuario where @login='Admin')
raiserror('El Usuario "Admin" es Inborrable, si se borraría Eliminarias el Acceso al Sistema de porvida, Acción Denegada', 16,1)
else
UPDATE usuario SET Estado='ELIMINADO'
WHERE idUsuario = @idusuario and Login <>'Admin'
GO
/****** Object:  StoredProcedure [dbo].[insertar_DETALLE_cierre_de_caja]    Script Date: 16/10/2021 9:59:18 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[insertar_DETALLE_cierre_de_caja]
   @fechaini datetime,
   @fechafin datetime,
   @fechacierre datetime,
   @ingresos numeric(18,2),
   @egresos numeric(18,2),
   @saldo numeric(18,2),
   @idusuario int,
   @totalcalculado numeric(18,2),
   @totalreal numeric(18,2),
   @estado as varchar(50),
   @diferencia as numeric(18,2),
   @idcaja as int
AS BEGIN

if exists (select Estado from MovimientoCajaCierre
where MovimientoCajaCierre.Estado='CAJA APERTURADA')
RAISERROR('Ya Fue Iniciado El Turno de esta Caja', 16,1)
else
BEGIN
   insert into MovimientoCajaCierre values
    (@fechaini,
   @fechafin,
   @fechacierre,
   @ingresos,
   @egresos,
   @saldo,
   @idusuario ,
   @totalcalculado,
   @totalreal,
   @estado,
   @diferencia,
   @idcaja)
   end
   end
GO
/****** Object:  StoredProcedure [dbo].[insertar_Grupo]    Script Date: 16/10/2021 9:59:18 a. m. ******/
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
/****** Object:  StoredProcedure [dbo].[insertar_Producto]    Script Date: 16/10/2021 9:59:18 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[insertar_Producto]   
           --Empezamos a declara primero los parametros para Productos

      @Descripcion varchar(50),
      @Imagen varchar(50),       
      @Id_grupo as int,
      @Usa_inventarios varchar(50),
      @Stock varchar(50),
      @Precio_de_compra numeric(18,2),
      @Fecha_de_vencimiento varchar(50),
      @Precio_de_venta numeric(18,2),
      @Codigo varchar(50),
	  @Se_vende_a varchar(50),
      @Impuesto varchar(50),
      @Stock_minimo numeric(18,2),
      @Precio_mayoreo numeric(18,2),
	  @A_partir_de numeric(18,2),
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
RAISERROR ('YA EXISTE UN PRODUCTO CON ESTE NOMBRE/CODIGO, POR FAVOR INGRESE DE NUEVO/ SE GENERARA CODIGO AUTOMATICO', 16,1)
else
BEGIN
DECLARE  @Id_producto  INT
	 INSERT INTO Producto1
     VALUES
		    (
       @Descripcion,      
       @Imagen,         
       @Id_grupo, 
	   @Usa_inventarios,
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
	   SELECT  
	   @id_producto = scope_identity()			 --Ahora Obtenemos los datos del producto ingresado para que sean insertados en la Tabla KARDEX
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
      @Cantidad,
	  @Id_producto 	,
	  @Id_usuario ,	
	  @Tipo,		
	  @Estado,
	  @Costo_unt, 
	  @Habia,
	  @Hay,
	  @Id_caja)
		END
		
END
END
GO
/****** Object:  StoredProcedure [dbo].[insertar_usuario]    Script Date: 16/10/2021 9:59:18 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[insertar_usuario]
@nombres varchar(50),
@Login varchar(50),
@Password varchar(50),
@Icono image,
@Nombre_de_icono varchar(max),
@Correo varchar(max),
@Rol varchar(max),
@Estado varchar(50)
as

if exists (select Login FROM usuario where Login=@Login AND Estado='ACTIVO')
raiserror('YA EXISTE UN USUARIO CON ESE LOGIN O ESE ICONO, POR FAVOR INGRESE DE NUEV0', 16,1)
else

insert into usuario
values(@nombres,@Login,@Password,@Icono,@Nombre_de_icono,@Correo,@Rol,@Estado)
GO
/****** Object:  StoredProcedure [dbo].[mostrar_cajas_por_Serial_de_DiscoDuro]    Script Date: 16/10/2021 9:59:18 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[mostrar_cajas_por_Serial_de_DiscoDuro]
@Serial as varchar(50)
as
select Id_Caja, Descripcion from Caja
where Serial_PC =@Serial
GO
/****** Object:  StoredProcedure [dbo].[mostrar_grupos]    Script Date: 16/10/2021 9:59:18 a. m. ******/
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
/****** Object:  StoredProcedure [dbo].[mostrar_movimientos_de_Caja_por_Serial]    Script Date: 16/10/2021 9:59:18 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrar_movimientos_de_Caja_por_Serial]
@serial as varchar(50)
as
select usuario.Login,usuario.Nombres_y_Apellidos FROM MovimientoCajaCierre
INNER JOIN usuario ON USUARIO.idUsuario=MovimientoCajaCierre.Id_usuario
INNER JOIN Caja ON Caja.Id_Caja =MovimientoCajaCierre.Id_caja
where Caja.Serial_PC = @serial and MovimientoCajaCierre.Estado='CAJA APERTURADA'
GO
/****** Object:  StoredProcedure [dbo].[mostrar_movimientos_de_Caja_por_Serial_y_usuario]    Script Date: 16/10/2021 9:59:18 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrar_movimientos_de_Caja_por_Serial_y_usuario]
@serial as varchar(50),
@idusuario int
as
select usuario.Login,usuario.Nombres_y_Apellidos FROM MovimientoCajaCierre
INNER JOIN usuario ON USUARIO.idUsuario=MovimientoCajaCierre.Id_usuario
INNER JOIN Caja ON Caja.Id_Caja =MovimientoCajaCierre.Id_caja
where Caja.Serial_PC = @serial and MovimientoCajaCierre.Estado='CAJA APERTURADA' and MovimientoCajaCierre.Id_usuario=@idusuario and usuario.Estado='ACTIVO'
GO
/****** Object:  StoredProcedure [dbo].[mostrar_permisos_por_usuario_Rol_Unico]    Script Date: 16/10/2021 9:59:18 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[mostrar_permisos_por_usuario_Rol_Unico]
@LOGIN varchar(50)
as
select
usuario.Rol
from usuario
where usuario.Login = @LOGIN AND usuario.Estado = 'ACTIVO'
GO
/****** Object:  StoredProcedure [dbo].[mostrar_usuario]    Script Date: 16/10/2021 9:59:18 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[mostrar_usuario]
as
select idUsuario,Nombres_y_Apellidos AS [Nombre],Login,Password,Icono,Nombre_de_icono
,Correo,rol FROM usuario where Estado = 'ACTIVO'
GO
/****** Object:  StoredProcedure [dbo].[validar_usuario]    Script Date: 16/10/2021 9:59:18 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[validar_usuario]
@password varchar(50),
@login varchar(50)
as
select * FROM usuario

where Password = @password AND Login=@Login  AND Estado='ACTIVO'
GO
USE [master]
GO
ALTER DATABASE [SistemaContable] SET  READ_WRITE 
GO
