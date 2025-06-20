using DevExpress.Web;
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
using System.Runtime.CompilerServices;
using System.Text;

namespace SACI_MEX.Formularios
{
    public partial class catProductos : System.Web.UI.Page
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
                    CatalogosProductos(2);
                    CatAlmacen();
                    CARGAUNIDAD();
                    CargarCatalogosMaterial();
                    grvLstMateriales.Columns["CommandColumnLista"].Visible = false;
                    AccesosUsuario();
                    //((GridViewCommandColumn)grvLstMateriales.Columns["CommandColumnLista"]).ShowNewButtonInHeader = false;
                    //lkb_Nuevo_Mat.Visible = false;

                }

                string valor_java = Request["__EVENTARGUMENT"];
                //if (valor_java != null && valor_java.Contains("sel"))
                //{
                //    btn_hid_Click(null, null);
                //}

                //LCG Deshabilitar botones
                //lkb_Nuevo_Mat.Attributes.Add("onClick", "return false;");
                //lkb_Editar_Mat.Attributes.Add("onClick", "return false;");
                //lnkBorrar_Mat.Attributes.Add("onClick", "return false;");
                btnGuardar.Attributes.Add("onClick", "return false;");
                lkb_Editar.Attributes.Add("onClick", "return false;");
                lkb_Eliminar.Attributes.Add("onClick", "return false;");
                btnAceptarDel.Attributes.Add("onClick", "return false;");


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
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;

                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        public void CargarCatalogosMaterial()
        {
            try
            {
                RNCat = new RNCatalogos();
                Session["dtCatMateriales_prod"] = RNCat.catMateriales(0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, string.Empty, string.Empty, 0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 2);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    Session["grvProduct"] = null;
                    Session["grvListaMaterial"] = null;
                    Session["gridEstructuras"] = null;
                    Session["grvAlternativo"] = null;

                    Session["dtCatMateriales_prod"] = null;
                    Session["dtUnidad_prod"] = null;
                }


                //Cuando se quiera filtrar el Grid entra en el if
                if (Session["grvProduct"] != null)
                {
                    grvProductos.DataSource = Session["grvProduct"];
                    grvProductos.DataBind();
                    grvProductos.SettingsPager.PageSize = GridPageSize;
                }

                if (Session["grvListaMaterial"] != null)
                {
                    grvLstMateriales.DataSource = Session["grvListaMaterial"];
                    grvLstMateriales.DataBind();
                }
                if (Session["gridEstructuras"] != null)
                {
                    grvFechaVig.DataSource = Session["gridEstructuras"];
                    grvFechaVig.DataBind();
                }
                if (Session["grvAlternativo"] != null)
                {
                    grvAlternativo.DataSource = Session["grvAlternativo"];
                    grvAlternativo.DataBind();
                }
                if (Session["dtCatMateriales_prod"] != null)
                {
                    ((GridViewDataComboBoxColumn)grvAlternativo.Columns["CVE_MATERIAL"]).PropertiesComboBox.DataSource = Session["dtCatMateriales_prod"];
                    ((GridViewDataComboBoxColumn)grvLstMateriales.Columns["CVE_MATERIAL"]).PropertiesComboBox.DataSource = Session["dtCatMateriales_prod"];
                }
                if (Session["dtUnidad_prod"] != null)
                {
                    ((GridViewDataComboBoxColumn)grvFactorProd.Columns["UnidadConvertir"]).PropertiesComboBox.DataSource = Session["dtUnidad_prod"];
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
                    mensaje = mensaje.Replace("\r\n", "|");
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
                titProducto.InnerHtml = "Nuevo Producto";
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
                    mensaje = mensaje.Replace("\r\n", "|");
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

                if (grvProductos.GetSelectedFieldValues("PRODUCTOKEY").Count > 0)
                {
                    NuevoRegistro();
                    TXT_PRODUCTOKEY.Text = grvProductos.GetSelectedFieldValues("PRODUCTOKEY")[0].ToString().Trim();
                    TXT_CVE_PRODUCTO.Text = grvProductos.GetSelectedFieldValues("CVE_PRODUCTO")[0].ToString().Trim();
                    TXT_NOMBRE.Text = grvProductos.GetSelectedFieldValues("NOMBRE")[0].ToString().Trim();
                    CMB_UNIDAD.Text = grvProductos.GetSelectedFieldValues("UNIDAD")[0].ToString().Trim();
                    TXT_fraccion.Text = grvProductos.GetSelectedFieldValues("fraccion")[0].ToString().Trim();
                    TXT_CVE_PRODUCTO_CLIENTE.Text = grvProductos.GetSelectedFieldValues("CVE_PRODUCTO_CLIENTE")[0].ToString().Trim();
                    CMB_ALMACEN.Value = grvProductos.GetSelectedFieldValues("ALMACENKEY")[0].ToString().Trim();
                    TXT_AUXILIAR.Text = grvProductos.GetSelectedFieldValues("AUXILIAR")[0].ToString().Trim();
                    TXT_TIPO.Text = grvProductos.GetSelectedFieldValues("TIPO")[0].ToString().Trim();
                    TXT_NICO.Text = grvProductos.GetSelectedFieldValues("NICO")[0].ToString().Trim();
                    CMB_UNIDADT.Value = grvProductos.GetSelectedFieldValues("UNIDADT")[0].ToString().Trim();
                    //
                    TXT_UM_AMERICANA.Text = grvProductos.GetSelectedFieldValues("UM_AMERICANA")[0].ToString().Trim();
                    TXT_FACTOR_UM_AMERICANA.Value = grvProductos.GetSelectedFieldValues("FACTOR_UM_AMERICANA")[0].ToString().Trim();
                    TXT_VALOR.Text = grvProductos.GetSelectedFieldValues("VALOR_TRASACCION")[0].ToString().Trim();

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
                    mensaje = mensaje.Replace("\r\n", "|");
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
                    LimpiaControles();
                    string mensaje = string.Format("Error en la pantalla: {0}. {1} {2} {3} {4}. {5} {6}. {7} {8}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
                    Environment.NewLine, "Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    Environment.NewLine, "Recurso: " + ex.Source,
                    Environment.NewLine, "Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
                    mensaje = mensaje.Replace("\r\n", "|");
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
            int ALMACEN;
            string UNIDAD;
            try
            {

                if (CMB_ALMACEN.Value == null)
                {
                    ALMACEN = 0;
                }
                else
                {
                    ALMACEN = Convert.ToInt32(CMB_ALMACEN.Value);
                }

                if (CMB_UNIDADT.Value == null)
                {
                    UNIDAD = string.Empty;
                }
                else
                {
                    UNIDAD = CMB_UNIDADT.Value.ToString().Trim();
                }


                CatalogosProductos(1, Convert.ToInt32(TXT_PRODUCTOKEY.Text.Trim()), TXT_CVE_PRODUCTO.Text.Trim(), TXT_NOMBRE.Text.Trim(), CMB_UNIDAD.Text.Trim(), TXT_fraccion.Text.Trim(), TXT_CVE_PRODUCTO_CLIENTE.Text.Trim(), ALMACEN, TXT_AUXILIAR.Text.Trim(), TXT_TIPO.Text.Trim(), UNIDAD, TXT_NICO.Text.Trim());
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
                    mensaje = mensaje.Replace("\r\n", "|");
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
                DataTable dtAccesos = new DataTable("CPRD");
                dtAccesos = RNCat.ACCESOS_USUARIOS(IDPERFIL, "CPRD");

                // Presuming the DataTable has a column named Date.
                string Expression;
                Expression = "CVE_ACTIVIDAD = 'CPRD1'";
                DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = dtAccesos.Select(Expression);
                lkb_Nuevo.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRD2'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Editar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRD3'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Eliminar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRD4'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Excel.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRD5'";
                foundRows = dtAccesos.Select(Expression);
                grvProductos.Columns["Factor"].Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRD6'";
                foundRows = dtAccesos.Select(Expression);
                ((GridViewCommandColumn)grvFactorProd.Columns["comandFactor"]).ShowNewButtonInHeader = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRD7'";
                foundRows = dtAccesos.Select(Expression);
                ((GridViewCommandColumn)grvFactorProd.Columns["comandFactor"]).ShowEditButton = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRD8'";
                foundRows = dtAccesos.Select(Expression);
                ((GridViewCommandColumn)grvFactorProd.Columns["comandFactor"]).ShowDeleteButton = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRD9'";
                foundRows = dtAccesos.Select(Expression);
                grvProductos.Columns["Estructuras"].Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRD10'";
                foundRows = dtAccesos.Select(Expression);
                ((GridViewCommandColumn)grvFechaVig.Columns["comandEstructura"]).ShowNewButtonInHeader = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRD11'";
                foundRows = dtAccesos.Select(Expression);
                ((GridViewCommandColumn)grvFechaVig.Columns["comandEstructura"]).ShowEditButton = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                //Expression = "CVE_ACTIVIDAD = 'CPRD12'";
                //foundRows = dtAccesos.Select(Expression);
                //grvFechaVig.Columns["BorrarEstructura"].Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRD13'";
                foundRows = dtAccesos.Select(Expression);
                ((GridViewCommandColumn)grvLstMateriales.Columns["CommandColumnLista"]).ShowNewButtonInHeader = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRD14'";
                foundRows = dtAccesos.Select(Expression);
                ((GridViewCommandColumn)grvLstMateriales.Columns["CommandColumnLista"]).ShowEditButton = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRD15'";
                foundRows = dtAccesos.Select(Expression);
                ((GridViewCommandColumn)grvLstMateriales.Columns["CommandColumnLista"]).ShowDeleteButton = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRD16'";
                foundRows = dtAccesos.Select(Expression);
                ((GridViewCommandColumn)grvAlternativo.Columns["comandAlternativo"]).ShowNewButtonInHeader = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRD17'";
                foundRows = dtAccesos.Select(Expression);
                ((GridViewCommandColumn)grvAlternativo.Columns["comandAlternativo"]).ShowEditButton = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRD18'";
                foundRows = dtAccesos.Select(Expression);
                ((GridViewCommandColumn)grvAlternativo.Columns["comandAlternativo"]).ShowDeleteButton = Convert.ToBoolean(foundRows[0].ItemArray[0]);
            }
        }

        public void CatAlmacen()
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

        public void CatalogosProductos(int OPCION = 0, int PRODUCTOKEY = 0, string CVE_PRODUCTO = "", string NOMBRE = "", string UNIDAD = "", string FRACCION = "", string CVE_PRODUCTO_CLIENTE = "",
            int ALMACENKEY = 0, string AUXILIAR = "", string TIPO = "", string UNIDADT = "", string NICO = "", string UM_AMERICANA = "", decimal FACTOR_UM_AMERICANA = 0, decimal VALOR_TRANSACCION = 0,
            string MONEDA = "", string USO = "", string CLIENTE = "", string PAIS = "", decimal COSTO_NETO = 0)
        {
            DataTable dtAcc = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvProduct"] = dtAcc = RNCat.CatalogosProductos(OPCION, PRODUCTOKEY, CVE_PRODUCTO, NOMBRE, UNIDAD, FRACCION, CVE_PRODUCTO_CLIENTE, ALMACENKEY, AUXILIAR, TIPO, UNIDADT, NICO, Session["Usuario"].ToString().Trim(), string.Empty,
                    UM_AMERICANA, FACTOR_UM_AMERICANA, VALOR_TRANSACCION, MONEDA, USO, CLIENTE, PAIS, COSTO_NETO);
                grvProductos.DataSource = dtAcc;
                grvProductos.DataBind();
                grvProductos.Settings.VerticalScrollableHeight = 300;
                grvProductos.SettingsPager.PageSize = 15;

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


        public void CARGAUNIDAD()
        {
            try
            {
                DataTable dtUni = new DataTable();

                RNCat = new RNCatalogos();
                Session["dtUnidad_prod"] = dtUni = RNCat.catUnidad(2);
                CMB_UNIDAD.DataSource = dtUni;
                CMB_UNIDAD.DataBind();
                CMB_UNIDADT.DataSource = dtUni;
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

        public void LimpiaControles()
        {
            try
            {
                TXT_PRODUCTOKEY.Text = "0";
                TXT_CVE_PRODUCTO.Text = string.Empty;
                TXT_NOMBRE.Text = string.Empty;
                CMB_UNIDAD.SelectedIndex = -1;
                TXT_fraccion.Text = string.Empty;
                TXT_CVE_PRODUCTO_CLIENTE.Text = string.Empty;
                CMB_ALMACEN.SelectedIndex = -1;
                TXT_TIPO.Text = string.Empty;
                TXT_NICO.Text = string.Empty;
                CMB_UNIDADT.SelectedIndex = -1;
                TXT_VALOR.Text = string.Empty;

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
                    return grvProductos.SettingsPager.PageSize;
                return (int)Session[PageSizeSessionKey];
            }
            set { Session[PageSizeSessionKey] = value; }
        }


        public string AddLog(string message, [CallerMemberName] string memberName = "",
[CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            //StringBuilder sb = new StringBuilder();
            string sb = string.Empty;
            //sb.AppendLine("Message: " + message);
            //sb.AppendLine("Member/Function name: " + memberName);
            //sb.AppendLine("Source file path: " + sourceFilePath);
            //sb.AppendLine("Source line number: " + sourceLineNumber);

            sb = message + " Source line number: " + sourceLineNumber;

            //Create log file
            //string FileName = @"D:\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".log";
            //if (File.Exists(FileName))
            //{
            //    File.Delete(FileName); // remove existing
            //}
            //using (StreamWriter sw = File.CreateText(FileName))
            //{
            //    sw.Write(sb.ToString());         // write entire contents
            //    sw.Close();
            //}

            return sb.ToString();
        }

        #endregion

        protected void grvFechaVig_CustomErrorText(object sender, DevExpress.Web.ASPxGridViewCustomErrorTextEventArgs e)
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


        protected void grvFechaVig_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                string orden = string.Empty;
                if (e.NewValues["ORDEN"] != null)
                {
                    orden = e.NewValues["ORDEN"].ToString().Trim();
                }
                //CatEstructuras(1, Convert.ToInt32(e.Keys[0]), Convert.ToDateTime(e.NewValues["inicio"].ToString().Trim()).ToString("yyyyMMdd"), Convert.ToInt32(grvProductos.GetRowValues(Convert.ToInt32(HiddenField1.Value), "PRODUCTOKEY").ToString().Trim()));
                CatEstructuras(1, Convert.ToInt32(e.Keys[0]), Convert.ToDateTime(e.NewValues["inicio"].ToString().Trim()).ToString("yyyyMMdd"), Convert.ToInt32(hdnProductoKey.Value), orden);
                ////Inicio-Actualizar titulo
                //hdnFechaSel.Value = Convert.ToDateTime(grvFechaVig.GetRowValues(Convert.ToInt32(hiddenFechaEstruct.Value), "inicio").ToString()).ToString("dd/MM/yyyy");
                //popUpEstructura.HeaderText = hdnTituloEstructura.Value + " FECHA INICIO: " + hdnFechaSel.Value;
                ////Fin-Actualizar titulo

                e.Cancel = true;
                grvFechaVig.CancelEdit();
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
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void grvFechaVig_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                //CatEstructuras(2, Convert.ToInt32(e.Keys[0]), string.Empty, Convert.ToInt32(grvProductos.GetRowValues(Convert.ToInt32(HiddenField1.Value), "PRODUCTOKEY").ToString().Trim()));
                //cbpCatProductos_Callback(null, null);
                e.Cancel = true;
                CatEstructuras(2, Convert.ToInt32(e.Keys[0]), string.Empty, Convert.ToInt32(hdnProductoKey.Value));
                //string javaScript = "FinGrid();";
                //string javaScript = "alert('Terminado');";
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", javaScript, true);
                //ScriptManager.RegisterStartupScript(this.Page, typeof(String), "MostrarModal", javaScript, false);
                //ScriptManager.RegisterStartupScript(this.Page, typeof(String), "script", "FinGrid();", false);
                //ASPxGridView gridMateriales = popUpEstructura.FindControl("grvLstMateriales") as ASPxGridView;
                //gridMateriales.DataSource = null;
                //gridMateriales.DataBind();

                grvLstMateriales.Columns["CommandColumnLista"].Visible = false;
                grvLstMateriales.DataSource = null;
                grvLstMateriales.DataBind();

                grvFechaVig.JSProperties["cpRefreshLstMat"] = true;
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
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void grvFechaVig_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                string orden = string.Empty;
                if (e.NewValues["ORDEN"] != null)
                {
                    orden = e.NewValues["ORDEN"].ToString().Trim();
                }
                //CatEstructuras(1, 0, Convert.ToDateTime(e.NewValues["inicio"].ToString().Trim()).ToString("yyyyMMdd"), Convert.ToInt32(grvProductos.GetRowValues(Convert.ToInt32(HiddenField1.Value), "PRODUCTOKEY").ToString().Trim()));
                CatEstructuras(1, 0, Convert.ToDateTime(e.NewValues["inicio"].ToString().Trim()).ToString("yyyyMMdd"), Convert.ToInt32(hdnProductoKey.Value), orden);
                e.Cancel = true;
                grvFechaVig.CancelEdit();

            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    Session["ErrorSql"] = ex.Message.Substring(2, ex.Message.Length - 2).ToString();
                    AlertError(ex.Message.Substring(2, ex.Message.Length - 2).ToString());
                }
                else
                {
                    string mensaje = string.Format("Error en la pantalla: {0}. {1} {2} {3} {4}. {5} {6}. {7} {8}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
                    Environment.NewLine, "Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    Environment.NewLine, "Recurso: " + ex.Source,
                    Environment.NewLine, "Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            try
            {
                hdnGuardar.Value = "4";
                ASPxButton btn = (ASPxButton)sender;
                GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)btn.NamingContainer;

                NuevoEstructura();
                popUpEstructura.HeaderText = "PRODUCTO: " + grvProductos.GetRowValues(container.VisibleIndex, "CVE_PRODUCTO").ToString().Trim();
                RelProdEstruc(Convert.ToInt32(grvProductos.GetRowValues(container.VisibleIndex, "PRODUCTOKEY").ToString().Trim()));

                hdnProductoKey.Value = grvProductos.GetRowValues(container.VisibleIndex, "PRODUCTOKEY").ToString().Trim();
                hdnTituloEstructura.Value = "PRODUCTO: " + grvProductos.GetRowValues(container.VisibleIndex, "CVE_PRODUCTO").ToString().Trim();
                ////titDetMat.InnerText = "PRODUCTO: " + grvProductos.GetRowValues(Convert.ToInt32(HiddenField1.Value), "CVE_PRODUCTO").ToString().Trim();

                Session["grvListaMaterial"] = null;

                grvLstMateriales.DataSource = null;
                grvLstMateriales.DataBind();

                grvLstMateriales.Columns["CommandColumnLista"].Visible = false;
                grvLstMateriales.CancelEdit();
                grvFechaVig.CancelEdit();
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
                    hdnGuardar.Value = "5";
                    string mensaje = string.Format("Error en la pantalla: {0}. {1} {2} {3} {4}. {5} {6}. {7} {8}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
                    Environment.NewLine, "Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    Environment.NewLine, "Recurso: " + ex.Source,
                    Environment.NewLine, "Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        public void NuevoEstructura()
        {
            popUpEstructura.ShowOnPageLoad = true;
            //ScriptManager.RegisterStartupScript(this.Page, typeof(String), "MostrarModal", "<script> document.getElementById('btnEstructura').click(); </script> ", false);            
        }


        //Metodo que muestra ventana de alerta
        public void AlertError(string mensaje)
        {
            p2.InnerText = mensaje;
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "", "<script> document.getElementById('btnErrorUser').setAttribute('data-whatever', '" + mensaje + "'); document.getElementById('btnErrorUser').click(); </script> ", false);
        }



        public void RelProdEstruc(int PRODUCTO_KEY = 0)
        {
            DataTable dtMat = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["gridEstructuras"] = dtMat = RNCat.RelProdEstruc(PRODUCTO_KEY);
                grvFechaVig.DataSource = dtMat;
                grvFechaVig.DataBind();

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

        public void CatEstructuras(int OPCION = 0, int ESTRUCTURA_KEY = 0, string FEC_ESTRUCTURA = "", int PRODUCTO_KEY = 0, string ORDEN = "")
        {
            DataTable dtMat = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["gridEstructuras"] = dtMat = RNCat.CatEstructuras(OPCION, ESTRUCTURA_KEY, FEC_ESTRUCTURA, PRODUCTO_KEY, ORDEN);
                grvFechaVig.DataSource = dtMat;
                grvFechaVig.DataBind();
                //grvLstMateriales.DataSource = null;
                //grvLstMateriales.DataBind();

                grvLstMateriales.DataSource = null;
                grvLstMateriales.DataBind();
                //grvLstMateriales_DataBinding(grvLstMateriales);


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


        public void RelEstructMaterial(int OPCION = 0, int PRODC_KEY = 0, string CVE_MAT = "", decimal CANT_MATERIAL = 0, decimal CANT_MERMA = 0, decimal CANT_DESPR = 0, int PRODUCTO_LINK = 0, int ESTRUCT_KEY = 0, string PEDIMENTO = "")
        {
            DataTable dtMat = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvListaMaterial"] = dtMat = RNCat.RelEstructMat(OPCION, PRODC_KEY, CVE_MAT, CANT_MATERIAL, CANT_MERMA, CANT_DESPR, PRODUCTO_LINK, ESTRUCT_KEY, PEDIMENTO);
                grvLstMateriales.DataSource = dtMat;
                grvLstMateriales.DataBind();
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


        public void NuevoFactorProd()
        {
            popFactor.ShowOnPageLoad = true;
        }


        public void RelFactorProd(int OPCIONCR = 0, int FMPKey = 0, int PRODUCTOLINK = 0, string CLAVECR = "", string UNIDADORG = "", string UNIDADCR = "", decimal FACTOR = 0)
        {
            DataTable dtFact = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                dtFact = RNCat.RelFactorProd(OPCIONCR, FMPKey, PRODUCTOLINK, CLAVECR, UNIDADORG, UNIDADCR, FACTOR);
                grvFactorProd.DataSource = dtFact;
                grvFactorProd.DataBind();
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



        protected void btnAlternativo_Click(object sender, EventArgs e)
        {
            try
            {
                RelMatAlternativo(1, 0, string.Empty, 0, Convert.ToInt64(grvLstMateriales.GetRowValues(Convert.ToInt32(HiddenAlterna.Value.ToString()), "PRODMATKEY").ToString().Trim()));
                popupAlternativo.ShowOnPageLoad = true;
                popupAlternativo.HeaderText = "PRODUCTO ALTERNATIVO: " + grvLstMateriales.GetRowValues(Convert.ToInt32(HiddenAlterna.Value), "CVE_MATERIAL").ToString().Trim();
                //titalternativo.InnerText = "Códigos alternativos, Material: " + grvLstMateriales.GetRowValues(Convert.ToInt32(hiddenFechaEstruct.Value), "CVE_MATERIAL").ToString().Trim();

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
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void grvLstMateriales_CustomErrorText(object sender, DevExpress.Web.ASPxGridViewCustomErrorTextEventArgs e)
        {
            try
            {
                if (Session["ErrorSql"] != null)
                {
                    e.ErrorText = Session["ErrorSql"].ToString();
                    //grvLstMateriales.CancelEdit();
                }
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
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void grvLstMateriales_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int contador = 0;
            try
            {
                if (Session["gridEstructuras"] != null)
                {
                    DataTable dt = ((DataTable)Session["gridEstructuras"]);
                    contador = dt.Rows.Count;
                }
                string pedimento = string.Empty;
                if (e.NewValues["AUXILIAR"] != null)
                {
                    pedimento = e.NewValues["AUXILIAR"].ToString().Trim();
                }
                //RelEstructMaterial(2, Convert.ToInt32(e.Keys[0]), e.NewValues["CVE_MATERIAL"].ToString().Trim(), Convert.ToDecimal(e.NewValues["CANT_UTILIZADA"].ToString().Trim()), Convert.ToDecimal(e.NewValues["CANT_MERMADA"].ToString().Trim()), Convert.ToDecimal(e.NewValues["CANT_MERMADA"].ToString().Trim()), Convert.ToInt32(grvProductos.GetRowValues(Convert.ToInt32(HiddenField1.Value), "PRODUCTOKEY").ToString().Trim()), e.NewValues["UNIDAD"].ToString().Trim(), Convert.ToInt32(grvFechaVig.GetRowValues(Convert.ToInt32(hiddenFechaEstruct.Value), "estructurakey").ToString().Trim()));
                RelEstructMaterial(2, Convert.ToInt32(e.Keys[0]), e.NewValues["CVE_MATERIAL"].ToString().Trim(), Convert.ToDecimal(e.NewValues["CANT_UTILIZADA"].ToString().Trim()), Convert.ToDecimal(e.NewValues["CANT_MERMADA"].ToString().Trim()), Convert.ToDecimal(e.NewValues["CANT_DESPERDICIADA"].ToString().Trim()), Convert.ToInt32(hdnProductoKey.Value), Convert.ToInt32(grvFechaVig.GetRowValues(Convert.ToInt32(hiddenFechaEstruct.Value), "estructurakey").ToString().Trim()), pedimento);
                e.Cancel = true;
                grvLstMateriales.CancelEdit();
            }
            catch (Exception ex)
            {
                if (contador.Equals(0))
                {
                    Session["ErrorSql"] = "Debe seleccionar un registro de vigencia";
                }
                else if (ex.Message.Substring(0, 1).ToString() == "1")
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
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void grvLstMateriales_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            int contador = 0;
            try
            {

                if (Session["gridEstructuras"] != null)
                {
                    DataTable dt = ((DataTable)Session["gridEstructuras"]);
                    contador = dt.Rows.Count;
                }
                string pedimento = string.Empty;
                if (e.NewValues["AUXILIAR"] != null)
                {
                    pedimento = e.NewValues["AUXILIAR"].ToString().Trim();
                }
                RelEstructMaterial(2, 0, e.NewValues["CVE_MATERIAL"].ToString().Trim(), Convert.ToDecimal(e.NewValues["CANT_UTILIZADA"].ToString().Trim()), Convert.ToDecimal(e.NewValues["CANT_MERMADA"].ToString().Trim()), Convert.ToDecimal(e.NewValues["CANT_DESPERDICIADA"].ToString().Trim()), Convert.ToInt32(hdnProductoKey.Value), Convert.ToInt32(grvFechaVig.GetRowValues(Convert.ToInt32(hiddenFechaEstruct.Value), "estructurakey").ToString().Trim()), pedimento);
                e.Cancel = true;
                grvLstMateriales.CancelEdit();


            }
            catch (Exception ex)
            {
                if (contador.Equals(0))
                {
                    Session["ErrorSql"] = "Debe seleccionar un registro de vigencia";
                    //e.Cancel = true;
                    //grvLstMateriales.CancelEdit();
                }
                else if (ex.Message.Substring(0, 1).ToString() == "1")
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
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void grvLstMateriales_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int contador = 0;
            try
            {
                if (Session["gridEstructuras"] != null)
                {
                    DataTable dt = ((DataTable)Session["gridEstructuras"]);
                    contador = dt.Rows.Count;
                }

                e.Cancel = true;
                RelEstructMaterial(3, Convert.ToInt32(e.Keys[0]), string.Empty, 0, 0, 0, Convert.ToInt32(hdnProductoKey.Value), Convert.ToInt32(grvFechaVig.GetRowValues(Convert.ToInt32(hiddenFechaEstruct.Value), "estructurakey").ToString().Trim()));

                grvLstMateriales.CancelEdit();
            }
            catch (Exception ex)
            {
                if (contador.Equals(0))
                {
                    Session["ErrorSql"] = "Debe seleccionar un registro de vigencia";
                }
                else if (ex.Message.Substring(0, 1).ToString() == "1")
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
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void grvLstMateriales_Init(object sender, EventArgs e)
        {
            try
            {
                //RNCat = new RNCatalogos();
                //GridViewDataComboBoxColumn column = (grvLstMateriales.Columns["CVE_MATERIAL"] as GridViewDataComboBoxColumn);
                //column.PropertiesComboBox.DataSource = RNCat.catMateriales(0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, string.Empty, string.Empty, 0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 2);
                //column.PropertiesComboBox.ValueField = "clave";
                //column.PropertiesComboBox.TextField = "nombre";

                if (grvFechaVig.GetSelectedFieldValues("estructurakey").Count > 0)
                {
                    grvLstMateriales.Columns["CommandColumnLista"].Visible = true;
                }
                else
                {
                    grvLstMateriales.Columns["CommandColumnLista"].Visible = false;
                }
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
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void grvAlternativo_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
        {
            try
            {
                if (Session["ErrorSql"] != null)
                    e.ErrorText = Session["ErrorSql"].ToString();
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
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void grvAlternativo_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                //RelMatAlternativo(2, Convert.ToInt32(e.Keys[0]), e.NewValues["CVE_MATERIAL"].ToString().Trim(), Convert.ToInt32(grvProductos.GetRowValues(Convert.ToInt32(HiddenField1.Value), "PRODUCTOKEY").ToString().Trim()), Convert.ToInt32(grvLstMateriales.GetRowValues(Convert.ToInt32(HiddenAlterna.Value), "PRODMATKEY").ToString().Trim()));               
                RelMatAlternativo(2, Convert.ToInt64(e.Keys[0]), e.NewValues["CVE_MATERIAL"].ToString().Trim(), Convert.ToInt64(hdnProductoKey.Value), Convert.ToInt64(grvLstMateriales.GetRowValues(Convert.ToInt32(HiddenAlterna.Value), "PRODMATKEY").ToString().Trim()));
                e.Cancel = true;
                grvAlternativo.CancelEdit();
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
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void grvAlternativo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                RelMatAlternativo(3, Convert.ToInt64(e.Keys[0]), string.Empty, 0, Convert.ToInt64(grvLstMateriales.GetRowValues(Convert.ToInt32(HiddenAlterna.Value), "PRODMATKEY").ToString().Trim()));
                e.Cancel = true;
                grvAlternativo.CancelEdit();
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
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void grvAlternativo_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                //RelMatAlternativo(2, 0, e.NewValues["CVE_MATERIAL"].ToString().Trim(), Convert.ToInt32(grvProductos.GetRowValues(Convert.ToInt32(HiddenField1.Value), "PRODUCTOKEY").ToString().Trim()), Convert.ToInt32(grvLstMateriales.GetRowValues(Convert.ToInt32(HiddenAlterna.Value), "PRODMATKEY").ToString().Trim()));               
                RelMatAlternativo(2, 0, e.NewValues["CVE_MATERIAL"].ToString().Trim(), Convert.ToInt64(hdnProductoKey.Value), Convert.ToInt64(grvLstMateriales.GetRowValues(Convert.ToInt32(HiddenAlterna.Value.ToString()), "PRODMATKEY").ToString().Trim()));
                e.Cancel = true;
                grvAlternativo.CancelEdit();
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
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        //protected void grvAlternativo_Init(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        RNCat = new RNCatalogos();
        //        GridViewDataComboBoxColumn column = (grvAlternativo.Columns["CVE_MATERIAL"] as GridViewDataComboBoxColumn);
        //        column.PropertiesComboBox.DataSource = RNCat.catMateriales(0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, string.Empty, string.Empty, 0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 2);
        //        column.PropertiesComboBox.ValueField = "clave";
        //        column.PropertiesComboBox.TextField = "nombre";

        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.Substring(0, 1).ToString() == "1")
        //        {
        //            Session["ErrorSql"] = ex.Message.ToString();
        //            AlertError(ex.Message.ToString());
        //        }
        //        else
        //        {
        //            string mensaje = string.Format("Error en la pantalla: {0}. {1} {2} {3} {4}. {5} {6}. {7} {8}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
        //            Environment.NewLine, "Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
        //            Environment.NewLine, "Recurso: " + ex.Source,
        //            Environment.NewLine, "Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
        //            mensaje = mensaje.Replace("\r\n", "|");
        //            Session["ErrorSql"] = mensaje;
        //            ErrorAlert(mensaje);
        //        }
        //    }
        //    finally
        //    {
        //        GC.Collect();
        //    }
        //}



        public void RelMatAlternativo(int OPCION = 0, Int64 ALTERNATIV_KEY = 0, string CVE_MAT = "", Int64 PRODUCTO_LINK = 0, Int64 PRODMAT_KEY = 0)
        {
            DataTable dtMatAlter = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvAlternativo"] = dtMatAlter = RNCat.RelMatAlternativo(OPCION, ALTERNATIV_KEY, CVE_MAT, PRODUCTO_LINK, PRODMAT_KEY);
                grvAlternativo.DataSource = dtMatAlter;
                grvAlternativo.DataBind();
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




        public void RelProdFactor(int OPCIONCR = 0, int FMPKey = 0, int PRODUCTOLINK = 0, string CLAVECR = "", string UNIDADORG = "", string UNIDADCR = "", decimal FACTOR = 0)
        {
            DataTable dtMat = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                dtMat = RNCat.RelFactorProd(OPCIONCR, FMPKey, PRODUCTOLINK, CLAVECR, UNIDADORG, UNIDADCR, FACTOR);
                grvFactorProd.DataSource = dtMat;
                grvFactorProd.DataBind();
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


        protected void btnFact_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxButton btn = (ASPxButton)sender;
                GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)btn.NamingContainer;

                NuevoFactorProd();
                popFactor.HeaderText = "FACTOR PRODUCTO: " + grvProductos.GetRowValues(container.VisibleIndex, "CVE_PRODUCTO").ToString().Trim();
                RelProdFactor(2, 0, Convert.ToInt32(grvProductos.GetRowValues(container.VisibleIndex, "PRODUCTOKEY").ToString().Trim()));

                hdnProductoKey.Value = grvProductos.GetRowValues(container.VisibleIndex, "PRODUCTOKEY").ToString().Trim();
                hdnUnidadSel.Value = grvProductos.GetRowValues(container.VisibleIndex, "UNIDAD").ToString().Trim();
                hdnClaveProductoSel.Value = grvProductos.GetRowValues(container.VisibleIndex, "CVE_PRODUCTO").ToString().Trim();
                hdnClaveProdClienteSel.Value = grvProductos.GetRowValues(container.VisibleIndex, "CVE_PRODUCTO_CLIENTE").ToString().Trim();

                //popFactor.HeaderText = "FACTOR PRODUCTO: " + grvProductos.GetRowValues(Convert.ToInt32(HiddenField1.Value), "CVE_PRODUCTO").ToString().Trim();
                //RelProdFactor(2, 0, Convert.ToInt32(grvProductos.GetRowValues(Convert.ToInt32(HiddenField1.Value), "PRODUCTOKEY").ToString().Trim()));
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
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void grvFactorProd_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            //e.NewValues["Unidad"] = grvProductos.GetRowValues(Convert.ToInt32(HiddenField1.Value), "UNIDAD").ToString().Trim();
            e.NewValues["Unidad"] = hdnUnidadSel.Value;
        }

        protected void grvFactorProd_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
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

        //protected void grvFactorProd_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        //{
        //    try
        //    {
        //        RNCat = new RNCatalogos();
        //        GridViewDataComboBoxColumn column = (grvFactorProd.Columns["UnidadConvertir"] as GridViewDataComboBoxColumn);
        //        column.PropertiesComboBox.DataSource = RNCat.catUnidad(2);
        //        column.PropertiesComboBox.ValueField = "CVE_UNIDAD";
        //        column.PropertiesComboBox.TextField = "CVE_UNIDAD";
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.Substring(0, 1).ToString() == "1")
        //        {
        //            AlertError(ex.Message.ToString());
        //        }
        //        else
        //        {
        //            string mensaje = string.Format("Error en la pantalla: {0}. {1} {2} {3} {4}. {5} {6}. {7} {8}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
        //            Environment.NewLine, "Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
        //            Environment.NewLine, "Recurso: " + ex.Source,
        //            Environment.NewLine, "Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
        //            mensaje = mensaje.Replace("\r\n", "|");
        //            Session["ErrorSql"] = mensaje;
        //            ErrorAlert(mensaje);
        //        }
        //    }
        //    finally
        //    {
        //        GC.Collect();
        //    }
        //}

        protected void grvFactorProd_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                string factor = e.NewValues["Factor"].ToString().Trim();
                //RelFactorProd(3, Convert.ToInt32(e.Keys[0]), Convert.ToInt32(grvProductos.GetRowValues(Convert.ToInt32(HiddenField1.Value), "PRODUCTOKEY").ToString().Trim()), grvProductos.GetRowValues(Convert.ToInt32(HiddenField1.Value), "CVE_PRODUCTO_CLIENTE").ToString().Trim(), e.NewValues["Unidad"].ToString().Trim(), e.NewValues["UnidadConvertir"].ToString().Trim(), Convert.ToInt32(Convert.ToDecimal(factor)));
                RelFactorProd(3, Convert.ToInt32(e.Keys[0]), Convert.ToInt32(hdnProductoKey.Value), hdnClaveProdClienteSel.Value, e.NewValues["Unidad"].ToString().Trim(), e.NewValues["UnidadConvertir"].ToString().Trim(), Convert.ToDecimal(Convert.ToDecimal(factor)));
                e.Cancel = true;
                grvFactorProd.CancelEdit();

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
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void grvFactorProd_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                //RelFactorProd(4, Convert.ToInt32(e.Keys[0]), Convert.ToInt32(grvProductos.GetRowValues(Convert.ToInt32(HiddenField1.Value), "PRODUCTOKEY").ToString().Trim()));
                RelFactorProd(4, Convert.ToInt32(e.Keys[0]), Convert.ToInt32(hdnProductoKey.Value));
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
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void grvFactorProd_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                //RelFactorProd(1, 0, Convert.ToInt32(grvProductos.GetRowValues(Convert.ToInt32(HiddenField1.Value), "PRODUCTOKEY").ToString().Trim()), grvProductos.GetRowValues(Convert.ToInt32(HiddenField1.Value), "CVE_PRODUCTO").ToString().Trim(), e.NewValues["Unidad"].ToString().Trim(), e.NewValues["UnidadConvertir"].ToString().Trim(), Convert.ToInt32(e.NewValues["Factor"].ToString().Trim()));
                RelFactorProd(1, 0, Convert.ToInt32(hdnProductoKey.Value), hdnClaveProductoSel.Value, e.NewValues["Unidad"].ToString().Trim(), e.NewValues["UnidadConvertir"].ToString().Trim(), Convert.ToDecimal(e.NewValues["Factor"].ToString().Trim()));
                e.Cancel = true;
                grvFactorProd.CancelEdit();

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
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
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
                options.SheetName = "Productos";

                compositeLink.CreateDocument();
                using (MemoryStream stream = new MemoryStream())
                {
                    switch (format)
                    {
                        case "xlsx":
                            compositeLink.ExportToXlsx(stream, options);
                            WriteToResponse("Productos", true, format, stream);
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

        protected void cbpCatProductos_Callback(object sender, CallbackEventArgsBase e)
        {
            string lineaError = string.Empty;
            try
            {
                hdnGuardar.Value = "0";
                if (e.Parameter.ToString() == "seleccionaMateriales")
                {
                    hdnFechaSel.Value = Convert.ToDateTime(grvFechaVig.GetRowValues(Convert.ToInt32(hiddenFechaEstruct.Value), "inicio").ToString()).ToString("dd/MM/yyyy");
                    popUpEstructura.HeaderText = hdnTituloEstructura.Value + " FECHA INICIO: " + hdnFechaSel.Value;
                    Session["grvListaMaterial"] = null;
                    RelEstructMaterial(1, 0, string.Empty, 0, 0, 0, Convert.ToInt32(hdnProductoKey.Value), Convert.ToInt32(grvFechaVig.GetRowValues(Convert.ToInt32(hiddenFechaEstruct.Value), "estructurakey").ToString().Trim()));
                    grvFechaVig.Selection.SelectRow(Convert.ToInt32(hiddenFechaEstruct.Value));
                    grvFechaVig.Selection.SelectRowByKey(Convert.ToInt32(grvFechaVig.GetRowValues(Convert.ToInt32(hiddenFechaEstruct.Value), "estructurakey").ToString().Trim()));

                    grvLstMateriales.Columns["CommandColumnLista"].Visible = true;
                    //((GridViewCommandColumn)grvLstMateriales.Columns["CommandColumnLista"]).ShowNewButtonInHeader = true;

                    //lkb_Nuevo_Mat.Visible = true;
                    //hdnGuardar.Value = "4";
                    //NuevoEstructura();
                }
                else if (e.Parameter.ToString() == "editarProducto")
                {
                    if (grvProductos.GetSelectedFieldValues("PRODUCTOKEY").Count > 0)
                    {
                        titProducto.InnerHtml = "Editar Producto";
                        Session["Editar"] = true;
                        hdnGuardar.Value = "3";
                        NuevoRegistro();
                        TXT_PRODUCTOKEY.Text = grvProductos.GetSelectedFieldValues("PRODUCTOKEY")[0].ToString().Trim();
                        hdnProductoKey.Value = grvProductos.GetSelectedFieldValues("PRODUCTOKEY")[0].ToString().Trim();
                        TXT_CVE_PRODUCTO.Text = grvProductos.GetSelectedFieldValues("CVE_PRODUCTO")[0].ToString().Trim();
                        TXT_NOMBRE.Text = grvProductos.GetSelectedFieldValues("NOMBRE")[0].ToString().Trim();
                        CMB_UNIDAD.Text = grvProductos.GetSelectedFieldValues("UNIDAD")[0].ToString().Trim();
                        TXT_fraccion.Text = grvProductos.GetSelectedFieldValues("fraccion")[0].ToString().Trim();
                        TXT_CVE_PRODUCTO_CLIENTE.Text = grvProductos.GetSelectedFieldValues("CVE_PRODUCTO_CLIENTE")[0].ToString().Trim();
                        CMB_ALMACEN.Value = grvProductos.GetSelectedFieldValues("ALMACENKEY")[0].ToString().Trim();
                        TXT_AUXILIAR.Text = grvProductos.GetSelectedFieldValues("AUXILIAR")[0].ToString().Trim();
                        TXT_TIPO.Text = grvProductos.GetSelectedFieldValues("TIPO")[0].ToString().Trim();
                        TXT_NICO.Text = grvProductos.GetSelectedFieldValues("NICO")[0].ToString().Trim();
                        CMB_UNIDADT.Value = grvProductos.GetSelectedFieldValues("UNIDADT")[0].ToString().Trim();
                        //
                        TXT_UM_AMERICANA.Text = grvProductos.GetSelectedFieldValues("UM_AMERICANA")[0].ToString().Trim();
                        TXT_FACTOR_UM_AMERICANA.Value = grvProductos.GetSelectedFieldValues("FACTOR_UM_AMERICANA")[0].ToString().Trim();
                        TXT_VALOR.Value = grvProductos.GetSelectedFieldValues("VALOR")[0].ToString().Trim();
                        TXT_MONEDA.Text = grvProductos.GetSelectedFieldValues("MONEDA")[0].ToString().Trim();
                        TXT_USO.Text = grvProductos.GetSelectedFieldValues("USO")[0].ToString().Trim();
                        TXT_CLIENTE.Text = grvProductos.GetSelectedFieldValues("CLIENTE")[0].ToString().Trim();
                        TXT_PAIS.Text = grvProductos.GetSelectedFieldValues("PAIS")[0].ToString().Trim();
                        TXT_COSTONETO.Text = grvProductos.GetSelectedFieldValues("COSTO_NETO")[0].ToString().Trim();
                    }
                    else
                    {
                        hdnGuardar.Value = "2";
                        AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
                    }
                }
                else if (e.Parameter.ToString() == "guardarProducto")
                {
                    lineaError = AddLog("");
                    int ALMACEN = 0;
                    lineaError = AddLog("");
                    string UNIDAD;
                    lineaError = AddLog("");
                    hdnGuardar.Value = "1";
                    lineaError = AddLog("");
                    //if (CMB_ALMACEN.Value == null)
                    //{
                    //    lineaError = AddLog("");
                    //    ALMACEN = 0;
                    //}
                    //else
                    //{
                    //    lineaError = AddLog(" Error en la linea antes de convertir ALMACEN a entero ");
                    //    ALMACEN = Convert.ToInt32(CMB_ALMACEN.Value);
                    //}

                    if (CMB_ALMACEN.Value != null)
                    {
                        if (CMB_ALMACEN.Value.ToString().Trim().Length > 0)
                        {
                            lineaError = AddLog("Error en la linea antes de convertir ALMACEN a entero ");
                            ALMACEN = Convert.ToInt32(CMB_ALMACEN.Value);
                        }
                    }

                    lineaError = AddLog("");
                    if (CMB_UNIDADT.Value == null)
                    {
                        lineaError = AddLog("");
                        UNIDAD = string.Empty;
                    }
                    else
                    {
                        lineaError = AddLog("");
                        UNIDAD = CMB_UNIDADT.Value.ToString().Trim();
                    }

                    decimal factor_um_americana = 0;
                    if (TXT_FACTOR_UM_AMERICANA.Value != null)
                    {
                        lineaError = AddLog("");
                        factor_um_americana = decimal.Parse(TXT_FACTOR_UM_AMERICANA.Text);
                    }

                    decimal valor, costo_neto;
                    valor = 0;
                    costo_neto = 0;

                    if (TXT_VALOR.Value != null)
                    {
                        valor = Convert.ToDecimal(TXT_VALOR.Value);
                    }
                    if (TXT_COSTONETO.Text.Trim() != string.Empty)
                    {
                        costo_neto = Convert.ToDecimal(TXT_COSTONETO.Text.Trim());
                    }



                    lineaError = AddLog("");
                    if (!Convert.ToBoolean(Session["Editar"]))
                    {
                        lineaError = AddLog("Error Inserta");
                        //Inserta
                        CatalogosProductos(1, 0, TXT_CVE_PRODUCTO.Text.Trim(), TXT_NOMBRE.Text.Trim(), CMB_UNIDAD.Text.Trim(), TXT_fraccion.Text.Trim(), TXT_CVE_PRODUCTO_CLIENTE.Text.Trim(), ALMACEN, TXT_AUXILIAR.Text.Trim(), TXT_TIPO.Text.Trim(), UNIDAD, TXT_NICO.Text.Trim(), TXT_UM_AMERICANA.Text.Trim(), factor_um_americana, valor, TXT_MONEDA.Text.Trim(), TXT_USO.Text.Trim(), TXT_CLIENTE.Text.Trim(), TXT_PAIS.Text.Trim(), costo_neto);
                    }
                    else
                    {
                        lineaError = AddLog("Error Actualiza");
                        //Actualiza
                        CatalogosProductos(1, Convert.ToInt32(hdnProductoKey.Value), TXT_CVE_PRODUCTO.Text.Trim(), TXT_NOMBRE.Text.Trim(), CMB_UNIDAD.Text.Trim(), TXT_fraccion.Text.Trim(), TXT_CVE_PRODUCTO_CLIENTE.Text.Trim(), ALMACEN, TXT_AUXILIAR.Text.Trim(), TXT_TIPO.Text.Trim(), UNIDAD, TXT_NICO.Text.Trim(), TXT_UM_AMERICANA.Text.Trim(), factor_um_americana, valor, TXT_MONEDA.Text.Trim(), TXT_USO.Text.Trim(), TXT_CLIENTE.Text.Trim(), TXT_PAIS.Text.Trim(), costo_neto);
                        Session["Editar"] = false;
                    }
                    lineaError = AddLog("");
                    AlertSucces(MsgRegistros.MsgRegistroAgregar);
                }
                else if (e.Parameter.ToString() == "eliminarProducto")
                {
                    hdnGuardar.Value = "1";
                    DataTable dtAcct = new DataTable();
                    RNCat = new RNCatalogos();
                    Session["grvProduct"] = dtAcct = RNCat.CatalogosProductos(4, Convert.ToInt32(grvProductos.GetSelectedFieldValues("PRODUCTOKEY")[0].ToString().Trim()));
                    grvProductos.DataSource = dtAcct;
                    grvProductos.DataBind();
                    grvProductos.Settings.VerticalScrollableHeight = 300;
                    grvProductos.SettingsPager.PageSize = 15;

                    AlertSucces(MsgRegistros.MsgRegistroElimina);
                }
                else if (e.Parameter.ToString() == "LimpiarGridMateriales")
                {
                    grvLstMateriales.DataSource = null;
                    grvLstMateriales.DataBind();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    hdnGuardar.Value = "2";
                    Session["ErrorSql"] = ex.Message.ToString();
                    AlertError(ex.Message.ToString());
                }
                else
                {
                    hdnGuardar.Value = "5";
                    string mensaje = string.Format("Error en la pantalla: {0}. {1} {2} {3} {4}. {5} {6}. {7} {8}.", this.GetType().Name.ToString() + lineaError, Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
                    Environment.NewLine, "Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    Environment.NewLine, "Recurso: " + ex.Source,
                    Environment.NewLine, "Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }



        protected void btnBorrarEst_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxButton btn = (ASPxButton)sender;
                GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)btn.NamingContainer;
                int estructuraKey = Convert.ToInt32(grvFechaVig.GetRowValues(container.VisibleIndex, "estructurakey").ToString().Trim());
                CatEstructuras(2, estructuraKey, string.Empty, Convert.ToInt32(hdnProductoKey.Value));
                grvLstMateriales.Columns["CommandColumnLista"].Visible = false;
                grvLstMateriales.DataSource = null;
                grvLstMateriales.DataBind();
            }
            catch (Exception ex)
            {
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
                    mensaje = mensaje.Replace("\r\n", "|");
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void btn_Init(object sender, EventArgs e)
        {
            try
            {
                ASPxButton btn = (ASPxButton)sender;
                GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)btn.NamingContainer;
                int renglon = container.VisibleIndex;
                int tieneEst = Convert.ToInt32(grvProductos.GetRowValues(container.VisibleIndex, "TIENEESTRUCTURA").ToString().Trim());

                if (renglon > -1)
                {
                    if (tieneEst.Equals(1))
                    {
                        btn.ImageUrl = "~/img/settings.png";
                        btn.Image.ToolTip = "Tiene estructuras";
                    }
                    else
                    {
                        btn.ImageUrl = "../img/settings1.png";
                        btn.Image.ToolTip = "Sin estructuras";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void grvProductos_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            if (e.Column.Name == "Estructuras")
            {
                e.Handled = true;
            }
        }

        protected void grvLstMateriales_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            grvLstMateriales.DataSource = null;
            grvLstMateriales.DataBind();
        }

        //protected void grvFechaVig_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        //{
        //    try
        //    {
        //        if (e.ButtonID == "btnEliminar")
        //        { 
        //            int estructuraKey = Convert.ToInt32(grvFechaVig.GetRowValues(e.VisibleIndex, "estructurakey").ToString().Trim());
        //            CatEstructuras(2, estructuraKey, string.Empty, Convert.ToInt32(hdnProductoKey.Value));
        //            grvLstMateriales.DataSource = null;
        //            grvLstMateriales.DataBind();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}



    }
}