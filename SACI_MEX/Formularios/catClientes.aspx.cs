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
    public partial class catClientes : System.Web.UI.Page
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
                    CatalogoClientes(2, 0, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 0, "", "", "", "", "", 0, Session["Usuario"].ToString().Trim(), "");
                    AccesosUsuario();
                    CARGAALMACEN();

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
                    Session["grvClientes"] = null;
                }

                
                //Cuando se quiera filtrar el Grid entra en el if
                if (Session["grvClientes"] != null)
                {
                    grvClientes.DataSource = Session["grvClientes"];
                    grvClientes.DataBind();
                    grvClientes.SettingsPager.PageSize = GridPageSize;
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
                //if (grvClientes.VisibleRowCount > 0)
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


        public void CatalogoClientes(int OPCION = 0, int CKEY = 0, string CLAVE = "", string NOMBRE = "", string IDFISCAL = "", string TIPONE = "", string PROGRAMA = "", string CALLENUMERO = "", string CODIGO = "", string COLONIA = "", string ENTIDAD = "", string PAIS = "", string TELEFONO = "", string CORREO = "", string FAX = "", string APATERNO = "", string AMATERNO = "", string CALLE = "", int CALLENUMINT = 0, string LOCALIDAD = "", string REFERENCIA = "", string MUNICIPIO = "", string TIPOIDENTIFICADOR = "", string CP = "", decimal almacenkey = 0, string USUARIO = "", string PLANTAS = "")
        {
            DataTable dtProv = new DataTable();
            try
            {
                RNCat = new RNCatalogos();
                Session["grvClientes"] = dtProv = RNCat.CatalogosClientes(OPCION, CKEY, CLAVE, NOMBRE, IDFISCAL, TIPONE, PROGRAMA, CALLENUMERO, CODIGO, COLONIA, ENTIDAD, PAIS, TELEFONO, CORREO, FAX, APATERNO, AMATERNO, CALLE, CALLENUMINT, LOCALIDAD, REFERENCIA, MUNICIPIO, TIPOIDENTIFICADOR, CP, almacenkey, USUARIO, PLANTAS);
                grvClientes.DataSource = dtProv;
                grvClientes.DataBind();
                grvClientes.Settings.VerticalScrollableHeight = 300;
                grvClientes.SettingsPager.PageSize = 15;

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
                TXT_CKey.Text = "0";
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
                TXT_ApellidoPaterno.Text = string.Empty;
                TXT_ApellidoMaterno.Text = string.Empty;
                TXT_calle.Text = string.Empty;
                TXT_callenumerointerior.Text = string.Empty;
                TXT_localidad.Text = string.Empty;
                TXT_referencia.Text = string.Empty;
                TXT_municipio.Text = string.Empty;
                TXT_tipoidentificador.Text = string.Empty;
                TXT_codigopostal.Text = string.Empty;
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
                    return grvClientes.SettingsPager.PageSize;
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
                DataTable dtAccesos = new DataTable("CCLI");
                dtAccesos = RNCat.ACCESOS_USUARIOS(IDPERFIL, "CCLI");

                // Presuming the DataTable has a column named Date.
                string Expression;
                Expression = "CVE_ACTIVIDAD = 'CCLI1'";
                DataRow[] foundRows;
                // Use the Select method to find all rows matching the filter.
                foundRows = dtAccesos.Select(Expression);
                lkb_Nuevo.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CCLI2'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Editar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CCLI3'";
                foundRows = dtAccesos.Select(Expression);
                lkb_Eliminar.Visible = Convert.ToBoolean(foundRows[0].ItemArray[0]);

                Expression = "CVE_ACTIVIDAD = 'CCLI4'";
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
                options.SheetName = "Clientes";

                compositeLink.CreateDocument();
                using (MemoryStream stream = new MemoryStream())
                {
                    switch (format)
                    {
                        case "xlsx":
                            compositeLink.ExportToXlsx(stream, options);
                            WriteToResponse("Clientes", true, format, stream);
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

        protected void cbpCatClientes_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                hdnGuardar.Value = "0";
                if (e.Parameter.ToString() == "nuevoCliente")
                {
                    hdnGuardar.Value = "3";
                    LimpiaControles();
                    NuevoRegistro();
                    titNewCliente.InnerHtml = "Nuevo Cliente";
                }
                else if (e.Parameter.ToString() == "editaCliente")
                {
                    if (grvClientes.GetSelectedFieldValues("CKey").Count > 0)
                    {
                        Session["Editar"] = true;
                        hdnGuardar.Value = "3";
                        NuevoRegistro();
                        titNewCliente.InnerHtml = "Editar Cliente";
                        TXT_CKey.Text = grvClientes.GetSelectedFieldValues("CKey")[0].ToString().Trim();
                        hdnClienteKey.Value = grvClientes.GetSelectedFieldValues("CKey")[0].ToString().Trim();
                        TXT_Clave.Text = grvClientes.GetSelectedFieldValues("Clave")[0].ToString().Trim();
                        TXT_NOMBRE.Text = grvClientes.GetSelectedFieldValues("Nombre")[0].ToString().Trim();
                        TXT_Idfiscal.Text = grvClientes.GetSelectedFieldValues("Idfiscal")[0].ToString().Trim();
                        TXT_Tipone.Value = grvClientes.GetSelectedFieldValues("Tipone")[0].ToString().Trim();
                        TXT_Programa.Text = grvClientes.GetSelectedFieldValues("Programa")[0].ToString().Trim();
                        TXT_CalleNumero.Text = grvClientes.GetSelectedFieldValues("CalleNumero")[0].ToString().Trim();
                        TXT_Codigo.Text = grvClientes.GetSelectedFieldValues("Codigo")[0].ToString().Trim();
                        TXT_Colonia.Text = grvClientes.GetSelectedFieldValues("Colonia")[0].ToString().Trim();
                        TXT_Entidad.Text = grvClientes.GetSelectedFieldValues("Entidad")[0].ToString().Trim();
                        TXT_Pais.Text = grvClientes.GetSelectedFieldValues("Pais")[0].ToString().Trim();
                        TXT_Telefono.Text = grvClientes.GetSelectedFieldValues("Telefono")[0].ToString().Trim();
                        TXT_Correo.Text = grvClientes.GetSelectedFieldValues("Correo")[0].ToString().Trim();
                        TXT_Fax.Text = grvClientes.GetSelectedFieldValues("Fax")[0].ToString().Trim();
                        TXT_ApellidoPaterno.Text = grvClientes.GetSelectedFieldValues("ApellidoPaterno")[0].ToString().Trim();
                        TXT_ApellidoMaterno.Text = grvClientes.GetSelectedFieldValues("ApellidoMaterno")[0].ToString().Trim();
                        TXT_calle.Text = grvClientes.GetSelectedFieldValues("calle")[0].ToString().Trim();
                        TXT_callenumerointerior.Text = grvClientes.GetSelectedFieldValues("callenumerointerior")[0].ToString().Trim();
                        TXT_localidad.Text = grvClientes.GetSelectedFieldValues("localidad")[0].ToString().Trim();
                        TXT_referencia.Text = grvClientes.GetSelectedFieldValues("referencia")[0].ToString().Trim();
                        TXT_municipio.Text = grvClientes.GetSelectedFieldValues("municipio")[0].ToString().Trim();
                        TXT_tipoidentificador.Text = grvClientes.GetSelectedFieldValues("tipoidentificador")[0].ToString().Trim();
                        TXT_codigopostal.Text = grvClientes.GetSelectedFieldValues("codigopostal")[0].ToString().Trim();
                        CMB_ALMACEN.Value = grvClientes.GetSelectedFieldValues("almacenkey")[0].ToString().Trim();
                    }
                    else
                    {
                        hdnGuardar.Value = "2";
                        AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
                    }
                }
                else if (e.Parameter.ToString() == "borrarCliente")
                {
                    if (grvClientes.GetSelectedFieldValues("CKey").Count > 0)
                    {
                        hdnGuardar.Value = "1";
                        DataTable dtCtes = new DataTable();
                        RNCat = new RNCatalogos();
                        Session["grvClientes"] = dtCtes = RNCat.CatalogosClientes(4, Convert.ToInt32(grvClientes.GetSelectedFieldValues("CKey")[0].ToString().Trim()));
                        grvClientes.DataSource = dtCtes;
                        grvClientes.DataBind();
                        AlertSucces(MsgRegistros.MsgRegistroElimina);
                    }
                    else
                    {
                        hdnGuardar.Value = "2";
                        AlertErrorUsuario(MsgRegistros.MsgSelectRegistro);
                    }
                }
                else if (e.Parameter.ToString() == "guardarCliente")
                {
                    hdnGuardar.Value = "1";
                    int num_Int;
                    int almacen;



                    if (string.IsNullOrEmpty(TXT_callenumerointerior.Text.Trim()))
                    {
                        num_Int = 0;
                    }
                    else
                    {
                        num_Int = Convert.ToInt32(TXT_callenumerointerior.Text.Trim());
                    }


                    if (CMB_ALMACEN.Value == null)
                    {
                        almacen = 0;
                    }
                    else
                    {
                        almacen = Convert.ToInt32(CMB_ALMACEN.Value);
                    }

                    if (!Convert.ToBoolean(Session["Editar"]))
                    {
                        //Inserta
                        CatalogoClientes(1, 0, TXT_Clave.Text.Trim(), TXT_NOMBRE.Text.Trim(), TXT_Idfiscal.Text.Trim(), TXT_Tipone.Value.ToString().Trim(), TXT_Programa.Text.Trim(), TXT_CalleNumero.Text.Trim(), TXT_Codigo.Text.Trim(), TXT_Colonia.Text.Trim(), TXT_Entidad.Text.Trim(), TXT_Pais.Text.Trim(), TXT_Telefono.Text.Trim(), TXT_Correo.Text.Trim(), TXT_Fax.Text.Trim(), TXT_ApellidoPaterno.Text.Trim(), TXT_ApellidoMaterno.Text.Trim(), TXT_calle.Text.Trim(), num_Int, TXT_localidad.Text.Trim(), TXT_referencia.Text.Trim(), TXT_municipio.Text.Trim(), TXT_tipoidentificador.Text.Trim(), TXT_codigopostal.Text.Trim(), almacen );
                    }
                    else
                    {
                        //Actualiza
                        CatalogoClientes(1, Convert.ToInt32(hdnClienteKey.Value), TXT_Clave.Text.Trim(), TXT_NOMBRE.Text.Trim(), TXT_Idfiscal.Text.Trim(), TXT_Tipone.Value.ToString().Trim(), TXT_Programa.Text.Trim(), TXT_CalleNumero.Text.Trim(), TXT_Codigo.Text.Trim(), TXT_Colonia.Text.Trim(), TXT_Entidad.Text.Trim(), TXT_Pais.Text.Trim(), TXT_Telefono.Text.Trim(), TXT_Correo.Text.Trim(), TXT_Fax.Text.Trim(), TXT_ApellidoPaterno.Text.Trim(), TXT_ApellidoMaterno.Text.Trim(), TXT_calle.Text.Trim(), num_Int, TXT_localidad.Text.Trim(), TXT_referencia.Text.Trim(), TXT_municipio.Text.Trim(), TXT_tipoidentificador.Text.Trim(), TXT_codigopostal.Text.Trim(), almacen);
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