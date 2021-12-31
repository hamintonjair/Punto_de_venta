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
    public partial class CANTIDAD_A_GRANEL : Form
    {
        public CANTIDAD_A_GRANEL()
        {
            InitializeComponent();
        }
        public double preciounitario;
        private void BtnCerrar_turno_Click(object sender, EventArgs e)
        {
            //Ventas_Menu_Princi.txtpantalla = Convert.ToDouble(txtcantidad.Text);
            //Dispose();
        }

        private void CANTIDAD_A_GRANEL_Load(object sender, EventArgs e)
        {
            txtprecio_unitario.Text = Convert.ToString(preciounitario);
        }

        private void txtcantidad_TextChanged(object sender, EventArgs e)
        {
            calcularTotal();
        }
        private void calcularTotal()
        {
            try
            {
                double total;
                double cantidad;
                cantidad = Convert.ToDouble(txtcantidad.Text);
                total = preciounitario * cantidad;
                txttotal.Text = Convert.ToString(total);
            }
            catch (Exception)
            {

            }

        }
    }
}
