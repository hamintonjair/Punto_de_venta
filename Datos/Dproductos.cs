using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Punto_de_venta.ConexionDt;
using Punto_de_venta.Logica;
using System.Data;
using System.Data.SqlClient;
namespace Ada369Csharp.Datos
{
   public class Dproductos
    {
        public void buscarProductos(ref DataTable dt, string buscador)
        {

            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("BUSCAR_PRODUCTOS_oka", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letrab", buscador);
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
