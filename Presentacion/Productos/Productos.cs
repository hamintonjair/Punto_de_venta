﻿using System;
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
using System.Threading;
using Punto_de_venta.Logica;
using Punto_de_venta.Datos;
using iText.Kernel.Pdf;
using DocumentFormat.OpenXml.Office2013;
//using DocumentFormat.OpenXml.Wordprocessing;
using FontFamily = System.Drawing.FontFamily;
using Font = System.Drawing.Font;
using Color = System.Drawing.Color;
using iText.Layout;
using iText.Kernel.Geom;
using iText.IO.Image;
using iText.Layout.Element;
using Punto_de_venta.ConexionDt;
using BarcodeLib;

namespace Punto_de_venta.Presentacion.Productos
{
    public partial class Productos : Form
    {
        int txtcontador;
        public Productos()
        {
            InitializeComponent();
       
        }
        string lblSerialPc;
        string lblIDSERIALL;
        int porcentaje;
        int porcentajeFinal;
        string UsaIva;
        double Subtotalventa;
        double ValorImpuesto19;
        double ValorImpuesto5;
        double sinIVA;
        private void PictureBox2_Click(object sender, EventArgs e)
        {
        

            datalistado.Visible = false;
            panelFrom.Visible = true;          
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
        public static int idusuario;
        public static int idcaja;
        internal void LIMPIAR()
        {
            txtidproducto.Text = "";
            txtdescripcion.Text = "";
            txtcosto.Text = "0";
            TXTPRECIODEVENTA2.Text = "0";
            txtpreciomayoreo.Text = "0";
            txtgrupo.Text = "";
            PanelImpuesto.Visible = false;
            label22.Visible = false;            
            agranel.Checked = false;
            Servicios.Checked = false;
            no.Checked = true;
            
            txtstockminimo.Text = "0";
            txtstock2.Text = "0";
            lblEstadoCodigo.Text = "NUEVO";
        }
        //public static int idusuario;
        private void Productoss_Load(object sender, EventArgs e)
        {          
            if (txtbusca.Text == "")
            {
                ControlSetFocus();
            }
         
           
            //ControlSetFocus();
            mostrar();
            Bases.Cambiar_idioma_regional();

            PANELDEPARTAMENTO.Visible = false;
            txtbusca.Text = "Buscar...";
            sumar_costo_de_inventario_CONTAR_PRODUCTOS();
            buscar();
            mostrar_grupos();
            int id = Presentacion.Admin_nivel_dios.DASHBOARD_PRINCIPAL.idcajavariable;
            Bases.Obtener_serialPC(ref lblSerialPc);

            Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);

            if (id == Convert.ToInt32(1))
            {
                Obtener_datos.mostrar_inicio_De_sesion2(ref idusuario);
            }
            else
            {
                Obtener_datos.mostrar_inicio_De_sesion(ref idusuario);
            }
            no.Checked = true;

            if (txtbusca.Text != "")
            {
                ControlSetFocus();
            }
            if (txtbusca.Text == "")
            {
                ControlSetFocus();
            }

        }
 
