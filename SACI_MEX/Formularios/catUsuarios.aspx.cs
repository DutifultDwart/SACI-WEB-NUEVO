using DevExpress.XtraPrinting;
using SACI.Negocio;
using SACI_MEX.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SecurityRiosul;
using System.Configuration;
using DevExpress.Web;

namespace SACI_MEX.Formularios
{
    public partial class catUsuarios : System.Web.UI.Page
    {

        #region PROPIEDADES GLOBALES

        RNCatalogos RNCat = null;
        const string PageSizeSessionKey = "ed5e843d-cff7-47a7-815e-832923f7fb09";
        AES pass = new AES();

        #endregion



        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ConfigurationManager.AppSettings["BDActivo"] = Session["idBase"].ToString();
                    CatalogoUsuarios(1);
                    CatalogoPerfiles(1);
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
                    Session["grvUsuarios"] = null;

                }

                //Cuando se quiera filtrar el Grid entra en el if
                if (Session["grvUsuarios"] != null)
                {
                    grvUsuarios.DataSource = Session["grvUsuarios"];
                    grvUsuarios.DataBind();
                    grvUsuarios.SettingsPager.PageSize = GridPageSize;
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

        protected void lkb_Nuevo_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiaControles();
                NuevoRegistro();
                DivBloqueo.Visible = false;

                RNCat = new RNCatalogos();
                DataTable dtAgente = new DataTable();
                Session["grvPlantas"] = dtAgente = RNCat.CatDivisionYAlmacen(2);


                ASPxDropDownEdit dde = (ASPxDropDownEdit)this.FindControl("ctl00$MainContent$dde_filtros");
                ASPxListBox lb = (ASPxListBox)dde.FindControl("listBox");

                lb.Items.Clear();



                for (int i = 0; i < dtAgente.Rows.Count; i++)
                {



                    ListEditItem itemN = new ListEditItem();
                    itemN.Text = dtAgente.Rows[i]["ALMACEN"].ToString().Trim();
                    itemN.Value = dtAgente.Rows[i]["ALMACEN"].ToString().Trim();
                    itemN.Selected = false;
                    lb.Items.Add(itemN);


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

        protected void lkb_Editar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grvUsuarios.GetSelectedFieldValues("PK_ID_USER").Count > 0)
                {
                    NuevoRegistro();

                    TXT_PK_ID_USER.Text = grvUsuarios.GetSelectedFieldValues("PK_ID_USER")[0].ToString().Trim();
                    TXT_CVE_USER.Text = grvUsuarios.GetSelectedFieldValues("CVE_USER")[0].ToString().Trim();
                    TXTPWD_USER.Text = pass.Desencriptar(grvUsuarios.GetSelectedFieldValues("PWD_USER")[0].ToString().Trim());
                    TXT_NOMBRE.Text = grvUsuarios.GetSelectedFieldValues("NOM_USER")[0].ToString().Trim();
                    TXT_DES_MAIL_USER.Text = grvUsuarios.GetSelectedFieldValues("DES_MAIL_USER")[0].ToString().Trim();
                    cmbVigencia.Text = grvUsuarios.GetSelectedFieldValues("VIGENCIA_TXT")[0].ToString().Trim();
                    
                    if (grvUsuarios.GetSelectedFieldValues("AC")[0].ToString().Trim().ToUpper().Equals("SI"))
                        chkMail.Checked = true;
                    else
                        chkMail.Checked = false;
                    CMB_PERFILES.Value = grvUsuarios.GetSelectedFieldValues("FK_ID_PERFIL_USER")[0].ToString().Trim();
                    //Se ejecuta funcion de javascript que valida la contraseña actual
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "UpdateIndicator();", true);
                    TXT_TEL.Text = grvUsuarios.GetSelectedFieldValues("NUMERO_CEL")[0].ToString().Trim();
                    if (grvUsuarios.GetSelectedFieldValues("SMS")[0].ToString().Trim().ToUpper().Equals("SI"))
                        chk_SMS.Checked = true;
                    else
                        chk_SMS.Checked = false;

                    DivBloqueo.Visible = true;
                    if (grvUsuarios.GetSelectedFieldValues("BLOQUEADO")[0].ToString().Trim().ToUpper().Equals("SI"))
                        chk_Bloqueo.Checked = true;
                    else
                        chk_Bloqueo.Checked = false;

                    CatalogoPlantas(2);




                }
                else
                {
                    AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
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

        protected void lkb_Eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grvUsuarios.GetSelectedFieldValues("PK_ID_USER").Count > 0)
                {
                    AlertQuestion(MsgRegistros.MsgConfirmaElimna);
                }
                else
                {
                    AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
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
                if (grvUsuarios.VisibleRowCount > 0)
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

        protected void btnAceptarDel_Click(object sender, EventArgs e)
        {
            DataTable dtCtes = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvUsuarios"] = dtCtes = RNCat.CatalogoUsuarios(4, Convert.ToInt32(grvUsuarios.GetSelectedFieldValues("PK_ID_USER")[0].ToString().Trim()));
                grvUsuarios.DataSource = dtCtes;
                grvUsuarios.DataBind();
                AlertSucces(MsgRegistros.MsgRegistroElimina);
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
                dtCtes.Dispose();
                GC.Collect();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int chk_sms = 0;
                if (chk_SMS.Checked)
                    chk_sms = 1;

                int chk_bloqueo = 0;
                if (chk_Bloqueo.Checked)
                    chk_bloqueo = 1;

                string chk_correo = "No";
                if (chkMail.Checked)
                    chk_correo = "Si";

                int vigencia = int.Parse(cmbVigencia.Value.ToString());

                CatalogoUsuarios(3, Convert.ToInt32(TXT_PK_ID_USER.Text.Trim()), TXT_CVE_USER.Text.Trim(), pass.Encriptar(TXTPWD_USER.Text.Trim()), TXT_NOMBRE.Text.Trim(), TXT_DES_MAIL_USER.Text.Trim(), true, Convert.ToInt32(CMB_PERFILES.Value), TXT_TEL.Text.Trim(), chk_sms, chk_bloqueo, chk_correo, dde_filtros.Text.Trim(), vigencia);
                AlertSucces(MsgRegistros.MsgRegistroAgregar);
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



        #region METODOS


        public void CatalogoUsuarios(int OPCION = 0, int PK_ID_USER = 0, string CVE_USER = "", string PWD_USER = "", string NOM_USER = "", string DES_MAIL_USER = "", bool STA_USUARIO = false, int FK_ID_PERFIL_USER = 0, string TEL = "", int SMS = 0, int BLOQUEO = 0, string CHK_MAIL = "", string PLANTAS = "", int VIGENCIA = 0)
        {
            DataTable dtAgente = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvUsuarios"] = dtAgente = RNCat.CatalogoUsuarios(OPCION, PK_ID_USER, CVE_USER, PWD_USER, NOM_USER, DES_MAIL_USER, STA_USUARIO, FK_ID_PERFIL_USER, TEL, SMS, BLOQUEO, CHK_MAIL, PLANTAS, VIGENCIA);
                grvUsuarios.DataSource = dtAgente;
                grvUsuarios.DataBind();
                LimpiaControles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtAgente.Dispose();
                GC.Collect();
            }
        }



        public void LimpiaControles()
        {
            try
            {
                TXT_PK_ID_USER.Text = "0";
                TXT_CVE_USER.Text = string.Empty;
                TXTPWD_USER.Text = string.Empty;
                TXT_NOMBRE.Text = string.Empty;
                TXT_DES_MAIL_USER.Text = string.Empty;
                TXT_TEL.Text = string.Empty;
                chk_SMS.Checked = false;
                chkMail.Checked = false;
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


        public void AlertQuestion(string mensaje)
        {

            pModalQuestion.InnerText = mensaje;

            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "", "<script> document.getElementById('btnQuestion').setAttribute('data-whatever', '" + mensaje + "'); document.getElementById('btnQuestion').click(); </script> ", false);

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
                    return grvUsuarios.SettingsPager.PageSize;
                return (int)Session[PageSizeSessionKey];
            }
            set { Session[PageSizeSessionKey] = value; }
        }



        public void CatalogoPerfiles(int OPCION = 0, int PK_ID_PERFIL = 0, string CVE_PERFIL = "", string NOM_PERFIL = "")
        {
            try
            {
                RNCat = new RNCatalogos();
                CMB_PERFILES.DataSource = RNCat.CatPerfiles(OPCION, PK_ID_PERFIL, CVE_PERFIL, NOM_PERFIL);
                CMB_PERFILES.DataBind();
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

        public void AccesosUsuario()
        {
            RNCat = new RNCatalogos();
            // **********************************************************
            // *************************** ACCESOS
            int IDPERFIL = 0;
            if (Session["IDPerfilSACI"] != null)
                IDPERFIL = int.Parse(Session["IDPerfilSACI"].ToString());
            //VALIDA PERMISOS USUARIO
            if (IDPERFIL != 1)
            {
                DataTable dtAccesos = new DataTable("CUSR");
                dtAccesos = RNCat.ACCESOS_USUARIOS(IDPERFIL, "CUSR");

                // Presuming the DataTable has a column named Date.
                string Expression;
                Expression = "CVE_ACTIVIDAD = 'CUSR1'";
                DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = dtAccesos.Select(Expression);
                lkb_Nuevo.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CUSR2'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Editar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CUSR3'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Eliminar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CUSR4'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Excel.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);
            }
        }


        public void CatalogoPlantas(int OPCION = 0, Int64 AKEY = 0, string ALMACEN = "", string DESCRIPCION = "")
        {
            DataTable dtAgente = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvPlantas"] = dtAgente = RNCat.CatDivisionYAlmacen(OPCION, AKEY, ALMACEN, DESCRIPCION);


                ASPxDropDownEdit dde = (ASPxDropDownEdit)this.FindControl("ctl00$MainContent$dde_filtros");
                ASPxListBox lb = (ASPxListBox)dde.FindControl("listBox");

                lb.Items.Clear();
                DataTable dtPlantas = new DataTable();
                RNCat = new RNCatalogos();
                dtPlantas = RNCat.TraePlantasPorUsuario(TXT_CVE_USER.Text);

                bool select = false;
                


                for (int i = 0; i < dtAgente.Rows.Count; i++)
                {

                    select = false;
                    for (int p = 0; p < dtPlantas.Rows.Count; p++)
                    {

                        if (dtPlantas.Rows[p]["Nombre"].ToString().Trim() == dtAgente.Rows[i]["ALMACEN"].ToString().Trim())
                        {
                            select = true;
                        }                                      
                    }

                    ListEditItem itemN = new ListEditItem();
                    itemN.Text = dtAgente.Rows[i]["ALMACEN"].ToString().Trim();
                    itemN.Value = dtAgente.Rows[i]["ALMACEN"].ToString().Trim();
                    itemN.Selected = select;
                    lb.Items.Add(itemN);




                }










            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtAgente.Dispose();
                GC.Collect();
            }
        }


        #endregion

    }
}