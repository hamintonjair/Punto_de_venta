using Punto_de_venta.ConexionDt;
using Punto_de_venta.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Punto_de_venta.Datos
{
   public class Dcaja
    {
        string serialPC;
        public  void ObtenerIdcaja(ref int idcaja)
        {
            try
            {
                Bases.Obtener_serialPC(ref serialPC);
                ConexionData.abrir();
                SqlCommand com = new SqlCommand("mostrar_cajas_por_Serial_de_DiscoDuro", ConexionData.conectar);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Serial", serialPC);
                idcaja =Convert.ToInt32( com.ExecuteScalar());

            }
            catch (Exception ex)
            {
                idcaja = 0;
                 MessageBox.Show("No se pudo completar el proceso", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);;
            }
            finally
            {
                ConexionData.cerrar();

            }
        }
    }
}
