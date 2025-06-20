using DevExpress.XtraPrinting;
using SACI.Negocio;
using SACI_MEX.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using DevExpress.XtraPrintingLinks;
using System.Drawing;
using System.Configuration;


namespace SACI_MEX.Formularios
{
    public partial class catProveedores : System.Web.UI.Page
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
                    ConfigurationManager.AppSettings["BDActivo"] = Session["idBase"].ToString();
                    CatalogosProveedores(2, 0, "", "", "", "", "", "", "", "", "", "", "", "", "", 0, "", "", "", 0, "", "", "", "", "", Session["Usuario"].ToString().Trim(), "");
                    CatAlmacen();
                    AccesosUsuario();
                    CARGAALMACEN();

                    //if (Session["Usuario"] != null)
                    //{
                    //    hf_Usuario.Value = Session["Usuario"].ToString().Trim();
                    //    RNCat = new RNCatalogos();
                    //    cmbPlantas.DataSource = Session["cPlantas"] = RNCat.TraePlantasPorUsuario(hf_Usuario.Value);
                    //    cmbPlantas.DataBind();
                    //}
                    //else
                    //{
                    //    hf_Usuario.Value = "";
                    //    cmbPlantas.DataSource = null;
                    //    cmbPlantas.DataBind();
                    //}


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
                    Session["grvProveedores"] = null;
                    //Session["cPlantas"] = null;
                }

                //if (Session["cPlantas"] != null)
                //{
                //    cmbPlantas.DataSource = Session["cPlantas"];
                //    cmbPlantas.DataBind();
                //}


                //Cuando se quiera filtrar el Grid entra en el if
                if (Session["grvProveedores"] != null)
                {
                    grvProveedores.DataSource = Session["grvProveedores"];
                    grvProveedores.DataBind();
                    grvProveedores.SettingsPager.PageSize = GridPageSize;
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
                //LimpiaControles();
                //NuevoRegistro();
                //Session["titNewProveedor"] = titNewProveedor.InnerHtml = "Nuevo Proveedor";
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

                //if (grvProveedores.GetSelectedFieldValues("PKey").Count > 0)
                //{
                //    NuevoRegistro();
                //    Session["titNewProveedor"] = titNewProveedor.InnerHtml = "Editar Proveedor";

                //    TXT_PKey.Text = grvProveedores.GetSelectedFieldValues("PKey")[0].ToString().Trim();
                //    TXT_Clave.Text = grvProveedores.GetSelectedFieldValues("Clave")[0].ToString().Trim();
                //    TXT_NOMBRE.Text = grvProveedores.GetSelectedFieldValues("Nombre")[0].ToString().Trim();
                //    TXT_Idfiscal.Text = grvProveedores.GetSelectedFieldValues("Idfiscal")[0].ToString().Trim();
                //    TXT_Tipone.Value = grvProveedores.GetSelectedFieldValues("Tipone")[0].ToString().Trim();
                //    TXT_Programa.Text = grvProveedores.GetSelectedFieldValues("Programa")[0].ToString().Trim();
                //    TXT_CalleNumero.Text = grvProveedores.GetSelectedFieldValues("CalleNumero")[0].ToString().Trim();
                //    TXT_Codigo.Text = grvProveedores.GetSelectedFieldValues("Codigo")[0].ToString().Trim();
                //    TXT_Colonia.Text = grvProveedores.GetSelectedFieldValues("Colonia")[0].ToString().Trim();
                //    TXT_Entidad.Text = grvProveedores.GetSelectedFieldValues("Entidad")[0].ToString().Trim();
                //    TXT_Pais.Text = grvProveedores.GetSelectedFieldValues("Pais")[0].ToString().Trim();
                //    TXT_Telefono.Text = grvProveedores.GetSelectedFieldValues("Telefono")[0].ToString().Trim();
                //    TXT_Correo.Text = grvProveedores.GetSelectedFieldValues("Correo")[0].ToString().Trim();
                //    TXT_Fax.Text = grvProveedores.GetSelectedFieldValues("Fax")[0].ToString().Trim();                    
                //    //CMB_ALMACEN.Value = grvProveedores.GetSelectedFieldValues("ALMACENKEY")[0].ToString().Trim();
                //    //TXT_ApellidoPaterno.Text = grvProveedores.GetSelectedFieldValues("ApellidoPaterno")[0].ToString().Trim();
                //    //TXT_ApellidoMaterno.Text = grvProveedores.GetSelectedFieldValues("ApellidoMaterno")[0].ToString().Trim();
                //    //TXT_calle.Text = grvProveedores.GetSelectedFieldValues("calle")[0].ToString().Trim();
                //    TXT_callenumerointerior.Text = grvProveedores.GetSelectedFieldValues("callenumerointerior")[0].ToString().Trim();
                //    TXT_localidad.Text = grvProveedores.GetSelectedFieldValues("localidad")[0].ToString().Trim();
                //    //TXT_referencia.Text = grvProveedores.GetSelectedFieldValues("referencia")[0].ToString().Trim();
                //    TXT_municipio.Text = grvProveedores.GetSelectedFieldValues("municipio")[0].ToString().Trim();
                //    //TXT_tipoidentificador.Text = grvProveedores.GetSelectedFieldValues("tipoidentificador")[0].ToString().Trim();
                //    //TXT_codigopostal.Text = grvProveedores.GetSelectedFieldValues("codigopostal")[0].ToString().Trim();

                //    TXT_Clave.IsValid = true;
                //    TXT_NOMBRE.IsValid = true;
                //    TXT_Tipone.IsValid = true;

                //}
                //else
                //{
                //    AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
                //}

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


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //int valida = 0;


                //if (TXT_Clave.Text.Trim().Length == 0)
                //{
                //    valida = 1;
                //    TXT_Clave.IsValid = false;
                //}
                //if (TXT_NOMBRE.Text.Trim().Length == 0)
                //{
                //    valida = 1;
                //    TXT_NOMBRE.IsValid = false;
                //}
                //if (TXT_Tipone.Text.Trim().Length == 0 || TXT_Tipone.SelectedIndex.Equals(-1))
                //{
                //    valida = 1;
                //    TXT_Tipone.IsValid = false;
                //}


                //if (valida.Equals(1))
                //{
                //    NuevoRegistro();

                //    if (Session["titNewProveedor"] != null)
                //        titNewProveedor.InnerHtml = Session["titNewProveedor"].ToString();

                //    return;
                //}



                //int num_Int;

                //if(string.IsNullOrEmpty(TXT_callenumerointerior.Text.Trim()))
                //{
                //    num_Int = 0;
                //}
                //else
                //{
                //    num_Int = Convert.ToInt32(TXT_callenumerointerior.Text.Trim());
                //}

                ////CatalogosProveedores(1, Convert.ToInt32(TXT_PKey.Text.Trim()), TXT_Clave.Text.Trim(), TXT_NOMBRE.Text.Trim(), TXT_Idfiscal.Text.Trim(), TXT_Tipone.Value.ToString().Trim(), TXT_Programa.Text.Trim(), TXT_CalleNumero.Text.Trim(), TXT_Codigo.Text.Trim(), TXT_Colonia.Text.Trim(), TXT_Entidad.Text.Trim(), TXT_Pais.Text.Trim(), TXT_Telefono.Text.Trim(), TXT_Correo.Text.Trim(), TXT_Fax.Text.Trim(), 0, TXT_ApellidoPaterno.Text.Trim(), TXT_ApellidoMaterno.Text.Trim(), TXT_calle.Text.Trim(), num_Int, TXT_localidad.Text.Trim(), TXT_referencia.Text.Trim(), TXT_municipio.Text.Trim(), TXT_tipoidentificador.Text.Trim(), TXT_codigopostal.Text.Trim());
                //CatalogosProveedores(1, Convert.ToInt32(TXT_PKey.Text.Trim()), TXT_Clave.Text.Trim(), TXT_NOMBRE.Text.Trim(), TXT_Idfiscal.Text.Trim(), TXT_Tipone.Value.ToString().Trim(), TXT_Programa.Text.Trim(), TXT_CalleNumero.Text.Trim(), TXT_Codigo.Text.Trim(), TXT_Colonia.Text.Trim(), TXT_Entidad.Text.Trim(), TXT_Pais.Text.Trim(), TXT_Telefono.Text.Trim(), TXT_Correo.Text.Trim(), TXT_Fax.Text.Trim(), 0, "", "", "", num_Int, "", "", "", "", "");                
                //AlertSucces(MsgRegistros.MsgRegistroAgregar);
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
                //if (grvProveedores.GetSelectedFieldValues("PKey").Count > 0)
                //{
                //    AlertQuestion(MsgRegistros.MsgConfirmaElimna);
                //}
                //else
                //{
                //    AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
                //}
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
                Export("xlsx");
                //if (grvProveedores.VisibleRowCount > 0)
                //{
                //    //Exporter.WriteXlsToResponse(h1_titulo.InnerText, new XlsExportOptionsEx() { SheetName = h1_titulo.InnerText });                    
                   
                //}
                //else
                //{
                //    string error = "No hay información para exportar";
                //    if (Session["Traducciones"] != null)
                //        try { error = ((DataTable)Session["Traducciones"]).Select("Name ='alert_no_hay_informacion_para_exportar'")[0]["Value"].ToString(); }
                //        catch { }
                //    AlertErrorUsuario(error);
                //}

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
            DataTable dtProv = new DataTable();
            try
            {
                //RNCat = new RNCatalogos();
                //Session["grvProveedores"] = dtProv = RNCat.CatalogosProveedores(4, Convert.ToInt32(grvProveedores.GetSelectedFieldValues("PKey")[0].ToString().Trim()));
                //grvProveedores.DataSource = dtProv;
                //grvProveedores.DataBind();
                //grvProveedores.Settings.VerticalScrollableHeight = 300;
                //grvProveedores.SettingsPager.PageSize = 15;
                //AlertSucces(MsgRegistros.MsgRegistroElimina);
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
                dtProv.Dispose();
                GC.Collect();
            }
        }


        #endregion






        #region METODOS

        public void CatAlmacen()
        {
            try
            {
                RNCat = new RNCatalogos();
                //CMB_ALMACEN.DataSource = RNCat.catAlamcen(1);
                //CMB_ALMACEN.DataBind();
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



        public void CatalogosProveedores(int OPCION = 0, int PKEY = 0, string CLAVE = "", string NOMBRE = "", string IDFISCAL = "", string TIPONE = "", string PROGRAMA = "", string CALLENUMERO = "", string CODIGO = "", string COLONIA = "", string ENTIDAD = "", string PAIS = "", string TELEFONO = "", string CORREO = "", string FAX = "", int ALMACENKEY = 0, string APATERNO = "", string AMATERNO = "", string CALLE = "", int CALLENUMINT = 0, string LOCALIDAD = "", string REFERENCIA = "", string MUNICIPIO = "", string TIPOIDENTIFICADOR = "", string CP = "", string USUARIO = "", string PLANTAS = "")
        {
            DataTable dtProv = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvProveedores"] = dtProv = RNCat.CatalogosProveedores(OPCION, PKEY, CLAVE, NOMBRE, IDFISCAL, TIPONE, PROGRAMA, CALLENUMERO, CODIGO, COLONIA, ENTIDAD, PAIS, TELEFONO, CORREO, FAX, ALMACENKEY, APATERNO, AMATERNO, CALLE, CALLENUMINT, LOCALIDAD, REFERENCIA, MUNICIPIO, TIPOIDENTIFICADOR, CP, USUARIO, PLANTAS);
                grvProveedores.DataSource = dtProv;
                grvProveedores.DataBind();
                grvProveedores.Settings.VerticalScrollableHeight = 300;
                grvProveedores.SettingsPager.PageSize = 15;
                LimpiaControles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtProv.Dispose();
                GC.Collect();
            }
        }



        public void LimpiaControles()
        {
            try
            {
                TXT_PKey.Text = "0";
                TXT_Clave.Text = string.Empty;
                TXT_NOMBRE.Text = string.Empty;
                TXT_Idfiscal.Text = string.Empty;
                TXT_Tipone.SelectedIndex = -1;
                TXT_Programa.Text = string.Empty;
                TXT_CalleNumero.Text = string.Empty;
                TXT_Codigo.Text = string.Empty;
                TXT_Colonia.Text = string.Empty;
                TXT_Entidad.Text = string.Empty;
                TXT_Pais.Text = string.Empty;
                TXT_Telefono.Text = string.Empty;
                TXT_Correo.Text = string.Empty;
                TXT_Idfiscal.Text = string.Empty;
                TXT_Fax.Text = string.Empty;                      
                //CMB_ALMACEN.SelectedIndex = -1;
                //TXT_ApellidoPaterno.Text = string.Empty;
                //TXT_ApellidoMaterno.Text = string.Empty;
                //TXT_calle.Text = string.Empty;
                TXT_callenumerointerior.Text = string.Empty;
                TXT_localidad.Text = string.Empty;
                //TXT_referencia.Text = string.Empty;
                TXT_municipio.Text = string.Empty;
                //TXT_tipoidentificador.Text = string.Empty;
                //TXT_codigopostal.Text = string.Empty;     

                TXT_Clave.IsValid = true;
                TXT_NOMBRE.IsValid = true;
                TXT_Tipone.IsValid = true;

                CMB_ALMACEN.SelectedIndex = -1;
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


        //public void AlertQuestion(string mensaje)
        //{

        //    pModalQuestion.InnerText = mensaje;

        //    ScriptManager.RegisterStartupScript(this.Page, typeof(String), "", "<script> document.getElementById('btnQuestion').setAttribute('data-whatever', '" + mensaje + "'); document.getElementById('btnQuestion').click(); </script> ", false);

        //}



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
                    return grvProveedores.SettingsPager.PageSize;
                return (int)Session[PageSizeSessionKey];
            }
            set { Session[PageSizeSessionKey] = value; }
        }

        public void CARGAALMACEN()
        {
            try
            {
                RNCat = new RNCatalogos();
                CMB_ALMACEN.DataSource = RNCat.catAlamcen(1);
                CMB_ALMACEN.DataBind();
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
            if (Session["IDPerfil"] != null)
                IDPERFIL = int.Parse(Session["IDPerfil"].ToString());
            //VALIDA PERMISOS USUARIO
            if (IDPERFIL != 1)
            {
                DataTable dtAccesos = new DataTable("CPRV");
                dtAccesos = RNCat.ACCESOS_USUARIOS(IDPERFIL, "CPRV");

                // Presuming the DataTable has a column named Date.
                string Expression;
                Expression = "CVE_ACTIVIDAD = 'CPRV1'";
                DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = dtAccesos.Select(Expression);
                lkb_Nuevo.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRV2'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Editar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRV3'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Eliminar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRV4'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Excel.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);
            }
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

                var options = new XlsxExportOptions();
                options.SheetName = "Proveedores";

                compositeLink.CreateDocument();
                using (MemoryStream stream = new MemoryStream())
                {
                    switch (format)
                    {
                        case "xlsx":
                            compositeLink.ExportToXlsx(stream, options);
                            WriteToResponse("Proveedores", true, format, stream);
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
            //e.Graph.BorderWidth = 0;
            //Rectangle r = new Rectangle(0, 0, headerImage.Width, headerImage.Height);
            //e.Graph.DrawImage(headerImage, r);
            //r = new Rectangle(0, headerImage.Height, 200, 50);
            //RNCat = new RNCatalogos();
            //dtDtsGrales = RNCat.CatDatosGenerales(1);
            //string encabezado = "";
            //if (dtDtsGrales.Rows.Count > 0)
            //{
            //    encabezado = " Empresa: " + dtDtsGrales.Rows[0]["Denominacion"].ToString().Trim() + " \n" + " RFC: " + dtDtsGrales.Rows[0]["Rfc"].ToString().Trim() + " \n" + " Registro IMMEX: " + dtDtsGrales.Rows[0]["RegistroIMMEX"].ToString().Trim() + " \n" + "Dirección: " + dtDtsGrales.Rows[0]["Dir"].ToString().Trim();
            //}
            //e.Graph.DrawString(encabezado, r);
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

        protected void cbpcatProv_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                hdnGuardar.Value = "0";
                //if (e.Parameter.ToString().Contains("BuscarPlantas"))
                //{
                //    CatalogosProveedores(2, 0, "", "", "", "", "", "", "", "", "", "", "", "", "", 0, "", "", "", 0, "", "", "", "", "", hf_Usuario.Value, cmbPlantas.Text.Trim());
                //}
                //else 
                if (e.Parameter.ToString().Contains("NuevoP"))
                {
                    hdnGuardar.Value = "3";
                    LimpiaControles();
                    NuevoRegistro();
                    Session["titNewProveedor"] = titNewProveedor.InnerHtml = "Nuevo Proveedor";
                    lblRepetido.Visible = false;
                    lblExisteProveedor.Visible = false;
                }
                else if (e.Parameter.ToString().Contains("EditarP"))
                {
                    lblRepetido.Visible = false;
                    lblExisteProveedor.Visible = false;

                    if (grvProveedores.GetSelectedFieldValues("PKey").Count > 0)
                    {
                        hdnGuardar.Value = "3";
                        NuevoRegistro();
                        Session["titNewProveedor"] = titNewProveedor.InnerHtml = "Editar Proveedor";

                        Session["PKey"] = TXT_PKey.Text = grvProveedores.GetSelectedFieldValues("PKey")[0].ToString().Trim();
                        TXT_Clave.Text = grvProveedores.GetSelectedFieldValues("Clave")[0].ToString().Trim();
                        TXT_NOMBRE.Text = grvProveedores.GetSelectedFieldValues("Nombre")[0].ToString().Trim();
                        TXT_Idfiscal.Text = grvProveedores.GetSelectedFieldValues("Idfiscal")[0].ToString().Trim();
                        TXT_Tipone.Value = grvProveedores.GetSelectedFieldValues("Tipone")[0].ToString().Trim();
                        TXT_Programa.Text = grvProveedores.GetSelectedFieldValues("Programa")[0].ToString().Trim();
                        TXT_CalleNumero.Text = grvProveedores.GetSelectedFieldValues("CalleNumero")[0].ToString().Trim();
                        TXT_Codigo.Text = grvProveedores.GetSelectedFieldValues("Codigo")[0].ToString().Trim();
                        TXT_Colonia.Text = grvProveedores.GetSelectedFieldValues("Colonia")[0].ToString().Trim();
                        TXT_Entidad.Text = grvProveedores.GetSelectedFieldValues("Entidad")[0].ToString().Trim();
                        TXT_Pais.Text = grvProveedores.GetSelectedFieldValues("Pais")[0].ToString().Trim();
                        TXT_Telefono.Text = grvProveedores.GetSelectedFieldValues("Telefono")[0].ToString().Trim();
                        TXT_Correo.Text = grvProveedores.GetSelectedFieldValues("Correo")[0].ToString().Trim();
                        TXT_Fax.Text = grvProveedores.GetSelectedFieldValues("Fax")[0].ToString().Trim();
                        //CMB_ALMACEN.Value = grvProveedores.GetSelectedFieldValues("ALMACENKEY")[0].ToString().Trim();
                        //TXT_ApellidoPaterno.Text = grvProveedores.GetSelectedFieldValues("ApellidoPaterno")[0].ToString().Trim();
                        //TXT_ApellidoMaterno.Text = grvProveedores.GetSelectedFieldValues("ApellidoMaterno")[0].ToString().Trim();
                        //TXT_calle.Text = grvProveedores.GetSelectedFieldValues("calle")[0].ToString().Trim();
                        TXT_callenumerointerior.Text = grvProveedores.GetSelectedFieldValues("callenumerointerior")[0].ToString().Trim();
                        TXT_localidad.Text = grvProveedores.GetSelectedFieldValues("localidad")[0].ToString().Trim();
                        //TXT_referencia.Text = grvProveedores.GetSelectedFieldValues("referencia")[0].ToString().Trim();
                        TXT_municipio.Text = grvProveedores.GetSelectedFieldValues("municipio")[0].ToString().Trim();
                        //TXT_tipoidentificador.Text = grvProveedores.GetSelectedFieldValues("tipoidentificador")[0].ToString().Trim();
                        //TXT_codigopostal.Text = grvProveedores.GetSelectedFieldValues("codigopostal")[0].ToString().Trim();

                        TXT_Clave.IsValid = true;
                        TXT_NOMBRE.IsValid = true;
                        TXT_Tipone.IsValid = true;

                        CMB_ALMACEN.Value = grvProveedores.GetSelectedFieldValues("ALMACENKEY")[0].ToString().Trim();
                    }
                    else
                    {
                        AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
                    }
                }
                else if (e.Parameter.ToString().Contains("BorrarP"))
                {
                    hdnGuardar.Value = "1";
                    DataTable dtProv = new DataTable();
                    RNCat = new RNCatalogos();
                    Session["grvProveedores"] = dtProv = RNCat.CatalogosProveedores(4, Convert.ToInt32(grvProveedores.GetSelectedFieldValues("PKey")[0].ToString().Trim()));
                    grvProveedores.DataSource = dtProv;
                    grvProveedores.DataBind();
                    grvProveedores.Settings.VerticalScrollableHeight = 300;
                    grvProveedores.SettingsPager.PageSize = 15;
                    AlertSucces(MsgRegistros.MsgRegistroElimina);
                }
                else if (e.Parameter.ToString().Contains("GuardarP"))
                {
                    //int valida = 0;


                    //if (TXT_Clave.Text.Trim().Length == 0)
                    //{
                    //    valida = 1;
                    //    TXT_Clave.IsValid = false;
                    //}
                    //if (TXT_NOMBRE.Text.Trim().Length == 0)
                    //{
                    //    valida = 1;
                    //    TXT_NOMBRE.IsValid = false;
                    //}
                    //if (TXT_Tipone.Text.Trim().Length == 0 || TXT_Tipone.SelectedIndex.Equals(-1))
                    //{
                    //    valida = 1;
                    //    TXT_Tipone.IsValid = false;
                    //}


                    //if (valida.Equals(1))
                    //{
                    //    NuevoRegistro();

                    //    if (Session["titNewProveedor"] != null)
                    //        titNewProveedor.InnerHtml = Session["titNewProveedor"].ToString();

                    //    return;
                    //}


                    if (Session["titNewProveedor"] != null)
                        titNewProveedor.InnerHtml = Session["titNewProveedor"].ToString();


                    int num_Int;

                    if (string.IsNullOrEmpty(TXT_callenumerointerior.Text.Trim()))
                    {
                        num_Int = 0;
                    }
                    else
                    {
                        num_Int = Convert.ToInt32(TXT_callenumerointerior.Text.Trim());
                    }

                    int almacen;
                    if (CMB_ALMACEN.Value == null)
                    {
                        almacen = 0;
                    }
                    else
                    {
                        almacen = Convert.ToInt32(CMB_ALMACEN.Value);
                    }



                    if (Session["titNewProveedor"] != null && Session["titNewProveedor"].ToString().Trim().ToUpper().Contains("NUEV"))
                        //CatalogosProveedores(1, Convert.ToInt32(TXT_PKey.Text.Trim()), TXT_Clave.Text.Trim(), TXT_NOMBRE.Text.Trim(), TXT_Idfiscal.Text.Trim(), TXT_Tipone.Value.ToString().Trim(), TXT_Programa.Text.Trim(), TXT_CalleNumero.Text.Trim(), TXT_Codigo.Text.Trim(), TXT_Colonia.Text.Trim(), TXT_Entidad.Text.Trim(), TXT_Pais.Text.Trim(), TXT_Telefono.Text.Trim(), TXT_Correo.Text.Trim(), TXT_Fax.Text.Trim(), 0, TXT_ApellidoPaterno.Text.Trim(), TXT_ApellidoMaterno.Text.Trim(), TXT_calle.Text.Trim(), num_Int, TXT_localidad.Text.Trim(), TXT_referencia.Text.Trim(), TXT_municipio.Text.Trim(), TXT_tipoidentificador.Text.Trim(), TXT_codigopostal.Text.Trim());
                        CatalogosProveedores(1, Convert.ToInt32(TXT_PKey.Text.Trim()), TXT_Clave.Text.Trim(), TXT_NOMBRE.Text.Trim(), TXT_Idfiscal.Text.Trim(), TXT_Tipone.Value.ToString().Trim(), TXT_Programa.Text.Trim(), TXT_CalleNumero.Text.Trim(), TXT_Codigo.Text.Trim(), TXT_Colonia.Text.Trim(), TXT_Entidad.Text.Trim(), TXT_Pais.Text.Trim(), TXT_Telefono.Text.Trim(), TXT_Correo.Text.Trim(), TXT_Fax.Text.Trim(), almacen, "", "", "", num_Int, TXT_localidad.Text.Trim(), "", TXT_municipio.Text.Trim(), "", "");
                    else
                    {
                        if (Session["PKey"] != null)
                            CatalogosProveedores(3, Convert.ToInt32(Session["PKey"].ToString().Trim()), TXT_Clave.Text.Trim(), TXT_NOMBRE.Text.Trim(), TXT_Idfiscal.Text.Trim(), TXT_Tipone.Value.ToString().Trim(), TXT_Programa.Text.Trim(), TXT_CalleNumero.Text.Trim(), TXT_Codigo.Text.Trim(), TXT_Colonia.Text.Trim(), TXT_Entidad.Text.Trim(), TXT_Pais.Text.Trim(), TXT_Telefono.Text.Trim(), TXT_Correo.Text.Trim(), TXT_Fax.Text.Trim(), almacen, "", "", "", num_Int, TXT_localidad.Text.Trim(), "", TXT_municipio.Text.Trim(), "", "");
                        else
                            return;
                    }

                    hdnGuardar.Value = "1";
                    AlertSucces(MsgRegistros.MsgRegistroAgregar);
                }
                else if (e.Parameter.ToString().Contains("Exportar"))
                {
                    Exporter.GridView.Columns[0].Visible = false;
                    Export("xls");
                }                
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")  // Error: 2.No existe proveedor. 
                {
                    hdnGuardar.Value = "3";
                    lblRepetido.Visible = true;
                    lblExisteProveedor.Visible = false;
                    //AlertError(ex.Message.ToString());
                }
                else if (ex.Message.ToString().Contains("No existe proveedor"))  // Error: 2.No existe proveedor. 
                {
                    hdnGuardar.Value = "3";
                    lblRepetido.Visible = false;
                    lblExisteProveedor.Visible = true;
                    lblExisteProveedor.Text = ex.InnerException.Message;
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


        //protected void cmbPlantas_DataBound(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["uPlantas"] != null && Session["uPlantas"].ToString().Trim() != "")
        //        {
        //            String[] claves = Session["uPlantas"].ToString().Split('|');

        //            foreach (var clave in claves)
        //            {
        //                cmbPlantas.GridView.Selection.SelectRowByKey(clave);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


    }
}