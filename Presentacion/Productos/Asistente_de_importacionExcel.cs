﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using SpreadsheetLight;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Punto_de_venta.Presentacion.Productos
{
    public partial class Asistente_de_importacionExcel : Form
    {
        public Asistente_de_importacionExcel()
        {
            InitializeComponent();
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            try
            {
                string ruta;
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    ruta = folderBrowserDialog1.SelectedPath + "ProductosJojama.xlsx";
                    SLDocument NombredeExcel = new SLDocument();
                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt.Columns.Add("Codigo", typeof(string));
                    dt.Columns.Add("Descripcion", typeof(string));
                    dt.Columns.Add("Precio_de_compra", typeof(string));                  
                    dt.Columns.Add("Precio_de_venta", typeof(string));
                    dt.Columns.Add("Cantidad", typeof(string));
                    dt.Columns.Add("Stock", typeof(string));
                    dt.Columns.Add("Stock_minimo", typeof(string));
                    dt.Columns.Add("Impuesto", typeof(string)); 


                    NombredeExcel.ImportDataTable(1, 1, dt, true);
                    NombredeExcel.SaveAs(ruta);
                    MessageBox.Show("Plantilla Obtenida ubicala en: " + ruta, "Archivo Excel Creado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                 MessageBox.Show("No se pudo completar el proceso", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);;
            }
        }

        private void TSIGUIENTE_Y_GUARDAR__Click(object sender, EventArgs e)
        {
            PanelDescarga_de_archivo.Visible = false;
            PanelCargarArchivo.Visible = true;
            B1.Enabled = false;
            B2.Enabled = true;
            B3.Enabled = false;
            Paso1.Visible = false;
            Paso2.Visible = true;
            Paso3.Visible = false;

        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog myFileDialog = new OpenFileDialog();
            myFileDialog.InitialDirectory = @"c:\\temp\";
            myFileDialog.Filter = "CSV files|*.csv;*.CSV)";
            myFileDialog.FilterIndex = 2;
            myFileDialog.RestoreDirectory = true;
            myFileDialog.Title = "Elija el Archivo .CSV";
            if (myFileDialog.ShowDialog() == DialogResult.OK)
            {
                lblnombre_Del_archivo.Text = myFileDialog.SafeFileName.ToString();
                lblArchivoListo.Text = lblnombre_Del_archivo.Text;
                lblRuta.Text = myFileDialog.FileName.ToString();
                archivo_correcto();
            }
        }
        private void archivo_correcto()
        {
            PanelCargarArchivo.BackColor = Color.White;
            lblarchivoCargado.Visible = true;
            label3.Visible = false;
            MenuStrip1.Visible = true;
            Pcsv.Visible = true;
            LinkLabel3.LinkColor = Color.Black;
            lblnombre_Del_archivo.ForeColor = Color.FromArgb(64, 64, 64);
            PanelCargarArchivo.BackgroundImage = null;

        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PanelCargarArchivo.Visible = false;
            PanelGuardarData.Visible = true;
            B1.Enabled = false;
            B2.Enabled = false;
            B3.Enabled = true;
            Paso1.Visible = false;
            Paso2.Visible = false;
            Paso3.Visible = true;         

        }

        private void Label11_Click(object sender, EventArgs e)
        {
            guardar_datos_Precargados();
        }
        private void guardar_datos_Precargados()
        {
            string Textlines = "";
            string[] Splitline;
            if (System.IO.File.Exists(lblRuta.Text) == true)
            {
                System.IO.StreamReader objReader = new System.IO.StreamReader(lblRuta.Text);
                while (objReader.Peek() != -1)
                {
                    Textlines = objReader.ReadLine();
                    Splitline = Textlines.Split(';');
                    datalistado.ColumnCount = Splitline.Length;
                    datalistado.Rows.Add(Splitline);
                }
            }
            else
            {
                MessageBox.Show("Archivo Inexistente", "CSV Inexistente");
            }
  
            try
            {
                foreach (DataGridViewRow row in datalistado.Rows)
                {
                    rellenar_vacios();
                    string CODIGO = Convert.ToString(row.Cells["Codigo"].Value);
                    string descripcion = Convert.ToString(row.Cells["Descripcion"].Value);
                    string precio_de_compra = Convert.ToString(row.Cells["Precio_de_compra"].Value);                  
                    string precio_de_venta = Convert.ToString(row.Cells["Precio_de_venta"].Value);
                    string impuesto = Convert.ToString(row.Cells["Impuesto"].Value);
                    string stock = Convert.ToString(row.Cells["Stock"].Value);
                    string stock_minimo = Convert.ToString(row.Cells["Stock_minimo"].Value);
                    string cantidad = Convert.ToString(row.Cells["Cantidad"].Value);


                    SqlCommand cmd;
                    ConexionDt.ConexionData.conectar.Open();
                    cmd = new SqlCommand("insertar_Producto_Importacion", ConexionDt.ConexionData.conectar);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@Imagen", ".");
                    cmd.Parameters.AddWithValue("@Usa_inventarios", "SI");
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@Precio_de_compra", precio_de_compra);
                    cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", "NO APLICA");
                    cmd.Parameters.AddWithValue("@Precio_de_venta", precio_de_venta);
                    cmd.Parameters.AddWithValue("@Codigo", CODIGO);

                    cmd.Parameters.AddWithValue("@Se_vende_a", "Unidad");
                    cmd.Parameters.AddWithValue("@Impuesto", impuesto);
                    cmd.Parameters.AddWithValue("@Stock_minimo", stock_minimo);
                    cmd.Parameters.AddWithValue("@Precio_mayoreo", 0);
                    cmd.Parameters.AddWithValue("@A_partir_de", 0);
                    cmd.Parameters.AddWithValue("@Fecha", DateTime.Today);
                    cmd.Parameters.AddWithValue("@Motivo", "Registro inicial de Producto");
                    cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@Id_usuario", Productos.idusuario);
                    cmd.Parameters.AddWithValue("@Tipo", "ENTRADA");
                    cmd.Parameters.AddWithValue("@Estado", "CONFIRMADO");
                    cmd.Parameters.AddWithValue("@Id_caja", Productos.idcaja);
                    cmd.ExecuteNonQuery();
                    ConexionDt.ConexionData.conectar.Close();

                }
                MessageBox.Show("Importacion Exitosa", "Importacion de Datos");
                Dispose();
            }
            catch (Exception ex)
            {
                 MessageBox.Show("No se pudo completar el proceso", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);;
            }

        }
        private void rellenar_vacios()
        {
            foreach (DataGridViewRow row in datalistado.Rows)
            {
                if (row.Cells["Descripcion"].Value.ToString() == "")
                {
                    row.Cells["Descripcion"].Value = "VACIO@";
                }
                if (row.Cells["Codigo"].Value.ToString() == "")
                {
                    row.Cells["Codigo"].Value = "VACIO@";
                }
                if (row.Cells["Precio_de_compra"].Value.ToString() == "")
                {
                    row.Cells["Precio_de_compra"].Value = "VACIO@";
                }
                if (row.Cells["Precio_de_venta"].Value.ToString() == "")
                {
                    row.Cells["Precio_de_venta"].Value = "VACIO@";
                }
                if (row.Cells["cantidad"].Value.ToString() == "")
                {
                    row.Cells["cantidad"].Value = "VACIO@";
                }
                if (row.Cells["Stock"].Value.ToString() == "")
                {
                    row.Cells["Stock"].Value = "VACIO@";
                }
                if (row.Cells["Stock_minimo"].Value.ToString() == "")
                {
                    row.Cells["Stock_minimo"].Value = "VACIO@";
                }
                if (row.Cells["Impuesto"].Value.ToString() == "")
                {
                    row.Cells["Impuesto"].Value = "VACIO@";
                }
           
            }
        }

        private void PanelCargarArchivo_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (String[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string path in files)
            {
                lblRuta.Text = path;
                string ruta = lblRuta.Text;
                if (ruta.Contains(".csv"))
                {
                    archivo_correcto();
                    lblnombre_Del_archivo.Text = Path.GetFileName(ruta);
                    lblArchivoListo.Text = lblnombre_Del_archivo.Text;
                }
                else
                {
                    MessageBox.Show("Archivo Incorrecto", "Formato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }

        private void PanelCargarArchivo_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Asistente_de_importacionExcel_Load(object sender, EventArgs e)
        {
            B1.Enabled = true;
            B2.Enabled = false;
            B3.Enabled = false;
            Paso1.Visible = true;
            Paso2.Visible = false;
            Paso3.Visible = false;
        }
    }
}
