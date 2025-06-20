using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using SACI.Negocio;
using SACI_MEX.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SACI_MEX.Formularios
{
    public partial class catRegion : System.Web.UI.Page
    {
        #region PROPIEDADES GLOBALES
        RNCatalogos RNCat = null;
        const string PageSizeSessionKey = "ed5e843d-cff7-47a7-815e-832923f7fb09";
        #endregion
        #region EVENTOS
        //1ER FILTRO PARA EL GETALL
        //3ER FILRTRO PARA INSERTAR DATOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ConfigurationManager.AppSettings["BDActivo"] = Session["idBase"].ToString();
                    cat_Region(1);
                    AccesosUsuario();
                }


            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    AlertErrorUsuario(ex.Message.ToString());
                }
                else
                {
                    string mensaje = string.Format("Error en la pantalla: {0}. {1}. {2}. {3}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
                    Environment.NewLine, "Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    Environment.NewLine, "Recurso: " + ex.Source,
                    Environment.NewLine, "Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
                    Session["ErrorSql"] = mensaje;

                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }
        //SEGUNDO PROCESO PARA QUE PODAMOS AGREGAR UN NUEVO REGISTRO
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    Session["grvRegion"] = null;
                    Session["EditarRegion"] = null;
                }


                //Cuando se quiera filtrar el Grid entra en el if
                if (Session["grvRegion"] != null)
                {
                    grvRegion.DataSource = Session["grvRegion"];
                    grvRegion.DataBind();
                    grvRegion.SettingsPager.PageSize = GridPageSize;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    AlertErrorUsuario(ex.Message.ToString());
                }
                else
                {
                    //LimpiaControles();
                    string mensaje = string.Format("Error en la pantalla: {0}. {1}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
                    Environment.NewLine, "Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    Environment.NewLine, "Recurso: " + ex.Source,
                    Environment.NewLine, "Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
                    Session["ErrorSql"] = mensaje;

                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        /*protected void dbpRegion_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    Session["grvRegion"] = null;
                    Session["EditarRegion"] = null;
                }


                //Cuando se quiera filtrar el Grid entra en el if
                if (Session["grvRegion"] != null)
                {
                    grvRegion.DataSource = Session["grvRegion"];
                    grvRegion.DataBind();
                    grvRegion.SettingsPager.PageSize = GridPageSize;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    AlertErrorUsuario(ex.Message.ToString());
                }
                else
                {
                    //LimpiaControles();
                    string mensaje = string.Format("Error en la pantalla: {0}. {1}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
                    Environment.NewLine, "Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    Environment.NewLine, "Recurso: " + ex.Source,
                    Environment.NewLine, "Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
                    Session["ErrorSql"] = mensaje;

                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }*/
        //ESTE ES EL 1ER FILTRO PARA QUE NOS PUEDA ABRIR EL FORMULARIO DE AGREGAR Y EDITAR
        protected void cbpRegion_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                hdnGuardar.Value = "0";
                if (e.Parameter.ToString() == "nuevo")
                {
                    hdnGuardar.Value = "3";
                    LimpiaControles();
                    //NuevoRegistro();
                    titNewRegion.InnerHtml = "Nueva Region";
                }
                else if (e.Parameter.ToString() == "editar")
                {
                    if (grvRegion.GetSelectedFieldValues("Region_Key").Count > 0)
                    {
                        Session["EditarRegion"] = true;
                        hdnGuardar.Value = "3";
                        NuevoRegistro();
                        titNewRegion.InnerHtml = "Editar Region";
                        TXT_REGION_Key.Text = grvRegion.GetSelectedFieldValues("Region_Key")[0].ToString().Trim();
                        hdnKey.Value = grvRegion.GetSelectedFieldValues("Region_Key")[0].ToString().Trim();
                        TXT_TRATADO.Text = grvRegion.GetSelectedFieldValues("Tratado")[0].ToString().Trim();
                        TXT_REGION.Text = grvRegion.GetSelectedFieldValues("Region")[0].ToString().Trim();
                    }
                    else
                    {
                        hdnGuardar.Value = "2";
                        AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
                    }
                }
                else if (e.Parameter.ToString() == "borrar")
                {
                    if (grvRegion.GetSelectedFieldValues("Region_Key").Count > 0)
                    {
                        hdnGuardar.Value = "1";
                        cat_Region(4, Convert.ToInt32(grvRegion.GetSelectedFieldValues("Region_Key")[0].ToString().Trim()));
                        AlertSucces(MsgRegistros.MsgRegistroElimina);
                    }
                    else
                    {
                        hdnGuardar.Value = "2";
                        AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
                    }
                }
                else if (e.Parameter.ToString() == "guardar")
                {
                    hdnGuardar.Value = "1";


                    if (!Convert.ToBoolean(Session["EditarRegion"]))
                    {
                        //Inserta
                        cat_Region(2, 0, TXT_TRATADO.Text.Trim(), TXT_REGION.Text.Trim());
                    }
                    else
                    {
                        //Actualiza
                        cat_Region(3, Convert.ToInt32(hdnKey.Value.ToString()), TXT_TRATADO.Text.Trim(), TXT_REGION.Text.Trim());
                        Session["EditarRegion"] = false;
                    }
                    AlertSucces(MsgRegistros.MsgRegistroAgregar);
                }
                else { }
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    //NuevoRegistro();
                    hdnGuardar.Value = "2";
                    AlertErrorUsuario(ex.Message.ToString());
                }
                else
                {
                    hdnGuardar.Value = "4";
                    string mensaje = string.Format("Error en la pantalla: {0}. {1}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
                    Environment.NewLine, "Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    Environment.NewLine, "Recurso: " + ex.Source,
                    Environment.NewLine, "Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }

            }
            finally
            {
                GC.Collect();
            }
        }
        protected void lkb_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                Export("xlsx");

            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    AlertErrorUsuario(ex.Message.ToString());
                }
                else
                {
                    //LimpiaControles();
                    string mensaje = string.Format("Error en la pantalla: {0}. {1}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
                    Environment.NewLine, "Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    Environment.NewLine, "Recurso: " + ex.Source,
                    Environment.NewLine, "Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
                    Session["ErrorSql"] = mensaje;

                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }
        #endregion
        #region METODOS
        public void LimpiaControles()
        {
            TXT_REGION_Key.Text = string.Empty;
            TXT_TRATADO.Text = string.Empty;
            TXT_REGION.Text = string.Empty;
        }
        protected int GridPageSize
        {
            get
            {
                if (Session[PageSizeSessionKey] == null)
                    return grvRegion.SettingsPager.PageSize;
                return (int)Session[PageSizeSessionKey];
            }
            set { Session[PageSizeSessionKey] = value; }
        }
        //3ER FILETRO PARA ABRIR EL GETALL
        public void AccesosUsuario()
        {
            RNCat = new RNCatalogos();
            // **********************************************************
            // *************************** ACCESOS
            int IDPERFIL = 0;
            if (Session["IDPerfil"] != null)
                IDPERFIL = int.Parse(Session["IDPerfil"].ToString());
            //VALIDA PERMISOS USUARIO
            if (IDPERFIL != 1)
            {
                DataTable dtAccesos = new DataTable("CREGI");
                dtAccesos = RNCat.ACCESOS_USUARIOS(IDPERFIL, "CREGI");

                // Presuming the DataTable has a column named Date.
                string Expression;
                Expression = "CVE_ACTIVIDAD = 'CREGI1'";
                DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = dtAccesos.Select(Expression);
                lkb_Nuevo.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CREGI2'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Editar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CREGI3'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Eliminar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CREGI4'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Excel.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);
            }
        }
        public void NuevoRegistro()
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "MostrarModal", "<script> document.getElementById('btnNuevo').click(); </script> ", false);
        }
        public void ErrorAlert(string mensaje)
        {
            //pModal.InnerText = "\n"+ mensaje;
            txtArea.Value = mensaje.Replace("|", "\n");
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "", "<script> document.getElementById('btnError').setAttribute('data-whatever', '" + mensaje + "'); document.getElementById('btnError').click(); </script> ", false);
        }
        public void AlertErrorUsuario(string mensaje)
        {
            p2.InnerText = mensaje;
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "", "<script> document.getElementById('btnErrorUser').setAttribute('data-whatever', '" + mensaje + "'); document.getElementById('btnErrorUser').click(); </script> ", false);
        }
        public void AlertSucces(string mensaje)
        {
            pSucces.InnerText = mensaje;
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "", "<script> document.getElementById('btnSucces').setAttribute('data-whatever', '" + mensaje + "'); document.getElementById('btnSucces').click(); </script> ", false);
        }
        //ESTE ES EL QUE NOS AYUDA A ABRIR EL GETALL POR LO QUE ESTE ES EL SEGUNDO FILTRO
        public void cat_Region(int OPCION = 0, Int64 REGION_KEY = 0, string TRATADO = "", string REGION = "")
        {
            DataTable dt = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvRegion"] = dt = RNCat.CatalogoRegion(OPCION, REGION_KEY, TRATADO, REGION);
                grvRegion.DataSource = dt;
                grvRegion.DataBind();
                grvRegion.Settings.VerticalScrollableHeight = 300;
                grvRegion.SettingsPager.PageSize = 15;

                //LimpiaControles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dt.Dispose();
                GC.Collect();
            }
        }
        void Export(string format)
        {
            PrintingSystemBase ps = new PrintingSystemBase();

            LinkBase header = new LinkBase();

            PrintableComponentLinkBase link1 = new PrintableComponentLinkBase();
            link1.Component = Exporter;
            CompositeLinkBase compositeLink = new CompositeLinkBase(ps);
            compositeLink.Links.AddRange(new object[] { header, link1 });

            var options = new XlsxExportOptions();
            options.SheetName = "Region";

            compositeLink.CreateDocument();
            using (MemoryStream stream = new MemoryStream())
            {
                switch (format)
                {
                    case "xlsx":
                        compositeLink.ExportToXlsx(stream, options);
                        WriteToResponse(h1_titulo.InnerText, true, format, stream);
                        break;
                    default:
                        break;
                }
            }
            ps.Dispose();
        }
        void WriteToResponse(string fileName, bool saveAsFile, string fileFormat,   MemoryStream stream)
        {
            if (Page == null || Page.Response == null)
                return;
            string disposition = saveAsFile ? "attachment" : "inline";
            Page.Response.Clear();
            Page.Response.Buffer = false;
            Page.Response.AppendHeader("Content-Type", string.Format("application/{0}", fileFormat));
            Page.Response.AppendHeader("Content-Transfer-Encoding", "binary");
            Page.Response.AppendHeader("Content-Disposition",
                string.Format("{0}; filename={1}.{2}", disposition, fileName, fileFormat));
            Page.Response.BinaryWrite(stream.ToArray());
            Page.Response.End();
        }
        #endregion
    }
}
