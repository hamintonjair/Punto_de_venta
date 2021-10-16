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

namespace Punto_de_venta.Modulos
{
    public partial class LOGIN : Form
    {
        int contador;
        int contadorCajas;
        int contador_Movimientos_de_caja;
        public static String idusuariovariable;
        public static String idcajavariable;
        public LOGIN()
        {
            InitializeComponent();
        }
        public void DibujarUsuario()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConexionDt.ConexionData.conexion;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("select * from Usuario where Estado = 'ACTIVO'", con);
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
                b.BackColor = Color.FromArgb(20, 20, 20);
                b.ForeColor = Color.White;
                b.Dock = DockStyle.Bottom;
                b.TextAlign = ContentAlignment.MiddleCenter;
                b.Cursor = Cursors.Hand;

                //propiedades programada para el panel
                p1.Size = new System.Drawing.Size(155, 167);
                p1.BorderStyle = BorderStyle.None;
                p1.Dock = DockStyle.Bottom;
                p1.BackColor = Color.FromArgb(20, 20, 20);

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
        private void MOSTRAR_PERMISOS()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConexionDt.ConexionData.conexion;

            SqlCommand com = new SqlCommand(" mostrar_permisos_por_usuario_Rol_Unico", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@LOGIN", txtlogin.Text);
            string importe;
            try
            {
                con.Open();
                importe = Convert.ToString(com.ExecuteScalar());
                con.Close();
                lblRol.Text = importe;

            }
            catch (Exception ex)
            {

            }
        }
       
        private void mieventoLabel(System.Object sender, EventArgs e)
        {
            //se trae el texto del label, es decir el login
            txtlogin.Text = ((Label)sender).Text;
            panel2.Visible = true;
            panel1.Visible = false;
            MOSTRAR_PERMISOS();
        }
        private void mieventoImage(System.Object sender, EventArgs e)
        {
            //traemos la imagen del login
            txtlogin.Text = ((PictureBox)sender).Tag.ToString();
            panel2.Visible = true;
            panel1.Visible = false;
            MOSTRAR_PERMISOS();
        }

