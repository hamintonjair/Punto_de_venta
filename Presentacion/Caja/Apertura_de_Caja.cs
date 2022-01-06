using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Management;
using System.Windows.Forms;
using Punto_de_venta.Logica;
using Punto_de_venta.Datos;

namespace Punto_de_venta.Presentacion.Caja
{
    public partial class Apertura_de_Caja : Form
    {
        public Apertura_de_Caja()
        {
            InitializeComponent();
        }
         int txtidcaja;
        private void iniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtmonto.Text))
            {
                txtmonto.Text = "0";
            }
            
            bool estado = Editar_datos.editar_dinero_caja_inicial(txtidcaja, Convert.ToDouble(txtmonto.Text));
            if (estado == true)
            {
                pasar_a_ventas();
            }          
        }
    
        private void Apertura_de_Caja_Load_1(object sender, EventArgs e)
        {
            Bases.Cambiar_idioma_regional();
            Obtener_datos.Obtener_id_caja_PorSerial(ref txtidcaja);
            centrar_panel();         

        }
        private static void OnlyNumber(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados;
            if (!isdecimal)
            {
                aceptados = "0123456789." + Convert.ToChar(8);
            }
            else
                aceptados = "0123456789," + Convert.ToChar(8);

            if (aceptados.Contains("" + e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void omitirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pasar_a_ventas();
            
        }
        private void pasar_a_ventas()
        {
            Dispose();
            Ventas_Menu_Principal.Ventas_Menu_Princi frm = new Ventas_Menu_Principal.Ventas_Menu_Princi();
            frm.ShowDialog();

        }
        private void centrar_panel()
        {
            panelCaja.Location = new Point((Width - panelCaja.Width) / 2, (Height - panelCaja.Height) / 2);
        }     
     
    }
}
