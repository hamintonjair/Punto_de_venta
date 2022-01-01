using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Punto_de_venta.ConexionDt;
using System.Windows.Forms;
using Punto_de_venta.Logica;
namespace Punto_de_venta.Datos
{
    class Eliminar_datos
    {
        public static void eliminar_venta(int idventa)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("eliminar_venta", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idventa", idventa);
                cmd.ExecuteNonQuery();
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
           
        }
        public static void eliminar_ingreso(int idingreso)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("eliminar_ingreso", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idingreso", idingreso);
                cmd.ExecuteNonQuery();
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }

        }
        public static void eliminar_gasto(int idgasto)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("eliminar_gasto", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idgasto", idgasto);
                cmd.ExecuteNonQuery();
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }

        }
        public bool eliminar_Proveedores(Lproveedores parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("eliminar_Proveedores", ConexionData.conectar);
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
        public bool eliminar_clientes(Lclientes  parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("eliminar_clientes", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idcliente", parametros.idcliente );
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
        public bool eliminarControlCobro(Lcontrolcobros  parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("eliminarControlCobro", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcontrol", parametros.IdcontrolCobro );
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
        public bool EliminarVentas(Lventas parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("eliminar_venta", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idventa", parametros.idventa);
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
