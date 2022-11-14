using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Management;
using System.Windows.Forms;
using Punto_de_venta.ConexionDt;
using Punto_de_venta.Logica;
using Punto_de_venta.Datos;

namespace Punto_de_venta.Presentacion
{
    public partial class LOGIN : Form
    {
        public static String inisioSecion = "INICIO";
        int contador;
        int contadorCajas;
        int contador_Movimientos_de_caja;
        public static int idusuariovariable;
        public static int idcajavariable;
        int idusuarioVerificador;
        string lblSerialPc;
        string lblSerialPcLocal;
        string cajero = "Cajero (¿Si estas autorizado para manejar dinero?)";
        string vendedor = "Solo Ventas (no esta autorizado para manejar dinero)";
        string administrador = "Administrador (Control total)";
        public static string lblRol;
        string txtlogin;
        string lblApertura_De_caja;
        string ResultadoLicencia;
        string FechaFinal;
        string Ip;
        public LOGIN()
        {
            InitializeComponent();
        }
        public void DibujarUsuario()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("select * from USUARIO2 where Estado = 'ACTIVO'", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Label b = new Label();
                    Panel p1 = new Panel();
                    PictureBox I1 = new PictureBox();

                    //propiedades programada para el label
                    b.Text = rdr["Login"].ToString();
                    b.Name = rdr["idUsuario"].ToString();
                    b.Size = new System.Drawing.Size(175, 25);
                    b.Font = new System.Drawing.Font("Microsoft Sans Serif", 13);
                    b.FlatStyle = FlatStyle.Flat;
                    b.BackColor = Color.FromArgb(32, 106, 93);
                    b.ForeColor = Color.White;
                    b.Dock = DockStyle.Bottom;
                    b.TextAlign = ContentAlignment.MiddleCenter;
                    b.Cursor = Cursors.Hand;

                    //propiedades programada para el panel
                    p1.Size = new System.Drawing.Size(155, 167);
                    p1.BorderStyle = BorderStyle.None;
                    p1.Dock = DockStyle.Bottom;
                    p1.BackColor = Color.FromArgb(32, 106, 93);

                    //propiedades programada para el picturebox
                    I1.Size = new System.Drawing.Size(175, 132);
                    I1.Dock = DockStyle.Top;
                    I1.BackgroundImage = null;
                    Byte[] bi = (Byte[])rdr["icono"];
                    MemoryStream ms = new MemoryStream(bi);
                    I1.Image = Image.FromStream(ms);
                    I1.SizeMode = PictureBoxSizeMode.Zoom;
                    I1.Tag = rdr["Login"].ToString();
                    I1.Cursor = Cursors.Hand;

                    //MOSTRAMOS EN EL PANEL
                    p1.Controls.Add(b);
                    p1.Controls.Add(I1);
                    b.BringToFront();
                    flowLayoutPanel1.Controls.Add(p1);
                    //funcion para que lleve al usuario a la opcion al darle clic                
                    b.Click += new EventHandler(mieventoLabel);
                    I1.Click += new EventHandler(mieventoImage);
                }
                con.Close();
            }
            catch(Exception ex)
            {

            }
           
        }
       
        
        private void mostrar_roles()
        {
            ConexionData.abrir();
            SqlCommand com = new SqlCommand("mostrar_permisos_por_usuario_ROL_UNICO", ConexionData.conectar);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@idusario", idusuariovariable);
            try
            {

                lblRol = Convert.ToString(com.ExecuteScalar());
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {

            }
        }
        private void mostrar_usuarios_registrados()
        {
            try
            {
                ConexionData.abrir();
                SqlCommand da = new SqlCommand("select idUsuario from USUARIO2", ConexionData.conectar);
                idusuarioVerificador = Convert.ToInt32(da.ExecuteScalar());
                ConexionData.cerrar();
                INDICADOR = "CORRECTO";
            }
            catch (Exception ex)
            {
                INDICADOR = "INCORRECTO";
                idusuarioVerificador = 0;
            }
        }      

        private void mieventoLabel(System.Object sender, EventArgs e)
        {
            //se trae el texto del label, es decir el login
            txtlogin = ((Label)sender).Text;
            PanelIngreso_de_contraseña.Visible = true;
            PanelUsuarios.Visible = false;
        
        }
        private void mieventoImage(System.Object sender, EventArgs e)
        {
            //traemos la imagen del login
            txtlogin = Convert.ToString(((PictureBox)sender).Tag);
            PanelIngreso_de_contraseña.Visible = true;
            PanelUsuarios.Visible = false;
      
        }

        private void LOGIN_Load(object sender, EventArgs e)
        {
           
            validar_conexion();
            escalar_paneles();
            Bases.Obtener_serialPC(ref lblSerialPc);
            ObtenerIpLocal();
            Obtener_datos.Obtener_id_caja_PorSerial(ref idcajavariable);
        }
        private void ObtenerIpLocal()
        {

            this.Text = Bases.ObtenerIp(ref Ip);
        }
        void escalar_paneles()
        {
            PanelUsuarios.Size = new System.Drawing.Size(1005, 649);
            PanelIngreso_de_contraseña.Size = new System.Drawing.Size(397, 654);
            PdeCarga.Size = new System.Drawing.Size(397, 654);
            PanelRestaurarCuenta.Size = new System.Drawing.Size(538, 654);
            PanelUsuarios.Location = new Point((Width - PanelUsuarios.Width) / 2, (Height - PanelUsuarios.Height) / 2);
            panel3.Visible = true;
            PanelIngreso_de_contraseña.Visible = false;
            panelRestaurarcontraseña.Dock = DockStyle.Fill;
            PdeCarga.Location = new Point((Width - PdeCarga.Width) / 2, (Height - PdeCarga.Height) / 2);
          
            //panelRestaurarcontraseña.Location = new Point((Width - panelRestaurarcontraseña.Width) / 2, (Height - panelRestaurarcontraseña.Height) / 2);
            PanelIngreso_de_contraseña.Location = new Point((Width - PanelIngreso_de_contraseña.Width) / 2, (Height - PanelIngreso_de_contraseña.Height) / 2);

        }
        private void Listarcierres_de_caja()
        {
           try
           {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();
                da = new SqlDataAdapter("MOSTRAR_MOVIMIENTOS_DE_CAJA_POR_SERIAL", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@serial", lblSerialPc);
                da.Fill(dt);
                datalistado_detalle_cierre_de_caja.DataSource = dt;
                con.Close();

           }
           catch (Exception ex)
           { 
                MessageBox.Show(ex.Message);
           }

        }
        private void txtPasswor_TextChanged_1(object sender, EventArgs e)
        {
            Iniciar_sesion_correcto();
        }
        public void contar_cierres_de_caja()
        {
            int x;
            x = datalistado_detalle_cierre_de_caja.Rows.Count;
            contadorCajas = (x);
        }
        private void aperturar_detalle_de_cierre_caja()
        {
            try
            {
                ConexionData.abrir();
                
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("insertar_DETALLE_cierre_de_caja", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fechaini", DateTime.Now);
                cmd.Parameters.AddWithValue("@fechafin", DateTime.Now);
                cmd.Parameters.AddWithValue("@fechacierre", DateTime.Now);
                cmd.Parameters.AddWithValue("@ingresos", "0.00");
                cmd.Parameters.AddWithValue("@egresos", "0.00");
                cmd.Parameters.AddWithValue("@saldo", "0.00");
                cmd.Parameters.AddWithValue("@idusuario", idusuariovariable);
                cmd.Parameters.AddWithValue("@totalcaluclado", "0.00");
                cmd.Parameters.AddWithValue("@totalreal", "0.00");

                cmd.Parameters.AddWithValue("@estado", "CAJA APERTURADA");
                cmd.Parameters.AddWithValue("@diferencia", "0.00");
                cmd.Parameters.AddWithValue("@id_caja", idcajavariable);

                cmd.ExecuteNonQuery();
                ConexionData.cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void obtener_idusuario()
        {
            try
            {
                idusuariovariable = Convert.ToInt32(dataListado.SelectedCells[1].Value);
            }
            catch
            {
            }        }
        private void Iniciar_sesion_correcto()
        {
            cargarusuarios();
            contar();

            if (contador > 0)
            {
                obtener_idusuario();
                mostrar_roles();
                if (lblRol != cajero)
                {
                    timerValidarRol.Start();
                }
                if (lblRol == cajero)
                {
                    validar_aperturas_de_caja();
                }
            }


        }
        private void obtener_usuario_que_aperturo_caja()
        {
            try
            {
                lblusuario_queinicioCaja.Text = datalistado_detalle_cierre_de_caja.SelectedCells[1].Value.ToString();
                lblnombredeCajero.Text = datalistado_detalle_cierre_de_caja.SelectedCells[2].Value.ToString();
            }
            catch
            {

            }
        }
        private void validar_aperturas_de_caja()
        {
            Listarcierres_de_caja();
            contar_cierres_de_caja();
            if (contadorCajas == 0)
            {
                aperturar_detalle_de_cierre_caja();
                lblApertura_De_caja = "Nuevo*****";
                timerValidarRol.Start();

            }
            else
            {
                mostrar_movimientos_de_caja_por_serial_y_usuario();
                contar_movimientos_de_caja_por_usuario();

                if (contador_Movimientos_de_caja == 0)
                {
                    obtener_usuario_que_aperturo_caja();
                    MessageBox.Show("Para poder continuar con el Turno de *" + lblnombredeCajero.Text + "* ,Inicia sesion con el Usuario " + lblusuario_queinicioCaja.Text + " -ó-el Usuario *admin*", "Caja Iniciada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblApertura_De_caja = "Aperturado";
                    timerValidarRol.Start();

                }
            }
        }
        private void mostrar_movimientos_de_caja_por_serial_y_usuario()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;               
                ConexionData.abrir();             

                da = new SqlDataAdapter("MOSTRAR_MOVIMIENTOS_DE_CAJA_POR_SERIAL_y_usuario", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@serial", lblSerialPc);
                da.SelectCommand.Parameters.AddWithValue("@idusuario", idusuariovariable);
                da.Fill(dt);
                datalistado_movimiento_validar.DataSource = dt;
                ConexionData.cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }
        private void contar_movimientos_de_caja_por_usuario()
        {
            int x;

            x = datalistado_movimiento_validar.Rows.Count;
            contador_Movimientos_de_caja = (x);

        }
        private void contar()
        {
            int x;

            x = dataListado.Rows.Count;
            contador = (x);

        }
        private void cargarusuarios()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;         
                ConexionData.abrir();           

                da = new SqlDataAdapter("validar_usuario", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@password", Bases.Encriptar(txtPasswor.Text));
                da.SelectCommand.Parameters.AddWithValue("@login", txtlogin);
                da.Fill(dt);
                dataListado.DataSource = dt;
               ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void mostrar_correo()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;               
                ConexionData.abrir();              
                da = new SqlDataAdapter("select Correo from USUARIO2 where Estado='ACTIVO'", ConexionData.conectar);

                da.Fill(dt);
                txtcorreo.DisplayMember = "Correo";
                txtcorreo.ValueMember = "Correo";
                txtcorreo.DataSource = dt;
               ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }                  
                
        private void btnrestaurarContraseña_Click(object sender, EventArgs e)
        {
            panelRestaurarcontraseña.Visible = true;
            mostrar_correo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelRestaurarcontraseña.Visible = false;
        }
        string clave;
        private void mostrar_usuario_por_correo()
        {
            try
            {
                //string resultado;

               ConexionData.abrir();
                SqlCommand da = new SqlCommand("buscar_USUARIO_por_correo", ConexionData.conectar);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@correo", txtcorreo.Text);                
                lblresultadocontraseña.Text = Convert.ToString(da.ExecuteScalar());
                clave = Bases.Desencriptar(lblresultadocontraseña.Text);
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnenviar_Click_1(object sender, EventArgs e)
        {
            enviarCorreo();
        }
        private void enviarCorreo()
        {
            mostrar_usuario_por_correo();
            richTextBox1.Text = richTextBox1.Text.Replace("@pass", clave);
            enviarCorreo("hamintonjair@gmail.com", "eorjdlcmzvzufiuw", richTextBox1.Text, "Solicitud de Contraseña", txtcorreo.Text, "");
        }
        private void MOSTRAR_CAJA_POR_SERIAL()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                ConexionData.abrir();              
                da = new SqlDataAdapter("mostrar_cajas_por_Serial_de_DiscoDuro", ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Serial", lblSerialPc);
                da.Fill(dt);
                datalistado_caja.DataSource = dt;
                ConexionData.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        string INDICADOR;
        private void MOSTRAR_licencia_temporal()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                ConexionData.abrir();               
                da = new SqlDataAdapter("select * from Marcan", ConexionData.conectar);
                da.Fill(dt);
                datalistado_licencia_temporal.DataSource = dt;
               ConexionData.cerrar();

            }
            catch (Exception ex)
            {

            }
        }
        int txtcontador_USUARIOS;
        private void validarLicencia()
        {
            DLicencias funcion = new DLicencias();
            funcion.ValidarLicencias(ref ResultadoLicencia, ref FechaFinal);
            if (ResultadoLicencia == "?ACTIVO?")
            {
                lblestadoLicencias.Text = "Licencia de Prueba hasta el: " + FechaFinal;
            }
            if (ResultadoLicencia == "?ACTIVADO PRO?")
            {
                lblestadoLicencias.Text = "Licencia PROFESIONAL hasta el: " + FechaFinal;
            }
            if (ResultadoLicencia == "VENCIDA")
            {
                funcion.EditarMarcanVencidas();
                Dispose();
                Licencias_Membresias.MembresiasNuevo frm = new Licencias_Membresias.MembresiasNuevo();
                frm.ShowDialog();
            }



        }
        private void validar_conexion()
        {

            mostrar_usuarios_registrados();

            if (INDICADOR == "CORRECTO")
            {
            
                if (idusuarioVerificador == 0)
                {
                    Hide();
                    Presentacion.Asistente_de_Instalacion_Servidor.REGISTRO_DE_EMPRESA frm = new Presentacion.Asistente_de_Instalacion_Servidor.REGISTRO_DE_EMPRESA();
                    frm.ShowDialog();
                    this.Dispose();

                }
                else
                {
                    validarLicencia();
                    DibujarUsuario();
                }

            }

            if (INDICADOR == "INCORRECTO")
            {
                Hide();
                Presentacion.Asistente_de_Instalacion_Servidor.Eleccion_Servidor_o_remoto frm = new Presentacion.Asistente_de_Instalacion_Servidor.Eleccion_Servidor_o_remoto();
                frm.ShowDialog();
                this.Dispose();

            }        
        }

        internal void enviarCorreo(string emisor, string password, string mensaje, string asunto, string destinatario, string ruta)
        {
            try
            {
                MailMessage correos = new MailMessage();
                SmtpClient envios = new SmtpClient();
                correos.To.Clear();
                correos.Body = "";
                correos.Subject = "";
                correos.Body = mensaje;
                correos.Subject = asunto;
                correos.IsBodyHtml = true;
                correos.To.Add((destinatario));
                correos.From = new MailAddress(emisor);
                envios.Credentials = new NetworkCredential(emisor,  password);

                envios.Host = "smtp.gmail.com";
                envios.Port = 587;
                envios.EnableSsl = true;

                envios.Send(correos);
                lblEstado_de_envio.Text = "ENVIADO";
                MessageBox.Show("Contraseña Enviada, revisa tu correo Electronico", "Restauracion de contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PanelRestaurarCuenta.Visible = false;

            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR, revisa tu correo Electronico", "Restauracion de contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblEstado_de_envio.Text = "Correo no registrado";
            }
        }
        private void btncerrar_Click(object sender, EventArgs e)
        {
            panelRestaurarcontraseña.Hide();
        }  
        private void Ingresar_por_licencia_Temporal()
        {
            lblestadoLicencias.Text = "Licencia de Prueba Activada hasta el: " + txtfecha_final_licencia_temporal.Text;

        }
        private void Ingresar_por_licencia_de_paga()
        {
            lblestadoLicencias.Text = "Licencia PROFESIONAL Activada hasta el: " + txtfecha_final_licencia_temporal.Text;
        }
    

        private void btn0_Click(object sender, EventArgs e)
        {
            txtPasswor.Text = txtPasswor.Text + "0";
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtPasswor.Text = txtPasswor.Text + "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtPasswor.Text = txtPasswor.Text + "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtPasswor.Text = txtPasswor.Text + "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtPasswor.Text = txtPasswor.Text + "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtPasswor.Text = txtPasswor.Text + "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtPasswor.Text = txtPasswor.Text + "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtPasswor.Text = txtPasswor.Text + "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtPasswor.Text = txtPasswor.Text + "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtPasswor.Text = txtPasswor.Text + "9";
        }

        private void btnborrar_Click(object sender, EventArgs e)
        {
            txtPasswor.Clear();
        }
        //metodo para reducir o eliminar parametros o caracteres según la cantidad que sele de
        public static string Mid(string param, int startIndex, int length)
        {
            string result = param.Substring(startIndex, length);
            return result;
        }
        //metodo para borrar de derecha a izquierda
        private void btnBorrarDerecha_Click(object sender, EventArgs e)
        {
            try
            {
                int largo;
                if (txtPasswor.Text != "")
                {
                    largo = txtPasswor.Text.Length;
                    txtPasswor.Text = Mid(txtPasswor.Text, 1, largo - 1);
                }
            }
            catch
            {

            }
        }

        private void tver_Click(object sender, EventArgs e)
        {
            txtPasswor.PasswordChar = '\0';
            tocultar.Visible = true;
            tver.Visible = false;
        }

        private void tocultar_Click(object sender, EventArgs e)
        {
            txtPasswor.PasswordChar = '*';
            tocultar.Visible = false;
            tver.Visible = true;
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Usuario o contraseña Incorrectos", "Datos Incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
          
               
            
        }
        private void editar_inicio_De_sesion()
        {
            try
            {
                ConexionData.abrir();
               
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("editar_inicio_De_sesion", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_serial_Pc", lblSerialPc);
                cmd.Parameters.AddWithValue("@id_usuario", idusuariovariable);
                cmd.ExecuteNonQuery();
                ConexionData.cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timerValidarRol_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                BackColor = Color.FromArgb(26, 26, 26);
                progressBar1.Value = progressBar1.Value + 10;
                progressBar1.Visible = true;
                PdeCarga.Visible = true;

            }
            else
            {
                progressBar1.Value = 0;
                timerValidarRol.Stop();
          
                if (lblRol == administrador)
                {
                    editar_inicio_De_sesion();
                    Dispose();
                    Admin_nivel_dios.DASHBOARD_PRINCIPAL frm = new Admin_nivel_dios.DASHBOARD_PRINCIPAL();
                    frm.ShowDialog();
             
                }
                else
                {

                    if (lblApertura_De_caja == "Nuevo*****" & lblRol == cajero)
                    {
                        editar_inicio_De_sesion();
                        Dispose();
                        Caja.Apertura_de_Caja frm = new Caja.Apertura_de_Caja();
                        frm.ShowDialog();
                     

                    }
                   
                    else if (lblApertura_De_caja == "Aperturado" & lblRol == cajero)
                    {
                        editar_inicio_De_sesion();
                        Dispose();
                        Ventas_Menu_Principal.Ventas_Menu_Princi frm = new Ventas_Menu_Principal.Ventas_Menu_Princi();
                        frm.ShowDialog();
                   

                    }
                   
                    else if (lblRol == vendedor)
                    {
                        editar_inicio_De_sesion();
                        Dispose();
                        Ventas_Menu_Principal.Ventas_Menu_Princi frm = new Ventas_Menu_Principal.Ventas_Menu_Princi();
                        frm.ShowDialog();         

                    }

                }
            }
        }

        private void btnCambiarUsuario_Click(object sender, EventArgs e)
        {
            PanelUsuarios.Visible = true;
            PanelIngreso_de_contraseña.Visible = false;
            txtPasswor.Clear();
        }

        private void btnOlvidoContraseña_Click(object sender, EventArgs e)
        {
            panelRestaurarcontraseña.Visible = true;
            mostrar_correo();
        }
    }
}
