using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_venta.Modulos.Ventas_Menu_Principal
{
    public partial class Ventas_Menu_Princi : Form
    {
        public Ventas_Menu_Princi()
        {
            InitializeComponent();
        }
        int contador_stock_detalle_de_venta;
        int idproducto;
        int idClienteEstandar;
        int idusuario_que_inicio_sesion;
        int idVenta;
        int iddetalleventa;
        int Contador;

        private void iToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Ventas_Menu_Princi_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CO");
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";

            ManagementObject MOS = new ManagementObject(@"Win32_PhysicalMedia='\\.\PHYSICALDRIVE0'");
            lblSerialPc.Text = MOS.Properties["SerialNumber"].Value.ToString();
            lblSerialPc.Text = lblSerialPc.Text.Trim();

            MOSTRAR_CAJA_POR_SERIAL();
            MOSTRAR_TIPO_DE_BUSQUEDA();
            //Obtener_id_de_cliente_estandar();
            //Obtener_id_de_usuario_que_inicio_sesion();

            if (Tipo_de_busqueda == "TECLADO")
            {
                lbltipodebusqueda2.Text = "Buscar con TECLADO";
                BTNLECTORA.BackColor = Color.WhiteSmoke;
                BTNTECLADO.BackColor = Color.FromArgb(129, 178, 20); ;
            }
            else
            {
                lbltipodebusqueda2.Text = "Buscar con LECTORA de Codigos de Barras";
                BTNLECTORA.BackColor = Color.FromArgb(129, 178, 20);
                BTNTECLADO.BackColor = Color.WhiteSmoke;
            }
            //limpiar();
        }
        private void Obtener_id_de_usuario_que_inicio_sesion()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConexionDt.ConexionData.conexion;
            SqlCommand com = new SqlCommand("mostrar_inicio_De_sesion", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@id_serial_pc", ConexionDt.Encryptar_en_texto.Encriptar(lblSerialPc.Text));
            try
            {
                con.Open();
                idusuario_que_inicio_sesion = Convert.ToInt32(com.ExecuteScalar());
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        int Id_caja;
        private void MOSTRAR_CAJA_POR_SERIAL()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConexionDt.ConexionData.conexion;
            SqlCommand com = new SqlCommand("mostrar_cajas_por_Serial_de_DiscoDuro", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Serial", lblSerialPc.Text);
            try
            {
                con.Open();
                Id_caja = Convert.ToInt32(com.ExecuteScalar());
                con.Close();              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }

        }
        string Tipo_de_busqueda;
        private void MOSTRAR_TIPO_DE_BUSQUEDA()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConexionDt.ConexionData.conexion;
            SqlCommand com = new SqlCommand("Select Modo_de_busqueda  from EMPRESA", con);

            try
            {
                con.Open();
                Tipo_de_busqueda = Convert.ToString(com.ExecuteScalar());
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        private void Obtener_id_de_cliente_estandar()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConexionDt.ConexionData.conexion;
            SqlCommand com = new SqlCommand("select idclientev  from clientes where Cliente='NEUTRO'", con);
            try
            {
                con.Open();
                idClienteEstandar = Convert.ToInt32(com.ExecuteScalar());
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Productos.Productoss frm = new Productos.Productoss();
            frm.ShowDialog();
        }

        private void BtnCerrar_turno_Click(object sender, EventArgs e)
        {
            Caja.Cierre_de_Caja frm = new Caja.Cierre_de_Caja();
            frm.ShowDialog();
        }

        private void BTNTECLADO_Click(object sender, EventArgs e)
        {
            lbltipodebusqueda2.Text = "Buscar con  TECLADO";
            Tipo_de_busqueda = "TECLADO";
            BTNTECLADO.BackColor = Color.LightGreen;
            BTNLECTORA.BackColor = Color.WhiteSmoke;
            txtbuscar.Clear();
            txtbuscar.Focus();
        }

        private void BTNLECTORA_Click(object sender, EventArgs e)
        {
            lbltipodebusqueda2.Text = "Buscar con LECTORA de Codigos de Barras";
            Tipo_de_busqueda = "LECTORA";
            BTNLECTORA.BackColor = Color.LightGreen;
            BTNTECLADO.BackColor = Color.WhiteSmoke;
            txtbuscar.Clear();
            txtbuscar.Focus();
        }
    }
}
