using DevExpress.Web;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using ExcelDataReader;
using SACI.Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
//using System.Web.UI.WebControls;

namespace SACI_MEX.Formularios
{
    public partial class procCargaBOM : System.Web.UI.Page
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
                    ConfigurationManager.AppSettings["BDActivo"] = Session["idBase"].ToString().Trim();
                    AccesosUsuario();
                    Session["grvCargaBOM"] = null;
                    Session["grvResBOM"] = null;
                }
                lnk_Cargar.Attributes.Add("onClick", "return false;");
                lnk_Limpiar.Attributes.Add("onClick", "return false;");
                lnk_Guardar.Attributes.Add("onClick", "return false;");
                grvCargaBOM.DataSource = Session["grvCargaBOM"];
                grvCargaBOM.DataBind();
                grvCargaBOM.Settings.VerticalScrollableHeight = 255;
                grvCargaBOM.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;
                grvCargaBOM.SettingsPager.PageSize = 20;

                if (Session["grvResBOM"] != null)
                {
                    grvRes.DataSource = Session["grvResBOM"];
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
                    PageCargaBOM.ActiveTabIndex = 0;
                    grvCargaBOM.DataSource = null;
                    grvCargaBOM.DataBind();
                    PageCargaBOM.ActiveTabIndex = 1;
                    grvRes.DataSource = null;
                    grvRes.DataBind();
                    Session["grvCargaBOM"] = null;
                    Session["grvResBOM"] = null;
                }
                else if (e.Parameter.ToString() == "Guardar")
                {
                    DataTable dt = new DataTable();
                    DataTable dtE = new DataTable();
                    dt = ((DataTable)grvCargaBOM.DataSource);

                    string v_id = string.Empty;
                    for (int i = 0; i < grvCargaBOM.VisibleRowCount; i++)
                    {
                        v_id = grvCargaBOM.GetRowValues(i, new string[] { "IdCarga" }).ToString().Trim();
                        break;
                    }

                    if (v_id != string.Empty)
                    {
                        rncm = new RNCartaMateriales();
                        Session["grvResBOM"] = dtE = rncm.GuardarBOM(v_id);
                        grvRes.DataSource = dtE;
                        grvRes.DataBind();

                        grvRes.Settings.VerticalScrollableHeight = 255;
                        grvRes.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;
                        grvRes.SettingsPager.PageSize = 20;

                        PageCargaBOM.ActiveTabPage = PageCargaBOM.TabPages.FindByText("Resultado");

                        //lnk_Guardar.Visible = false;
                        //lnk_Guardar.Enabled = false;
                    }
                }
                else
                {
                    if (Session["ErrSqlBOM"] != null)
                    {
                        hdnGuardar.Value = "3";
                        ErrorAlert(Session["ErrSqlBOM"].ToString());
                    }
                    else
                    {
                        if (grvCargaBOM.GetRowValues(0, "IdCarga") == null)
                        {
                            hdnGuardar.Value = "2";
                            AlertError("Hubo un error al cargar el archivo, valide las columnas y la información del archivo.");
                        }
                        else
                        {
                            grvCargaBOM.DataSource = Session["grvCargaBOM"];
                            grvCargaBOM.DataBind();
                            grvCargaBOM.Settings.VerticalScrollableHeight = 255;
                            grvCargaBOM.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;
                            grvCargaBOM.SettingsPager.PageSize = 20;
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
            Session["ErrSqlBOM"] = null;
            string columnaErrorFormato = string.Empty;
            int renglon = 0;
            try
            {
                e.UploadedFile.SaveAs(FilePath);
                dt.Columns.Add("CVE_PRODUCTO", typeof(string));
                dt.Columns.Add("PT_FRACCCION", typeof(string));
                dt.Columns.Add("PT_VALOR", typeof(decimal));
                dt.Columns.Add("PT_MONEDA", typeof(string));
                dt.Columns.Add("PT_UNIDAD", typeof(string));
                dt.Columns.Add("E_INICIO", typeof(string));
                dt.Columns.Add("E_FIN", typeof(string));
                dt.Columns.Add("CVE_MATERIAL", typeof(string));
                dt.Columns.Add("CVE_CONSUMO", typeof(decimal));
                dt.Columns.Add("MP_UNIDAD", typeof(string));
                dt.Columns.Add("CVE_FRACCION", typeof(string));
                dt.Columns.Add("CVE_PRECIO_UNITARIO", typeof(decimal));
                dt.Columns.Add("MONEDA", typeof(string));
                dt.Columns.Add("PROV_TAX_ID", typeof(string));
                dt.Columns.Add("CLIENTE_TAX_ID", typeof(string));
                dt.Columns.Add("ORIGEN", typeof(string));

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
                            row["CVE_PRODUCTO"] = fila[0].ToString().Trim();
                            row["PT_FRACCCION"] = fila[1].ToString().Trim();
                            columnaErrorFormato = "PT_VALOR";
                            row["PT_VALOR"] = fila[2].ToString().Trim().Length == 0 ? 0 : double.Parse(fila[2].ToString().Trim());
                            row["PT_MONEDA"] = fila[3].ToString().Trim();
                            row["PT_UNIDAD"] = fila[4].ToString().Trim();
                            columnaErrorFormato = "E_INICIO";
                            string Inicio = string.IsNullOrEmpty(fila[5].ToString()) ? null : DateTime.Parse(fila[5].ToString()).ToString("yyyyMMdd");
                            row["E_INICIO"] = Inicio;
                            columnaErrorFormato = "E_FIN";
                            string Fin = string.IsNullOrEmpty(fila[6].ToString()) ? null : DateTime.Parse(fila[6].ToString()).ToString("yyyyMMdd");
                            row["E_FIN"] = Fin;

                            row["CVE_MATERIAL"] = fila[7].ToString().Trim();

                            columnaErrorFormato = "CVE_CONSUMO";
                            row["CVE_CONSUMO"] = fila[8].ToString().Trim().Length == 0 ? 0 : double.Parse(fila[8].ToString().Trim());

                            row["MP_UNIDAD"] = fila[9].ToString().Trim();
                            row["CVE_FRACCION"] = fila[10].ToString().Trim();
                            columnaErrorFormato = "CVE_PRECIO_UNITARIO";
                            row["CVE_PRECIO_UNITARIO"] = fila[11].ToString().Trim().Length == 0 ? 0 : double.Parse(fila[11].ToString().Trim());
                            row["MONEDA"] = fila[12].ToString().Trim();
                            row["PROV_TAX_ID"] = fila[13].ToString().Trim();
                            row["CLIENTE_TAX_ID"] = fila[14].ToString().Trim();
                            row["ORIGEN"] = fila[15].ToString().Trim();

                            dt.Rows.Add(row);
                        }
                        renglon += 1;
                    }
                    Session["grvCargaBOM"] = dtCm = rncm.ImportaBOM(dt, Name);
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
                            Session["ErrSqlBOM"] = "Error en el archivo " + e.UploadedFile.FileName + ". Verifique el formato de la columna " + columnaErrorFormato + " linea " + renglon + "";
                    }
                    else
                    {
                        Session["ErrSqlBOM"] = mensaje;
                    }
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
                DataTable dtAccesos = new DataTable("IBOM");
                dtAccesos = RNCat.ACCESOS_USUARIOS(IDPERFIL, "IBOM");

                // Presuming the DataTable has a column named Date.
                string expression;
                expression = "CVE_ACTIVIDAD = 'IBOM1'";
                DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = dtAccesos.Select(expression);
                lnk_Cargar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                expression = "CVE_ACTIVIDAD = 'IBOM2'";
                foundRows = dtAccesos.Select(expression);
                lnk_Guardar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                expression = "CVE_ACTIVIDAD = 'IBOM3'";
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
                string Name = "Layout_BOM.xlsx";
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