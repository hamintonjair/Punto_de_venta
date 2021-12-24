using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_venta.Modulos.Configuracion
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

            Modulos.Epresa_Configuracion.EMPRESA_CONFIG frm = new Modulos.Epresa_Configuracion.EMPRESA_CONFIG();
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
    }
}
