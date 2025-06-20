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
    public partial class catCasosRegla : System.Web.UI.Page
    {
        #region PROPIEDADES GLOBALES

        RNCatalogos RNCat = null;
        const string PageSizeSessionKey = "ed5e843d-cff7-47a7-815e-832923f7fb09";

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ConfigurationManager.AppSettings["BDActivo"] = Session["idBase"].ToString();
                    CatCasosRegla(1);
                    AccesosUsuario();
                }

                RNCat = new RNCatalogos();
                Session["dtReglasOrigen"] = RNCat.CatalogoReglaOrigen(1);
                CMB_REGLA_LINK.DataSource = Session["dtReglasOrigen"];
                CMB_REGLA_LINK.DataBind();
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    AlertErrorUsuario(ex.Message.ToString());
                }
                else
                {
                    string mensaje = string.Format("Error en la pantalla: {0}. {1} {2} {3} {4}. {5} {6}. {7} {8}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
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

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    Session["grvCasosRegla"] = null;
                    Session["EditarCR"] = null;
                }
                if (Session["grvCasosRegla"] != null)
                {
                    grvCasosRegla.DataSource = Session["grvCasosRegla"];
                    grvCasosRegla.DataBind();
                    grvCasosRegla.SettingsPager.PageSize = GridPageSize;
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
                    string mensaje = string.Format("Error en la pantalla: {0}. {1} {2} {3} {4}. {5} {6}. {7} {8}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
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

        protected void cbpCasosRegla_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                hdnGuardar.Value = "0";
                if (e.Parameter.ToString() == "nuevo")
                {
                    hdnGuardar.Value = "3";
                    LimpiaControles();
                    NuevoRegistro();
                    titNewTratado.InnerHtml = "Nuevo Casos de Regla";
                }
                else if (e.Parameter.ToString() == "editar")
                {
                    if (grvCasosRegla.GetSelectedFieldValues("CASO_KEY").Count > 0)
                    {
                        Session["EditarCR"] = true;
                        hdnGuardar.Value = "3";
                        NuevoRegistro();
                        titNewTratado.InnerHtml = "Editar Caso de Regla";
                        TXT_CRKey.Text = grvCasosRegla.GetSelectedFieldValues("CASO_KEY")[0].ToString().Trim();
                        hdnKey.Value = grvCasosRegla.GetSelectedFieldValues("CASO_KEY")[0].ToString().Trim();
                        CMB_REGLA_LINK.Value = grvCasosRegla.GetSelectedFieldValues("REGLA_LINK")[0].ToString().Trim();
                        TXT_CONDICION_CASO.Text = grvCasosRegla.GetSelectedFieldValues("CONDICION_CASO")[0].ToString().Trim();
                        TXT_TEXTO_CASO.Text = grvCasosRegla.GetSelectedFieldValues("TEXTO_CASO")[0].ToString().Trim();
                    }
                    else
                    {
                        hdnGuardar.Value = "2";
                        AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
                    }
                }
                else if (e.Parameter.ToString() == "borrar")
                {
                    if (grvCasosRegla.GetSelectedFieldValues("CASO_KEY").Count > 0)
                    {
                        hdnGuardar.Value = "1";
                        CatCasosRegla(4, Convert.ToInt32(grvCasosRegla.GetSelectedFieldValues("CASO_KEY")[0].ToString().Trim()));
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


                    if (!Convert.ToBoolean(Session["EditarCR"]))
                    {
                        //Inserta
                        CatCasosRegla(2, 0, Convert.ToInt32(CMB_REGLA_LINK.Value), TXT_CONDICION_CASO.Text.Trim(), TXT_TEXTO_CASO.Text.Trim());
                    }
                    else
                    {
                        //Actualiza
                        CatCasosRegla(3, Convert.ToInt32(hdnKey.Value.ToString()), Convert.ToInt32(CMB_REGLA_LINK.Value), TXT_CONDICION_CASO.Text.Trim(), TXT_TEXTO_CASO.Text.Trim());
                        Session["EditarCR"] = false;
                    }
                    AlertSucces(MsgRegistros.MsgRegistroAgregar);
                }
                else { }
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    NuevoRegistro();
                    hdnGuardar.Value = "2";
                    AlertErrorUsuario(ex.Message.ToString());
                }
                else
                {
                    hdnGuardar.Value = "4";
                    string mensaje = string.Format("Error en la pantalla: {0}. {1} {2} {3} {4}. {5} {6}. {7} {8}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
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
                    string mensaje = string.Format("Error en la pantalla: {0}. {1} {2} {3} {4}. {5} {6}. {7} {8}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
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

        #region METODOS

        public void LimpiaControles()
        {

            TXT_CRKey.Text = string.Empty;
            CMB_REGLA_LINK.Text = string.Empty;
            TXT_CONDICION_CASO.Text = string.Empty;
            TXT_TEXTO_CASO.Text = string.Empty;
        }

        protected int GridPageSize
        {
            get
            {
                if (Session[PageSizeSessionKey] == null)
                    return grvCasosRegla.SettingsPager.PageSize;
                return (int)Session[PageSizeSessionKey];
            }
            set { Session[PageSizeSessionKey] = value; }
        }
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
                DataTable dtAccesos = new DataTable("CCASR");
                dtAccesos = RNCat.ACCESOS_USUARIOS(IDPERFIL, "CCASR");

                // Presuming the DataTable has a column named Date.
                string Expression;
                Expression = "CVE_ACTIVIDAD = 'CCASR1'";
                DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = dtAccesos.Select(Expression);
                lkb_Nuevo.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CCASR2'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Editar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CCASR3'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Eliminar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CCASR4'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Excel.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);
            }

        }

        public void NuevoRegistro()
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "MostrarModal", "<script> document.getElementById('btnNuevo').click(); </script> ", false);
        }

        //Metodo que muestra ventana de alerta
        public void ErrorAlert(string mensaje)
        {
            //pModal.InnerText = "\n"+ mensaje;
            txtArea.Value = mensaje.Replace("|", "\n");
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "", "<script> document.getElementById('btnError').setAttribute('data-whatever', '" + mensaje + "'); document.getElementById('btnError').click(); </script> ", false);
        }

        //Metodo que muestra ventana de alerta
        public void AlertErrorUsuario(string mensaje)
        {
            p2.InnerText = mensaje;
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "", "<script> document.getElementById('btnErrorUser').setAttribute('data-whatever', '" + mensaje + "'); document.getElementById('btnErrorUser').click(); </script> ", false);
        }

         //Metodo que muestra ventana de alerta
        public void AlertSucces(string mensaje)
        {
            pSucces.InnerText = mensaje;
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "", "<script> document.getElementById('btnSucces').setAttribute('data-whatever', '" + mensaje + "'); document.getElementById('btnSucces').click(); </script> ", false);
        }

        public void CatCasosRegla(int OPCION = 0, Int64 CASO_KEY = 0, decimal REGLA_LINK = 0, string CONDICION_CASO = "", string TEXTO_CASO = "")
        {
            DataTable dt = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvCasosRegla"] = dt = RNCat.CatalogoCasosRegla(OPCION, CASO_KEY, REGLA_LINK, CONDICION_CASO, TEXTO_CASO);
                grvCasosRegla.DataSource = dt;
                grvCasosRegla.DataBind();
                grvCasosRegla.Settings.VerticalScrollableHeight = 300;
                grvCasosRegla.SettingsPager.PageSize = 15;

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
            options.SheetName = "Casos Regla";

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

        void WriteToResponse(string fileName, bool saveAsFile, string fileFormat, MemoryStream stream)
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