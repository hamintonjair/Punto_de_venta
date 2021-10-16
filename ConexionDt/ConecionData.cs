using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_venta.ConexionDt
{
    class ConexionData
    {
        //CONEXCION MAESTRA
        public static string conexion = Convert.ToString(ConexionDt.Desencryptacion.checkServer());/*"Data source=DESKTOP-L33KPJH; Initial Catalog = SistemaContable; Integrated Security = True";*/
    }
}
