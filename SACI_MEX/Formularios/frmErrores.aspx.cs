using SACI.Datos;
using SACI.Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SACI_MEX.Formularios.WebServices
{
    public partial class frmErrores : System.Web.UI.Page
    {
        #region PROPIEDADES GLOBALES
        const string PageSizeSessionKey = "ed5e843d-cff7-47a7-815e-832923f7fb09";
        #endregion


        #region EVENTOS


        protected void Page_Load(object sender, EventArgs e)
        {
            //CreaArchvioLog();
            if (Session["ErrorSql"] != null)
                txtArea.InnerText = Session["ErrorSql"].ToString();
        }


        #endregion



        #region METODOS


        public void CreaArchvioLog()
        {
            string FilePath = string.Empty;
            try
            {
                string FolderPath = this.Server.MapPath("Log");
                //FilePath = Server.MapPath(FolderPath);

                if (File.Exists(FolderPath))
                {
                    StreamWriter sw = new StreamWriter(FilePath);

                    //for (int i = 0; i < Conn.Length; i++)
                    //{
                    sw.WriteLine(Session["MsgError"].ToString());
                    //}
                    sw.Close();
                }
                else
                {
                    StreamWriter arch1 = new StreamWriter(FolderPath + "/log.txt", true);
                    arch1.WriteLine("PC: " + Environment.MachineName + "; Fecha:" + DateTime.Now + "; " + Session["MsgError"].ToString());
                    arch1.Close();
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion


    }
}