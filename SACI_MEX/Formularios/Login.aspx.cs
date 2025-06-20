using SACI.Datos;
using SACI.Negocio;
using SACI_MEX.Clases;
using SecurityRiosul;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;
using System.Web.Helpers;

namespace SACI_MEX
{
    public partial class Login : System.Web.UI.Page
    {

        #region Propiedades Globales
        //protected string version = "versión " + Assembly.GetExecutingAssembly().GetName().Version.ToString(4);
        RNUsuarios rnUsuario = new RNUsuarios();
        AES encrypth = new AES();
        RNBitacora RNBit = new RNBitacora();
        //RNNotificaciones RNNotify = new RNNotificaciones();   
        ConnectionStringSettingsCollection conexiones = ConfigurationManager.ConnectionStrings;
        RNRegistroSaci rnRegistroSaci = new RNRegistroSaci();
        RNCatalogos rnCatalogos = new RNCatalogos();

        GetADUsers GUser = null;
        #endregion


        #region EVENTOS




        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(Session["Usuario"]).Length > 0)
                {
                    if (Session["RETURN"] != null)
                    {
                        if (Convert.ToInt32(Session["RETURN"]) == 1)
                        {
                            Session["RETURN"] = 0;

                        }
                        else
                        {
                            Response.Redirect("Default.aspx", false);
                        }
                    }
                }


