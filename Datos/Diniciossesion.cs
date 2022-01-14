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
   public class Diniciossesion
    {
        string serialpc;
        public void mostrar_inicio_De_sesion(ref int idusuario)
        {
            Bases.Obtener_serialPC(ref serialpc);
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("mostrar_inicio_De_sesion", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_serial_pc", Bases.Encriptar(serialpc));
                idusuario = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                idusuario = 0;
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
   }
}
