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
    public partial class catNota : System.Web.UI.Page
    {
        #region PROPIEDADES GLOBALES
        RNCatalogos RNCat = null;
        const string PageSizeSessionKey = "ed5e843d-cff7-47a7-815e-832923f7fb09";
        #endregion
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ConfigurationManager.AppSettings["BDActivo"] = Session["idBase"].ToString();
                    cat_Nota(1);
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
                    string mensaje = string.Format("Error en la pantalla: {0}. {1} {2} {3} {4}. {5} {6}. {7} {8}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
                    Environment.NewLine, "Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    Environment.NewLine, "Recurso: " + ex.Source,
                    Environment.NewLine, "Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
                    Session["ErrorSql"] = mensaje;

                    ErrorAlert(mensaje);
                }
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    Session["grvNota"] = null;
                    Session["EditarNota"] = null;
                }


                //Cuando se quiera filtrar el Grid entra en el if
                if (Session["grvNota"] != null)
                {
                    grvNota.DataSource = Session["grvNota"];
                    grvNota.DataBind();
                    grvNota.SettingsPager.PageSize = GridPageSize;
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
                    string mensaje = string.Format("Error en la pantalla: {0}. {1} {2} {3} {4}. {5}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
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
        protected void cpbNota_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                hdnGuardar.Value = "0";
                if (e.Parameter.ToString() == "nuevo")
                {
                    hdnGuardar.Value = "3";
                    LimpiaControles();
                    NuevoRegistro();
                    titNewNota.InnerHtml = "Nueva Nota";
                }
                else if (e.Parameter.ToString() == "editar")
                {
                    if (grvNota.GetSelectedFieldValues("Notas_Key").Count > 0)
                    {
                        Session["EditarNota"] = true;
                        hdnGuardar.Value = "3";
                        NuevoRegistro();
                        titNewNota.InnerHtml = "Editar Nota";
                        TXT_NOTAS_Key.Text = grvNota.GetSelectedFieldValues("Notas_Key")[0].ToString().Trim();
                        hdnKey.Value = grvNota.GetSelectedFieldValues("Notas_Key")[0].ToString().Trim();
                        TXT_TRATADO.Text = grvNota.GetSelectedFieldValues("Tratado")[0].ToString().Trim();
                        TXT_NOTA_Id.Text = grvNota.GetSelectedFieldValues("Nota_Id")[0].ToString().Trim();
                        TXT_NOTA_TEXTO.Text = grvNota.GetSelectedFieldValues("Nota_Texto")[0].ToString().Trim();
                    }
                    else
                    {
                        hdnGuardar.Value = "2";
                        AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
                    }
                }
                else if (e.Parameter.ToString() == "borrar")
                {
                    if (grvNota.GetSelectedFieldValues("Notas_Key").Count > 0)
                    {
                        hdnGuardar.Value = "1";
                        cat_Nota(4, Convert.ToInt32(grvNota.GetSelectedFieldValues("Notas_Key")[0].ToString().Trim()));
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


                    if (!Convert.ToBoolean(Session["EditarNota"]))
                    {
                        //Inserta
                        cat_Nota(2, 0, TXT_TRATADO.Text.Trim(), Convert.ToInt64(TXT_NOTA_Id.Text.Trim()), TXT_NOTA_TEXTO.Text.Trim());
                    }
                    else
                    {
                        //Actualiza
                        cat_Nota(3, Convert.ToInt32(hdnKey.Value.ToString()), TXT_TRATADO.Text.Trim(), Convert.ToInt64(TXT_NOTA_Id.Text.Trim()), TXT_NOTA_TEXTO.Text.Trim());
                        Session["EditarNota"] = false;
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
                    string mensaje = string.Format("Error en la pantalla: {0}. {1} {2} {3} {4}. {5}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
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
        #endregion
        #region METODOS
        public void cat_Nota(int OPCION = 0, Int64 Notas_Key = 0, string TRATADO = "", Int64 Nota_Id = 0, string Nota_Texto = "")
        {
            DataTable dt = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvNota"] = dt = RNCat.CatalogoNota(OPCION, Notas_Key, TRATADO, Nota_Id, Nota_Texto);
                grvNota.DataSource = dt;
                grvNota.DataBind();
                grvNota.Settings.VerticalScrollableHeight = 300;
                grvNota.SettingsPager.PageSize = 15;

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
        public void AccesosUsuario()
        {
            RNCat = new RNCatalogos();
            // **********************************************************
            // *************************** ACCESOS
            int IDPERFIL = 0;
            if (Session["IDPerfil"] != null)
                IDPERFIL = int.Parse(Session["IDPerfil"].ToString());
            if (IDPERFIL != 1)
            {
                DataTable dtAccesos = new DataTable("CNOTA");
                dtAccesos = RNCat.ACCESOS_USUARIOS(IDPERFIL, "CNOTA");

                // Presuming the DataTable has a column named Date.
                string Expression;
                Expression = "CVE_ACTIVIDAD = 'CNOTA1'";
                DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = dtAccesos.Select(Expression);
                lkb_Nuevo.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CNOTA2'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Editar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CNOTA3'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Eliminar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CNOTA4'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Excel.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);
            }

        }
        public void AlertErrorUsuario(string mensaje)
        {
            p2.InnerText = mensaje;
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "", "<script> document.getElementById('btnErrorUser').setAttribute('data-whatever', '" + mensaje + "'); document.getElementById('btnErrorUser').click(); </script> ", false);
        }
        public void ErrorAlert(string mensaje)
        {
            //pModal.InnerText = "\n"+ mensaje;
            txtArea.Value = mensaje.Replace("|", "\n");
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "", "<script> document.getElementById('btnError').setAttribute('data-whatever', '" + mensaje + "'); document.getElementById('btnError').click(); </script> ", false);
        }
        protected int GridPageSize
        {
            get
            {
                if (Session[PageSizeSessionKey] == null)
                    return grvNota.SettingsPager.PageSize;
                return (int)Session[PageSizeSessionKey];
            }
            set { Session[PageSizeSessionKey] = value; }
        }
        public void LimpiaControles()
        {
            TXT_NOTAS_Key.Text = string.Empty;
            TXT_TRATADO.Text = string.Empty;
            TXT_NOTA_Id.Text = string.Empty;
            TXT_NOTA_TEXTO.Text = string.Empty;
        }
        public void NuevoRegistro()
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "MostrarModal", "<script> document.getElementById('btnNuevo').click(); </script> ", false);
        }
        public void AlertSucces(string mensaje)
        {
            pSucces.InnerText = mensaje;
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "", "<script> document.getElementById('btnSucces').setAttribute('data-whatever', '" + mensaje + "'); document.getElementById('btnSucces').click(); </script> ", false);
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
            options.SheetName = "Nota";

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