using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Punto_de_venta.Datos;
using Punto_de_venta.Logica;


namespace Punto_de_venta.Presentacion.Ventas_Menu_Principal
{
    public partial class Ventas_Menu_Princi : Form
    {
        public Ventas_Menu_Princi()
        {
            InitializeComponent();
            BTNTECLADO.Focus();
            BTNTECLADO.Focus();


        }
        int contador_stock_detalle_de_venta;
        int idproducto;
        int idClienteEstandar;
        public static int idusuario_que_inicio_sesion;
        public static int idVenta;
        int iddetalleventa;
        int Contador;
        public static double txtpantalla;
        double ValorIva_5;
        double ValorIva_19;
        double sinIvas;
        double subTotal;
        double lblStock_de_Productos;
        public static double total;
        public static double valorTotalImportiva19;
        public static double valorTotalImportiva5;
        public static double SinIVA;
        public static int Id_caja;
        double cantidad;
        string SerialPC;
        string sevendePor;
        public static string txtventagenerada;
        public static string txtventageneradaS;
        double txtprecio_unitarios;
        string lblusaInventarios;
        string Tema;
        string Ip;
        int contadorVentasEspera;
        bool EstadoCobrar = false;
        int impuesto;
        int impuesto2;
        int impuesto_final;

 
        public static int IVA19;
        public static int IVA5;
    
        public static double Basesin5;
        public static double Basesin19;
    

        string administrador = "Administrador (Control total)";
        string cajero = "Cajero (¿Si estas autorizado para manejar dinero?)";
        Panel panel_mostrador_de_productos = new Panel();
        Panel panel_mostrador_de_productos_inventario = new Panel();



        private void iToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Ventas_Menu_Princi_Load(object sender, EventArgs e)
        {
            if (LOGIN.lblRol == administrador)
            {              
                btnCreditoPagar.Enabled = true;
                btnverMovimientosCaja.Enabled = true;
                StatusStrip4.Enabled = true;
            }
            if(LOGIN.lblRol == cajero)
            {
                btnverMovimientosCaja.Enabled = true;
                StatusStrip4.Enabled = true;
        
            }
          
            MenuStrip10.Visible = false;

            Bases.Cambiar_idioma_regional();
            Bases.Obtener_serialPC(ref SerialPC);
            Obtener_datos.Obtener_id_caja_PorSerial(ref Id_caja);

            Obtener_id_de_cliente_estandar();

            int id = Presentacion.LOGIN.idcajavariable;
            if (id == Convert.ToInt32(1))
            {
                Obtener_datos.mostrar_inicio_De_sesion2(ref idusuario_que_inicio_sesion);
            }
            else
            {
                Obtener_datos.mostrar_inicio_De_sesion(ref idusuario_que_inicio_sesion);
            }

            try
            {

                string Impuesto;
                string Impuesto2;
                Impuesto = "SELECT Porcentaje_Impuesto FROM EMPRESA";
                Impuesto2 = "SELECT Porcentaje_otros_Impuesto FROM EMPRESA";
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                SqlCommand Porcentaje = new SqlCommand(Impuesto, con);
                SqlCommand Porcentaje1 = new SqlCommand(Impuesto2, con);
                con.Open();
                lblIVA19.Text = Porcentaje.ExecuteScalar().ToString();
                lblIVA5.Text = Porcentaje1.ExecuteScalar().ToString();
                impuesto2 = Convert.ToInt32(lblIVA5.Text);
                impuesto = Convert.ToInt32(lblIVA19.Text);
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ValidarTiposBusqueda();
            Obtener_datos.mostrarTemaCaja(ref Tema);
            if (Tema == "Oscuro")
            {
                ValidarTemaCaja2();
            }
            else
            {
                ValidarTemaCaja();
            }

            limpiar_para_venta_nueva();
            ObtenerIpLocal();

        }
        private void ContarVentasEspera()
        {
            Obtener_datos.contarVentasEspera(ref contadorVentasEspera);
            if (contadorVentasEspera == 0)
            {
                panelNotificacionEspera.Visible = false;
            }
            else
            {
                panelNotificacionEspera.Visible = true;
                lblContadorEspera.Text = contadorVentasEspera.ToString();
            }
        }
        private void ValidarTiposBusqueda()
        {
            MOSTRAR_TIPO_DE_BUSQUEDA();
            if (Tipo_de_busqueda == "TECLADO")
            {
                lbltipodebusqueda2.Text = "Buscar con TECLADO";
                BTNLECTORA.BackColor = Color.WhiteSmoke;
                BTNTECLADO.BackColor = Color.FromArgb(129, 178, 20); ;
                txtbuscar.Clear();
                txtbuscar.Focus();
            }
            else
            {
                lbltipodebusqueda2.Text = "Buscar con LECTORA de Codigos de Barras";
                BTNLECTORA.BackColor = Color.FromArgb(129, 178, 20);
                BTNTECLADO.BackColor = Color.WhiteSmoke;
                txtbuscar.Focus();
                txtbuscar.Clear();
            }
        }
        private void ObtenerIpLocal()
        {

            this.Text = Bases.ObtenerIp(ref Ip);
        }
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
                //MessageBox.Show(ex.StackTrace);
            }
        }
        private void limpiar_para_venta_nueva()
        {
            idVenta = 0;
            Listarproductosagregados();
            txtventagenerada = "VENTA NUEVA";
            txtventageneradaS = "COTIZACIÓN";
            Sumar();
            PanelEnespera.Visible = false;
            panelBienvenida.Visible = true;
            ContarVentasEspera();
            PanelOperaciones.Visible = false;
            Listarproductosagregados();
        }


        double valorTotalUnitarioiva19;
        double valorTotalUnitarioiva5;    
        double sub19;
        double subt5;

        double iva;
        double TOTA5;
        double TOTAL19;
        private void Sumar()
        {
            try
            {
                int x;
                x = datalistadoDetalleVenta.Rows.Count;
                if (x == 0)
                {
                    txt_total_suma.Text = "0.00";
                    lblsubtotalIVA19.Text = "0.00";
                    lblIVA19.Text = "0";
                    lblIVA5.Text = "0";
                    lblvalorIVA19.Text = "0.00";
                }

                double total;
                total = 0;
                double iva19;
                iva19 = 0;
                double iva5;
                iva5 = 0;
                double sinIVA;
                sinIVA = 0;
                double masiva;
                double base5;
                base5 = 0;
                double base19;
                base19 = 0;

                if (impuesto_final == impuesto)
                {
                    foreach (DataGridViewRow fila in datalistadoDetalleVenta.Rows)
                    {
                        total += Convert.ToDouble(fila.Cells["Vlor_Total"].Value);
                        txt_total_suma.Text = total.ToString("N0");

                        iva19 += Convert.ToDouble(fila.Cells["Vlor_IVA_19"].Value);
                        valorTotalUnitarioiva19 = iva19;
                        IVA19 = impuesto_final;
                        lblIVA19.Text = IVA19.ToString();
                        base19 += Convert.ToDouble(fila.Cells["Base_Gravable19"].Value);
                        TOTAL19 = base19;

                    }

                }
                if (impuesto_final == impuesto2)
                {
                    foreach (DataGridViewRow fila in datalistadoDetalleVenta.Rows)
                    {
                        total += Convert.ToDouble(fila.Cells["Vlor_Total"].Value);
                        txt_total_suma.Text = total.ToString("N0");

                        iva5 += Convert.ToDouble(fila.Cells["Vlor_IVA_5"].Value);
                        valorTotalUnitarioiva5 = iva5;
                        IVA5 = impuesto_final;
                        lblIVA5.Text = IVA5.ToString();
                        base5 += Convert.ToDouble(fila.Cells["Base_Gravable5"].Value);
                        TOTA5 = base5;
                    }
                }
                if (impuesto_final == 0)

                {
                    foreach (DataGridViewRow fila in datalistadoDetalleVenta.Rows)
                    {

                        total += Convert.ToDouble(fila.Cells["Vlor_Total"].Value);
                        txt_total_suma.Text = total.ToString("N0");

                        sinIVA += Convert.ToDouble(fila.Cells["V_ttSin_IVA"].Value);
                        iva = sinIVA;

                        if (id19 == impuesto)
                        {

                            iva19 += Convert.ToDouble(fila.Cells["Vlor_IVA_19"].Value);
                            valorTotalUnitarioiva19 = iva19;                 
                            IVA19 = id19;

                            base19 += Convert.ToDouble(fila.Cells["Base_Gravable19"].Value);
                            TOTAL19 = base19;
                        }
                        if (id15 == impuesto2)
                        {

                            iva5 += Convert.ToDouble(fila.Cells["Vlor_IVA_5"].Value);
                            valorTotalUnitarioiva5 = iva5;
                            IVA5 = id15;

                            base5 += Convert.ToDouble(fila.Cells["Base_Gravable5"].Value);
                            TOTA5 = base5;

                        }

                    }

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        string Tipo_de_busqueda;

        private void Obtener_id_de_cliente_estandar()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConexionDt.ConexionData.conexion;
            SqlCommand com = new SqlCommand("select idclientev  from clientes where Estado=0", con);
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
        private void LISTAR_PRODUCTOS_Abuscador()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                ConexionDt.ConexionData.abrir();

                da = new SqlDataAdapter("BUSCAR_PRODUCTOS_oka", ConexionDt.ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letrab", txtbuscar.Text);
                da.Fill(dt);
                DATALISTADO_PRODUCTOS_OKA.DataSource = dt;
                ConexionDt.ConexionData.cerrar();
                DATALISTADO_PRODUCTOS_OKA.Columns[0].Visible = false;
                DATALISTADO_PRODUCTOS_OKA.Columns[1].Visible = false;
                DATALISTADO_PRODUCTOS_OKA.Columns[2].Width = 600;
                DATALISTADO_PRODUCTOS_OKA.Columns[3].Visible = false;
                DATALISTADO_PRODUCTOS_OKA.Columns[4].Visible = false;
                DATALISTADO_PRODUCTOS_OKA.Columns[5].Visible = false;
                DATALISTADO_PRODUCTOS_OKA.Columns[6].Visible = false;
                DATALISTADO_PRODUCTOS_OKA.Columns[7].Visible = false;
                DATALISTADO_PRODUCTOS_OKA.Columns[8].Visible = false;
                DATALISTADO_PRODUCTOS_OKA.Columns[9].Visible = false;
                DATALISTADO_PRODUCTOS_OKA.Columns[10].Visible = false;
                DATALISTADO_PRODUCTOS_OKA.Columns[11].Visible = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show("");
            }
            Logica.Bases.Multilinea(ref DATALISTADO_PRODUCTOS_OKA);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Productos.Productos frm = new Productos.Productos();
            frm.ShowDialog();
        }

        private void BtnCerrar_turno_Click(object sender, EventArgs e)
        {
            Dispose();
            Caja.Cierre_de_Caja frm = new Caja.Cierre_de_Caja();
            frm.ShowDialog();
        }


        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            if (Tipo_de_busqueda == "LECTORA")
            {

                ValidarVentasNuevas();
                lbltipodebusqueda2.Visible = false;
                TimerBUSCADORcodigodebarras.Start();
            }
            else if (Tipo_de_busqueda == "TECLADO")
            {
                if (txtbuscar.Text == "")
                {
                    ocultar_mostrar_productos();

                }
                else if (txtbuscar.Text != "")
                {
                    mostrar_productos();

                }
                LISTAR_PRODUCTOS_Abuscador();
            }

        }

        private void mostrar_productos()
        {
            panel_mostrador_de_productos.Size = new System.Drawing.Size(372, 185);
            panel_mostrador_de_productos.BackColor = Color.White;
            panel_mostrador_de_productos.Location = new Point(txtreferencia.Location.X, txtreferencia.Location.Y);
            panel_mostrador_de_productos.Visible = true;
            DATALISTADO_PRODUCTOS_OKA.Visible = true;
            DATALISTADO_PRODUCTOS_OKA.Dock = DockStyle.Fill;
            DATALISTADO_PRODUCTOS_OKA.BackgroundColor = Color.White;
            lbltipodebusqueda2.Visible = false;
            panel_mostrador_de_productos.Controls.Add(DATALISTADO_PRODUCTOS_OKA);

            this.Controls.Add(panel_mostrador_de_productos);
            panel_mostrador_de_productos.BringToFront();
        }
        private void ocultar_mostrar_productos()
        {
            panel_mostrador_de_productos.Visible = false;
            DATALISTADO_PRODUCTOS_OKA.Visible = false;
            lbltipodebusqueda2.Visible = true;
        }
        public void ValidarVentasNuevas()
        {
            if (datalistadoDetalleVenta.RowCount == 0)
            {
                Limpiar_para_venta_nueva();
            }
        }

        private void vender_a_granel()
        {
            CANTIDAD_A_GRANEL frm = new CANTIDAD_A_GRANEL();
            frm.preciounitario = txtprecio_unitarios;
            frm.txtProducto.Text = producto;
            frm.FormClosing += Frm_FormClosing;
            frm.ShowDialog();
        }

        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ejecutar_ventas_a_granel();
        }

