using SACI.Datos;
using SACI.Negocio;
using System.Data;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System;

namespace SACI_MEX.Formularios.WebServices
{
    /// <summary>
    /// Descripción breve de Principal
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Importaciones : System.Web.Services.WebService
    {

        [WebMethod]
        public e_Importaciones_Enc[] SelecImpoByFecha(int OPCION = 0, int YEAR = 0, int MONTH = 0, string PEDIMENTOARMADO = "", string FACTURA = "", string DESCRIPCION = "", string FRACCION = "", string DESDE = "", string HASTA = "")
        {
            List<e_Importaciones_Enc> lsImpo = new List<e_Importaciones_Enc>();
            DataTable dtMeses = new DataTable();
            dtMeses = n_Importaciones.SelecImpoByFecha(OPCION, YEAR, MONTH, PEDIMENTOARMADO, FACTURA, DESCRIPCION, FRACCION, DESDE, HASTA);
            foreach (DataRow dtRow in dtMeses.Rows)
            {
                e_Importaciones_Enc DataObj = new e_Importaciones_Enc();
                DataObj.documento = Convert.ToString(dtRow["Documento"]);
                DataObj.fecha = Convert.ToDateTime(dtRow["Fecha"]);
                DataObj.aduana = Convert.ToString(dtRow["Aduana"]);
                DataObj.patente = Convert.ToString(dtRow["Patente"]);
                DataObj.cod_proveedor = Convert.ToString(dtRow["Codigo Proveedor"]);
                DataObj.cve_pedimento = Convert.ToString(dtRow["Clave Pedimento"]);
                DataObj.tc = Convert.ToDecimal(dtRow["TC"]);
                DataObj.aduana_imp = Convert.ToDecimal(dtRow["Valor Aduana"]);
                DataObj.val_dls = Convert.ToDecimal(dtRow["Valor Dolares"]);
                DataObj.descarga = Convert.ToString(dtRow["DESCARGA"]);
                DataObj.carga_m = Convert.ToString(dtRow["Carga M"]);
                DataObj.div_alm = Convert.ToString(dtRow["Division/Alamcen"]);

                lsImpo.Add(DataObj);
            }
            return lsImpo.ToArray();
        }
    }
}
