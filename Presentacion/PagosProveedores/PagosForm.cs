using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Punto_de_venta.Datos;
using Punto_de_venta.Logica;
using System.Windows.Forms;

namespace Punto_de_venta.Presentacion.PagosProveedores
{
    public partial class PagosForm : Form
    {
        public PagosForm()
        {
            InitializeComponent();
        }
        public static int idproveedores;
        public static double pago;
        public static string estado;
        private static double total = 0;

        string administrador = "Administrador (Control total)";
        private void txtclientesolicitante_TextChanged(object sender, EventArgs e)
        {
            buscar();
        }
        private void buscar()
        {
            DataTable dt = new DataTable();
            Obtener_datos.buscar_Proveedores(ref dt, txtclientesolicitante.Text);
            datalistadoProveedores.DataSource = dt;
            datalistadoProveedores.Columns[0].Visible = false;

            datalistadoProveedores.Columns[1].Visible = false;
            datalistadoProveedores.Columns[4].Visible = false;
            datalistadoProveedores.Columns[5].Visible = false;
            datalistadoProveedores.Columns[6].Visible = false;
            datalistadoProveedores.Columns[7].Visible = false;
            datalistadoProveedores.Columns[3].Visible = false;
            datalistadoProveedores.Columns[2].Width = datalistadoProveedores.Width;
            datalistadoProveedores.BringToFront();
            datalistadoProveedores.Visible = true;
            datalistadoProveedores.Location = new Point(panelRegistros.Location.X, panelRegistros.Location.Y);
            datalistadoProveedores.Size = new Size(290, 162);
            panelRegistros.Visible = false;
        }
        public void BuacarPagos()
        {
            //Obtener_datos.buscarPagos(ref monto, idproveedores);
        }

        private void datalistadoProveedores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            idproveedores = (int)datalistadoProveedores.SelectedCells[1].Value;
            txtclientesolicitante.Text = datalistadoProveedores.SelectedCells[2].Value.ToString();
            obtenerSaldo();
            panelHistorial.Dock = DockStyle.Fill;
            datalistadoProveedores.Visible = false;
            panelRegistros.Visible = true;
            mostrarEstadosPagosProveedores();
        }
        private void mostrarEstadosPagosProveedores()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrarEstadosPagoProveedores(ref dt, idproveedores);
            Obtener_datos.mostrarAbonosPagoProveedores(ref dt, idproveedores);
            datalistadoHistorial.DataSource = dt;
            Bases estilo = new Bases();
            estilo.MultilineaCobros(ref datalistadoHistorial);

            panelH.Visible = true;
            panelM.Visible = false;
            panelHistorial.Visible = true;
            //panelHistorial.Dock = DockStyle.Fill;
            panelMovimiento.Visible = false;
            panelMovimiento.Dock = DockStyle.None;
        }
        private void obtenerSaldo()
        {
            txttotal_saldo.Text = datalistadoProveedores.SelectedCells[7].Value.ToString();
            pago = Convert.ToDouble(datalistadoProveedores.SelectedCells[7].Value);
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            mostrarEstadosPagosProveedores();
        }       

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            mostrarControlPagos();
        }
        private void mostrarControlPagos()
        {
            DataTable dt = new DataTable();
            Obtener_datos.mostrar_ControlPagos(ref dt, idproveedores);
            datalistadoMovimientos.DataSource = dt;
            Bases estilo = new Bases();
            estilo.MultilineaCobros2(ref datalistadoMovimientos);
            datalistadoMovimientos.Columns[1].Visible = true;
            datalistadoMovimientos.Columns[5].Visible = false;
            datalistadoMovimientos.Columns[6].Visible = true;
            datalistadoMovimientos.Columns[7].Visible = true;


            panelH.Visible = false;
            panelM.Visible = true;
            panelHistorial.Visible = false;
            panelMovimiento.Visible = true;
            panelMovimiento.Dock = DockStyle.Fill;
            panelHistorial.Dock = DockStyle.None;
        }

        private void datalistadoMovimientos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void EliminarPago()
        {
            //double monto;
            //monto = Convert.ToDouble(datalistadoMovimientos.SelectedCells[2].Value);
            Lproveedores parametros = new Lproveedores();
            Editar_datos funcion = new Editar_datos();
            parametros.IdProveedor = idproveedores;
            eliminarControlPagos();
         

        }
       
        private void eliminarControlPagos()
        {

            Lcontrolpagos parametros = new Lcontrolpagos();
            Eliminar_datos funcion = new Eliminar_datos();        
            parametros.IdcontrolPago = Convert.ToInt32(datalistadoMovimientos.SelectedCells[1].Value);
            if (funcion.eliminarControlPago(parametros) == true)
            {
                buscar();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (pago > 0)
            {
                MediosPagos frm = new MediosPagos();
                frm.FormClosing += Frm_FormClosing;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("El saldo del cliente actual es 0");
            }
        }
        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            buscar();
            obtenerSaldo();
            mostrarControlPagos();
        }

        private void button1_Click(object sender, EventArgs e)
        {

                if (LOGIN.lblRol == administrador)
                {
                    DialogResult result = MessageBox.Show("¿Realmente deseas Deshacer esta Abono?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.OK)
                    {

                        if (total == pago)
                        {
                            eliminarControlPagos();
                        }
                        else
                        {
                            MessageBox.Show("No puedes eliminar el registro porque tienes deudas por pagar", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }


                    }
                }
                else
                {
                    MessageBox.Show("¡Solo el administrador puede eliminar estos registros", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

            
        }
    }
}
