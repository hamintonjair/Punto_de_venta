using Punto_de_venta.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Punto_de_venta.ConexionDt;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Punto_de_venta.Datos
{
  public  class Dproveedores
    {
        public bool insertar_Proveedores(Lproveedores parametros)
        {
			try
			{
				ConexionData.abrir();
				SqlCommand cmd = new SqlCommand("insertar_Proveedores", ConexionData.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Nombre", parametros.Nombre);
				cmd.Parameters.AddWithValue("@Direccion", parametros.Direccion);
				cmd.Parameters.AddWithValue("@IdentificadorFiscal", parametros.IdentificadorFiscal);
				cmd.Parameters.AddWithValue("@Celular", parametros.Celular);
				cmd.Parameters.AddWithValue("@Estado", "ACTIVO");
				cmd.Parameters.AddWithValue("@Saldo", 0);
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
		public void buscar_Proveedores(ref DataTable dt, string buscador)
		{
			try
			{
				ConexionData.abrir();
				SqlDataAdapter da = new SqlDataAdapter("buscar_Proveedores", ConexionData.conectar);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;
				da.SelectCommand.Parameters.AddWithValue("@letra", buscador);
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
