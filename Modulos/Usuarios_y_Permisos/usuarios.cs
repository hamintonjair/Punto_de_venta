using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Punto_de_venta
{
    public partial class usuarios : Form
    {
        public usuarios()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwmd, int wmsg, int wparam, int lparam);

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
        //metodo para que no se repitan la seleccion de los icono
        public void Cargar_estado_de_iconos()
        {
            try
            {
                foreach(DataGridViewRow row in dataListado.Rows)
                {
                    try
                    {
                        string icono = Convert.ToString(row.Cells["Nombre_de_icono"].Value);

                        if (icono == "1")
                        {
                            pictureBox3.Visible = false;
                        }
                        else if (icono == "2")
                        {
                            pictureBox4.Visible = false;
                        }
                        else if (icono == "3")
                        {
                            pictureBox5.Visible = false;
                        }
                        else if (icono == "4")
                        {
                            pictureBox6.Visible = false;
                        }
                        else if (icono == "5")
                        {
                            pictureBox7.Visible = false;
                        }
                        else if (icono == "6")
                        {
                            pictureBox8.Visible = false;
                        }
                        else if (icono == "7")
                        {
                            pictureBox9.Visible = false;
                        }
                        else if (icono == "8")
                        {
                            pictureBox10.Visible = false;
                        }
                    

                    }catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //metodo para validar el formato de correo
        public bool validar_Mail(string sMail)
        {
            return Regex.IsMatch(sMail, @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (validar_Mail(txtCorreo.Text) == false)
            {
                MessageBox.Show("Dirección de correo electronico no valido, el correo debe tener el formato: nombre@dominio.com, " + "por favor selecciona un correo valido", "Validación de correo electronico", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCorreo.Focus();
                txtCorreo.SelectAll();
            }
            else
            {
                if(txtNombre.Text !="")
                {
                    if(cbRol.Text !="")
                    {
                        if(lblAnuncioIcono.Visible == false)
                        {
                             try
                             {
                                    SqlConnection con = new SqlConnection();
                                    con.ConnectionString = ConexionDt.ConexionData.conexion;
                                    con.Open();
                                    SqlCommand cmd = new SqlCommand();
                                    cmd = new SqlCommand("insertar_usuario", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@nombres", txtNombre.Text);
                                    cmd.Parameters.AddWithValue("@Login", txtUsuario.Text);
                                    cmd.Parameters.AddWithValue("@Password", txtContraseña.Text);
                                    cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                                    cmd.Parameters.AddWithValue("@Rol", cbRol.Text);
                                    //nos permiter guardar las imagenes y mopstarlas en la sesion del icono
                                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                                    Icono.Image.Save(ms, Icono.Image.RawFormat);
                                    cmd.Parameters.AddWithValue("@Icono", ms.GetBuffer());
                                    cmd.Parameters.AddWithValue("@Nombre_de_icono",      lblNumeroIcono.Text);
                                    cmd.Parameters.AddWithValue("@Estado", "ACTIVO");
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                    mostrar();
                                    panel4.Visible = false;
                             }
                             catch (Exception ex)
                             {
                                    MessageBox.Show(ex.Message);
                             }
                            
                        }
                        else
                        {
                            MessageBox.Show("Elija un icono", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Elija el Rol", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Asegúrese de haber llenado todos los campos para el registero", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //metodo para guardar el registro
               
            }
        }
        private void mostrar()
        {
            //metodo para que nos muestre en e dtagrid
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();
                da = new SqlDataAdapter("mostrar_usuario", con);
                da.Fill(dt);
                dataListado.DataSource = dt;
                con.Close();
                dataListado.Columns[1].Visible = false;
                dataListado.Columns[5].Visible = false;
                dataListado.Columns[6].Visible = false;
                dataListado.Columns[7].Visible = false;
                dataListado.Columns[8].Visible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ConexionDt.Tamaño_automatico_de_datatables.Multilinea(ref dataListado);
           
        }
        //funciones para que nos muestre la imagen selecionada en el pictubox
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox3.Image;
            lblNumeroIcono.Text = "1";
            lblAnuncioIcono.Visible = false;
            PanelICONO.Visible = false;
        }

        private void lblAnuncioIcono_Click(object sender, EventArgs e)
        {
            Cargar_estado_de_iconos();
            PanelICONO.Visible = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox4.Image;
            lblNumeroIcono.Text = "2";
            lblAnuncioIcono.Visible = false;
            PanelICONO.Visible = false;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox5.Image;
            lblNumeroIcono.Text = "3";
            lblAnuncioIcono.Visible = false;
            PanelICONO.Visible = false;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox6.Image;
            lblNumeroIcono.Text = "4";
            lblAnuncioIcono.Visible = false;
            PanelICONO.Visible = false;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox7.Image;
            lblNumeroIcono.Text = "5";
            lblAnuncioIcono.Visible = false;
            PanelICONO.Visible = false;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox8.Image;
            lblNumeroIcono.Text = "6";
            lblAnuncioIcono.Visible = false;
            PanelICONO.Visible = false;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox9.Image;
            lblNumeroIcono.Text = "7";
            lblAnuncioIcono.Visible = false;
            PanelICONO.Visible = false;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox10.Image;
            lblNumeroIcono.Text = "8";
            lblAnuncioIcono.Visible = false;
            PanelICONO.Visible = false;
        }

        private void usuarios_Load(object sender, EventArgs e)
        {
            panel4.Visible = false;
            PanelICONO.Visible = false;
            mostrar();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            lblAnuncioIcono.Visible = true;
            txtNombre.Text = "";
            txtContraseña.Text = "";
            txtUsuario.Text = "";
            txtCorreo.Text = "";
            btnguardar.Visible = true;
            btnguardarcambios.Visible = false;
            
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblIdiUsuario.Text = dataListado.SelectedCells[1].Value.ToString();
            txtNombre.Text = dataListado.SelectedCells[2].Value.ToString();
            txtUsuario.Text = dataListado.SelectedCells[3].Value.ToString();
            txtContraseña.Text = dataListado.SelectedCells[4].Value.ToString();
            //para que nos muestre la imagen o icono
            Icono.BackgroundImage = null;
            byte[] b = (Byte[])dataListado.SelectedCells[5].Value;
            MemoryStream ms = new MemoryStream(b);
            Icono.Image = Image.FromStream(ms);
            
            lblAnuncioIcono.Visible = false;
            lblNumeroIcono.Text = dataListado.SelectedCells[6].Value.ToString();           
            txtCorreo.Text = dataListado.SelectedCells[7].Value.ToString();
            cbRol.Text = dataListado.SelectedCells[8].Value.ToString();
            panel4.Visible = true;
            btnguardar.Visible = false;
            btnguardarcambios.Visible = true;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void btnguardarcambios_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "")
            {
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConexionDt.ConexionData.conexion;
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("editar_usuario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idUsuario", lblIdiUsuario.Text);
                    cmd.Parameters.AddWithValue("@nombres", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@Login", txtUsuario.Text);
                    cmd.Parameters.AddWithValue("@Password", txtContraseña.Text);
                    cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("@Rol", cbRol.Text);
                    //nos permiter guardar las imagenes y mopstarlas en la sesion del icono
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    Icono.Image.Save(ms, Icono.Image.RawFormat);
                    cmd.Parameters.AddWithValue("@Icono", ms.GetBuffer());
                    cmd.Parameters.AddWithValue("@Nombre_de_icono", lblAnuncioIcono.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    mostrar();
                    panel4.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Icono_Click(object sender, EventArgs e)
        {
            Cargar_estado_de_iconos();
            PanelICONO.Visible = true;
        }

        private void dataListado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==this.dataListado.Columns["Eli"].Index)
            {
                //le mandamos un sms si desea eliminar realmente ese registro
                DialogResult result;
               result= MessageBox.Show("¿Realmente desea eliminar este Usuario?", "Eliminando resgistro", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if(result ==DialogResult.OK)
                {
                    SqlCommand cmd;
                    try
                    {
                        foreach(DataGridViewRow row in dataListado.SelectedRows)
                        {
                            //se traen los datos y se almacenan en la variables
                            int onekey = Convert.ToInt32(row.Cells["idUsuario"].Value);
                            string usuario = Convert.ToString(row.Cells["Login"].Value);
                            try
                            {
                                try
                                {
                                    SqlConnection con = new SqlConnection();
                                    con.ConnectionString = ConexionDt.ConexionData.conexion;
                                    con.Open();                                
                                    cmd = new SqlCommand("eliminar_usuario", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@idusuario", onekey);
                                    cmd.Parameters.AddWithValue("@Login", usuario);
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }catch(Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }

                            }catch(Exception ex)
                            {
                               MessageBox.Show(ex.Message);
                            }
                        }
                        mostrar();

                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
           
        }
        //para buscar la imagen del equipo 
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            dlg.InitialDirectory = "";
            dlg.Filter = "Imagenes|*.jpg;*.png";
            dlg.FilterIndex = 2;
            dlg.Title = "Cargador de Imagenes Sistema contable";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Icono.BackgroundImage = null;
                Icono.Image = new Bitmap(dlg.FileName);
                Icono.SizeMode = PictureBoxSizeMode.Zoom;
                lblNumeroIcono.Text = Path.GetFileName(dlg.FileName);
                lblAnuncioIcono.Visible = false;
                PanelICONO.Visible = false;
            }
        }
        private void buscar_usuario()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();

                da = new SqlDataAdapter("buscar_usuario", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", txtbuscar.Text);
                da.Fill(dt);
                dataListado.DataSource = dt;
                con.Close();

                dataListado.Columns[1].Visible = false;
                dataListado.Columns[5].Visible = false;
                dataListado.Columns[6].Visible = false;
                dataListado.Columns[7].Visible = false;
                dataListado.Columns[8].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            ConexionDt.Tamaño_automatico_de_datatables.Multilinea(ref dataListado);

        }
        //ME PERMITE INGRESAR SOLO NUMEROS
        public void Numeros(System.Windows.Forms.TextBox CajaTexto, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            buscar_usuario();
            txtbuscar.Focus();
        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            Numeros(txtbuscar, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
} 
