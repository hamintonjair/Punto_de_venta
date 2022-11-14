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
    public partial class MediosPagos : Form
    {
        public MediosPagos()
        {
            InitializeComponent();
        }
        double saldo;
        int idproveedor;
        int idcaja;
        int idusuario;
        //
        public static  double debo;
        double efectivo;
        double tarjeta;
        double vuelto;
        double restante;
        double efectivoCalculado;
        double montoabonado;
        private void button15_Click(object sender, EventArgs e)
        {
            montoabonado = efectivoCalculado /*+ tarjeta*/;
            if (montoabonado > 0)
            {
                insertarControlCobro();
                disminuirSaldocliente();
            }
            else
            {
                MessageBox.Show("Especifique un monto a abonar");
            }
        }
        private void insertarControlCobro()
        {
            Lcontrolpagos parametros = new Lcontrolpagos();
            Lcontrolpagos parametro = new Lcontrolpagos();
            Insertar_datos funcion = new Insertar_datos();
            Editar_datos funcions = new Editar_datos();
            parametros.Monto = debo/* + tarjeta*/;
            parametros.Fecha = DateTime.Now;
            parametro.Fecha = DateTime.Now;
            parametros.Detalle = "Pago a proveedor";
            parametro.Detalle = "Pago a proveedor";
            parametros.IdProveedor = idproveedor;
            parametro.IdProveedor = idproveedor;
            parametros.IdUsuario = idusuario;
            parametros.Comprobante = "-";
            parametros.efectivo = efectivoCalculado;
            parametro.efectivo = efectivoCalculado;
            parametros.tarjeta = tarjeta;
            if (funcion.Insertar_ControlPagos(parametros) == true)
            {
                if(funcions.InsertarControlPorPagar(parametro) == true)
                {
                  Dispose();
                }
             
            }
        }
        private void disminuirSaldocliente()
        {
            Lproveedores parametros = new Lproveedores();
            Editar_datos funcion = new Editar_datos();
            parametros.IdProveedor = idproveedor;
            funcion.disminuirSaldoProveedor(parametros, montoabonado);

        }

        private void txtefectivo2_TextChanged(object sender, EventArgs e)
        {
            calcularRestante();
        }
        private void calcularRestante()
        {
            try
            {
                efectivo = 0;
                tarjeta = 0;
                if (string.IsNullOrEmpty(txtefectivo2.Text))
                {
                    efectivo = 0;
                }
                else
                {
                    efectivo = Convert.ToDouble(txtefectivo2.Text);

                }
                //calculo de vuelto
                if (efectivo > saldo)
                {
                    vuelto = efectivo - saldo;
                    efectivoCalculado = (efectivo - vuelto);
                    TXTVUELTO.Text = vuelto.ToString();
                }
                else
                {
                    vuelto = 0;
                    efectivoCalculado = efectivo;
                    TXTVUELTO.Text = vuelto.ToString();

                }

                //calculo del restante
                restante = saldo - efectivoCalculado - tarjeta;
                txtrestante.Text = restante.ToString();
                if (restante < 0)
                {
                    txtrestante.Visible = false;
                    Label8.Visible = false;
                }
                else
                {
                    txtrestante.Visible = true;
                    Label8.Visible = true;
                }

                if (tarjeta == saldo)
                {
                    efectivo = 0;
                    txtefectivo2.Text = efectivo.ToString();
                }

            }
            catch (Exception)
            {

            }
        }

        private void MediosPagos_Load(object sender, EventArgs e)
        {
            saldo = PagosForm.pago;
            debo= Convert.ToDouble(lbltotal.Text = saldo.ToString());
            idproveedor = PagosForm.idproveedores;
            Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
            Obtener_datos.mostrar_inicio_De_sesion2(ref idusuario);
        }

        private void txtefectivo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtefectivo2, e);
        }
    }
}
