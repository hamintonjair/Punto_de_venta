﻿using Punto_de_venta.Datos;
using Punto_de_venta.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_venta.Presentacion.Clientes_Proveedores
{
    public partial class clientes : Form
    {
            
        public clientes()
        {
            InitializeComponent();
        }
        int idcliente;
        string estado;
        int CantClientes;
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtnombre.Text))
            {
                rellenarCamposVacios();
                insertar();
            }
            else
            {
                MessageBox.Show("Ingrese un nombre", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            limpiar();
        }
        private void insertar()
        {
            Lclientes parametros = new Lclientes();
            Insertar_datos funcion = new Insertar_datos();
            parametros.Nombre = txtnombre.Text;
            parametros.IdentificadorFiscal = txtIdentificador.Text;
            parametros.Celular = txtcelular.Text;
            parametros.Direccion = txtdireccion.Text;
            if (funcion.insertar_clientes(parametros) == true)
            {
                mostrar();
            }

        }
        private void rellenarCamposVacios()
        {
            if (string.IsNullOrEmpty(txtcelular.Text)) { txtcelular.Text = "-"; };
            if (string.IsNullOrEmpty(txtdireccion.Text)) { txtdireccion.Text = "-"; };
            if (string.IsNullOrEmpty(txtIdentificador.Text)) { txtIdentificador.Text = "-"; };

        }
        private void pintarDatalistado()
        {
            Bases.Multilinea2(ref datalistado);
            datalistado.Columns[2].Visible = false;
            foreach (DataGridViewRow row in datalistado.Rows)
            {
                string estado = Convert.ToString(row.Cells["Estado"].Value);
                if (estado == "ELIMINADO")
                {
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Strikeout | FontStyle.Bold);
                    row.DefaultCellStyle.ForeColor = Color.Red;
                }

            }
        }
        private void editar()
        {
            Lclientes parametros = new Lclientes();
            Editar_datos funcion = new Editar_datos();
            parametros.idcliente = idcliente;
            parametros.Nombre = txtnombre.Text;
            parametros.IdentificadorFiscal = txtIdentificador.Text;
            parametros.Celular = txtcelular.Text;
            parametros.Direccion = txtdireccion.Text;
            if (funcion.editar_clientes(parametros) == true)
            {
                mostrar();
            }
        }
        private void eliminar()
        {
            try
            {
                Lclientes parametros = new Lclientes();
                Eliminar_datos funcion = new Eliminar_datos();
                parametros.idcliente = idcliente;
                if (funcion.eliminar_clientes(parametros) == true)
                {
                    mostrar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }
        private void restaurar()
        {
            Lclientes parametros = new Lclientes();
            Editar_datos funcion = new Editar_datos();
            parametros.idcliente = idcliente;
            if (funcion.restaurar_clientes(parametros) == true)
            {
                mostrar();
            }

        }
        private void buscar()
        {
            DataTable dt = new DataTable();
            Obtener_datos.buscar_clientes(ref dt, txtbusca.Text);
            datalistado.DataSource = dt;
            pintarDatalistado();
        }
        private void limpiar()
        {           
            txtnombre.Clear();
            txtIdentificador.Clear();
            txtcelular.Clear();
            txtdireccion.Clear();
            txtnombre.Focus();
            btnGuardar.Visible = true;
            btnGuardarCambios.Visible = false;
        }     
    

        private void datalistado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datalistado.Columns["Editar"].Index)
            {
                obtenerDatos();
            }
            if (e.ColumnIndex == datalistado.Columns["Eliminar"].Index)
            {
                obtenerId_estado();
                if (estado == "ACTIVO")
                {
                    DialogResult result = MessageBox.Show("¿Realmente desea eliminar este Registro?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        eliminar();
                    }
                }
            }
            ReporteCantClientes();
            ReporteCantClientesEliminados();

        }
        private void obtenerId_estado()
        {
            try
            {
                idcliente = Convert.ToInt32(datalistado.SelectedCells[2].Value);
                estado = datalistado.SelectedCells[7].Value.ToString();

            }
            catch (Exception)
            {

            }
        }
        private void obtenerDatos()
        {
            try
            {
                idcliente = Convert.ToInt32(datalistado.SelectedCells[2].Value.ToString());
                txtnombre.Text = datalistado.SelectedCells[3].Value.ToString();
                txtdireccion.Text = datalistado.SelectedCells[4].Value.ToString();
                txtIdentificador.Text = datalistado.SelectedCells[5].Value.ToString();
                txtcelular.Text = datalistado.SelectedCells[6].Value.ToString();
                //Panelregistro.Visible = true;
                btnGuardarCambios.Visible = true;
                btnGuardar.Visible = false;
                //Panelregistro.Dock = DockStyle.Fill;
                estado = datalistado.SelectedCells[7].Value.ToString();
                if (estado == "ELIMINADO")
                {
                    DialogResult result = MessageBox.Show("Este Cliente se Elimino. ¿Desea Volver a Habilitarlo?", "Restaurando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        restaurar();
                        prepararEdicion();
                    }
                }
                else
                {
                    prepararEdicion();
                }
            }
            catch (Exception ex)
            {
                 MessageBox.Show("No se pudo completar el proceso", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);;
            }
        }
        private void prepararEdicion()
        {
            Panelregistro.Visible = true;
            Panelregistro.Dock = DockStyle.Left;
            btnGuardar.Visible = false;
            btnGuardarCambios.Visible = true;
        }

        private void mostrar()
        {
            
                DataTable dt = new DataTable();
                Obtener_datos.mostrar_clientes(ref dt);
                datalistado.DataSource = dt;
                Panelregistro.Visible = true;             
                pintarDatalistado();
            
          

        }

        private void clientes_Load(object sender, EventArgs e)
        {
            mostrar();
            Panelregistro.Visible = true;         
            Panelregistro.Dock = DockStyle.Left;
            ReporteCantClientes();
            ReporteCantClientesEliminados();

            txtbusca.Clear();
        }
        private void ReporteCantClientes()
        {
            Obtener_datos.ReportCantClientes(ref CantClientes);
            lblclientesActivos.Text = CantClientes.ToString();
        }
        private void ReporteCantClientesEliminados()
        {
            Obtener_datos.ReporteCantClientesEliminado(ref CantClientes);
            lblclientesEliminados.Text = CantClientes.ToString();
        }

        private void datalistado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            obtenerDatos();
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtnombre.Text))
            {
                rellenarCamposVacios();
                editar();
            }
            else
            {
                MessageBox.Show("Ingrese un nombre", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            limpiar();
        }

        private void txtbusca_TextChanged(object sender, EventArgs e)
        {
            buscar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnGuardar.Visible = true;
            btnGuardarCambios.Visible = false;
        }
    }
}
