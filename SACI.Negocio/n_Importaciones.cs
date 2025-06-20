using SACI.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACI.Negocio
{
    public static  class n_Importaciones
    {
        public static DataTable SelecImpoByFecha(int OPCION = 0, int YEAR = 0, int MONTH = 0, string PEDIMENTOARMADO = "", string FACTURA = "", string DESCRIPCION = "", string FRACCION = "", string DESDE = "", string HASTA = "")
        {
            return d_Importaciones.SelecImpoByFecha(OPCION, YEAR, MONTH, PEDIMENTOARMADO, FACTURA, DESCRIPCION, FRACCION, DESDE, HASTA);
        }
    }
}
