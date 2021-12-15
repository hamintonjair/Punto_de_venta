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

namespace Punto_de_venta.Modulos.Caja
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

            //try
            //{
            //    SqlConnection con = new SqlConnection();
            //    con.ConnectionString = ConexionDt.ConexionData.conexion;
            //    con.Open();
            //    SqlCommand cmd = new SqlCommand();
            //    cmd = new SqlCommand("editar_dinero_caja_inicial", con);
            //    cmd.CommandType = CommandType.StoredProcedure;         
            //    cmd.Parameters.AddWithValue("@Id_caja", lbltidcaja.Text);
            //    cmd.Parameters.AddWithValue("@saldo", txtMonto.Text);
            //    cmd.ExecuteNonQuery();
     
            //    con.Close();

            //    this.Hide();
            //    Ventas_Menu_Principal.Ventas_Menu_Princi frm = new Ventas_Menu_Principal.Ventas_Menu_Princi();
            //    frm.ShowDialog();
            //    this.Hide();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            
            //}
        }
        private void MOSTRAR_CAJA_POR_SERIAL()
        {
        
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();

                da = new SqlDataAdapter("mostrar_cajas_por_Serial_de_DiscoDuro", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Serial", lblserialPC.Text);
                da.Fill(dt);
                datalistado_caja.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void Apertura_de_Caja_Load_1(object sender, EventArgs e)
        {
            Bases.Cambiar_idioma_regional();
            Obtener_datos.Obtener_id_caja_PorSerial(ref txtidcaja);
            centrar_panel(); ;

            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CO");
            //System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
            //System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
            //System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            //System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";
            //ManagementObject MOS = new ManagementObject(@"Win32_PhysicalMedia='\\.\PHYSICALDRIVE0'");
            
            //    lblserialPC.Text = MOS.Properties["SerialNumber"].Value.ToString();
            //    MOSTRAR_CAJA_POR_SERIAL();
            //    try
            //    {
            //        lbltidcaja.Text = datalistado_caja.SelectedCells[1].Value.ToString();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            

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
            //try
            //{
            //    SqlConnection con = new SqlConnection();
            //    con.ConnectionString = ConexionDt.ConexionData.conexion;
            //    con.Open();
            //    SqlCommand cmd = new SqlCommand();
            //    cmd = new SqlCommand("editar_dinero_caja_inicial", con);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@Id_caja", lbltidcaja.Text);
            //    cmd.Parameters.AddWithValue("@saldo", 0);
            //    cmd.ExecuteNonQuery();

            //    con.Close();

            //    this.Hide();
            //    Ventas_Menu_Principal.Ventas_Menu_Princi frm = new Ventas_Menu_Principal.Ventas_Menu_Princi();
            //    this.ShowDialog();
            //    this.Hide();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
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
        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //OnlyNumber(e, false);
            //{
            //    // Si se pulsa la tecla Intro, pasar al siguiente
            //    //if( e.KeyChar == Convert.ToChar('\r') ){
            //    if (e.KeyChar == '\r')
            //    {
            //        e.Handled = true;

            //    }
            //    else if (e.KeyChar == ',')
            //    {
            //        // si se pulsa en el punto se convertirá en coma
            //        e.Handled = true;
            //        SendKeys.Send(".");
            //    }
            //}
            //CONEXION.Numeros_separadores.Separador_de_Numeros(txtmonto, e);
            //if (Char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = false;
            //}
            //else if (Char.IsControl(e.KeyChar))
            //{
            //    e.Handled = false;
            //}
            //else if (char.IsSeparator('.'))
            //{
            //    e.Handled = false;

            //}
            //else if (e.KeyChar == ',')
            //{
            //    e.Handled = false;
            //}
            //else
            //{
            //    e.Handled = true;
            //}


          ConexionDt.Numeros_separadores.Separador_de_Numeros(txtmonto, e);
        }

     
    }
}
