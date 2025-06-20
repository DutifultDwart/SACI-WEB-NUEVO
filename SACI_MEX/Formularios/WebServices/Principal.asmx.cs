using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;


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
    public class Principal : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public void CerrarSesion()
        {
            Session["Perfil"] = null;
            Session["CerrarSesion"] = true;
            //Session["idBase"] = null;
            //Session.Clear();
            //HttpContext.Current.Response.Redirect("~/Default.aspx", false);

        }
    }
}
