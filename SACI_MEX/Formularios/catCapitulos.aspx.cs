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
    public partial class catCapitulos : System.Web.UI.Page
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
                    CatalogoCapitulos(1);
                    AccesosUsuario();

                    DataTable dt = new DataTable();
                    RNCat = new RNCatalogos();
                    dt = RNCat.CatalogosTratados(1);
                    CBX_TRATADOS.DataSource = dt;
                    CBX_TRATADOS.DataBind();
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
                    Session["grvCapitulos"] = null;
                }

                
                //Cuando se quiera filtrar el Grid entra en el if
                if (Session["grvCapitulos"] != null)
                {
                    grvCapitulos.DataSource = Session["grvCapitulos"];
                    grvCapitulos.DataBind();
                    grvCapitulos.SettingsPager.PageSize = GridPageSize;
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
                //if (grvCapitulos.VisibleRowCount > 0)
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

        #endregion





        #region METODOS


        public void CatalogoCapitulos(int OPCION = 0, Int64 CKEY = 0, string CAPITULO = "", string DESC = "", string TRATADO = "")
        {
            DataTable dt = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvCapitulos"] = dt = RNCat.CatalogosCapitulos(OPCION, CKEY, CAPITULO, DESC, TRATADO);
                grvCapitulos.DataSource = dt;
                grvCapitulos.DataBind();
                grvCapitulos.Settings.VerticalScrollableHeight = 300;
                grvCapitulos.SettingsPager.PageSize = 15;

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
                TXT_Capitulo.Text = string.Empty;
                MEMO_DESC.Text = string.Empty;
                CBX_TRATADOS.SelectedIndex = -1;
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
                    return grvCapitulos.SettingsPager.PageSize;
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
                DataTable dtAccesos = new DataTable("CCAPI");
                dtAccesos = RNCat.ACCESOS_USUARIOS(IDPERFIL, "CCAPI");

                // Presuming the DataTable has a column named Date.
                string Expression;
                Expression = "CVE_ACTIVIDAD = 'CCAPI1'";
                DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = dtAccesos.Select(Expression);
                lkb_Nuevo.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CCAPI2'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Editar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CCAPI3'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Eliminar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CCAPI4'";
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
                options.SheetName = "Capitulos";

                compositeLink.CreateDocument();
                using (MemoryStream stream = new MemoryStream())
                {
                    switch (format)
                    {
                        case "xlsx":
                            compositeLink.ExportToXlsx(stream, options);
                            WriteToResponse("Capitulos", true, format, stream);
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

        protected void cbpCatCapitulos_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                hdnGuardar.Value = "0";
                if (e.Parameter.ToString() == "nuevoCapitulo")
                {
                    hdnGuardar.Value = "3";
                    LimpiaControles();
                    NuevoRegistro();
                    titNewCapitulo.InnerHtml = "Nuevo Capitulo";
                }
                else if (e.Parameter.ToString() == "editaCapitulo")
                {
                    if (grvCapitulos.GetSelectedFieldValues("Capitulo_KEY").Count > 0)
                    {
                        Session["Editar"] = true;
                        hdnGuardar.Value = "3";
                        NuevoRegistro();
                        titNewCapitulo.InnerHtml = "Editar Capitulo";
                        TXT_CKey.Text = grvCapitulos.GetSelectedFieldValues("Capitulo_KEY")[0].ToString().Trim();
                        hdnCapituloKey.Value = grvCapitulos.GetSelectedFieldValues("Capitulo_KEY")[0].ToString().Trim();
                        TXT_Capitulo.Text = grvCapitulos.GetSelectedFieldValues("Capitulo")[0].ToString().Trim();
                        MEMO_DESC.Text = grvCapitulos.GetSelectedFieldValues("Descripcion")[0].ToString().Trim();
                        CBX_TRATADOS.Text = grvCapitulos.GetSelectedFieldValues("Tratado")[0].ToString().Trim();
                    }
                    else
                    {
                        hdnGuardar.Value = "2";
                        AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
                    }
                }
                else if (e.Parameter.ToString() == "borrarCapitulo")
                {
                    if (grvCapitulos.GetSelectedFieldValues("Capitulo_KEY").Count > 0)
                    {
                        hdnGuardar.Value = "1";
                        DataTable dt = new DataTable();
                        RNCat = new RNCatalogos();
                        Session["grvCapitulos"] = dt = RNCat.CatalogosCapitulos(4, Convert.ToInt32(grvCapitulos.GetSelectedFieldValues("Capitulo_KEY")[0].ToString().Trim()));
                        grvCapitulos.DataSource = dt;
                        grvCapitulos.DataBind();
                        AlertSucces(MsgRegistros.MsgRegistroElimina);
                    }
                    else
                    {
                        hdnGuardar.Value = "2";
                        AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
                    }
                }
                else if (e.Parameter.ToString() == "guardarCapitulo")
                {
                    hdnGuardar.Value = "1";

                    if (TXT_Capitulo.Text.Trim().Length.Equals(0))
                    {
                        NuevoRegistro();
                        hdnGuardar.Value = "2";
                        AlertErrorUsuario(MsgAF.PermisosAlertValidaClave);
                        return;
                    }

                    if (MEMO_DESC.Text.Trim().Length.Equals(0))
                    {
                        NuevoRegistro();
                        hdnGuardar.Value = "2";
                        AlertErrorUsuario(MsgDivisionAlmacenes.DivisionAlmacenesAlertValidaDescription);
                        return;
                    }


                    //Valida repetir valores
                    if (Session["grvCapitulos"] != null)
                    {
                        foreach (DataRow fila in ((DataTable)Session["grvCapitulos"]).Rows)
                        {
                            //Al Guardar
                            if (titNewCapitulo.InnerHtml.Contains("Nuevo") && fila["Capitulo"].ToString().ToUpper().Trim() == TXT_Capitulo.Text.ToUpper().Trim())
                            {
                                NuevoRegistro();
                                hdnGuardar.Value = "2";
                                AlertErrorUsuario("El capitulo ya existe");
                                return;
                            }

                            //Al Editar
                            if (titNewCapitulo.InnerHtml.Contains("Editar") &&
                               fila["Capitulo"].ToString().Trim().ToUpper() == TXT_Capitulo.Text.Trim().ToUpper() &&
                               fila["Capitulo_KEY"].ToString().Trim().ToUpper() != TXT_CKey.Text.Trim().ToUpper())
                            {
                                NuevoRegistro();
                                hdnGuardar.Value = "2";
                                AlertErrorUsuario("El capitulo ya existe");
                                return;
                            }

                        }
                    }



                    if (!Convert.ToBoolean(Session["Editar"]))
                    {
                        //Inserta
                        CatalogoCapitulos(2, 0, TXT_Capitulo.Text.Trim(), MEMO_DESC.Text.Trim(), CBX_TRATADOS.Text.Trim());
                    }
                    else
                    {
                        //Actualiza
                        CatalogoCapitulos(3, Convert.ToInt32(hdnCapituloKey.Value), TXT_Capitulo.Text.Trim(), MEMO_DESC.Text.Trim(), CBX_TRATADOS.Text.Trim());
                        Session["Editar"] = false;
                    }
                    AlertSucces(MsgRegistros.MsgRegistroAgregar);
                }
                else { }
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