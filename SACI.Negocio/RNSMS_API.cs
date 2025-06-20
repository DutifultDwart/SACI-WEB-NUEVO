using SACI.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Globalization;
using System.IO;

namespace SACI.Negocio
{
    public class RNSMS_API
    {

        #region RNSMS_Respuesta

        public static RNSMS_Respuesta EnvioSMS(RNSMS mensaje, RNSMS_Token token)
        {
            RNSMS_Respuesta respuesta = new RNSMS_Respuesta();
            //respuesta.Mensaje = new List<String>();
            
            try
            {
                //String urlWs = "https://avc.egi.systems:3005/";

                System.Net.ServicePointManager.Expect100Continue = true;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls
                    | System.Net.SecurityProtocolType.Tls11
                    | System.Net.SecurityProtocolType.Tls12
                    | System.Net.SecurityProtocolType.Ssl3;
                System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender1, certificate, chain, sslPolicyErrors) => true);

                var url = "https://api.broadcastermobile.com/brdcstr-endpoint-web/services/messaging/";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                string json = JsonConvert.SerializeObject(mensaje);
                json = json.ToString().Replace("\"[", "[").Replace("]\"", "]");
                
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, token.Authorization);
                //httpWebRequest.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                try
                {
                    using (WebResponse response = httpWebRequest.GetResponse())
                    {
                        using (Stream strReader = response.GetResponseStream())
                        {
                            if (strReader == null)
                                throw new Exception("Error: response.GetResponseStream null");

                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                string responseBody = objReader.ReadToEnd();
                                respuesta = JsonConvert.DeserializeObject<RNSMS_Respuesta>(responseBody);
                                respuesta.TieneError = false;
                                respuesta.Response = responseBody.ToString();
                                //respuesta.Mensaje = ObtenerError(responseBody);
                            }
                        }
                    }
                }
                catch (WebException ex)
                {
                    using (var stream = ex.Response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        String respuestaJson = reader.ReadToEnd();
                        //respuesta.Mensaje = ObtenerError(respuestaJson);
                        respuesta = JsonConvert.DeserializeObject<RNSMS_Respuesta>(respuestaJson);
                        respuesta.TieneError = true;
                    }
                }
                catch (Exception ex)
                {
                    respuesta.message=ex.Message;
                    respuesta.TieneError = true;
                }
            }
            catch (Exception exp)
            {
                respuesta.message=exp.Message;
                respuesta.TieneError = true;
            }
            
            return respuesta;           
        }

        #endregion


    }
}