                string UserName = string.Empty;
                if (!IsPostBack)
                {
                    HDu.Value = null;
                    HDp.Value = null;
                    t_u.Text = string.Empty;
                    t_p.Text = string.Empty;
                    t_p.Visible = false;

                    t_u.Focus();

                    MostrarView1();
                    string user_name = string.Empty;

                    WindowsIdentity user = WindowsIdentity.GetCurrent();
                    UserName = HttpContext.Current.User.Identity.Name.ToString().ToUpper();

                    if (UserName != string.Empty && UserName.Contains("\\"))
                    {
                        string[] User = UserName.Split('\\');
                        t_u.Text = User[1].ToString();
                        t_p.Visible = false;
                    }
                    else
                    {
                        t_u.Text = "";
                        t_p.Visible = true;
                    }


                    if (Session["idBase"] != null)
                    {
                        Response.Redirect("default.aspx", false);
                    }
                    else
                    {
                        Session.Clear();
                        idBase.Value = "0";
                        MultiView1.ActiveViewIndex = 0;
                    }
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

        protected void Page_Init(object sender, EventArgs e)
        {
            MyDivI.InnerHtml = AntiForgery.GetHtml().ToString();

            try
            {
                lbl_version.Text = "versión " + Assembly.GetExecutingAssembly().GetName().Version.ToString(4);

                if (Convert.ToString(Session["Usuario"]).Length > 0)
                {
                    if (Session["RETURN"] != null)
                    {
                        if (Convert.ToInt32(Session["RETURN"]) == 1)
                        {
                            Session["RETURN"] = 0;

                        }
                        else
                        {
                            Response.Redirect("Default.aspx", false);
                        }
                    }
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
                    Session["ErrorSql"] = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }


        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            //AntiForgery.Validate();
            DataTable dtUsuarios = new DataTable();
            //string validaRegistro;

            try
            {
                Session.Clear();
                if (idBase.Value.ToString() == "0")
                {
                    MostrarView1();
                    AlertError("Debe seleccionar una base de datos");
                    return;
                }

                Session["idBase"] = null;
                Session["idBase"] = ConfigurationManager.AppSettings["BDActivo"] = idBase.Value.ToString();

                //Se valida informacion del registro
                //try
                //{
                    //Session["RegistroSACI"] = validaRegistro = rnRegistroSaci.ValidaRegistro(1);
                    //if (validaRegistro.Trim().Length > 0)
                    //{
                    //    if (validaRegistro.Substring(0, 2).ToString() == "NO")
                    //    {
                    //        MostrarView1();
                    //        AlertError(MsgRegistroSACI.RegistroInvalido);
                    //        ConfigurationManager.AppSettings["BDActivo"] = null;
                    //        Session["idBase"] = null;
                    //        return;
                    //    }
                    //}
                    //else
                    //{
                    //    MostrarView1();
                    //    AlertError(MsgRegistroSACI.RegistroVacio);
                    //    ConfigurationManager.AppSettings["BDActivo"] = null;
                    //    Session["idBase"] = null;
                    //    Session["RegistroSACI"] = null;
                    //    return;
                    //}

                //}
                //catch (Exception ex)
                //{
                //    MostrarView1();
                //    AlertError(ex.Message.ToString());
                //    ConfigurationManager.AppSettings["BDActivo"] = null;
                //    Session["idBase"] = null;
                //    Session["RegistroSACI"] = null;
                //    return;
                //}

                dtUsuarios = new DataTable();
                //Se valida usuario y contraseña


                //INICIO-DESCIFRAR USUARIO Y PWD
                Encryptacion AesEncryption = new Encryptacion();
                var username = AesEncryption.DecryptStringAES(HDu.Value);
                var password = string.Empty;
                if (t_p.Visible)
                {
                    if (HDp.Value.ToString().Trim() != string.Empty)
                    {
                        password = AesEncryption.DecryptStringAES(HDp.Value);
                    }

                }
                //FIN-DESCIFRAR USUARIO Y PWD


                //Usuario y Contraseña
                //
                string us_newEncrip = encrypth.Encriptar(username);
                string p_newEncrip = string.Empty;
                if (t_p.Visible)
                {
                    if (password.Trim() != string.Empty)
                    {
                        p_newEncrip = encrypth.Encriptar(password);
                    }

                }
                Session["t_u"] = username;
                Session["t_p"] = password;

                //Usuario y Contraseña
                if (t_p.Visible)
                {
                    dtUsuarios = rnUsuario.CatUsuarios(2, 0, username, encrypth.Encriptar(password));
                }
                else
                {
                    dtUsuarios = rnUsuario.CatUsuarios(5, 0, username, encrypth.Encriptar(password));
                }


                if (dtUsuarios.Rows.Count > 0)
                {
                    #region Valida sí el usuario tiene vigencia, entonces valida los días que le quedan para volver a cargar una nueva contraseña

                    if (dtUsuarios.Rows[0]["VALOR_DIAS_VIGENCIA"].ToString().Trim().Length > 0 && int.Parse(dtUsuarios.Rows[0]["VALOR_DIAS_VIGENCIA"].ToString()) <= 0)
                    {
                        Session["IdUsuario"] = dtUsuarios.Rows[0]["PK_ID_USER"].ToString().Trim();
                        MostrarModalVigencia();
                        return;
                    }

                    #endregion

                    #region VALIDA CONFIRMACIÓN POR CORREO

                    string AutentificaCorreo = dtUsuarios.Rows[0]["AC"].ToString().Trim();

                    if (AutentificaCorreo.Trim().Length > 0 && AutentificaCorreo.ToUpper().Equals("SI"))
                    {
                        string correo_enviar = dtUsuarios.Rows[0]["DES_MAIL_USER"].ToString().Trim();

                        if (correo_enviar.Trim().Length.Equals(0))
                        {
                            MostrarView1();
                            AlertError("El usuario no tiene dirección de correo");
                            return;
                        }


                        MultiView1.ActiveViewIndex = 2;

                        //Obtener correo


                        //Generar código verificador
                        Guid codigo = Guid.NewGuid();
                        Session["codigo_verificadorM"] = codigo.ToString().Substring(0, 8);

                        //Datos correo que envia
                        string mensaje = string.Empty;
                        DataTable dtMail = new DataTable();
                        rnCatalogos = new RNCatalogos();

                        dtMail = rnCatalogos.TraerMail();
                        string mailFrom = string.Empty;
                        string mailpwd = string.Empty;
                        string mailsmtp = string.Empty;
                        int puerto = 0;
                        if (dtMail != null && dtMail.Rows.Count > 0)
                        {
                            mailFrom = dtMail.Rows[0]["MailFrom"].ToString().Trim();
                            mailpwd = dtMail.Rows[0]["PwdMailFrom"].ToString().Trim();
                            mailsmtp = dtMail.Rows[0]["Smtp"].ToString().Trim();
                            puerto = int.Parse(dtMail.Rows[0]["Puerto"].ToString().Trim());
                            //Enviar por correo el código verificador
                            //
                            try
                            {
                                string body = "<body>" +
                                              "<h2>Generador de código de acceso de SACI Web</h2>" +
                                              "<br/>" +
                                              "<span>Copie el siguiente código y agreguelo en el sistema para poder accesar:</span>" +
                                              "<h4>" + Session["codigo_verificadorM"].ToString().Trim() + "</h4>" +
                                              "<br/>" +
                                              "<span>Saludos cordiales</span>" +
                                              "<br/><br/><br/><br/>" +
                                              "<table style='width:100%; border-spacing:0px; padding:0px' border='0'> " +
                                              "   <tr> " +
                                              "     <td style='width:100%; text-align:center; font-size:10pt'> " +
                                              "          <h5> " +
                                              "             SACI System by Sac Grupo de Ingeniería S.A. de C.V &copy;" + DateTime.Now.Year.ToString() + ". Todos los derechos reservados. &#10; " +
                                              "         </h5> " +
                                              "     </td> " +
                                              "   </tr> " +
                                              "</table> " +
                                              "</body>";
                                MailMessage mail = new MailMessage();
                                SmtpClient SmtpServer = new SmtpClient(mailsmtp);
                                mail.From = new MailAddress(mailFrom);
                                mail.To.Add(correo_enviar);
                                mail.Subject = "Código de acceso";
                                mail.IsBodyHtml = true;
                                mail.Body = body;

                                //Importante linea de código para servidores windows 2016  
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                SmtpServer.Port = puerto;
                                SmtpServer.Credentials = new System.Net.NetworkCredential(mailFrom, mailpwd);
                                SmtpServer.EnableSsl = true;
                                SmtpServer.Send(mail);

                                Session["dt_usuario"] = dtUsuarios;

                                AlertSuccessM("Se envío un código de acceso al correo ", correo_enviar);
                            }
                            catch (Exception ex)
                            {
                                AlertError(ex.Message);
                                throw;
                            }
                        }

                        return;
                    }

                    #endregion

                    #region VALIDA SI EL USUARIO TIENE VERIFICACIÓN SMS Y NÚMERO DE CELULAR
                    if ((dtUsuarios.Rows[0]["SMS"].ToString().Trim() != null && (dtUsuarios.Rows[0]["SMS"].ToString().Trim().Equals("1") || dtUsuarios.Rows[0]["SMS"].ToString().Trim().ToUpper().Equals("SI"))) &&
                        dtUsuarios.Rows[0]["NUMERO_CEL"].ToString().Trim().Length >= 10)
                    {

                        //Generar código verificador
                        Guid codigo = Guid.NewGuid();
                        Session["codigo_verificador"] = codigo.ToString().Substring(0, 8);

                        Session["dt_usuario"] = dtUsuarios;

                        //AGREGAR API ENVIO SMS CODIGO
                        RNSMS sms = new RNSMS();
                        sms.apiKey = "14006";
                        sms.country = "MX";
                        sms.dial = "27272";
                        sms.message = "Su codigo de verificacion para saci-web es " + Session["codigo_verificador"].ToString();
                        sms.msisdns = "[" + dtUsuarios.Rows[0]["NUMERO_CEL"].ToString().Trim() + "]";
                        sms.tag = "tag-prueba";

                        RNSMS_Token token = new RNSMS_Token();
                        token.Authorization = "ZClIC7/1HJozW1JYKWk8d+iuENA=";
                        token.TokenType = "application/json";
                        RNSMS_API api = new RNSMS_API();

                        try
                        {
                            var respuesta = SACI.Negocio.RNSMS_API.EnvioSMS(sms, token);

                            if (respuesta != null)
                            {
                                if (!respuesta.TieneError)
                                {
                                    MultiView1.ActiveViewIndex = 2;
                                    AlertSuccessV("Se envío un código de acceso al celular", dtUsuarios.Rows[0]["NUMERO_CEL"].ToString().Trim());
                                    //AlertSuccessV("Se envío un código de acceso al celular", dtUsuarios.Rows[0]["NUMERO_CEL"].ToString().Trim() + ". " + respuesta.Response.ToString());
                                }
                                else
                                {
                                    MostrarView1();
                                    AlertError("Sucedio un error al enviar el código por SMS " + respuesta.message.ToString());
                                }
                            }
                        }
                        catch (Exception exx)
                        {
                            AlertError(exx.Message);
                            return;
                        }

                        //MultiView1.ActiveViewIndex = 2;
                        //AlertSuccessV("Se envío un código (" + Session["codigo_verificador"].ToString() + ") de acceso al celular", dtUsuarios.Rows[0]["NUMERO_CEL"].ToString().Trim());


                        return;
                    }
                    #endregion

                    ///ASIGNAMOS LAS VARIABLES DE SESION DEL SISTEMA
                    ///
                    Session["IDPerfil"] = dtUsuarios.Rows[0]["FK_ID_PERFIL_USER"].ToString().Trim();
                    Session["IDPerfilSACI"] = dtUsuarios.Rows[0]["FK_ID_PERFIL_USER"].ToString().Trim();
                    Session["Usuario"] = username;
                    Session["Empresa"] = dtUsuarios.Rows[0]["EMPRESA"].ToString().Trim();
                    Session["IdUsuario"] = dtUsuarios.Rows[0]["PK_ID_USER"].ToString().Trim();
                    FormsAuthentication.RedirectFromLoginPage(username, false);
                    HttpCookie cookieSACI = Request.Cookies["SACISession"];
                    if (cookieSACI == null)
                        //cookieSACI = new HttpCookie("SACISession");
                        cookieSACI = new HttpCookie("SACISession", username);
                    cookieSACI.Expires = DateTime.Today.AddMinutes(1);
                    Response.Cookies.Add(cookieSACI);
                    LimpiaControles();
                    Session["Menu"] = "0";
                    Session["Perfil"] = "admin";


                    ////Valida si se quiere guardar información en bitacora
                    //rnCatalogos = new RNCatalogos();
                    //DataTable dt = new DataTable();
                    //dt = rnCatalogos.InfoRegistro(1);
                    //Session["IdBitacora"] = string.Empty;

                    //try
                    //{
                    //    if (dt != null && dt.Rows[0]["BITACORA"].ToString().Equals("1"))
                    //    {
                    //        rnCatalogos = new RNCatalogos();
                    //        DataTable dtB = rnCatalogos.ValidarBitacora(1, 0, Int64.Parse(Session["IdUsuario"].ToString()), "", "", "");
                    //        Session.Add("IdBitacora", dtB.Rows[0]["IDKEY_BITACORA"].ToString().Trim());
                    //    }
                    //    else
                           Session["IdBitacora"] = string.Empty;
                    //}
                    //catch (Exception)
                    //{
                    //    Session["IdBitacora"] = string.Empty;
                    //}


                }
                else
                {

                    //Validar, si el usuario existe

                    dtUsuarios = new DataTable();
                    string menssaje = string.Empty;
                    int count = 0;

                    dtUsuarios = rnUsuario.Acceso(0, username);

                    if (dtUsuarios != null && dtUsuarios.Rows.Count > 0)
                    {
                        //El usuario existe, validar consulta de usuario si es igual o mayor a 3 registros en el tiempo de 20 min
                        DataTable dtv = new DataTable();
                        DateTime fecha2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                        string mes2 = fecha2.Month.ToString().Length.Equals(1) ? "0" + fecha2.Month.ToString() : fecha2.Month.ToString();
                        string dia2 = fecha2.Day.ToString().Length.Equals(1) ? "0" + fecha2.Day.ToString() : fecha2.Day.ToString();
                        string xfecha2 = fecha2.Year.ToString() + mes2 + dia2 + " " + fecha2.ToString("HH:mm:ss");

                        dtv = rnUsuario.Acceso(1, username, "", "", "", "", xfecha2);


                        if (dtv != null)
                        {
                            if (dtv.Rows.Count.Equals(0))
                            {
                                count = 1;
                            }
                            if (dtv.Rows.Count.Equals(1))
                            {
                                count = 2;
                            }
                            else if (dtv.Rows.Count.Equals(2))
                            {
                                count = 3;
                            }
                            if (dtv.Rows.Count > 2)
                            {
                                count = 3;
                                MostrarView1();
                                AlertError("Usuario Bloqueado, contactar al administrador para desbloquear");
                            }
                        }
                        else
                            count = 1;


                        //insertar en tabla Acceso el usuario
                        if (dtv == null || dtv.Rows.Count < 3)
                        {
                            DateTime fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                            string mes = fecha.Month.ToString().Length.Equals(1) ? "0" + fecha.Month.ToString() : fecha.Month.ToString();
                            string dia = fecha.Day.ToString().Length.Equals(1) ? "0" + fecha.Day.ToString() : fecha.Day.ToString();
                            string xfecha = fecha2.Year.ToString() + mes + dia + " " + fecha2.ToString("HH:mm:ss");
                            rnUsuario.Acceso(2, username, "Iniciar sesion fallida", "No se pudo iniciar sesion por password incorrecto: " + password, "", xfecha, xfecha);
                        }

                        //Obtener la hora inicial
                        dtv = new DataTable();
                        fecha2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                        string mes0 = fecha2.Month.ToString().Length.Equals(1) ? "0" + fecha2.Month.ToString() : fecha2.Month.ToString();
                        string dia0 = fecha2.Day.ToString().Length.Equals(1) ? "0" + fecha2.Day.ToString() : fecha2.Day.ToString();
                        string xfecha0 = fecha2.Year.ToString() + mes0 + dia0 + " " + fecha2.ToString("HH:mm:ss");

                        dtv = rnUsuario.Acceso(1, username, "", "", "", "", xfecha0);

                        Session["primer_intento"] = dtv.Rows[0]["Fecha"].ToString().Substring(11, 14);


                        if (count >= 3)
                        {
                            MostrarView1();
                            lbl_Intentos.Visible = false;
                            lbl_Intentos.Text = string.Empty;
                            AlertError("Usuario Bloqueado, contactar al administrador para desbloquear");
                        }
                        else
                        {
                            MostrarView1();
                            lbl_Intentos.Visible = true;
                            lbl_Intentos.Text = count.ToString() + " de 3 intentos, para bloquearse, esperar 20 minutos a partir de\n" + Session["primer_intento"].ToString();
                        }
                    }
                    else
                    {
                        MostrarView1();
                        ConfigurationManager.AppSettings["BDActivo"] = null;
                        Session["idBase"] = null;
                        lbl_Intentos.Text = string.Empty;
                        lbl_Intentos.Visible = false;
                        AlertError(MsgCatUsuarios.UsuarioInvalido);
                    }
                }

                //RNBit.InsertBitacora("Se ingresó al sistema concentrado web el usuario: " + Session["Usuario"].ToString(), DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), Session["Usuario"].ToString(), "", Environment.MachineName);
            }
            catch (Exception ex)
            {
                Session["idBase"] = null;
                MostrarView1();
                idBase.Value = "0";
                //AlertError(ex.Message.ToString());

                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    AlertError(ex.Message.ToString());
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
        }

        //Valida el guardar la nueva contraseña por vigencia
        protected void BtnValidaPwd_Click(object sender, EventArgs e)
        {
            try
            {
                //Validar la contraseña actual no debe ser igual a la contraseña nueva

                if (TXT_PWD_A.Text.Trim() == string.Empty)
                {
                    MostrarModalVigencia();
                    AlertError("Escriba la contraseña actual");
                    return;
                }

                if (!TXT_PWD_A.Text.Trim().Equals(Session["t_p"].ToString()))
                {
                    MostrarModalVigencia();
                    AlertError("La contraseña actual no coincide");
                    return;
                }

                if (TXT_PWD_B.Text.Trim() == string.Empty)
                {
                    MostrarModalVigencia();
                    AlertError("Escriba la nueva contraseña");
                    return;
                }

                if (TXT_PWD_C.Text.Trim() == string.Empty)
                {
                    MostrarModalVigencia();
                    AlertError("Confirme la nueva contraseña");
                    return;
                }

                if (!TXT_PWD_B.Text.Trim().Equals(TXT_PWD_C.Text.Trim()))
                {
                    MostrarModalVigencia();
                    AlertError("La contraseña nueva no es igual a la de confirmación");
                    return;
                }

                if (TXT_PWD_A.Text.Trim().Equals(TXT_PWD_B.Text.Trim()))
                {
                    MostrarModalVigencia();
                    AlertError("La contraseña nueva es igual a la contraseña actual");
                    return;
                }

                DataTable dt = new DataTable();
                string mensaje = string.Empty;

                dt = rnUsuario.CatUsuarios(6, int.Parse(Session["IdUsuario"].ToString()), "", encrypth.Encriptar(TXT_PWD_B.Text.Trim()));

                if (!dt.Rows[0]["Valor"].ToString().Equals("OK"))
                {
                    MostrarModalVigencia();
                    AlertError(dt.Rows[0]["Valor"].ToString());
                }
                else
                {
                    AlertSucces("Contraseña actualizada");
                    MostrarView1();
                    ConfigurationManager.AppSettings["BDActivo"] = null;
                    Session["idBase"] = null;
                    lbl_Intentos.Text = string.Empty;
                    lbl_Intentos.Visible = false;
                }

                t_p.Text = string.Empty;

            }
            catch (Exception)
            {

                throw;
            }
            finally { GC.Collect(); }
        }

        protected void LinkCancelPwd_Click(object sender, EventArgs e)
        {
            try
            {
                MostrarView1();
                ConfigurationManager.AppSettings["BDActivo"] = null;
                Session["idBase"] = null;
                lbl_Intentos.Text = string.Empty;
                lbl_Intentos.Visible = false;
            }
            catch (Exception)
            {

                throw;
            }
            finally { GC.Collect(); }
        }


        #region METODOS

        public void LimpiaControles()
        {
            try
            {
                t_u.Text = string.Empty;
                t_p.Text = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LimpiaControlesRegistro()
        {
            try
            {
                idBase.Value = "0";
                txtRFC.Text = string.Empty;
                txtRazonSocial.Text = string.Empty;
                txtRegistro.Text = string.Empty;
                txtCadenaNumerica.Text = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void MostrarView1()
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "MostrarView1", "<script> document.getElementById('btnView1').click(); </script> ", false);
        }

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

        private void MostrarModalVigencia()
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "MostrarModalVigencia", "<script> document.getElementById('btnModalVigencia').click(); </script> ", false);
        }

        #endregion

        protected void lkb_Registrar_Click(object sender, EventArgs e)
        {
            DataTable dtDatosGrales = new DataTable();

            try
            {
                if (idBase.Value.ToString() != "0" && t_p.Text.ToString() == "SACREGISTRO")
                {
                    Session["idBase"] = ConfigurationManager.AppSettings["BDActivo"] = idBase.Value.ToString();
                    dtDatosGrales = rnCatalogos.InfoRegistro(1);

                    MultiView1.ActiveViewIndex = 1;
                    if (dtDatosGrales.Rows.Count > 0)
                    {
                        txtRFC.Text = dtDatosGrales.Rows[0]["Rfc"].ToString().Trim();
                        txtRazonSocial.Text = dtDatosGrales.Rows[0]["Denominacion"].ToString().Trim();
                        txtRegistro.Text = dtDatosGrales.Rows[0]["REGISTROSACI"].ToString().Trim();
                    }
                }
                else
                {
                    MostrarView1();
                    AlertError("Debe seleccionar una BD e ingresar usuario/contraseña");
                    ConfigurationManager.AppSettings["BDActivo"] = null;
                    Session["idBase"] = null;
                    idBase.Value = "0";
                    return;
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

        protected void lkb_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["idBase"] != null)
                {
                    ConfigurationManager.AppSettings["BDActivo"] = Session["idBase"].ToString();
                }

                string validaFecha = string.Empty;
                DataTable dtDatosGrales = new DataTable();
                dtDatosGrales = rnCatalogos.InfoRegistro(2, txtRazonSocial.Text.Trim(), txtRFC.Text.Trim(), txtRegistro.Text.Trim());
                validaFecha = rnRegistroSaci.ValidaRegistro(2);
                //lblFechaVigencia.Visible = true;
                //Registrar();
                AlertSucces(validaFecha);

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
                    Session["ErrorEx"] = ex.Message.ToString();
                    lblError.Visible = true;
                    lblError.InnerText = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void lkb_Cancelar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiaControlesRegistro();
                MultiView1.ActiveViewIndex = 0;
                MostrarView1();

            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    AlertError(ex.Message.ToString());
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

        protected void lnk_ConvertirSimbolos_Click(object sender, EventArgs e)
        {
            try
            {
                string cadenanumerica;
                string resultado;

                cadenanumerica = txtCadenaNumerica.Text.Trim();
                if (cadenanumerica == string.Empty)
                {
                    AlertError("Debe ingresar un valor");
                    return;
                }
                resultado = rnRegistroSaci.PipesStringToChine(cadenanumerica);
                //txtRegistro.Text = resultado;
                //hdnCadenaRegistro.Value = resultado;
                DataTable dtDatosGrales = new DataTable();
                dtDatosGrales = rnCatalogos.InfoRegistro(2, txtRazonSocial.Text.Trim(), txtRFC.Text.Trim(), resultado);
                AlertSucces("Se actualizó correctamente la cadena de registro.");
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
                    Session["ErrorEx"] = ex.Message.ToString();
                    lblError.Visible = true;
                    lblError.InnerText = mensaje;
                    ErrorAlert(mensaje);
                }
            }
            finally
            {
                GC.Collect();
            }
        }


        #region CODIGO VERIFICACIÓN SMS

        protected void btnAbrirModalVerificar_Click(object sender, EventArgs e)
        {
            try
            {
                //Abrir modal de verificación
                MostrarModalVerificador();
                ModalTit.InnerHtml = "Código de acceso";
                lblTit_Verificar1.Text = "Instrucciones:";
                lblTit_Verificar2.Text = "1. Copie el código de su celular";
                lblTit_Verificar3.Text = "2. Agreguelo en la siguiente casilla";
                lblTit_Verificar4.Text = "3. De clic en el botón verificar";

                TxT_CodigoVerificador.Text = string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
            finally { GC.Collect(); }
        }

        protected void btnVerificar_Click(object sender, EventArgs e)
        {
            try
            {
                //Valida Session["codigo_verificador"] y TxT_CodigoVerificador.Text que sean iguales
                if (Session["codigo_verificador"] != null && Session["codigo_verificador"].ToString().Trim() == TxT_CodigoVerificador.Text.Trim())
                {
                    if (Session["t_u"] == null)
                    {
                        AlertError("Vuelva a cargar la página");
                        ConfigurationManager.AppSettings["BDActivo"] = null;
                        Session["idBase"] = null;
                        return;
                    }


                    Session["codigo_verificador"] = null;
                    string mensaje = string.Empty;

                    if (Session["dt_usuario"] == null)
                    {
                        AlertError("El código ingresado caducó");
                        return;
                    }

                    DataTable dtUsuarios = ((DataTable)Session["dt_usuario"]);

                    if (dtUsuarios.Rows.Count > 0)
                    {
                        ///ASIGNAMOS LAS VARIABLES DE SESION DEL SISTEMA
                        ///
                        Session["IDPerfil"] = dtUsuarios.Rows[0]["FK_ID_PERFIL_USER"].ToString().Trim();
                        Session["Usuario"] = Session["t_u"].ToString().Trim();
                        Session["Empresa"] = dtUsuarios.Rows[0]["EMPRESA"].ToString().Trim();
                        Session["IdUsuario"] = dtUsuarios.Rows[0]["PK_ID_USER"].ToString().Trim();
                        FormsAuthentication.RedirectFromLoginPage(Session["t_u"].ToString(), false);
                        HttpCookie cookieSACI = Request.Cookies["SACISession"];
                        if (cookieSACI == null)
                            //cookieSACI = new HttpCookie("SACISession");
                            cookieSACI = new HttpCookie("SACISession", Session["t_u"].ToString());
                        cookieSACI.Expires = DateTime.Today.AddMinutes(1);
                        Response.Cookies.Add(cookieSACI);
                        LimpiaControles();
                        Session["Menu"] = "0";
                        Session["Perfil"] = "admin";


                        ////Valida si se quiere guardar información en bitacora
                        //rnCatalogos = new RNCatalogos();
                        //DataTable dt = new DataTable();
                        //dt = rnCatalogos.InfoRegistro(1);

                        //if (dt != null && dt.Rows[0]["BITACORA"].ToString().Equals("1"))
                        //{
                        //    rnCatalogos = new RNCatalogos();
                        //    DataTable dtB = rnCatalogos.ValidarBitacora(1, 0, Int64.Parse(Session["IdUsuario"].ToString()), "", "", "");
                        //    Session.Add("IdBitacora", dtB.Rows[0]["IDKEY_BITACORA"].ToString().Trim());
                        //}
                        //else
                            Session["IdBitacora"] = string.Empty;


                    }
                    else
                    {
                        ConfigurationManager.AppSettings["BDActivo"] = null;
                        Session["idBase"] = null;
                        AlertError(MsgCatUsuarios.UsuarioInvalido);
                    }

                }
                else
                {
                    MostrarModalVerificador();
                    AlertError("El código ingresado caducó o no es correcto");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { GC.Collect(); }
        }

        protected void btRegresar_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            MostrarView1();
        }

        public void AlertSuccessV(string mensaje, string mensaje2)
        {
            pModalSuccesV.InnerText = mensaje;
            pModalSuccesV2.InnerText = mensaje2;
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "", "<script> document.getElementById('btnSuccessV').setAttribute('data-whatever', '" + mensaje + "'); document.getElementById('btnSuccessV').click(); </script> ", false);
        }

        private void MostrarModalVerificador()
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "MostrarModalVerificador", "<script> document.getElementById('btnModalVerificaSMS').click(); </script> ", false);
        }

        #endregion



        #region CODIGO VERIFICACIÓN CORREO

        protected void btnAbrirModalVerificarM_Click(object sender, EventArgs e)
        {
            try
            {
                //Abrir modal de verificación
                MostrarModalVerificadorM();
                ModalTit.InnerHtml = "Código de acceso";
                lblTit_Verificar1M.Text = "Instrucciones:";
                lblTit_Verificar2M.Text = "1. Copie el código de su correo";
                lblTit_Verificar3M.Text = "2. Agreguelo en la siguiente casilla";
                lblTit_Verificar4M.Text = "3. De clic en el botón verificar";

                TxT_CodigoVerificadorM.Text = string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
            finally { GC.Collect(); }
        }

        protected void btnVerificarM_Click(object sender, EventArgs e)
        {
            try
            {
                //Valida Session["codigo_verificador"] y TxT_CodigoVerificadorM.Text que sean iguales
                if (Session["codigo_verificadorM"] != null && Session["codigo_verificadorM"].ToString().Trim() == TxT_CodigoVerificadorM.Text.Trim())
                {
                    if (Session["t_u"] == null)
                    {
                        AlertError("Vuelva a cargar la página");
                        ConfigurationManager.AppSettings["BDActivo"] = null;
                        Session["idBase"] = null;
                        return;
                    }

                    Session["codigo_verificadorM"] = null;
                    string mensaje = string.Empty;

                    if (Session["dt_usuario"] == null)
                    {
                        AlertError("El código ingresado caducó");
                        return;
                    }

                    DataTable dtUsuarios = ((DataTable)Session["dt_usuario"]);

                    if (dtUsuarios.Rows.Count > 0)
                    {

                        ///ASIGNAMOS LAS VARIABLES DE SESION DEL SISTEMA
                        ///
                        Session["IDPerfil"] = dtUsuarios.Rows[0]["FK_ID_PERFIL_USER"].ToString().Trim();
                        Session["Usuario"] = Session["t_u"].ToString().Trim();
                        Session["Empresa"] = dtUsuarios.Rows[0]["EMPRESA"].ToString().Trim();
                        Session["IdUsuario"] = dtUsuarios.Rows[0]["PK_ID_USER"].ToString().Trim();
                        FormsAuthentication.RedirectFromLoginPage(Session["t_u"].ToString(), false);
                        HttpCookie cookieSACI = Request.Cookies["SACISession"];
                        if (cookieSACI == null)
                            //cookieSACI = new HttpCookie("SACISession");
                            cookieSACI = new HttpCookie("SACISession", Session["t_u"].ToString());
                        cookieSACI.Expires = DateTime.Today.AddMinutes(1);
                        Response.Cookies.Add(cookieSACI);
                        LimpiaControles();
                        Session["Menu"] = "0";
                        Session["Perfil"] = "admin";


                        //Valida si se quiere guardar información en bitacora
                        rnCatalogos = new RNCatalogos();
                        DataTable dt = new DataTable();
                        dt = rnCatalogos.InfoRegistro(1);

                        if (dt != null && dt.Rows[0]["BITACORA"].ToString().Equals("1"))
                        {
                            rnCatalogos = new RNCatalogos();
                            DataTable dtB = rnCatalogos.ValidarBitacora(1, 0, Int64.Parse(Session["IdUsuario"].ToString()), "", "", "");
                            Session.Add("IdBitacora", dtB.Rows[0]["IDKEY_BITACORA"].ToString().Trim());
                        }
                        else
                            Session["IdBitacora"] = string.Empty;

                    }
                    else
                    {
                        ConfigurationManager.AppSettings["BDActivo"] = null;
                        Session["idBase"] = null;
                        AlertError(MsgCatUsuarios.UsuarioInvalido);
                    }

                }
                else
                {
                    MostrarModalVerificador();
                    AlertError("El código ingresado caducó o no es correcto");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { GC.Collect(); }
        }

        protected void btRegresarM_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            MostrarView1();
        }

        public void AlertSuccessM(string mensaje, string mensaje2)
        {
            pModalSuccesM.InnerText = mensaje;
            pModalSuccesM2.InnerText = mensaje2;
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "", "<script> document.getElementById('btnSuccesM').setAttribute('data-whatever', '" + mensaje + "'); document.getElementById('btnSuccesM').click(); </script> ", false);
        }

        private void MostrarModalVerificadorM()
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(String), "MostrarModalVerificador", "<script> document.getElementById('btnModalVerificaCorreo').click(); </script> ", false);
        }

        #endregion




    }
}