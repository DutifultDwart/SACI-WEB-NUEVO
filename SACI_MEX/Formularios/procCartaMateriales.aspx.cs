using DevExpress.Web;//
using DevExpress.XtraPrinting;//
using DevExpress.XtraPrintingLinks;//
using ExcelDataReader;//
using SACI.Negocio;//
using System;//
using System.Collections.Generic;//
using System.Configuration;//
using System.Data;//
using System.Drawing;//
using System.IO;//
using System.Linq;//
using System.Web;//
using System.Web.UI;//

namespace SACI_MEX.Formularios
{
    public partial class procCartaMateriales : System.Web.UI.Page
    {


        #region PROPIEDADES GLOBALES

        RNCartaMateriales rncm = null;
        RNCatalogos RNCat = null;
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
                    Session["gridCM"] = null;
                    Session["grvRes"] = null;
                    //lnk_Cargar.Enabled = false;
                    //lnk_Guardar.Visible = false;
                }
                lnk_Cargar.Attributes.Add("onClick", "return false;");
                lnk_Limpiar.Attributes.Add("onClick", "return false;");
                lnk_Guardar.Attributes.Add("onClick", "return false;");
                grvCm.DataSource = Session["gridCM"];
                grvCm.DataBind();
                grvCm.Settings.VerticalScrollableHeight = 255;
                grvCm.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;
                grvCm.SettingsPager.PageSize = 20;

                if (Session["grvRes"] != null)
                {
                    grvRes.DataSource = Session["grvRes"];
                    grvRes.DataBind();
                }
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

        protected void panelPrinc_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                hdnGuardar.Value = "0";
                if (e.Parameter.ToString() == "Limpia")
                {
                    ASPxPageControlCM.ActiveTabIndex = 0;
                    grvCm.DataSource = null;
                    grvCm.DataBind();
                    ASPxPageControlCM.ActiveTabIndex = 1;
                    grvRes.DataSource = null;
                    grvRes.DataBind();
                    Session["gridCM"] = null;
                    Session["grvRes"] = null;
                }
                else if (e.Parameter.ToString() == "Guardar")
                {
                    DataTable dt = new DataTable();
                    DataTable dtE = new DataTable();
                    dt = ((DataTable)grvCm.DataSource);

                    string v_id = string.Empty;
                    for (int i = 0; i < grvCm.VisibleRowCount; i++)
                    {
                        v_id = grvCm.GetRowValues(i, new string[] { "IdCarga" }).ToString().Trim();
                        break;
                    }

                    if (v_id != string.Empty)
                    {
                        rncm = new RNCartaMateriales();
                        Session["grvRes"] = dtE = rncm.Guardar_CartaMateriales(v_id);
                        grvRes.DataSource = dtE;
                        grvRes.DataBind();

                        grvRes.Settings.VerticalScrollableHeight = 255;
                        grvRes.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;
                        grvRes.SettingsPager.PageSize = 20;

                        ASPxPageControlCM.ActiveTabPage = ASPxPageControlCM.TabPages.FindByText("Resultado");

                        //lnk_Guardar.Visible = false;
                        //lnk_Guardar.Enabled = false;
                    }
                }
                else
                {
                    if (Session["ErrorSqlUpl"] != null)
                    {
                        hdnGuardar.Value = "3";
                        ErrorAlert(Session["ErrorSqlUpl"].ToString());
                    }
                    else
                    {
                        if (grvCm.GetRowValues(0, "IdCarga") == null)
                        {
                            hdnGuardar.Value = "2";
                            AlertError("Hubo un error al cargar el archivo, valide las columnas y la información del archivo.");
                        }
                        else
                        {
                            //lnk_Cargar.Enabled = false;
                            //lnk_Guardar.Visible = true;
                            grvCm.DataSource = Session["gridCM"];
                            grvCm.DataBind();
                            grvCm.Settings.VerticalScrollableHeight = 255;
                            grvCm.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;
                            grvCm.SettingsPager.PageSize = 20;
                            hdnGuardar.Value = "1";
                            AlertSucces("Se cargó correctamente el archivo");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    hdnGuardar.Value = "2";
                    AlertError(ex.Message.ToString());
                }
                else
                {
                    hdnGuardar.Value = "3";
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

        protected void UploadControl_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            string Name = Path.GetFileName(e.UploadedFile.FileName);
            string path = ConfigurationManager.AppSettings["FolderPath"];
            string folderPath = Server.MapPath(path);
            folderPath = Path.GetDirectoryName(folderPath);
            folderPath = folderPath + @"\Files";
            string FilePath = folderPath + @"\" + Name;
            DataTable dt = new DataTable();
            rncm = new RNCartaMateriales();
            DataTable dtCm = new DataTable();
            Session["ErrorSqlUpl"] = null;
            string columnaErrorFormato = string.Empty;
            int renglon = 0;
            try
            {
                e.UploadedFile.SaveAs(FilePath);
                dt.Columns.Add("codigodeproducto", typeof(string));
                dt.Columns.Add("CODIGODEMATERIAL1", typeof(string));
                dt.Columns.Add("CODIGODEMATERIAL2", typeof(string));
                dt.Columns.Add("CODIGODEMATERIAL3", typeof(string));
                dt.Columns.Add("CODIGODEMATERIAL4", typeof(string));
                dt.Columns.Add("iniciovigencia", typeof(string));
                dt.Columns.Add("cantidadumc", typeof(decimal));
                dt.Columns.Add("umc", typeof(string));
                dt.Columns.Add("desperdicio", typeof(decimal));
                dt.Columns.Add("tipodematerial", typeof(string));
                dt.Columns.Add("merma", typeof(decimal));
                dt.Columns.Add("DIVISION", typeof(string));
                //[LCG][20240807][Nuevos campos]
                dt.Columns.Add("ORDEN", typeof(string));
                dt.Columns.Add("PEDIMENTO", typeof(string));


                using (FileStream stream = File.Open(FilePath, FileMode.Open, FileAccess.Read))
                {
                    IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream);

                    DataSet result = excelReader.AsDataSet();
                    DataTable dtE = result.Tables[0];
                    var dtResultado = dtE.Rows.Cast<DataRow>().Where(row => !Array.TrueForAll(row.ItemArray, value => { return value.ToString().Length == 0; }));
                    dtE = dtResultado.CopyToDataTable();

                    foreach (DataRow fila in dtE.Rows)
                    {
                        if (renglon != 0)
                        {
                            DataRow row = dt.NewRow();
                            row["codigodeproducto"] = fila[0].ToString();
                            row["CODIGODEMATERIAL1"] = fila[1].ToString();
                            row["CODIGODEMATERIAL2"] = fila[2].ToString();
                            row["CODIGODEMATERIAL3"] = fila[3].ToString();
                            row["CODIGODEMATERIAL4"] = fila[4].ToString();
                            columnaErrorFormato = "Vigencia";
                            String Vigencia = string.IsNullOrEmpty(fila[5].ToString()) ? null : DateTime.Parse(fila[5].ToString()).ToString("yyyyMMdd");
                            row["iniciovigencia"] = Vigencia;
                            columnaErrorFormato = "cantidadumc";
                            row["cantidadumc"] = fila[6].ToString().Trim().Length == 0 ? 0 : double.Parse(fila[6].ToString().Trim());
                            row["umc"] = fila[7].ToString();
                            columnaErrorFormato = "desperdicio";
                            row["desperdicio"] = fila[8].ToString().Trim().Length == 0 ? 0 : double.Parse(fila[8].ToString().Trim());
                            row["tipodematerial"] = fila[9].ToString();
                            columnaErrorFormato = "merma";
                            row["merma"] = fila[10].ToString().Trim().Length == 0 ? 0 : double.Parse(fila[10].ToString().Trim());
                            row["DIVISION"] = fila[11].ToString();
                            row["ORDEN"] = fila[12].ToString();
                            row["PEDIMENTO"] = fila[13].ToString();

                            dt.Rows.Add(row);
                        }
                        renglon += 1;
                    }
                    Session["gridCM"] = dtCm = rncm.ImportaCartaMateriales(dt);
                    e.CallbackData = "";
                }
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

                    if (mensaje.Contains("format") || mensaje.Contains("DateTime"))
                    {
                        string[] Msj = mensaje.Split('.');

                        if (Msj.Length > 0)
                            Session["ErrorSqlUpl"] = "Error en el archivo " + e.UploadedFile.FileName + ". Verifique el formato de la columna " + columnaErrorFormato + " linea " + renglon + "";
                    }
                    else
                    {
                        Session["ErrorSqlUpl"] = mensaje;
                    }
                    //ErrorAlert(mensaje);
                }
            }
            finally
            {
                dt.Dispose();
                rncm = null;
                dtCm.Dispose();
                //Borra el archivo cargado  si es que existe
                if (System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Delete(FilePath);
                }
                GC.Collect();
            }
        }


