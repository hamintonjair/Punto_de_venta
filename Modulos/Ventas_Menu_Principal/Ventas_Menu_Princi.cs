using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_venta.Modulos.Ventas_Menu_Principal
{
    public partial class Ventas_Menu_Princi : Form
    {
        public Ventas_Menu_Princi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Caja.Cierre_de_Caja frm = new Caja.Cierre_de_Caja();
            frm.ShowDialog();
        }

        private void iToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
