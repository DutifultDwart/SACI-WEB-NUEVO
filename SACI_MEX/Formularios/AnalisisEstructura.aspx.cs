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
using System.IO.Compression;

namespace SACI_MEX.Formularios
{
    public partial class AnalisisEstructura : System.Web.UI.Page
    {

        #region PROPIEDADES GLOBALES

        RNCatalogos RNCat = null;
        RNAnalisis RNAna = null;
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
                    AccesosUsuario();
                    Tratados();
                }

                lkb_Buscar.Attributes.Add("onClick", "return false;");
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
                    Session["grvAnaEsts"] = null;
                }

                
                //Cuando se quiera filtrar el Grid entra en el if
                if (Session["grvAnaEsts"] != null)
                {
                    grvAnaEsts.DataSource = Session["grvAnaEsts"];
                    grvAnaEsts.DataBind();
                    grvAnaEsts.SettingsPager.PageSize = GridPageSize;
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
                Export("xlsx");
                //if (grvAnaEsts.VisibleRowCount > 0)
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

        public void Tratados()
        {
            DataTable dt = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                dt = RNCat.CatalogosTratados(1);

                ASPxDropDownEdit dde = (ASPxDropDownEdit)this.FindControl("ctl00$MainContent$cbpCatAnaEsts$dde_filtros");
                ASPxListBox lb = (ASPxListBox)dde.FindControl("listBox");


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListEditItem itemNP = new ListEditItem();
                    itemNP.Text = dt.Rows[i]["Tratado_Clave"].ToString().Trim();
                    itemNP.Value = dt.Rows[i]["Tratado_Clave"].ToString().Trim();
                    lb.Items.Add(itemNP);

                }
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


        protected void lkb_Analisis_Click(object sender, EventArgs e)
        {
            try
            {
                if (grvAnaEsts.GetSelectedFieldValues("UID_CARGA").Count > 0)
                {
                    if(dde_filtros.Text.Trim().Length.Equals(0))
                    {
                        AlertErrorUsuario("Debe seleciconar al menos un tratado");
                        return;
                    }


                    List<object> lista = grvAnaEsts.GetSelectedFieldValues("UID_CARGA");

                    //int li = lista.Count;
                    for (int i = 0; i < lista.Count; i++)
                    {
                        string pk;
                        pk = lista[i].ToString();
                        //agregar metodo para enviar parametro id UID_CARGA, dde_filtros.Text.Trim()
                    }

                    //Se actualiza el grid
                    VerAnalisisEstructura(1, Convert.ToDateTime(DESDE.Value).ToString("yyyyMMdd"), Convert.ToDateTime(HASTA.Value).ToString("yyyyMMdd"));

                    // AlertSucces("Registro(s) eliminado correctamente");
                }
                else
                    AlertErrorUsuario("Debe seleciconar al menos un registro");

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


        public void VerAnalisisEstructura(int OPCION = 0, string DESDE = "", string HASTA = "")
        {
            DataTable dt = new DataTable();
            try
            {
                RNAna = new RNAnalisis();
                Session["grvAnaEsts"] = dt = RNAna.SP_AnalisisEstructura(OPCION, DESDE, HASTA);
                grvAnaEsts.DataSource = dt;
                grvAnaEsts.DataBind();
                grvAnaEsts.Settings.VerticalScrollableHeight = 300;
                grvAnaEsts.SettingsPager.PageSize = 15;

                LimpiaControles();
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

        public void LimpiaControles()
        {
            try
            {
                TXT_CKey.Text = "0";
                TXT_Clave.Text = string.Empty;
                MEMO_DESC.Text = string.Empty;
                
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
                    return grvAnaEsts.SettingsPager.PageSize;
                return (int)Session[PageSizeSessionKey];
            }
            set { Session[PageSizeSessionKey] = value; }
        }

        public void AccesosUsuario()
        {
            RNCatalogos RNCat = new RNCatalogos();
            // **********************************************************
            // *************************** ACCESOS
            int IDPERFIL = 0;
            if (Session["IDPerfil"] != null)
                IDPERFIL = int.Parse(Session["IDPerfil"].ToString());
            //VALIDA PERMISOS USUARIO
            if (IDPERFIL != 1)
            {
                DataTable dtAccesos = new DataTable("ANEST");
                dtAccesos = RNCat.ACCESOS_USUARIOS(IDPERFIL, "ANEST");

                // Presuming the DataTable has a column named Date.
                string Expression;
                Expression = "CVE_ACTIVIDAD = 'ANEST1'";
                DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = dtAccesos.Select(Expression);
                lkb_Buscar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'ANEST2'";
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
                options.SheetName = "AnaEsts";

                compositeLink.CreateDocument();
                using (MemoryStream stream = new MemoryStream())
                {
                    switch (format)
                    {
                        case "xlsx":
                            compositeLink.ExportToXlsx(stream, options);
                            WriteToResponse("AnaEsts", true, format, stream);
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

        protected void cbpCatAnaEsts_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {               
                if (e.Parameter.ToString() == "buscaAnaEst")
                {
                    VerAnalisisEstructura(1, Convert.ToDateTime(DESDE.Value).ToString("yyyyMMdd"), Convert.ToDateTime(HASTA.Value).ToString("yyyyMMdd"));
                }
                //else if (e.Parameter.ToString() == "editaAnaEst")
                //{
                //    if (grvAnaEsts.GetSelectedFieldValues("AnaEst_Key").Count > 0)
                //    {
                //        Session["Editar"] = true;
                //        hdnGuardar.Value = "3";
                //        NuevoRegistro();
                //        titNewAnaEst.InnerHtml = "Editar AnaEst";
                //        TXT_CKey.Text = grvAnaEsts.GetSelectedFieldValues("AnaEst_Key")[0].ToString().Trim();
                //        hdnAnaEstKey.Value = grvAnaEsts.GetSelectedFieldValues("AnaEst_Key")[0].ToString().Trim();
                //        TXT_Clave.Text = grvAnaEsts.GetSelectedFieldValues("AnaEst_Clave")[0].ToString().Trim();
                //        MEMO_DESC.Text = grvAnaEsts.GetSelectedFieldValues("AnaEst_Descripcion")[0].ToString().Trim();                        
                //    }
                //    else
                //    {
                //        hdnGuardar.Value = "2";
                //        AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
                //    }
                //}
                //else if (e.Parameter.ToString() == "borrarAnaEst")
                //{
                //    if (grvAnaEsts.GetSelectedFieldValues("AnaEst_Key").Count > 0)
                //    {
                //        hdnGuardar.Value = "1";
                //        DataTable dt = new DataTable();
                //        RNCat = new RNCatalogos();
                //        Session["grvAnaEsts"] = dt = RNCat.CatalogosAnaEsts(4, Convert.ToInt32(grvAnaEsts.GetSelectedFieldValues("AnaEst_Key")[0].ToString().Trim()));
                //        grvAnaEsts.DataSource = dt;
                //        grvAnaEsts.DataBind();
                //        AlertSucces(MsgRegistros.MsgRegistroElimina);
                //    }
                //    else
                //    {
                //        hdnGuardar.Value = "2";
                //        AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
                //    }
                //}
                //else if (e.Parameter.ToString() == "guardarAnaEst")
                //{
                //    hdnGuardar.Value = "1";

                //    if (TXT_Clave.Text.Trim().Length.Equals(0))
                //    {
                //        NuevoRegistro();
                //        hdnGuardar.Value = "2";
                //        AlertErrorUsuario(MsgAF.PermisosAlertValidaClave);
                //        return;
                //    }

                //    if (MEMO_DESC.Text.Trim().Length.Equals(0))
                //    {
                //        NuevoRegistro();
                //        hdnGuardar.Value = "2";
                //        AlertErrorUsuario(MsgDivisionAlmacenes.DivisionAlmacenesAlertValidaDescription);
                //        return;
                //    }


                //    //Valida repetir valores
                //    if (Session["grvAnaEsts"] != null)
                //    {
                //        foreach (DataRow fila in ((DataTable)Session["grvAnaEsts"]).Rows)
                //        {
                //            //Al Guardar
                //            if (titNewAnaEst.InnerHtml.Contains("Nuevo") && fila["AnaEst_Clave"].ToString().ToUpper().Trim() == TXT_Clave.Text.ToUpper().Trim())
                //            {
                //                NuevoRegistro();
                //                hdnGuardar.Value = "2";
                //                AlertErrorUsuario("La clave ya existe");
                //                return;
                //            }

                //            //Al Editar
                //            if (titNewAnaEst.InnerHtml.Contains("Editar") &&
                //               fila["AnaEst_Clave"].ToString().Trim().ToUpper() == TXT_Clave.Text.Trim().ToUpper() &&
                //               fila["AnaEst_Key"].ToString().Trim().ToUpper() != TXT_CKey.Text.Trim().ToUpper())
                //            {
                //                NuevoRegistro();
                //                hdnGuardar.Value = "2";
                //                AlertErrorUsuario("La clave ya existe");
                //                return;
                //            }

                //        }
                //    }



                //    if (!Convert.ToBoolean(Session["Editar"]))
                //    {
                //        //Inserta
                //        AnalisisEstructura(2, 0, TXT_Clave.Text.Trim(), MEMO_DESC.Text.Trim());
                //    }
                //    else
                //    {
                //        //Actualiza
                //        AnalisisEstructura(3, Convert.ToInt32(hdnAnaEstKey.Value), TXT_Clave.Text.Trim(), MEMO_DESC.Text.Trim());
                //        Session["Editar"] = false;
                //    }
                //    AlertSucces(MsgRegistros.MsgRegistroAgregar);
                //}
                //else { }
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    hdnGuardar.Value = "2";
                    AlertErrorUsuario(ex.Message.ToString());
                    //lblRepetido.Visible = true;
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

    }
}