        #endregion


        #region METODOS


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
                DataTable dtAccesos = new DataTable("ICMAT");
                dtAccesos = RNCat.ACCESOS_USUARIOS(IDPERFIL, "ICMAT");

                // Presuming the DataTable has a column named Date.
                string expression;
                expression = "CVE_ACTIVIDAD = 'ICMAT1'";
                DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = dtAccesos.Select(expression);
                lnk_Cargar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                expression = "CVE_ACTIVIDAD = 'ICMAT2'";
                foundRows = dtAccesos.Select(expression);
                lnk_Guardar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                expression = "CVE_ACTIVIDAD = 'ICMAT3'";
                foundRows = dtAccesos.Select(expression);
                lnkExcel.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

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
                options.SheetName = "Carta Materiales";

                compositeLink.CreateDocument();
                using (MemoryStream stream = new MemoryStream())
                {
                    switch (format)
                    {
                        case "xls":
                            compositeLink.ExportToXlsx(stream, options);
                            WriteToResponse("Carta Materiales Resultados", true, format, stream);
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

        protected void lnkExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Export("xls");
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    hdnGuardar.Value = "2";
                    AlertError(ex.Message.ToString());
                }
                else
                {
                    hdnGuardar.Value = "3";
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

        protected void lnk_DescargarLy_Click(object sender, EventArgs e)
        {
            //Declaración de variables
            string FilePath = string.Empty;
            int valida_err = 0;

            try
            {
                valida_err = 1;
                //Obtiene la ruta del directorio del archivo
                string Name = "Layout_Carta_Materiales.xlsx";
                string path = ConfigurationManager.AppSettings["FolderPath"];
                string folderPath = Server.MapPath(path);
                folderPath = Path.GetDirectoryName(folderPath);
                folderPath = folderPath + @"\Layouts";
                FilePath = folderPath + @"\" + Name;

                //Descarga el archivo
                Response.Clear();
                Page.Response.Buffer = false;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Name);
                Response.ContentType = "application/vnd.ms-excel";
                Response.TransmitFile(FilePath);
                Response.End();
            }
            catch (Exception ex)
            {
                if (valida_err.Equals(0))
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
            }
            finally
            {
                GC.Collect();
            }


        }

    }
}