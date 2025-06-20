using DevExpress.XtraPrinting;
using SACI.Negocio;
using SACI_MEX.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SACI_MEX.Formularios
{
    public partial class Cat_Actividades : System.Web.UI.Page
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
                    CatalogoActividades(1);
                    Session["Editar"] = false;
                    AccesosUsuario();
                }
                lkb_Nuevo.Attributes.Add("onClick", "return false;");
                lkb_Editar.Attributes.Add("onClick", "return false;");
                lkb_Eliminar.Attributes.Add("onClick", "return false;");
                btnAceptarDel.Attributes.Add("onClick", "return false;");
                btnGuardar.Attributes.Add("onClick", "return false;");
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    AlertErrorUsuario(ex.Message.ToString());
                }
                else
                {
                    LimpiaControles();
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
                    Session["grvAcct"] = null;

                }

                //Cuando se quiera filtrar el Grid entra en el if
                if (Session["grvAcct"] != null)
                {
                    grvActividad.DataSource = Session["grvAcct"];
                    grvActividad.DataBind();
                    grvActividad.SettingsPager.PageSize = GridPageSize;
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
                    LimpiaControles();
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
                if (grvActividad.VisibleRowCount > 0)
                {
                    Exporter.WriteXlsxToResponse(h1_titulo.InnerText, new XlsxExportOptionsEx() { SheetName = h1_titulo.InnerText });
                }
                else
                {
                    string error = "No hay información para exportar";
                    if (Session["Traducciones"] != null)
                        try { error = ((DataTable)Session["Traducciones"]).Select("Name ='alert_no_hay_informacion_para_exportar'")[0]["Value"].ToString(); }
                        catch { }
                    AlertErrorUsuario(error);
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
                    LimpiaControles();
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


        protected void cbpActividades_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                DataTable dtAcct = new DataTable();
                hdnGuardar.Value = "0";
                if (e.Parameter.ToString() == "nuevaActividad")
                {
                    hdnGuardar.Value = "2";
                    LimpiaControles();
                    NuevoRegistro();
                    titNewActividad.InnerText = "Nueva actividad";
                }
                else if (e.Parameter.ToString() == "editarActividad")
                {
                    if (grvActividad.GetSelectedFieldValues("PK_ID_ACTIVIDAD").Count > 0)
                    {
                        hdnGuardar.Value = "2";
                        titNewActividad.InnerText = "Editar actividad";
                        NuevoRegistro();
                        Session["Editar"] = true;
                        hdnIdActividad.Value = grvActividad.GetSelectedFieldValues("PK_ID_ACTIVIDAD")[0].ToString().Trim();
                        TXT_CVE_ACTIVIDAD.Text = grvActividad.GetSelectedFieldValues("CVE_ACTIVIDAD")[0].ToString().Trim();
                        TXT_NOM_ACTIVIDAD.Text = grvActividad.GetSelectedFieldValues("NOM_ACTIVIDAD")[0].ToString().Trim();
                    }
                    else
                    {
                        hdnGuardar.Value = "3";
                        AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
                    }
                }
                else if (e.Parameter.ToString() == "borrarActividad")
                {
                    hdnGuardar.Value = "1";
                    RNCat = new RNCatalogos();
                    Session["grvAcct"] = dtAcct = RNCat.CatActividades(3, Convert.ToInt32(grvActividad.GetSelectedFieldValues("PK_ID_ACTIVIDAD")[0].ToString().Trim()));
                    grvActividad.DataSource = dtAcct;
                    grvActividad.DataBind();
                    grvActividad.SettingsPager.PageSize = 15;
                    grvActividad.Settings.VerticalScrollableHeight = 330;
                    AlertSucces(MsgRegistros.MsgRegistroElimina);
                }
                else if (e.Parameter.ToString() == "guardarActividad")
                {
                    hdnGuardar.Value = "1";
                    if (!Convert.ToBoolean(Session["Editar"]))
                    {
                        CatalogoActividades(2, 0, TXT_CVE_ACTIVIDAD.Text.Trim(), TXT_NOM_ACTIVIDAD.Text.Trim());
                    }
                    else
                    {
                        CatalogoActividades(2, Convert.ToInt32(hdnIdActividad.Value), TXT_CVE_ACTIVIDAD.Text.Trim(), TXT_NOM_ACTIVIDAD.Text.Trim());
                        Session["Editar"] = false;
                    }
                    AlertSucces(MsgRegistros.MsgRegistroAgregar);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    hdnGuardar.Value = "3";
                    //AlertErrorUsuario(ex.Message.ToString());
                    lblRepetido.Visible = true;
                }
                else
                {
                    hdnGuardar.Value = "4";
                    LimpiaControles();
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



        public void CatalogoActividades(int OPCION = 0, int PK_ID_ACTIVIDAD = 0, string CVE_ACTIVIDAD = "", string NOM_ACTIVIDAD = "")
        {
            DataTable dtAcc = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvAcct"] = dtAcc = RNCat.CatActividades(OPCION, PK_ID_ACTIVIDAD, CVE_ACTIVIDAD, NOM_ACTIVIDAD);

                grvActividad.DataSource = dtAcc;
                grvActividad.DataBind();
                grvActividad.SettingsPager.PageSize = 15;
                grvActividad.Settings.VerticalScrollableHeight = 330;
                LimpiaControles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtAcc.Dispose();
                GC.Collect();
            }
        }



        public void LimpiaControles()
        {
            try
            {
                TXT_ID_ACTIVIDAD.Text = "0";
                TXT_CVE_ACTIVIDAD.Text = string.Empty;
                TXT_NOM_ACTIVIDAD.Text = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
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

        //Metodo que muestra ventana de alerta
        public void AlertSucces(string mensaje)
        {
            pSucces.InnerText = mensaje;
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "", "<script> document.getElementById('btnSucces').setAttribute('data-whatever', '" + mensaje + "'); document.getElementById('btnSucces').click(); </script> ", false);
        }


        public void NuevoRegistro()
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "MostrarModal", "<script> document.getElementById('btnNuevo').click(); </script> ", false);
        }



        /// <summary>
        /// Propiedad GridPageSize
        /// </summary>
        protected int GridPageSize
        {
            get
            {
                if (Session[PageSizeSessionKey] == null)
                    return grvActividad.SettingsPager.PageSize;
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
                DataTable dtAccesos = new DataTable("CACT");
                dtAccesos = RNCat.ACCESOS_USUARIOS(IDPERFIL, "CACT");

                // Presuming the DataTable has a column named Date.
                string Expression;
                Expression = "CVE_ACTIVIDAD = 'CACT1'";
                DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = dtAccesos.Select(Expression);
                lkb_Nuevo.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CACT2'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Editar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CACT3'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Eliminar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CACT4'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Excel.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);
            }
        }

        #endregion

        






    }
}