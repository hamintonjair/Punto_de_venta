using Punto_de_venta.ConexionDt;
using Punto_de_venta.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ada369Csharp.Datos
{
   public class Ddetallecompra
    {
        public void mostrar_DetalleCompra(ref DataTable dt, Ldetallecompra parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_DetalleCompra", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Idcompra", parametros.IdCompra);
                da.Fill(dt);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        public bool eliminar_detalle_compra(Ldetallecompra parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("eliminar_detalle_compra", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iddetallecompra", parametros.IdDetallecompra);
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
        public bool editar_detalle_compra_Cantidad(Ldetallecompra parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("editar_detalle_compra_Cantidad", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_producto", parametros.IdProducto);
                cmd.Parameters.AddWithValue("@cantidad", parametros.Cantidad);
                cmd.Parameters.AddWithValue("@Idcompra", parametros.IdCompra);
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
        public bool editar_detalle_compra_Precio(Ldetallecompra parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("editar_detalle_compra_Precio", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_producto", parametros.IdProducto);
                cmd.Parameters.AddWithValue("@precio", parametros.Costo);
                cmd.Parameters.AddWithValue("@Idcompra", parametros.IdCompra);
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

    }
}
