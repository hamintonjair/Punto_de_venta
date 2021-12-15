using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Punto_de_venta.ConexionDt;
using Punto_de_venta.Logica;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_venta.Datos
{
    class Obtener_datos
    {
        private static string serialPC;
        private static int idcaja;

        public static void Obtener_id_caja_PorSerial(ref int idcaja)
        {

            try
            {
           
                Bases.Obtener_serialPC(ref serialPC);
                ConexionData.abrir();
                SqlCommand com = new SqlCommand("mostrar_cajas_por_Serial_de_DiscoDuro", ConexionData.conectar);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Serial", serialPC);
                idcaja = Convert.ToInt32(com.ExecuteScalar());
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }
        }

        public static void mostrar_ventas_en_espera_con_fecha_y_monto(ref DataTable dt)
        {

            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_ventas_en_espera_con_fecha_y_monto", ConexionData.conectar);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }
        }
        public static void mostrar_productos_agregados_a_ventas_en_espera(ref DataTable dt, int idventa)
        {

            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_productos_agregados_a_ventas_en_espera", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idventa", idventa);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }
        }
        public static void buscar_conceptos(ref DataTable dt, string buscador)
        {

            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscar_conceptos", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", buscador);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }
        }

        public static void mostrar_gastos_por_turnos(int idcaja, DateTime fi, DateTime ff, ref DataTable dt)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_gastos_por_turnos", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idcaja", idcaja);
                da.SelectCommand.Parameters.AddWithValue("@fi", fi);
                da.SelectCommand.Parameters.AddWithValue("@ff", ff);

                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }

        }
        public static void mostrar_ingresos_por_turnos(int idcaja, DateTime fi, DateTime ff, ref DataTable dt)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_ingresos_por_turnos", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idcaja", idcaja);
                da.SelectCommand.Parameters.AddWithValue("@fi", fi);
                da.SelectCommand.Parameters.AddWithValue("@ff", ff);

                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }

        }

        public static void mostrar_cierre_de_caja_pendiente(ref DataTable dt)
        {
            Obtener_id_caja_PorSerial(ref idcaja);

            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_cierre_de_caja_pendiente", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idcaja", idcaja);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }

        }
        public static void mostrar_inicio_De_sesion(ref int idusuario)
        {
            Bases.Obtener_serialPC(ref serialPC);
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("mostrar_inicio_De_sesion", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_serial_pc", serialPC);
                idusuario = Convert.ToInt32(cmd.ExecuteScalar());
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void mostrarUsuariosSesion(ref DataTable dt)
        {
            try
            {
                Bases.Obtener_serialPC(ref serialPC);
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_inicio_De_sesion", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@id_serial_pc", serialPC);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void mostrar_ventas_en_efectivo_por_turno(int idcaja, DateTime fi, DateTime ff, ref double monto)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("mostrar_ventas_en_efectivo_por_turno", ConexionData.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                da.Parameters.AddWithValue("@fi", fi);
                da.Parameters.AddWithValue("@ff", ff);
                monto = Convert.ToDouble(da.ExecuteScalar());
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                monto = 0;

            }

        }
        public static void mostrar_cobros_en_efectivo_por_turno(int idcaja, DateTime fi, DateTime ff, ref double monto)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("mostrar_cobros_en_efectivo_por_turno", ConexionData.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                da.Parameters.AddWithValue("@fi", fi);
                da.Parameters.AddWithValue("@ff", ff);
                monto = Convert.ToDouble(da.ExecuteScalar());
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                monto = 0;

            }

        }
        public static void mostrar_cobros_tarjeta_por_turno(int idcaja, DateTime fi, DateTime ff, ref double monto)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("mostrar_cobros_tarjeta_por_turno", ConexionData.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                da.Parameters.AddWithValue("@fi", fi);
                da.Parameters.AddWithValue("@ff", ff);
                monto = Convert.ToDouble(da.ExecuteScalar());
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                monto = 0;

            }

        }

        public static void M_ventas_Tarjeta_por_turno(int idcaja, DateTime fi, DateTime ff, ref double monto)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("M_ventas_Tarjeta_por_turno", ConexionData.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                da.Parameters.AddWithValue("@fi", fi);
                da.Parameters.AddWithValue("@ff", ff);
                monto = Convert.ToDouble(da.ExecuteScalar());
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                monto = 0;
            }
        }
        public static void M_ventas_credito_por_turno(int idcaja, DateTime fi, DateTime ff, ref double monto)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("M_ventas_credito_por_turno", ConexionData.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                da.Parameters.AddWithValue("@fi", fi);
                da.Parameters.AddWithValue("@ff", ff);
                monto = Convert.ToDouble(da.ExecuteScalar());
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                monto = 0;
            }
        }

        public static void sumar_ingresos_por_turno(int idcaja, DateTime fi, DateTime ff, ref double monto)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("sumar_ingresos_por_turno", ConexionData.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                da.Parameters.AddWithValue("@fi", fi);
                da.Parameters.AddWithValue("@ff", ff);
                monto = Convert.ToDouble(da.ExecuteScalar());
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                monto = 0;

            }

        }
        public static void sumar_gastos_por_turno(int idcaja, DateTime fi, DateTime ff, ref double monto)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("sumar_gastos_por_turno", ConexionData.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                da.Parameters.AddWithValue("@fi", fi);
                da.Parameters.AddWithValue("@ff", ff);
                monto = Convert.ToDouble(da.ExecuteScalar());
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                monto = 0;

            }

        }
        public static void buscar_Proveedores(ref DataTable dt, string buscador)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscar_Proveedores", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", buscador);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void sumar_CreditoPorPagar(int idcaja, DateTime fi, DateTime ff, ref double monto)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("sumar_CreditoPorPagar", ConexionData.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                da.Parameters.AddWithValue("@fi", fi);
                da.Parameters.AddWithValue("@ff", ff);
                monto = Convert.ToDouble(da.ExecuteScalar());
                ConexionData.cerrar();
            }
            catch (Exception)
            {

                monto = 0;
            }
        }

        public static void sumar_CreditoPorCobrar(int idcaja, DateTime fi, DateTime ff, ref double monto)
        {
            try
            { 
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("sumar_CreditoPorCobrar", ConexionData.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                da.Parameters.AddWithValue("@fi", fi);
                da.Parameters.AddWithValue("@ff", ff);
                monto = Convert.ToDouble(da.ExecuteScalar());
            ConexionData.cerrar();
            }
            catch (Exception)
            {

                monto = 0;
            }
        }

        public static void mostrar_cajas(ref DataTable dt)
        {
            try
            {
                Bases.Obtener_serialPC(ref serialPC);
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_cajas_por_Serial_de_DiscoDuro", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Serial", serialPC);
                da.Fill(dt);
                ConexionData.cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void mostrar_empresa(ref DataTable dt)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("select * from EMPRESA", ConexionData.conectar);
                da.Fill(dt);
                ConexionData.cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void mostrarCorreoBase(ref DataTable dt)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("Select * from CorreoBase", ConexionData.conectar);
                da.Fill(dt);
                ConexionData.cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        //Clientes
        public static void mostrar_clientes(ref DataTable dt)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_clientes", ConexionData.conectar);
                da.Fill(dt);
                ConexionData.cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void buscar_clientes(ref DataTable dt, string buscador)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscar_clientes", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", buscador);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void mostrarEstadosCuentaCliente(ref DataTable dt, int idcliente)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarEstadosCuentaCliente", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idcliente", idcliente);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);

            }
        }

        //controlCobros
        public static void mostrar_ControlCobros(ref DataTable dt)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_ControlCobros", ConexionData.conectar);
                da.Fill(dt);

                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void ReportePorCobrar(ref double Monto)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("ReportePorCobrar", ConexionData.conectar);
                Monto = Convert.ToDouble(da.ExecuteScalar());
                ConexionData.cerrar();

            }
            catch (Exception)
            {
                Monto = 0;
            }
        }
        public static void ReporteCantClientes(ref int Cantidad)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("select count(idclientev) from clientes", ConexionData.conectar);
                Cantidad = Convert.ToInt32(da.ExecuteScalar());
                ConexionData.cerrar();
            }
            catch (Exception)
            {
                Cantidad = 0;
            }
        }


        //Proveedores
        public static void ReportePorPagar(ref double Monto)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("ReportePorPagar", ConexionData.conectar);
                Monto = Convert.ToDouble(da.ExecuteScalar());
                ConexionData.cerrar();

            }
            catch (Exception)
            {
                Monto = 0;
            }
        }
        public static void mostrar_Proveedores(ref DataTable dt)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_Proveedores", ConexionData.conectar);
                da.Fill(dt);
                ConexionData.cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }


        //Ventas
        public static void mostrarVentasGrafica(ref DataTable dt)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarVentasGrafica", ConexionData.conectar);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void mostrarVentasGraficaFechas(ref DataTable dt, DateTime fi, DateTime ff)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarVentasGraficaFechas", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fi", fi);
                da.SelectCommand.Parameters.AddWithValue("@ff", ff);

                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void ReporteTotalVentas(ref double Monto)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("ReporteTotalVentas", ConexionData.conectar);
                Monto = Convert.ToDouble(da.ExecuteScalar());
                ConexionData.cerrar();

            }
            catch (Exception)
            {
                Monto = 0;
            }
        }
        public static void ReporteTotalVentasFechas(ref double Monto, DateTime fi, DateTime ff)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("ReporteTotalVentasFechas", ConexionData.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@fi", fi);
                da.Parameters.AddWithValue("@ff", ff);
                Monto = Convert.ToDouble(da.ExecuteScalar());
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                Monto = 0;
            }
        }
        public static void buscarVentas(ref DataTable dt, string buscador)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscarVentas", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@busqueda", buscador);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void buscarVentasPorFechas(ref DataTable dt, DateTime fi, DateTime ff)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscarVentasPorFechas", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fi", fi);
                da.SelectCommand.Parameters.AddWithValue("@ff", ff);

                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void contarVentasEspera(ref int Contador)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("contarVentasEspera", ConexionData.conectar);
                Contador = Convert.ToInt32(da.ExecuteScalar());
                ConexionData.cerrar();
            }
            catch (Exception)
            {
                Contador = 0;


            }
        }
        public static void ReporteResumenVentasHoy(ref DataTable dt)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteResumenVentasHoy", ConexionData.conectar);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void ReporteResumenVentasHoyEmpleado(ref DataTable dt, int idEmpleado)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteResumenVentasHoyEmpleado", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        public static void ReporteResumenVentasFechas(ref DataTable dt, DateTime fi, DateTime ff)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteResumenVentasFechas", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fi", fi);
                da.SelectCommand.Parameters.AddWithValue("@ff", ff);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void ReporteResumenVentasEmpleadoFechas(ref DataTable dt, int idEmpleado, DateTime fi, DateTime ff)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteResumenVentasEmpleadoFechas", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                da.SelectCommand.Parameters.AddWithValue("@fi", fi);
                da.SelectCommand.Parameters.AddWithValue("@ff", ff);

                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        //Detalle ventas
        public static void ReporteGanancias(ref double Monto)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("ReporteGanancias", ConexionData.conectar);
                Monto = Convert.ToDouble(da.ExecuteScalar());
                ConexionData.cerrar();

            }
            catch (Exception)
            {
                Monto = 0;
            }
        }
        public static void ReporteGananciasFecha(ref double Monto, DateTime fi, DateTime ff)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("ReporteGananciasFecha", ConexionData.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@fi", fi);
                da.Parameters.AddWithValue("@ff", ff);
                Monto = Convert.ToDouble(da.ExecuteScalar());
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                Monto = 0;
            }
        }
        public static void MostrarDetalleVenta(ref DataTable dt, int idventa)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_productos_agregados_a_venta", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idventa", idventa);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        //Productos
        public static void ReporteProductoBajoMinimo(ref int Monto)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("ReporteProductoBajoMinimo", ConexionData.conectar);
                Monto = Convert.ToInt32(da.ExecuteScalar());
                ConexionData.cerrar();
            }
            catch (Exception)
            {
                Monto = 0;
            }
        }
        public static void ReporteCantProductos(ref int Cantidad)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("select count(Id_Producto1) from Producto1", ConexionData.conectar);
                Cantidad = Convert.ToInt32(da.ExecuteScalar());
                ConexionData.cerrar();
            }
            catch (Exception)
            {
                Cantidad = 0;
            }
        }
        public static void mostrarPmasVendidos(ref DataTable dt)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarPmasVendidos", ConexionData.conectar);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);

            }
        }
        public static void imprimir_inventarios_todos(ref DataTable dt)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("imprimir_inventarios_todos", ConexionData.conectar);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);

            }
        }
        public static void mostrar_productos_vencidos(ref DataTable dt)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_productos_vencidos", ConexionData.conectar);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);

            }
        }
        public static void MOSTRAR_Inventarios_bajo_minimo(ref DataTable dt)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("MOSTRAR_Inventarios_bajo_minimo", ConexionData.conectar);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);

            }
        }
        public static void BUSCAR_PRODUCTOS_KARDEX(ref DataTable dt, string buscador)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("BUSCAR_PRODUCTOS_KARDEX", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letrab", buscador);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);

            }
        }

        //Empresa
        public static void MostrarMoneda(ref string moneda)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("select Moneda  from EMPRESA", ConexionData.conectar);
                moneda = Convert.ToString(da.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }
        }
        //Gastos
        public static void ReporteGastosAnioCombo(ref DataTable dt)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteGastosAnioCombo", ConexionData.conectar);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void ReporteGastosMesCombo(ref DataTable dt, int anio)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteGastosMesCombo", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@anio", anio);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }

        public static void ReporteGastosAnio(ref DataTable dt, int año)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteGastosAnioGrafica", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@anio", año);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void ReporteGastosAnioMesGrafica(ref DataTable dt, int año, string mes)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteGastosAnioMesGrafica", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@anio", año);
                da.SelectCommand.Parameters.AddWithValue("@mes", mes);

                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        //Caja
        public static void mostrarPuertos(ref DataTable dt)
        {
            try
            {
                Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarPuertos", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idcaja", idcaja);
                da.Fill(dt);
                ConexionData.cerrar();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        public static void mostrarTemaCaja(ref string Tema)
        {
            try
            {
                Obtener_id_caja_PorSerial(ref idcaja);
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("mostrarTemaCaja", ConexionData.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@idcaja", idcaja);
                Tema = da.ExecuteScalar().ToString();
                ConexionData.cerrar();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }

        //Tickets
        public static void mostrar_ticket_impreso(ref DataTable dt, int idventa, string TotalLetras)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_ticket_impreso", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id_venta", idventa);
                da.SelectCommand.Parameters.AddWithValue("@total_en_letras", TotalLetras);
                da.Fill(dt);
                ConexionData.cerrar();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        //Usuarios
        public static void mostrarUsuarios(ref DataTable dt)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("Select * from USUARIO2", ConexionData.conectar);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        //Clientes
        public static void ReporteCuestasPorCobrar(ref DataTable dt)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteCuestasPorCobrar", ConexionData.conectar);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        //Proveedores
        public static void ReporteCuestasPorPagar(ref DataTable dt)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("ReporteCuestasPorPagar", ConexionData.conectar);
                da.Fill(dt);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }


    }
}