        private void mostrar()
        {
            try
            {
                string Impuesto;
                string Impuesto2;
                string iva;
                Impuesto = "SELECT Porcentaje_Impuesto FROM EMPRESA";
                Impuesto2 = "SELECT Porcentaje_otros_Impuesto FROM EMPRESA";
                iva = "SELECT Impuesto FROM EMPRESA";
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                SqlCommand Porcentaje = new SqlCommand(Impuesto, con);
                SqlCommand Porcentaje1 = new SqlCommand(Impuesto2, con);
                SqlCommand Porcentaje2 = new SqlCommand(iva, con);
                con.Open();
                txtporcentaje.Text = Porcentaje.ExecuteScalar().ToString();
                txtporcentaje2.Text = Porcentaje1.ExecuteScalar().ToString();
                txtimpuesto.Text = Porcentaje2.ExecuteScalar().ToString();
                porcentajeFinal = Convert.ToInt32(txtporcentaje2.Text);
                con.Close();

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
                ConexionDt.ConexionData.conectar.Open();

                da = new SqlDataAdapter("mostrar_grupos", ConexionDt.ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@buscar", txtgrupo.Text);
                da.Fill(dt);
                datalistadoGrupos.DataSource = dt;
                ConexionDt.ConexionData.conectar.Close();

                datalistadoGrupos.DataSource = dt;
                datalistadoGrupos.Columns[2].Visible = false;
                datalistadoGrupos.Columns[3].Width = 500;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Logica.Bases.Multilinea2(ref datalistado);
        }
                           
        private void GENERAR_CODIGO_DE_BARRAS_AUTOMATICO()
        {
            Double resultado;
            string queryMoneda;
            queryMoneda = "SELECT max(Id_Producto1)  FROM Producto1";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConexionDt.ConexionData.conexion;
            SqlCommand comMoneda = new SqlCommand(queryMoneda, con);
            try
            {
                con.Open();
                resultado = Convert.ToDouble(comMoneda.ExecuteScalar()) + 1;
                con.Close();
            }
            catch (Exception ex)
            {
                resultado = 1;
            }

            string Cadena = txtgrupo.Text;
            string[] Palabra;
            String espacio = " ";
      
            Palabra = Cadena.Split(Convert.ToChar(espacio));
            try
            {
                txtcodigodebarras.Text = resultado + Palabra[0].Substring(0, 2) + 761;
            }
            catch (Exception ex)
            {
            }
        }      
           
        private void insertar_productos()
        {
            if (txtpreciomayoreo.Text == "0" | txtpreciomayoreo.Text == "") txtapartirde.Text = "0";

            try  
            {
                ConexionDt.ConexionData.conectar.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("insertar_Producto", ConexionDt.ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Descripcion", txtdescripcion.Text);
                cmd.Parameters.AddWithValue("@Imagen", ".");
                cmd.Parameters.AddWithValue("@Precio_de_compra", txtcosto.Text);
                cmd.Parameters.AddWithValue("@Precio_de_venta", TXTPRECIODEVENTA2.Text);
                cmd.Parameters.AddWithValue("@Codigo", txtcodigodebarras.Text);
                cmd.Parameters.AddWithValue("@A_partir_de", txtapartirde.Text);
                cmd.Parameters.AddWithValue("@Impuesto", porcentaje);
                cmd.Parameters.AddWithValue("@Precio_mayoreo", txtpreciomayoreo.Text);           

                if (porunidad.Checked == true) txtse_vende_a.Text = "Unidad";
                if (agranel.Checked == true) txtse_vende_a.Text = "Granel";
                if (Servicios.Checked == true) txtse_vende_a.Text = "Servicios";

                cmd.Parameters.AddWithValue("@Se_vende_a", txtse_vende_a.Text);
                cmd.Parameters.AddWithValue("@Id_grupo", lblIdGrupo.Text);
                if (PANELINVENTARIO.Visible == true)
                {
                    cmd.Parameters.AddWithValue("@Usa_inventarios", "SI");
                    cmd.Parameters.AddWithValue("@Stock_minimo", txtstockminimo.Text);
                    cmd.Parameters.AddWithValue("@Stock", txtstock2.Text);

                    if (No_aplica_fecha.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", "NO APLICA");
                    }

                    if (No_aplica_fecha.Checked == false)
                    {
                       
                     
                        cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", txtfechaoka.Text);
                    }
                }
                if (PANELINVENTARIO.Visible == false)
                {
                    cmd.Parameters.AddWithValue("@Usa_inventarios", "NO");
                    cmd.Parameters.AddWithValue("@Stock_minimo", 0);
                    cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", "NO APLICA");
                    cmd.Parameters.AddWithValue("@Stock", "Ilimitado");
                }
                cmd.Parameters.AddWithValue("@Fecha", DateTime.Today);
                cmd.Parameters.AddWithValue("@Motivo", "Registro inicial de Producto");
                cmd.Parameters.AddWithValue("@Cantidad ", txtstock2.Text);
                cmd.Parameters.AddWithValue("@Id_usuario", idusuario);
                cmd.Parameters.AddWithValue("@Tipo", "ENTRADA");
                cmd.Parameters.AddWithValue("@Estado", "CONFIRMADO");
                cmd.Parameters.AddWithValue("@Id_caja", idcaja);
                cmd.Parameters.AddWithValue("@Sub_total_pv", Subtotalventa);
                cmd.Parameters.AddWithValue("@Valor_Impuesto_5", ValorImpuesto5);
                cmd.Parameters.AddWithValue("@Valor_Impuesto_19", ValorImpuesto19);
                cmd.Parameters.AddWithValue("@Sin_IVA", sinIVA);


                cmd.ExecuteNonQuery();

                ConexionDt.ConexionData.conectar.Close();
                PANELDEPARTAMENTO.Visible = false;
                panelFrom.Visible = true;
                datalistado.Visible = true;
                panederecho.Visible = true;
                panelizquierdo.Visible = true;
                txtbusca.Text = txtdescripcion.Text;
                buscar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ConexionDt.ConexionData.conectar.Close();
            }

        }
        private void editar_productos()
        {
            if (txtpreciomayoreo.Text == "0" | txtpreciomayoreo.Text == "") txtapartirde.Text = "0";

            try
            {
                ConexionDt.ConexionData.conectar.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("editar_Producto1", ConexionDt.ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Producto1", TXTIDPRODUCTOOk.Text);
                cmd.Parameters.AddWithValue("@Descripcion", txtdescripcion.Text);
                cmd.Parameters.AddWithValue("@Imagen", ".");

                cmd.Parameters.AddWithValue("@Precio_de_compra", txtcosto.Text);
                cmd.Parameters.AddWithValue("@Precio_de_venta", TXTPRECIODEVENTA2.Text);
                cmd.Parameters.AddWithValue("@Codigo", txtcodigodebarras.Text);
                cmd.Parameters.AddWithValue("@A_partir_de", txtapartirde.Text);
                cmd.Parameters.AddWithValue("@Impuesto", porcentaje);
                cmd.Parameters.AddWithValue("@Precio_mayoreo", txtpreciomayoreo.Text);
                if (porunidad.Checked == true) txtse_vende_a.Text = "Unidad";
                if (agranel.Checked == true) txtse_vende_a.Text = "Granel";
                if (Servicios.Checked == true) txtse_vende_a.Text = "Servicios";

                cmd.Parameters.AddWithValue("@Se_vende_a", txtse_vende_a.Text);
                cmd.Parameters.AddWithValue("@Id_grupo", lblIdGrupo.Text);
                if (PANELINVENTARIO.Visible == true)
                {
                    cmd.Parameters.AddWithValue("@Usa_inventarios", "SI");
                    cmd.Parameters.AddWithValue("@Stock_minimo", txtstockminimo.Text);
                    cmd.Parameters.AddWithValue("@Stock", txtstock2.Text);

                    if (No_aplica_fecha.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", "NO APLICA");
                    }

                    if (No_aplica_fecha.Checked == false)
                    {
                   
                        cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", txtfechaoka.Text);
                      
                    }

                }
                if (PANELINVENTARIO.Visible == false)
                {
                    cmd.Parameters.AddWithValue("@Usa_inventarios", "NO");
                    cmd.Parameters.AddWithValue("@Stock_minimo", 0);
                    cmd.Parameters.AddWithValue("@Fecha_de_vencimiento", "NO APLICA");
                    cmd.Parameters.AddWithValue("@Stock", "Ilimitado");
                }
                cmd.Parameters.AddWithValue("@Sub_total_pv", Subtotalventa);
                cmd.Parameters.AddWithValue("@Valor_Impuesto_5", ValorImpuesto5);
                cmd.Parameters.AddWithValue("@Valor_Impuesto_19", ValorImpuesto19);
                cmd.Parameters.AddWithValue("@Sin_IVA", sinIVA); 
                cmd.ExecuteNonQuery();

                ConexionDt.ConexionData.conectar.Close();
                PANELDEPARTAMENTO.Visible = false;
                txtbusca.Text = txtdescripcion.Text;
                buscar();

                panederecho.BackColor = System.Drawing.Color.FromArgb(129, 178, 20);
                panelizquierdo.BackColor = System.Drawing.Color.FromArgb(129, 178, 20);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                panederecho.BackColor = System.Drawing.Color.FromArgb(255, 204, 41);
                panelizquierdo.BackColor = System.Drawing.Color.FromArgb(255, 204, 41);
                ConexionDt.ConexionData.conectar.Close();
            }

        }
        private void buscar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                ConexionDt.ConexionData.conectar.Open();

                da = new SqlDataAdapter("buscar_producto_por_descripcion", ConexionDt.ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", txtbusca.Text);
                da.Fill(dt);
                datalistado.DataSource = dt;
                ConexionDt.ConexionData.conectar.Close();

                datalistado.Columns[2].Visible = false;
                datalistado.Columns[7].Visible = false;
                datalistado.Columns[10].Visible = false;
                datalistado.Columns[15].Visible = false;
                datalistado.Columns[16].Visible = false;

            }           
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Logica.Bases.Multilinea(ref datalistado);
            sumar_costo_de_inventario_CONTAR_PRODUCTOS();
        }
        private void contar()
        {
            int x;

            x = DATALISTADO_PRODUCTOS_OKA.Rows.Count;
            txtcontador = (x);
        }
       
        internal void sumar_costo_de_inventario_CONTAR_PRODUCTOS()
        {

            //string resultado;
            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = CONEXION.CONEXIONMAESTRA.conexion;
            //SqlCommand da = new SqlCommand("buscar_USUARIO_por_correo", con);
            //da.CommandType = CommandType.StoredProcedure;
            //da.Parameters.AddWithValue("@correo", txtcorreo.Text);

            //con.Open();
            //lblResultadoContraseña.Text = Convert.ToString(da.ExecuteScalar());
            //con.Close();

            string resultado;
            string queryMoneda;
            queryMoneda = "SELECT Moneda  FROM EMPRESA";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConexionDt.ConexionData.conexion;
            SqlCommand comMoneda = new SqlCommand(queryMoneda, con);
            try
            {
                con.Open();
                resultado = Convert.ToString(comMoneda.ExecuteScalar()); //asignamos el valor del importe
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                resultado = "";
            }

            double importe;
            string query;
            query = "SELECT      CONVERT(NUMERIC(18,0),sum(Producto1.Precio_de_compra * Stock )) as suma FROM  Producto1 where  Usa_inventarios ='SI'";

            SqlCommand com = new SqlCommand(query, con);
            try
            {
                con.Open();
                importe = Convert.ToDouble(com.ExecuteScalar()); //asignamos el valor del importe
        
                con.Close();
                lblcosto_inventario.Text = resultado + " " + importe.ToString("N0"); 
            }
            catch (Exception ex)
            {
                con.Close();              

                lblcosto_inventario.Text = resultado + " " + 0;
            }

            string conteoresultado;
            string querycontar;
            querycontar = "select count(Id_Producto1 ) from Producto1 ";
            SqlCommand comcontar = new SqlCommand(querycontar, con);
            try
            {
                con.Open();
                conteoresultado = Convert.ToString(comcontar.ExecuteScalar()); //asignamos el valor del importe
                con.Close();
                lblcantidad_productos.Text = conteoresultado;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);

                conteoresultado = "";
                lblcantidad_productos.Text = "0";
            }

        }
        private void mostrar_descripcion_produco_sin_repetir()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                ConexionDt.ConexionData.conectar.Open();

                da = new SqlDataAdapter("mostrar_descripcion_produco_sin_repetir", ConexionDt.ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@buscar", txtdescripcion.Text);
                da.Fill(dt);
                DATALISTADO_PRODUCTOS_OKA.DataSource = dt;
                ConexionDt.ConexionData.conectar.Close();

                datalistado.Columns[1].Width = 500;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }        
               
        internal void proceso_para_obtener_datos_de_productos()
        {
            try
            {
                Panel25.Enabled = true;
                DATALISTADO_PRODUCTOS_OKA.Visible = false;

                Panel6.Visible = false;
                TGUARDAR.Visible = false;
                TGUARDARCAMBIOS.Visible = true;
                PANELDEPARTAMENTO.Visible = true;

                btnNuevoGrupo.Visible = true;
                TXTIDPRODUCTOOk.Text = datalistado.SelectedCells[2].Value.ToString();
                lblEstadoCodigo.Text = "EDITAR";
                PanelGRUPOSSELECT.Visible = false;
                BtnGuardarCambios.Visible = false;
                btnGuardar_grupo.Visible = false;
                BtnCancelar.Visible = false;
                btnNuevoGrupo.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                txtidproducto.Text = datalistado.SelectedCells[2].Value.ToString();
                txtcodigodebarras.Text = datalistado.SelectedCells[3].Value.ToString();
                txtgrupo.Text = datalistado.SelectedCells[4].Value.ToString();

                txtdescripcion.Text = datalistado.SelectedCells[5].Value.ToString();
                txtnumeroigv.Text = datalistado.SelectedCells[6].Value.ToString();
                lblIdGrupo.Text = datalistado.SelectedCells[15].Value.ToString();

                LBL_ESSERVICIO.Text = datalistado.SelectedCells[7].Value.ToString();

                txtcosto.Text = datalistado.SelectedCells[8].Value.ToString();
                txtpreciomayoreo.Text = datalistado.SelectedCells[9].Value.ToString();
                LBLSEVENDEPOR.Text = datalistado.SelectedCells[10].Value.ToString();
                if (LBLSEVENDEPOR.Text == "Unidad")
                {
                    porunidad.Checked = true;

                }
                if (LBLSEVENDEPOR.Text == "Granel")
                {
                    agranel.Checked = true;
                }
                if (LBLSEVENDEPOR.Text == "Servicios")
                {
                    Servicios.Checked = true;
                }
                txtstockminimo.Text = datalistado.SelectedCells[11].Value.ToString();
                lblfechasvenci.Text = datalistado.SelectedCells[12].Value.ToString();
                if (lblfechasvenci.Text == "NO APLICA")
                {
                    No_aplica_fecha.Checked = true;
                }
                if (lblfechasvenci.Text != "NO APLICA")
                {
                    No_aplica_fecha.Checked = false;
                }
                txtstock2.Text = datalistado.SelectedCells[13].Value.ToString();
                TXTPRECIODEVENTA2.Text = datalistado.SelectedCells[14].Value.ToString();
                try
                {

                    double TotalVentaVariabledouble;
                    double TXTPRECIODEVENTA2V = Convert.ToDouble(TXTPRECIODEVENTA2.Text);
                    double txtcostov = Convert.ToDouble(txtcosto.Text);

                    TotalVentaVariabledouble = ((TXTPRECIODEVENTA2V - txtcostov) / (txtcostov)) * 100;

                    if (TotalVentaVariabledouble > 0)
                    {
                        this.txtPorcentajeGanancia.Text = Convert.ToString(TotalVentaVariabledouble);
                    }
                    else
                    {
                        //Me.txtPorcentajeGanancia.Text = 0
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                if (LBL_ESSERVICIO.Text == "SI")
                {

                    PANELINVENTARIO.Visible = true;
                    PANELINVENTARIO.Visible = true;
                    txtstock2.ReadOnly = true;
                    CheckInventarios.Checked = true;

                }
                if (LBL_ESSERVICIO.Text == "NO")
                {
                    CheckInventarios.Checked = false;

                    PANELINVENTARIO.Visible = false;
                    PANELINVENTARIO.Visible = false;
                    txtstock2.ReadOnly = true;
                    txtstock2.Text = "0";
                    txtstockminimo.Text = "0";
                    No_aplica_fecha.Checked = true;
                    txtstock2.ReadOnly = false;
                }
                txtapartirde.Text = datalistado.SelectedCells[16].Value.ToString();

                PanelGRUPOSSELECT.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void datalistado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.datalistado.Columns["Eliminar"].Index)
            {
                DialogResult result;
                result = MessageBox.Show("¿Realmente desea eliminar este Producto?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    SqlCommand cmd;
                    try
                    {
                        foreach (DataGridViewRow row in datalistado.SelectedRows)
                        {
                            int onekey = Convert.ToInt32(row.Cells["Id_Producto1"].Value);

                            try
                            {
                                try
                                {
                                    ConexionDt.ConexionData.conectar.Open();
                                    cmd = new SqlCommand("eliminar_Producto1", ConexionDt.ConexionData.conectar);
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@id", onekey);
                                    cmd.ExecuteNonQuery();

                                    ConexionDt.ConexionData.conectar.Close();

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message);
                            }
                        }
                        buscar();
                    }

                    catch (Exception ex)
                    {
                    }
                }
            }
            if (e.ColumnIndex == this.datalistado.Columns["Editar"].Index)
            {
                proceso_para_obtener_datos_de_productos();
                PanelImpuesto.Visible = false;
                txtPorcentajeGanancia.Text = "";
                TXTPRECIODEVENTA2.Text = "0";
                txtpreciomayoreo.Text = "0";
                txtapartirde.Text = "0";
                txtImpuestos.Text = "";

                panederecho.BackColor = System.Drawing.Color.FromArgb(255, 204, 41);
                panelizquierdo.BackColor = System.Drawing.Color.FromArgb(255, 204, 41);
             
            }

        }          
           

        private void txtcosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '.') || (e.KeyChar != ','))
            {
                //char letras2 = '.';

                string CultureName = Thread.CurrentThread.CurrentCulture.Name;
                CultureInfo ci = new CultureInfo(CultureName);

                // Forcing use of decimal separator for numerical values
                ci.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = ci;
                //e.KeyChar = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = (0);
            }
            Separador_de_Numeros(txtcosto, e);
        }
        public static void Separador_de_Numeros(System.Windows.Forms.TextBox CajaTexto, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }

            else if (!(e.KeyChar == CajaTexto.Text.IndexOf('.')))
            {
                e.Handled = true;
            }
            else if (e.KeyChar == '.')
            {
                e.Handled = false;
            }
            else if (e.KeyChar == ',')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void txtbusca_TextChanged(object sender, EventArgs e)
       {
            buscar();
          
        }      
        private void datalistado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            proceso_para_obtener_datos_de_productos();
        }

