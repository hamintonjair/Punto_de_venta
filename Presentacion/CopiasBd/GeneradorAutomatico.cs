using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Punto_de_venta.ConexionDt;
using System.Windows.Forms;
using System.Data.SqlClient;
using Punto_de_venta.Datos;
using Punto_de_venta.Logica;

namespace Punto_de_venta.Presentacion.CopiasBd
{
    public partial class GeneradorAutomatico : Form
    {
        public GeneradorAutomatico()
        {
            InitializeComponent();
        }
        int contador = 10;
        string txtsoftware = "jojama";
        string Base_De_datos = "SistemaContable";
        private Thread Hilo;
        private bool acaba = false;
        string lblfrecuencia;
        private void GeneradorAutomatico_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            Mostrar_empresa();
            timerContador.Start();
        }
        private void GenerarCopia()
        {
            if (!string.IsNullOrEmpty(txtRuta.Text))
            {
                Hilo = new Thread(new ThreadStart(ejecucion));
                Pcargando.Visible = true;
                Hilo.Start();
                acaba = false;
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Selecciona una Ruta donde Guardar las Copias de Seguridad", "Seleccione Ruta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRuta.Focus();

            }
        }
        private void ejecucion()
        {
            string miCarpeta = "Copias de Seguridad de " + txtsoftware;
            if (System.IO.Directory.Exists(txtRuta.Text + miCarpeta))
            {
            }
            else
            {
                System.IO.Directory.CreateDirectory(txtRuta.Text + miCarpeta);
            }
            string ruta_completa = txtRuta.Text + miCarpeta;
            string SubCarpeta = ruta_completa + @"\Respaldo del " + DateTime.Now.Day + "-" + (DateTime.Now.Month) + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + " " + DateTime.Now.Minute;
            try
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(ruta_completa, SubCarpeta));

            }
            catch (Exception)
            {
            }
            try
            {
                string v_nombre_respaldo = Base_De_datos + ".bak";
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("BACKUP DATABASE " + Base_De_datos + " TO DISK = '" + SubCarpeta + @"\" + v_nombre_respaldo + "'", ConexionData.conectar);
                cmd.ExecuteNonQuery();
                acaba = true;
            }
            catch (Exception ex)
            {
                acaba = false;
                MessageBox.Show(ex.Message);
            }
        }
        private void Mostrar_empresa()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrar_empresa(ref dt);
            foreach (DataRow row in dt.Rows)
            {
                txtRuta.Text = row["Carpeta_para_copias_de_seguridad"].ToString();
                lblfrecuencia = row["Frecuencia_de_copias"].ToString();

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (acaba == true)
            {
                timer1.Stop();
                Pcargando.Visible = false;
                editarRespaldos();

            }
        }
        private void editarRespaldos()
        {
            Lempresa parametros = new Lempresa();
            Editar_datos funcion = new Editar_datos();
            parametros.Carpeta_para_copias_de_seguridad = txtRuta.Text;
            parametros.Ultima_fecha_de_copia_de_seguridad = DateTime.Now.ToString();
            parametros.Ultima_fecha_de_copia_date = DateTime.Now;
            parametros.Frecuencia_de_copias = Convert.ToInt32(lblfrecuencia);
            if (funcion.editarRespaldos(parametros) == true)
            {
                Application.Exit();
            }
        }
        private void timerContador_Tick(object sender, EventArgs e)
        {
            contador -= 1;
            lbltiempo.Text = contador.ToString();
            if (contador == 0)
            {
                contador = 0;
                timerContador.Stop();
                GenerarCopia();
            }
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void GeneradorAutomatico_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
