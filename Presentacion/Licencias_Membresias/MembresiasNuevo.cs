using Punto_de_venta.ConexionDt;
using Punto_de_venta.Datos;
using Punto_de_venta.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Punto_de_venta.Presentacion.Licencias_Membresias
{
    public partial class MembresiasNuevo : Form
    {
        public MembresiasNuevo()
        {
            InitializeComponent();
        }
        string serialPc;
        string ruta;
        string dbcnString;
        string LicenciaDescifrada;
        private AES aes = new AES();
        string SerialPcLicencia;
        string FechaFinLicencia;
        string EstadoLicencia;
        string NombreSoftwareLicencia;
        string Resultado;
        private void obtenerSerialPc()
        {
            Bases.Obtener_serialPC(ref serialPc);
            txtSerial.Text = serialPc;

        }

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtSerial.Text);
        }

        private void MembresiasNuevo_Load(object sender, EventArgs e)
        {
            obtenerSerialPc();           
        }

        private void btnActivacioManual_Click(object sender, EventArgs e)
        {
            dlg.Filter = "Licencias jojama|*.xml";
            dlg.Title = "Cargador de Licencias jojama";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ruta = Path.GetFullPath(dlg.FileName);
                DescifrarLicencia();
                string cadena = LicenciaDescifrada;
                string[] separadas = cadena.Split('|');
                SerialPcLicencia = separadas[1];
                FechaFinLicencia = separadas[2];
                EstadoLicencia = separadas[3];
                NombreSoftwareLicencia = separadas[4];
                if (NombreSoftwareLicencia == "jojama")
                {
                    if (EstadoLicencia == "PENDIENTE")
                    {
                        if (SerialPcLicencia == serialPc)
                        {
                            activarLicenciaManual();
                        }
                    }
                }

            }          

        }
        private void activarLicenciaManual()
        {
            Bases.Obtener_serialPC(ref serialPc);
            string fechaFin = Bases.Encriptar(FechaFinLicencia);
            string estado = Bases.Encriptar("?ACTIVADO PRO?");
            string fechaActivacion = Bases.Encriptar(DateTime.Now.ToString());
            LMarcan parametros = new LMarcan();
            Editar_datos funcion = new Editar_datos();
            parametros.E = estado;
            parametros.FA = fechaActivacion;
            parametros.F = fechaFin;
            parametros.S = txtSerial.Text;          

            if (funcion.editarMarcan(parametros) == true)
            {
                MessageBox.Show("Licencia activada, se cerrara el sistema para un nuevo Inicio");
                Application.Exit();
            }
        }
        private void DescifrarLicencia()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(ruta);
                XmlElement root = doc.DocumentElement;
                dbcnString = root.Attributes.Item(0).Value;
                LicenciaDescifrada = (aes.Decrypt(dbcnString, Desencryptacion.appPwdUnique, int.Parse("256")));
            }
            catch (CryptographicException ex)
            {

            }
        }
    }
}
