using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACI.Datos
{
    public static class d_Importaciones
    {

        /// <summary>
        /// METODO PARA SELECCIONAR LAS FECHAS DE LAS IMPORTACIONES
        /// </summary>
        public static DataTable SelecImpoByFecha(int OPCION = 0, int YEAR = 0, int MONTH = 0, string PEDIMENTOARMADO = "", string FACTURA = "", string DESCRIPCION = "", string FRACCION = "", string DESDE = "", string HASTA = "")
        {
            try
            {
                string CadenaSQL;
                CadenaSQL = sp_Importaciones.SACIWEB_MC_IMPO_BUSCAR + string.Format("{0}, {1}, '{2}', '{3}', '{4}', '{5}', {6}, '{7}', '{8}'", YEAR, MONTH, PEDIMENTOARMADO, FACTURA, DESCRIPCION, FRACCION, OPCION, DESDE, HASTA);
                return static_ConecctionSQL.Conectar(CadenaSQL);
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    throw ex;
                }
                else
                {
                    throw new ApplicationException(string.Format("Error: {0}. {1}. {2}. {3}. {4}", ex.Message.Replace("\r\n", "|").Replace("'", "´"), "|Metodo: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    "|Recurso: " + ex.Source,
                    "|Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"),
                    "|Sp: " + sp_Importaciones.SACIWEB_MC_IMPO_AÑO_MES.Replace("\r\n", "|").Replace("'", "´")), ex);
                }

            }           
        }
    }
}
