using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Punto_de_venta.ConexionDt;
using System.Windows.Forms;
using Punto_de_venta.Logica;
using Punto_de_venta.Datos;

namespace Punto_de_venta.Datos
{
  public  class Insertar_datos
  {
        int idcaja;
        int idusuario;
        public static bool insertar_Conceptos(string descripcion)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("insertar_Conceptos", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                cmd.ExecuteNonQuery();
                ConexionData.cerrar();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public static bool insertar_Gastos_varios(DateTime fecha,string Nro_documento,
          string  Tipo_comprobante, double Importe,string Descripcion,int Id_caja,int Id_concepto)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("insertar_Gastos_varios", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fecha", fecha);
                cmd.Parameters.AddWithValue("@Nro_documento", Nro_documento);
                cmd.Parameters.AddWithValue("@Tipo_comprobante", Tipo_comprobante);
                cmd.Parameters.AddWithValue("@Importe", Importe);
                cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
                cmd.Parameters.AddWithValue("@Id_caja", Id_caja);
                cmd.Parameters.AddWithValue("@Id_concepto", Id_concepto);
                cmd.ExecuteNonQuery();
                ConexionData.cerrar();
                return true;        
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return false;
            }
        }
        public static bool insertar_Ingresos_varios(DateTime fecha, string Nro_documento,
         string Tipo_comprobante, double Importe, string Descripcion, int Id_caja)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("insertar_Ingresos_varios", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fecha", fecha);
                cmd.Parameters.AddWithValue("@Nro_comprobante", Nro_documento);
                cmd.Parameters.AddWithValue("@Tipo_comprobante", Tipo_comprobante);
                cmd.Parameters.AddWithValue("@Importe", Importe);
                cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
                cmd.Parameters.AddWithValue("@Id_caja", Id_caja);
             
                cmd.ExecuteNonQuery();
                ConexionData.cerrar();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public  bool insertar_CreditoPorPagar(LcreditosPorPagar parametros)
        {
            try
            {
                Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("insertar_CreditoPorPagar", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Descripcion", parametros.Descripcion);
                cmd.Parameters.AddWithValue("@Fecha_registro", parametros.Fecha_registro);
                cmd.Parameters.AddWithValue("@Fecha_vencimiento", parametros.Fecha_vencimiento);
                cmd.Parameters.AddWithValue("@Total", parametros.Total);
                cmd.Parameters.AddWithValue("@Pago", 0);
                cmd.Parameters.AddWithValue("@Estado", "LE DEBO");
                cmd.Parameters.AddWithValue("@Id_caja", idcaja);
                cmd.Parameters.AddWithValue("@Id_Proveedor", parametros.Id_proveedor );
                cmd.ExecuteNonQuery() ;
                return true;                       
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }

        }

        public bool insertar_cobro_proveedores(Lproveedores param)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("insertar_pagos_proveedores", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Saldo", param.Saldo);
                cmd.Parameters.AddWithValue("@idproveedor", param.IdProveedor);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }

        }
        public bool insertar_Proveedores(Lproveedores parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("insertar_Proveedores", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", parametros.Nombre);
                cmd.Parameters.AddWithValue("@Direccion", parametros.Direccion);
                cmd.Parameters.AddWithValue("@IdentificadorFiscal", parametros.IdentificadorFiscal);
                cmd.Parameters.AddWithValue("@Celular", parametros.Celular);
                cmd.Parameters.AddWithValue("@Estado","ACTIVO");
                cmd.Parameters.AddWithValue("@Saldo", 0);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        public bool insertar_clientes(Lclientes  parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("insertar_clientes", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", parametros.Nombre);
                cmd.Parameters.AddWithValue("@Direccion", parametros.Direccion);
                cmd.Parameters.AddWithValue("@IdentificadorFiscal", parametros.IdentificadorFiscal);
                cmd.Parameters.AddWithValue("@Celular", parametros.Celular);
                cmd.Parameters.AddWithValue("@Estado", "ACTIVO");
                cmd.Parameters.AddWithValue("@Saldo", 0);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        public bool insertar_CreditoPorCobrar(LcreditoPorCobrar  parametros)
        {
            try
            {
                Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("insertar_CreditoPorCobrar", ConexionData.conectar);            
                cmd.CommandType = CommandType.StoredProcedure;        
                cmd.Parameters.AddWithValue("@Descripcion", parametros.Descripcion);
                cmd.Parameters.AddWithValue("@Fecha_registro", parametros.Fecha_registro);
                cmd.Parameters.AddWithValue("@Fecha_vencimiento", parametros.Fecha_vencimiento);
                cmd.Parameters.AddWithValue("@Total", parametros.Total);
                cmd.Parameters.AddWithValue("@Saldo", parametros.Saldo);            
                cmd.Parameters.AddWithValue("@Estado", "DEBE");
                cmd.Parameters.AddWithValue("@Id_caja", idcaja);
                cmd.Parameters.AddWithValue("@Id_cliente", parametros.Id_cliente);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }

        }
        public bool insertar_cobro_cliente(Lcliente param)
        {
            try
            {          
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("insertar_cobros_clientes", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Saldo", param.Saldo);             
                cmd.Parameters.AddWithValue("@idcliente", param.idcliente);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionData.cerrar();
            }
           
        }
        public bool Insertar_ControlCobros(Lcontrolcobros parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("Insertar_ControlCobros", ConexionData.conectar);
              
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Monto", parametros.Monto);
                cmd.Parameters.AddWithValue("@Fecha", parametros.Fecha);
                cmd.Parameters.AddWithValue("@Detalle", parametros.Detalle);
                cmd.Parameters.AddWithValue("@IdCliente", parametros.IdCliente);
                cmd.Parameters.AddWithValue("@IdUsuario", parametros.IdUsuario);
                cmd.Parameters.AddWithValue("@IdCaja", parametros.IdCaja);
                cmd.Parameters.AddWithValue("@Comprobante", parametros.Comprobante);
                cmd.Parameters.AddWithValue("@efectivo", parametros.efectivo);
                cmd.Parameters.AddWithValue("@tarjeta", parametros.tarjeta);     
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return true;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }

        public bool Insertar_ControlPagos(Lcontrolpagos parametros)
        {
            try
            {
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("insertar_ControlPagos", ConexionData.conectar);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Monto", parametros.Monto);
                cmd.Parameters.AddWithValue("@Fecha", parametros.Fecha);
                cmd.Parameters.AddWithValue("@Detalle", parametros.Detalle);
                cmd.Parameters.AddWithValue("@IdProveedor", parametros.IdProveedor);
                cmd.Parameters.AddWithValue("@IdUsuario", parametros.IdUsuario);              
                cmd.Parameters.AddWithValue("@Comprobante", parametros.Comprobante);
                cmd.Parameters.AddWithValue("@efectivo", parametros.efectivo);
                cmd.Parameters.AddWithValue("@tarjeta", parametros.tarjeta);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return true;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        

        //Kardex
        public bool insertar_KARDEX_Entrada(LKardex parametros)
        {
            try
            {
                Obtener_datos.mostrar_inicio_De_sesion2(ref idusuario);
                Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("insertar_KARDEX_Entrada", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fecha",parametros.Fecha);
                cmd.Parameters.AddWithValue("@Motivo", parametros.Motivo );
                cmd.Parameters.AddWithValue("@Cantidad", parametros.Cantidad);
                cmd.Parameters.AddWithValue("@Id_producto", parametros.Id_producto);
                cmd.Parameters.AddWithValue("@Id_usuario", idusuario);
                cmd.Parameters.AddWithValue("@Tipo", "ENTRADA");
                cmd.Parameters.AddWithValue("@Estado", "DESPACHO CONFIRMADO");
                cmd.Parameters.AddWithValue("@Id_caja", idcaja);
                cmd.ExecuteNonQuery();
                return true;
           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return true;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }
        public bool insertar_KARDEX_SALIDA(LKardex parametros)
        {
            try
            {
                Obtener_datos.mostrar_inicio_De_sesion2(ref idusuario);
                Obtener_datos.Obtener_id_caja_PorSerial(ref idcaja);
                ConexionData.abrir();
                SqlCommand cmd = new SqlCommand("insertar_KARDEX_SALIDA", ConexionData.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fecha", parametros.Fecha);
                cmd.Parameters.AddWithValue("@Motivo", parametros.Motivo);
                cmd.Parameters.AddWithValue("@Cantidad", parametros.Cantidad);
                cmd.Parameters.AddWithValue("@Id_producto", parametros.Id_producto);
                cmd.Parameters.AddWithValue("@Id_usuario", idusuario);
                cmd.Parameters.AddWithValue("@Tipo", "SALIDA");
                cmd.Parameters.AddWithValue("@Estado", "DESPACHO CONFIRMADO");
                cmd.Parameters.AddWithValue("@Id_caja", idcaja);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return true;
            }
            finally
            {
                ConexionData.cerrar();
            }
        }

    }
}
