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
    public partial class catNotasCapitulo : System.Web.UI.Page
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
                    CatalogoNotasCapitulos(1);
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
                    Session["grvNotasCapitulos"] = null;
                }

                
                //Cuando se quiera filtrar el Grid entra en el if
                if (Session["grvNotasCapitulos"] != null)
                {
                    grvNotasCapitulos.DataSource = Session["grvNotasCapitulos"];
                    grvNotasCapitulos.DataBind();
                    grvNotasCapitulos.SettingsPager.PageSize = GridPageSize;
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


        public void CatalogoNotasCapitulos(int OPCION = 0, Int64 KEY = 0, int CAPITULO_ID = 0, string TEXTO = "")
        {
            DataTable dt = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvNotasCapitulos"] = dt = RNCat.CatalogosNotasCapitulos(OPCION, KEY, CAPITULO_ID, TEXTO);
                grvNotasCapitulos.DataSource = dt;
                grvNotasCapitulos.DataBind();
                grvNotasCapitulos.Settings.VerticalScrollableHeight = 300;
                grvNotasCapitulos.SettingsPager.PageSize = 15;

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
                TXT_NotaCapituloID.Text = string.Empty;
                TXT_TEXTO.Text = string.Empty;
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
                    return grvNotasCapitulos.SettingsPager.PageSize;
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
                DataTable dtAccesos = new DataTable("CNCAPI");
                dtAccesos = RNCat.ACCESOS_USUARIOS(IDPERFIL, "CNCAPI");

                // Presuming the DataTable has a column named Date.
                string Expression;
                Expression = "CVE_ACTIVIDAD = 'CNCAPI1'";
                DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = dtAccesos.Select(Expression);
                lkb_Nuevo.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CNCAPI2'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Editar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CNCAPI3'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Eliminar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CNCAPI4'";
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

        protected void cbpCatNotasCapitulos_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                hdnGuardar.Value = "0";
                if (e.Parameter.ToString() == "nuevoNotaCapitulo")
                {
                    hdnGuardar.Value = "3";
                    LimpiaControles();
                    NuevoRegistro();
                    titNewCapitulo.InnerHtml = "Nueva Nota Capítulo";
                }
                else if (e.Parameter.ToString() == "editaNotaCapitulo")
                {
                    if (grvNotasCapitulos.GetSelectedFieldValues("Notas_Capitulo_Key").Count > 0)
                    {
                        Session["Editar"] = true;
                        hdnGuardar.Value = "3";
                        NuevoRegistro();
                        titNewCapitulo.InnerHtml = "Editar Nota Capítulo";
                        TXT_CKey.Text = grvNotasCapitulos.GetSelectedFieldValues("Notas_Capitulo_Key")[0].ToString().Trim();
                        hdnCapituloKey.Value = grvNotasCapitulos.GetSelectedFieldValues("Notas_Capitulo_Key")[0].ToString().Trim();
                        TXT_NotaCapituloID.Text = grvNotasCapitulos.GetSelectedFieldValues("Nota_Capitulo_Id")[0].ToString().Trim();
                        TXT_TEXTO.Text = grvNotasCapitulos.GetSelectedFieldValues("Texto")[0].ToString().Trim();
                        
                    }
                    else
                    {
                        hdnGuardar.Value = "2";
                        AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
                    }
                }
                else if (e.Parameter.ToString() == "borrarNotaCapitulo")
                {
                    if (grvNotasCapitulos.GetSelectedFieldValues("Notas_Capitulo_Key").Count > 0)
                    {
                        hdnGuardar.Value = "1";
                        DataTable dt = new DataTable();
                        RNCat = new RNCatalogos();
                        CatalogoNotasCapitulos(4, Convert.ToInt32(grvNotasCapitulos.GetSelectedFieldValues("Notas_Capitulo_Key")[0].ToString().Trim()));
                        AlertSucces(MsgRegistros.MsgRegistroElimina);
                    }
                    else
                    {
                        hdnGuardar.Value = "2";
                        AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
                    }
                }
                else if (e.Parameter.ToString() == "guardarNotaCapitulo")
                {
                    hdnGuardar.Value = "1";

                    if (TXT_NotaCapituloID.Text.Trim().Length.Equals(0))
                    {
                        NuevoRegistro();
                        hdnGuardar.Value = "2";
                        AlertErrorUsuario("Debe escribir un ID de nota capítulo");
                        return;
                    }

                    if (TXT_TEXTO.Text.Trim().Length.Equals(0))
                    {
                        NuevoRegistro();
                        hdnGuardar.Value = "2";
                        AlertErrorUsuario("Debe escribir un texto");
                        return;
                    }


                    //Valida repetir valores
                    if (Session["grvNotasCapitulos"] != null)
                    {
                        foreach (DataRow fila in ((DataTable)Session["grvNotasCapitulos"]).Rows)
                        {
                            //Al Guardar
                            if (titNewCapitulo.InnerHtml.Contains("Nueva") && fila["Nota_Capitulo_Id"].ToString().ToUpper().Trim() == TXT_NotaCapituloID.Text.ToUpper().Trim())
                            {
                                NuevoRegistro();
                                hdnGuardar.Value = "2";
                                AlertErrorUsuario("La nota capítulo ya existe");
                                return;
                            }

                            //Al Editar
                            if (titNewCapitulo.InnerHtml.Contains("Editar") &&
                               fila["Nota_Capitulo_Id"].ToString().Trim().ToUpper() == TXT_NotaCapituloID.Text.Trim().ToUpper() &&
                               fila["Notas_Capitulo_Key"].ToString().Trim().ToUpper() != TXT_CKey.Text.Trim().ToUpper())
                            {
                                NuevoRegistro();
                                hdnGuardar.Value = "2";
                                AlertErrorUsuario("La nota capítulo ya existe");
                                return;
                            }

                        }
                    }


                    int id_nota = 0;
                    if (TXT_NotaCapituloID.Text.Trim().Length > 0)
                        id_nota = int.Parse(TXT_NotaCapituloID.Text.Trim());

                    if (!Convert.ToBoolean(Session["Editar"]))
                    {
                        //Inserta
                        CatalogoNotasCapitulos(2, 0, id_nota, TXT_TEXTO.Text.Trim());
                    }
                    else
                    {
                        //Actualiza
                        CatalogoNotasCapitulos(3, Convert.ToInt32(hdnCapituloKey.Value), id_nota, TXT_TEXTO.Text.Trim());
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