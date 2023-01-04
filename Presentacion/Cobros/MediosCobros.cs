﻿using Punto_de_venta.Datos;
using Punto_de_venta.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Punto_de_venta.Presentacion.Cobros
{
    public partial class MediosCobros : Form
    {
        public MediosCobros()
        {
            InitializeComponent();
        }
        double saldo;
        int idcliente;
        int idcaja;
        int idusuario;
        //
        double efectivo;
        double tarjeta;
        double vuelto;
        double restante;
        double efectivoCalculado;
        double montoabonado;
        private void button15_Click(object sender, EventArgs e)
        {
            montoabonado = efectivoCalculado + tarjeta;
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
        private void insertarControlCobro()
        {
            Lcontrolcobros parametros = new Lcontrolcobros();
            Insertar_datos funcion = new Insertar_datos();
            parametros.Monto = efectivoCalculado + tarjeta;
            parametros.Fecha = DateTime.Now;
            parametros.Detalle = "Cobro a cliente";
            parametros.IdCliente = idcliente;
            parametros.IdUsuario = idusuario;
            parametros.IdCaja = idcaja;
            parametros.Comprobante = "-";
            parametros.efectivo = efectivoCalculado;
            parametros.tarjeta = tarjeta;
            if (funcion.Insertar_ControlCobros(parametros) == true)
            {
                if (funcion.Insertar_ControlCobrosT(parametros) == true)
                {
                    if (funcion.InsertarControlPorCobrar(parametros) == true)
                    {
                        
                            Dispose();
                       
                    }
                    
                }
            }
        }
        private void disminuirSaldocliente()
        {
            Lclientes parametros = new Lclientes();
            Editar_datos funcion = new Editar_datos();
            parametros.idcliente = idcliente;
            funcion.disminuirSaldocliente(parametros, montoabonado);

        }

        private void MediosCobros_Load(object sender, EventArgs e)
        {
            saldo = CobrosForm.saldo;
            lbltotal.Text = saldo.ToString();
            idcliente = CobrosForm.idcliente;
            Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
            Obtener_datos.mostrar_inicio_De_sesion2(ref idusuario);
        }

        private void txtefectivo2_TextChanged(object sender, EventArgs e)
        {
            calcularRestante();
        }

        private void txttarjeta2_TextChanged(object sender, EventArgs e)
        {
            calcularRestante();
        }

        private void txtefectivo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Separador_de_Numeros(txtefectivo2, e);
        }

        private void txttarjeta2_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }
    }
}
