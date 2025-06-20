using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using System.Data;
using SACI_MEX.Clases;
using SACI.Negocio;
using DevExpress.Web;
using System.IO;
using DevExpress.XtraPrintingLinks;
using System.Drawing;
using System.Configuration;


namespace SACI_MEX.Formularios
{
    public partial class catMateriales : System.Web.UI.Page
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
                    CatalogoMateriales(2);
                    CARGAUNIDAD();
                    CARGAALMACEN();
                    CARGATIPO_NAC_IMP();
                    TipoDeMateriales(2);
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
                ;
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    hdnGuardar.Value = "2";
                    Session["ErrorSql"] = ex.Message.ToString();
                    AlertError(ex.Message.ToString());
                }
                else
                {
                    hdnGuardar.Value = "5";
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
                    Session["grvMat"] = null;
                }


                //Cuando se quiera filtrar el Grid entra en el if
                if (Session["grvMat"] != null)
                {
                    grvMateriales.DataSource = Session["grvMat"];
                    grvMateriales.DataBind();
                    grvMateriales.SettingsPager.PageSize = GridPageSize;
                }
            }
            catch (Exception ex)
            {
                ;
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    hdnGuardar.Value = "2";
                    Session["ErrorSql"] = ex.Message.ToString();
                    AlertError(ex.Message.ToString());
                }
                else
                {
                    hdnGuardar.Value = "5";
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
            //try
            //{
            //    LimpiaControles();
            //    Session["Nuevo"] = 1;
            //    NuevoRegistro();
            //}
            //catch (Exception ex)
            //{
            //    if (ex.Message.Substring(0, 1).ToString() == "1")
            //    {
            //        AlertError(ex.Message.ToString());
            //    }
            //    else
            //    {
            //        string mensaje = string.Format("Error: {0}. {1}. {2}. {3}", ex.Message.Replace("\r\n", "|").Replace("'", "´"), "|Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
            //        "|Recurso: " + ex.Source,
            //        "|Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
            //        ErrorAlert(mensaje);
            //    }
            //}
            //finally
            //{
            //    GC.Collect();
            //}
        }

        protected void lkb_Editar_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (grvMateriales.GetSelectedFieldValues("materialkey").Count > 0)
            //    {
            //        NuevoRegistro();
            //        Session["Nuevo"] = null;
            //        TXT_materialkey.Text = grvMateriales.GetSelectedFieldValues("materialkey")[0].ToString().Trim();
            //        TXT_CVE_MATERIAL.Text = grvMateriales.GetSelectedFieldValues("clave")[0].ToString().Trim();
            //        TXT_FRACCION.Text = grvMateriales.GetSelectedFieldValues("fraccion")[0].ToString().Trim();
            //        TXT_MP_PROVEEDOR.Text = grvMateriales.GetSelectedFieldValues("claveProveedor")[0].ToString().Trim();
            //        TXT_DESCRIPCION.Text = grvMateriales.GetSelectedFieldValues("descripcion")[0].ToString().Trim();
            //        CMB_FAMILIA.Text = grvMateriales.GetSelectedFieldValues("tipomaterial")[0].ToString().Trim();
            //        CMB_ALMACEN.Value = grvMateriales.GetSelectedFieldValues("ALMACENKEY")[0].ToString().Trim();
            //        CMB_UNIDAD.Text = grvMateriales.GetSelectedFieldValues("unidad")[0].ToString().Trim();
            //        CMB_UNIDADT.Text = grvMateriales.GetSelectedFieldValues("unidadt")[0].ToString().Trim();
            //        CMB_NAC_IMPORTADO.Text = grvMateriales.GetSelectedFieldValues("TIPOM")[0].ToString().Trim();
            //    }

            //    else
            //    {
            //        AlertError(MsgRegistros.MsgSelectRegistro);

            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (ex.Message.Substring(0, 1).ToString() == "1")
            //    {
            //        AlertError(ex.Message.ToString());
            //    }
            //    else
            //    {
            //        LimpiaControles();
            //        string mensaje = string.Format("Error: {0}. {1}. {2}. {3}", ex.Message.Replace("\r\n", "|").Replace("'", "´"), "|Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
            //        "|Recurso: " + ex.Source,
            //        "|Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);

            //        ErrorAlert(mensaje);
            //    }
            //}
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string familia = "";
            //    int almacen = 0;
            //    string nacional = "";

            //    if (CMB_FAMILIA.Value == null)
            //        familia = "";
            //    else
            //    {
            //        familia = CMB_FAMILIA.Value.ToString().Trim();
            //    }

            //    if (CMB_ALMACEN.Value == null)
            //    {
            //        almacen = 0;
            //    }
            //    else
            //    {
            //        almacen = Convert.ToInt32(CMB_ALMACEN.Value);
            //    }

            //    if (CMB_NAC_IMPORTADO.Value == null)
            //    {
            //        nacional = string.Empty;
            //    }
            //    else
            //    {
            //        nacional = CMB_NAC_IMPORTADO.Value.ToString().Trim();
            //    }


            //    if (Session["Nuevo"] != null)
            //    {
            //        CatalogoMateriales(1, 0, TXT_CVE_MATERIAL.Text.Trim(), TXT_DESCRIPCION.Text.Trim(), TXT_FRACCION.Text.Trim(), CMB_UNIDAD.Value.ToString(), CMB_UNIDADT.Value.ToString(), familia, 0, 0, string.Empty, TXT_MP_PROVEEDOR.Text.Trim(), almacen, nacional, string.Empty, string.Empty, string.Empty, string.Empty);
            //    }
            //    else
            //    {
            //        CatalogoMateriales(3, int.Parse(TXT_materialkey.Text.Trim()), TXT_CVE_MATERIAL.Text.Trim(), TXT_DESCRIPCION.Text.Trim(), TXT_FRACCION.Text.Trim(), CMB_UNIDAD.Value.ToString(), CMB_UNIDADT.Value.ToString(), familia, 0, 0, string.Empty, TXT_MP_PROVEEDOR.Text.Trim(), almacen, nacional, string.Empty, string.Empty, string.Empty, string.Empty);
            //    }

            //    LimpiaControles();
            //    AlertSucces("El registro se guardó correctamente");
            //}
            //catch (Exception ex)
            //{
            //    if (ex.Message.Substring(0, 1).ToString() == "1")
            //    {
            //        AlertError(ex.Message.ToString());
            //    }
            //    else
            //    {
            //        LimpiaControles();
            //        string mensaje = string.Format("Error: {0}. {1}. {2}. {3}", ex.Message.Replace("\r\n", "|").Replace("'", "´"), "|Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
            //        "|Recurso: " + ex.Source,
            //        "|Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);

            //        ErrorAlert(mensaje);
            //    }
            //}
        }

        protected void lkb_Eliminar_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (grvMateriales.GetSelectedFieldValues("materialkey").Count > 0)
            //    {
            //        AlertQuestion("Estas seguro de eliminar el registro");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (ex.Message.Substring(0, 1).ToString() == "1")
            //    {
            //        AlertError(ex.Message.ToString());
            //    }
            //    else
            //    {
            //        string mensaje = string.Format("Error: {0}. {1}. {2}. {3}", ex.Message.Replace("\r\n", "|").Replace("'", "´"), "|Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
            //        "|Recurso: " + ex.Source,
            //        "|Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
            //        ErrorAlert(mensaje);
            //    }
            //}
            //finally
            //{
            //    GC.Collect();
            //}
        }

        protected void btnAceptarDel_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    CatalogoMateriales(4, int.Parse(grvMateriales.GetSelectedFieldValues("materialkey")[0].ToString()));
            //    AlertSucces("El registo se elimino correctamente");
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }

        protected void lkb_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                Exporter.GridView.Columns[0].Visible = false;
                Exporter.GridView.Columns["KG"].Visible = true;
                Exporter.GridView.Columns["GR"].Visible = true;
                Exporter.GridView.Columns["ML"].Visible = true;
                Exporter.GridView.Columns["MCUA"].Visible = true;
                Exporter.GridView.Columns["MCUB"].Visible = true;
                Exporter.GridView.Columns["PZA"].Visible = true;
                Exporter.GridView.Columns["CAB"].Visible = true;
                Exporter.GridView.Columns["LT"].Visible = true;
                Exporter.GridView.Columns["PAR"].Visible = true;
                Exporter.GridView.Columns["KW"].Visible = true;
                Exporter.GridView.Columns["MI"].Visible = true;
                Exporter.GridView.Columns["JGO"].Visible = true;
                Exporter.GridView.Columns["KWH"].Visible = true;
                Exporter.GridView.Columns["TON"].Visible = true;
                Exporter.GridView.Columns["BAR"].Visible = true;
                Exporter.GridView.Columns["GRN"].Visible = true;
                Exporter.GridView.Columns["DECE"].Visible = true;
                Exporter.GridView.Columns["CIEN"].Visible = true;
                Exporter.GridView.Columns["DOCE"].Visible = true;
                Exporter.GridView.Columns["CAJA"].Visible = true;
                Exporter.GridView.Columns["BOT"].Visible = true;
                Export("xlsx");

                //if (grvMateriales.VisibleRowCount > 0)
                //{
                //    //Exporter.WriteXlsToResponse(h1_titulo.InnerText, new XlsExportOptionsEx() { SheetName = h1_titulo.InnerText });

                //}
                //else
                //{
                //    string error = "No hay información para exportar";
                //    if (Session["Traducciones"] != null)
                //        try { error = ((DataTable)Session["Traducciones"]).Select("Name ='alert_no_hay_informacion_para_exportar'")[0]["Value"].ToString(); }
                //        catch { }
                //    AlertError(error);
                //}

            }
            catch (Exception ex)
            {
                AlertError(ex.Message);
            }
        }

        //protected void btn_Init(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ASPxButton btn = sender as ASPxButton;
        //        GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
        //        Int32 rowindex = container.VisibleIndex;


        //        NuevoDetalleMaterial();
        //        titDetalleMat.InnerText = "Factor para el material " + grvMateriales.GetRowValues(rowindex, "clave").ToString().Trim();
        //        RelTipoMateUnidad(2, 0, Convert.ToInt32(grvMateriales.GetRowValues(rowindex, "materialkey").ToString().Trim()));

        //    }
        //    catch (Exception ex)
        //    {
        //        AlertError(ex.Message);
        //    }
        //    finally
        //    {
        //        GC.Collect();
        //    }
        //}

        protected void btn_Click(object sender, EventArgs e)
        {
            try
            {

                ASPxButton btn = (ASPxButton)sender;
                GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)btn.NamingContainer;

                NuevoDetalleMaterial();
                titDetalleMat.InnerText = "Factor para el material " + grvMateriales.GetRowValues(container.VisibleIndex, "clave").ToString().Trim();
                RelTipoMateUnidad(2, 0, Convert.ToInt32(grvMateriales.GetRowValues(container.VisibleIndex, "materialkey").ToString().Trim()));

                hdnMaterialKey.Value = grvMateriales.GetRowValues(container.VisibleIndex, "materialkey").ToString().Trim();
                hdnClaveSel.Value = grvMateriales.GetRowValues(container.VisibleIndex, "clave").ToString().Trim();
                hdnUnidadSel.Value = grvMateriales.GetRowValues(container.VisibleIndex, "unidad").ToString().Trim();


                //NuevoDetalleMaterial();
                //titDetalleMat.InnerText = "Factor para el material " + grvMateriales.GetRowValues(Convert.ToInt32(HiddenField1.Value), "clave").ToString().Trim();
                //RelTipoMateUnidad(2, 0, Convert.ToInt32(grvMateriales.GetRowValues(Convert.ToInt32(HiddenField1.Value), "materialkey").ToString().Trim()));

            }
            catch (Exception ex)
            {
                ;
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    hdnGuardar.Value = "2";
                    Session["ErrorSql"] = ex.Message.ToString();
                    AlertError(ex.Message.ToString());
                }
                else
                {
                    hdnGuardar.Value = "5";
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

        public void CatalogoMateriales(int OPCION = 0, int MATERIALKEY = 0, string CLAVE = "", string DESCRIPCION = "", string FRACCION = "", string UNIDAD = "",
                                       string UNIDADT = "", string TIPOMATERIAL = "", decimal FACTORUM = 0, Int64 IGIE = 0, string CERTNAFTA = "",
                                       string CLAVEPROVEEDOR = "", Int64 ALMACENKEY = 0, string TIPOM = "", string FAMILIA = "", string NUMEROSERIE = "",
                                       string MARCA = "", string MODELO = "", string NICO = "", 
                                       string UM_AMERICANA = "", decimal FACTOR_UM_AMERICANA = 0)
        {

            DataTable dtMat = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvMat"] = dtMat = RNCat.catMateriales(MATERIALKEY, CLAVE, DESCRIPCION, FRACCION, UNIDAD, UNIDADT, TIPOMATERIAL, FACTORUM, IGIE, CERTNAFTA, CLAVEPROVEEDOR, ALMACENKEY,
                    TIPOM, FAMILIA, NUMEROSERIE, MARCA, MODELO, OPCION, NICO, Session["Usuario"].ToString().Trim(), string.Empty, UM_AMERICANA, FACTOR_UM_AMERICANA);
                grvMateriales.DataSource = dtMat;
                grvMateriales.DataBind();
                grvMateriales.Settings.VerticalScrollableHeight = 300;
                grvMateriales.SettingsPager.PageSize = 15;
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

        public void LimpiaControles()
        {
            try
            {
                TXT_materialkey.Text = "0";
                TXT_CVE_MATERIAL.Text = string.Empty;

                TXT_DESCRIPCION.Text = string.Empty;
                CMB_NAC_IMPORTADO.SelectedIndex = -1;
                CMB_ALMACEN.Text = string.Empty;
                CMB_UNIDAD.Text = string.Empty;
                CMB_UNIDADT.Text = string.Empty;
                TXT_FRACCION.Text = string.Empty;
                CMB_FAMILIA.SelectedIndex = -1;
                TXT_MP_PROVEEDOR.Text = string.Empty;

                TXT_CVE_MATERIAL.IsValid = true;
                TXT_DESCRIPCION.IsValid = true;
                TXT_FRACCION.IsValid = true;
                CMB_UNIDADT.IsValid = true;
                CMB_UNIDAD.IsValid = true;
                TXT_NICO.Text = string.Empty;
                TXT_FAMILIA.Text = string.Empty;

                TXT_UM_AMERICANA.Text = string.Empty;
                TXT_FACTOR_UM_AMERICANA.Value = 0;
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
        public void AlertError(string mensaje)
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

        //
        /// <summary>
        /// sjj
        /// </summary>
        /// <param name="mensaje"></param>



        //public void AlertQuestion(string mensaje)
        //{

        //    pModalQuestion.InnerText = mensaje;

        //    ScriptManager.RegisterStartupScript(this.Page, typeof(String), "", "<script> document.getElementById('btnQuestion').setAttribute('data-whatever', '" + mensaje + "'); document.getElementById('btnQuestion').click(); </script> ", false);

        //}


        public void NuevoRegistro()
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "MostrarModal", "<script> document.getElementById('btnNuevo').click(); </script> ", false);
        }


        public void NuevoDetalleMaterial()
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "MostrarModal", "<script> document.getElementById('btnDetMaterial').click(); </script> ", false);
        }


        protected int GridPageSize
        {
            get
            {
                if (Session[PageSizeSessionKey] == null)
                    return grvMateriales.SettingsPager.PageSize;
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
                DataTable dtAccesos = new DataTable("CMAT");
                dtAccesos = RNCat.ACCESOS_USUARIOS(IDPERFIL, "CMAT");

                // Presuming the DataTable has a column named Date.
                string Expression;
                Expression = "CVE_ACTIVIDAD = 'CMAT1'";
                DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = dtAccesos.Select(Expression);
                lkb_Nuevo.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CMAT2'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Editar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CMAT3'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Eliminar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CMAT4'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Excel.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CMAT5'";
                foundRows = dtAccesos.Select(Expression);
                grvMateriales.Columns[0].Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CMAT6'";
                foundRows = dtAccesos.Select(Expression);
                Session["fator_agregar"] = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CMAT7'";
                foundRows = dtAccesos.Select(Expression);
                Session["fator_editar"] = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CMAT8'";
                foundRows = dtAccesos.Select(Expression);
                Session["fator_eliminar"] = Convert.ToBoolean(foundRows[0].ItemArray[0]);
            }
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

        public void CARGAUNIDAD()
        {
            try
            {
                RNCat = new RNCatalogos();
                CMB_UNIDAD.DataSource = RNCat.catUnidad(2);
                CMB_UNIDAD.DataBind();
                CMB_UNIDADT.DataSource = RNCat.catUnidad(2);
                CMB_UNIDADT.DataBind();
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



        public void CARGATIPO_NAC_IMP()
        {
            try
            {
                RNCat = new RNCatalogos();
                CMB_NAC_IMPORTADO.DataSource = RNCat.catTipoNac_Imp();
                CMB_NAC_IMPORTADO.DataBind();
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


        public void TipoDeMateriales(int OPCION = 0, int TMKey = 0, string TIPOMATERIAL = "")
        {
            RNCat = new RNCatalogos();
            DataTable dtTIPOMAT = new DataTable();
            try
            {
                Session["grvTipoMat"] = dtTIPOMAT = RNCat.TipoDeMateriales(OPCION, TMKey, TIPOMATERIAL);
                CMB_FAMILIA.DataSource = dtTIPOMAT;
                CMB_FAMILIA.DataBind();
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public void RelTipoMateUnidad(int OPCIONCR = 0, int FMPKey = 0, int MATERIALLINK = 0, string CLAVECR = "", string UNIDADORG = "", string UNIDADCR = "", decimal FACTOR = 0)
        {
            DataTable dtMat = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                dtMat = RNCat.RelMaterialUnidad(OPCIONCR, FMPKey, MATERIALLINK, CLAVECR, UNIDADORG, UNIDADCR, FACTOR);
                grvDetMaterial.DataSource = dtMat;
                grvDetMaterial.DataBind();

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



        #endregion




        protected void grvDetMaterial_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            try
            {
                RNCat = new RNCatalogos();
                GridViewDataComboBoxColumn column = (grvDetMaterial.Columns["UnidadConvertir"] as GridViewDataComboBoxColumn);
                column.PropertiesComboBox.DataSource = RNCat.catUnidad(2);
                column.PropertiesComboBox.ValueField = "CVE_UNIDAD";
                column.PropertiesComboBox.TextField = "CVE_UNIDAD";


                //GridViewColumnLayoutItem colUnidad = (grvDetMaterial.Columns["Unidad"] as GridViewColumnLayoutItem);
                //colUnidad
                //grvMateriales.GetRowValues(Convert.ToInt32(HiddenField1.Value), "materialkey").ToString().Trim();

            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    AlertError(ex.Message.ToString());
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







        protected void lnk_ExcelDetalle_Click(object sender, EventArgs e)
        {

        }

        protected void grvMateriales_DataBinding(object sender, EventArgs e)
        {
            try
            {
                //if (Session["grvMat"] != null)
                //    grvMateriales.DataSource = Session["grvMat"] as DataTable;
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void grvDetMaterial_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                //RelTipoMateUnidad(1, 0, Convert.ToInt32(grvMateriales.GetRowValues(Convert.ToInt32(HiddenField1.Value), "materialkey").ToString().Trim()), grvMateriales.GetRowValues(Convert.ToInt32(HiddenField1.Value), "clave").ToString().Trim(), e.NewValues["Unidad"].ToString().Trim(), e.NewValues["UnidadConvertir"].ToString().Trim(), Convert.ToInt32(e.NewValues["Factor"].ToString().Trim()));
                RelTipoMateUnidad(1, 0, Convert.ToInt32(hdnMaterialKey.Value), hdnClaveSel.Value, e.NewValues["Unidad"].ToString().Trim(), e.NewValues["UnidadConvertir"].ToString().Trim(), Convert.ToDecimal(e.NewValues["Factor"].ToString().Trim()));
                e.Cancel = true;
                grvDetMaterial.CancelEdit();

                //Si existe idbitacora se guardara la información
                if (Session["IdBitacora"] != null && Session["IdBitacora"].ToString().Length > 0)
                    registro_bitacora(Session["IdBitacora"].ToString(), Int64.Parse(Session["IdUsuario"].ToString()), "Catálogo Materiales", "Nuevo", "Grid de detalle factor, código mp compañia " + hdnClaveSel.Value + ", se agregó unidad " + e.NewValues["Unidad"].ToString().Trim() + ", unidad  a convertir " + e.NewValues["UnidadConvertir"].ToString().Trim() + " y factor " + Convert.ToDecimal(e.NewValues["Factor"].ToString().Trim()));


            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    Session["ErrorSql"] = ex.Message.ToString();
                    AlertError(ex.Message.ToString());
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

        protected void grvDetMaterial_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                string factor = e.NewValues["Factor"].ToString().Trim();
                RelTipoMateUnidad(3, Convert.ToInt32(e.Keys[0]), Convert.ToInt32(hdnMaterialKey.Value), hdnClaveSel.Value, e.NewValues["Unidad"].ToString().Trim(), e.NewValues["UnidadConvertir"].ToString().Trim(), Convert.ToDecimal(Convert.ToDecimal(factor)));
                e.Cancel = true;
                grvDetMaterial.CancelEdit();

                //Si existe idbitacora se guardara la información
                if (Session["IdBitacora"] != null && Session["IdBitacora"].ToString().Length > 0)
                    registro_bitacora(Session["IdBitacora"].ToString(), Int64.Parse(Session["IdUsuario"].ToString()), "Catálogo Materiales", "Editar", "Grid de detalle factor, id " + e.Keys[0].ToString() + ", idMaterial " + hdnMaterialKey.Value + ", código mp compañia " + hdnClaveSel.Value + ", se edita unidad " + e.NewValues["Unidad"].ToString().Trim() + ", unidad  a convertir " + e.NewValues["UnidadConvertir"].ToString().Trim() + " y factor " + Convert.ToDecimal(e.NewValues["Factor"].ToString().Trim()));

            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    Session["ErrorSql"] = ex.Message.ToString();
                    AlertError(ex.Message.ToString());
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

        protected void grvDetMaterial_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                //Si existe idbitacora se guardara la información
                if (Session["IdBitacora"] != null && Session["IdBitacora"].ToString().Length > 0)
                    registro_bitacora(Session["IdBitacora"].ToString(), Int64.Parse(Session["IdUsuario"].ToString()), "Catálogo Materiales", "Borrar", "Grid de detalle factor, id " + e.Keys[0].ToString() + ", idMaterial " + hdnMaterialKey.Value + ", código mp compañia " + hdnClaveSel.Value + ", se borro unidad " + e.Values["Unidad"].ToString().Trim() + ", unidad  a convertir " + e.Values["UnidadConvertir"].ToString().Trim() + " y factor " + Convert.ToDecimal(e.Values["Factor"].ToString().Trim()));

                RelTipoMateUnidad(4, Convert.ToInt32(e.Keys[0]), Convert.ToInt32(hdnMaterialKey.Value));
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    AlertError(ex.Message.ToString());
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

        protected void grvDetMaterial_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["Unidad"] = hdnUnidadSel.Value;
        }

        protected void grvDetMaterial_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
        {
            try
            {
                e.ErrorText = Session["ErrorSql"].ToString();
            }
            catch (Exception)
            {

                throw;
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
                options.SheetName = "Materiales";

                compositeLink.CreateDocument();
                using (MemoryStream stream = new MemoryStream())
                {
                    switch (format)
                    {
                        //case "xlsx":
                        //    compositeLink.ExportToXlsx(stream);
                        //    WriteToResponse(h1_titulo.InnerText.Substring(5, h1_titulo.InnerText.Trim().Length - 3), true, "xlsx", stream);
                        //    break;
                        //default:
                        //    break;
                        case "xlsx":
                            compositeLink.ExportToXlsx(stream, options);
                            WriteToResponse("Materiales", true, format, stream);
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

        protected void cbpcatMateriales_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                hdnGuardar.Value = "0";
                if (e.Parameter.ToString().Contains("NuevoM"))
                {
                    hdnGuardar.Value = "3";
                    LimpiaControles();
                    Session["Nuevo"] = 1;
                    NuevoRegistro();
                    titNewActividad.InnerHtml = "Nuevo Material";
                    lblRepetido.Visible = false;
                }
                else if (e.Parameter.ToString().Contains("EditarM"))
                {


                    if (grvMateriales.GetSelectedFieldValues("materialkey").Count > 0)
                    {
                        lblRepetido.Visible = false;
                        hdnGuardar.Value = "3";
                        NuevoRegistro();
                        Session["Nuevo"] = null;
                        titNewActividad.InnerHtml = "Editar Material";
                        hdnMaterialKey.Value = TXT_materialkey.Text = grvMateriales.GetSelectedFieldValues("materialkey")[0].ToString().Trim();
                        TXT_CVE_MATERIAL.Text = grvMateriales.GetSelectedFieldValues("clave")[0].ToString().Trim();
                        TXT_FRACCION.Text = grvMateriales.GetSelectedFieldValues("fraccion")[0].ToString().Trim();
                        TXT_MP_PROVEEDOR.Text = grvMateriales.GetSelectedFieldValues("Clave_Proveedor")[0].ToString().Trim();
                        TXT_DESCRIPCION.Text = grvMateriales.GetSelectedFieldValues("descripcion")[0].ToString().Trim();
                        CMB_FAMILIA.Text = grvMateriales.GetSelectedFieldValues("tipomaterial")[0].ToString().Trim();
                        CMB_ALMACEN.Value = grvMateriales.GetSelectedFieldValues("ALMACENKEY")[0].ToString().Trim();
                        CMB_UNIDAD.Text = grvMateriales.GetSelectedFieldValues("unidad")[0].ToString().Trim();
                        CMB_UNIDADT.Text = grvMateriales.GetSelectedFieldValues("unidadt")[0].ToString().Trim();
                        CMB_NAC_IMPORTADO.Text = grvMateriales.GetSelectedFieldValues("TIPOM")[0].ToString().Trim();
                        TXT_NICO.Text = grvMateriales.GetSelectedFieldValues("NICO")[0].ToString().Trim();
                        TXT_CVE_MATERIAL.IsValid = true;
                        TXT_DESCRIPCION.IsValid = true;
                        TXT_FRACCION.IsValid = true;
                        CMB_UNIDADT.IsValid = true;
                        CMB_UNIDAD.IsValid = true;
                        TXT_FAMILIA.Text = grvMateriales.GetSelectedFieldValues("FAMILIA")[0].ToString().Trim();
                        //
                        TXT_UM_AMERICANA.Text = grvMateriales.GetSelectedFieldValues("UM_AMERICANA")[0].ToString().Trim();
                        TXT_FACTOR_UM_AMERICANA.Value = grvMateriales.GetSelectedFieldValues("FACTOR_UM_AMERICANA")[0].ToString().Trim();
                    }

                    else
                    {
                        hdnGuardar.Value = "2";
                        AlertError(MsgRegistros.MsgSelectRegistro);

                    }
                }
                else if (e.Parameter.ToString().Contains("BorrarM"))
                {
                    if (grvMateriales.GetSelectedFieldValues("materialkey").Count > 0)
                    {
                        hdnGuardar.Value = "1";
                        CatalogoMateriales(4, int.Parse(grvMateriales.GetSelectedFieldValues("materialkey")[0].ToString()));
                        AlertSucces("El registo se elimino correctamente");

                        //Si existe idbitacora se guardara la información
                        if (Session["IdBitacora"] != null && Session["IdBitacora"].ToString().Length > 0)
                            registro_bitacora(Session["IdBitacora"].ToString(), Int64.Parse(Session["IdUsuario"].ToString()), "Catálogo Materiales", "Borrar", "Se borró código MP compañia: " + grvMateriales.GetSelectedFieldValues("clave")[0].ToString());

                    }
                    else
                    {
                        hdnGuardar.Value = "2";
                        AlertError(MsgRegistros.MsgSelectRegistro);

                    }
                }
                else if (e.Parameter.ToString().Contains("GuardarM"))
                {
                    string familia = "";
                    int almacen = 0;
                    string nacional = "";
                    //int valida = 0;

                    //Validaciones
                    //hdnGuardar.Value = "3";
                    //if (TXT_CVE_MATERIAL.Text.Trim().Length == 0)
                    //{
                    //    valida = 1;
                    //    NuevoRegistro();
                    //    TXT_CVE_MATERIAL.IsValid = false;
                    //    //AlertError("Debe escribir un código MP compañÍa");
                    //    //return;
                    //}

                    //if (TXT_DESCRIPCION.Text.Trim().Length == 0)
                    //{
                    //    valida = 1;
                    //    NuevoRegistro();
                    //    TXT_DESCRIPCION.IsValid = false;
                    //    //hdnGuardar.Value = "2";
                    //    //AlertError("Debe escribir una descripción comercial de mercancía");
                    //    //return;
                    //}

                    //if (TXT_FRACCION.Text.Trim().Length == 0)
                    //{
                    //    valida = 1;
                    //    NuevoRegistro();
                    //    TXT_FRACCION.IsValid = false;
                    //    //hdnGuardar.Value = "2";
                    //    //AlertError("Debe escribir una fracción");
                    //    //return;
                    //}

                    //if (CMB_UNIDAD.Text.Trim().Length == 0 || CMB_UNIDAD.SelectedIndex.Equals(-1))
                    //{
                    //    valida = 1;
                    //    NuevoRegistro();
                    //    CMB_UNIDAD.IsValid = false;
                    //    //hdnGuardar.Value = "2";
                    //    //AlertError("Debe seleccionar una unidad");
                    //    //return;
                    //}

                    //if (CMB_UNIDADT.Text.Trim().Length == 0 || CMB_UNIDADT.SelectedIndex.Equals(-1))
                    //{
                    //    valida = 1;
                    //    NuevoRegistro();
                    //    CMB_UNIDADT.IsValid = false;
                    //    //hdnGuardar.Value = "2";
                    //    //AlertError("Debe seleccionar una unidad tarifaria");
                    //    //return;
                    //}


                    //if (valida.Equals(1))
                    //    return;



                    if (CMB_FAMILIA.Value == null)
                        familia = "";
                    else
                    {
                        familia = CMB_FAMILIA.Value.ToString().Trim();
                    }

                    if (CMB_ALMACEN.Value == null)
                    {
                        almacen = 0;
                    }
                    else
                    {
                        almacen = Convert.ToInt32(CMB_ALMACEN.Value);
                    }

                    if (CMB_NAC_IMPORTADO.Value == null)
                    {
                        nacional = string.Empty;
                    }
                    else
                    {
                        nacional = CMB_NAC_IMPORTADO.Value.ToString().Trim();
                    }



                    ////Valida repetir valores
                    //if (Session["grvMat"] != null)
                    //{
                    //    hdnGuardar.Value = "2";
                    //    foreach (DataRow fila in ((DataTable)Session["grvMat"]).Rows)
                    //    {
                    //        //Al Guardar
                    //        if (titNewActividad.InnerHtml.Contains("Nuevo") && fila["clave"].ToString().ToUpper().Trim() == TXT_CVE_MATERIAL.Text.ToUpper().Trim())
                    //        {
                    //            NuevoRegistro();
                    //            AlertError("El código MP compañÍa ya existe");
                    //            return;
                    //        }

                    //        //Al Editar
                    //        if (titNewActividad.InnerHtml.Contains("Editar") && fila["clave"].ToString().Trim().ToUpper() == TXT_CVE_MATERIAL.Text.Trim().ToUpper() &&
                    //           fila["materialkey"].ToString().Trim().ToUpper() != hdnMaterialKey.Value.Trim().ToUpper())
                    //        {
                    //            NuevoRegistro();
                    //            AlertError("El código MP compañÍa ya existe");
                    //            return;
                    //        }
                    //    }
                    //}


                    decimal factor_um_americana = 0;
                    if (TXT_FACTOR_UM_AMERICANA.Value!=null)
                    {
                        factor_um_americana = decimal.Parse(TXT_FACTOR_UM_AMERICANA.Text);
                    }



                    if (Session["Nuevo"] != null)
                    {
                        CatalogoMateriales(1, 0, TXT_CVE_MATERIAL.Text.Trim(), TXT_DESCRIPCION.Text.Trim(), TXT_FRACCION.Text.Trim(), CMB_UNIDAD.Value.ToString(), CMB_UNIDADT.Value.ToString(), familia, 0, 0, string.Empty, TXT_MP_PROVEEDOR.Text.Trim(), almacen, nacional, TXT_FAMILIA.Text.Trim(), string.Empty, string.Empty, string.Empty, TXT_NICO.Text.Trim(), TXT_UM_AMERICANA.Text.Trim(), factor_um_americana);

                        //Si existe idbitacora se guardara la información
                        if (Session["IdBitacora"] != null && Session["IdBitacora"].ToString().Length > 0)
                            registro_bitacora(Session["IdBitacora"].ToString(), Int64.Parse(Session["IdUsuario"].ToString()), "Catálogo Materiales", "Nuevo", "Nuevo código MP compañia: " + TXT_CVE_MATERIAL.Text.Trim());
                    }
                    else
                    {
                        CatalogoMateriales(3, int.Parse(hdnMaterialKey.Value.Trim()), TXT_CVE_MATERIAL.Text.Trim(), TXT_DESCRIPCION.Text.Trim(), TXT_FRACCION.Text.Trim(), CMB_UNIDAD.Value.ToString(), CMB_UNIDADT.Value.ToString(), familia, 0, 0, string.Empty, TXT_MP_PROVEEDOR.Text.Trim(), almacen, nacional, TXT_FAMILIA.Text.Trim(), string.Empty, string.Empty, string.Empty, TXT_NICO.Text.Trim(), TXT_UM_AMERICANA.Text.Trim(), factor_um_americana);

                        //Si existe idbitacora se guardara la información
                        if (Session["IdBitacora"] != null && Session["IdBitacora"].ToString().Length > 0)
                            registro_bitacora(Session["IdBitacora"].ToString(), Int64.Parse(Session["IdUsuario"].ToString()), "Catálogo Materiales", "Editar", "Editar código MP compañia: " + TXT_CVE_MATERIAL.Text.Trim());
                    }

                    LimpiaControles();
                    hdnGuardar.Value = "1";



                    AlertSucces("El registro se guardó correctamente");
                }
                else if (e.Parameter.ToString().Contains("Exportar"))
                {
                    Exporter.GridView.Columns[0].Visible = false;
                    Export("xls");

                    //Si existe idbitacora se guardara la información
                    if (Session["IdBitacora"] != null && Session["IdBitacora"].ToString().Length > 0)
                        registro_bitacora(Session["IdBitacora"].ToString(), Int64.Parse(Session["IdUsuario"].ToString()), "Catálogo Materiales", "Exportar", "Grid Principal");
                }
                else if (e.Parameter.ToString().Contains("Factor"))
                {
                    hdnGuardar.Value = "5";
                    NuevoDetalleMaterial();
                    titDetalleMat.InnerText = "Factor para el material " + grvMateriales.GetRowValues(Convert.ToInt32(HiddenField1.Value), "clave").ToString().Trim();
                    RelTipoMateUnidad(2, 0, Convert.ToInt32(grvMateriales.GetRowValues(Convert.ToInt32(HiddenField1.Value), "materialkey").ToString().Trim()));
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    hdnGuardar.Value = "3";
                    lblRepetido.Visible = true;
                    //AlertError(ex.Message.ToString());
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

        protected void grvDetMaterial_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            int IDPERFIL = 0;
            if (Session["IDPerfil"] != null)
                IDPERFIL = int.Parse(Session["IDPerfil"].ToString());
            //VALIDA PERMISOS USUARIO
            if (IDPERFIL != 1)
            {
                if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.New)
                    e.Visible = bool.Parse(Session["fator_agregar"].ToString());

                if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Edit)
                    e.Visible = bool.Parse(Session["fator_editar"].ToString());

                if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete)
                    e.Visible = bool.Parse(Session["fator_eliminar"].ToString());
            }

        }


        //Valida Rgistro Bitacora
        protected void registro_bitacora(string idBitacora, Int64 IdUsuario, string modulo, string accion, string detalle)
        {
            //Si existe idbitacora se guardara la información
            RNCat = new RNCatalogos();
            DataTable dtB = RNCat.ValidarBitacora(2, Int64.Parse(idBitacora), IdUsuario, modulo, accion, detalle);

        }


    }
}