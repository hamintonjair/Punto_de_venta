using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using Punto_de_venta.Logica;
using Punto_de_venta.Datos;

namespace Punto_de_venta.Presentacion.Ventas_Menu_Principal
{
    public partial class Ventas_en_espera : Form
    {

        Ventas_Menu_Princi prin = new Ventas_Menu_Princi();
        public Ventas_en_espera()
        {
            InitializeComponent();

         
        }
        int idcaja;
        public static int idventa;
        private void Ventas_en_espera_Load(object sender, EventArgs e)
        {
            mostrar_ventas_en_espera_con_fecha_y_monto();
            Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
            txtbusca.Focus();

        }
        private void mostrar_ventas_en_espera_con_fecha_y_monto()
        {
            try
            {
                DataTable dt = new DataTable();
                Obtener_datos.mostrar_ventas_en_espera_con_fecha_y_monto(ref dt);
                datalistado_ventas_en_espera.DataSource = dt;
                datalistado_ventas_en_espera.Columns[1].Visible = false;
                datalistado_ventas_en_espera.Columns[4].Visible = false;   
                Bases.Multilinea2(ref datalistado_ventas_en_espera);
            }
            catch (Exception ex)
            {

            }
        }
        string fecha;   
        private void datalistado_ventas_en_espera_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                idventa = Convert.ToInt32(datalistado_ventas_en_espera.SelectedCells[1].Value);
                mostrar_detalle_venta();
      
                fecha = datalistado_ventas_en_espera.Rows[e.RowIndex].Cells[3].Value.ToString();
              
            }
            catch (Exception ex)
            {
               
            }
        }
        private void mostrar_detalle_venta()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrar_productos_agregados_a_ventas_en_espera(ref dt, idventa);
            datalistadodetalledeventasarestaurar.DataSource = dt;
            datalistadodetalledeventasarestaurar.Columns[5].Visible= false;
            Bases.Multilinea2(ref datalistadodetalledeventasarestaurar);
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            Eliminar_ventas_esperas();
        }
        private void Eliminar_ventas_esperas()
        {
             Eliminar_datos.eliminar_venta(idventa);
            idventa = 0;
            mostrar_ventas_en_espera_con_fecha_y_monto();
            mostrar_detalle_venta();
            datalistado_ventas_en_espera.Focus();
        }
  
        private void btnrestaurar_Click(object sender, EventArgs e)
        {
         
            DateTime original = DateTime.Now;
            DateTime updated = original.Add(new TimeSpan(0, -15, 0));         


            if (Convert.ToDateTime(fecha) > updated)
            {
                if (idventa == 0)
                {
                    MessageBox.Show("Seleccione una venta a Restaurar");
                }
                Ventas_Menu_Princi.idVenta = idventa;
                Ventas_Menu_Princi.txtventagenerada = "VENTA GENERADA";
                Editar_datos.cambio_de_Caja(idcaja, idventa);
               
                Dispose();
        
                //prin.mostrar_panel_de_Cobro();
            }
            else
            {
                MessageBox.Show("Esta venta ya no Existe, debes eliminarla y generar otra de nuevo ", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }        
               
    }
}
