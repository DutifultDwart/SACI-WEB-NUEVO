using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using SACI.Negocio;
using SACI_MEX.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace SACI_MEX.Formularios
{
    public partial class catReglaOrigen : System.Web.UI.Page
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
                    catReglasOrigen(1);
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
                    Session["grvReglasOrigen"] = null;
                    Session["EditarRO"] = null;
                }


                //Cuando se quiera filtrar el Grid entra en el if
                if (Session["grvReglasOrigen"] != null)
                {
                    grvReglasOrigen.DataSource = Session["grvReglasOrigen"];
                    grvReglasOrigen.DataBind();
                    grvReglasOrigen.SettingsPager.PageSize = GridPageSize;
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

        protected void cbpReglaorigen_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                hdnGuardar.Value = "0";
                if (e.Parameter.ToString() == "nuevo")
                {
                    hdnGuardar.Value = "3";
                    LimpiaControles();
                    //NuevoRegistro();
                    titNewTratado.InnerHtml = "Nueva Regla de Origen";
                }
                else if (e.Parameter.ToString() == "editar")
                {
                    if (grvReglasOrigen.GetSelectedFieldValues("Regla_Key").Count > 0)
                    {
                        Session["EditarRO"] = true;
                        hdnGuardar.Value = "3";
                        NuevoRegistro();
                        titNewTratado.InnerHtml = "Editar Regla de Origen";
                        TXT_RKey.Text = grvReglasOrigen.GetSelectedFieldValues("Regla_Key")[0].ToString().Trim();
                        hdnKey.Value = grvReglasOrigen.GetSelectedFieldValues("Regla_Key")[0].ToString().Trim();
                        TXT_TRATADO.Text = grvReglasOrigen.GetSelectedFieldValues("Tratado")[0].ToString().Trim();
                        TXT_CAPITULO.Text = grvReglasOrigen.GetSelectedFieldValues("Capitulo")[0].ToString().Trim();
                        TXT_CONDICION.Text = grvReglasOrigen.GetSelectedFieldValues("Condicion")[0].ToString().Trim();
                        TXT_REGLA.Text = grvReglasOrigen.GetSelectedFieldValues("Regla")[0].ToString().Trim();
                        TXT_NOTAID.Text = grvReglasOrigen.GetSelectedFieldValues("Nota_Id")[0].ToString().Trim();
                    }
                    else
                    {
                        hdnGuardar.Value = "2";
                        AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
                    }
                }
                else if (e.Parameter.ToString() == "borrar")
                {
                    if (grvReglasOrigen.GetSelectedFieldValues("Regla_Key").Count > 0)
                    {
                        hdnGuardar.Value = "1";
                        catReglasOrigen(4, Convert.ToInt32(grvReglasOrigen.GetSelectedFieldValues("Regla_Key")[0].ToString().Trim()));
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


                    if (!Convert.ToBoolean(Session["EditarRO"]))
                    {
                        //Inserta
                        catReglasOrigen(2, 0, TXT_TRATADO.Text.Trim(), TXT_CAPITULO.Text.Trim(), TXT_CONDICION.Text.Trim(), TXT_REGLA.Text.Trim(), TXT_NOTAID.Text.Trim());
                    }
                    else
                    {
                        //Actualiza
                        catReglasOrigen(3, Convert.ToInt32(hdnKey.Value.ToString()), TXT_TRATADO.Text.Trim(), TXT_CAPITULO.Text.Trim(), TXT_CONDICION.Text.Trim(), TXT_REGLA.Text.Trim(), TXT_NOTAID.Text.Trim());
                        Session["EditarRO"] = false;
                    }
                    AlertSucces(MsgRegistros.MsgRegistroAgregar);
                }
                else if (e.Parameter.ToString() == "buscar")
                {
                    if (TXT_TRATADOB.Text.Trim() == string.Empty || TXT_REGLAB.Text.Trim() == string.Empty)
                    {
                        hdnGuardar.Value = "2";
                        AlertErrorUsuario("Debe ingresar los filtros de busqueda.");
                    }
                    else
                    {
                        catReglasOrigen(5, 0, "", "0", "", "", "0", TXT_TRATADOB.Text.Trim(), TXT_REGLAB.Text.Trim());
                    }
                    
                }
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

        #endregion

        #region METODOS

        public void LimpiaControles()
        {
            TXT_RKey.Text = string.Empty;
            TXT_CAPITULO.Text = string.Empty;
            TXT_CONDICION.Text = string.Empty;
            TXT_NOTAID.Text = string.Empty;
            TXT_REGLA.Text = string.Empty;
            TXT_TRATADO.Text = string.Empty;
        }

        protected int GridPageSize
        {
            get
            {
                if (Session[PageSizeSessionKey] == null)
                    return grvReglasOrigen.SettingsPager.PageSize;
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
                DataTable dtAccesos = new DataTable("CRORIG");
                dtAccesos = RNCat.ACCESOS_USUARIOS(IDPERFIL, "CRORIG");

                // Presuming the DataTable has a column named Date.
                string Expression;
                Expression = "CVE_ACTIVIDAD = 'CRORIG1'";
                DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = dtAccesos.Select(Expression);
                lkb_Nuevo.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CRORIG2'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Editar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CRORIG3'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Eliminar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CRORIG4'";
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


        public void catReglasOrigen(int OPCION = 0, Int64 REGLA_KEY = 0, string TRATADO = "", string CAPITULO = "0", string CONDICION = "", string REGLA = "", string NOTA_ID = "0", string TRADADO_BUSQUEDA = "", string REGLA_BUSQUEDA = "")
        {
            DataTable dt = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvReglasOrigen"] = dt = RNCat.CatalogoReglaOrigen(OPCION, REGLA_KEY, TRATADO, CAPITULO, CONDICION, REGLA, NOTA_ID, TRADADO_BUSQUEDA, REGLA_BUSQUEDA);
                grvReglasOrigen.DataSource = dt;
                grvReglasOrigen.DataBind();
                grvReglasOrigen.Settings.VerticalScrollableHeight = 300;
                grvReglasOrigen.SettingsPager.PageSize = 15;

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
            options.SheetName = "Reglas Origen";

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