        private void TimerCalucular_porcentaje_ganancia_Tick(object sender, EventArgs e)
        {
            TimerCalucular_porcentaje_ganancia.Stop();
            try
            {
                double TotalVentaVariabledouble;
                double TXTPRECIODEVENTA2V = Convert.ToDouble(TXTPRECIODEVENTA2.Text);
                double txtcostov = Convert.ToDouble(txtcosto.Text);

                TotalVentaVariabledouble = ((TXTPRECIODEVENTA2V - txtcostov) / (txtcostov)) * 100;

                if (TotalVentaVariabledouble > 0)
                {
                    this.txtPorcentajeGanancia.Text = Convert.ToString(TotalVentaVariabledouble);
                }
                else
                {
                    //Me.txtPorcentajeGanancia.Text = 0
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Tmensajes_Popup(object sender, PopupEventArgs e)
        {

        }
        double venta;
        private void TimerCalcular_precio_venta_Tick(object sender, EventArgs e)
        {
            TimerCalcular_precio_venta.Stop();

            try 
            {
                double TotalVentaVariabledouble;
                double txtcostov = Convert.ToDouble(txtcosto.Text);
                double txtPorcentajeGananciav = Convert.ToDouble(txtPorcentajeGanancia.Text);

                TotalVentaVariabledouble = txtcostov + ((txtcostov * txtPorcentajeGananciav) / 100);

                if (TotalVentaVariabledouble > 0 && txtPorcentajeGanancia.Focused == true)
                {
                    venta = TotalVentaVariabledouble;
                    this.TXTPRECIODEVENTA2.Text = Convert.ToString(TotalVentaVariabledouble);
                }

          
                else
                {
                    //Me.txtPorcentajeGanancia.Text = 0
                }


            }
            catch (Exception ex)
            {

            }
        }

        private void toolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            DATALISTADO_PRODUCTOS_OKA.Visible = false;
        }

        private void txtdescripcion_TextChanged_1(object sender, EventArgs e)
        {

            mostrar_descripcion_produco_sin_repetir();
            contar();


            if (txtcontador == 0)
            {
                DATALISTADO_PRODUCTOS_OKA.Visible = false;
            }
            if (txtcontador > 0)
            {
                DATALISTADO_PRODUCTOS_OKA.Visible = true;
            }
            if (TGUARDAR.Visible == false)
            {
                DATALISTADO_PRODUCTOS_OKA.Visible = false;
            }
        }

        private void DATALISTADO_PRODUCTOS_OKA_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtdescripcion.Text = DATALISTADO_PRODUCTOS_OKA.SelectedCells[1].Value.ToString();
                DATALISTADO_PRODUCTOS_OKA.Visible = false;
            }
            catch (Exception ex)
            {

            }
        }

