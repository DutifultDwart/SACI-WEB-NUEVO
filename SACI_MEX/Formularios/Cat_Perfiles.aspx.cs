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
    public partial class Cat_Perfiles : System.Web.UI.Page
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
                    CatalogoPerfiles(1);
                    AccesosUsuario();
                }
                lkb_Eliminar.Attributes.Add("onClick", "return false;");
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
                    Session["grvPerfiles"] = null;

                }

                //Cuando se quiera filtrar el Grid entra en el if
                if (Session["grvPerfiles"] != null)
                {
                    grvPerfiles.DataSource = Session["grvPerfiles"];
                    grvPerfiles.DataBind();
                    grvPerfiles.SettingsPager.PageSize = GridPageSize;
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
                titModalPerfil.InnerText = "Nuevo Perfil";
                NuevoRegistro();               
                
                CargaActividadesPerfiles(1);
                CargaActividadesPerfilesADD(2);
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
                if (grvPerfiles.GetSelectedFieldValues("PK_ID_PERFIL").Count > 0)
                {
                    titModalPerfil.InnerText = "Editar Perfil";
                    NuevoRegistro();
                    mtdPoblarCamposEditar();


                    //IDPerfil.Value = grvPerfiles.GetSelectedFieldValues("PK_ID_PERFIL")[0].ToString().Trim();
                    //TXT_ID_PERFIL.Text = grvPerfiles.GetSelectedFieldValues("PK_ID_PERFIL")[0].ToString().Trim();
                    //TXT_CVE_PERFIL.Text = grvPerfiles.GetSelectedFieldValues("CVE_PERFIL")[0].ToString().Trim();
                    //OBL_TXT_NOM_PERFIL.Text = grvPerfiles.GetSelectedFieldValues("NOM_PERFIL")[0].ToString().Trim();
                    //CargaActividades(1, Convert.ToInt32(TXT_ID_PERFIL.Text));

                    
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                mtdInsertarUpdatePerfiles();
                AlertSucces(MsgRegistros.MsgRegistroAgregar);
                CatalogoPerfiles(1);
                LimpiaControles();
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
            try
            {
                mtdEliminarPerfilActividades();
                CatalogoPerfiles(1);

                //if (grvPerfiles.GetSelectedFieldValues("PK_ID_PERFIL").Count > 0)
                //{
                //    CatalogoPerfiles(3, Convert.ToInt32(grvPerfiles.GetSelectedFieldValues("PK_ID_PERFIL")[0].ToString()), TXT_CVE_PERFIL.Text.Trim(), OBL_TXT_NOM_PERFIL.Text.Trim());
                //    LimpiaControles();
                //    CargaActividades(1);
                //    AlertSucces(MsgRegistros.MsgRegistroElimina);
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

        protected void lkb_Eliminar_Click(object sender, EventArgs e)
        {

        }

        protected void lkb_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (grvPerfiles.VisibleRowCount > 0)
                {
                    Exporter.WriteXlsxToResponse(h1_titulo.InnerText, new XlsxExportOptionsEx() { SheetName = h1_titulo.InnerText });
                }
                else
                {
                    string error = "No hay información para exportar";
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



        #endregion



        #region METODOS


        public void CatalogoPerfiles(int OPCION = 0, int PK_ID_PERFIL = 0, string CVE_PERFIL = "", string NOM_PERFIL = "")
        {
            DataTable dtPerfiles = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvPerfiles"] = dtPerfiles = RNCat.CatPerfiles(OPCION, PK_ID_PERFIL, CVE_PERFIL, NOM_PERFIL);

                grvPerfiles.DataSource = dtPerfiles;
                grvPerfiles.DataBind();
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
              
                TXT_ID_PERFIL.Text = "0";
                TXT_CVE_PERFIL.Text = string.Empty;
                OBL_TXT_NOM_PERFIL.Text = string.Empty;
                
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


        /// <summary>
        /// Propiedad GridPageSize
        /// </summary>
        protected int GridPageSize
        {
            get
            {
                if (Session[PageSizeSessionKey] == null)
                    return grvPerfiles.SettingsPager.PageSize;
                return (int)Session[PageSizeSessionKey];
            }
            set { Session[PageSizeSessionKey] = value; }
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

            //pModalQuestion.InnerText = mensaje;

            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "", "<script> document.getElementById('btnQuestion').setAttribute('data-whatever', '" + mensaje + "'); document.getElementById('btnQuestion').click(); </script> ", false);

        }

        public void NuevoRegistro()
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "MostrarModal", "<script> document.getElementById('btnNuevo').click(); </script> ", false);
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
                DataTable dtAccesos = new DataTable("CPRF");
                dtAccesos = RNCat.ACCESOS_USUARIOS(IDPERFIL, "CPRF");

                // Presuming the DataTable has a column named Date.
                string Expression;
                Expression = "CVE_ACTIVIDAD = 'CPRF1'";
                DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = dtAccesos.Select(Expression);
                lkb_Nuevo.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRF2'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Editar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRF3'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Eliminar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CPRF4'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Excel.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);
            }
        }


        #endregion



        #region Cambios Nuevos

        public void CargaActividadesPerfiles(int OPCION = 0, int PERFIL_ID = 0, int ACTIVIDAD_ID = 0)
        {
            DataTable dt = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvActividades"] = dt = RNCat.CatalogoRelActividades2(OPCION, PERFIL_ID, ACTIVIDAD_ID);


                GRIDACTIVITIES.DataSource = dt;
                GRIDACTIVITIES.DataBind();

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

        public void CargaActividadesPerfilesADD(int OPCION = 0, int PERFIL_ID = 0, int ACTIVIDAD_ID = 0)
        {
            DataTable dt = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvActividadesADD"] = dt = RNCat.CatalogoRelActividades2(OPCION, PERFIL_ID, ACTIVIDAD_ID);


                if (dt != null && dt.Rows.Count > 0)
                {
                    DataView view = dt.DefaultView;
                    view.Sort = "CVE_ACTIVIDAD ASC";
                    DataTable dtSort = view.ToTable();

                    GRIDACTIVITIESADD.DataSource = dtSort;
                    GRIDACTIVITIESADD.DataBind();
                }
                else
                {
                    GRIDACTIVITIESADD.DataSource = dt;
                    GRIDACTIVITIESADD.DataBind();
                }
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


        private void mtdInsertarUpdatePerfiles()
        {
            try
            {
                int idPerfil = int.Parse(TXT_ID_PERFIL.Text.Trim());
                string strClave = TXT_CVE_PERFIL.Text.Trim();
                string strNombrePerfil = OBL_TXT_NOM_PERFIL.Text.Trim();

                if (string.IsNullOrEmpty(strClave))
                {
                    NuevoRegistro();
                    ErrorAlert("Debe escribir una Clave"); //"Debe escribir una Clave"
                    return;
                }

                if (string.IsNullOrEmpty(strNombrePerfil))
                {
                    NuevoRegistro();
                    ErrorAlert("Debe escribir un Nombre"); //"Debe escribir un Nombre"
                    return;
                }

                //Se agrega el nuevo Perfil o se edita el Perfil
                CatalogoPerfiles(2, idPerfil, strClave, strNombrePerfil);

                //[LCG][30/11/2023][Descrubri que cuando se editaba en el select siempre devuelve 0 en la columna ID_INSERT por lo tanto se planchaba el valor de la variable idPerfil inicial 
                //Se valida para que solo se realice cuando sea un nuevo registro]

                if (Session["grvPerfiles"] != null)
                {
                    if (titModalPerfil.InnerText.Contains("Nuev"))
                    {
                        idPerfil = int.Parse(((DataTable)Session["grvPerfiles"]).Rows[0]["ID_INSERT"].ToString());
                    }
                    
                }

                
                //SE ELIMINAN TODAS LOS ACTIVIDADES DEL PERFIL
                CargaActividadesPerfiles(3, idPerfil);


                //SE AGREGAN LAS ACTIVIDADES DEL PERFIL
                for (int i = 0; i < GRIDACTIVITIESADD.VisibleRowCount; i++)
                {
                    int idActividad = int.Parse(GRIDACTIVITIESADD.GetRowValues(i, "ACTIVIDAD_ID").ToString());
                    CargaActividadesPerfiles(4, idPerfil, idActividad);
                }

                //ACTUALIZAR LA SESSION DE ACTIVIDADES
                DataTable dt = new DataTable();
                Session["Actividades"] = dt = RNCat.CatalogoRelActividades2(2, idPerfil);

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void mtdPoblarCamposEditar()
        {
            try
            {
                TXT_ID_PERFIL.Text = grvPerfiles.GetSelectedFieldValues("PK_ID_PERFIL")[0].ToString().Trim();
                int idPerfil = int.Parse(TXT_ID_PERFIL.Text);
                TXT_CVE_PERFIL.Text = grvPerfiles.GetSelectedFieldValues("CVE_PERFIL")[0].ToString().Trim();
                OBL_TXT_NOM_PERFIL.Text = grvPerfiles.GetSelectedFieldValues("NOM_PERFIL")[0].ToString().Trim();

                CargaActividadesPerfiles(6, idPerfil);
                CargaActividadesPerfilesADD(2, idPerfil);

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void mtdEliminarPerfilActividades()
        {
            DataTable dtPerfil = new DataTable();
            try
            {
                int idPerfil = Convert.ToInt32(grvPerfiles.GetSelectedFieldValues("PK_ID_PERFIL")[0].ToString().Trim());

                //Se eliminan las actividades ligadas al Perfil y el Id Perfil en cat_perfiles
                CargaActividadesPerfiles(3, idPerfil);
                CatalogoPerfiles(3, idPerfil);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void bs_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(HiddenField1.Value.ToString().Trim()))
                {
                    NuevoRegistro();
                    AlertErrorUsuario("Debe seleccionar una actividad"); //"Debe seleccionar una actividad"                
                    return;
                }

                DataTable dt = Session["grvActividadesADD"] as DataTable;
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("ACTIVIDAD_ID", typeof(string));
                    dt.Columns.Add("CVE_ACTIVIDAD", typeof(string));
                    dt.Columns.Add("NOM_ACTIVIDAD", typeof(string));
                }

                if (dt.Columns.Count == 4)
                    dt.Columns.RemoveAt(0);



                ///////////////////////////////////////////////////////////////////////////////////
                //Multiseleccionable

                //GRID PARA ELIMINAR DATOS
                DataTable dtActivSelect = Session["grvActividades"] as DataTable;

                List<object> lista = GRIDACTIVITIES.GetSelectedFieldValues("PK_ID_ACTIVIDAD");

                int li = lista.Count;
                for (int i = 0; i < lista.Count; i++)
                {
                    DataRow row = dt.NewRow();

                    string id = GRIDACTIVITIES.GetSelectedFieldValues("PK_ID_ACTIVIDAD")[i].ToString().Trim();
                    string KEY = GRIDACTIVITIES.GetSelectedFieldValues("CVE_ACTIVIDAD")[i].ToString().Trim();
                    string NOMBRE = GRIDACTIVITIES.GetSelectedFieldValues("NOM_ACTIVIDAD")[i].ToString().Trim();

                    row["ACTIVIDAD_ID"] = id;
                    row["CVE_ACTIVIDAD"] = id;
                    row["NOM_ACTIVIDAD"] = id;

                    dt.Rows.Add(id, KEY, NOMBRE);
                    GRIDACTIVITIESADD.DataSource = dt;
                    GRIDACTIVITIESADD.DataBind();


                    //ELIMINAR DATOS
                    foreach (DataRow renglon in dtActivSelect.Rows)
                    {
                        if (renglon["PK_ID_ACTIVIDAD"].ToString().Trim() == id.Trim())
                        {
                            dtActivSelect.Rows.Remove(renglon);
                            break;
                        }
                    }
                }

                if (dtActivSelect.Rows.Count == 0)
                {
                    HiddenField1.Value = string.Empty;
                    dtActivSelect = null;
                }

                //Ordenar datatable
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataView view = dt.DefaultView;
                    view.Sort = "CVE_ACTIVIDAD ASC";
                    DataTable dtSort = view.ToTable();
                    GRIDACTIVITIESADD.DataSource = dtSort;
                    GRIDACTIVITIESADD.DataBind();
                }
                else
                {
                    GRIDACTIVITIESADD.DataSource = dt;
                    GRIDACTIVITIESADD.DataBind();
                }

                NuevoRegistro();

                GRIDACTIVITIES.DataSource = dtActivSelect;
                GRIDACTIVITIES.DataBind();

            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    ErrorAlert(ex.Message.ToString());
                }
                else
                {
                    LimpiaControles();
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

        protected void bs_AddAll_Click(object sender, EventArgs e)
        {
            DataTable dtActDel = null;
            try
            {
                DataTable dtActivSelect = Session["grvActividades"] as DataTable;
                dtActDel = Session["grvActividadesADD"] as DataTable;

                for (int i = 0; i < dtActivSelect.Rows.Count; i++)
                {

                    if (dtActDel.Columns.Count == 0)
                    {
                        dtActDel.Columns.Add("ACTIVIDAD_ID", typeof(string));
                        dtActDel.Columns.Add("CVE_ACTIVIDAD", typeof(string));
                        dtActDel.Columns.Add("NOM_ACTIVIDAD", typeof(string));
                    }
                    if (dtActDel.Columns.Count == 4)
                        dtActDel.Columns.RemoveAt(0);

                    DataRow row = dtActDel.NewRow();

                    string id = dtActivSelect.Rows[i]["PK_ID_ACTIVIDAD"].ToString().Trim();
                    string KEY = dtActivSelect.Rows[i]["CVE_ACTIVIDAD"].ToString().Trim();
                    string NOMBRE = dtActivSelect.Rows[i]["NOM_ACTIVIDAD"].ToString().Trim();

                    row["ACTIVIDAD_ID"] = id;
                    row["CVE_ACTIVIDAD"] = id;
                    row["NOM_ACTIVIDAD"] = id;
                    dtActDel.Rows.Add(id, KEY, NOMBRE);
                }

                GRIDACTIVITIESADD.DataSource = dtActDel;
                GRIDACTIVITIESADD.DataBind();
                dtActivSelect.Clear();
                NuevoRegistro();
                GRIDACTIVITIES.DataSource = dtActivSelect;
                GRIDACTIVITIES.DataBind();
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    ErrorAlert(ex.Message.ToString());
                }
                else
                {
                    LimpiaControles();
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

        protected void bs_DelAll_Click(object sender, EventArgs e)
        {
            DataTable dtActDel = null;
            try
            {
                DataTable dtActivSelect = Session["grvActividadesADD"] as DataTable;
                dtActDel = Session["grvActividades"] as DataTable;

                for (int i = 0; i < dtActivSelect.Rows.Count; i++)
                {
                    if (dtActDel.Columns.Count == 0)
                    {
                        dtActDel.Columns.Add("PK_ID_ACTIVIDAD", typeof(string));
                        dtActDel.Columns.Add("CVE_ACTIVIDAD", typeof(string));
                        dtActDel.Columns.Add("NOM_ACTIVIDAD", typeof(string));
                    }

                    DataRow row = dtActDel.NewRow();

                    string id = dtActivSelect.Rows[i]["ACTIVIDAD_ID"].ToString().Trim();
                    string KEY = dtActivSelect.Rows[i]["CVE_ACTIVIDAD"].ToString().Trim();
                    string NOMBRE = dtActivSelect.Rows[i]["NOM_ACTIVIDAD"].ToString().Trim();

                    row["PK_ID_ACTIVIDAD"] = id;
                    row["CVE_ACTIVIDAD"] = id;
                    row["NOM_ACTIVIDAD"] = id;
                    dtActDel.Rows.Add(id, KEY, NOMBRE);
                }


                GRIDACTIVITIES.DataSource = dtActDel;
                GRIDACTIVITIES.DataBind();
                dtActivSelect.Clear();
                NuevoRegistro();
                GRIDACTIVITIESADD.DataSource = dtActivSelect;
                GRIDACTIVITIESADD.DataBind();
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    ErrorAlert(ex.Message.ToString());
                }
                else
                {
                    LimpiaControles();
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

        protected void bs_Del_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(IndexGridAdd.Value.ToString().Trim()))
                {
                    NuevoRegistro();
                    AlertErrorUsuario("Debe seleccionar una actividad"); //"Debe seleccionar una actividad"                
                    return;
                }

                DataTable dtActDel = Session["grvActividades"] as DataTable;
                if (dtActDel.Columns.Count == 0)
                {
                    dtActDel.Columns.Add("PK_ID_ACTIVIDAD", typeof(string));
                    dtActDel.Columns.Add("CVE_ACTIVIDAD", typeof(string));
                    dtActDel.Columns.Add("NOM_ACTIVIDAD", typeof(string));
                }


                ///////////////////////////////////////////////////////////////////////////////////
                //Multiseleccionable

                //GRID PARA ELIMINAR DATOS
                DataTable dtActivSelect = Session["grvActividadesADD"] as DataTable;

                List<object> lista = GRIDACTIVITIESADD.GetSelectedFieldValues("ACTIVIDAD_ID");

                int li = lista.Count;
                for (int i = 0; i < lista.Count; i++)
                {
                    DataRow row = dtActDel.NewRow();

                    string id = GRIDACTIVITIESADD.GetSelectedFieldValues("ACTIVIDAD_ID")[i].ToString();
                    string KEY = GRIDACTIVITIESADD.GetSelectedFieldValues("CVE_ACTIVIDAD")[i].ToString();
                    string NOMBRE = GRIDACTIVITIESADD.GetSelectedFieldValues("NOM_ACTIVIDAD")[i].ToString();

                    row["PK_ID_ACTIVIDAD"] = id;
                    row["CVE_ACTIVIDAD"] = id;
                    row["NOM_ACTIVIDAD"] = id;

                    dtActDel.Rows.Add(id, KEY, NOMBRE);
                    GRIDACTIVITIES.DataSource = dtActDel;
                    GRIDACTIVITIES.DataBind();


                    //ELIMINAR DATOS
                    foreach (DataRow renglon in dtActivSelect.Rows)
                    {
                        if (renglon["ACTIVIDAD_ID"].ToString().Trim() == id.Trim())
                        {
                            dtActivSelect.Rows.Remove(renglon);
                            break;
                        }
                    }
                }

                if (dtActivSelect.Rows.Count == 0)
                {
                    HiddenField1.Value = string.Empty;
                    dtActivSelect = null;
                }

                //Ordenar datatable
                if (dtActDel != null && dtActDel.Rows.Count > 0)
                {
                    DataView view = dtActDel.DefaultView;
                    view.Sort = "CVE_ACTIVIDAD ASC";
                    DataTable dtActDelSort = view.ToTable();
                    GRIDACTIVITIES.DataSource = dtActDelSort;
                    GRIDACTIVITIES.DataBind();
                }
                else
                {
                    GRIDACTIVITIES.DataSource = dtActDel;
                    GRIDACTIVITIES.DataBind();
                }

                //Ordenar datatable
                if (dtActivSelect != null && dtActivSelect.Rows.Count > 0)
                {
                    DataView view2 = dtActivSelect.DefaultView;
                    view2.Sort = "CVE_ACTIVIDAD ASC";
                    DataTable dtActivSelectSort = view2.ToTable();
                    GRIDACTIVITIESADD.DataSource = dtActivSelectSort;
                    GRIDACTIVITIESADD.DataBind();
                }
                else
                {
                    GRIDACTIVITIESADD.DataSource = dtActivSelect;
                    GRIDACTIVITIESADD.DataBind();
                }

                NuevoRegistro();
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    ErrorAlert(ex.Message.ToString());
                }
                else
                {
                    LimpiaControles();
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



    }
}