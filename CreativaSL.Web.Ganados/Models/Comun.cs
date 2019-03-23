using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Script.Serialization;

namespace CreativaSL.Web.Ganados.Models
{
    public static class Comun
    {
        public static Image ResizeImage(Image srcImage, int newWidth, int newHeight)
        {
            using (Bitmap imagenBitmap =
               new Bitmap(newWidth, newHeight, PixelFormat.Format32bppRgb))
            {
                imagenBitmap.SetResolution(
                   Convert.ToInt32(srcImage.HorizontalResolution),
                   Convert.ToInt32(srcImage.HorizontalResolution));

                using (Graphics imagenGraphics =
                        Graphics.FromImage(imagenBitmap))
                {
                    imagenGraphics.SmoothingMode =
                       SmoothingMode.AntiAlias;
                    imagenGraphics.InterpolationMode =
                       InterpolationMode.HighQualityBicubic;
                    imagenGraphics.PixelOffsetMode =
                       PixelOffsetMode.HighQuality;
                    imagenGraphics.DrawImage(srcImage,
                       new Rectangle(0, 0, newWidth, newHeight),
                       new Rectangle(0, 0, srcImage.Width, srcImage.Height),
                       GraphicsUnit.Pixel);
                    MemoryStream imagenMemoryStream = new MemoryStream();
                    imagenBitmap.Save(imagenMemoryStream, ImageFormat.Jpeg);
                    srcImage = Image.FromStream(imagenMemoryStream);
                }
            }
            return srcImage;
        }

        public static string ToBase64String(this Bitmap bmp, ImageFormat imageFormat)
        {
            string base64String = string.Empty;

            MemoryStream memoryStream = new MemoryStream();
            bmp.Save(memoryStream, imageFormat);

            memoryStream.Position = 0;
            byte[] byteBuffer = memoryStream.ToArray();

            memoryStream.Close();

            base64String = Convert.ToBase64String(byteBuffer);
            byteBuffer = null;

            return base64String;
        }
        public static string ToBase64ImageTag(this Bitmap bmp, ImageFormat imageFormat)
        {
            string imgTag = string.Empty;
            string base64String = string.Empty;

            base64String = bmp.ToBase64String(imageFormat);

            imgTag = "<img src=\"data:image/" + imageFormat.ToString() + ";base64,";
            imgTag += base64String + "\" ";
            imgTag += "width=\"" + bmp.Width.ToString() + "\" ";
            imgTag += "height=\"" + bmp.Height.ToString() + "\" />";

            return imgTag;
        }
        public static string ToBase64ImageReport(this Bitmap bmp, ImageFormat imageFormat)
        {
            string imgTag = string.Empty;
            string base64String = string.Empty;

            base64String = bmp.ToBase64String(imageFormat);

            imgTag = "<img class='img-report' src=\"data:image/" + imageFormat.ToString() + ";base64,";
            imgTag += base64String + "\"/> ";
            //imgTag += "width=\"" + bmp.Width.ToString() + "\" ";
            //imgTag += "height=\"" + bmp.Height.ToString() + "\" />";

            return imgTag;
        }
        public static Bitmap Base64StringToBitmap(this string base64String)
        {
            Bitmap bmpReturn = null;
            
            byte[] byteBuffer = Convert.FromBase64String(base64String);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);

            memoryStream.Position = 0;

            bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);

            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;

