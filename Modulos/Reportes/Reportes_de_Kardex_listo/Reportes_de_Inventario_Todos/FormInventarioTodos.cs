using Punto_de_venta.Modulos.Reportes_de_Kardex_listo.Reportes.Reportes_de_Inventario_Todos;
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

namespace Punto_de_venta.Modulos.Reportes.Reportes_de_Kardex_listo.Reportes_de_Inventario_Todos
{
    public partial class FormInventarioTodos : Form
    {
        public FormInventarioTodos()
        {
            InitializeComponent();
        }
        ReportInventarios_Todos rptFREPORT2 = new ReportInventarios_Todos();
        private void mostrar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDt.ConexionData.conexion;
                con.Open();

                da = new SqlDataAdapter("imprimir_inventarios_todos", con);
                da.Fill(dt);
                con.Close();
                rptFREPORT2 = new ReportInventarios_Todos();
                rptFREPORT2.DataSource = dt;
                rptFREPORT2.table1.DataSource = dt;
                reportViewer1.Report = rptFREPORT2;
                reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }      
        private void FormInventarioTodos_Load(object sender, EventArgs e)
        {
            mostrar();
        }
    }
}
