using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using Telerik.Reporting.Processing;
using Punto_de_venta.Logica;
using Punto_de_venta.Datos;

namespace Punto_de_venta.Presentacion.Ventas_Menu_Principal
{
    public partial class MEDIOS_DE_PAGO : Form
    {
        public MEDIOS_DE_PAGO()
        {
            InitializeComponent();
        }
        private PrintDocument DOCUMENTO;
        string moneda;
        int idcliente;
        int idventa;
        String total;
        double valorIVA19;
        double valorIVA5;
        double SINIVA;
        double vuelto = 0;
        double efectivo_calculado = 0;
        double restante = 0;
        int INDICADOR_DE_FOCO;
        bool SECUENCIA1 = true;
        bool SECUENCIA2 = true;
        bool SECUENCIA3 = true;
        string indicador;
        string indicador_de_tipo_de_pago_string;
        string txttipo;
        string TXTTOTAL_STRING;
        string lblproceso;
        double credito = 0;
        int idcomprobante;
        string lblSerialPC;
        int iva19;
        int iva5;
        double base5;
        double base19;
        string cot;
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MEDIOS_DE_PAGO_Load(object sender, EventArgs e)
        {
        
            numerlent = txtnumeroconvertidoenletra.Text;
            cot = Ventas_Menu_Princi.activado;
            if (cot == "ACTIVADO")
            {
                this.Size = new System.Drawing.Size(428, 181);
                this.StartPosition = FormStartPosition.CenterScreen;
                btncerrar.Visible = true;
                TGuardarSinImprimir.Visible = false;

            }
            if(cot =="DESACTIVADO")
            {
                this.Size = new System.Drawing.Size(1054, 592);
                //this.FormBorderStyle = FormBorderStyle.None;
                this.StartPosition = FormStartPosition.CenterScreen;                         
                TGuardarSinImprimir.Visible = true;
            }

            cambiar_el_formato_de_separador_de_decimales();
            MOSTRAR_comprobante_serializado_POR_DEFECTO();
            validar_tipos_de_comprobantes();
            obtener_serial_pc();
            mostrar_moneda_de_empresa();
            configuraciones_de_diseño();
            Obtener_id_de_venta();
            mostrar_impresora();
            cargar_impresoras_del_equipo();

            if (cot != "ACTIVADO")
            {
                calcular_restante();
            }

           
        }
        private void MOSTRAR_comprobante_serializado_POR_DEFECTO()
        {
            SqlCommand cmd = new SqlCommand("select tipodoc from Serializacion Where Por_defecto='SI'", ConexionDt.ConexionData.conectar);
            try
            {
                ConexionDt.ConexionData.abrir();
                lblComprobante.Text = Convert.ToString(cmd.ExecuteScalar());
                ConexionDt.ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dibujarCOMPROBANTES();
        }
        private void dibujarCOMPROBANTES()
        {
            FlowLayoutPanel3.Controls.Clear();
            try
            {
                ConexionDt.ConexionData.abrir();
                string query = "select tipodoc from Serializacion where Destino='VENTAS'";
                SqlCommand cmd = new SqlCommand(query, ConexionDt.ConexionData.conectar);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Button b = new Button();
                    b.Text = rdr["tipodoc"].ToString();
                    b.Size = new System.Drawing.Size(191, 60);
                    b.BackColor = Color.FromArgb(70, 70, 71);
                    b.Font = new System.Drawing.Font("Segoe UI", 13);
                    b.FlatStyle = FlatStyle.Flat;
                    b.ForeColor = Color.WhiteSmoke;
                    FlowLayoutPanel3.Controls.Add(b);
                    if (b.Text == lblComprobante.Text)
                    {
                        b.Visible = false;
                    }
                    b.Click += miEvento;
                }
                ConexionDt.ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        private void miEvento(System.Object sender, EventArgs e)
        {
            lblComprobante.Text = ((Button)sender).Text;
            dibujarCOMPROBANTES();
            validar_tipos_de_comprobantes();
            identificar_el_tipo_de_pago();
            if (lblComprobante.Text == "FACTURA" && txttipo == "CREDITO")
            {
                PANEL_CLIENTE_FACTURA.Visible = false;
            }
            if (lblComprobante.Text == "FACTURA" && txttipo == "EFECTIVO")
            {
                PANEL_CLIENTE_FACTURA.Visible = true;
                lblindicador_de_factura_1.Text = "Cliente: (Obligatorio)";
                lblindicador_de_factura_1.ForeColor = Color.FromArgb(255, 192, 192);

            }
            else if (lblComprobante.Text != "FACTURA" && txttipo == "EFECTIVO")
            {
                PANEL_CLIENTE_FACTURA.Visible = true;
                lblindicador_de_factura_1.Text = "Cliente: (Opcional)";
                lblindicador_de_factura_1.ForeColor = Color.DimGray;

            }

            if (lblComprobante.Text == "FACTURA" && txttipo == "TARJETA")
            {
                PANEL_CLIENTE_FACTURA.Visible = true;
                lblindicador_de_factura_1.Text = "Cliente: (Obligatorio)";
                lblindicador_de_factura_1.ForeColor = Color.FromArgb(255, 192, 192);

            }
                else if (lblComprobante.Text != "FACTURA" && txttipo == "TARJETA")
                {
                    PANEL_CLIENTE_FACTURA.Visible = true;
                    lblindicador_de_factura_1.Text = "Cliente: (Opcional)";
                    lblindicador_de_factura_1.ForeColor = Color.DimGray;
                }


            }
        void validar_tipos_de_comprobantes()
        {
            buscar_Tipo_de_documentos_para_insertar_en_ventas();
            try
            {
                int numerofin;

                txtserie.Text = dtComprobantes.SelectedCells[2].Value.ToString();

                numerofin = Convert.ToInt32(dtComprobantes.SelectedCells[4].Value);
                idcomprobante = Convert.ToInt32(dtComprobantes.SelectedCells[5].Value);
                txtnumerofin.Text = Convert.ToString(numerofin + 1);
                lblCantidad_de_numeros.Text = dtComprobantes.SelectedCells[3].Value.ToString();
                lblCorrelativoconCeros.Text = ConexionDt.Agregar_ceros_adelante_De_numero.ceros(txtnumerofin.Text, Convert.ToInt32(lblCantidad_de_numeros.Text));
            }
            catch (Exception ex)
            {

            }
        }
        void buscar_Tipo_de_documentos_para_insertar_en_ventas()
        {
            DataTable dt = new DataTable();
            try
            {
                ConexionDt.ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscar_Tipo_de_documentos_para_insertar_en_ventas", ConexionDt.ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", lblComprobante.Text);
                da.Fill(dt);
                dtComprobantes.DataSource = dt;
                ConexionDt.ConexionData.cerrar();
            }
            catch (Exception ex)
            {
            }
        }
        void identificar_el_tipo_de_pago()
        {
            int indicadorEfectivo = 4;
            int indicadorCredito = 2;
            //int indicadorTarjeta = 3;

            // validacion para evitar valores vacios
            if (txtefectivo2.Text == "")
            {
                txtefectivo2.Text = "0";
            }           
            if (txtcredito2.Text == "")
            {
                txtcredito2.Text = "0";
            }
            //validacion de .
            if (txtefectivo2.Text == ".")
            {
                txtefectivo2.Text = "0";
            }          
            if (txtcredito2.Text == ".")
            {
                txtcredito2.Text = "0";
            }
            //validacion de 0
            if (txtefectivo2.Text == "0")
            {
                indicadorEfectivo = 0;
            }          
            if (txtcredito2.Text == "0")
            {
                indicadorCredito = 0;
            }
            //calculo de indicador
            int calculo_identificacion = indicadorCredito + indicadorEfectivo /*+ indicadorTarjeta*/;
            //consulta al identificador
            if (calculo_identificacion == 4)
            {
                indicador_de_tipo_de_pago_string = "EFECTIVO";
            }
            if (calculo_identificacion == 2)
            {
                indicador_de_tipo_de_pago_string = "CREDITO";
            }
            //if (calculo_identificacion == 3)
            //{
            //    indicador_de_tipo_de_pago_string = "TARJETA";
            //}
            if (calculo_identificacion > 4)
            {
                indicador_de_tipo_de_pago_string = "MIXTO";
            }
            txttipo = indicador_de_tipo_de_pago_string;

        }
        public void cambiar_el_formato_de_separador_de_decimales()
        {
            ConexionDt.cambiar_el_formato_de_separador_de_decimales.cambiar();
        }
        public void obtener_serial_pc()
        {
            Logica.Bases.Obtener_serialPC(ref lblSerialPC);
        }
        void mostrar_moneda_de_empresa()
        {
            SqlCommand cmd = new SqlCommand("Select Moneda From Empresa", ConexionDt.ConexionData.conectar);
            try
            {
                ConexionDt.ConexionData.abrir();
                moneda = Convert.ToString(cmd.ExecuteScalar());
                ConexionDt.ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        void configuraciones_de_diseño()
        {
            TXTVUELTO.Text = "0.0";
            txtrestante.Text = "0.0";
            TXTTOTAL.Text = moneda + " " + Ventas_Menu_Princi.total;
            txtefectivo2.Text = Ventas_Menu_Princi.total.ToString();
            valorIVA19 = Ventas_Menu_Princi.valorTotalImportiva19;
            valorIVA5 = Ventas_Menu_Princi.valorTotalImportiva5;
            iva19 = Ventas_Menu_Princi.IVA19;
            iva5 = Ventas_Menu_Princi.IVA5;
            SINIVA = Ventas_Menu_Princi.SinIVA;
            base5 = Ventas_Menu_Princi.Basesin5;
            base19 = Ventas_Menu_Princi.Basesin19;
            total = txtefectivo2.Text;
            idcliente = 0;

        }
        void Obtener_id_de_venta()
        {
            idventa = Ventas_Menu_Princi.idVenta;
        }
        void mostrar_impresora()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("mostrar_impresoras_por_caja", ConexionDt.ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Serial", lblSerialPC);
                try
                {
                    ConexionDt.ConexionData.abrir();
                    txtImpresora.Text = Convert.ToString(cmd.ExecuteScalar());
                    ConexionDt.ConexionData.cerrar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        void cargar_impresoras_del_equipo()
        {
            txtImpresora.Items.Clear();
     
            for (var I = 0; I <PrinterSettings.InstalledPrinters.Count; I++)
            {
                txtImpresora.Items.Add(PrinterSettings.InstalledPrinters[I]);
            }
            txtImpresora.Items.Add("Ninguna");
        }
        void calcular_restante()
        {
            try
            {
                double efectivo = 0;
                double tarjeta = 0;

                if (txtefectivo2.Text == "")
                {
                    efectivo = 0;
                }
                else
                {
                    efectivo = Convert.ToDouble(txtefectivo2.Text);
                }
                if (txtcredito2.Text == "")
                {
                    credito = 0;
                }
                else
                {
                    credito = Convert.ToDouble(txtcredito2.Text);
                }           
             
                if (txtefectivo2.Text == "0.00")
                {
                    efectivo = 0;
                }
                if (txtcredito2.Text == "0.00")
                {
                    credito = 0;
                }            

                if (txtefectivo2.Text == ".")
                {
                    efectivo = 0;
                }
                if (txtcredito2.Text == ".")
                {
                    tarjeta = 0;
                }              
                ///////
                //Total= 5 
                //Efectivo= 10
                // Tarjeta = 22
                //EC=E-(T+TA)
                //EC= 10-(5+22)
                //EC= 3
                //V=E-(T-TA)
                //V=10-(5-2)
                //V=7

                try
                {
                    if (efectivo > Convert.ToDouble(total))
                    {
                        efectivo_calculado = efectivo - (Convert.ToDouble(total) + credito + tarjeta);
                        if (efectivo_calculado < 0)
                        {
                            vuelto = 0;
                            TXTVUELTO.Text = "0";
                            txtrestante.Text = Convert.ToString(efectivo_calculado);
                            restante = efectivo_calculado;
                        }
                        else
                        {
                            vuelto = efectivo - (Convert.ToDouble(total) - credito - tarjeta);
                            TXTVUELTO.Text = Convert.ToString(vuelto);
                            restante = efectivo - (Convert.ToDouble(total) + credito + tarjeta + efectivo_calculado);
                            txtrestante.Text = Convert.ToString(restante);
                            txtrestante.Text = decimal.Parse(txtrestante.Text).ToString("##0.00");
                        }

                    }
                    else
                    {
                        vuelto = 0;
                        TXTVUELTO.Text = "0";
                        efectivo_calculado = efectivo;
                        restante = Convert.ToDouble(total) - efectivo_calculado - credito - tarjeta;
                        txtrestante.Text = Convert.ToString(restante);
                        txtrestante.Text = decimal.Parse(txtrestante.Text).ToString("##0.00");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void txttarjeta2_TextChanged(object sender, EventArgs e)
        {
            calcular_restante();
        }

        private void txtefectivo2_TextChanged(object sender, EventArgs e)
        {
            calcular_restante();
        }

        private void txtcredito2_TextChanged(object sender, EventArgs e)
        {
            calcular_restante();
            hacer_visible_panel_de_clientes_a_credito();
        }
        void hacer_visible_panel_de_clientes_a_credito()
        {
            try
            {
                double textocredito = 0;
                if (txtcredito2.Text == ".")
                {
                    textocredito = 0;
                }
                if (txtcredito2.Text == "")
                {
                    textocredito = 0;
                }
                else
                {
                    textocredito = Convert.ToDouble(txtcredito2.Text);
                }

                if (textocredito > 0)
                {
                    pcredito.Visible = true;
                }
                else
                {
                    pcredito.Visible = false;
                    idcliente = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "1";
            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txtcredito2.Text = txtcredito2.Text + "1";
            }         
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "2";
            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txtcredito2.Text = txtcredito2.Text + "2";
            }         
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "3";
            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txtcredito2.Text = txtcredito2.Text + "3";
            }          
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "4";

            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txtcredito2.Text = txtcredito2.Text + "4";
            }          
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "5";

            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txtcredito2.Text = txtcredito2.Text + "5";
            }          
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "6";

            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txtcredito2.Text = txtcredito2.Text + "6";
            }          
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "7";

            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txtcredito2.Text = txtcredito2.Text + "7";
            }        
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "8";

            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txtcredito2.Text = txtcredito2.Text + "8";
            }        
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "9";

            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txtcredito2.Text = txtcredito2.Text + "9";
            }          
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                txtefectivo2.Text = txtefectivo2.Text + "0";

            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                txtcredito2.Text = txtcredito2.Text + "0";
            }         
        }

        private void btnpunto_Click(object sender, EventArgs e)
        {
            if (INDICADOR_DE_FOCO == 1)
            {
                if (SECUENCIA1 == true)
                {
                    txtefectivo2.Text = txtefectivo2.Text + ".";
                    SECUENCIA1 = false;
                }

                else
                {
                    return;
                }

            }
            else if (INDICADOR_DE_FOCO == 2)
            {
                if (SECUENCIA2 == true)
                {
                    txtcredito2.Text = txtcredito2.Text + ".";
                    SECUENCIA2 = false;
                }

                else
                {
                    return;
                }

            }       
        }

        private void btnborrartodo_Click(object sender, EventArgs e)
        {

        }

        private void txtclientesolicitabnte2_TextChanged(object sender, EventArgs e)
        {
            buscarclientes2();
            datalistadoclientes2.Visible = true;
        }
        void buscarclientes2()
        {
           
            try
            {        

                DataTable dt = new DataTable();
                Obtener_datos.buscar_clientes(ref dt, txtclientesolicitabnte2.Text);
                datalistadoclientes2.DataSource = dt;
                datalistadoclientes2.Columns[1].Visible = false;
                datalistadoclientes2.Columns[3].Visible = false;
                datalistadoclientes2.Columns[4].Visible = false;
                datalistadoclientes2.Columns[5].Visible = false;
                datalistadoclientes2.Columns[2].Width = 420;
                ConexionDt.ConexionData.cerrar();
            }
            catch (Exception ex)
            {

            }
        }

        private void datalistadoclientes2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtclientesolicitabnte2.Text = datalistadoclientes2.SelectedCells[2].Value.ToString();
            idcliente = Convert.ToInt32(datalistadoclientes2.SelectedCells[1].Value.ToString());
            datalistadoclientes2.Visible = false;
        }

