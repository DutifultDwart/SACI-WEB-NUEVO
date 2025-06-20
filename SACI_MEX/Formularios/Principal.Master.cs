using SACI.Negocio;
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
    [System.Web.Script.Services.ScriptService]
    public partial class Principal : System.Web.UI.MasterPage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (Session["Perfil"] == null)
                {
                    navMenu.Visible = false;
                }
                else
                {
                    navMenu.Visible = true;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            RNCatalogos RNCat = new RNCatalogos();
            try
            {
                if (Session["CerrarSesion"] != null)
                {
                    if (Convert.ToBoolean(Session["CerrarSesion"]))
                    {
                        Session.Clear();
                    }
                }



                if (!IsPostBack)
                {
                    if (Session["idBase"] != null)
                    {
                        ConfigurationManager.AppSettings["BDActivo"] = Session["idBase"].ToString();
                    }

                    if (Session["Usuario"] != null)
                    {
                        //navBarUsuario.InnerText = "<i class='fa fa-use'></i><span class='clearfix d-none d-sm-inline-block'>" + Session["Usuario"].ToString().Trim() + "</span>";
                        lblUsuario.InnerText = Session["Usuario"].ToString().Trim();
                        lblEmpresa.InnerText = Session["Empresa"].ToString().Trim();

                        // **********************************************************
                        // *************************** ACCESOS
                        int IDPERFIL = 0;
                        if (Session["IDPerfil"] != null)
                            IDPERFIL = int.Parse(Session["IDPerfil"].ToString());
                        ////VALIDA PERMISOS USUARIO
                        //if (IDPERFIL != 1)
                        //{
                        DataTable dtAccesos = new DataTable("ACC");
                        dtAccesos = RNCat.ACCESOS_USUARIOS(IDPERFIL, "");

                        if (dtAccesos.Rows.Count > 0 && dtAccesos.Columns.Count > 1)
                        {

                            // Presuming the DataTable has a column named Date.
                            string expression;
                            expression = "CVE_ACTIVIDAD = 'CDAG'";
                            DataRow[] foundRows;

                            //// PERMISOS CATALOGOS
                            //foundRows = dtAccesos.Select(expression);
                            //DtsGrales.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            // PERMISOS CATALOGO USUARIOS
                            expression = "CVE_ACTIVIDAD = 'CUSR'";
                            foundRows = dtAccesos.Select(expression);
                            CatUsuarios.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            // PERMISOS CATALOGO ACTIVIDADES
                            expression = "CVE_ACTIVIDAD = 'CACT'";
                            foundRows = dtAccesos.Select(expression);
                            CatActividades.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            // PERMISOS CATALOGO PERFILES
                            expression = "CVE_ACTIVIDAD = 'CPRF'";
                            foundRows = dtAccesos.Select(expression);
                            CatPerfiles.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'CTIMAT'";
                            //foundRows = dtAccesos.Select(expression);
                            //TipMat.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            expression = "CVE_ACTIVIDAD = 'CMAT'";
                            foundRows = dtAccesos.Select(expression);
                            Mat.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            expression = "CVE_ACTIVIDAD = 'CPRD'";
                            foundRows = dtAccesos.Select(expression);
                            Prod.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            expression = "CVE_ACTIVIDAD = 'CPRV'";
                            foundRows = dtAccesos.Select(expression);
                            Prov.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            expression = "CVE_ACTIVIDAD = 'CCLI'";
                            foundRows = dtAccesos.Select(expression);
                            Ctes.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            expression = "CVE_ACTIVIDAD = 'CTRAT'";
                            foundRows = dtAccesos.Select(expression);
                            Trat.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            expression = "CVE_ACTIVIDAD = 'CCAPI'";
                            foundRows = dtAccesos.Select(expression);
                            Capit.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //Catalogo Reglas de Origen
                            expression = "CVE_ACTIVIDAD = 'CRORIG'";
                            foundRows = dtAccesos.Select(expression);
                            ReglaOrigen.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //Casos de Regla

                            expression = "CVE_ACTIVIDAD = 'CCASR'";
                            foundRows = dtAccesos.Select(expression);
                            CasoRegla.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);


                            //Catalogo Region
                            expression = "CVE_ACTIVIDAD = 'CREGI'";
                            foundRows = dtAccesos.Select(expression);
                            Region.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'ICMAT'";
                            //foundRows = dtAccesos.Select(expression);
                            //IFCMAT.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //Interface BOM
                            expression = "CVE_ACTIVIDAD = 'IBOM'";
                            foundRows = dtAccesos.Select(expression);
                            ITBOM.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //// ANALISIS
                            expression = "CVE_ACTIVIDAD = 'ANEST'";
                            foundRows = dtAccesos.Select(expression);
                            AnaEstr.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //Notas
                            expression = "CVE_ACTIVIDAD = 'CNOTA'";
                            foundRows = dtAccesos.Select(expression);
                            Nota.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //NOTAS CAPITULO
                            expression = "CVE_ACTIVIDAD = 'CNCAPI'";
                            foundRows = dtAccesos.Select(expression);
                            NCapit.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);


                            //expression = "CVE_ACTIVIDAD = 'CAGE'";
                            //foundRows = dtAccesos.Select(expression);
                            //AgAdu.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'CSUB'";
                            //foundRows = dtAccesos.Select(expression);
                            //SubMaq.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'CAFI'";
                            //foundRows = dtAccesos.Select(expression);
                            //ActF.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'CCAT'";
                            //foundRows = dtAccesos.Select(expression);
                            //Cat.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'CDIV'";
                            //foundRows = dtAccesos.Select(expression);
                            //DivAlm.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'CUNI'";
                            //foundRows = dtAccesos.Select(expression);
                            //Uni.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'CPER'";
                            //foundRows = dtAccesos.Select(expression);
                            //Permi.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'CCON'";
                            //foundRows = dtAccesos.Select(expression);
                            //Consultas.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //// **************************************************
                            //// PERMISOS CAPTURA                                  
                            //// **************************************************                    
                            //expression = "CVE_ACTIVIDAD = 'IMPO'";
                            //foundRows = dtAccesos.Select(expression);
                            //Impo.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'EXPO'";
                            //foundRows = dtAccesos.Select(expression);
                            //Expo.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'CMRE'";
                            //foundRows = dtAccesos.Select(expression);
                            //CReg.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'AFIJ'";
                            //foundRows = dtAccesos.Select(expression);
                            //ActFij.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'REGU'";
                            //foundRows = dtAccesos.Select(expression);
                            //Regu.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'ADES'";
                            //foundRows = dtAccesos.Select(expression);
                            //ActDest.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'DDIR'";
                            //foundRows = dtAccesos.Select(expression);
                            //DescDir.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'CTRS'";
                            //foundRows = dtAccesos.Select(expression);
                            //ConstTran.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'TSUBMAQ'";
                            //foundRows = dtAccesos.Select(expression);
                            //TransSubMaq.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);
                            ////Facturacion
                            //expression = "CVE_ACTIVIDAD = 'FACT'";
                            //foundRows = dtAccesos.Select(expression);
                            //Facturacion.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);
                            ////Facturacion Servicios
                            //expression = "CVE_ACTIVIDAD = 'FACTS'";
                            //foundRows = dtAccesos.Select(expression);
                            //FactServicios.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //// **************************************************
                            //// PERMISOS DESCARGOS                                  
                            //// **************************************************                    
                            //expression = "CVE_ACTIVIDAD = 'BLQD'";
                            //foundRows = dtAccesos.Select(expression);
                            //BloqDes.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'DCAR'";
                            //foundRows = dtAccesos.Select(expression);
                            //Desc.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'ANDS'";
                            //foundRows = dtAccesos.Select(expression);
                            //AnaDesc.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);


                            //// **************************************************
                            //// PERMISOS INFORMES                                  
                            //// **************************************************  
                            //expression = "CVE_ACTIVIDAD = 'IFSAL'";
                            //foundRows = dtAccesos.Select(expression);
                            //ISaldos.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'IFSF'";
                            //foundRows = dtAccesos.Select(expression);
                            //ISalFec.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'IFEST'";
                            //foundRows = dtAccesos.Select(expression);
                            //IEstruc.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'IFIMPO'";
                            //foundRows = dtAccesos.Select(expression);
                            //IImpo.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'IFEXPO'";
                            //foundRows = dtAccesos.Select(expression);
                            //IExpo.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'IFHIM'";
                            //foundRows = dtAccesos.Select(expression);
                            //IHistI.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'IFHEX'";
                            //foundRows = dtAccesos.Select(expression);
                            //IHistE.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'IFVEN'";
                            //foundRows = dtAccesos.Select(expression);
                            //IVenc.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'IFCTM'";
                            //foundRows = dtAccesos.Select(expression);
                            //ICtm.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'IFDES'";
                            //foundRows = dtAccesos.Select(expression);
                            //IDesp.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'IFCOM'";
                            //foundRows = dtAccesos.Select(expression);
                            //ICompul.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'IFDIR'";
                            //foundRows = dtAccesos.Select(expression);
                            //IDirig.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            ////Informe Descargas SCRAP
                            //expression = "CVE_ACTIVIDAD = 'IFDSCRAP'";
                            //foundRows = dtAccesos.Select(expression);
                            //IfDesScrap.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            ////[MBA][19/01/2021][nuevo repote de informe de permisos]
                            ////Informe Permisos
                            //expression = "CVE_ACTIVIDAD = 'IFPER'";
                            //foundRows = dtAccesos.Select(expression);
                            //IPermisos.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            ////[MBA][09/08/2022][2 nuevos repotes en informes]
                            //expression = "CVE_ACTIVIDAD = 'IFRECT'";
                            //foundRows = dtAccesos.Select(expression);
                            //IRectificacion.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'IFEXD'";
                            //foundRows = dtAccesos.Select(expression);
                            //IExpDescargos.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            ////Reporte Complementario (T-MEC antes TLCAN)
                            //expression = "CVE_ACTIVIDAD = 'IFCOMP'";
                            //foundRows = dtAccesos.Select(expression);
                            //IfComplementario.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            ////[MBA][28/04/2023][7 nuevos repotes en informes --especiales para Magna Assembly]
                            //expression = "CVE_ACTIVIDAD = 'IFCF4CTMA'";
                            //foundRows = dtAccesos.Select(expression);
                            //ICF4CTMA.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'IFCCTM'";
                            //foundRows = dtAccesos.Select(expression);
                            //ICCTM.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'IFCHDE'";
                            //foundRows = dtAccesos.Select(expression);
                            //ICHDE.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'IFCSAL'";
                            //foundRows = dtAccesos.Select(expression);
                            //ICSAL.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'IFCMAT'";
                            //foundRows = dtAccesos.Select(expression);
                            //ICMATE.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'IFCPROD'";
                            //foundRows = dtAccesos.Select(expression);
                            //ICPROD.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'IFCESTR'";
                            //foundRows = dtAccesos.Select(expression);
                            //ICESTR.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //expression = "CVE_ACTIVIDAD = 'IFMAUT'";
                            //foundRows = dtAccesos.Select(expression);
                            //IMAUT.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //// INFORMES 31
                            //expression = "CVE_ACTIVIDAD = 'IFA31'";
                            //foundRows = dtAccesos.Select(expression);
                            //Desc31.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);

                            //// INTERFACES
                            //expression = "CVE_ACTIVIDAD = 'ICTM'";
                            //foundRows = dtAccesos.Select(expression);
                            //IFCTM.Visible = foundRows.Length == 0 ? false : Convert.ToBoolean(foundRows[0].ItemArray[0]);



                        }
                        else
                        {
                            // CATALOGOS
                            //DtsGrales.Visible = false;
                            //TipMat.Visible = false;
                            Mat.Visible = false;
                            Prod.Visible = false;
                            Prov.Visible = false;
                            Ctes.Visible = false;
                            Trat.Visible = false;
                            Capit.Visible = false;
                            ReglaOrigen.Visible = false;
                            Region.Visible = false;
                            ITBOM.Visible = false;
                            AnaEstr.Visible = false;
                            NCapit.Visible = false;
                            Nota.Visible = false;
                            CasoRegla.Visible = false;

                        }
                    }

                }
                if (Session["Empresa"] != null)
                {
                    if (lblEmpresa.InnerText.Trim() != Session["Empresa"].ToString().Trim())
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "DoPostBack", "__doPostBack(sender, e)", true);
                        Response.Redirect("/default.aspx");
                    }
                }
                else
                {
                    if (Session["CerrarSesion"] != null)
                    {
                        if (Convert.ToBoolean(Session["CerrarSesion"]))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "DoPostBack", "__doPostBack(sender, e)", true);
                            Response.Redirect("/Formularios/default.aspx");
                        }
                    }
                    else
                        navMenu.Visible = false;
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                GC.Collect();
            }
        }


    }
}