using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Windows.Forms;

namespace Punto_de_venta.Modulos.Productos
{
    public partial class Productoss : Form
    {
        int txtcontador;
        public Productoss()
        {
            InitializeComponent();
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            PANELDEPARTAMENTO.Visible = true;
            CheckInventarios.Checked = true;
            PANELINVENTARIO.Visible = true;
            PanelGRUPOSSELECT.Visible = true;
            btnGuardar_grupo.Visible = false;
            BtnGuardarCambios.Visible = false;
            BtnCancelar.Visible = false;
            btnNuevoGrupo.Visible = true;
            mostrar_grupos();
            txtgrupo.Text = "";
            txtPorcentajeGanancia.Clear();

            lblEstadoCodigo.Text = "NUEVO";
            PanelGRUPOSSELECT.Visible = true;
            btnGuardar_grupo.Visible = false;
            BtnGuardarCambios.Visible = false;
            BtnCancelar.Visible = false;
            btnNuevoGrupo.Visible = true;
            mostrar_grupos();

            txtapartirde.Text = "0";
            txtstock2.ReadOnly = false;
            Panel25.Enabled = true;
            Panel21.Visible = false;
            Panel22.Visible = false;
            Panel18.Visible = false;
            TXTIDPRODUCTOOk.Text = "0";

            PANELINVENTARIO.Visible = true;

            txtdescripcion.AutoCompleteCustomSource = ConexionDt.DataHelper.LoadAutoComplete();
            txtdescripcion.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtdescripcion.AutoCompleteSource = AutoCompleteSource.CustomSource;

            PANELDEPARTAMENTO.Visible = true;
            porunidad.Checked = true;
            No_aplica_fecha.Checked = false;
            Panel6.Visible = false;

            LIMPIAR();        
            btnagregaryguardar.Visible = true;
            btnagregar.Visible = false;


            txtdescripcion.Text = "";
            PANELINVENTARIO.Visible = true;


            TGUARDAR.Visible = true;
            TGUARDARCAMBIOS.Visible = false;

        }
        internal void LIMPIAR()
        {
            txtidproducto.Text = "";
            txtdescripcion.Text = "";
            txtcosto.Text = "0";
            TXTPRECIODEVENTA2.Text = "0";
            txtpreciomayoreo.Text = "0";
            txtgrupo.Text = "";

            agranel.Checked = false;
            txtstockminimo.Text = "0";
            txtstock2.Text = "0";
            lblEstadoCodigo.Text = "NUEVO";
        }

        private void Productoss_Load(object sender, EventArgs e)
        {
            PANELDEPARTAMENTO.Visible = false;
        }

        private void btnGuardar_grupo_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("insertar_Grupo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Grupo", txtgrupo.Text);
                cmd.Parameters.AddWithValue("@Por_defecto", "NO");
                cmd.ExecuteNonQuery();
                con.Close();
                mostrar_grupos();

                lblIdGrupo.Text = datalistadoGrupos.SelectedCells[2].Value.ToString();
                txtgrupo.Text = datalistadoGrupos.SelectedCells[3].Value.ToString();

                PanelGRUPOSSELECT.Visible = false;
                btnGuardar_grupo.Visible = false;
                BtnGuardarCambios.Visible = false;
                BtnCancelar.Visible = false;
                btnNuevoGrupo.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void mostrar_grupos()
        {
            PanelGRUPOSSELECT.Visible = true;
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();

                da = new SqlDataAdapter("mostrar_grupos", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@buscar", txtgrupo.Text);
                da.Fill(dt);
                datalistadoGrupos.DataSource = dt;
                con.Close();

                datalistadoGrupos.DataSource = dt;
                datalistadoGrupos.Columns[2].Visible = false;
                datalistadoGrupos.Columns[3].Width = 500;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           //ConexionDt.Tamaño_automatico_de_datatables.Multilinea(ref datalistado);
        }

        private void btnNuevoGrupo_Click(object sender, EventArgs e)
        {
            txtgrupo.Text = "Escribe el Nuevo GRUPO";
            txtgrupo.SelectAll();
            txtgrupo.Focus();

            PanelGRUPOSSELECT.Visible = false;
            btnGuardar_grupo.Visible = true;
            BtnGuardarCambios.Visible = false;
            BtnCancelar.Visible = true;
            btnNuevoGrupo.Visible = false;
        }

        private void txtgrupo_TextChanged(object sender, EventArgs e)
        {
            mostrar_grupos();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            PanelGRUPOSSELECT.Visible = false;
            btnGuardar_grupo.Visible = false;
            BtnGuardarCambios.Visible = false;
            BtnCancelar.Visible = false;
            btnNuevoGrupo.Visible = true;
            txtgrupo.Clear();
            mostrar_grupos();
        }
        //private void buscar()
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        SqlDataAdapter da;
        //        SqlConnection con = new SqlConnection();
        //        con.ConnectionString = ConexionDt.ConexionData.conexion;
        //        con.Open();

        //        da = new SqlDataAdapter("buscar_producto_por_descripcion", con);
        //        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        da.SelectCommand.Parameters.AddWithValue("@letra", txtbusca.Text);
        //        da.Fill(dt);
        //        datalistado.DataSource = dt;
        //        con.Close();

        //        datalistado.Columns[2].Visible = false;
        //        datalistado.Columns[7].Visible = false;
        //        datalistado.Columns[10].Visible = false;
        //        datalistado.Columns[15].Visible = false;
        //        datalistado.Columns[16].Visible = false;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);

        //    }

        //    CONEXION.Tamaño_automatico_de_datatables.Multilinea(ref datalistado);
        //    sumar_costo_de_inventario_CONTAR_PRODUCTOS();
        //}
    }
}
