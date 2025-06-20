using SACI.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.Drawing;
using System.IO;

namespace SACI_MEX
{
    public partial class _default : System.Web.UI.Page
    {
        #region PROPIEDADES GLOBALES

        RNCatalogos RNCat = null;
        const string PageSizeSessionKey = "ed5e843d-cff7-47a7-815e-832923f7fb09";
        private Image headerImage;
        DataTable dtDtsGrales = new DataTable();

        #endregion

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    
                    if (Convert.ToBoolean(Session["CerrarSesion"]))
                    {
                        FormsAuthentication.SignOut();
                        FormsAuthentication.RedirectToLoginPage();
                    }
                    //else
                    //{
                    //    //AccesosUsuario();
                    //    //CargarMensajes(1);
                    //    //CargarMensajes(2);
                    //}

                    
                }
                lkb_verDetalle.Attributes.Add("onClick", "return false;");
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    AlertErrorUsuario(ex.Message.ToString());
                }
                else
                {
                   // string mensaje = string.Format("Error: {0}. {1}. {2}. {3}", ex.Message.Replace("\r\n", "|").Replace("'", "´"), "|Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                   //"|Recurso: " + ex.Source,
                   //"|Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);

                    string mensaje = string.Format("Error en la pantalla: {0}. {1} {2} {3} {4}. {5} {6}. {7} {8}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
                        Environment.NewLine, "Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                        Environment.NewLine, "Recurso: " + ex.Source,
                        Environment.NewLine, "Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
                    mensaje = mensaje.Replace("\r\n", "|");
                    
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
                    Session["grvVencimientos"] = null;
                    Session["grvWarnings"] = null;
                    Session["grvDetWarnings"] = null;
                }

                //Cuando se quiera filtrar el Grid entra en el if
                if (Session["grvVencimientos"] != null)
                {
                    grvVencimientos.DataSource = Session["grvVencimientos"];
                    grvVencimientos.DataBind();
                    //grvVencimientos.SettingsPager.PageSize = GridPageSize;
                }

                if (Session["grvWarnings"] != null)
                {
                    grvWarnings.DataSource = Session["grvWarnings"];
                    grvWarnings.DataBind();
                }

                if (Session["grvDetWarnings"] != null)
                {
                    grvDetWarnings.DataSource = Session["grvDetWarnings"];
                    grvDetWarnings.DataBind();
                }

                if (Session["RegistroSACI"] != null)
                {
                    string registro = Session["RegistroSACI"].ToString();
                    int largo = registro.Length;
                    lblDiasRestantes.Text = registro.Substring(3, largo-3);
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
                    string mensaje = string.Format("Error: {0}. {1}. {2}. {3}", ex.Message.Replace("\r\n", "|").Replace("'", "´"), "|Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    "|Recurso: " + ex.Source,
                    "|Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);

                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void cbpDefault_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                hdnGuardar.Value = "0";
                if (e.Parameter.ToString() == "GenerarDetalle")
                {
                    CargarDetalle();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    hdnGuardar.Value = "1";
                    AlertErrorUsuario(ex.Message.ToString());
                }
                else
                {
                    hdnGuardar.Value = "2";
                    string mensaje = string.Format("Error: {0}. {1}. {2}. {3}", ex.Message.Replace("\r\n", "|").Replace("'", "´"), "|Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    "|Recurso: " + ex.Source,
                    "|Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
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
                    string mensaje = string.Format("Error: {0}. {1}. {2}. {3}", ex.Message.Replace("\r\n", "|").Replace("'", "´"), "|Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    "|Recurso: " + ex.Source,
                    "|Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);

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
        public void CargarMensajes(int OPCION = 0)
        {
            DataTable dtMensajes = new DataTable();
            try
            {
                RNCat = new RNCatalogos();

                if (OPCION.Equals(1))
                {
                    Session["grvVencimientos"] = dtMensajes = RNCat.MENSAJES_INICIO(OPCION);
                    grvVencimientos.DataSource = dtMensajes;
                    grvVencimientos.DataBind();
                    //grvVencimientos.Settings.VerticalScrollableHeight = 300;
                    //grvVencimientos.SettingsPager.PageSize = 15;
                }
                else
                {
                    Session["grvWarnings"] = dtMensajes = RNCat.MENSAJES_INICIO(OPCION);
                    grvWarnings.DataSource = dtMensajes;
                    grvWarnings.DataBind();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtMensajes.Dispose();
                GC.Collect();
            }
        }

        public void CargarDetalle()
        {
            DataTable dtMensajes = new DataTable();
            try
            {
                RNCat = new RNCatalogos();

                Session["grvDetWarnings"] = dtMensajes = RNCat.DETALLE_WARNINGS();
                grvDetWarnings.DataSource = dtMensajes;
                grvDetWarnings.DataBind();
                grvDetWarnings.Settings.VerticalScrollableHeight = 300;
                grvDetWarnings.SettingsPager.PageSize = 15;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtMensajes.Dispose();
                GC.Collect();
            }
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

        void Export(string format)
        {
            PrintingSystemBase ps = new PrintingSystemBase();

            using (headerImage = Image.FromFile(Server.MapPath("~/img/LogoSac.png")))
            {
                LinkBase header = new LinkBase();
                header.CreateDetailHeaderArea += Header_CreateDetailHeaderArea;

                PrintableComponentLinkBase link1 = new PrintableComponentLinkBase();
                link1.Component = Exporter;
                CompositeLinkBase compositeLink = new CompositeLinkBase(ps);
                compositeLink.Links.AddRange(new object[] { header, link1 });


                compositeLink.CreateDocument();
                using (MemoryStream stream = new MemoryStream())
                {
                    switch (format)
                    {
                        case "xlsx":
                            compositeLink.ExportToXlsx(stream);
                            WriteToResponse("Detalle errores", true, format, stream);
                            break;
                        default:
                            break;
                    }
                }
                ps.Dispose();
            }
        }
        void Header_CreateDetailHeaderArea(object sender, CreateAreaEventArgs e)
        {
            e.Graph.BorderWidth = 0;
            Rectangle r = new Rectangle(0, 0, headerImage.Width, headerImage.Height);
            e.Graph.DrawImage(headerImage, r);
            r = new Rectangle(0, headerImage.Height, 200, 50);
            RNCat = new RNCatalogos();
            dtDtsGrales = RNCat.CatDatosGenerales(1);
            string encabezado = "";
            if (dtDtsGrales.Rows.Count > 0)
            {
                encabezado = " Empresa: " + dtDtsGrales.Rows[0]["Denominacion"].ToString().Trim() + " \n" + " RFC: " + dtDtsGrales.Rows[0]["Rfc"].ToString().Trim() + " \n" + " Registro IMMEX: " + dtDtsGrales.Rows[0]["RegistroIMMEX"].ToString().Trim() + " \n" + "Dirección: " + dtDtsGrales.Rows[0]["Dir"].ToString().Trim();
            }
            e.Graph.DrawString(encabezado, r);
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
                DataTable dtAccesos = new DataTable("INIC");
                dtAccesos = RNCat.ACCESOS_USUARIOS(IDPERFIL, "INIC");

                if (dtAccesos.Rows.Count > 0 && dtAccesos.Columns.Count > 1)
                {
                    // Presuming the DataTable has a column named Date.
                    string Expression;
                    Expression = "CVE_ACTIVIDAD = 'INIC1'";
                    DataRow[] foundRows;
                    // Use the Select method to find all rows matching the filter.
                    foundRows = dtAccesos.Select(Expression);
                    lkb_verDetalle.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                    Expression = "CVE_ACTIVIDAD = 'INIC2'";
                    foundRows = dtAccesos.Select(Expression);
                    lkb_Excel.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);
                }
            }
        }

        #endregion
        
    }
}