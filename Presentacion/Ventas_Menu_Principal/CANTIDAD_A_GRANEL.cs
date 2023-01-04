using Punto_de_venta.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_venta.Presentacion.Ventas_Menu_Principal
{
    public partial class CANTIDAD_A_GRANEL : Form
    {
        public CANTIDAD_A_GRANEL()
        {
            InitializeComponent();
        }      
        public double preciounitario;   
        private string BufeerRespuesta;
        string puertoBalanza;
        string estadoPuerto;

        private delegate void DelegadoAcceso(string accion);

        private void AccesoForm(string accion)
        {
            double cantidadLibra;
          
            BufeerRespuesta = accion;          
            if(Convert.ToDouble(BufeerRespuesta) >= 500)
            {
               
               cantidadLibra= Convert.ToDouble(BufeerRespuesta) * 0.0022;
                txtcantidad.Text = cantidadLibra.ToString();
            }
            if (Convert.ToDouble(BufeerRespuesta) > 500)
            {
               
                cantidadLibra = Convert.ToDouble(BufeerRespuesta) / 1000;
                txtcantidadKilo.Text = cantidadLibra.ToString();
            }
          
        }
        private void accesoInterrupcion(string accion)
        {
            DelegadoAcceso Var_delagadoacceso;
            Var_delagadoacceso = new DelegadoAcceso(AccesoForm);
            Object[] arg = { accion };
            base.Invoke(Var_delagadoacceso, arg);
        }
        private void abrirPuertosBalanza()
        {
            puertos.Close();
            try
            {
                puertos.BaudRate = 9600;
                puertos.DataBits = 8;
                puertos.Parity = Parity.None;
                puertos.StopBits = (StopBits)1;
                puertos.PortName = puertoBalanza;
                puertos.Open();       
                if (puertos.IsOpen)
                {
                    estadoPuerto = "Conectado";
                }
                else
                {
                    estadoPuerto = "Fallo la conexion";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void mostrarPuertos()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrarPuertos(ref dt);
            foreach (DataRow rdr in dt.Rows)
            {
                puertoBalanza = rdr["PuertoBalanza"].ToString();
                estadoPuerto = rdr["EstadoBalanza"].ToString();
            }          
            MessageBox.Show(estadoPuerto + " DE CONFIGURAR LA BALANZA" , "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (estadoPuerto == "CONFIRMADO")
            {
                abrirPuertosBalanza();
            }
        }
        private void puertos_DataReceived(Object sender, SerialDataReceivedEventArgs e)
        {
            accesoInterrupcion(puertos.ReadExisting());
        }
        private void BtnCerrar_turno_Click(object sender, EventArgs e)
        {
            if(txtcantidad.Text != "")
            {
                string tipo = "libra de ";
                Ventas_Menu_Princi.txtpantalla = Convert.ToDouble(txtcantidad.Text);
                Ventas_Menu_Princi.tipo = tipo;
                Dispose();
            }
            if (txtcantidadKilo.Text != "")
            {
                double total;
                double cantidad;
                cantidad = Convert.ToDouble(txtcantidadKilo.Text);
                total =  cantidad * 2;
                string tipo = "libra de ";
                Ventas_Menu_Princi.txtpantalla = Convert.ToDouble(total);
                Ventas_Menu_Princi.tipo = tipo;
                Dispose();
            }
           
        }

        private void CANTIDAD_A_GRANEL_Load(object sender, EventArgs e)
        {
            txtprecio_unitario.Text = Convert.ToString(preciounitario);
            mostrarPuertos();
        }

        private void txtcantidad_TextChanged(object sender, EventArgs e)
        {
            calcularTotal();
        }
        private void calcularTotal()
        {
           
            try
            {

                double total;
                double cantidad;
                cantidad = Convert.ToDouble(txtcantidad.Text);
                total = preciounitario * cantidad;
                txttotal.Text = Convert.ToString(total);
            }
            catch (Exception)
            {

            }
          

        }

        private void txtcantidadKilo_TextChanged(object sender, EventArgs e)
        {
            calcularkilos();
        }
        private void calcularkilos()
        {
           
            try
            {

                double total;
                double cantidad;
                cantidad = Convert.ToDouble(txtcantidadKilo.Text);
                total = preciounitario * (cantidad * 2);
                txttotal.Text = Convert.ToString(total);
            }
            catch (Exception)
            {

            }
           

        }

        private void txtcantidad_MouseClick(object sender, MouseEventArgs e)
        {
            txtcantidadKilo.Text = "";
        }

        private void txtcantidadKilo_MouseClick(object sender, MouseEventArgs e)
        {
            txtcantidad.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtcantidad.Text = "";
            txtcantidadKilo.Text = "";
            txttotal.Text = "";
        }
    }
}