        private void LOGIN_Load(object sender, EventArgs e)
        {
            DibujarUsuario();
            panel3.Visible = true;
            panel2.Visible = false;
            timer1.Start();
            PictureBox2.Location = new Point((Width - PictureBox2.Width) / 2, (Height - PictureBox2.Height) / 2);
            panel1.Location = new Point((Width - panel1.Width) / 2, (Height - panel1.Height) / 2);
            panelRestaurarcontraseña.Location = new Point((Width - panelRestaurarcontraseña.Width) / 2, (Height - panelRestaurarcontraseña.Height) / 2);
            panel2.Location = new Point((Width - panel2.Width) / 2, (Height - panel2.Height) / 2);
        }
        private void ListarApertura_de_detallles_de_cierre_de_caja()
        {
           try
           {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();
                da = new SqlDataAdapter("mostrar_movimientos_de_Caja_por_Serial", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@serial", lblSerialPc.Text);
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
        public void contar_aperturas_de_dertalles_de_cierre_de_caja()
        {
            int x;
            x = datalistado_detalle_cierre_de_caja.Rows.Count;
            contadorCajas = (x);
        }
        private void aperturar_detalle_de_cierre_caja()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("insertar_DETALLE_cierre_de_caja", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fechaini", DateTime.Today);
                cmd.Parameters.AddWithValue("@fechafin", DateTime.Today);
                cmd.Parameters.AddWithValue("@fechacierre", DateTime.Today);
                cmd.Parameters.AddWithValue("@ingresos", "0.00");
                cmd.Parameters.AddWithValue("@egresos", "0.00");
                cmd.Parameters.AddWithValue("@saldo", "0.00");
                cmd.Parameters.AddWithValue("@idusuario", IDUSUARIO.Text);
                cmd.Parameters.AddWithValue("@totalcalculado", "0.00");
                cmd.Parameters.AddWithValue("@totalreal", "0.00");

                cmd.Parameters.AddWithValue("@estado", "CAJA APERTURADA");
                cmd.Parameters.AddWithValue("@diferencia", "0.00");
                cmd.Parameters.AddWithValue("@idcaja", txtidcaja.Text);

                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Iniciar_sesion_correcto()
        {
            cargar_usuarios();
            contar();
            try
            {
                IDUSUARIO.Text = dataListado.SelectedCells[1].Value.ToString();
                txtnombre.Text = dataListado.SelectedCells[2].Value.ToString();
            }
            catch
            {

            }
            if (contador > 0)
            {
                ListarApertura_de_detallles_de_cierre_de_caja();
                contar_aperturas_de_dertalles_de_cierre_de_caja();
                if (contadorCajas == 0 & lblRol.Text != "Solo Ventas (no esta autorizado para manejar dinero)")
                {
                   aperturar_detalle_de_cierre_caja();
                   lblApertura_De_caja.Text = "Nuevo*****";
                   timer2.Start();
                }
                else
                {
                   if (lblRol.Text != "Solo Ventas (no esta autorizado para manejar dinero)")
                   {
                       Mostrar_movimientos_de_caja__por_serial_y_usuario();
                       contar_mostrar_movimientos_de_Caja_por_Serial_y_usuario();
                       try
                       {
                          lblusuario_queinicioCaja.Text = datalistado_detalle_cierre_de_caja.SelectedCells[1].Value.ToString();
                          lblnombredeCajero.Text = datalistado_detalle_cierre_de_caja.SelectedCells[2].Value.ToString();
                       }
                       catch
                       {

                       }
                       if (contador_Movimientos_de_caja == 0)
                       {
                                //se valida que usuario inicio caja
                          if (lblusuario_queinicioCaja.Text != "admin" & txtlogin.Text == "admin")
                          {
                               MessageBox.Show("Continuaras Turno de ''" + lblnombredeCajero.Text + "'' Todos los Registros seran con ese Usuario", "Caja Iniciada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    lblpermisodeCaja.Text = "correcto";
                          }
                          if (lblusuario_queinicioCaja.Text == "admin" & txtlogin.Text == "admin")
                          {
                              lblpermisodeCaja.Text = "correcto";
                          }
                          else if (lblusuario_queinicioCaja.Text != txtlogin.Text)
                          {
                             MessageBox.Show("Para poder continuar con el Turno de ''" + lblnombredeCajero.Text + "'', Inicia sesion con el Usuario " + lblusuario_queinicioCaja.Text + " ó el Usuario ''Admin''", "Caja Iniciada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    lblpermisodeCaja.Text = "vacio";

                          }
                          else if (lblusuario_queinicioCaja.Text == txtlogin.Text)
                          {
                            lblpermisodeCaja.Text = "correcto";
                          }
                       }
                       else
                       {
                         lblpermisodeCaja.Text = "correcto";
                       }                        
                       if (lblpermisodeCaja.Text == "correcto")
                       {
                         lblApertura_De_caja.Text = "Aperturado";
                         timer2.Start();
                       }
                   }
                   else
                   {
                       timer2.Start();
                   }
                }                
            }
        }
    
        private void Mostrar_movimientos_de_caja__por_serial_y_usuario()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();

                da = new SqlDataAdapter("mostrar_movimientos_de_Caja_por_Serial_y_usuario", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@serial", lblSerialPc.Text);
                da.SelectCommand.Parameters.AddWithValue("@idusuario", IDUSUARIO.Text);
                da.Fill(dt);
                datalistado_movimiento_validar.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        public void contar_mostrar_movimientos_de_Caja_por_Serial_y_usuario()
        {
            int x;
            x = datalistado_movimiento_validar.Rows.Count;
            contador_Movimientos_de_caja = (x);
        }
        public void contar()
        {
            int x;
            x = dataListado.Rows.Count;
            contador = (x);
        }
        private void cargar_usuarios()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();

                da = new SqlDataAdapter("validar_usuario", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@password", txtPasswor.Text);
                da.SelectCommand.Parameters.AddWithValue("@login", txtlogin.Text);
                da.Fill(dt);
                dataListado.DataSource = dt;
                con.Close();
               

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
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();

                da = new SqlDataAdapter("select Correo from usuario where Estado='ACTIVO'", con);
              
                da.Fill(dt);
                txtcorreo.DisplayMember = "Correo";
                txtcorreo.ValueMember = "Correo";
                txtcorreo.DataSource = dt;
                con.Close();
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
        private void mostrar_usuario_por_correo()
        {
            try
            {
                string resultado;
               
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;         

                SqlCommand da = new SqlCommand("buscar_usuario_por_correo", con);
                da.CommandType = CommandType.StoredProcedure;
                da.Parameters.AddWithValue("@correo", txtcorreo.Text);
                con.Open();
                lblresultadocontraseña.Text = Convert.ToString(da.ExecuteScalar());
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnenviar_Click_1(object sender, EventArgs e)
        {
            mostrar_usuario_por_correo();
            richTextBox1.Text = richTextBox1.Text.Replace("@pass", lblresultadocontraseña.Text);
            enviarCorreo("jairjomena@gmail.com", "Johanjair01", richTextBox1.Text, "Solicitud de Contraseña", txtcorreo.Text, "");
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
                envios.Credentials = new NetworkCredential(emisor, password);

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
        private void MOSTRAR_CAJA_POR_SERIAL()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();
                da = new SqlDataAdapter("mostrar_cajas_por_Serial_de_DiscoDuro", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Serial", lblSerialPc.Text);
                da.Fill(dt);
                datalistado_caja.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }
        //metodo para selecionar el serial del equipo ya que la caja se registra es cada equipo
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            try
            {
                ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * From Win32_BaseBoard");
                foreach (ManagementObject getserial in MOS.Get())
                {
                    lblSerialPc.Text = getserial.Properties["SerialNumber"].Value.ToString();

                    MOSTRAR_CAJA_POR_SERIAL();
                    try
                    {
                        txtidcaja.Text = datalistado_caja.SelectedCells[1].Value.ToString();
                        lblcaja.Text = datalistado_caja.SelectedCells[2].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
            if (progressBar1.Value < 100)
            {
                BackColor = Color.FromArgb(26, 26, 26);
                progressBar1.Value = progressBar1.Value + 10;
                progressBar1.Visible = true;
                PictureBox2.Visible = true;
                

            }
            else
            {
                progressBar1.Value = 0;
                timer2.Stop();
                if (lblApertura_De_caja.Text == "Nuevo*****" & lblRol.Text != "Solo Ventas (no esta autorizado para manejar dinero)")
                {
                    this.Hide();
                    Caja.Apertura_de_Caja frm = new Caja.Apertura_de_Caja();
                    frm.ShowDialog();
                    this.Hide();
                }
                else
                {
                    this.Hide();
                    Ventas_Menu_Principal.Ventas_Menu_Princi frm = new Ventas_Menu_Principal.Ventas_Menu_Princi();
                    frm.ShowDialog();
                    this.Hide();

                }
            }
        }
    }
}
