using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Punto_de_venta.Presentacion.Ventas_Menu_Principal
{
    public partial class Ver_Productos_Inventario : Form
    {
        public Ver_Productos_Inventario()
        {
            InitializeComponent();
        }
        Ventas_Menu_Princi prin = new Ventas_Menu_Princi();
        private void Ver_Productos_Inventario_Load(object sender, EventArgs e)
        {
            txtbusca.Text = "Buscar...";
        }
        private void buscar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                ConexionDt.ConexionData.conectar.Open();

                da = new SqlDataAdapter("buscar_producto_por_descripcionOK", ConexionDt.ConexionData.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@letra", txtbusca.Text);
                da.Fill(dt);
                datalistado.DataSource = dt;
                ConexionDt.ConexionData.conectar.Close();

                datalistado.Columns[2].Visible = false;
                datalistado.Columns[7].Visible = false;
                datalistado.Columns[9].Visible = false;
                datalistado.Columns[10].Visible = false;
                datalistado.Columns[13].Visible = false;
                datalistado.Columns[14].Visible = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            sumar_costo_de_inventario_CONTAR_PRODUCTOS();
            Logica.Bases.Multilinea(ref datalistado);        
        }

        private void txtbusca_TextChanged(object sender, EventArgs e)
        {
            buscar();
           
        }
        internal void sumar_costo_de_inventario_CONTAR_PRODUCTOS()
        {

            string conteoresultado;
            string querycontar;
            querycontar = "select count(Id_Producto1 ) from Producto1 ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConexionDt.ConexionData.conexion;
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

    }
}