            return bmpReturn;
        }

        public static string RemoverAcentos(string texto)
        {
            const string ConSignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜçÇ";
            const string SinSignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUcC";
            string textoSinAcentos = string.Empty;

            foreach (var caracter in texto)
            {
                var indexConAcento = ConSignos.IndexOf(caracter);
                if (indexConAcento > -1)
                    textoSinAcentos = textoSinAcentos + (SinSignos.Substring(indexConAcento, 1));
                else
                    textoSinAcentos = textoSinAcentos + (caracter);
            }
            textoSinAcentos = textoSinAcentos.ToLower();
            return textoSinAcentos;
        }

        public static bool EnviarCorreo(string De, string Contraseña, string Para, string Asunto, string Mensaje, bool banArchivo, string Archivo, bool Html, string Host, int Port, bool Ssl)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(De);
                mailMessage.To.Add(Para);
                mailMessage.Subject = Asunto;
                if (banArchivo)
                    mailMessage.Attachments.Add(new Attachment(Archivo));
                mailMessage.Body = Mensaje;
                mailMessage.IsBodyHtml = Html;
                SmtpClient smtpClient = new SmtpClient(Host);
                smtpClient.Port = Port;
                smtpClient.EnableSsl = Ssl;
                smtpClient.Credentials = new NetworkCredential(De, Contraseña);
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public static string GenerarHtmlResetContraseña(string Usuario, string Password)
        {
            int año = DateTime.Now.Year;
            string html = @"
            <!DOCTYPE html>
            <html lang=""en"">

            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <meta http-equiv=""X-UA-Compatible"" content=""ie=edge"">
                <title>Correo Ocampo</title>
            </head>
            <!-- width: 0; 
                                height: 0; 
                                border-top: 10px solid transparent;
                                border-bottom: 10px solid transparent;
                                border-left: 10px solid #333; -->

            <body style=""
            margin: 0; 
            padding: 0;
            background-color: #e8e8e8;
            box-sizing: border-box;
            "">
                <header style=""
                width: 100%; 
                height: 180px; 
                background-color: #333;
                "">
                    <div class=""logo"" style=""
                    background-image: url('http://www.grupoocampo.com.mx/Content/img/logo.png');
                    width: 160px;
                    height: 160px;
                    display: block;
                    margin: auto;
                    border-radius: 50%;
                    background-color: #fff;
                    background-repeat: no-repeat;
                    background-position: center;
                    background-size: contain;
                    position: relative;
                    top: 10px;
                    ""></div>
                </header>
                <main style=""margin-top: 0px"">
                    <div class=""titulo"" style=""
                    background-color: #f00;
                    padding: 0% 0%;
                    "">
                        <h2 style=""
                        color: #fff;
                        text-align: center;
                        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                        height: auto;
                        font-size: 2.5em;
                        padding: 1% 0%;
                        margin: auto;
                        "">SOLICITUD DE NUEVA CONTRASEÑA</h2>
                    </div>
                    <div class=""contenido"" style=""
                    padding: 2%;
                    "">
                        <div class=""description"" style=""
                        width: 70%;
                        display: block;
                        margin: auto;
                        "">
                            <p style=""
                        text-align: center;
                        font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
                        "">Hemos recibido su solicitud de cambio de contraseña. Usted puede entrar a la plataforma con los datos que se muestran a continuación. Cualquier inconveniente favor de contactar con la oficina.</p>
                            <p style=""
                        text-align: center;
                        font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
                        font-weight: bold;
                        "">Atentamente: Grupo Ocampo</p>
                            <div class=""table""></div>
                        </div>

                        <div class=""tablaCorreo"" style=""
                        padding: 0% 5%;

                        "">
                            <table style=""width: 100%;"">
                                <tbody>
                                    <tr style=""
                                    border-bottom: 1px solid #c8c8c8;
                                    font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                    height: 50px;
                                    text-align: center;
                                    background-color: #d8d8d8;
                                    "">
                                        <td>Usuario</td>
                                        <td>" + Usuario + @"</td>
                                    </tr>
                                    <tr style=""
                                    border-bottom: 1px solid #c8c8c8;
                                    font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                    height: 50px;
                                    text-align: center;
                                    background-color: #d8d8d8;
                                    "">
                                        <td>Contraseña</td>
                                        <td>" + Password + @"</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </main>
                <footer>
                    <p style=""
                    text-align: center;
                    color: #333;
                    font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
                    "">© " + año + @" - Impulsado por Creativa Softline - Todos los derechos reservados</p>
                </footer>
            </body>

            </html>
            ";
            return html;
        }

        public static string GenerarHtmlCorreoJaula(DateTime Fecha, string Proveedor, string NombreChofer, string ChoferAux, int CabezaHembras, int PesosHembras, int CabezaMachos, int PesosMachos,
                                              int PesoGeneral, string LugarDestino, string TelefonoMovil, string Modelo, string Color, string Placa, string GPS, string PlajasJaula, string ColorJaula, string Marca)
        {
            int año = DateTime.Now.Year;
            string html = @"
                <!DOCTYPE html>
                <html lang=""en"">

                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <meta http-equiv=""X-UA-Compatible"" content=""ie=edge"">
                    <title>Correo Ocampo</title>
                </head>
                <!-- width: 0; 
                                    height: 0; 
                                    border-top: 10px solid transparent;
                                    border-bottom: 10px solid transparent;
                                    border-left: 10px solid #333; -->

                <body style=""
                margin: 0; 
                padding: 0;
                background-color: #e8e8e8;
                box-sizing: border-box;
                "">
                    <header style=""
                    width: 100%; 
                    height: 180px; 
                    background-color: #333;
                    "">
                        <div class=""logo"" style=""
                        background-image: url('http://www.grupoocampo.com.mx/Content/img/logo.png');
                        width: 160px;
                        height: 160px;
                        display: block;
                        margin: auto;
                        border-radius: 50%;
                        background-color: #fff;
                        background-repeat: no-repeat;
                        background-position: center;
                        background-size: contain;
                        position: relative;
                        top: 10px;
                        ""></div>
                    </header>
                    <main style=""margin-top: 0px"">
                        <div class=""titulo"" style=""
                        background-color: #f00;
                        padding: 0% 0%;
                        "">
                            <h2 style=""
                            color: #fff;
                            text-align: center;
                            font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                            height: auto;
                            font-size: 2.5em;
                            padding: 1% 0%;
                            margin: auto;
                            "">REPORTE DE JAULA</h2>
                        </div>
                        <div class=""contenido"" style=""
                        padding: 2%;
                        "">
                            <div class=""description"" style=""
                            width: 70%;
                            display: block;
                            margin: auto;
                            "">
                                <p style=""
                                text-align: center;
                                font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
                                "">Buen dia, envío reporte de peso. Estamos a la orden GRUPO OCAMPO.</p>
                                <div class=""table""></div>
                            </div>

                            <div class=""tablaCorreo"" style=""
                            padding: 0% 5%;

                            "">
                                <table style=""width: 100%;"">
                                    <thead style=""
                                    background-color: #333;
                                    color: #fff;
                                    text-align: center;
                                    font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                    height: 50px;
                                    "">
                                        <th style=""height: 50px;"">CAMPO</th>
                                        <th style=""height: 50px;"">DESCRIPCIÓN</th>
                                    </thead>
                                    <tbody>
                                        <tr style=""
                                        border-bottom: 1px solid #c8c8c8;
                                        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                        height: 50px;
                                        text-align: center;
                                        background-color: #d8d8d8;
                                        "">
                                            <td>Fecha de Embarque</td>
                                            <td>" + Fecha.ToLongDateString() + @"</td>
                                        </tr>
                                        <tr style=""
                                        border-bottom: 1px solid #c8c8c8;
                                        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                        height: 50px;
                                        text-align: center;
                                        background-color: #d8d8d8;
                                        "">
                                            <td>Proveedor</td>
                                            <td>" + Proveedor + @"</td>
                                        </tr>
                                        <tr style=""
                                        border-bottom: 1px solid #c8c8c8;
                                        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                        height: 50px;
                                        text-align: center;
                                        background-color: #d8d8d8;
                                        "">
                                            <td>Nombre Chofer</td>
                                            <td>" + NombreChofer + @"</td>
                                        </tr>
                                        <tr style=""
                                        border-bottom: 1px solid #c8c8c8;
                                        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                        height: 50px;
                                        text-align: center;
                                        background-color: #d8d8d8;
                                        "">
                                            <td>Nombre Chofer Auxiliar</td>
                                            <td>" + ChoferAux + @"</td>
                                        </tr>
                                        <tr style=""
                                        border-bottom: 1px solid #c8c8c8;
                                        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                        height: 50px;
                                        text-align: center;
                                        background-color: #d8d8d8;
                                        "">
                                            <td>Cabezas Hembras</td>
                                            <td>" + CabezaHembras + @"</td>
                                        </tr>
                                        <tr style=""
                                        border-bottom: 1px solid #c8c8c8;
                                        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                        height: 50px;
                                        text-align: center;
                                        background-color: #d8d8d8;
                                        "">
                                            <td>Pesos Hembras (KG)</td>
                                            <td>" + PesosHembras + @" (KG)</td>
                                        </tr>
                                        <tr style=""
                                        border-bottom: 1px solid #c8c8c8;
                                        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                        height: 50px;
                                        text-align: center;
                                        background-color: #d8d8d8;
                                        "">
                                            <td>Cabeza Machos</td>
                                            <td>" + CabezaMachos + @"</td>
                                        </tr>
                                        <tr style=""
                                        border-bottom: 1px solid #c8c8c8;
                                        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                        height: 50px;
                                        text-align: center;
                                        background-color: #d8d8d8;
                                        "">
                                            <td>Pesos Machos (KG)</td>
                                            <td>" + PesosMachos + @" (KG)</td>
                                        </tr>
                                        <tr style=""
                                        border-bottom: 1px solid #c8c8c8;
                                        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                        height: 50px;
                                        text-align: center;
                                        background-color: #d8d8d8;
                                        "">
                                            <td>Peso Total (KG)</td>
                                            <td>" + PesoGeneral + @" (KG)</td>
                                        </tr>
                                        <tr style=""
                                        border-bottom: 1px solid #c8c8c8;
                                        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                        height: 50px;
                                        text-align: center;
                                        background-color: #d8d8d8;
                                        "">
                                            <td>Lugar Destino</td>
                                            <td>" + LugarDestino + @"</td>
                                        </tr>
                                        <tr style=""
                                        border-bottom: 1px solid #c8c8c8;
                                        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                        height: 50px;
                                        text-align: center;
                                        background-color: #d8d8d8;
                                        "">
                                            <td>Telefono Movil</td>
                                            <td>" + TelefonoMovil + @"</td>
                                        </tr>
                                        <tr style=""
                                        border-bottom: 1px solid #c8c8c8;
                                        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                        height: 50px;
                                        text-align: center;
                                        background-color: #d8d8d8;
                                        "">
                                            <td>Modelo</td>
                                            <td>" + Modelo + @"</td>
                                        </tr>
                                        <tr style=""
                                        border-bottom: 1px solid #c8c8c8;
                                        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                        height: 50px;
                                        text-align: center;
                                        background-color: #d8d8d8;
                                        "">
                                            <td>Color</td>
                                            <td>" + Color + @"</td>
                                        </tr>
                                        <tr style=""
                                        border-bottom: 1px solid #c8c8c8;
                                        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                        height: 50px;
                                        text-align: center;
                                        background-color: #d8d8d8;
                                        "">
                                            <td>Placa</td>
                                            <td>" + Placa + @"</td>
                                        </tr>
                                        <tr style=""
                                        border-bottom: 1px solid #c8c8c8;
                                        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                        height: 50px;
                                        text-align: center;
                                        background-color: #d8d8d8;
                                        "">
                                            <td>GPS</td>
                                            <td>" + GPS + @"</td>
                                        </tr>
                                        <tr style=""
                                        border-bottom: 1px solid #c8c8c8;
                                        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                        height: 50px;
                                        text-align: center;
                                        background-color: #d8d8d8;
                                        "">
                                            <td>Placas Jaula</td>
                                            <td>" + PlajasJaula + @"</td>
                                        </tr>
                                        <tr style=""
                                        border-bottom: 1px solid #c8c8c8;
                                        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                        height: 50px;
                                        text-align: center;
                                        background-color: #d8d8d8;
                                        "">
                                            <td>Color Jaula</td>
                                            <td>" + ColorJaula + @"</td>
                                        </tr>
                                        <tr style=""
                                        border-bottom: 1px solid #c8c8c8;
                                        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                                        height: 50px;
                                        text-align: center;
                                        background-color: #d8d8d8;
                                        "">
                                            <td>Marca</td>
                                            <td>" + Marca + @"</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </main>
                    <footer>
                        <p style=""
                        text-align: center;
                        color: #333;
                        font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
                        "">© " + año + @" - Impulsado por Creativa Softline - Todos los derechos reservados</p>
                    </footer>
                </body>

                </html>
            ";
            return html;
        }

        private static string ConcatParams(Dictionary<string, string> parameters)
        {
            bool FirstParam = true;
            StringBuilder Parametros = null;

            if (parameters != null)
            {
                Parametros = new StringBuilder();
                foreach (KeyValuePair<string, string> param in parameters)
                {
                    Parametros.Append(FirstParam ? "" : " & ");
                    Parametros.Append(param.Key + "=" + System.Web.HttpUtility.UrlEncode(param.Value));
                    FirstParam = false;
                }
            }

            return Parametros == null ? string.Empty : Parametros.ToString();
        }

        public static string GetResponse_POST(string url, Dictionary<string, string> parameters)
        {
            try
            {
                //Concatenamos los parametros, OJO: NO se añade el caracter "?"
                string parametrosConcatenados = ConcatParams(parameters);

                System.Net.WebRequest wr = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                wr.Method = "POST";

                wr.ContentType = "application/x-www-form-urlencoded";

                System.IO.Stream newStream;
                //Codificación del mensaje
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                byte[] byte1 = encoding.GetBytes(parametrosConcatenados);
                wr.ContentLength = byte1.Length;
                //Envio de parametros
                newStream = wr.GetRequestStream();
                newStream.Write(byte1, 0, byte1.Length);

                // Obtiene la respuesta
                System.Net.WebResponse response = wr.GetResponse();
                // Stream con el contenido recibido del servidor
                newStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(newStream);
                // Leemos el contenido
                string responseFromServer = reader.ReadToEnd();

                // Cerramos los streams
                reader.Close();
                newStream.Close();
                response.Close();
                return responseFromServer;
            }
            catch (System.Web.HttpException ex)
            {
                if (ex.ErrorCode == 404)
                    throw new Exception("Not found remote service " + url);
                else throw ex;
            }
        }

        public static string EnviarMensaje(string deviceId, string title, string message, int Badge, int IDTipoCelular)
        {
            try
            {
                var applicationID = "AAAABNNJpxA:APA91bH7fHVuyvfJ9Vg8lOk8KODgqM-W_Vfg-ZY5zUQYvM7F9TvpXjieyfalj4uL6vE3VNJfgRimGba26P6ZsxxdbIfUJdR1DbVR8HoiPJ6FZnR1RZAfr9HPmzDp5zLB226nn-RKcxz4";
                //var applicationID = "AAAABNNJpxA:APA91bH7fHVuyvfJ9Vg8lOk8KODgqM-W_Vfg-ZY5zUQYvM7F9TvpXjieyfalj4uL6vE3VNJfgRimGba26P6ZsxxdbIfUJdR1DbVR8HoiPJ6FZnR1RZAfr9HPmzDp5zLB226nn-RKcxz4";
                
                //string deviceId = "djHUgfqmi6k:APA91bHN4snCuKXkUcTj2eMfJ0BFVtMMj5LlkG3JFL9esuClBaaU9_iuzqdyE65sqMJC0GKVrf1FGRWc0wur06kDWTn_vBVpJ9IGYFQDbitBYh67aSlt4rkhNO8WpDp-WdP402OqLisY";
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
              
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = message.ToUpper(),
                        title = title.ToUpper(),
                        sound = "default",
                        badge = Badge
                    }
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                string aux = string.Format("Authorization: key={0}", applicationID);
                tRequest.Headers.Add(string.Format("Authorization:key={0}", applicationID));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                return sResponseFromServer;
                                //Response.Write(sResponseFromServer);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}