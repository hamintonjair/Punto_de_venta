﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Punto_de_venta.Modulos.Licencias_Membresias
{
    public partial class Membresias : Form
    {
        public Membresias()
        {
            InitializeComponent();
        }

        private void Membresias_Load(object sender, EventArgs e)
        {
            ManagementObject MOS = new ManagementObject(@"Win32_PhysicalMedia='\\.\PHYSICALDRIVE0'");

            lblSerialPc.Text = MOS.Properties["SerialNumber"].Value.ToString();
            lblSerialPc.Text = lblSerialPc.Text.Trim();
            lblSerialPc.Text = ConexionDt.Encryptar_en_texto.Encriptar(lblSerialPc.Text);
            lblIDSERIAL.Text = ConexionDt.Encryptar_en_texto.Encriptar(MOS.Properties["SerialNumber"].Value.ToString());
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            dlg.InitialDirectory = "";
            dlg.Filter = "Licencia jojama|*.xml";
            dlg.FilterIndex = 2;
            dlg.Title = "Cargador de Licencia JOJAMA";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    lblruta.Text = Path.GetFullPath(dlg.FileName);
                    ReadfromXML_SERIAL_PC();
                    string cadena = lblarchivo1.Text;

                    String[] separadas;

                    separadas = cadena.Split(Convert.ToChar("|"));
                    lblSerial.Text = separadas[1];
                    lblFecha.Text = separadas[2];
                    lblEstado.Text = separadas[3];
                    lblSoftware.Text = separadas[4];

                }
                catch (Exception ex)
                {

                }

                if (lblSoftware.Text == "jojama" & lblEstado.Text == "PENDIENTE")


                {
                    MessageBox.Show("0");
                    ACTIVACION_DE_LICENCIA_manual();
                    MOSTRAR_licencia_temporal();


                    try
                    {
                        txtfecha_final_licencia_temporal.Value = Convert.ToDateTime(lblFecha.Text);
                        lblSerialPcLocal.Text = (datalistado_licencia_temporal.SelectedCells[2].Value.ToString());
                        LBLESTADOLicenciaLocal.Text = ConexionDt.Encryptar_en_texto.Desencriptar(datalistado_licencia_temporal.SelectedCells[4].Value.ToString());
                        txtfecha_inicio_licencia.Value = Convert.ToDateTime(ConexionDt.Encryptar_en_texto.Desencriptar(datalistado_licencia_temporal.SelectedCells[5].Value.ToString()));

                    }
                    catch (Exception ex)
                    {
                    }
                    if (txtfecha_final_licencia_temporal.Value >= TXTFECHA_SISTEMA.Value && lblSerialPcLocal.Text == lblIDSERIAL.Text)
                    {
                        MessageBox.Show("1");
                        if (txtfecha_inicio_licencia.Value <= TXTFECHA_SISTEMA.Value)
                        {

                            if (LBLESTADOLicenciaLocal.Text == "?ACTIVADO PRO?")
                            {
                                PanelActivando_licencia.Visible = true;
                                PanelActivando_licencia.Dock = DockStyle.Fill;
                                PictureBox1.Visible = false;
                                lblActivando_licencia.Text = "Licencia Activada hasta " + lblFecha.Text;

                            }

                        }
                        else
                        {

                        }
                    }

                }
                else
                {
                    MessageBox.Show("Archivo de licencia rechazado por Datos Incorrectos", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

        }
        private void MOSTRAR_licencia_temporal()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();
                da = new SqlDataAdapter("select * from Marcan", con);
                da.Fill(dt);
                datalistado_licencia_temporal.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {

            }
        }
        private ConexionDt.AES aes = new ConexionDt.AES();
        internal void ACTIVACION_DE_LICENCIA_manual()
        {
            string SERIALpC;
            SERIALpC = lblIDSERIAL.Text;

            string FECHA_FINAL;
            FECHA_FINAL = ConexionDt.Encryptar_en_texto.Encriptar(this.lblFecha.Text.Trim());
            string estado;
            estado = ConexionDt.Encryptar_en_texto.Encriptar("?ACTIVADO PRO?");
            string fecha_activacion;
            fecha_activacion = ConexionDt.Encryptar_en_texto.Encriptar(this.TXTFECHA_SISTEMA.Text.Trim());
            try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("EDITAR_marcan_a", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@e", estado);
                cmd.Parameters.AddWithValue("@fa", fecha_activacion);
                cmd.Parameters.AddWithValue("@f", FECHA_FINAL);
                cmd.Parameters.AddWithValue("@s", SERIALpC);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

            string dbcnString;
        public void ReadfromXML_SERIAL_PC()
        {

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(lblruta.Text);
                XmlElement root = doc.DocumentElement;
                dbcnString = root.Attributes[0].Value;
                lblarchivo1.Text = (aes.Decrypt(dbcnString, ConexionDt.Desencryptacion.appPwdUnique, int.Parse("256")));

            }
            catch (System.Security.Cryptography.CryptographicException ex)
            {


            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
