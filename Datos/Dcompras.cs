using Punto_de_venta.ConexionDt;
using Punto_de_venta.Datos;
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
   public class Dcompras
    {
        int Idcaja;
        public bool Insertar_Compras(Ldetallecompra parametros)
        {
            try
            {
                var funcion = new Dcaja();
                funcion.ObtenerIdcaja(ref Idcaja);
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("Insertar_Compras", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fechacompra", DateTime.Now);
                cmd.Parameters.AddWithValue("@Cantidad", parametros.Cantidad);
                cmd.Parameters.AddWithValue("@Costo", parametros.Costo);
                cmd.Parameters.AddWithValue("@Moneda", parametros.Moneda);
                cmd.Parameters.AddWithValue("@IdProducto", parametros.IdProducto);
                cmd.Parameters.AddWithValue("@Descripcion", parametros.Descripcion);
                cmd.Parameters.AddWithValue("@Estado", parametros.Estado);
                cmd.Parameters.AddWithValue("@Idcaja", Idcaja);
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
        public void MostrarUltimoIdcompra(ref int idcompra)
        {

            try
            {
                var funcion = new Dcaja();
                funcion.ObtenerIdcaja(ref Idcaja);
                ConexionData.abrir();
                SqlCommand com = new SqlCommand("MostrarUltimoIdcompra", ConexionData.conectar);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Idcaja", Idcaja);
                idcompra = Convert.ToInt32(com.ExecuteScalar());
            }
            catch (Exception ex)
            {
                idcompra = 0;
                MessageBox.Show(ex.StackTrace);

            }
            finally
            {
                ConexionData.cerrar();

            }
        }
        public bool eliminarComprasvacias()
        {
            try
            {
                var funcion = new Dcaja();
                funcion.ObtenerIdcaja(ref Idcaja);
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("eliminarComprasvacias", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idcaja", Idcaja);

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
        public bool confirmarCompra(Lcompras parametros)
        {
            try
            {
                var funcion = new Dcaja();
                funcion.ObtenerIdcaja(ref Idcaja);
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("confirmarCompra", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idcompra", parametros.Idcompra);
                cmd.Parameters.AddWithValue("@Total", parametros.Total);
                cmd.Parameters.AddWithValue("@Idcaja", Idcaja);
                cmd.Parameters.AddWithValue("@Idproveedor", parametros.IdProveedor);
                cmd.Parameters.AddWithValue("@fechacompra", DateTime.Now);
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
        public void buscarCompras(ref DataTable dt, string buscador)
        {

            try
            {
                ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscarCompras", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@buscar", buscador);
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

    }
}