        private void txtPorcentajeGanancia_TextChanged_1(object sender, EventArgs e)
        {
            TimerCalucular_porcentaje_ganancia.Stop();

            TimerCalcular_precio_venta.Start();
            TimerCalucular_porcentaje_ganancia.Stop();

        }

        private void txtgrupo_TextChanged_1(object sender, EventArgs e)
        {
            mostrar_grupos();
        }

        private void datalistadoGrupos_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.datalistadoGrupos.Columns["EliminarG"].Index)
            {
                DialogResult result;
                result = MessageBox.Show("¿Realmente desea eliminar este Grupo?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    SqlCommand cmd;
                    try
                    {
                        foreach (DataGridViewRow row in datalistadoGrupos.SelectedRows)
                        {

                            int onekey = Convert.ToInt32(row.Cells["Idline"].Value);

                            try
                            {

                                try
                                {

                                    ConexionDt.ConexionData.conectar.Open();
                                    cmd = new SqlCommand("eliminar_grupos", ConexionDt.ConexionData.conectar);
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@id", onekey);
                                    cmd.ExecuteNonQuery();

                                    ConexionDt.ConexionData.conectar.Close();

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message);
                            }

                        }
                        txtgrupo.Text = "GENERAL";
                        mostrar_grupos();
                        lblIdGrupo.Text = datalistadoGrupos.SelectedCells[2].Value.ToString();
                        PanelGRUPOSSELECT.Visible = true;
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            if (e.ColumnIndex == this.datalistadoGrupos.Columns["EditarG"].Index)

            {
                lblIdGrupo.Text = datalistadoGrupos.SelectedCells[2].Value.ToString();
                txtgrupo.Text = datalistadoGrupos.SelectedCells[3].Value.ToString();
                PanelGRUPOSSELECT.Visible = false;
                btnGuardar_grupo.Visible = false;
                BtnGuardarCambios.Visible = true;
                BtnCancelar.Visible = true;
                btnNuevoGrupo.Visible = false;
            }
            if (e.ColumnIndex == this.datalistadoGrupos.Columns["Grupo"].Index)
            {
                lblIdGrupo.Text = datalistadoGrupos.SelectedCells[2].Value.ToString();
                txtgrupo.Text = datalistadoGrupos.SelectedCells[3].Value.ToString();
                PanelGRUPOSSELECT.Visible = false;
                btnGuardar_grupo.Visible = false;
                BtnGuardarCambios.Visible = false;
                BtnCancelar.Visible = false;
                btnNuevoGrupo.Visible = true;
                if (lblEstadoCodigo.Text == "NUEVO")
                {
                    GENERAR_CODIGO_DE_BARRAS_AUTOMATICO();                  
                }

            }
        }
        private void btnGuardar_grupo_Click_1(object sender, EventArgs e)
        {
            try
            {
                ConexionDt.ConexionData.conectar.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("insertar_Grupo", ConexionDt.ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Grupo", txtgrupo.Text);
                cmd.Parameters.AddWithValue("@Por_defecto", "NO");
                cmd.ExecuteNonQuery();
                ConexionDt.ConexionData.conectar.Close();
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

        private void BtnCancelar_Click_1(object sender, EventArgs e)
        {
            PanelGRUPOSSELECT.Visible = false;
            btnGuardar_grupo.Visible = false;
            BtnGuardarCambios.Visible = false;
            BtnCancelar.Visible = false;
            btnNuevoGrupo.Visible = true;
            txtgrupo.Clear();
            mostrar_grupos();
        }

        private void btnNuevoGrupo_Click_1(object sender, EventArgs e)
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

        private void Button2_Click_1(object sender, EventArgs e)
        {
            PANELDEPARTAMENTO.Visible = false;
            datalistado.Visible = true;
            panederecho.BackColor = System.Drawing.Color.FromArgb(129, 178, 20);
            panelizquierdo.BackColor = System.Drawing.Color.FromArgb(129, 178, 20);          
            Logica.Bases.Multilinea(ref datalistado);
            ControlSetFocus();
        }

        private void CheckInventarios_CheckedChanged_1(object sender, EventArgs e)
        {
            if (TXTIDPRODUCTOOk.Text != "0" & Convert.ToDouble(txtstock2.Text) > 0)
            {
                if (CheckInventarios.Checked == false)
                {
                    MessageBox.Show("Hay Aun En Stock, Dirijete al Modulo Inventarios para Ajustar el Inventario a cero", "Stock Existente", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    PANELINVENTARIO.Visible = true;
                    CheckInventarios.Checked = true;
                }
            }

            if (TXTIDPRODUCTOOk.Text != "0" & Convert.ToDouble(txtstock2.Text) == 0)
            {
                if (CheckInventarios.Checked == false)
                {
                    PANELINVENTARIO.Visible = false;

                }
            }

            if (TXTIDPRODUCTOOk.Text == "0")
            {
                if (CheckInventarios.Checked == false)
                {
                    PANELINVENTARIO.Visible = false;

                }
            }

            if (CheckInventarios.Checked == true)
            {

                PANELINVENTARIO.Visible = true;
            }
        }

        private void TGUARDAR_Click_1(object sender, EventArgs e)
        {
            double txtpreciomayoreoV = Convert.ToDouble(txtpreciomayoreo.Text);

            double txtapartirdeV = Convert.ToDouble(txtapartirde.Text);
            double txtcostoV = Convert.ToDouble(txtcosto.Text);
            double TXTPRECIODEVENTA2V = Convert.ToDouble(TXTPRECIODEVENTA2.Text);
            if (txtpreciomayoreo.Text == "") txtpreciomayoreo.Text = "0";
            if (txtapartirde.Text == "") txtapartirde.Text = "0";
            //TXTPRECIODEVENTA2.Text = TXTPRECIODEVENTA2.Text.Replace(lblmoneda.Text + " ", "");
            //TXTPRECIODEVENTA2.Text = System.String.Format(((decimal)TXTPRECIODEVENTA2.Text), "##0.00");
            if ((txtpreciomayoreoV > 0 & Convert.ToDouble(txtapartirde.Text) > 0) | (txtpreciomayoreoV == 0 & txtapartirdeV == 0))
            {                         
                try
                {                       
                    if (no.Checked == true)
                    {
                         
                        if (txtcodigodebarras.Text != "0" & txtcodigodebarras.Text != "" & no.Checked == true)
                        {
                                
                            PanelImpuesto.Visible = false;                               
                            porcentaje = 0;
                            txtImpuestos.Text = Convert.ToInt32(porcentaje).ToString();
                            Subtotalventa = 0.00;
                            ValorImpuesto5 = 0.00;
                            ValorImpuesto19 = 0.00;
                            sinIVA =Convert.ToDouble(TXTPRECIODEVENTA2.Text);
                            if (txtcostoV >= TXTPRECIODEVENTA2V)
                            {
                                DialogResult result;
                                result = MessageBox.Show("El precio de Venta es menor que el COSTO, Esto Te puede Generar Perdidas", "Producto con Perdidas", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                                if (result == DialogResult.OK)
                                {                                
                                    insertar_productos();
                                }
                                else
                                {
                                    TXTPRECIODEVENTA2.Focus();
                                }

                            }
                            else if (txtcostoV < TXTPRECIODEVENTA2V)
                            {
                                insertar_productos();
                            }
                            txtImpuestos.Text = "";
                            no.Checked = true;
                        }
                    }
                    if (si.Checked == true)
                    {
                        if (txtImpuestos.Text == txtporcentaje.Text)
                        {
                            String total = String.Format("{0},{1}", 1, txtImpuestos.Text);
                            double tot = Convert.ToDouble(venta) * (Convert.ToDouble(total) / 100);
                            double tota = tot - venta;
                            ValorImpuesto19 = tota;
                            ValorImpuesto5 = 0.00;
                            Subtotalventa = venta;
                            this.TXTPRECIODEVENTA2.Text = tot.ToString();

                            String mayo = String.Format("{0},{1}", 1, txtImpuestos.Text);
                            double mayoreo = Convert.ToDouble(txtpreciomayoreo.Text) * (Convert.ToDouble(mayo) / 100);
                            txtpreciomayoreo.Text = mayoreo.ToString();

                        }
                        if (txtImpuestos.Text == porcentajeFinal.ToString())
                        {
                            String total = String.Format("{0},{1}", 0,0+ txtImpuestos.Text);
                            double tot = Convert.ToDouble(venta) * (Convert.ToDouble(total) / 100);
                            double tota = venta + tot;
                            ValorImpuesto5 = tot;
                            ValorImpuesto19 = 0.00;
                            Subtotalventa = venta;
                            this.TXTPRECIODEVENTA2.Text = tota.ToString();
                            sinIVA = 0.00;

                            String mayo = String.Format("{0},{1}", 1, txtImpuestos.Text);
                            double mayoreo = Convert.ToDouble(txtpreciomayoreo.Text) * (Convert.ToDouble(mayo) / 100);
                            txtpreciomayoreo.Text = mayoreo.ToString();

                        }
                        if (string.IsNullOrEmpty(txtImpuestos.Text) | string.IsNullOrEmpty(txtcodigodebarras.Text) )
                        {
                                MessageBox.Show("Revisa si el campo del IVA es igual al del sistema ó que el código no sea Cero", "aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        if(Convert.ToDouble(txtImpuestos.Text) != Convert.ToDouble(txtporcentaje.Text) & Convert.ToDouble(txtImpuestos.Text) != Convert.ToDouble( txtporcentaje2.Text))
                        {
                            MessageBox.Show("Revisa si el campo del IVA es igual al del sistema", "aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            porcentaje = Convert.ToInt32(txtImpuestos.Text);
                            PanelImpuesto.Visible = true;
                            if (Convert.ToInt32(txtImpuestos.Text) == porcentaje & txtcodigodebarras.Text != "0" & txtcodigodebarras.Text != "")
                            {

                                if (txtcostoV >= TXTPRECIODEVENTA2V)
                                {
                                    DialogResult result;
                                    result = MessageBox.Show("El precio de Venta es menor que el COSTO, Esto Te puede Generar Perdidas", "Producto con Perdidas", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                                    if (result == DialogResult.OK)
                                    {
                                        insertar_productos();

                                    }
                                    else
                                    {
                                        TXTPRECIODEVENTA2.Focus();
                                    }

                                }
                                else if (txtcostoV < TXTPRECIODEVENTA2V)
                                {
                                    insertar_productos();
                                }
                                txtImpuestos.Text = "";
                                no.Checked = true;
                            }                   
                        }                      
                    }
                }
                catch (Exception ex)
                {

                }
               
            }
            else if (txtpreciomayoreoV != 0 | txtapartirdeV != 0)
            {
                MessageBox.Show("Estas configurando Precio mayoreo, debes completar los campos de Precio mayoreo y A partir de, si no deseas configurarlo dejalos en blanco", "Datos incompletos", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            }         
        }

        private void TGUARDARCAMBIOS_Click_1(object sender, EventArgs e)
        {
            try
            {
                double txtpreciomayoreoV = Convert.ToDouble(txtpreciomayoreo.Text);

                double txtapartirdeV = Convert.ToDouble(txtapartirde.Text);
                double txtcostoV = Convert.ToDouble(txtcosto.Text);
                double TXTPRECIODEVENTA2V = Convert.ToDouble(TXTPRECIODEVENTA2.Text);
                if (txtpreciomayoreo.Text == "") txtpreciomayoreo.Text = "0";
                if (txtapartirde.Text == "") txtapartirde.Text = "0";


                //TXTPRECIODEVENTA2.Text = TXTPRECIODEVENTA2.Text.Replace(lblmoneda.Text + " ", "");
                //TXTPRECIODEVENTA2.Text = System.String.Format(((decimal)TXTPRECIODEVENTA2.Text), "##0.00");
                if ((txtpreciomayoreoV > 0 & Convert.ToDouble(txtapartirde.Text) > 0) | (txtpreciomayoreoV == 0 & txtapartirdeV == 0))
                {

                    try
                    {

                        if (no.Checked == true)
                        {

                            if (txtcodigodebarras.Text != "0" & txtcodigodebarras.Text != "" & no.Checked == true)
                            {
                             
                                PanelImpuesto.Visible = false;
                                porcentaje = 0;
                                txtImpuestos.Text = Convert.ToInt32(porcentaje).ToString();

                                Subtotalventa = 0.00;
                                ValorImpuesto5 = 0.00;
                                ValorImpuesto19 = 0.00;
                                sinIVA = Convert.ToDouble(TXTPRECIODEVENTA2.Text);
                                if (txtcostoV >= TXTPRECIODEVENTA2V)
                                {

                                    DialogResult result;
                                    result = MessageBox.Show("El precio de Venta es menor que el COSTO, Esto Te puede Generar Perdidas", "Producto con Perdidas", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                                    if (result == DialogResult.OK)
                                    {
                                        editar_productos();
                                    }
                                    else
                                    {
                                        TXTPRECIODEVENTA2.Focus();
                                    }
                                }
                                else if (txtcostoV < TXTPRECIODEVENTA2V)
                                {
                                    editar_productos();
                                }
                                txtImpuestos.Text = "";
                                no.Checked = true;
                            }
                        }
                        if (si.Checked == true)
                        {
                            if (txtImpuestos.Text == txtporcentaje.Text)
                            {
                                String total = String.Format("{0},{1}", 1, txtImpuestos.Text);
                                double tot = Convert.ToDouble(venta) * (Convert.ToDouble(total) / 100);                              
                                double tota =  tot - venta;
                                ValorImpuesto19 = tota;
                                ValorImpuesto5 = 0.00;
                                Subtotalventa = venta;
                                this.TXTPRECIODEVENTA2.Text = tot.ToString();
                                sinIVA = 0.00;

                                String mayo = String.Format("{0},{1}", 1, txtImpuestos.Text);
                                double mayoreo = Convert.ToDouble(txtpreciomayoreo.Text) * (Convert.ToDouble(mayo) / 100);
                                txtpreciomayoreo.Text = mayoreo.ToString();
                            }
                            if (txtImpuestos.Text == porcentajeFinal.ToString())
                            {
                                String total = String.Format("{0},{1}", 0, 0 + txtImpuestos.Text);
                                double tot = Convert.ToDouble(venta) * (Convert.ToDouble(total) / 100);
                                double tota = venta + tot;
                                ValorImpuesto5 = tot;
                                ValorImpuesto19 = 0.00;
                                Subtotalventa = venta;
                                this.TXTPRECIODEVENTA2.Text = tota.ToString();

                                String mayo = String.Format("{0},{1}", 1, txtImpuestos.Text);
                                double mayoreo = Convert.ToDouble(txtpreciomayoreo.Text) * (Convert.ToDouble(mayo) / 100);
                                txtpreciomayoreo.Text = mayoreo.ToString();
                            }
                            if (string.IsNullOrEmpty(txtImpuestos.Text) | string.IsNullOrEmpty(txtcodigodebarras.Text))
                            {
                                MessageBox.Show("Revisa si el campo del IVA es igual al del sistema ó que el código no sea Cero", "aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            if (Convert.ToDouble(txtImpuestos.Text) != Convert.ToDouble(txtporcentaje.Text) & Convert.ToDouble(txtImpuestos.Text) != Convert.ToDouble(txtporcentaje2.Text))
                            {
                                MessageBox.Show("Revisa si el campo del IVA es igual al del sistema", "aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                porcentaje = Convert.ToInt32(txtImpuestos.Text);
                                PanelImpuesto.Visible = true;
                                if (Convert.ToInt32(txtImpuestos.Text) == porcentaje & txtcodigodebarras.Text != "0" & txtcodigodebarras.Text != "")
                                {
                                   

                                    if (txtcostoV >= TXTPRECIODEVENTA2V)
                                    {

                                        DialogResult result;
                                        result = MessageBox.Show("El precio de Venta es menor que el COSTO, Esto Te puede Generar Perdidas", "Producto con Perdidas", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                                        if (result == DialogResult.OK)
                                        {
                                            editar_productos();
                                            panederecho.BackColor = System.Drawing.Color.FromArgb(129, 178, 20);
                                            panelizquierdo.BackColor = System.Drawing.Color.FromArgb(129, 178, 20);
                                        }
                                        else
                                        {
                                            TXTPRECIODEVENTA2.Focus();
                                        }
                                    }
                                    else if (txtcostoV < TXTPRECIODEVENTA2V)
                                    {
                                        editar_productos();
                                     
                                    }
                                    txtImpuestos.Text = "";
                                    no.Checked = true;

                                }
                            }                                                     
                        }
                    }
                    catch (Exception ex)
                    {

                    }                 
                }
                else
                {
                    MessageBox.Show("El precio de Mayoreo y Apartir de son incorrecto, si no vas a llenar esos campos dejalos en CERO", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {

            }
        
        }

        private void txtstock2_MouseClick_1(object sender, MouseEventArgs e)
        {
            try
            {
                if (TXTIDPRODUCTOOk.Text != "0")
                {
                    Tmensajes.SetToolTip(txtstock2, "Para modificar el Stock Hazlo desde el Modulo de Inventarios");
                    Tmensajes.ToolTipTitle = "Accion denegada";
                    Tmensajes.ToolTipIcon = ToolTipIcon.Info;

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnGenerarCodigo_Click_1(object sender, EventArgs e)
        {
            GENERAR_CODIGO_DE_BARRAS_AUTOMATICO();
        
        }   

        private void ToolStripMenuItem15_Click(object sender, EventArgs e)
        {
            exportar();
        }
        public void exportar()
        {
            Presentacion.Productos.Asistente_de_importacionExcel frm = new Presentacion.Productos.Asistente_de_importacionExcel();
            frm.ShowDialog();
        }

        //private void Productoss_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    Dispose();
        //    Configuracion.PANEL_CONFIGURACIONES frm = new Configuracion.PANEL_CONFIGURACIONES();
        //    frm.ShowDialog();
        //}
        public void impuesto()
        {

           
               
        }

        private void cbImpuestos_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void si_Click(object sender, EventArgs e)
        {
            label22.Visible = true;
            PanelImpuesto.Visible = true;
   
           
        }

        private void no_Click(object sender, EventArgs e)
        {
            label22.Visible = false;
            PanelImpuesto.Visible = false;
         
        }
        private void Exportar(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                exportar();
            }
           
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            Exportar(e);
        }
        public void ControlSetFocus()        {
          txtbusca.Focus();           
          button1.Focus();  
          
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            if(txtRuta.Text == "")
            {
               MessageBox.Show("Por favor escoge la ruta para guardar los códigos de barra", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                QR();
            }
         
        }      
        public void QR()
        {
            try
            {      
              

                PdfWriter pdfWriter = new PdfWriter(txtRuta.Text + "\\" + "Codigo_de_barra.pdf");
                PdfDocument pdf = new PdfDocument(pdfWriter);
                Document documento = new Document(pdf, PageSize.LETTER);
                documento.SetMargins(20, 20, 20, 20);
              
                Barcode Codigo = new Barcode();
                Codigo.IncludeLabel = true;

                ConexionData.abrir();

                SqlCommand da = new SqlCommand("SELECT  Codigo, Descripcion FROM Producto1", ConexionData.conectar);
                SqlDataReader reader = da.ExecuteReader();


                while (reader.Read())
                {
       

                    string codigo = (reader["Codigo"].ToString());
                    string descripcion = (reader["Descripcion"].ToString());

                    Codigo.Alignment = AlignmentPositions.CENTER;
                    Codigo.LabelFont = new Font(FontFamily.GenericMonospace, 8, FontStyle.Regular);
                    Codigo.Encode(TYPE.CODE128, codigo, Color.Black, Color.White, 200, 50);                 

                    Codigo.SaveImage(txtRuta.Text + "\\" + codigo + " - " + descripcion + ".png", SaveTypes.JPG);

                    var imagen = new iText.Layout.Element.Image(ImageDataFactory.Create(txtRuta.Text + "\\" + codigo +" - " + descripcion + ".png"));
                    var parrafo = new Paragraph().Add(imagen);
                    documento.Add(parrafo);

                }
                documento.Close();
                MessageBox.Show("Códigos de barra generado", "Mensaje", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {              
                MessageBox.Show("No se completo el proceso", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            ConexionData.cerrar();


        }  
        private void ObtenerRuta()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void txtRuta_TextChanged(object sender, EventArgs e)
        {
            ObtenerRuta();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ObtenerRuta();
        }

        private void txtRuta_Click(object sender, EventArgs e)
        {
            ObtenerRuta();
        }
    }
}