        public void ejecutar_ventas_a_granel()
        {
            ejecutar_insertar_ventas();
            if (txtventagenerada == "VENTA GENERADA")
            {
                insertar_detalle_venta();
                Listarproductosagregados();
                txtbuscar.Text = "";
                txtbuscar.Focus();
            }
            if (txtventagenerada == "COTIZACIÓN")
            {
                ejecutar_insertar_ventas_cot();
                Listarproductosagregados();
                txtbuscar.Text = "";
                txtbuscar.Focus();
            }
        }
        private void contar_stock_detalle_ventas()
        {
            int x;
            x = datalistado_stock_detalle_venta.Rows.Count;
            contador_stock_detalle_de_venta = (x);
        }
        private void mostrar_stock_de_detalle_de_ventas()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();
                da = new SqlDataAdapter("mostrar_stock_de_detalle_de_ventas", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id_producto", idproducto);
                da.Fill(dt);
                datalistado_stock_detalle_venta.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + ex.Message);
            }
        }

        private void vender_por_unidad()
        {
            try
            {
                if (MenuStrip10.Visible == true & txtbuscar.Text == DATALISTADO_PRODUCTOS_OKA.SelectedCells[10].Value.ToString())
                {
                    DATALISTADO_PRODUCTOS_OKA.Visible = true;
                     ejecutar_insertar_ventas_cot();
                    if (txtventageneradaS == "COTIZACIÓN GENERADA")
                    {
                        
                        insertar_detalle_venta();
                        Listarproductosagregados();
                        txtbuscar.Text = "";
                        txtbuscar.Focus();
                    }
                    
                }
                else
                {
                    if (txtbuscar.Text == DATALISTADO_PRODUCTOS_OKA.SelectedCells[10].Value.ToString())
                    {
                        DATALISTADO_PRODUCTOS_OKA.Visible = true;
                        ejecutar_insertar_ventas();
                        if (txtventagenerada == "VENTA GENERADA")
                        {
                            insertar_detalle_venta();
                            Listarproductosagregados();
                            txtbuscar.Text = "";
                            txtbuscar.Focus();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        private void ejecutar_insertar_ventas()
        {
            int impu = 0;
            if (txtventagenerada == "VENTA NUEVA")
            {
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConexionDt.ConexionData.conexion;
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("insertar_venta", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcliente", idClienteEstandar);
                    cmd.Parameters.AddWithValue("@fecha_venta", DateTime.Today);
                    cmd.Parameters.AddWithValue("@nume_documento", 0);
                    cmd.Parameters.AddWithValue("@montototal", 0);
                    cmd.Parameters.AddWithValue("@Tipo_de_pago", 0);
                    cmd.Parameters.AddWithValue("@estado", "EN ESPERA");

                    if (impuesto_final == impuesto)
                    {
                        cmd.Parameters.AddWithValue("@ValorIVA5", 0);
                        cmd.Parameters.AddWithValue("@ValorIVA19", valorTotalImportiva19);
                        cmd.Parameters.AddWithValue("@Sin_IVA", 0);
                        cmd.Parameters.AddWithValue("@Base5", 0);
                        cmd.Parameters.AddWithValue("@Base19", sub19);
                    }
                    if (impuesto_final == impuesto2)
                    {
                        cmd.Parameters.AddWithValue("@ValorIVA5", valorTotalImportiva5);
                        cmd.Parameters.AddWithValue("@ValorIVA19", 0);
                        cmd.Parameters.AddWithValue("@Sin_IVA", 0);
                        cmd.Parameters.AddWithValue("@Base5", subt5);
                        cmd.Parameters.AddWithValue("@Base19", 0);
                    }
                    if (impuesto_final == impu)
                    {
                        cmd.Parameters.AddWithValue("@ValorIVA5", 0);
                        cmd.Parameters.AddWithValue("@ValorIVA19", 0);
                        cmd.Parameters.AddWithValue("@Sin_IVA", 0);
                        cmd.Parameters.AddWithValue("@Base5", 0);
                        cmd.Parameters.AddWithValue("@Base19", 0);
                    }


                    cmd.Parameters.AddWithValue("@Comprobante", 0);
                    cmd.Parameters.AddWithValue("@id_usuario", idusuario_que_inicio_sesion);
                    cmd.Parameters.AddWithValue("@Fecha_de_pago", DateTime.Today);
                    cmd.Parameters.AddWithValue("@ACCION", "VENTA");
                    cmd.Parameters.AddWithValue("@Saldo", 0);
                    cmd.Parameters.AddWithValue("@Pago_con", 0);

                    if (impuesto_final == impuesto)
                    {
                        cmd.Parameters.AddWithValue("@Porcentaje_IVA_5", impu);
                        cmd.Parameters.AddWithValue("@Porcentaje_IVA_19", impuesto_final);
                    }
                    if (impuesto_final == impuesto2)
                    {
                        cmd.Parameters.AddWithValue("@Porcentaje_IVA_5", impuesto_final);
                        cmd.Parameters.AddWithValue("@Porcentaje_IVA_19", impu);
                    }
                    if (impuesto_final == impu)
                    {
                        cmd.Parameters.AddWithValue("@Porcentaje_IVA_5", 0);
                        cmd.Parameters.AddWithValue("@Porcentaje_IVA_19", 0);
                    }

                    cmd.Parameters.AddWithValue("@Id_caja", Id_caja);
                    cmd.Parameters.AddWithValue("@Referencia_tarjeta", 0);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Obtener_id_venta_recien_Creada();
                    txtventagenerada = "VENTA GENERADA";
                    mostrar_panel_de_Cobro();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("insertar_venta");
                }
            }

        }

        private void ejecutar_insertar_ventas_cot()
        {
            int impu = 0;
            if (txtventageneradaS == "COTIZACIÓN")
            {
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConexionDt.ConexionData.conexion;
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("insertar_venta", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcliente", idClienteEstandar);
                    cmd.Parameters.AddWithValue("@fecha_venta", DateTime.Today);
                    cmd.Parameters.AddWithValue("@nume_documento", 0);
                    cmd.Parameters.AddWithValue("@montototal", 0);
                    cmd.Parameters.AddWithValue("@Tipo_de_pago", 0);
                    cmd.Parameters.AddWithValue("@estado", "COTIZACION");
                    if (impuesto_final == impuesto)
                    {
                        cmd.Parameters.AddWithValue("@ValorIVA5", 0);
                        cmd.Parameters.AddWithValue("@ValorIVA19", valorTotalImportiva19);
                        cmd.Parameters.AddWithValue("@Sin_IVA", 0);
                        cmd.Parameters.AddWithValue("@Base5", 0);
                        cmd.Parameters.AddWithValue("@Base19", sub19);
                    }
                    if (impuesto_final == impuesto2)
                    {
                        cmd.Parameters.AddWithValue("@ValorIVA5", valorTotalImportiva5);
                        cmd.Parameters.AddWithValue("@ValorIVA19", 0);
                        cmd.Parameters.AddWithValue("@Sin_IVA", 0);
                        cmd.Parameters.AddWithValue("@Base5", subt5);
                        cmd.Parameters.AddWithValue("@Base19", 0);
                    }
                    if (impuesto_final == impu)
                    {
                        cmd.Parameters.AddWithValue("@ValorIVA5", 0);
                        cmd.Parameters.AddWithValue("@ValorIVA19", 0);
                        cmd.Parameters.AddWithValue("@Sin_IVA", 0);
                        cmd.Parameters.AddWithValue("@Base5", 0);
                        cmd.Parameters.AddWithValue("@Base19", 0);
                    }

                    cmd.Parameters.AddWithValue("@Comprobante", 0);
                    cmd.Parameters.AddWithValue("@id_usuario", idusuario_que_inicio_sesion);
                    cmd.Parameters.AddWithValue("@Fecha_de_pago", DateTime.Today);
                    cmd.Parameters.AddWithValue("@ACCION", "COTIZACIÓN");
                    cmd.Parameters.AddWithValue("@Saldo", 0);
                    cmd.Parameters.AddWithValue("@Pago_con", 0);

                    if (impuesto_final == impuesto)
                    {

                        cmd.Parameters.AddWithValue("@Porcentaje_IVA_5", impu);
                        cmd.Parameters.AddWithValue("@Porcentaje_IVA_19", impuesto_final);
                    }
                    if (impuesto_final == impuesto2)
                    {
                        cmd.Parameters.AddWithValue("@Porcentaje_IVA_5", impuesto_final);
                        cmd.Parameters.AddWithValue("@Porcentaje_IVA_19", impu);
                    }
                    if (impuesto_final == impu)
                    {
                        cmd.Parameters.AddWithValue("@Porcentaje_IVA_5", 0);
                        cmd.Parameters.AddWithValue("@Porcentaje_IVA_19", 0);
                    }
                    cmd.Parameters.AddWithValue("@Id_caja", Id_caja);
                    cmd.Parameters.AddWithValue("@Referencia_tarjeta", 0);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Obtener_id_venta_recien_Creada();
                    txtventageneradaS = "COTIZACIÓN GENERADA";
                    mostrar_panel_de_Cobro();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("insertar_cotizacion");
                }

            }

        }
    
         public void mostrar_panel_de_Cobro()
        {
            panelBienvenida.Visible = false;
            PanelOperaciones.Visible = true;
        }
        private void Obtener_id_venta_recien_Creada()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConexionDt.ConexionData.conexion;
            SqlCommand com = new SqlCommand("mostrar_id_venta_por_Id_caja", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id_caja", Id_caja);
            try
            {
                con.Open();
                idVenta = Convert.ToInt32(com.ExecuteScalar());
              
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("mostrar_id_venta_por_Id_caja");
            }
        }
        public void Listarproductosagregados()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();
                da = new SqlDataAdapter("mostrar_productos_agregados_a_venta", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idventa", idVenta);
                da.Fill(dt);
                
                
                datalistadoDetalleVenta.DataSource = dt;
                con.Close();

                if (MenuStrip10.Visible == true)
                {
                    datalistadoDetalleVenta.Columns[0].Visible = false;
                    datalistadoDetalleVenta.Columns[1].Visible = false;
                    datalistadoDetalleVenta.Columns[2].Visible = false;
                }
                else
                {
                    datalistadoDetalleVenta.Columns[0].Visible = true;
                    datalistadoDetalleVenta.Columns[1].Visible = true;
                    datalistadoDetalleVenta.Columns[2].Visible = true;
                    datalistadoDetalleVenta.Columns[0].Width = 50;
                    datalistadoDetalleVenta.Columns[1].Width = 50;
                    datalistadoDetalleVenta.Columns[2].Width = 50;
                }
               
                datalistadoDetalleVenta.Columns[3].Width = 50;
                datalistadoDetalleVenta.Columns[4].Width = 250;
                datalistadoDetalleVenta.Columns[5].Width = 25;
                datalistadoDetalleVenta.Columns[6].Visible = true;
                datalistadoDetalleVenta.Columns[7].Visible = false;
                datalistadoDetalleVenta.Columns[8].Visible = false;
                datalistadoDetalleVenta.Columns[9].Width = datalistadoDetalleVenta.Width - (datalistadoDetalleVenta.Columns[0].Width - datalistadoDetalleVenta.Columns[1].Width - datalistadoDetalleVenta.Columns[2].Width - datalistadoDetalleVenta.Columns[3].Width- datalistadoDetalleVenta.Columns[4].Width - datalistadoDetalleVenta.Columns[5].Width - datalistadoDetalleVenta.Columns[7].Width - datalistadoDetalleVenta.Columns[14].Width);
                datalistadoDetalleVenta.Columns[10].Visible = false;
                datalistadoDetalleVenta.Columns[11].Visible = false;
                datalistadoDetalleVenta.Columns[12].Visible = false;
                datalistadoDetalleVenta.Columns[13].Visible = false;
                datalistadoDetalleVenta.Columns[14].Width = 100;
                datalistadoDetalleVenta.Columns[15].Visible = false;
                datalistadoDetalleVenta.Columns[16].Visible = true;
                datalistadoDetalleVenta.Columns[17].Visible = true;
                datalistadoDetalleVenta.Columns[18].Visible = true;       
                datalistadoDetalleVenta.Columns[19].Visible = false;
                datalistadoDetalleVenta.Columns[20].Visible = false;
                datalistadoDetalleVenta.Columns[21].Visible = true;
                datalistadoDetalleVenta.Columns[22].Visible = false;
                datalistadoDetalleVenta.Columns[23].Visible = false;
                datalistadoDetalleVenta.Columns[24].Visible = false;
                datalistadoDetalleVenta.Columns[25].Visible = true;
                datalistadoDetalleVenta.Columns[26].Visible = false;
                datalistadoDetalleVenta.Columns[27].Visible = false;
                datalistadoDetalleVenta.Columns[28].Visible = false;
                datalistadoDetalleVenta.Columns[29].Visible = false;
              

                if (Tema == "Redentor")
                {
                    Bases.Multilinea2(ref datalistadoDetalleVenta);
                }
                else
                {

                    Bases.MultilineaTemaOscuro(ref datalistadoDetalleVenta);
                }
                Sumar();
          
 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }    
       
        }
   
        private void insertar_detalle_venta()
        {
            try
            {
                if (lblusaInventarios == "SI")
                {
                    if (lblStock_de_Productos >= txtpantalla)
                    {
                        insertar_detalle_venta_Validado();
                    }
                    else
                    {
                        TimerLABEL_STOCK.Start();
                    }
                }
                else if (lblusaInventarios == "NO")
                {
                    insertar_detalle_venta_SIN_VALIDAR();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }


        private void insertar_detalle_venta_Validado()
        {
          
                int impu = 0;
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConexionDt.ConexionData.conexion;
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("insertar_detalle_venta", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idventa", idVenta);
                    cmd.Parameters.AddWithValue("@fecha_venta", DateTime.Today);
                    cmd.Parameters.AddWithValue("@Id_presentacionfraccionada", idproducto);
                    cmd.Parameters.AddWithValue("@cantidad", txtpantalla);
                    cmd.Parameters.AddWithValue("@preciounitario", txtprecio_unitarios);
                    cmd.Parameters.AddWithValue("@moneda", 0);
                    cmd.Parameters.AddWithValue("@unidades", "Unidad");
                    cmd.Parameters.AddWithValue("@Cantidad_mostrada", txtpantalla);

                    if (MenuStrip10.Visible == true)
                    {
                        cmd.Parameters.AddWithValue("@Estado", "COTIZACIÓN");
                    }
                    if (MenuStrip10.Visible == false)
                    {
                        cmd.Parameters.AddWithValue("@Estado", "EN ESPERA");
                    }
                
                    cmd.Parameters.AddWithValue("@Descripcion", lbldescripcion.Text);
                    cmd.Parameters.AddWithValue("@Codigo", lblcodigo.Text);
                    cmd.Parameters.AddWithValue("@Stock", lblStock_de_Productos);
                    cmd.Parameters.AddWithValue("@Se_vende_a", sevendePor);
                    cmd.Parameters.AddWithValue("@Usa_inventarios", lblusaInventarios);
                    cmd.Parameters.AddWithValue("@Costo", lblcosto.Text);                 


                    if (impuesto_final == impuesto)
                    {
                        cmd.Parameters.AddWithValue("@IVA5", impu);
                        cmd.Parameters.AddWithValue("@IVA19", impuesto_final);
                        cmd.Parameters.AddWithValue("@Sub_total_variante", subTotal);
                        cmd.Parameters.AddWithValue("@Sub_IVA_5", ValorIva_5);
                        cmd.Parameters.AddWithValue("@Sub_IVA_19", ValorIva_19);
                        cmd.Parameters.AddWithValue("@Sin_IVA", 0);
                        cmd.Parameters.AddWithValue("@variante5", 0);
                        cmd.Parameters.AddWithValue("@variante19", subTotal);
                    }
                    if (impuesto_final == impuesto2)
                    {
                        cmd.Parameters.AddWithValue("@IVA5", impuesto_final);
                        cmd.Parameters.AddWithValue("@IVA19", 0);
                        cmd.Parameters.AddWithValue("@Sub_total_variante", subTotal);
                        cmd.Parameters.AddWithValue("@Sub_IVA_5", ValorIva_5);
                        cmd.Parameters.AddWithValue("@Sub_IVA_19", ValorIva_19);
                        cmd.Parameters.AddWithValue("@Sin_IVA", 0);
                        cmd.Parameters.AddWithValue("@variante5", subTotal);
                        cmd.Parameters.AddWithValue("@variante19", 0);
                    }
                    if (impuesto_final == impu)
                    {
                            cmd.Parameters.AddWithValue("@IVA5", 0);
                            cmd.Parameters.AddWithValue("@IVA19", 0);
                            cmd.Parameters.AddWithValue("@Sub_total_variante", subTotal);
                            cmd.Parameters.AddWithValue("@Sub_IVA_5", 0);
                            cmd.Parameters.AddWithValue("@Sub_IVA_19", 0);
                            cmd.Parameters.AddWithValue("@Sin_IVA", sinIvas);
                            cmd.Parameters.AddWithValue("@variante5", 0);
                            cmd.Parameters.AddWithValue("@variante19", 0);
                    }

                    cmd.ExecuteNonQuery();
                    con.Close();
                    if(MenuStrip10.Visible == false)
                    {
                        disminuir_stock_en_detalle_de_venta();
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace + ex.Message);
                }
               

        }
    
        private void disminuir_stock_en_detalle_de_venta()
        {
            try
            {
                ConexionDt.ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("disminuir_stock_en_detalle_de_venta", ConexionDt.ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Producto1", idproducto);
                cmd.Parameters.AddWithValue("@cantidad", txtpantalla);
                cmd.ExecuteNonQuery();
                ConexionDt.ConexionData.cerrar();
            }
            catch (Exception)
            {


            }
        }
        private void insertar_detalle_venta_SIN_VALIDAR()
        {
             int impu = 0;
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConexionDt.ConexionData.conexion;
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("insertar_detalle_venta", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idventa", idVenta);
                    cmd.Parameters.AddWithValue("@fecha_venta", DateTime.Today);
                    cmd.Parameters.AddWithValue("@Id_presentacionfraccionada", idproducto);
                    cmd.Parameters.AddWithValue("@cantidad", txtpantalla);
                    cmd.Parameters.AddWithValue("@preciounitario", txtprecio_unitarios);
                    cmd.Parameters.AddWithValue("@moneda", 0);
                    cmd.Parameters.AddWithValue("@unidades", "Unidad");
                    cmd.Parameters.AddWithValue("@Cantidad_mostrada", txtpantalla);
                    cmd.Parameters.AddWithValue("@Estado", "EN ESPERA");
                    cmd.Parameters.AddWithValue("@Descripcion", lbldescripcion.Text);
                    cmd.Parameters.AddWithValue("@Codigo", lblcodigo.Text);
                    cmd.Parameters.AddWithValue("@Stock", lblStock_de_Productos);
                    cmd.Parameters.AddWithValue("@Se_vende_a", sevendePor);
                    cmd.Parameters.AddWithValue("@Usa_inventarios", lblusaInventarios);
                    cmd.Parameters.AddWithValue("@Costo", lblcosto.Text);

                    if (impuesto_final == impuesto)
                    {
                        cmd.Parameters.AddWithValue("@IVA5", impu);
                        cmd.Parameters.AddWithValue("@IVA19", impuesto_final);
                        cmd.Parameters.AddWithValue("@Sub_total_variante", subTotal);
                        cmd.Parameters.AddWithValue("@Sub_IVA_5", ValorIva_5);
                        cmd.Parameters.AddWithValue("@Sub_IVA_19", ValorIva_19);
                        cmd.Parameters.AddWithValue("@Sin_IVA", 0);
                        cmd.Parameters.AddWithValue("@variante5", 0);
                        cmd.Parameters.AddWithValue("@variante19", subTotal);
                    }
                    if (impuesto_final == impuesto2)
                    {
                        cmd.Parameters.AddWithValue("@IVA5", impuesto_final);
                        cmd.Parameters.AddWithValue("@IVA19", 0);
                        cmd.Parameters.AddWithValue("@Sub_total_variante", subTotal);
                        cmd.Parameters.AddWithValue("@Sub_IVA_5", ValorIva_5);
                        cmd.Parameters.AddWithValue("@Sub_IVA_19", ValorIva_19);
                        cmd.Parameters.AddWithValue("@Sin_IVA", 0);
                        cmd.Parameters.AddWithValue("@variante5", subTotal);
                        cmd.Parameters.AddWithValue("@variante19", 0);
                    }
                    if (impuesto_final == impu)
                    {
                            cmd.Parameters.AddWithValue("@IVA5", 0);
                            cmd.Parameters.AddWithValue("@IVA19", 0);
                            cmd.Parameters.AddWithValue("@Sub_total_variante", subTotal);
                            cmd.Parameters.AddWithValue("@Sub_IVA_5", 0);
                            cmd.Parameters.AddWithValue("@Sub_IVA_19", 0);
                            cmd.Parameters.AddWithValue("@Sin_IVA", sinIvas);
                            cmd.Parameters.AddWithValue("@variante5", 0);
                            cmd.Parameters.AddWithValue("@variante19", 0);
                    }
                                      

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace + ex.Message);
                }
         }       
           
        

 
        private void datalistadoDetalleVenta_CellClick(object sender, DataGridViewCellEventArgs e)  
        {                 

       
            Obtener_datos_del_detalle_de_venta();
         
            if(MenuStrip10.Visible == true)
            {               
               
                Listarproductosagregados();       
            
                
            }
            else
            {
                if (e.ColumnIndex == this.datalistadoDetalleVenta.Columns["S"].Index)
                {

                    txtpantalla = 1;
                    editar_detalle_venta_sumar();


                }
                if (e.ColumnIndex == this.datalistadoDetalleVenta.Columns["R"].Index)
                {
                    txtpantalla = 1;
                    impuesto_final = Convert.ToInt32(DATALISTADO_PRODUCTOS_OKA.SelectedCells[11].Value.ToString());

                    editar_detalle_venta_restar();
                    Eliminar_datos.eliminar_venta(idventa);


                }

                if (e.ColumnIndex == this.datalistadoDetalleVenta.Columns["EL"].Index)
                {
                    foreach (DataGridViewRow row in datalistadoDetalleVenta.SelectedRows)
                    {
                        int iddetalle_venta = Convert.ToInt32(row.Cells["iddetalle_venta"].Value);
                        try
                        {
                            SqlCommand cmd;
                            SqlConnection con = new SqlConnection();
                            con.ConnectionString = ConexionDt.ConexionData.conexion;
                            con.Open();
                            cmd = new SqlCommand("eliminar_detalle_venta", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@iddetalleventa", iddetalle_venta);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    Listarproductosagregados();
                    EliminarVentas();
                    //EliminardeVenta();
                }

            }

           
       
            double TT;
            double b5;
            double b19;

            b19 = valorTotalUnitarioiva19 + valorTotalUnitarioiva5;
            TT = TOTA5 + TOTAL19 + iva;
            lblsubtotalIVA19.Text = TT.ToString("N0");
            lblvalorIVA19.Text = b19.ToString("N0");

            if (datalistadoDetalleVenta.RowCount < 1)
            {
                PanelOperaciones.Visible = false;
                panelBienvenida.Visible = true;
                iva = 0.00;
            }
        }
        private void EliminarVentas()
        {
            contar_tablas_ventas();
            if (Contador == 0)
            {
                eliminar_venta_al_agregar_productos();
                Limpiar_para_venta_nueva();
            }
        }
        int id19;
        int id15;
        int idventa;
        int idvent;
        double viva19;
        double viva5;
        double SINIVA;
        double cost;  

        private void Obtener_datos_del_detalle_de_venta()
        {
            
            try
            {
                iddetalleventa = Convert.ToInt32(datalistadoDetalleVenta.SelectedCells[8].Value.ToString());
                idproducto = Convert.ToInt32(datalistadoDetalleVenta.SelectedCells[7].Value.ToString());
                idventa = Convert.ToInt32(datalistadoDetalleVenta.SelectedCells[8].Value.ToString());
                sevendePor = datalistadoDetalleVenta.SelectedCells[14].Value.ToString();
                lblcosto.Text = datalistadoDetalleVenta.SelectedCells[6].Value.ToString();
                cost = Convert.ToDouble(lblcosto.Text);
                lblusaInventarios = datalistadoDetalleVenta.SelectedCells[13].Value.ToString();
                cantidad = Convert.ToDouble(datalistadoDetalleVenta.SelectedCells[5].Value);
                id15 = Convert.ToInt32(datalistadoDetalleVenta.SelectedCells[16].Value.ToString());
                id19 = Convert.ToInt32(datalistadoDetalleVenta.SelectedCells[17].Value.ToString());            
                viva19 = Convert.ToDouble(datalistadoDetalleVenta.SelectedCells[24].Value.ToString());              
                viva5 = Convert.ToDouble(datalistadoDetalleVenta.SelectedCells[23].Value.ToString());   
                SINIVA = Convert.ToDouble(datalistadoDetalleVenta.SelectedCells[26].Value.ToString());
            
      
            }
            catch (Exception ex)
            {
              
            }
        }
        private void editar_detalle_venta_sumar()
        {
           
            if (lblusaInventarios == "SI")
            {
                
                    lblStock_de_Productos = Convert.ToDouble(datalistadoDetalleVenta.SelectedCells[9].Value.ToString());
     

                if (lblStock_de_Productos > 0 )
                {

                    ejecutar_editar_detalle_venta_sumar();
                    disminuir_stock_en_detalle_de_venta();
                }
                else
                {
                    TimerLABEL_STOCK.Start();
                }

            }
            else
            {
                ejecutar_editar_detalle_venta_sumar();
            }
            Listarproductosagregados();

        }
        private void ejecutar_editar_detalle_venta_sumar()
        {
            try
            {
                SqlCommand cmd;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();
                cmd = new SqlCommand("editar_detalle_venta_sumar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_producto", idproducto);
                cmd.Parameters.AddWithValue("@cantidad", txtpantalla);
                cmd.Parameters.AddWithValue("@Cantidad_mostrada", txtpantalla);
                cmd.Parameters.AddWithValue("@Id_venta", idVenta);
                cmd.ExecuteNonQuery();
                con.Close();              
            }
            catch (Exception)
            {


            }

        }
        private void editar_detalle_venta_restar()
        {
           
            if (lblusaInventarios == "SI")
            {
                ejecutar_editar_detalle_venta_restar();
                aumentar_stock_en_detalle_de_venta();
            }
            else
            {
                ejecutar_editar_detalle_venta_restar();
            }
            Listarproductosagregados();
        }
        private void aumentar_stock_en_detalle_de_venta()
        {
            try
            {
                ConexionDt.ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("aumentar_stock_en_detalle_de_venta", ConexionDt.ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Producto1", idproducto);
                cmd.Parameters.AddWithValue("@cantidad", txtpantalla);
                cmd.ExecuteNonQuery();
                ConexionDt.ConexionData.cerrar();
            }
            catch (Exception)
            {

            }
        }
        private void ejecutar_editar_detalle_venta_restar()
        {
            try
            {
                SqlCommand cmd;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();
                cmd = new SqlCommand("editar_detalle_venta_restar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iddetalle_venta", iddetalleventa);
                cmd.Parameters.AddWithValue("cantidad", txtpantalla);
                cmd.Parameters.AddWithValue("@Cantidad_mostrada", txtpantalla);
                cmd.Parameters.AddWithValue("@Id_producto", idproducto);
                cmd.Parameters.AddWithValue("@Id_venta", idVenta);
                cmd.ExecuteNonQuery();
                con.Close();
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void contar_tablas_ventas()
        {
            int x;
            x = datalistadoDetalleVenta.Rows.Count;
            Contador = (x);
        }
        private void eliminar_venta_al_agregar_productos()
        {
            try
            {
                SqlCommand cmd;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();
                cmd = new SqlCommand("eliminar_venta", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idventa", idVenta);
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void datalistadoDetalleVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (datalistadoDetalleVenta.RowCount > 0)
            {

                Obtener_datos_del_detalle_de_venta();
                if (e.KeyChar == Convert.ToChar("+"))
                {
                    editar_detalle_venta_sumar();
                }
                if (e.KeyChar == Convert.ToChar("-"))
                {
                    editar_detalle_venta_restar();
                    contar_tablas_ventas();
                    if (Contador == 0)
                    {
                        eliminar_venta_al_agregar_productos();
                        txtventagenerada = "VENTA NUEVA";
                        txtventageneradaS = "COTIZACIÓN";
                    }
                }
            }
          
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "9";
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtmonto.Text = txtmonto.Text + "0";
        }

        private void txtmonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            
           Bases.Separador_de_Numeros(txtmonto, e);
        }
        bool SECUENCIA = true;
        private void btnborrartodo_Click(object sender, EventArgs e)
        {
            txtmonto.Clear();
            SECUENCIA = true;
        }

        private void btnborrarderecha_Click(object sender, EventArgs e)
        {
            if (SECUENCIA == true)
            {
                txtmonto.Text = txtmonto.Text + ".";
                SECUENCIA = false;
            }
            else
            {
                return;
            }
        }      

        private void TimerBUSCADORcodigodebarras_Tick(object sender, EventArgs e)
        {
            TimerBUSCADORcodigodebarras.Stop();
            vender_por_lectora_de_barras();
        }
        private void vender_por_lectora_de_barras()
        {
            try
            {
                    if (txtbuscar.Text == "")
                {
                    DATALISTADO_PRODUCTOS_OKA.Visible = false;
                    lbltipodebusqueda2.Visible = true;
                }
                if (txtbuscar.Text != "")
                {
                    DATALISTADO_PRODUCTOS_OKA.Visible = true;
                    lbltipodebusqueda2.Visible = false;
                    LISTAR_PRODUCTOS_Abuscador();
                
                        idproducto = Convert.ToInt32(DATALISTADO_PRODUCTOS_OKA.SelectedCells[1].Value.ToString());
               
               
                    mostrar_stock_de_detalle_de_ventas();
                    contar_stock_detalle_ventas();

                    if (contador_stock_detalle_de_venta == 0)
                    {
                        lblStock_de_Productos = Convert.ToDouble(DATALISTADO_PRODUCTOS_OKA.SelectedCells[4].Value.ToString());
                    }
                    else
                    {
                        lblStock_de_Productos =Convert.ToDouble( datalistado_stock_detalle_venta.SelectedCells[1].Value.ToString());
                    }
                    lblusaInventarios = DATALISTADO_PRODUCTOS_OKA.SelectedCells[3].Value.ToString();
                    lbldescripcion.Text = DATALISTADO_PRODUCTOS_OKA.SelectedCells[9].Value.ToString();
                    lblcodigo.Text = DATALISTADO_PRODUCTOS_OKA.SelectedCells[10].Value.ToString();
                    lblcosto.Text = DATALISTADO_PRODUCTOS_OKA.SelectedCells[5].Value.ToString();
                    txtprecio_unitarios =Convert.ToDouble( DATALISTADO_PRODUCTOS_OKA.SelectedCells[6].Value.ToString());
                    sevendePor = DATALISTADO_PRODUCTOS_OKA.SelectedCells[8].Value.ToString();
                    impuesto_final = Convert.ToInt32(DATALISTADO_PRODUCTOS_OKA.SelectedCells[11].Value.ToString());
                    subTotal = Convert.ToInt32(DATALISTADO_PRODUCTOS_OKA.SelectedCells[12].Value.ToString());
                    ValorIva_5 = Convert.ToDouble(DATALISTADO_PRODUCTOS_OKA.SelectedCells[13].Value.ToString());
                    ValorIva_19 = Convert.ToDouble(DATALISTADO_PRODUCTOS_OKA.SelectedCells[14].Value.ToString());
                    if (sevendePor == "Unidad")
                    {
                        txtpantalla = 1;
                        vender_por_unidad();
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtmonto.Text))
            {
                if (datalistadoDetalleVenta.RowCount > 0)
                {

                    if (sevendePor == "Unidad")

                    {
                        string cadena = txtmonto.Text;
                        if (cadena.Contains("."))
                        {
                            MessageBox.Show("Este Producto no acepta decimales ya que esta configurado para ser vendido por UNIDAD", "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            BotonCantidad();

                        }
                    }
                    else if (sevendePor == "Granel")
                    {
                        BotonCantidad();
                    }

                }
                else
                {
                    txtmonto.Clear();
                    txtmonto.Focus();
                }
            }
            double TT;
            double b5;
            double b19;

            b19 = valorTotalUnitarioiva19 + valorTotalUnitarioiva5;
            TT = TOTA5 + TOTAL19 + iva;
            lblsubtotalIVA19.Text = TT.ToString("N0");
            lblvalorIVA19.Text = b19.ToString("N0");
        }
        private void BotonCantidad()
        {

            double MontoaIngresar;
            MontoaIngresar = Convert.ToDouble(txtmonto.Text);
            double Cantidad;
            Cantidad = Convert.ToDouble(datalistadoDetalleVenta.SelectedCells[5].Value);

            double stock;
            double condicional;
            string ControlStock;
            ControlStock = datalistadoDetalleVenta.SelectedCells[16].Value.ToString();
            if (ControlStock == "SI")
            {
                stock = Convert.ToDouble(datalistadoDetalleVenta.SelectedCells[11].Value);
                condicional = Cantidad + stock;
                if (condicional >= MontoaIngresar)
                {
                    BotonCantidadEjecuta();
                }
                else
                {
                    TimerLABEL_STOCK.Start();
                }
            }
            else
            {
                BotonCantidadEjecuta();
            }    

        }
        private void BotonCantidadEjecuta()
        {
            double MontoaIngresar;
            MontoaIngresar = Convert.ToDouble(txtmonto.Text);
            double Cantidad;
            Cantidad = Convert.ToDouble(datalistadoDetalleVenta.SelectedCells[5].Value);

            if (MontoaIngresar > Cantidad)
            {
                txtpantalla = MontoaIngresar - Cantidad;
                editar_detalle_venta_sumar();
            }
            else if (MontoaIngresar < Cantidad)
            {
                txtpantalla = Cantidad - MontoaIngresar;
                editar_detalle_venta_restar();
            }
        }
        private void editar_detalle_venta_CANTIDAD()
        {
            try
            {
                SqlCommand cmd;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();
                cmd = new SqlCommand("editar_detalle_venta_CANTIDAD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_producto", idproducto);
                cmd.Parameters.AddWithValue("@cantidad", txtmonto.Text);
                cmd.Parameters.AddWithValue("@Cantidad_mostrada", txtmonto.Text);
                cmd.Parameters.AddWithValue("@Id_venta", idVenta);
                cmd.ExecuteNonQuery();
                con.Close();
                Listarproductosagregados();
                txtmonto.Clear();
                txtmonto.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        private void frm_FormClosed(Object sender, FormClosedEventArgs e)
        {
            Limpiar_para_venta_nueva();
        }
        private void Limpiar_para_venta_nueva()
        {
            idVenta = 0;
            Listarproductosagregados();
            txtventagenerada = "VENTA NUEVA";
            txtventageneradaS = "COTIZACIÓN";
            Sumar();            
            PanelEnespera.Visible = false;        
            panelBienvenida.Visible = true;
            PanelOperaciones.Visible = false;
            ContarVentasEspera();
            lblsubtotalIVA19.Text = "";
            lblvalorIVA19.Text = "";
            lblIVA19.Text = "";
            lblIVA5.Text = "";
            TOTA5 = 0;
            TOTAL19 = 0;
            iva = 0;
            activado = "DESACTIVADO";
        }

        private void TimerLABEL_STOCK_Tick(object sender, EventArgs e)
        {
            if(MenuStrip10.Visible == true)
            {
                if (ProgressBarETIQUETA_STOCK.Value < 100)
                {
                    ProgressBarETIQUETA_STOCK.Value = ProgressBarETIQUETA_STOCK.Value + 10;
                    lblcoti.Visible = false;
                    LABEL_STOCK.Visible = true;
                    LABEL_STOCK.Dock = DockStyle.Fill;
                }
                else
                {

                    LABEL_STOCK.Visible = false;
                    lblcoti.Visible = true;
                    LABEL_STOCK.Dock = DockStyle.None;
                    ProgressBarETIQUETA_STOCK.Value = 0;
                    TimerLABEL_STOCK.Stop();
                }
            
            }
            else
            {
                if (ProgressBarETIQUETA_STOCK.Value < 100)
                {
                    ProgressBarETIQUETA_STOCK.Value = ProgressBarETIQUETA_STOCK.Value + 10;
                    LABEL_STOCK.Visible = true;
                    LABEL_STOCK.Dock = DockStyle.Fill;
                }
                else
                {
                    LABEL_STOCK.Visible = false;
                    LABEL_STOCK.Dock = DockStyle.None;
                    ProgressBarETIQUETA_STOCK.Value = 0;
                    TimerLABEL_STOCK.Stop();
                }
            }
      
        }

        private void btEfectivo_Click(object sender, EventArgs e)
        {
            if(MenuStrip10.Visible==true)
            {
                MessageBox.Show("En el MODO COTIZADOR no puedes generar ventas, imprimir la cotización directa desde el boton IMPRIMIR ","Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information );
            }
            else
            {
                Cobrar();
            }
            lblsubtotalIVA19.Text = "0.00";
            lblvalorIVA19.Text = "0.00";
            lblIVA19.Text = "0.00";
            lblIVA5.Text = "0.00";
        }
        public static string activado; 
        private void Cobrar()
        {
            if (datalistadoDetalleVenta.RowCount > 0)
            {
                if(MenuStrip10.Visible==true)
                {
                    activado = "ACTIVADO";                

                    total = Convert.ToDouble(txt_total_suma.Text);
                    Ventas_Menu_Principal.MEDIOS_DE_PAGO frm = new Ventas_Menu_Principal.MEDIOS_DE_PAGO();
                    frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                    frm.ShowDialog();
                }
            }
            if (datalistadoDetalleVenta.RowCount > 0)
            {
                
                    subt5 = Convert.ToDouble(TOTA5);
                    sub19 = Convert.ToDouble(TOTAL19);

                    total = Convert.ToDouble(txt_total_suma.Text);
                    valorTotalImportiva19 = valorTotalUnitarioiva19;
                    valorTotalImportiva5 = valorTotalUnitarioiva5;
                    SinIVA = iva;
                    Basesin5 = subt5;
                    Basesin19 = sub19;
                    Ventas_Menu_Principal.MEDIOS_DE_PAGO frm = new Ventas_Menu_Principal.MEDIOS_DE_PAGO();
                    frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                    frm.ShowDialog();
                
            }
                //Sumar();
              
        }
        

        private void DATALISTADO_PRODUCTOS_OKA_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
        
            if (MenuStrip10.Visible ==true)
            {
                vender_por_teclado_coti();
            }
            else
            {
                vender_por_teclado();
            }
           
        }

        public string producto;
        private void vender_por_teclado_coti()
        {
            ValidarVentasNuevas();
            txtbuscar.Text = DATALISTADO_PRODUCTOS_OKA.SelectedCells[10].Value.ToString();
            idproducto = Convert.ToInt32(DATALISTADO_PRODUCTOS_OKA.SelectedCells[1].Value.ToString());
            impuesto_final = Convert.ToInt32(DATALISTADO_PRODUCTOS_OKA.SelectedCells[11].Value.ToString());
            // mostramos los registros del producto en el detalle de venta          
            mostrar_stock_de_detalle_de_ventas();
            contar_stock_detalle_ventas();

            if (contador_stock_detalle_de_venta == 0)
            {
                // Si es producto no esta agregado a las ventas se tomara el Stock de la tabla Productos
                lblStock_de_Productos = Convert.ToDouble(DATALISTADO_PRODUCTOS_OKA.SelectedCells[4].Value.ToString());
            }
            else
            {
                //en caso que el producto ya este agregado al detalle de venta se va a extraer el Stock de la tabla Detalle_de_venta
                lblStock_de_Productos = Convert.ToDouble(datalistado_stock_detalle_venta.SelectedCells[1].Value.ToString());
            }
            //Extraemos los datos del producto de la tabla Productos directamente
            lblusaInventarios = DATALISTADO_PRODUCTOS_OKA.SelectedCells[3].Value.ToString();
            lbldescripcion.Text = DATALISTADO_PRODUCTOS_OKA.SelectedCells[9].Value.ToString();
            lblcodigo.Text = DATALISTADO_PRODUCTOS_OKA.SelectedCells[10].Value.ToString();
            lblcosto.Text = DATALISTADO_PRODUCTOS_OKA.SelectedCells[5].Value.ToString();
            sevendePor = DATALISTADO_PRODUCTOS_OKA.SelectedCells[8].Value.ToString();
            txtprecio_unitarios = Convert.ToDouble(DATALISTADO_PRODUCTOS_OKA.SelectedCells[6].Value.ToString());
            subTotal = Convert.ToDouble(DATALISTADO_PRODUCTOS_OKA.SelectedCells[12].Value.ToString());
            ValorIva_5 = Convert.ToDouble(DATALISTADO_PRODUCTOS_OKA.SelectedCells[13].Value.ToString());
            ValorIva_19 = Convert.ToDouble(DATALISTADO_PRODUCTOS_OKA.SelectedCells[14].Value.ToString());
            sinIvas = Convert.ToDouble(DATALISTADO_PRODUCTOS_OKA.SelectedCells[15].Value.ToString());


            //Preguntamos que tipo de producto sera el que se agrege al detalle de venta

            if (sevendePor == "Granel")
            {
                vender_a_granel();
            }
            else if (sevendePor == "Unidad")
            {
                txtpantalla = 1;
                vender_por_unidad();
            }
            double TT;
            double b5;
            double b19;

            b19 = valorTotalUnitarioiva19 + valorTotalUnitarioiva5;
            TT = TOTA5 + TOTAL19 + iva + vtempo;
            lblsubtotalIVA19.Text = TT.ToString("N0");
            lblvalorIVA19.Text = b19.ToString("N0");
            lblcoti.Visible = false;
            lblcotizador.Visible = true;
        }
        private void vender_por_teclado()
        {
            ValidarVentasNuevas();
            txtbuscar.Text = DATALISTADO_PRODUCTOS_OKA.SelectedCells[10].Value.ToString();
            idproducto = Convert.ToInt32(DATALISTADO_PRODUCTOS_OKA.SelectedCells[1].Value.ToString());
            impuesto_final = Convert.ToInt32(DATALISTADO_PRODUCTOS_OKA.SelectedCells[11].Value.ToString());
            // mostramos los registros del producto en el detalle de venta          
            mostrar_stock_de_detalle_de_ventas();
            contar_stock_detalle_ventas();

            if (contador_stock_detalle_de_venta == 0)
            {
                // Si es producto no esta agregado a las ventas se tomara el Stock de la tabla Productos
                lblStock_de_Productos = Convert.ToDouble(DATALISTADO_PRODUCTOS_OKA.SelectedCells[4].Value.ToString());
            }
            else
            {
                //en caso que el producto ya este agregado al detalle de venta se va a extraer el Stock de la tabla Detalle_de_venta
                lblStock_de_Productos = Convert.ToDouble(datalistado_stock_detalle_venta.SelectedCells[1].Value.ToString());
            }
            //Extraemos los datos del producto de la tabla Productos directamente
            lblusaInventarios = DATALISTADO_PRODUCTOS_OKA.SelectedCells[3].Value.ToString();
            producto = lbldescripcion.Text = DATALISTADO_PRODUCTOS_OKA.SelectedCells[9].Value.ToString();
            lblcodigo.Text = DATALISTADO_PRODUCTOS_OKA.SelectedCells[10].Value.ToString();
            lblcosto.Text = DATALISTADO_PRODUCTOS_OKA.SelectedCells[5].Value.ToString();
            sevendePor = DATALISTADO_PRODUCTOS_OKA.SelectedCells[8].Value.ToString();
            txtprecio_unitarios = Convert.ToDouble(DATALISTADO_PRODUCTOS_OKA.SelectedCells[6].Value.ToString());
            subTotal = Convert.ToDouble(DATALISTADO_PRODUCTOS_OKA.SelectedCells[12].Value.ToString());
            ValorIva_5 = Convert.ToDouble(DATALISTADO_PRODUCTOS_OKA.SelectedCells[13].Value.ToString());
            ValorIva_19 = Convert.ToDouble(DATALISTADO_PRODUCTOS_OKA.SelectedCells[14].Value.ToString());
            sinIvas = Convert.ToDouble(DATALISTADO_PRODUCTOS_OKA.SelectedCells[15].Value.ToString());   
        

            //Preguntamos que tipo de producto sera el que se agrege al detalle de venta

            if (sevendePor == "Granel")
            {
                vender_a_granel();
            }
            else if (sevendePor == "Unidad")
            {
                txtpantalla = 1;
                vender_por_unidad();
            }         
            double TT;
            double b5;
            double b19;
           
            b19 = valorTotalUnitarioiva19 + valorTotalUnitarioiva5;       
            

                TT = TOTA5 + TOTAL19 + iva + vtempo;
                lblsubtotalIVA19.Text = TT.ToString("N0");
                lblvalorIVA19.Text = b19.ToString("N0");
            
         
        }
        private void btnespera_Click(object sender, EventArgs e)
        {
            if (datalistadoDetalleVenta.RowCount > 0)
            {
                PanelEnespera.Visible = true;
                PanelEnespera.BringToFront();
                PanelEnespera.Dock = DockStyle.Fill;
                txtnombre.Clear();
            }
        }
         double control;
        private void btnrestaurar_Click(object sender, EventArgs e)
        {
         
            Ventas_en_espera frm = new Ventas_en_espera();
            frm.FormClosing += Frm_FormClosing1;
            frm.ShowDialog();
            mostrar_panel_de_Cobro();
            if (control > 0)
            {
               
                MessageBox.Show("Se está restaurando la venta de ESPERA, recuerda eliminar el registro de la vista VENTAS EN ESPERA AL TERMINAR ESTÁ VENTA.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
              PanelOperaciones.Visible = false;
            }         
          
        }

        double vtempo;
        private void Frm_FormClosing1(object sender, FormClosingEventArgs e)
        {
            Listarproductosagregados();       
       
            double tt;          
            double b19;
            tt = TOTA5 + TOTAL19 + iva;
            lblsubtotalIVA19.Text = tt.ToString("N0");
            control = tt;
            vtempo = tt;
            vtempo = 0;
            b19 = valorTotalUnitarioiva19 + valorTotalUnitarioiva5;
            lblvalorIVA19.Text = b19.ToString("N0");
        }        
    
        private void btneliminar_Click(object sender, EventArgs e)
        {
            elimivarventas();
        }
        public void elimivarventas()
        {        
           
          
                if (datalistadoDetalleVenta.RowCount > 0)
                {
                    DialogResult pregunta = MessageBox.Show("¿Realmente desea eliminar esta Venta?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (pregunta == DialogResult.OK)
                    {
                        Eliminar_datos.eliminar_venta(idVenta);
                        Limpiar_para_venta_nueva();
                    }
                }           

    
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtnombre.Text))
            {
                editarVentaEspera();
                MessageBox.Show("Esta venta se mantendrá en espera por 15 minutos, Pasado ese tiempo no podrás recuperarlo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ingrese una referencia");
            }
        }
        private void editarVentaEspera()

        {
            Editar_datos.ingresar_nombre_a_venta_en_espera(idVenta, txtnombre.Text);
            Limpiar_para_venta_nueva();
            ocularPanelenEspera();
            lblsubtotalIVA19.Text = "0.00";
            lblIVA19.Text = "0";
            lblIVA5.Text = "0";
            lblvalorIVA19.Text = "0.00";
        }
        private void button9_Click(object sender, EventArgs e)
        {
            ocularPanelenEspera();
        }
        private void ocularPanelenEspera()
        {
            PanelEnespera.Visible = false;
            PanelEnespera.Dock = DockStyle.None;

        }
 
        private void button6_Click(object sender, EventArgs e)
        {
            txtnombre.Text = "Ticket" + idVenta;       
            editarVentaEspera();
            MessageBox.Show("Esta venta se mantendrá en espera por 15 minutos, Pasado ese tiempo no podrás recuperarlo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCreditoCobrar_Click(object sender, EventArgs e)
        {
            Apertura_de_credito.PorCobrarOk frm = new Apertura_de_credito.PorCobrarOk();
            frm.ShowDialog();
        }

        private void btnCreditoPagar_Click(object sender, EventArgs e)
        {
            Apertura_de_credito.PorPagar frm = new Apertura_de_credito.PorPagar();
            frm.ShowDialog();
        }

        private void btnIngresosCaja_Click(object sender, EventArgs e)
        {
            Ingresos_varios.IngresosVarios frm = new Ingresos_varios.IngresosVarios();
            frm.ShowDialog();
        }

        private void btnGastos_Click(object sender, EventArgs e)
        {
            Gastos_varios.Gastos frm = new Gastos_varios.Gastos();
            frm.ShowDialog();
        }

        private void btnverMovimientosCaja_Click(object sender, EventArgs e)
        {
            Caja.Listado_gastos_ingresos frm = new Caja.Listado_gastos_ingresos();
            frm.ShowDialog();
        }

        private void btnadmin_Click(object sender, EventArgs e)
        {
            Dispose();
            Presentacion.Admin_nivel_dios.DASHBOARD_PRINCIPAL frm = new Presentacion.Admin_nivel_dios.DASHBOARD_PRINCIPAL();
            frm.ShowDialog();
        }
        private void Ventas_Meno_Princi_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dlgRes = MessageBox.Show("¿Realmente desea Cerrar el Sistema?", "Cerrando", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgRes == DialogResult.Yes)
            {
                Dispose();
                CopiasBd.GeneradorAutomatico frm = new CopiasBd.GeneradorAutomatico();
                frm.ShowDialog();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void BTNLECTORA_Click(object sender, EventArgs e)
        {
            ModoLectora();

        }
        private void ModoLectora()
        {
            ocultar_mostrar_productos();
            lbltipodebusqueda2.Text = "Buscar con LECTORA de Codigos de Barras";
            Tipo_de_busqueda = "LECTORA";
            activado2.Visible = true;
            activado1.Visible = false;

            txtbuscar.Clear();
            txtbuscar.Focus();
        }
        private void BTNTECLADO_Click(object sender, EventArgs e)
        {
            ModoTeclado();
        }
        private void ModoTeclado()
        {
            ocultar_mostrar_productos();
            lbltipodebusqueda2.Text = "Buscar con TECLADO";
            Tipo_de_busqueda = "TECLADO";
            activado1.Visible = true;
            activado2.Visible = false;                 
            txtbuscar.Clear();
            txtbuscar.Focus();
        }

 

        private void btnMayoreo_Click(object sender, EventArgs e)
        {
            aplicar_precio_mayoreo();
        }
        private void aplicar_precio_mayoreo()
        {
            if (datalistadoDetalleVenta.Rows.Count > 0)
            {
                LdetalleVenta parametros = new LdetalleVenta();
                Editar_datos funcion = new Editar_datos();
                parametros.Id_producto = idproducto;
                parametros.iddetalle_venta = iddetalleventa;
                if (funcion.aplicar_precio_mayoreo(parametros) == true)
                {
                    Listarproductosagregados();
                }
            }
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtmonto.Text))
            {
                LdetalleVenta parametros = new LdetalleVenta();
                Editar_datos funcion = new Editar_datos();
                parametros.iddetalle_venta = iddetalleventa;
                parametros.preciounitario = Convert.ToDouble(txtmonto.Text);
                if (funcion.editarPrecioVenta(parametros) == true)
                {
                    Listarproductosagregados();
                    //subtotalImpuesto();
                }
            }
        }

        private void btnDevolucion_Click(object sender, EventArgs e)
        {
            HistorialVentas.HistorialVentasForm frm = new HistorialVentas.HistorialVentasForm();
            frm.ShowDialog();
        }

        private void activarTema_Click(object sender, EventArgs e)
        {
            Obtener_datos.mostrarTemaCaja(ref Tema);
            Tema = "Oscuro";
            EditarTemaCaja();
            TemaOscuro();
            Listarproductosagregados();
        }
        private void ValidarTemaCaja2()
        {
            Obtener_datos.mostrarTemaCaja(ref Tema);
            if (Tema == "Oscuro")
            {
                TemaOscuro();
            }                    
           
        }
        private void ValidarTemaCaja()
        {
            Obtener_datos.mostrarTemaCaja(ref Tema);
            if (Tema == "Redentor")           
            {               
                TemaClaro();              
            }              
        }
        private void EditarTemaCaja()
        {
            Lcaja parametros = new Lcaja();
            Editar_datos funcion = new Editar_datos();
            parametros.Tema = Tema;
            funcion.EditarTemaCaja(parametros);

        }
        private void TemaOscuro() 
        {
            //PanelC1 Encabezado
            Panelc1.BackColor = Color.FromArgb(35, 35, 35);               
            txtbuscar.BackColor = Color.FromArgb(20, 20, 20);
            txtbuscar.ForeColor = Color.White;
            lbltipodebusqueda2.BackColor = Color.FromArgb(20, 20, 20);
            lbltipodebusqueda2.ForeColor = Color.Silver;
            label15.ForeColor = Color.Silver;    
            BTNTECLADO.BackColor = Color.FromArgb(20, 20, 20);
            BTNLECTORA.BackColor = Color.FromArgb(20, 20, 20);
            BTNTECLADO.ForeColor = Color.White;
            BTNLECTORA.ForeColor = Color.White;
            lblContadorEspera.BackColor = Color.FromArgb(20, 20, 20);
    
            lblpeso.ForeColor = Color.WhiteSmoke;
            Panel15.BackColor= Color.FromArgb(20, 20, 20);
            button12.ForeColor = Color.FromArgb(20, 20, 20);          
            MenuStrip10.BackColor = Color.FromArgb(20, 20, 20);
            MenuStrip10.ForeColor = Color.White;       
            lblcotireferencia.ForeColor = Color.White;
            lblcotizador.ForeColor = Color.White;
            //datalistadoDetalleVenta.BackgroundColor = Color.Silver;
            LABEL_STOCK.BackColor = Color.Silver;
            //PanelC2 Intermedio
            panelc2.BackColor = Color.FromArgb(35, 35, 35);
            Cobros.BackColor = Color.FromArgb(45, 45, 45);
            Cobros.ForeColor = Color.White;
            PagosProveedores.BackColor = Color.FromArgb(45, 45, 45);
            PagosProveedores.ForeColor = Color.White;




            btnCreditoCobrar.BackColor = Color.FromArgb(45, 45, 45);
            btnCreditoCobrar.ForeColor = Color.White;
            btnCreditoPagar.BackColor = Color.FromArgb(45, 45, 45);
            btnCreditoPagar.ForeColor = Color.White;

            //PanelC3
           
            btnMayoreo.BackColor = Color.FromArgb(45, 45, 45);
            btnMayoreo.ForeColor = Color.White;
          
            btnIngresosCaja.BackColor = Color.FromArgb(45, 45, 45);
            btnIngresosCaja.ForeColor = Color.White;
            btnGastos.BackColor = Color.FromArgb(45, 45, 45);
            btnGastos.ForeColor = Color.White;
            //BtnTecladoV.BackColor = Color.FromArgb(45, 45, 45);
            //BtnTecladoV.ForeColor = Color.White;
            //PanelC4 Pie de pagina
            panelc4.BackColor = Color.FromArgb(20, 20, 20);
            btnespera.BackColor = Color.FromArgb(20, 20, 20);
            btnespera.ForeColor = Color.White;
            btnrestaurar.BackColor = Color.FromArgb(20, 20, 20);
            btnrestaurar.ForeColor = Color.White;
            btneliminar.BackColor = Color.FromArgb(20, 20, 20);
            btneliminar.ForeColor = Color.White;
            btnDevolucion.BackColor = Color.FromArgb(20, 20, 20);
            btnDevolucion.ForeColor = Color.White;
            //PanelOperaciones
            PanelOperaciones.BackColor = Color.FromArgb(28, 28, 28);
            txt_total_suma.ForeColor = Color.WhiteSmoke;
            //PanelBienvenida
            panelBienvenida.BackColor = Color.FromArgb(35, 35, 35);
            label8.ForeColor = Color.WhiteSmoke;
            Listarproductosagregados();
        }

        private void desactivarTEMA_Click(object sender, EventArgs e)
        {
            Obtener_datos.mostrarTemaCaja(ref Tema);
            Tema = "Redentor";
            EditarTemaCaja();
            TemaClaro();
            Listarproductosagregados();
        }
        private void TemaClaro()
        {
            //PanelC1 encabezado
            Panelc1.BackColor = Color.FromArgb(32, 106, 93);
            txtbuscar.BackColor = Color.White;
            txtbuscar.ForeColor = Color.Black;
            lbltipodebusqueda2.BackColor = Color.White;
            lbltipodebusqueda2.ForeColor = Color.DimGray;
            label15.ForeColor = Color.Black;      
            lblpeso.ForeColor = Color.White;
            Panel15.BackColor = Color.FromArgb(231, 63, 67);
            lblcotireferencia.ForeColor = Color.Black;
            lblContadorEspera.BackColor = Color.Transparent;
            lblcotizador.ForeColor = Color.Black;

            MenuStrip10.BackColor = Color.FromArgb(32, 106, 93);
            MenuStrip10.ForeColor = Color.White;            
            BTNTECLADO.BackColor = Color.FromArgb(32, 106, 93);
            BTNLECTORA.BackColor = Color.FromArgb(32, 106, 93);
            BTNTECLADO.ForeColor = Color.White;
            BTNLECTORA.ForeColor = Color.White;
            //PanelC2 intermedio
            panelc2.BackColor = Color.White;
            Cobros.BackColor = Color.WhiteSmoke;
            Cobros.ForeColor = Color.Black;
            PagosProveedores.BackColor = Color.WhiteSmoke;
            PagosProveedores.ForeColor = Color.Black;
            button12.ForeColor = Color.Black;
        



            btnCreditoCobrar.BackColor = Color.WhiteSmoke;
            btnCreditoCobrar.ForeColor = Color.Black;
            btnCreditoPagar.BackColor = Color.WhiteSmoke;
            btnCreditoPagar.ForeColor = Color.Black;

            //PanelC3
          
            btnMayoreo.BackColor = Color.WhiteSmoke;
           
            btnMayoreo.ForeColor = Color.Black;
          
            btnIngresosCaja.BackColor = Color.WhiteSmoke;
            btnIngresosCaja.ForeColor = Color.Black;
            btnGastos.BackColor = Color.WhiteSmoke;
            btnGastos.ForeColor = Color.Black;
            //BtnTecladoV.BackColor = Color.WhiteSmoke;
            //BtnTecladoV.ForeColor = Color.Black;
            //PanelC4 pie de pagina
            panelc4.BackColor = Color.Gainsboro;
            btnespera.BackColor = Color.Gainsboro;
            btnespera.ForeColor = Color.Black;
            btnrestaurar.BackColor = Color.Gainsboro;
            btnrestaurar.ForeColor = Color.Black;
            btneliminar.BackColor = Color.Gainsboro;
            btneliminar.ForeColor = Color.Black;
            btnDevolucion.BackColor = Color.Gainsboro;
            btnDevolucion.ForeColor = Color.Black;
            //PanelOperaciones
            PanelOperaciones.BackColor = Color.FromArgb(242, 243, 245);
            txt_total_suma.ForeColor = Color.White;
            //PanelBienvenida
            panelBienvenida.BackColor = Color.White;
            label8.ForeColor = Color.FromArgb(64, 64, 64);
            Listarproductosagregados();
        }

        private void txtbuscar_KeyDown(object sender, KeyEventArgs e)
        {
            EventosTiposbusqueda(e);
            EventosNavegarDgproductos(e);
            EventosNavegarDgDetallVenta(e);
        }
        private void EventosNavegarDgDetallVenta(KeyEventArgs e)
        {
            if (DATALISTADO_PRODUCTOS_OKA.Visible == false)
            {
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    DATALISTADO_PRODUCTOS_OKA.Focus();
                }
            }
        }

        private void EventosNavegarDgproductos(KeyEventArgs e)
        {
            EstadoCobrar = true;
            if (DATALISTADO_PRODUCTOS_OKA.Visible == true)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    EstadoCobrar = false;
                    vender_por_teclado();
                }
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    DATALISTADO_PRODUCTOS_OKA.Focus();
                }
            }
            else
            {
                if (e.KeyCode == Keys.Enter && EstadoCobrar == true)
                {
                    Cobrar();
                }

            }

        }
        private void EventoCobros(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && DATALISTADO_PRODUCTOS_OKA.Visible == false)
            {
                Cobrar();
            }
        }
        private void EventosTiposbusqueda(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                ModoLectora();
            }
            if (e.KeyCode == Keys.F2)
            {
                ModoTeclado();
            }
            if (e.KeyCode == Keys.Escape)
            {
                ValidarTiposBusqueda();
            }

        }

        private void datalistadoDetalleVenta_KeyDown(object sender, KeyEventArgs e)
       {
            EventosTiposbusqueda(e);
            EventoCobros(e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {    
            Ventas_Menu_Principal.Ver_Productos_Inventario frm = new Ventas_Menu_Principal.Ver_Productos_Inventario();
            frm.ShowDialog();
        }      
     
        private void button12_Click(object sender, EventArgs e)
        {


            if (MenuStrip10.Visible == true & datalistadoDetalleVenta.RowCount > 0 )
            {
                DialogResult pregunta = MessageBox.Show("Se cerrara esta ventana y perderas la cotización", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (pregunta == DialogResult.OK)
                {
                    lblcoti.Visible = false;
                    lblcoti.Dock = DockStyle.None;
                    lblcotireferencia.Visible = false;
                    MenuStrip10.Visible = false;
                    Button22.Visible = true;
                    btEfectivo.Visible = true;
                    lblcotizador.Visible = false;
                    Limpiar_para_venta_nueva();
                    Listarproductosagregados();
                    txtbuscar.Focus();

                }
                if (pregunta == DialogResult.Cancel)
                {
                    txtbuscar.Focus();
                }

            }
            else if(MenuStrip10.Visible == true & datalistadoDetalleVenta.RowCount == 0)
            {
               
                    lblcoti.Visible = false;
                    lblcoti.Dock = DockStyle.None;
                    lblcotireferencia.Visible = false;
                    MenuStrip10.Visible = false;
                    Button22.Visible = true;
                    btEfectivo.Visible = true;
                    lblcotizador.Visible = false;
                    Limpiar_para_venta_nueva();
                    Listarproductosagregados();
                    txtbuscar.Focus();

            }          
            else if (datalistadoDetalleVenta.RowCount > 0)
            {
                DialogResult pregunta = MessageBox.Show("Pon en espera la venta antes de cerrar esta ventana", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (pregunta == DialogResult.OK)
                {
                    lblcoti.Visible = false;
                    lblcoti.Dock = DockStyle.None;
                    lblcotireferencia.Visible = false;
                    MenuStrip10.Visible = false;
                    Button22.Visible = true;
                    btEfectivo.Visible = true;
                    lblcotizador.Visible = false;
                    Limpiar_para_venta_nueva();
                    Listarproductosagregados();
                    txtbuscar.Focus();
                }
                if (pregunta == DialogResult.Cancel)
                {                 
                    txtbuscar.Focus();
                }
            }               
        
        }

        private void cotizador_Click(object sender, EventArgs e)
        {


            if (MenuStrip10.Visible == true & datalistadoDetalleVenta.RowCount > 0)
            {
                Cobrar();
                Limpiar_para_venta_nueva();

            }
        }

        private void btncotizador_Click(object sender, EventArgs e)
        {
            MenuStrip10.Visible = true;
            MenuStrip10.Focus();
            if (MenuStrip10.Visible == true & datalistadoDetalleVenta.RowCount > 0)
            {
                DialogResult pregunta = MessageBox.Show("Se cerrara esta ventana y perderas la cotización", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (pregunta == DialogResult.OK)
                {                
                 
                    lblcoti.Visible = true;
                    lblcoti.Dock = DockStyle.Fill;
                    lblcotireferencia.Visible = true;
                    MenuStrip10.Visible = true;
                    Button22.Visible = false;
                    btEfectivo.Visible = false;
                    Limpiar_para_venta_nueva();
                    Listarproductosagregados();
                    txtbuscar.Focus();

                }
                if (pregunta == DialogResult.Cancel)
                {
                    MenuStrip10.Visible = false;
                    txtbuscar.Focus();
                }


            }
            else if (MenuStrip10.Visible == true & datalistadoDetalleVenta.RowCount == 0)
            {

             
                lblcoti.Visible = true;
                lblcoti.Dock = DockStyle.Fill;
                lblcotireferencia.Visible = true;
                MenuStrip10.Visible = true;
                Button22.Visible = false;
                btEfectivo.Visible = false;
                Limpiar_para_venta_nueva();
                Listarproductosagregados();
            }
            else if(datalistadoDetalleVenta.RowCount > 0)
            {
                DialogResult pregunta = MessageBox.Show("Pon en espera la venta antes de cerrar esta ventana", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (pregunta == DialogResult.OK)
                {
                
                    lblcoti.Visible = true;
                    lblcoti.Dock = DockStyle.Fill;
                    lblcotireferencia.Visible = true;
                    MenuStrip10.Visible = true;
                    Button22.Visible = false;
                    btEfectivo.Visible = false;
                    Limpiar_para_venta_nueva();
                }
                if (pregunta == DialogResult.Cancel)
                {
                    MenuStrip10.Visible = false;
                    txtbuscar.Focus();
                }
            }

        }        //continuar para el proceso de cotizacion

        private void btEfectivo_KeyDown(object sender, KeyEventArgs e)
        {
        
        }

        private void Cobros_Click(object sender, EventArgs e)
        {
            Cobros.CobrosForm frm = new Cobros.CobrosForm();
            frm.ShowDialog();
        }

        private void PagosProveedores_Click(object sender, EventArgs e)
        {
            PagosProveedores.PagosForm frm = new PagosProveedores.PagosForm();
            frm.ShowDialog();
        }
    }
}
