using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Punto_de_venta.ConexionDt;
using System.Windows.Forms;
using System.Data;
using Punto_de_venta.Logica;

namespace Punto_de_venta.Datos
{
    class Editar_datos
    {
        int idcaja;

        public bool InsertarControlPorPagar(Lcontrolpagos parametros)
        {
            try
            {
                Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("insertarPago", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Descripcion", parametros.Detalle);
                cmd.Parameters.AddWithValue("@Fecha_registro", parametros.Fecha);
                cmd.Parameters.AddWithValue("@Fecha_vencimiento", parametros.Fecha);
                cmd.Parameters.AddWithValue("@Total", 0);
                cmd.Parameters.AddWithValue("@Pago", parametros.efectivo);
                cmd.Parameters.AddWithValue("@Estado", "PAGO");
                cmd.Parameters.AddWithValue("@Id_caja", idcaja);
                cmd.Parameters.AddWithValue("@Id_Proveedor", parametros.IdProveedor);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        public static void cambio_de_Caja(int idcaja, int idventa)
        {
            ConexionData.abrir();
            SqlCommand cmd = new SqlCommand("cambio_de_Caja", ConexionData.conectar);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idcaja", idcaja);
            cmd.Parameters.AddWithValue("@idventa", idventa);
            cmd.ExecuteNonQuery();
            ConexionData.cerrar();
  
        }
        public static void ingresar_nombre_a_venta_en_espera(int idventa, string nombre)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("ingresar_nombre_a_venta_en_espera", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idventa", idventa);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                cmd.ExecuteNonQuery();
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        public static bool editar_Conceptos(int idconcepto, string descripcion)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("editar_Conceptos", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_concepto", idconcepto);
                cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                cmd.ExecuteNonQuery();
                ConexionData.cerrar();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public static bool editar_dinero_caja_inicial(int idcaja, double saldo)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("editar_dinero_caja_inicial", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_caja", idcaja);
                cmd.Parameters.AddWithValue("@saldo", saldo);
                cmd.ExecuteNonQuery();
                ConexionData.cerrar();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool editar_Proveedores(Lproveedores parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("editar_Proveedores", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProveedor", parametros.IdProveedor);
                cmd.Parameters.AddWithValue("@Nombre", parametros.Nombre);
                cmd.Parameters.AddWithValue("@Direccion", parametros.Direccion);
                cmd.Parameters.AddWithValue("@IdentificadorFiscal", parametros.IdentificadorFiscal);
                cmd.Parameters.AddWithValue("@Celular", parametros.Celular);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        public bool restaurar_Proveedores(Lproveedores parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("restaurar_Proveedores", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProveedor", parametros.IdProveedor);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }

        }
        public bool restaurar_clientes(Lclientes parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("restaurar_clientes", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idcliente", parametros.idcliente);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }

        }
        public bool editar_caja_impresoras(Limpresoras parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("editar_caja_impresoras", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcaja", parametros.idcaja);
                cmd.Parameters.AddWithValue("@Impresora_Ticket", parametros.Impresora_Ticket);
                cmd.Parameters.AddWithValue("@Impresora_A4", parametros.Impresora_A4);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        public bool editarRespaldos(Lempresa parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("editarRespaldos", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Carpeta_para_copias_de_seguridad", parametros.Carpeta_para_copias_de_seguridad);
                cmd.Parameters.AddWithValue("@Ultima_fecha_de_copia_de_seguridad", parametros.Ultima_fecha_de_copia_de_seguridad);
                cmd.Parameters.AddWithValue("@Ultima_fecha_de_copia_date", parametros.Ultima_fecha_de_copia_date);
                cmd.Parameters.AddWithValue("@Frecuencia_de_copias", parametros.Frecuencia_de_copias);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        public bool editarCorreoBase(Lcorreo p)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("editar_correo", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Correo",  p.Correo);
                cmd.Parameters.AddWithValue("@Password", p.Password);
                cmd.Parameters.AddWithValue("@Estado_De_envio", p.Estado);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
 
        public bool cerrarCaja(Lmcaja parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("cerrarCaja", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fechafin", parametros.fechafin);
                cmd.Parameters.AddWithValue("@fechacierre", parametros.fechacierre);
                cmd.Parameters.AddWithValue("@ingresos", parametros.ingresos);
                cmd.Parameters.AddWithValue("@egresos", parametros.egresos);
                cmd.Parameters.AddWithValue("@Saldo_queda_en_caja", parametros.Saldo_queda_en_caja);
                cmd.Parameters.AddWithValue("@Id_usuario", parametros.Id_usuario);
                cmd.Parameters.AddWithValue("@Total_calculado", parametros.Total_calculado);
                cmd.Parameters.AddWithValue("@Total_real", parametros.Total_real);
                cmd.Parameters.AddWithValue("@Estado", parametros.Estado);
                cmd.Parameters.AddWithValue("@Diferencia", parametros.Diferencia);
                cmd.Parameters.AddWithValue("@Id_caja", parametros.Id_caja);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return true;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        public bool editarMarcan(LMarcan parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("EDITAR_marcan_a", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@e", parametros.E);
                cmd.Parameters.AddWithValue("@fa", parametros.FA);
                cmd.Parameters.AddWithValue("@f", parametros.F);
                cmd.Parameters.AddWithValue("@s", parametros.S);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        //Clientes
        public bool editar_clientes(Lclientes parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("editar_clientes", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idcliente", parametros.idcliente);
                cmd.Parameters.AddWithValue("@Nombre", parametros.Nombre);
                cmd.Parameters.AddWithValue("@Direccion", parametros.Direccion);
                cmd.Parameters.AddWithValue("@IdentificadorFiscal", parametros.IdentificadorFiscal);
                cmd.Parameters.AddWithValue("@Celular", parametros.Celular);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        public bool disminuirSaldocliente(Lclientes parametros, double monto)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("disminuirSaldocliente", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcliente", parametros.idcliente);
                cmd.Parameters.AddWithValue("@monto", monto);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        public bool aumentarSaldocliente(Lclientes parametros, double monto)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("aumentar_saldo_a_cliente", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcliente", parametros.idcliente);
                cmd.Parameters.AddWithValue("@Saldo", monto);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        public bool disminuirSaldoProveedor(Lproveedores parametros, double monto)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("disminuirSaldoproveedor", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idProveedor", parametros.IdProveedor);
                cmd.Parameters.AddWithValue("@monto", monto);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }

        //Caja
        public bool EditarBascula(Lcaja parametros)
        {
            try
            {
                Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
                ConexionData.abrir();
                
                SqlCommand cmd = new SqlCommand("EditarBascula", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcaja", parametros.Id_Caja);
                cmd.Parameters.AddWithValue("@Puerto", parametros.PuertoBalanza);
                cmd.Parameters.AddWithValue("@Estado", parametros.EstadoBalanza);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        public bool EditarTemaCaja(Lcaja parametros)
        {
            try
            {
                Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("EditarTemaCaja", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcaja", idcaja);
                cmd.Parameters.AddWithValue("@tema", parametros.Tema);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }

        //Detalleventa
        public bool aplicar_precio_mayoreo(LdetalleVenta parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("aplicar_precio_mayoreo", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idproducto", parametros.Id_producto);
                cmd.Parameters.AddWithValue("@iddetalleventa", parametros.iddetalle_venta);
                cmd.ExecuteNonQuery();
                return true;


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        public bool editarPrecioVenta(LdetalleVenta parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("editarPrecioVenta", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iddetalleventa", parametros.iddetalle_venta);
                cmd.Parameters.AddWithValue("@precio", parametros.preciounitario);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        public bool DetalleventaDevolucion(LdetalleVenta parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("DetalleventaDevolucion", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iddetalle", parametros.iddetalle_venta);
                cmd.Parameters.AddWithValue("@cantidad", parametros.cantidad);
                cmd.Parameters.AddWithValue("@cantidadMostrada", parametros.Cantidad_mostrada);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        public bool AumentarStockDetalle(LdetalleVenta parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("aumentar_stock_en_detalle_de_venta", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Producto1", parametros.Id_producto);
                cmd.Parameters.AddWithValue("@cantidad", parametros.cantidad);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        //Productos
        public bool aumentarStock(Lproductos parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("aumentarStock", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idproducto", parametros.Id_Producto1);
                cmd.Parameters.AddWithValue("@cantidad", parametros.Stock);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        public bool disminuir_stock(Lproductos parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("disminuir_stock", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idproducto", parametros.Id_Producto1);
                cmd.Parameters.AddWithValue("@cantidad", parametros.Stock);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }

        public bool EditarPreciosProductos(Lproductos parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("EditarPreciosProductos", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idproducto", parametros.Id_Producto1);
                cmd.Parameters.AddWithValue("@precioventa", parametros.Precio_de_venta);
                cmd.Parameters.AddWithValue("@costo", parametros.Precio_de_compra);
                cmd.Parameters.AddWithValue("@preciomayoreo", parametros.Precio_mayoreo);
                cmd.Parameters.AddWithValue("@cantidadAgregada", parametros.Stock);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }

        //Ventas
        public bool EditarVenta(Lventas parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("editarVenta", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idventa", parametros.idventa);
                cmd.Parameters.AddWithValue("@monto", parametros.Monto_total);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }

        }
    }
}