        private void ToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            PanelregistroClientes.Visible = true;
            PanelregistroClientes.Dock = DockStyle.Fill;
            PanelregistroClientes.BringToFront();
            limpiar_datos_de_registrodeclientes();
        }
        void limpiar_datos_de_registrodeclientes()
        {
            txtnombrecliente.Clear();
            txtdirecciondefactura.Clear();
            txtcelular.Clear();
            txtrucdefactura.Clear();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtnombrecliente.Text))
            {
                rellenarCamposVacios();
                insertar();
            }
            else
            {
                MessageBox.Show("Ingrese un nombre", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            limpiar();
        
            PanelregistroClientes.Visible = false;
        }
        private void limpiar()
        {
            txtnombrecliente.Clear();
            txtrucdefactura.Clear();
            txtcelular.Clear();
            txtdirecciondefactura.Clear();
            txtnombrecliente.Focus();
          
        }
        private void rellenarCamposVacios()
        {
            if (string.IsNullOrEmpty(txtcelular.Text)) { txtcelular.Text = "-"; };
            if (string.IsNullOrEmpty(txtdirecciondefactura.Text)) { txtdirecciondefactura.Text = "-"; };
            if (string.IsNullOrEmpty(txtrucdefactura.Text)) { txtrucdefactura.Text = "-"; };

        }
        private void insertar()
        {
            Lclientes parametros = new Lclientes();
            Insertar_datos funcion = new Insertar_datos();
            parametros.Nombre = txtnombrecliente.Text;
            parametros.IdentificadorFiscal = txtrucdefactura.Text;
            parametros.Celular = txtcelular.Text;
            parametros.Direccion = txtdirecciondefactura.Text;
            if (funcion.insertar_clientes(parametros) == true)
            {
              
            }

        }


        private void BtnVolver_Click(object sender, EventArgs e)
        {
            PanelregistroClientes.Visible = false;
        }

        private void txtefectivo2_Click(object sender, EventArgs e)
        {
            calcular_restante();
            INDICADOR_DE_FOCO = 1;
            if (txtrestante.Text == "0.00")
            {
                txtefectivo2.Text = "";
            }
            else
            {
                txtefectivo2.Text = txtrestante.Text;
            }
        }
   

        private void txtcredito2_Click(object sender, EventArgs e)
        {
            calcular_restante();
            INDICADOR_DE_FOCO = 3;
            if (txtrestante.Text == "0.00")
            {
                txtcredito2.Text = "";
            }
            else
            {
                txtcredito2.Text = txtrestante.Text;
                hacer_visible_panel_de_clientes_a_credito();
            }
        }

       
        void INGRESAR_LOS_DATOS()
        {
            CONVERTIR_TOTAL_A_LETRAS();
            if (cot != "ACTIVADO")
            {
                completar_con_ceros_los_texbox_de_otros_medios_de_pago();
            }
            if (cot == "ACTIVADO")
            {
                txttipo = "MIXTO";
            }


            if (txttipo == "EFECTIVO" && vuelto >= 0)
            {
                vender_en_efectivo();

            }
            else if (txttipo == "EFECTIVO" && vuelto < 0)
            {
                MessageBox.Show("El vuelto no puede ser menor a el Total pagado ", "Datos Incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            // condicional para creditos
            if (txttipo == "CREDITO" && datalistadoclientes2.Visible == false)
            {
                vender_en_efectivo();
            }
            else if (txttipo == "CREDITO" && datalistadoclientes2.Visible == true)
            {
                MessageBox.Show("Seleccione un Cliente para Activar Pago a Credito", "Datos Incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }      

            if (txttipo == "MIXTO")
            {
                if (cot == "ACTIVADO")
                {
                    txttipo = "COTIZACIÓN";
                }

                vender_en_efectivo();
            }                   

        }
        void MOSTRAR_cliente_standar()
        {
            SqlCommand com = new SqlCommand("select idclientev from clientes where Estado = 0", ConexionDt.ConexionData.conectar);
            try
            {
                ConexionDt.ConexionData.abrir();
                idcliente = Convert.ToInt32(com.ExecuteScalar());
                ConexionDt.ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        void vender_en_efectivo()
        {
            if (idcliente == 0)
            {
                MOSTRAR_cliente_standar();
            }
            if (lblComprobante.Text == "FACTURA" && idcliente == 0 && txttipo != "CREDITO")
            {
                MessageBox.Show("Seleccione un Cliente, para Facturas es Obligatorio", "Datos Incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (lblComprobante.Text == "FACTURA" && idcliente != 0)
            {
                procesar_venta_efectivo();
            }

            else if (lblComprobante.Text != "FACTURA" && txttipo != "CREDITO")
            {
                procesar_venta_efectivo();
            }
            else if (lblComprobante.Text != "FACTURA" && txttipo == "CREDITO")
            {
                procesar_venta_efectivo();
            }
        }
        void CONVERTIR_TOTAL_A_LETRAS()
        {
            try
            {
                TXTTOTAL.Text = Convert.ToString(total);
                TXTTOTAL.Text = decimal.Parse(TXTTOTAL.Text).ToString("##0.00");
                int numero = Convert.ToInt32(Math.Floor(Convert.ToDouble(total)));
                TXTTOTAL_STRING = ConexionDt.total_en_letras.Num2Text(numero);
                string[] a = TXTTOTAL.Text.Split('.');
                txttotaldecimal.Text = a[1];
                txtnumeroconvertidoenletra.Text = TXTTOTAL_STRING + " CON " + txttotaldecimal.Text + "/100 ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        void completar_con_ceros_los_texbox_de_otros_medios_de_pago()
        {
            if (txtefectivo2.Text == "")
            {
                txtefectivo2.Text = "0";
            }
            if (txtcredito2.Text == "")
            {
                txtcredito2.Text = "0";
            }          
            if (TXTVUELTO.Text == "")
            {
                TXTVUELTO.Text = "0";
            }
        }
        void procesar_venta_efectivo()
        {
            actualizar_serie_mas_uno();
            validar_tipos_de_comprobantes();
           
            if(cot == "ACTIVADO")
            {
                CONFIRMAR_VENTA_COTIZA();           
            }
            else
            {
                CONFIRMAR_VENTA_EFECTIVO();
            }

            if (lblproceso == "PROCEDE" & cot == "ACTIVADO")
            {             
                if (cot == "ACTIVADO")
                {
                  
                    imprimir_directo();
                }
                else
                {
                   
                    validar_tipo_de_impresion();
                }
          
            }
            else if(lblproceso == "PROCEDE")
            {
                disminuir_stock_productos();
                INSERTAR_KARDEX_SALIDA();
                aumentar_monto_a_cliente();
                if (cot == "ACTIVADO")
                {

                    imprimir_directo();
                }
                else
                {

                    validar_tipo_de_impresion();
                }
            }
        }
        void actualizar_serie_mas_uno()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                ConexionDt.ConexionData.abrir();
                cmd = new SqlCommand("actualizar_serializacion_mas_uno", ConexionDt.ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idserie", idcomprobante);
                cmd.ExecuteNonQuery();
                ConexionDt.ConexionData.cerrar();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void CONFIRMAR_VENTA_EFECTIVO()
        {
            try
                {
                    ConexionDt.ConexionData.abrir();
                    SqlCommand cmd = new SqlCommand("Confirmar_venta", ConexionDt.ConexionData.conectar);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idventa", idventa);
                    cmd.Parameters.AddWithValue("@montototal",Convert.ToDouble(total));
                    cmd.Parameters.AddWithValue("@Saldo", vuelto);
                    cmd.Parameters.AddWithValue("@Tipo_de_pago", txttipo);
                    cmd.Parameters.AddWithValue("@Estado", "CONFIRMADO");
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.Parameters.AddWithValue("@Comprobante", lblComprobante.Text);
                    cmd.Parameters.AddWithValue("@Numero_de_doc", (txtserie.Text + "-" + lblCorrelativoconCeros.Text));
                    cmd.Parameters.AddWithValue("@fecha_venta", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ACCION", "VENTA");
                    cmd.Parameters.AddWithValue("@Fecha_de_pago", txtfecha_de_pago.Value);
                    cmd.Parameters.AddWithValue("@Pago_con", txtefectivo2.Text);
                    cmd.Parameters.AddWithValue("@ValorIVA5", valorIVA5);
                    cmd.Parameters.AddWithValue("@ValorIVA19", valorIVA19);
                    cmd.Parameters.AddWithValue("@Porcentaje_IVA_5", iva5);
                    cmd.Parameters.AddWithValue("@Porcentaje_IVA_19", iva19);
                    cmd.Parameters.AddWithValue("@Sin_IVA", SINIVA);
                    cmd.Parameters.AddWithValue("@Base5", base5);
                    cmd.Parameters.AddWithValue("@Base19", base19);
                    cmd.Parameters.AddWithValue("@Referencia_tarjeta", "NULO");
                    cmd.Parameters.AddWithValue("@Vuelto", TXTVUELTO.Text);
                    cmd.Parameters.AddWithValue("@Efectivo", total);
                    cmd.Parameters.AddWithValue("@Credito", txtcredito2.Text);
                    cmd.Parameters.AddWithValue("@Tarjeta", 0);
                    cmd.ExecuteNonQuery();
                    ConexionDt.ConexionData.cerrar();
                    lblproceso = "PROCEDE";
                }
                catch (Exception ex)
                {
                    ConexionDt.ConexionData.cerrar();
                    lblproceso = "NO PROCEDE";
                    MessageBox.Show(ex.Message);
                }
            
          
        }

        public static int ranNum;
        void CONFIRMAR_VENTA_COTIZA()
        {
            Random myObject = new Random();
            ranNum = myObject.Next(1000, 1000000000);

            Random idi = new Random();
            int id = idi.Next(1, 1000000000);
            try
            {
                ConexionDt.ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("Confirmar_ventass", ConexionDt.ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idventa", idventa);
                cmd.Parameters.AddWithValue("@montototal", Convert.ToDouble(total));
                cmd.Parameters.AddWithValue("@Saldo", 0);
                cmd.Parameters.AddWithValue("@Tipo_de_pago", 0);
                cmd.Parameters.AddWithValue("@Estado", "CONFIRMADO");
            
                cmd.Parameters.AddWithValue("@Comprobante", ranNum);
                cmd.Parameters.AddWithValue("@Numero_de_doc", 0);
                cmd.Parameters.AddWithValue("@fecha_venta", DateTime.Now);
                cmd.Parameters.AddWithValue("@ACCION", "COTIZACIÓN");
                cmd.Parameters.AddWithValue("@Fecha_de_pago", 0);
                cmd.Parameters.AddWithValue("@Pago_con", 0);
                cmd.Parameters.AddWithValue("@ValorIVA5", 0);
                cmd.Parameters.AddWithValue("@ValorIVA19", 0);
                cmd.Parameters.AddWithValue("@Porcentaje_IVA_5", 0);
                cmd.Parameters.AddWithValue("@Porcentaje_IVA_19", 0);
                cmd.Parameters.AddWithValue("@Sin_IVA", 0);
                cmd.Parameters.AddWithValue("@Base5", 0);
                cmd.Parameters.AddWithValue("@Base19", 0);
                cmd.Parameters.AddWithValue("@Referencia_tarjeta", "NULO");
                cmd.Parameters.AddWithValue("@Vuelto", 0);
                cmd.Parameters.AddWithValue("@Efectivo", 0);
                cmd.Parameters.AddWithValue("@Credito", 0);
                cmd.Parameters.AddWithValue("@Tarjeta", 0);
                cmd.ExecuteNonQuery();
                ConexionDt.ConexionData.cerrar();
                lblproceso = "PROCEDE";
            }
            catch (Exception ex)
            {
                ConexionDt.ConexionData.cerrar();
                lblproceso = "PROCEDE";
                //MessageBox.Show(ex.Message);
            }


        }
        void disminuir_stock_productos()
        {
            mostrar_productos_agregados_a_venta();
            foreach (DataGridViewRow row in datalistadoDetalleVenta.Rows)
            {
                int idproducto = Convert.ToInt32(row.Cells["Id_producto"].Value);
                double cantidad = Convert.ToInt32(row.Cells["Cant"].Value);
                try
                {            
                    ConexionDt.ConexionData.abrir();
                    SqlCommand cmd = new SqlCommand("disminuir_stock", ConexionDt.ConexionData.conectar);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idproducto", idproducto);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.ExecuteNonQuery();
                    ConexionDt.ConexionData.cerrar();
                }
                catch (Exception ex)
                {
                    ConexionDt.ConexionData.cerrar();
                    MessageBox.Show(ex.Message);
                }
            }


        }
        void INSERTAR_KARDEX_SALIDA()
        {
            try
            {
                foreach (DataGridViewRow row in datalistadoDetalleVenta.Rows)
                {
                    int Id_producto = Convert.ToInt32(row.Cells["Id_producto"].Value);
                    double cantidad = Convert.ToDouble(row.Cells["Cant"].Value);
                    string STOCK = Convert.ToString(row.Cells["Stock"].Value);
                    if (STOCK != "Ilimitado")
                    {
                        ConexionDt.ConexionData.abrir();
                        SqlCommand cmd = new SqlCommand("insertar_KARDEX_SALIDA", ConexionDt.ConexionData.conectar);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Fecha", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Motivo", "Venta #" + lblComprobante.Text + " " + lblCorrelativoconCeros.Text);
                        cmd.Parameters.AddWithValue("@Cantidad ", cantidad);
                        cmd.Parameters.AddWithValue("@Id_producto", Id_producto);
                        cmd.Parameters.AddWithValue("@Id_usuario", Ventas_Menu_Princi.idusuario_que_inicio_sesion);
                        cmd.Parameters.AddWithValue("@Tipo", "SALIDA");
                        cmd.Parameters.AddWithValue("@Estado", "DESPACHO CONFIRMADO");
                        cmd.Parameters.AddWithValue("@Id_caja", Ventas_Menu_Princi.Id_caja);
                        cmd.ExecuteNonQuery();
                        ConexionDt.ConexionData.cerrar();

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.StackTrace);
            }
        }
        void aumentar_monto_a_cliente()
        {
            if (credito > 0)
            {
                try
                {
                    ConexionDt.ConexionData.abrir();
                    SqlCommand cmd = new SqlCommand("aumentar_saldo_a_cliente", ConexionDt.ConexionData.conectar);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Saldo", txtcredito2.Text);
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.ExecuteNonQuery();
                    ConexionDt.ConexionData.cerrar();

                }
                catch (Exception ex)
                {
                    ConexionDt.ConexionData.cerrar();
                    MessageBox.Show(ex.StackTrace);
                }
            }

        }
        void validar_tipo_de_impresion()
        {
            if (indicador == "VISTA PREVIA")
            {
                mostrar_ticket_impreso_VISTA_PREVIA();
            }
            else if (indicador == "DIRECTO")
            {
                imprimir_directo();
            
            }
        }
        public static string numerlent;
        void mostrar_ticket_impreso_VISTA_PREVIA()
         {
            PanelImpresionvistaprevia.Visible = true;
            PanelImpresionvistaprevia.Dock = DockStyle.Fill;
            panelGuardado_de_datos.Dock = DockStyle.None;
            panelGuardado_de_datos.Visible = false;

        
                Presentacion.Reportes.Impresion_de_comprobantes.Ticket_report rpt = new Presentacion.Reportes.Impresion_de_comprobantes.Ticket_report();
                DataTable dt = new DataTable();
                try
                {
                    ConexionDt.ConexionData.abrir();
                    SqlDataAdapter da = new SqlDataAdapter("mostrar_ticket_impreso", ConexionDt.ConexionData.conectar);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Id_venta", idventa);
                    da.SelectCommand.Parameters.AddWithValue("@total_en_letras", txtnumeroconvertidoenletra.Text);
                    da.Fill(dt);
                    rpt = new Presentacion.Reportes.Impresion_de_comprobantes.Ticket_report();
                    rpt.table1.DataSource = dt;
                    rpt.DataSource = dt;
                    reportViewer1.Report = rpt;
                    reportViewer1.RefreshReport();
                    ConexionDt.ConexionData.cerrar();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }            
   

        }
        void imprimir_directo()
        {
            mostrar_Ticket_lleno();
            try
            {
                DOCUMENTO = new PrintDocument();
                DOCUMENTO.PrinterSettings.PrinterName = txtImpresora.Text;
                if (DOCUMENTO.PrinterSettings.IsValid)
                {
                    PrinterSettings printerSettings = new PrinterSettings();
                    printerSettings.PrinterName = txtImpresora.Text;
                    ReportProcessor reportProcessor = new ReportProcessor();
                    if (cot == "ACTIVADO")
                    {
                        reportProcessor.PrintReport(reportViewer3.ReportSource, printerSettings);
                    }
                    else
                    {
                        reportProcessor.PrintReport(reportViewer2.ReportSource, printerSettings);
                    }
               
                }
                Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        void mostrar_Ticket_lleno()
        {
            if (cot == "ACTIVADO")
            {
                mostrar();
                reportViewer3.Visible = true;
                reportViewer3.Dock = DockStyle.Fill;
            }
            else
            {
                Presentacion.Reportes.Impresion_de_comprobantes.Ticket_report rpt = new Presentacion.Reportes.Impresion_de_comprobantes.Ticket_report();
                DataTable dt = new DataTable();
                try
                {
                    reportViewer2.Visible = true;
                    ConexionDt.ConexionData.abrir();
                    SqlDataAdapter da = new SqlDataAdapter("mostrar_ticket_impreso", ConexionDt.ConexionData.conectar);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Id_venta", idventa);
                    da.SelectCommand.Parameters.AddWithValue("@total_en_letras", txtnumeroconvertidoenletra.Text);
                    da.Fill(dt);
                    rpt = new Presentacion.Reportes.Impresion_de_comprobantes.Ticket_report();
                    rpt.table1.DataSource = dt;
                    rpt.DataSource = dt;
                    reportViewer2.Report = rpt;
                    reportViewer2.RefreshReport();
                    ConexionDt.ConexionData.cerrar();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
            }

           
        }
        private void mostrar()
        {
            Presentacion.Reportes.Reportes_de_Kardex_listo.Reportes_de_Inventario_Todos.ReportCotizacion rpt = new Presentacion.Reportes.Reportes_de_Kardex_listo.Reportes_de_Inventario_Todos.ReportCotizacion();
            DataTable dt = new DataTable();
            try
            {
           


                ConexionDt.ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_ticket_impreso", ConexionDt.ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id_venta", idventa);
                da.SelectCommand.Parameters.AddWithValue("@total_en_letras", txtnumeroconvertidoenletra.Text);
                da.Fill(dt);
                rpt = new Presentacion.Reportes.Reportes_de_Kardex_listo.Reportes_de_Inventario_Todos.ReportCotizacion();
                rpt.DataSource = dt;
                rpt.table1.DataSource = dt;
                reportViewer3.Report = rpt;
                reportViewer3.RefreshReport();
                ConexionDt.ConexionData.cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }
        void mostrar_productos_agregados_a_venta()
        {
            try
            {
                DataTable dt = new DataTable();
                ConexionDt.ConexionData.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_productos_agregados_a_venta", ConexionDt.ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idventa", idventa);
                da.Fill(dt);
                datalistadoDetalleVenta.DataSource = dt;
                ConexionDt.ConexionData.cerrar();

            }
            catch (Exception ex)
            {
                ConexionDt.ConexionData.cerrar();
                MessageBox.Show(ex.Message);
            }
        }
       
        void editar_eleccion_de_impresora()
        {
            try
            {
                ConexionDt.ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("editar_eleccion_impresoras", ConexionDt.ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Impresora_Ticket", txtImpresora.Text);
                cmd.Parameters.AddWithValue("@idcaja", Ventas_Menu_Princi.Id_caja);
                cmd.ExecuteNonQuery();
                ConexionDt.ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void txtclientesolicitabnte3_TextChanged(object sender, EventArgs e)
        {
            buscarclientes3();
            datalistadoclientes3.Visible = true;
        }
        void buscarclientes3()
        {
            try
            {
                DataTable dt = new DataTable();
                Obtener_datos.buscar_clientes(ref dt, txtclientesolicitabnte3.Text);
                datalistadoclientes3.DataSource = dt;
                datalistadoclientes3.Columns[1].Visible = false;
                datalistadoclientes3.Columns[3].Visible = false;
                datalistadoclientes3.Columns[4].Visible = false;
                datalistadoclientes3.Columns[5].Visible = false;
                datalistadoclientes3.Columns[2].Width = 420;
                ConexionDt.ConexionData.cerrar();
            }
            catch (Exception ex)
            {

            }
        }

        private void datalistadoclientes3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtclientesolicitabnte3.Text = datalistadoclientes3.SelectedCells[2].Value.ToString();
            idcliente = Convert.ToInt32(datalistadoclientes3.SelectedCells[1].Value.ToString());
            datalistadoclientes3.Visible = false;
        }

        private void ToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            PanelregistroClientes.Visible = true;
            PanelregistroClientes.Dock = DockStyle.Fill;
            PanelregistroClientes.BringToFront();
            limpiar_datos_de_registrodeclientes();
        }

        private void btnGuardarImprimirdirecto_Click_1(object sender, EventArgs e)
        {
            ProcesoImprimirdirecto();
        }
        private void ProcesoImprimirdirecto()
        {
            if (restante == 0)
            {
                if (txtImpresora.Text != "Ninguna")
                {
                    editar_eleccion_de_impresora();
                    indicador = "DIRECTO";

                    if (cot == "ACTIVADO")
                    {
                        INGRESAR_LOS_DATOS();

                    }
                    else
                    {
                        identificar_el_tipo_de_pago();
                        INGRESAR_LOS_DATOS();
                    }

                }
                else
                {
                    MessageBox.Show("Seleccione una Impresora", "Impresora Inexistente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("El restante debe ser 0", "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void TGuardarSinImprimir_Click_1(object sender, EventArgs e)
        {
            ProcesoVerenpantalla();
         
        }
        private void ProcesoVerenpantalla()
        {
            if (restante == 0)
            {
                indicador = "VISTA PREVIA";
                identificar_el_tipo_de_pago();
                INGRESAR_LOS_DATOS();
            }
            else
            {
                MessageBox.Show("El restante debe ser 0", "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void txtefectivo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtefectivo2, e);
        }    

        private void txtcredito2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtcredito2, e);
        }

        private void txtefectivo2_KeyDown(object sender, KeyEventArgs e)
        {
            EventoCobro(e);
        }
        private void EventoCobro(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcesoImprimirdirecto();
            }
            if (e.KeyCode == Keys.F1)
            {
                ProcesoVerenpantalla();
            }
        }

        private void txtcredito2_KeyDown(object sender, KeyEventArgs e)
        {
            EventoCobro(e);
        }

        private void txtclientesolicitabnte2_KeyDown(object sender, KeyEventArgs e)
        {
            EventoCobro(e);
        }

        private void txtclientesolicitabnte3_KeyDown(object sender, KeyEventArgs e)
        {
            EventoCobro(e);
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
