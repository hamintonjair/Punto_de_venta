using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_venta.Presentacion.Configuracion
{
    public partial class PANEL_CONFIGURACIONES : Form
    {
        public PANEL_CONFIGURACIONES()
        {
            InitializeComponent();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Dispose();
            Productos.Productoss frm = new Productos.Productoss();
            frm.ShowDialog();
        }

        private void Logo_empresa_Click(object sender, EventArgs e)
        {
            Configurar_empresa();
        }
        private void Configurar_empresa()
        {
          
            Presentacion.Epresa_Configuracion.EMPRESA_CONFIG frm = new Presentacion.Epresa_Configuracion.EMPRESA_CONFIG();
            frm.ShowDialog();
        }

        private void Label47_Click(object sender, EventArgs e)
        {
            Configurar_empresa();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Usuarios();
        }
        private void Usuarios()
        {
         
            usuarios frm = new usuarios();
            frm.ShowDialog();

        }

        private void Label26_Click(object sender, EventArgs e)
        {
            Usuarios();
        }

        private void PANEL_CONFIGURACIONES_Load(object sender, EventArgs e)
        {

        }

        private void btnCajas_Click(object sender, EventArgs e)
        {
            mostrar_cajas();
        }
        private void mostrar_cajas()
        {
        
            Caja.Cajas_formu frm = new Caja.Cajas_formu();
            frm.ShowDialog();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            mostrarclientes(); 
        }
        public void mostrarclientes()
        {
         
            Clientes_Proveedores.clientes frm = new Clientes_Proveedores.clientes();
            frm.ShowDialog();
        }
        private void Button9_Click(object sender, EventArgs e)
        {
            mostrarproveedor();

        }
        public void mostrarproveedor()
        {
        
            Clientes_Proveedores.Proveedores frm = new Clientes_Proveedores.Proveedores();
            frm.ShowDialog();
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            mostrarserializacion();   
        }
        public void mostrarserializacion()
        {
        
            Serializacion_de_Comprobantes.SERIALIZACION frm = new Serializacion_de_Comprobantes.SERIALIZACION();
            frm.ShowDialog();
        }

        private void btnEnvios_a_correo_Click(object sender, EventArgs e)
        {
            correo();
        }
        public void correo()
        {
            Presentacion.CorreoBase.ConfigurarCorreo frm = new Presentacion.CorreoBase.ConfigurarCorreo();
            frm.ShowDialog();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            correo();
        }

        private void ToolStripButton22_Click(object sender, EventArgs e)
        {
            Dispose();
            Admin_nivel_dios.DASHBOARD_PRINCIPAL frm = new Admin_nivel_dios.DASHBOARD_PRINCIPAL();
            frm.ShowDialog();
        }
        private void PANEL_CONFIGURACIONES_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose();
            Admin_nivel_dios.DASHBOARD_PRINCIPAL frm = new Admin_nivel_dios.DASHBOARD_PRINCIPAL();
            frm.ShowDialog();
        }

    }
}
