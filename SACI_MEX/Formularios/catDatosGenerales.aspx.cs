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
    public partial class catDatosGenerales : System.Web.UI.Page
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
                    CatDatosGenerales(1);
                    CatalogoPlantas(2);
                    //Certificaciones
                    procCertificaciones(1);

                    System.Drawing.SizeF f = new System.Drawing.SizeF(100F, 110F);


                    AccesosUsuario();
                }

                lkb_Nueva_planta.Attributes.Add("onClick", "return false;");
                lkb_Editar_planta.Attributes.Add("onClick", "return false;");
                lkb_Eliminar_planta.Attributes.Add("onClick", "return false;");
                lnk_Guardar_planta.Attributes.Add("onClick", "return false;");
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
                    Session["grvDtsGrales"] = null;
                    Session["grvPlantas"] = null;
                    Session["ErrSqlDGCertificaciones"] = null;
                    Session["grvDGCertificaciones"] = null;
                }

                //Cuando se quiera filtrar el Grid entra en el if
                if (Session["grvPlantas"] != null)
                {
                    grvPlantas.DataSource = Session["grvPlantas"];
                    grvPlantas.DataBind();
                    grvPlantas.SettingsPager.PageSize = GridPageSize;
                }

                if (Session["grvDGCertificaciones"] != null)
                {
                    grvCertificaciones.DataSource = Session["grvDGCertificaciones"];
                    grvCertificaciones.DataBind();
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
        }


        protected void lkb_Editar_Click(object sender, EventArgs e)
        {
            try
            {

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

                //if (DATE_FECHA_VIGENCIA.Value == null)
                //    DATE_FECHA_VIGENCIA.Value = DateTime.Now;

                //CatDatosGenerales(2, Convert.ToInt32(TXT_DGKey.Text), R_TXT_DENOMINACION.Text.Trim(), R_TXT_RFC.Text.Trim(), TXT_REGISTRO_PITEX.Text.Trim(), TXT_REGISTRO_MAQUILA.Text.Trim(), TXT_REGISTRO_ECEX.Text.Trim(), TXT_REGISTRO_RECIME.Text.Trim(), TXT_REGISTRO_PROSEC.Text.Trim(), TXT_CALLE_NUM.Text.Trim(), TXT_CODIGO_POSTAL.Text.Trim(), TXT_COLONIA.Text.Trim(), TXT_ENTIDAD.Text.Trim(), TXT_TELEFONO.Text.Trim(), TXT_FAX.Text.Trim(), TXT_CORREO.Text.Trim(), TXT_ACTIVIDAD.Text.Trim(), TXT_REGISTRO_ALTEX.Text.Trim(), TXT_REGISTRO_IMMEX.Text.Trim(), TXT_PAIS.Text.Trim(), TXT_MUNICIPIO.Text.Trim(), TXT_LOCALIDAD.Text.Trim(), TXT_CALLE_NUM_INT.Text.Trim(), TXT_OFICIO_CERTIFICACION.Text.Trim(), Convert.ToDateTime(DATE_FECHA_VIGENCIA.Value).ToString("yyyyMMdd"));
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


        protected void lkb_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (grvPlantas.VisibleRowCount > 0)
                {
                    //Exporter.WriteXlsToResponse(tit_plantas.InnerText, new XlsExportOptionsEx() { SheetName = tit_plantas.InnerText });
                    Export("xlsx");
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


        protected void lkb_Nueva_planta_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    LimpiControlesPlantas();
            //    NuevoRegistro();
            //}
            //catch (Exception ex)
            //{
            //    if (ex.Message.Substring(0, 1).ToString() == "1")
            //    {
            //        AlertErrorUsuario(ex.Message.ToString());
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
            //finally
            //{
            //    GC.Collect();
            //}
        }

        protected void lkb_Editar_planta_Click(object sender, EventArgs e)
        {
            try
            {
                //if (grvPlantas.GetSelectedFieldValues("PlantaKey").Count > 0)
                //{
                //    NuevoRegistro();
                //    TXT_PLANTAKEY.Text = grvPlantas.GetSelectedFieldValues("PlantaKey")[0].ToString().Trim();
                //    TXT_NOMBRE.Text = grvPlantas.GetSelectedFieldValues("nombre")[0].ToString().Trim();
                //    TXT_UBICACION.Text = grvPlantas.GetSelectedFieldValues("ubicacion")[0].ToString().Trim();
                //    TXT_FOLIO.Text = grvPlantas.GetSelectedFieldValues("Folio")[0].ToString().Trim();
                //    TXT_Direccion1.Text = grvPlantas.GetSelectedFieldValues("Direccion1")[0].ToString().Trim();
                //    TXT_Direccion2.Text = grvPlantas.GetSelectedFieldValues("Direccion2")[0].ToString().Trim();
                //    TXT_Direccion3.Text = grvPlantas.GetSelectedFieldValues("Direccion3")[0].ToString().Trim();
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

        protected void lkb_Eliminar_planta_Click(object sender, EventArgs e)
        {
            try
            {
                //if (grvPlantas.GetSelectedFieldValues("PlantaID").Count > 0)
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

        protected void lnk_Guardar_planta_Click(object sender, EventArgs e)
        {
            try
            {
                //CatalogoPlantas(1, Convert.ToInt32(TXT_PLANTAKEY.Text.Trim()), TXT_PlantaID.Text.Trim(), TXT_NOMBRE.Text.Trim(), TXT_UBICACION.Text.Trim(), Convert.ToInt32(TXT_FOLIO.Text.Trim()), TXT_Direccion1.Text.Trim(), TXT_Direccion2.Text.Trim(), TXT_Direccion3.Text.Trim());
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

        protected void btnAceptarDel_Click(object sender, EventArgs e)
        {
            DataTable dtCtes = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvPlantas"] = dtCtes = RNCat.CatalogoPlantas(4, Convert.ToInt32(grvPlantas.GetSelectedFieldValues("PlantaKey")[0].ToString().Trim()));
                grvPlantas.DataSource = dtCtes;
                grvPlantas.DataBind();
                grvPlantas.SettingsPager.PageSize = 15;
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



        #endregion



        #region METODOS

        public void CatDatosGenerales(int OPCION = 0, int DGKey = 0, string DENOMINACION = "", string RFC = "", string REGISTROPITEX = "", string REGISTROMAQUILA = "", string REGISTROECEX = "",
            string REGISTRORECIME = "", string REGISTROPROSEC = "", string CALLENUMERO = "", string CODIGOPOSTAL = "", string COLONIA = "", string ENTIDAD = "", string TELEFONO = "", string FAX = "",
            string CORREO = "", string ACTIVIDAD = "", string REGISTROALTEX = "", string REGISTROIMMEX = "", string PAIS = "", string MUNICIPIO = "", string LOCALIDAD = "", string CALLENUMEROINTERIOR = "",
            string OFICIOCERTIFICACION = "", string FECHAVIGENCIA = "", string TIPOCERT_EMPRESA = "", string CERT_OEA = "")
        {
            DataTable dtDtsGrales = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                if (OPCION == 1)
                {

                    dtDtsGrales = RNCat.CatDatosGenerales(OPCION, DGKey, DENOMINACION, RFC, REGISTROPITEX, REGISTROMAQUILA, REGISTROECEX, REGISTRORECIME, REGISTROPROSEC, CALLENUMERO, CODIGOPOSTAL, COLONIA, ENTIDAD, TELEFONO, FAX, CORREO, ACTIVIDAD, REGISTROALTEX, REGISTROIMMEX, PAIS, MUNICIPIO, LOCALIDAD, CALLENUMEROINTERIOR, OFICIOCERTIFICACION, FECHAVIGENCIA, TIPOCERT_EMPRESA, CERT_OEA);
                    if (dtDtsGrales.Rows.Count > 0)
                    {
                        TXT_DGKey.Text = dtDtsGrales.Rows[0]["DGKey"].ToString().Trim();
                        R_TXT_DENOMINACION.Text = dtDtsGrales.Rows[0]["Denominacion"].ToString().Trim();
                        R_TXT_RFC.Text = dtDtsGrales.Rows[0]["Rfc"].ToString().Trim();
                        TXT_ACTIVIDAD.Text = dtDtsGrales.Rows[0]["Actividad"].ToString().Trim();
                        TXT_CALLE_NUM.Text = dtDtsGrales.Rows[0]["CalleNumero"].ToString().Trim();
                        TXT_CALLE_NUM_INT.Text = dtDtsGrales.Rows[0]["CalleNumeroInterior"].ToString().Trim();
                        TXT_CODIGO_POSTAL.Text = dtDtsGrales.Rows[0]["Codigopostal"].ToString().Trim();
                        TXT_COLONIA.Text = dtDtsGrales.Rows[0]["Colonia"].ToString().Trim();
                        TXT_LOCALIDAD.Text = dtDtsGrales.Rows[0]["Localidad"].ToString().Trim();
                        TXT_MUNICIPIO.Text = dtDtsGrales.Rows[0]["Municipio"].ToString().Trim();
                        TXT_ENTIDAD.Text = dtDtsGrales.Rows[0]["Entidad"].ToString().Trim();
                        TXT_PAIS.Text = dtDtsGrales.Rows[0]["pais"].ToString().Trim();
                        TXT_TELEFONO.Text = dtDtsGrales.Rows[0]["Telefono"].ToString().Trim();
                        TXT_FAX.Text = dtDtsGrales.Rows[0]["Fax"].ToString().Trim();
                        TXT_CORREO.Text = dtDtsGrales.Rows[0]["Correo"].ToString().Trim();
                        TXT_REGISTRO_PITEX.Text = dtDtsGrales.Rows[0]["RegistroPitex"].ToString().Trim();
                        TXT_REGISTRO_MAQUILA.Text = dtDtsGrales.Rows[0]["RegistroMaquila"].ToString().Trim();
                        TXT_REGISTRO_ALTEX.Text = dtDtsGrales.Rows[0]["RegistroAltex"].ToString().Trim();
                        TXT_REGISTRO_ECEX.Text = dtDtsGrales.Rows[0]["RegistroEcex"].ToString().Trim();
                        TXT_REGISTRO_RECIME.Text = dtDtsGrales.Rows[0]["RegistroRecime"].ToString().Trim();
                        TXT_REGISTRO_PROSEC.Text = dtDtsGrales.Rows[0]["RegistroProsec"].ToString().Trim();
                        TXT_REGISTRO_IMMEX.Text = dtDtsGrales.Rows[0]["RegistroIMMEX"].ToString().Trim();
                        TXT_OFICIO_CERTIFICACION.Text = dtDtsGrales.Rows[0]["oficioCertificacion"].ToString().Trim();
                        DATE_FECHA_VIGENCIA.Value = dtDtsGrales.Rows[0]["fechaVigencia"].ToString().Trim();
                        CMB_TIPOCERT_EMPRESA.Value = dtDtsGrales.Rows[0]["TIPOCERTIFICACION_EMPRESA"].ToString().Trim();
                        TXT_CERT_OEA.Text = dtDtsGrales.Rows[0]["CERTIFICACION_OEA"].ToString().Trim();
                    }
                }
                else
                {
                    dtDtsGrales = RNCat.CatDatosGenerales(OPCION, DGKey, DENOMINACION, RFC, REGISTROPITEX, REGISTROMAQUILA, REGISTROECEX, REGISTRORECIME,
                        REGISTROPROSEC, CALLENUMERO, CODIGOPOSTAL, COLONIA, ENTIDAD, TELEFONO, FAX, CORREO, ACTIVIDAD, REGISTROALTEX, REGISTROIMMEX, PAIS,
                        MUNICIPIO, LOCALIDAD, CALLENUMEROINTERIOR, OFICIOCERTIFICACION, FECHAVIGENCIA, TIPOCERT_EMPRESA, CERT_OEA);
                }

                //LimpiaControles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtDtsGrales.Dispose();
                GC.Collect();
            }
        }


        public void LimpiaControles()
        {
            try
            {

                TXT_DGKey.Text = "0";
                R_TXT_DENOMINACION.Text = string.Empty;
                R_TXT_RFC.Text = string.Empty;
                TXT_REGISTRO_PITEX.Text = string.Empty;
                TXT_REGISTRO_MAQUILA.Text = string.Empty;
                TXT_REGISTRO_ECEX.Text = string.Empty;
                TXT_REGISTRO_RECIME.Text = string.Empty;
                TXT_REGISTRO_PROSEC.Text = string.Empty;
                TXT_CALLE_NUM.Text = string.Empty;
                TXT_CODIGO_POSTAL.Text = string.Empty;
                TXT_COLONIA.Text = string.Empty;
                TXT_ENTIDAD.Text = string.Empty;
                TXT_TELEFONO.Text = string.Empty;
                TXT_FAX.Text = string.Empty;
                TXT_CORREO.Text = string.Empty;
                TXT_ACTIVIDAD.Text = string.Empty;
                TXT_REGISTRO_ALTEX.Text = string.Empty;
                TXT_REGISTRO_IMMEX.Text = string.Empty;
                TXT_PAIS.Text = string.Empty;
                TXT_MUNICIPIO.Text = string.Empty;
                TXT_LOCALIDAD.Text = string.Empty;
                TXT_CALLE_NUM_INT.Text = string.Empty;
                TXT_OFICIO_CERTIFICACION.Text = string.Empty;
                DATE_FECHA_VIGENCIA.Value = DateTime.Now;
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


        public void LimpiControlesPlantas()
        {
            try
            {
                TXT_PlantaID.Text = "0";
                TXT_NOMBRE.Text = string.Empty;
                TXT_UBICACION.Text = string.Empty;
                TXT_FOLIO.Text = string.Empty;
                TXT_Direccion1.Text = string.Empty;
                TXT_Direccion2.Text = string.Empty;
                TXT_Direccion3.Text = string.Empty;
                TXT_PLANTAKEY.Text = "0";
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



        public void CatalogoPlantas(int OPCION = 0, int PLANTAKEY = 0, string PLANTAID = "", string NOMBRE = "", string UBICACION = "", int FOLIO = 0, string DIR1 = "", string DIR2 = "", string DIR3 = "")
        {
            DataTable dtAgente = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvPlantas"] = dtAgente = RNCat.CatalogoPlantas(OPCION, PLANTAKEY, PLANTAID, NOMBRE, UBICACION, FOLIO, DIR1, DIR2, DIR3);
                grvPlantas.DataSource = dtAgente;
                grvPlantas.DataBind();
                grvPlantas.SettingsPager.PageSize = 15;
                LimpiControlesPlantas();
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


        public void procCertificaciones(int OPCION = 0, Int64 Certificacion_Key = 0, string rfc = "", string clave = "", string descripcion = "", string inicio = "", string fin = "")
        {
            DataTable dtCertificaciones = new DataTable();
            RNCat = new RNCatalogos();
            try
            {
                Session["grvDGCertificaciones"] = dtCertificaciones = RNCat.CertificacionesDG(OPCION, Certificacion_Key, rfc, clave, descripcion, inicio, fin);
                grvCertificaciones.DataSource = dtCertificaciones;
                grvCertificaciones.DataBind();
                //grvCertificaciones.SettingsPager.PageSize = 15;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtCertificaciones = null;
                RNCat = null;
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



        //public void AlertQuestion(string mensaje)
        //{

        //    pModalQuestion.InnerText = mensaje;

        //    ScriptManager.RegisterStartupScript(this.Page, typeof(String), "", "<script> document.getElementById('btnQuestion').setAttribute('data-whatever', '" + mensaje + "'); document.getElementById('btnQuestion').click(); </script> ", false);

        //}



        /// <summary>
        /// Propiedad GridPageSize
        /// </summary>
        protected int GridPageSize
        {
            get
            {
                if (Session[PageSizeSessionKey] == null)
                    return grvPlantas.SettingsPager.PageSize;
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
                DataTable dtAccesos = new DataTable("CDAG");
                dtAccesos = RNCat.ACCESOS_USUARIOS(IDPERFIL, "CDAG");

                // Presuming the DataTable has a column named Date.
                string Expression;
                Expression = "CVE_ACTIVIDAD = 'CDAG1'";
                DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = dtAccesos.Select(Expression);
                btnGuardar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CDAG2'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Nueva_planta.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CDAG3'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Editar_planta.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CDAG4'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Eliminar_planta.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CDAG5'";
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
                options.SheetName = h1_titulo.InnerText;

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


        #endregion

        protected void cbpcatDatosGenerales_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                hdnGuardar.Value = "0";
                if (e.Parameter.ToString().Contains("Nuevo"))
                {
                    hdnGuardar.Value = "3";
                    LimpiControlesPlantas();
                    NuevoRegistro();
                    Session["titNewPlanta"] = titNewPlanta.InnerText = "Nueva Planta";
                    lblRepetido.Visible = false;
                }
                else if (e.Parameter.ToString().Contains("Editar"))
                {
                    if (grvPlantas.GetSelectedFieldValues("PlantaKey").Count > 0)
                    {
                        lblRepetido.Visible = false;
                        hdnGuardar.Value = "3";
                        NuevoRegistro();
                        Session["titNewPlanta"] = titNewPlanta.InnerText = "Editar Planta";
                        hdnPLANTAKEY.Value = TXT_PLANTAKEY.Text = grvPlantas.GetSelectedFieldValues("PlantaKey")[0].ToString().Trim();
                        TXT_NOMBRE.Text = grvPlantas.GetSelectedFieldValues("nombre")[0].ToString().Trim();
                        TXT_UBICACION.Text = grvPlantas.GetSelectedFieldValues("ubicacion")[0].ToString().Trim();
                        TXT_FOLIO.Text = grvPlantas.GetSelectedFieldValues("Folio")[0].ToString().Trim();
                        TXT_Direccion1.Text = grvPlantas.GetSelectedFieldValues("Direccion1")[0].ToString().Trim();
                        TXT_Direccion2.Text = grvPlantas.GetSelectedFieldValues("Direccion2")[0].ToString().Trim();
                        TXT_Direccion3.Text = grvPlantas.GetSelectedFieldValues("Direccion3")[0].ToString().Trim();
                    }
                    else
                    {
                        hdnGuardar.Value = "2";
                        AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
                    }
                }
                else if (e.Parameter.ToString().Contains("Borrar"))
                {
                    hdnGuardar.Value = "1";
                    DataTable dtCtes = new DataTable();
                    RNCat = new RNCatalogos();
                    Session["grvPlantas"] = dtCtes = RNCat.CatalogoPlantas(4, Convert.ToInt32(grvPlantas.GetSelectedFieldValues("PlantaKey")[0].ToString().Trim()));
                    grvPlantas.DataSource = dtCtes;
                    grvPlantas.DataBind();
                    grvPlantas.SettingsPager.PageSize = 15;
                    AlertSucces(MsgRegistros.MsgRegistroElimina);
                }
                else if (e.Parameter.ToString().Contains("Guardar"))
                {

                    //int valida = 0;

                    ////Validaciones
                    //if (TXT_NOMBRE.Text.Trim().Length == 0)
                    //{
                    //    valida = 1;
                    //    TXT_NOMBRE.IsValid = false;
                    //}
                    //if (TXT_UBICACION.Text.Trim().Length == 0)
                    //{
                    //    valida = 1;
                    //    TXT_UBICACION.IsValid = false;
                    //}

                    //if (valida.Equals(1))
                    //{
                    //    hdnGuardar.Value = "3";
                    //    NuevoRegistro();

                    //    if (Session["titNewPlanta"] != null)
                    //        titNewPlanta.InnerText = Session["titNewPlanta"].ToString();

                    //    return;
                    //}

                    if (Session["titNewPlanta"] != null)
                        titNewPlanta.InnerText = Session["titNewPlanta"].ToString();


                    string folio = "0";
                    if (!TXT_FOLIO.Text.Trim().Length.Equals(0))
                        folio = TXT_FOLIO.Text.Trim();


                    ////Valida repetir valores
                    //if (Session["grvPlantas"] != null)
                    //{
                    //    hdnGuardar.Value = "2";
                    //    foreach (DataRow fila in ((DataTable)Session["grvPlantas"]).Rows)
                    //    {
                    //        //Al Guardar
                    //        if (titNewPlanta.InnerHtml.Contains("Nueva") && fila["nombre"].ToString().ToUpper().Trim() == TXT_NOMBRE.Text.ToUpper().Trim())
                    //        {
                    //            NuevoRegistro();
                    //            AlertErrorUsuario("El nombre ya existe"); 
                    //            return;
                    //        }                        

                    //        //Al Editar
                    //        if (titNewPlanta.InnerHtml.Contains("Editar") && fila["nombre"].ToString().Trim().ToUpper() == TXT_NOMBRE.Text.Trim().ToUpper() &&
                    //           fila["PlantaKey"].ToString().Trim().ToUpper() != hdnPLANTAKEY.Value.Trim().ToUpper())
                    //        {
                    //            NuevoRegistro();
                    //            AlertErrorUsuario("El nombre ya existe"); 
                    //            return;
                    //        }                        
                    //    }
                    //}

                    hdnGuardar.Value = "1";
                    if (Session["titNewPlanta"] != null && Session["titNewPlanta"].ToString().ToUpper().Contains("NUEV"))
                        CatalogoPlantas(1, Convert.ToInt32(hdnPLANTAKEY.Value.Trim()), TXT_PlantaID.Text.Trim(), TXT_NOMBRE.Text.Trim(), TXT_UBICACION.Text.Trim(), Convert.ToInt32(folio), TXT_Direccion1.Text.Trim(), TXT_Direccion2.Text.Trim(), TXT_Direccion3.Text.Trim());
                    else
                        CatalogoPlantas(5, Convert.ToInt32(hdnPLANTAKEY.Value.Trim()), TXT_PlantaID.Text.Trim(), TXT_NOMBRE.Text.Trim(), TXT_UBICACION.Text.Trim(), Convert.ToInt32(folio), TXT_Direccion1.Text.Trim(), TXT_Direccion2.Text.Trim(), TXT_Direccion3.Text.Trim());
                    AlertSucces(MsgRegistros.MsgRegistroAgregar);
                }
                else if (e.Parameter.ToString().Contains("G2uardar"))
                {

                    int valida = 0;
                    string tipocertEmpresa;

                    //Validaciones
                    if (R_TXT_DENOMINACION.Text.Trim().Length == 0)
                    {
                        valida = 1;
                        R_TXT_DENOMINACION.IsValid = false;
                    }
                    if (R_TXT_RFC.Text.Trim().Length == 0)
                    {
                        valida = 1;
                        R_TXT_RFC.IsValid = false;
                    }

                    if (CMB_TIPOCERT_EMPRESA.Value == null)
                    {
                        tipocertEmpresa = string.Empty;
                    }
                    else
                    {
                        tipocertEmpresa = CMB_TIPOCERT_EMPRESA.Value.ToString();
                    }

                    if (valida.Equals(1))
                        return;


                    hdnGuardar.Value = "1";
                    string fecha_certificacion_iva = string.Empty;
                    if (DATE_FECHA_VIGENCIA.Value != null)
                        fecha_certificacion_iva=Convert.ToDateTime(DATE_FECHA_VIGENCIA.Value).ToString("yyyyMMdd");
                        //DATE_FECHA_VIGENCIA.Value = DateTime.Now;

                    CatDatosGenerales(2, Convert.ToInt32(TXT_DGKey.Text), R_TXT_DENOMINACION.Text.Trim(), R_TXT_RFC.Text.Trim(), TXT_REGISTRO_PITEX.Text.Trim(),
                        TXT_REGISTRO_MAQUILA.Text.Trim(), TXT_REGISTRO_ECEX.Text.Trim(), TXT_REGISTRO_RECIME.Text.Trim(), TXT_REGISTRO_PROSEC.Text.Trim(),
                        TXT_CALLE_NUM.Text.Trim(), TXT_CODIGO_POSTAL.Text.Trim(), TXT_COLONIA.Text.Trim(), TXT_ENTIDAD.Text.Trim(), TXT_TELEFONO.Text.Trim(),
                        TXT_FAX.Text.Trim(), TXT_CORREO.Text.Trim(), TXT_ACTIVIDAD.Text.Trim(), TXT_REGISTRO_ALTEX.Text.Trim(), TXT_REGISTRO_IMMEX.Text.Trim(),
                        TXT_PAIS.Text.Trim(), TXT_MUNICIPIO.Text.Trim(), TXT_LOCALIDAD.Text.Trim(), TXT_CALLE_NUM_INT.Text.Trim(), TXT_OFICIO_CERTIFICACION.Text.Trim(),
                        fecha_certificacion_iva, tipocertEmpresa, TXT_CERT_OEA.Text.Trim());
                    AlertSucces(MsgRegistros.MsgRegistroAgregar);
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    hdnGuardar.Value = "2";
                    //lblRepetido.Visible = true;
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

        protected void grvCertificaciones_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                string rfc, clave, descripcion, inicio, fin;
                rfc = clave = descripcion = inicio = fin = string.Empty;
                if (e.NewValues["RFC"] != null) { rfc = e.NewValues["RFC"].ToString().Trim(); }
                if (e.NewValues["CLAVE_CERTIFICACION"] != null) { clave = e.NewValues["CLAVE_CERTIFICACION"].ToString().Trim(); }
                if (e.NewValues["DESCRIPCION_CERTIFICACION"] != null) { descripcion = e.NewValues["DESCRIPCION_CERTIFICACION"].ToString().Trim(); }
                if (e.NewValues["FECHA_INICIO"] != null) { inicio = e.NewValues["FECHA_INICIO"].ToString().Trim(); }
                if (e.NewValues["FECHA_FIN"] != null) { fin = e.NewValues["FECHA_FIN"].ToString().Trim(); }

                procCertificaciones(2, 0, rfc, clave, descripcion, Convert.ToDateTime(inicio).ToString("yyyyMMdd"), Convert.ToDateTime(fin).ToString("yyyyMMdd"));

                e.Cancel = true;
                grvCertificaciones.CancelEdit();
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    Session["ErrSqlDGCertificaciones"] = ex.Message.ToString();
                    AlertErrorUsuario(ex.Message.ToString());
                }
                else
                {
                    string mensaje = string.Format("Error en la pantalla: {0}. {1} {2} {3} {4}. {5} {6}. {7} {8}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
                    Environment.NewLine, "Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    Environment.NewLine, "Recurso: " + ex.Source,
                    Environment.NewLine, "Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
                    Session["ErrorSql"] = mensaje;
                    Session["ErrSqlDGCertificaciones"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void grvCertificaciones_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                string rfc, clave, descripcion, inicio, fin;
                rfc = clave = descripcion = inicio = fin = string.Empty;
                if (e.NewValues["RFC"] != null) { rfc = e.NewValues["RFC"].ToString().Trim(); }
                if (e.NewValues["CLAVE_CERTIFICACION"] != null) { clave = e.NewValues["CLAVE_CERTIFICACION"].ToString().Trim(); }
                if (e.NewValues["DESCRIPCION_CERTIFICACION"] != null) { descripcion = e.NewValues["DESCRIPCION_CERTIFICACION"].ToString().Trim(); }
                if (e.NewValues["FECHA_INICIO"] != null) { inicio = e.NewValues["FECHA_INICIO"].ToString().Trim(); }
                if (e.NewValues["FECHA_FIN"] != null) { fin = e.NewValues["FECHA_FIN"].ToString().Trim(); }

                procCertificaciones(3, Convert.ToInt32(e.Keys[0]), rfc, clave, descripcion, Convert.ToDateTime(inicio).ToString("yyyyMMdd"), Convert.ToDateTime(fin).ToString("yyyyMMdd"));

                e.Cancel = true;
                grvCertificaciones.CancelEdit();
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    Session["ErrSqlDGCertificaciones"] = ex.Message.ToString();
                    AlertErrorUsuario(ex.Message.ToString());
                }
                else
                {
                    string mensaje = string.Format("Error en la pantalla: {0}. {1} {2} {3} {4}. {5} {6}. {7} {8}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
                     Environment.NewLine, "Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                     Environment.NewLine, "Recurso: " + ex.Source,
                     Environment.NewLine, "Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
                    Session["ErrorSql"] = mensaje;
                    Session["ErrSqlDGCertificaciones"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void grvCertificaciones_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                procCertificaciones(4, Convert.ToInt32(e.Keys[0]));

                e.Cancel = true;
                grvCertificaciones.CancelEdit();
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    Session["ErrSqlDGCertificaciones"] = ex.Message.ToString();
                    AlertErrorUsuario(ex.Message.ToString());
                }
                else
                {
                    string mensaje = string.Format("Error en la pantalla: {0}. {1} {2} {3} {4}. {5} {6}. {7} {8}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
                    Environment.NewLine, "Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    Environment.NewLine, "Recurso: " + ex.Source,
                    Environment.NewLine, "Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
                    Session["ErrorSql"] = mensaje;
                    Session["ErrSqlDGCertificaciones"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void grvCertificaciones_CustomErrorText(object sender, DevExpress.Web.ASPxGridViewCustomErrorTextEventArgs e)
        {
            try
            {
                if (Session["ErrSqlDGCertificaciones"] != null)
                    e.ErrorText = Session["ErrSqlDGCertificaciones"].ToString();
            }
            catch (Exception ex)
            {
                ;
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    Session["ErrSqlDGCertificaciones"] = ex.Message.ToString();
                    AlertErrorUsuario(ex.Message.ToString());
                }
                else
                {
                    string mensaje = string.Format("Error en la pantalla: {0}. {1} {2} {3} {4}. {5} {6}. {7} {8}.", this.GetType().Name.ToString(), Environment.NewLine, ex.Message.Replace("\r\n", "|").Replace("'", "´"),
                    Environment.NewLine, "Evento: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    Environment.NewLine, "Recurso: " + ex.Source,
                    Environment.NewLine, "Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"), ex);
                    Session["ErrorSql"] = mensaje;
                    Session["ErrSqlDGCertificaciones"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

    }
}