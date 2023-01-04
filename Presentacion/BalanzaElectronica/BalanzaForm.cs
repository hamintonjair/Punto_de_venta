using Punto_de_venta.Datos;
using Punto_de_venta.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Punto_de_venta.Presentacion.BalanzaElectronica
{
    public partial class BalanzaForm : Form
    {
        public BalanzaForm()
        {
            InitializeComponent();
        }
        int idcajavariable;
        private string BufeerRespuesta;
        private delegate void DelegadoAcceso(string accion);
        private void BalanzaForm_Load(object sender, EventArgs e)
        {
            PanelContenedorTODO.Location = new Point((Width - PanelContenedorTODO.Width) / 2, (Height - PanelContenedorTODO.Height) / 2);
            listarPuertos();
        }
        private void listarPuertos()
        {
            try
            {
                cbListarPuertos.Items.Clear();
                string[] PuertosDisponibles = SerialPort.GetPortNames();
                foreach (string puerto in PuertosDisponibles)
                {
                    cbListarPuertos.Items.Add(puerto);

                }
                if (cbListarPuertos.Items.Count > 0)
                {
                    cbListarPuertos.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("No se encontraron Puertos");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encontraron Puertos");
            }
        }
        private void AccesoForm(string accion)
        {
            Obtener_datos.Obtener_id_caja_PorSerial(ref idcajavariable);
            BufeerRespuesta = accion;
            txtresultado.Text = BufeerRespuesta;
        }
        private void accesoInterrupcion(string accion)
        {
            DelegadoAcceso Var_delagadoacceso;
            Var_delagadoacceso = new DelegadoAcceso(AccesoForm);
            Object[] arg = { accion };
            base.Invoke(Var_delagadoacceso, arg);
        }
        private void puertos_DataReceived(Object sender, SerialDataReceivedEventArgs e)
        {
            accesoInterrupcion(puertos.ReadExisting());
        }
        private void btnProbar_Click(object sender, EventArgs e)
        {
            puertos.Close();
            try
            {
                puertos.BaudRate = 9600;
                puertos.DataBits = 8;
                puertos.Parity = Parity.None;
                puertos.StopBits = (StopBits)1;
                puertos.PortName = cbListarPuertos.Text;
                puertos.Open();
                if (puertos.IsOpen)
                {
                    lblestado.Text = "Conectado........";
                 
                }
                else
                {
                    MessageBox.Show("Fallo la conexion");

                }
            }
            catch (Exception ex)
            {
                 MessageBox.Show("No se pudo completar el proceso", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (puertos.IsOpen)
            {
                puertos.WriteLine(txtresultado.Text);
               
            }
            else
            {
                MessageBox.Show("Fallo la conexion");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtresultado.Text))
            {
                editarBascula();
            }
            else
            {
                MessageBox.Show("El resultado tiene que ser diferente de vacio para confirmar la balanza");
            }
        }
        private void editarBascula()
        {
            Lcaja parametros = new Lcaja();
            Editar_datos funcion = new Editar_datos();
            parametros.Id_Caja= idcajavariable;
            parametros.EstadoBalanza = "CONFIRMADO";
            parametros.PuertoBalanza = cbListarPuertos.Text;
            if (funcion.EditarBascula(parametros) == true)
            {
                MessageBox.Show("Balanza configurada y guardada correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
        }
    }
}
