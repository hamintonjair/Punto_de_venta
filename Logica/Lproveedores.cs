﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Punto_de_venta.Logica
{
   public  class Lproveedores
    {
        public int IdProveedor { set; get; }
        public string  Nombre { set; get; }
        public string Direccion { set; get; }
        public string IdentificadorFiscal { set; get; }
        public string Celular { set; get; }
        public string Estado { set; get; }
        public double  Saldo { set; get; }

    }
}
