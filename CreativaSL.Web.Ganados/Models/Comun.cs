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
            Graphics G = Graphics.FromImage(bmpReturn);
            //G.Clear(Color.FromArgb(105, 105, 105));
            //G.DrawImage(bmpReturn, bmpReturn.Width, 0, bmpReturn.Width, bmpReturn.Height);

            ColorMap[] colorMap = new ColorMap[1];
            colorMap[0] = new ColorMap();
            colorMap[0].OldColor = Color.Black;
            colorMap[0].NewColor = Color.Blue;
            ImageAttributes attr = new ImageAttributes();
            attr.SetRemapTable(colorMap);
            // Draw using the color map
            Rectangle rect = new Rectangle(0, 0, bmpReturn.Width, bmpReturn.Height);
            G.DrawImage(bmpReturn, bmpReturn.Width, 0, bmpReturn.Width, bmpReturn.Height);
            //G.DrawImage(bmpReturn, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, attr);


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
        
        public static string GenerarHtmlRegistoTutorAlumno(string id_cuenta, string usuario, string password)
        {
            string dominio = "http://www.creativasoftlineapps.com/ofelianarvaez";
            string ValidarCuenta = "/Home/ValidarCuenta/";
            // string login = "/Admin/Account/";
            string creativa = "creativasoftline.com";
            int año = DateTime.Now.Year;
            string html = @"
            <!DOCTYPE html>
            <html>
            <head>
                <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1"">
                <meta name=""format-detection"" content=""telephone=no"" />
                <title>Colegio Ofelia Narváez Rincón</title>
                <style type=""text/css"">
                    html { background-color:#E1E1E1; margin:0; padding:0; }
                    body, #bodyTable, #bodyCell, #bodyCell{height:100% !important; margin:0; padding:0; width:100% !important;font-family:Helvetica, Arial, ""Lucida Grande"", sans-serif;}
                    table{border-collapse:collapse;}
                    table[id=bodyTable] {width:100%!important;margin:auto;max-width:500px!important;color:#b6b24c;font-weight:normal;}
                    img, a img{border:0; outline:none; text-decoration:none;height:auto; line-height:100%;}
                    a {text-decoration:none !important;border-bottom: 1px solid;}
                    h1, h2, h3, h4, h5, h6{color:#5F5F5F; font-weight:normal; font-family:Helvetica; font-size:20px; line-height:125%; text-align:Left; letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:10px;margin-left:0;padding-top:0;padding-bottom:0;padding-left:0;padding-right:0;}
                    .ReadMsgBody{width:100%;} .ExternalClass{width:100%;} /* Force Hotmail/Outlook.com to display emails at full width. */
                    .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div{line-height:100%;} /* Force Hotmail/Outlook.com to display line heights normally. */
                    table, td{mso-table-lspace:0pt; mso-table-rspace:0pt;} /* Remove spacing between tables in Outlook 2007 and up. */
                    #outlook a{padding:0;} /* Force Outlook 2007 and up to provide a ""view in browser"" message. */
                    img{-ms-interpolation-mode: bicubic;display:block;outline:none; text-decoration:none;} /* Force IE to smoothly render resized images. */
                    body, table, td, p, a, li, blockquote{-ms-text-size-adjust:100%; -webkit-text-size-adjust:100%; font-weight:normal!important;} /* Prevent Windows- and Webkit-based mobile platforms from changing declared text sizes. */
                    .ExternalClass td[class=""ecxflexibleContainerBox""] h3 {padding-top: 10px !important;} /* Force hotmail to push 2-grid sub headers down */

                    h1{display:block;font-size:26px;font-style:normal;font-weight:normal;line-height:100%;}
                    h2{display:block;font-size:20px;font-style:normal;font-weight:normal;line-height:120%;}
                    h3{display:block;font-size:17px;font-style:normal;font-weight:normal;line-height:110%;}
                    h4{display:block;font-size:18px;font-style:italic;font-weight:normal;line-height:100%;}
                    .flexibleImage{height:auto;}
                    .linkRemoveBorder{border-bottom:0 !important;}
                    table[class=flexibleContainerCellDivider] {padding-bottom:0 !important;padding-top:0 !important;}

                    body, #bodyTable{background-color:#E1E1E1;}
                    #emailHeader{background-color:#b6b24c;}
                    #emailBody{background-color:#FFFFFF;}
                    #emailFooter{background-color:#E1E1E1;}
                    .nestedContainer{background-color:#F8F8F8; border:1px solid #CCCCCC;}
                    .emailButton{background-color:#205478; border-collapse:separate;}
                    .buttonContent{color:#FFFFFF; font-family:Helvetica; font-size:18px; font-weight:bold; line-height:100%; padding:15px; text-align:center;}
                    .buttonContent a{color:#FFFFFF; display:block; text-decoration:none!important; border:0!important;}
                    .emailCalendar{background-color:#FFFFFF; border:1px solid #CCCCCC;}
                    .emailCalendarMonth{background-color:#205478; color:#FFFFFF; font-family:Helvetica, Arial, sans-serif; font-size:16px; font-weight:bold; padding-top:10px; padding-bottom:10px; text-align:center;}
                    .emailCalendarDay{color:#205478; font-family:Helvetica, Arial, sans-serif; font-size:60px; font-weight:bold; line-height:100%; padding-top:20px; padding-bottom:20px; text-align:center;}
                    .imageContentText {margin-top: 10px;line-height:0;}
                    .imageContentText a {line-height:0;}
                    #invisibleIntroduction {display:none !important;} /* Removing the introduction text from the view */
                    span[class=ios-color-hack] a {color:#275100!important;text-decoration:none!important;}
                    span[class=ios-color-hack2] a {color:#205478!important;text-decoration:none!important;}
                    span[class=ios-color-hack3] a {color:#8B8B8B!important;text-decoration:none!important;}
                    .a[href^=""tel""], a[href^=""sms""] {text-decoration:none!important;color:#606060!important;pointer-events:none!important;cursor:default!important;}
                    .mobile_link a[href^=""tel""], .mobile_link a[href^=""sms""] {text-decoration:none!important;color:#606060!important;pointer-events:auto!important;cursor:default!important;}

                    @media only screen and (max-width: 480px){
                        body{width:100% !important; min-width:100% !important;}
                        /*td[class=""textContent""], td[class=""flexibleContainerCell""] { width: 100%; padding-left: 10px !important; padding-right: 10px !important; }*/
                        table[id=""emailHeader""],
                        table[id=""emailBody""],
                        table[id=""emailFooter""],
                        table[class=""flexibleContainer""],
                        td[class=""flexibleContainerCell""] {width:100% !important;}
                        td[class=""flexibleContainerBox""], td[class=""flexibleContainerBox""] table {display: block;width: 100%;text-align: left;}
                        td[class=""imageContent""] img {height:auto !important; width:100% !important; max-width:100% !important; }
                        img[class=""flexibleImage""]{height:auto !important; width:100% !important;max-width:100% !important;}
                        img[class=""flexibleImageSmall""]{height:auto !important; width:auto !important;}
                        table[class=""emailButton""]{width:100% !important;}
                        td[class=""buttonContent""]{padding:0 !important;}
                        td[class=""buttonContent""] a{padding:15px !important;}
                    }
                    @media only screen and (-webkit-device-pixel-ratio:.75){
                    }
                    @media only screen and (-webkit-device-pixel-ratio:1){
                    }
                    @media only screen and (-webkit-device-pixel-ratio:1.5){
                    }
                    @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
                    }
                </style>
                <!--[if mso 12]><style type=""text/css"">.flexibleContainer{display:block !important; width:100% !important;}</style><![endif]-->
                <!--[if mso 14]><style type=""text/css"">.flexibleContainer{display:block !important; width:100% !important;}</style><![endif]-->
            </head>
            <body bgcolor=""#E1E1E1"" leftmargin=""0"" marginwidth=""0"" topmargin=""0"" marginheight=""0"" offset=""0"">
                <center style=""background-color:#E1E1E1;"">
                    <table border=""0"" cellpadding=""0"" cellspacing=""0"" height=""100%"" width=""100%"" id=""bodyTable"" style=""table-layout: fixed;max-width:100% !important;width: 100% !important;min-width: 100% !important;"">
                        <tr>
                            <td align=""center"" valign=""top"" id=""bodyCell"">
                                <table bgcolor=""#FFFFFF""  border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" id=""emailBody"">
                                    <tr>
                                        <td align=""center"" valign=""top"">
                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""color:#FFFFFF;"" bgcolor=""#3498db"">
                                                <tr>
                                                    <td align=""center"" valign=""top"">
                                                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
                                                            <tr>
                                                                <td align=""center"" valign=""top"" width=""500"" class=""flexibleContainerCell"">
                                                                    <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""100%"">
                                                                        <tr>
                                                                            <td align=""center"" valign=""top"" class=""textContent"">
                                                                                <h1 style=""color:#FFFFFF;line-height:100%;font-family:Helvetica,Arial,sans-serif;font-size:35px;font-weight:normal;margin-bottom:5px;text-align:center;"">Colegio Ofelia Narváez Rincón</h1>
                                                                                <h2 style=""text-align:center;font-weight:normal;font-family:Helvetica,Arial,sans-serif;font-size:23px;margin-bottom:10px;color:#205478;line-height:135%;"">Colegio Ofelia Narváez Rincón, A.C.</h2>
                                                                                <div style=""text-align:center;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#FFFFFF;line-height:135%;"">Registro</div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr mc:hideable>
                                        <td align=""center"" valign=""top"">
                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                                                <tr>
                                                    <td align=""center"" valign=""top"">
                                                        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
                                                            <tr>
                                                                <td valign=""top"" width=""500"" class=""flexibleContainerCell"">
                                                                    <table align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                                                                        <tr>
                                                                            <td align=""left"" valign=""top"" class=""flexibleContainerBox"">
                                                                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""210"" style=""max-width: 100%;"">
                                                                                    <tr>
                                                                                        <td align=""left"" class=""textContent"">
                                                                                            <h3 style=""color:#5F5F5F;line-height:125%;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;margin-top:0;margin-bottom:3px;text-align:left;"">Usuario:</h3>
                                                                                            <div style=""text-align:left;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#5F5F5F;line-height:135%;"">" + usuario + @"</div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td align=""right"" valign=""middle"" class=""flexibleContainerBox"">
                                                                                <table class=""flexibleContainerBoxNext"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""210"" style=""max-width: 100%;"">
                                                                                    <tr>
                                                                                        <td align=""left"" class=""textContent"">
                                                                                            <h3 style=""color:#5F5F5F;line-height:125%;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;margin-top:0;margin-bottom:3px;text-align:left;"">Contraseña:</h3>
                                                                                            <div style=""text-align:left;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#5F5F5F;line-height:135%;"">" + password + @"</div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <br><br>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align=""center"" valign=""top"">
                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                                                <tr style=""padding-top:0;"">
                                                    <td align=""center"" valign=""top"">
                                                        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
                                                            <tr>
                                                                <td style=""padding-top:0;"" align=""center"" valign=""top"" width=""500"" class=""flexibleContainerCell"">
                                                                    <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""50%"" class=""emailButton"" style=""background-color: #b6b24c;"">
                                                                        <tr>
                                                                            <td align=""center"" valign=""middle"" class=""buttonContent"" style=""padding-top:15px;padding-bottom:15px;padding-right:15px;padding-left:15px;"">
                                                                                <a style=""color:#FFFFFF;text-decoration:none;font-family:Helvetica,Arial,sans-serif;font-size:20px;line-height:135%;"" href=""" + dominio + ValidarCuenta + "?id=" + id_cuenta + @""" target=""_blank"">Ingresar</a>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table bgcolor=""#E1E1E1"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" id=""emailFooter"">
                                    <tr>
                                        <td align=""center"" valign=""top"">
                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                                                <tr>
                                                    <td align=""center"" valign=""top"">
                                                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
                                                            <tr>
                                                                <td align=""center"" valign=""top"" width=""500"" class=""flexibleContainerCell"">
                                                                    <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""100%"">
                                                                        <tr>
                                                                            <td valign=""top"" bgcolor=""#E1E1E1"">
                                                                                <div style=""font-family:Helvetica,Arial,sans-serif;font-size:13px;color:#828282;text-align:center;line-height:120%;"">
                                                                                    <div>Copyright &#169; """+ año + @""" <a href=""" + dominio + @""" target=""_blank"" style=""text-decoration:none;color:#828282;""><span style=""color:#828282;"">CSL</span></a>. All&nbsp;rights&nbsp;reserved.</div>
                                                                                    <div>Creativa Softline <a href=""" + creativa + @""" target=""""_blank"""" style=""text-decoration:none;color:#828282;""><span style=""color:#828282;"">comunicarse</span></a>.</div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </center>
            </body>
        </html>
            ";
            return html;
        }

        public static string GenerarHtmlRegistoUsuario(string usuario, string password)
        {
            string dominio = "http://www.creativasoftlineapps.com/ofelianarvaez";
            string login = "/Admin/Account/";
            string creativa = "creativasoftline.com";
            int año = DateTime.Now.Year;
            string html = @"
            <!DOCTYPE html>
	        <html>
	        <head>
		        <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
		        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
		        <meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1"">
		        <meta name=""format-detection"" content=""telephone=no"" />
		        <title>Colegio Ofelia Narváez Rincón</title>
		        <style type=""text/css"">
			        html { background-color:#E1E1E1; margin:0; padding:0; }
			        body, #bodyTable, #bodyCell, #bodyCell{height:100% !important; margin:0; padding:0; width:100% !important;font-family:Helvetica, Arial, ""Lucida Grande"", sans-serif;}
			        table{border-collapse:collapse;}
			        table[id=bodyTable] {width:100%!important;margin:auto;max-width:500px!important;color:#b6b24c;font-weight:normal;}
			        img, a img{border:0; outline:none; text-decoration:none;height:auto; line-height:100%;}
			        a {text-decoration:none !important;border-bottom: 1px solid;}
			        h1, h2, h3, h4, h5, h6{color:#5F5F5F; font-weight:normal; font-family:Helvetica; font-size:20px; line-height:125%; text-align:Left; letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:10px;margin-left:0;padding-top:0;padding-bottom:0;padding-left:0;padding-right:0;}
			        .ReadMsgBody{width:100%;} .ExternalClass{width:100%;} /* Force Hotmail/Outlook.com to display emails at full width. */
			        .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div{line-height:100%;} /* Force Hotmail/Outlook.com to display line heights normally. */
			        table, td{mso-table-lspace:0pt; mso-table-rspace:0pt;} /* Remove spacing between tables in Outlook 2007 and up. */
			        #outlook a{padding:0;} /* Force Outlook 2007 and up to provide a ""view in browser"" message. */
			        img{-ms-interpolation-mode: bicubic;display:block;outline:none; text-decoration:none;} /* Force IE to smoothly render resized images. */
			        body, table, td, p, a, li, blockquote{-ms-text-size-adjust:100%; -webkit-text-size-adjust:100%; font-weight:normal!important;} /* Prevent Windows- and Webkit-based mobile platforms from changing declared text sizes. */
			        .ExternalClass td[class=""ecxflexibleContainerBox""] h3 {padding-top: 10px !important;} /* Force hotmail to push 2-grid sub headers down */

			        h1{display:block;font-size:26px;font-style:normal;font-weight:normal;line-height:100%;}
			        h2{display:block;font-size:20px;font-style:normal;font-weight:normal;line-height:120%;}
			        h3{display:block;font-size:17px;font-style:normal;font-weight:normal;line-height:110%;}
			        h4{display:block;font-size:18px;font-style:italic;font-weight:normal;line-height:100%;}
			        .flexibleImage{height:auto;}
			        .linkRemoveBorder{border-bottom:0 !important;}
			        table[class=flexibleContainerCellDivider] {padding-bottom:0 !important;padding-top:0 !important;}

			        body, #bodyTable{background-color:#E1E1E1;}
			        #emailHeader{background-color:#b6b24c;}
			        #emailBody{background-color:#FFFFFF;}
			        #emailFooter{background-color:#E1E1E1;}
			        .nestedContainer{background-color:#F8F8F8; border:1px solid #CCCCCC;}
			        .emailButton{background-color:#205478; border-collapse:separate;}
			        .buttonContent{color:#FFFFFF; font-family:Helvetica; font-size:18px; font-weight:bold; line-height:100%; padding:15px; text-align:center;}
			        .buttonContent a{color:#FFFFFF; display:block; text-decoration:none!important; border:0!important;}
			        .emailCalendar{background-color:#FFFFFF; border:1px solid #CCCCCC;}
			        .emailCalendarMonth{background-color:#205478; color:#FFFFFF; font-family:Helvetica, Arial, sans-serif; font-size:16px; font-weight:bold; padding-top:10px; padding-bottom:10px; text-align:center;}
			        .emailCalendarDay{color:#205478; font-family:Helvetica, Arial, sans-serif; font-size:60px; font-weight:bold; line-height:100%; padding-top:20px; padding-bottom:20px; text-align:center;}
			        .imageContentText {margin-top: 10px;line-height:0;}
			        .imageContentText a {line-height:0;}
			        #invisibleIntroduction {display:none !important;} /* Removing the introduction text from the view */
			        span[class=ios-color-hack] a {color:#275100!important;text-decoration:none!important;}
			        span[class=ios-color-hack2] a {color:#205478!important;text-decoration:none!important;}
			        span[class=ios-color-hack3] a {color:#8B8B8B!important;text-decoration:none!important;}
			        .a[href^=""tel""], a[href^=""sms""] {text-decoration:none!important;color:#606060!important;pointer-events:none!important;cursor:default!important;}
			        .mobile_link a[href^=""tel""], .mobile_link a[href^=""sms""] {text-decoration:none!important;color:#606060!important;pointer-events:auto!important;cursor:default!important;}

			        @media only screen and (max-width: 480px){
				        body{width:100% !important; min-width:100% !important;}
				        /*td[class=""textContent""], td[class=""flexibleContainerCell""] { width: 100%; padding-left: 10px !important; padding-right: 10px !important; }*/
				        table[id=""emailHeader""],
				        table[id=""emailBody""],
				        table[id=""emailFooter""],
				        table[class=""flexibleContainer""],
				        td[class=""flexibleContainerCell""] {width:100% !important;}
				        td[class=""flexibleContainerBox""], td[class=""flexibleContainerBox""] table {display: block;width: 100%;text-align: left;}
				        td[class=""imageContent""] img {height:auto !important; width:100% !important; max-width:100% !important; }
				        img[class=""flexibleImage""]{height:auto !important; width:100% !important;max-width:100% !important;}
				        img[class=""flexibleImageSmall""]{height:auto !important; width:auto !important;}
				        table[class=""emailButton""]{width:100% !important;}
				        td[class=""buttonContent""]{padding:0 !important;}
				        td[class=""buttonContent""] a{padding:15px !important;}
			        }
			        @media only screen and (-webkit-device-pixel-ratio:.75){
			        }
			        @media only screen and (-webkit-device-pixel-ratio:1){
			        }
			        @media only screen and (-webkit-device-pixel-ratio:1.5){
			        }
			        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
			        }
		        </style>
		        <!--[if mso 12]><style type=""text/css"">.flexibleContainer{display:block !important; width:100% !important;}</style><![endif]-->
		        <!--[if mso 14]><style type=""text/css"">.flexibleContainer{display:block !important; width:100% !important;}</style><![endif]-->
	        </head>
	        <body bgcolor=""#E1E1E1"" leftmargin=""0"" marginwidth=""0"" topmargin=""0"" marginheight=""0"" offset=""0"">
		        <center style=""background-color:#E1E1E1;"">
			        <table border=""0"" cellpadding=""0"" cellspacing=""0"" height=""100%"" width=""100%"" id=""bodyTable"" style=""table-layout: fixed;max-width:100% !important;width: 100% !important;min-width: 100% !important;"">
				        <tr>
					        <td align=""center"" valign=""top"" id=""bodyCell"">
						        <table bgcolor=""#FFFFFF""  border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" id=""emailBody"">
							        <tr>
								        <td align=""center"" valign=""top"">
									        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""color:#FFFFFF;"" bgcolor=""#3498db"">
										        <tr>
											        <td align=""center"" valign=""top"">
												        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
													        <tr>
														        <td align=""center"" valign=""top"" width=""500"" class=""flexibleContainerCell"">
															        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""100%"">
																        <tr>
																	        <td align=""center"" valign=""top"" class=""textContent"">
																		        <h1 style=""color:#FFFFFF;line-height:100%;font-family:Helvetica,Arial,sans-serif;font-size:35px;font-weight:normal;margin-bottom:5px;text-align:center;"">Colegio Ofelia Narváez Rincón</h1>
																		        <h2 style=""text-align:center;font-weight:normal;font-family:Helvetica,Arial,sans-serif;font-size:23px;margin-bottom:10px;color:#205478;line-height:135%;"">Colegio Ofelia Narváez Rincón, A.C.</h2>
																		        <div style=""text-align:center;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#FFFFFF;line-height:135%;"">Registro</div>
																	        </td>
																        </tr>
															        </table>
														        </td>
													        </tr>
												        </table>
											        </td>
										        </tr>
									        </table>
								        </td>
							        </tr>
							        <tr mc:hideable>
								        <td align=""center"" valign=""top"">
									        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
										        <tr>
											        <td align=""center"" valign=""top"">
												        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
													        <tr>
														        <td valign=""top"" width=""500"" class=""flexibleContainerCell"">
															        <table align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
																        <tr>
																	        <td align=""left"" valign=""top"" class=""flexibleContainerBox"">
																		        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""210"" style=""max-width: 100%;"">
																			        <tr>
																				        <td align=""left"" class=""textContent"">
																					        <h3 style=""color:#5F5F5F;line-height:125%;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;margin-top:0;margin-bottom:3px;text-align:left;"">Usuario:</h3>
																					        <div style=""text-align:left;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#5F5F5F;line-height:135%;"">" + usuario + @"</div>
																				        </td>
																			        </tr>
																		        </table>
																	        </td>
																	        <td align=""right"" valign=""middle"" class=""flexibleContainerBox"">
																		        <table class=""flexibleContainerBoxNext"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""210"" style=""max-width: 100%;"">
																			        <tr>
																				        <td align=""left"" class=""textContent"">
																					        <h3 style=""color:#5F5F5F;line-height:125%;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;margin-top:0;margin-bottom:3px;text-align:left;"">Contraseña:</h3>
																					        <div style=""text-align:left;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#5F5F5F;line-height:135%;"">" + password + @"</div>
																				        </td>
																			        </tr>
																		        </table>
																	        </td>
																        </tr>
															        </table>
															        <br><br>
														        </td>
													        </tr>
												        </table>
											        </td>
										        </tr>
									        </table>
								        </td>
							        </tr>
							        <tr>
								        <td align=""center"" valign=""top"">
									        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
										        <tr style=""padding-top:0;"">
											        <td align=""center"" valign=""top"">
												        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
													        <tr>
														        <td style=""padding-top:0;"" align=""center"" valign=""top"" width=""500"" class=""flexibleContainerCell"">
															        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""50%"" class=""emailButton"" style=""background-color: #b6b24c;"">
																        <tr>
																	        <td align=""center"" valign=""middle"" class=""buttonContent"" style=""padding-top:15px;padding-bottom:15px;padding-right:15px;padding-left:15px;"">
																		        <a style=""color:#FFFFFF;text-decoration:none;font-family:Helvetica,Arial,sans-serif;font-size:20px;line-height:135%;"" href=""" + dominio + login + @""" target=""_blank"">Ingresar</a>
																	        </td>
																        </tr>
															        </table>
														        </td>
													        </tr>
												        </table>
											        </td>
										        </tr>
									        </table>
								        </td>
							        </tr>
						        </table>
						        <table bgcolor=""#E1E1E1"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" id=""emailFooter"">
							        <tr>
								        <td align=""center"" valign=""top"">
									        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
										        <tr>
											        <td align=""center"" valign=""top"">
												        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
													        <tr>
														        <td align=""center"" valign=""top"" width=""500"" class=""flexibleContainerCell"">
															        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""100%"">
																        <tr>
																	        <td valign=""top"" bgcolor=""#E1E1E1"">
																		        <div style=""font-family:Helvetica,Arial,sans-serif;font-size:13px;color:#828282;text-align:center;line-height:120%;"">
																			        <div>Copyright &#169; """ + año + @""" <a href=""" + dominio + @""" target=""_blank"" style=""text-decoration:none;color:#828282;""><span style=""color:#828282;"">CSL</span></a>. All&nbsp;rights&nbsp;reserved.</div>
																			        <div>Creativa Softline <a href=""" + creativa + @""" target=""""_blank"""" style=""text-decoration:none;color:#828282;""><span style=""color:#828282;"">comunicarse</span></a>.</div>
																		        </div>
																	        </td>
																        </tr>
															        </table>
														        </td>
													        </tr>
												        </table>
											        </td>
										        </tr>
									        </table>
								        </td>
							        </tr>
						        </table>
					        </td>
				        </tr>
			        </table>
		        </center>
	        </body>
        </html>
            ";
            return html;
        }

        public static string GenerarHtmlMensajeContacto(string Nombre, string Correo, string Telefono, string Mensaje)
        {
            string dominio = "http://viajesItzaa.com.mx.negox.com";
            string login = "/Admin/Account/";
            string creativa = "creativasoftline.com";
            string html = @"
            <!DOCTYPE html>
	        <html>
	        <head>
		        <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
		        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
		        <meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1"">
		        <meta name=""format-detection"" content=""telephone=no"" />
		        <title>Colegio Ofelia Narváez Rincón</title>
		        <style type=""text/css"">
			        html { background-color:#E1E1E1; margin:0; padding:0; }
			        body, #bodyTable, #bodyCell, #bodyCell{height:100% !important; margin:0; padding:0; width:100% !important;font-family:Helvetica, Arial, ""Lucida Grande"", sans-serif;}
			        table{border-collapse:collapse;}
			        table[id=bodyTable] {width:100%!important;margin:auto;max-width:500px!important;color:#b6b24c;font-weight:normal;}
			        img, a img{border:0; outline:none; text-decoration:none;height:auto; line-height:100%;}
			        a {text-decoration:none !important;border-bottom: 1px solid;}
			        h1, h2, h3, h4, h5, h6{color:#5F5F5F; font-weight:normal; font-family:Helvetica; font-size:20px; line-height:125%; text-align:Left; letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:10px;margin-left:0;padding-top:0;padding-bottom:0;padding-left:0;padding-right:0;}
			        .ReadMsgBody{width:100%;} .ExternalClass{width:100%;} /* Force Hotmail/Outlook.com to display emails at full width. */
			        .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div{line-height:100%;} /* Force Hotmail/Outlook.com to display line heights normally. */
			        table, td{mso-table-lspace:0pt; mso-table-rspace:0pt;} /* Remove spacing between tables in Outlook 2007 and up. */
			        #outlook a{padding:0;} /* Force Outlook 2007 and up to provide a ""view in browser"" message. */
			        img{-ms-interpolation-mode: bicubic;display:block;outline:none; text-decoration:none;} /* Force IE to smoothly render resized images. */
			        body, table, td, p, a, li, blockquote{-ms-text-size-adjust:100%; -webkit-text-size-adjust:100%; font-weight:normal!important;} /* Prevent Windows- and Webkit-based mobile platforms from changing declared text sizes. */
			        .ExternalClass td[class=""ecxflexibleContainerBox""] h3 {padding-top: 10px !important;} /* Force hotmail to push 2-grid sub headers down */

			        h1{display:block;font-size:26px;font-style:normal;font-weight:normal;line-height:100%;}
			        h2{display:block;font-size:20px;font-style:normal;font-weight:normal;line-height:120%;}
			        h3{display:block;font-size:17px;font-style:normal;font-weight:normal;line-height:110%;}
			        h4{display:block;font-size:18px;font-style:italic;font-weight:normal;line-height:100%;}
			        .flexibleImage{height:auto;}
			        .linkRemoveBorder{border-bottom:0 !important;}
			        table[class=flexibleContainerCellDivider] {padding-bottom:0 !important;padding-top:0 !important;}

			        body, #bodyTable{background-color:#E1E1E1;}
			        #emailHeader{background-color:#b6b24c;}
			        #emailBody{background-color:#FFFFFF;}
			        #emailFooter{background-color:#E1E1E1;}
			        .nestedContainer{background-color:#F8F8F8; border:1px solid #CCCCCC;}
			        .emailButton{background-color:#205478; border-collapse:separate;}
			        .buttonContent{color:#FFFFFF; font-family:Helvetica; font-size:18px; font-weight:bold; line-height:100%; padding:15px; text-align:center;}
			        .buttonContent a{color:#FFFFFF; display:block; text-decoration:none!important; border:0!important;}
			        .emailCalendar{background-color:#FFFFFF; border:1px solid #CCCCCC;}
			        .emailCalendarMonth{background-color:#205478; color:#FFFFFF; font-family:Helvetica, Arial, sans-serif; font-size:16px; font-weight:bold; padding-top:10px; padding-bottom:10px; text-align:center;}
			        .emailCalendarDay{color:#205478; font-family:Helvetica, Arial, sans-serif; font-size:60px; font-weight:bold; line-height:100%; padding-top:20px; padding-bottom:20px; text-align:center;}
			        .imageContentText {margin-top: 10px;line-height:0;}
			        .imageContentText a {line-height:0;}
			        #invisibleIntroduction {display:none !important;} /* Removing the introduction text from the view */
			        span[class=ios-color-hack] a {color:#275100!important;text-decoration:none!important;}
			        span[class=ios-color-hack2] a {color:#205478!important;text-decoration:none!important;}
			        span[class=ios-color-hack3] a {color:#8B8B8B!important;text-decoration:none!important;}
			        .a[href^=""tel""], a[href^=""sms""] {text-decoration:none!important;color:#606060!important;pointer-events:none!important;cursor:default!important;}
			        .mobile_link a[href^=""tel""], .mobile_link a[href^=""sms""] {text-decoration:none!important;color:#606060!important;pointer-events:auto!important;cursor:default!important;}

			        @media only screen and (max-width: 480px){
				        body{width:100% !important; min-width:100% !important;}
				        /*td[class=""textContent""], td[class=""flexibleContainerCell""] { width: 100%; padding-left: 10px !important; padding-right: 10px !important; }*/
				        table[id=""emailHeader""],
				        table[id=""emailBody""],
				        table[id=""emailFooter""],
				        table[class=""flexibleContainer""],
				        td[class=""flexibleContainerCell""] {width:100% !important;}
				        td[class=""flexibleContainerBox""], td[class=""flexibleContainerBox""] table {display: block;width: 100%;text-align: left;}
				        td[class=""imageContent""] img {height:auto !important; width:100% !important; max-width:100% !important; }
				        img[class=""flexibleImage""]{height:auto !important; width:100% !important;max-width:100% !important;}
				        img[class=""flexibleImageSmall""]{height:auto !important; width:auto !important;}
				        table[class=""emailButton""]{width:100% !important;}
				        td[class=""buttonContent""]{padding:0 !important;}
				        td[class=""buttonContent""] a{padding:15px !important;}
			        }
			        @media only screen and (-webkit-device-pixel-ratio:.75){
			        }
			        @media only screen and (-webkit-device-pixel-ratio:1){
			        }
			        @media only screen and (-webkit-device-pixel-ratio:1.5){
			        }
			        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
			        }
		        </style>
		        <!--[if mso 12]><style type=""text/css"">.flexibleContainer{display:block !important; width:100% !important;}</style><![endif]-->
		        <!--[if mso 14]><style type=""text/css"">.flexibleContainer{display:block !important; width:100% !important;}</style><![endif]-->
	        </head>
	        <body bgcolor=""#E1E1E1"" leftmargin=""0"" marginwidth=""0"" topmargin=""0"" marginheight=""0"" offset=""0"">
		        <center style=""background-color:#E1E1E1;"">
			        <table border=""0"" cellpadding=""0"" cellspacing=""0"" height=""100%"" width=""100%"" id=""bodyTable"" style=""table-layout: fixed;max-width:100% !important;width: 100% !important;min-width: 100% !important;"">
				        <tr>
					        <td align=""center"" valign=""top"" id=""bodyCell"">
						        <table bgcolor=""#FFFFFF""  border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" id=""emailBody"">
							        <tr>
								        <td align=""center"" valign=""top"">
									        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""color:#FFFFFF;"" bgcolor=""#3498db"">
										        <tr>
											        <td align=""center"" valign=""top"">
												        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
													        <tr>
														        <td align=""center"" valign=""top"" width=""500"" class=""flexibleContainerCell"">
															        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""100%"">
																        <tr>
																	        <td align=""center"" valign=""top"" class=""textContent"">
																		        <h1 style=""color:#FFFFFF;line-height:100%;font-family:Helvetica,Arial,sans-serif;font-size:35px;font-weight:normal;margin-bottom:5px;text-align:center;"">Colegio Ofelia Narváez Rincón</h1>
																		        <h2 style=""text-align:center;font-weight:normal;font-family:Helvetica,Arial,sans-serif;font-size:23px;margin-bottom:10px;color:#205478;line-height:135%;"">Colegio Ofelia Narváez Rincón</h2>
																		        <div style=""text-align:center;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#FFFFFF;line-height:135%;"">Gracias por contactarnos, hemos recibido su mensaje, en breve le responderemos</div>
																	        </td>
																        </tr>
															        </table>
														        </td>
													        </tr>
												        </table>
											        </td>
										        </tr>
									        </table>
								        </td>
							        </tr>
							        <tr mc:hideable>
								        <td align=""center"" valign=""top"">
									        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
										        <tr>
											        <td align=""center"" valign=""top"">
												        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
													        <tr>
														        <td valign=""top"" width=""500"" class=""flexibleContainerCell"">
															        <table align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
																        <tr>
																	        <td align=""left"" valign=""top"" class=""flexibleContainerBox"">
																		        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""210"" style=""max-width: 100%;"">
																			        <tr>
																				        <td align=""left"" class=""textContent"">
																					        <h3 style=""color:#5F5F5F;line-height:125%;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;margin-top:0;margin-bottom:3px;text-align:left;"">Nombre:</h3>
																					        <div style=""text-align:left;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#5F5F5F;line-height:135%;"">" + Nombre + @"</div>
																				        </td>
																			        </tr>
																		        </table>
																	        </td>
																	        <td align=""right"" valign=""middle"" class=""flexibleContainerBox"">
																		        <table class=""flexibleContainerBoxNext"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""210"" style=""max-width: 100%;"">
																			        <tr>
																				        <td align=""left"" class=""textContent"">
																					        <h3 style=""color:#5F5F5F;line-height:125%;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;margin-top:0;margin-bottom:3px;text-align:left;"">Correo:</h3>
																					        <div style=""text-align:left;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#5F5F5F;line-height:135%;"">" + Correo + @"</div>
																				        </td>
																			        </tr>
																		        </table>
																	        </td>
																        </tr>
																        <tr>
																	        <td align=""left"" valign=""top"" class=""flexibleContainerBox"">
																		        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""210"" style=""max-width: 100%;"">
																			        <tr>
																				        <td align=""left"" class=""textContent"">
																					        <h3 style=""color:#5F5F5F;line-height:125%;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;margin-top:0;margin-bottom:3px;text-align:left;"">Telefono:</h3>
																					        <div style=""text-align:left;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#5F5F5F;line-height:135%;"">" + Telefono + @"</div>
																				        </td>
																			        </tr>
																		        </table>
																	        </td>
																        </tr>
															        </table>
															        <br><br>
														        </td>
													        </tr>
												        </table>
											        </td>
										        </tr>
									        </table>
								        </td>
							        </tr>
							        <tr>
								        <td align=""center"" valign=""top"">
											<table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
												<tr style=""padding-top:0;"">
													<td align=""center"" valign=""top"">
														<table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
															<tr>
																<td align=""left"" class=""textContent"">
																	<h3 style=""color:#5F5F5F;line-height:125%;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;margin-top:0;margin-bottom:3px;text-align:left;"">Mensaje:</h3>
																	<div style=""text-align:left;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#5F5F5F;line-height:135%;"">" + Mensaje + @"</div>
														        </td>
															</tr>
														</table>
													</td>
												</tr>
											</table>
										</td>
							        </tr>
						        </table>
						        <table bgcolor=""#E1E1E1"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" id=""emailFooter"">
							        <tr>
								        <td align=""center"" valign=""top"">
									        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
										        <tr>
											        <td align=""center"" valign=""top"">
												        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
													        <tr>
														        <td align=""center"" valign=""top"" width=""500"" class=""flexibleContainerCell"">
															        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""100%"">
																        <tr>
																	        <td valign=""top"" bgcolor=""#E1E1E1"">
																		        <div style=""font-family:Helvetica,Arial,sans-serif;font-size:13px;color:#828282;text-align:center;line-height:120%;"">
																			        <div>Copyright &#169; 2016 <a href=""" + dominio + @""" target=""_blank"" style=""text-decoration:none;color:#828282;""><span style=""color:#828282;"">CSL</span></a>. All&nbsp;rights&nbsp;reserved.</div>
																			        <div>Creativa Softline <a href=""" + creativa + @""" target=""""_blank"""" style=""text-decoration:none;color:#828282;""><span style=""color:#828282;"">comunicarse</span></a>.</div>
																		        </div>
																	        </td>
																        </tr>
															        </table>
														        </td>
													        </tr>
												        </table>
											        </td>
										        </tr>
									        </table>
								        </td>
							        </tr>
						        </table>
					        </td>
				        </tr>
			        </table>
		        </center>
	        </body>
        </html>
            ";
            return html;
        }

        public static string GenerarHtmlRespuestaContacto(string Nombre, string Correo, string Telefono, string Mensaje, string Respuesta)
        {
            string dominio = "http://viajesItzaa.com.mx.negox.com";
            string login = "/Admin/Account/";
            string creativa = "creativasoftline.com";
            string html = @"
                        <!DOCTYPE html>
	        <html>
	        <head>
		        <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
		        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
		        <meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1"">
		        <meta name=""format-detection"" content=""telephone=no"" />
		        <title>Colegio Ofelia Narváez Rincón</title>
		        <style type=""text/css"">
			        html { background-color:#E1E1E1; margin:0; padding:0; }
			        body, #bodyTable, #bodyCell, #bodyCell{height:100% !important; margin:0; padding:0; width:100% !important;font-family:Helvetica, Arial, ""Lucida Grande"", sans-serif;}
			        table{border-collapse:collapse;}
			        table[id=bodyTable] {width:100%!important;margin:auto;max-width:500px!important;color:#b6b24c;font-weight:normal;}
			        img, a img{border:0; outline:none; text-decoration:none;height:auto; line-height:100%;}
			        a {text-decoration:none !important;border-bottom: 1px solid;}
			        h1, h2, h3, h4, h5, h6{color:#5F5F5F; font-weight:normal; font-family:Helvetica; font-size:20px; line-height:125%; text-align:Left; letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:10px;margin-left:0;padding-top:0;padding-bottom:0;padding-left:0;padding-right:0;}
			        .ReadMsgBody{width:100%;} .ExternalClass{width:100%;} /* Force Hotmail/Outlook.com to display emails at full width. */
			        .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div{line-height:100%;} /* Force Hotmail/Outlook.com to display line heights normally. */
			        table, td{mso-table-lspace:0pt; mso-table-rspace:0pt;} /* Remove spacing between tables in Outlook 2007 and up. */
			        #outlook a{padding:0;} /* Force Outlook 2007 and up to provide a ""view in browser"" message. */
			        img{-ms-interpolation-mode: bicubic;display:block;outline:none; text-decoration:none;} /* Force IE to smoothly render resized images. */
			        body, table, td, p, a, li, blockquote{-ms-text-size-adjust:100%; -webkit-text-size-adjust:100%; font-weight:normal!important;} /* Prevent Windows- and Webkit-based mobile platforms from changing declared text sizes. */
			        .ExternalClass td[class=""ecxflexibleContainerBox""] h3 {padding-top: 10px !important;} /* Force hotmail to push 2-grid sub headers down */

			        h1{display:block;font-size:26px;font-style:normal;font-weight:normal;line-height:100%;}
			        h2{display:block;font-size:20px;font-style:normal;font-weight:normal;line-height:120%;}
			        h3{display:block;font-size:17px;font-style:normal;font-weight:normal;line-height:110%;}
			        h4{display:block;font-size:18px;font-style:italic;font-weight:normal;line-height:100%;}
			        .flexibleImage{height:auto;}
			        .linkRemoveBorder{border-bottom:0 !important;}
			        table[class=flexibleContainerCellDivider] {padding-bottom:0 !important;padding-top:0 !important;}

			        body, #bodyTable{background-color:#E1E1E1;}
			        #emailHeader{background-color:#b6b24c;}
			        #emailBody{background-color:#FFFFFF;}
			        #emailFooter{background-color:#E1E1E1;}
			        .nestedContainer{background-color:#F8F8F8; border:1px solid #CCCCCC;}
			        .emailButton{background-color:#205478; border-collapse:separate;}
			        .buttonContent{color:#FFFFFF; font-family:Helvetica; font-size:18px; font-weight:bold; line-height:100%; padding:15px; text-align:center;}
			        .buttonContent a{color:#FFFFFF; display:block; text-decoration:none!important; border:0!important;}
			        .emailCalendar{background-color:#FFFFFF; border:1px solid #CCCCCC;}
			        .emailCalendarMonth{background-color:#205478; color:#FFFFFF; font-family:Helvetica, Arial, sans-serif; font-size:16px; font-weight:bold; padding-top:10px; padding-bottom:10px; text-align:center;}
			        .emailCalendarDay{color:#205478; font-family:Helvetica, Arial, sans-serif; font-size:60px; font-weight:bold; line-height:100%; padding-top:20px; padding-bottom:20px; text-align:center;}
			        .imageContentText {margin-top: 10px;line-height:0;}
			        .imageContentText a {line-height:0;}
			        #invisibleIntroduction {display:none !important;} /* Removing the introduction text from the view */
			        span[class=ios-color-hack] a {color:#275100!important;text-decoration:none!important;}
			        span[class=ios-color-hack2] a {color:#205478!important;text-decoration:none!important;}
			        span[class=ios-color-hack3] a {color:#8B8B8B!important;text-decoration:none!important;}
			        .a[href^=""tel""], a[href^=""sms""] {text-decoration:none!important;color:#606060!important;pointer-events:none!important;cursor:default!important;}
			        .mobile_link a[href^=""tel""], .mobile_link a[href^=""sms""] {text-decoration:none!important;color:#606060!important;pointer-events:auto!important;cursor:default!important;}

			        @media only screen and (max-width: 480px){
				        body{width:100% !important; min-width:100% !important;}
				        /*td[class=""textContent""], td[class=""flexibleContainerCell""] { width: 100%; padding-left: 10px !important; padding-right: 10px !important; }*/
				        table[id=""emailHeader""],
				        table[id=""emailBody""],
				        table[id=""emailFooter""],
				        table[class=""flexibleContainer""],
				        td[class=""flexibleContainerCell""] {width:100% !important;}
				        td[class=""flexibleContainerBox""], td[class=""flexibleContainerBox""] table {display: block;width: 100%;text-align: left;}
				        td[class=""imageContent""] img {height:auto !important; width:100% !important; max-width:100% !important; }
				        img[class=""flexibleImage""]{height:auto !important; width:100% !important;max-width:100% !important;}
				        img[class=""flexibleImageSmall""]{height:auto !important; width:auto !important;}
				        table[class=""emailButton""]{width:100% !important;}
				        td[class=""buttonContent""]{padding:0 !important;}
				        td[class=""buttonContent""] a{padding:15px !important;}
			        }
			        @media only screen and (-webkit-device-pixel-ratio:.75){
			        }
			        @media only screen and (-webkit-device-pixel-ratio:1){
			        }
			        @media only screen and (-webkit-device-pixel-ratio:1.5){
			        }
			        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
			        }
		        </style>
		        <!--[if mso 12]><style type=""text/css"">.flexibleContainer{display:block !important; width:100% !important;}</style><![endif]-->
		        <!--[if mso 14]><style type=""text/css"">.flexibleContainer{display:block !important; width:100% !important;}</style><![endif]-->
	        </head>
	        <body bgcolor=""#E1E1E1"" leftmargin=""0"" marginwidth=""0"" topmargin=""0"" marginheight=""0"" offset=""0"">
		        <center style=""background-color:#E1E1E1;"">
			        <table border=""0"" cellpadding=""0"" cellspacing=""0"" height=""100%"" width=""100%"" id=""bodyTable"" style=""table-layout: fixed;max-width:100% !important;width: 100% !important;min-width: 100% !important;"">
				        <tr>
					        <td align=""center"" valign=""top"" id=""bodyCell"">
						        <table bgcolor=""#FFFFFF""  border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" id=""emailBody"">
							        <tr>
								        <td align=""center"" valign=""top"">
									        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""color:#FFFFFF;"" bgcolor=""#3498db"">
										        <tr>
											        <td align=""center"" valign=""top"">
												        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
													        <tr>
														        <td align=""center"" valign=""top"" width=""500"" class=""flexibleContainerCell"">
															        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""100%"">
																        <tr>
																	        <td align=""center"" valign=""top"" class=""textContent"">
																		        <h1 style=""color:#FFFFFF;line-height:100%;font-family:Helvetica,Arial,sans-serif;font-size:35px;font-weight:normal;margin-bottom:5px;text-align:center;"">Colegio Ofelia Narváez Rincón</h1>
																		        <h2 style=""text-align:center;font-weight:normal;font-family:Helvetica,Arial,sans-serif;font-size:23px;margin-bottom:10px;color:#205478;line-height:135%;"">Colegio Ofelia Narváez Rincón</h2>
																		        <div style=""text-align:center;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#FFFFFF;line-height:135%;"">Gracias por contactarnos</div>
																	        </td>
																        </tr>
															        </table>
														        </td>
													        </tr>
												        </table>
											        </td>
										        </tr>
									        </table>
								        </td>
							        </tr>
							        <tr mc:hideable>
								        <td align=""center"" valign=""top"">
									        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
										        <tr>
											        <td align=""center"" valign=""top"">
												        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
													        <tr>
														        <td valign=""top"" width=""500"" class=""flexibleContainerCell"">
															        <table align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
																        <tr>
																	        <td align=""left"" valign=""top"" class=""flexibleContainerBox"">
																		        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""210"" style=""max-width: 100%;"">
																			        <tr>
																				        <td align=""left"" class=""textContent"">
																					        <h3 style=""color:#5F5F5F;line-height:125%;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;margin-top:0;margin-bottom:3px;text-align:left;"">Nombre:</h3>
																					        <div style=""text-align:left;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#5F5F5F;line-height:135%;"">" + Nombre + @"</div>
																				        </td>
																			        </tr>
																		        </table>
																	        </td>
																	        <td align=""right"" valign=""middle"" class=""flexibleContainerBox"">
																		        <table class=""flexibleContainerBoxNext"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""210"" style=""max-width: 100%;"">
																			        <tr>
																				        <td align=""left"" class=""textContent"">
																					        <h3 style=""color:#5F5F5F;line-height:125%;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;margin-top:0;margin-bottom:3px;text-align:left;"">Correo:</h3>
																					        <div style=""text-align:left;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#5F5F5F;line-height:135%;"">" + Correo + @"</div>
																				        </td>
																			        </tr>
																		        </table>
																	        </td>
																        </tr>
																        <tr>
																	        <td align=""left"" valign=""top"" class=""flexibleContainerBox"">
																		        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""210"" style=""max-width: 100%;"">
																			        <tr>
																				        <td align=""left"" class=""textContent"">
																					        <h3 style=""color:#5F5F5F;line-height:125%;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;margin-top:0;margin-bottom:3px;text-align:left;"">Telefono:</h3>
																					        <div style=""text-align:left;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#5F5F5F;line-height:135%;"">" + Telefono + @"</div>
																				        </td>
																			        </tr>
																		        </table>
																	        </td>
																        </tr>
															        </table>
															        <br><br>
														        </td>
													        </tr>
												        </table>
											        </td>
										        </tr>
									        </table>
								        </td>
							        </tr>
							        <tr>
								        <td align=""center"" valign=""top"">
											<table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
												<tr style=""padding-top:0;"">
													<td align=""center"" valign=""top"">
														<table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
															<tr>
																<td align=""left"" class=""textContent"">
																	<h3 style=""color:#5F5F5F;line-height:125%;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;margin-top:0;margin-bottom:3px;text-align:left;"">Mensaje:</h3>
																	<div style=""text-align:left;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#5F5F5F;line-height:135%;"">" + Mensaje + @"</div>
														        </td>
															</tr>
														</table>
													</td>
												</tr>
											</table>
										</td>
							        </tr>
							        <tr>
								        <td align=""center"" valign=""top"">
											<table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
												<tr style=""padding-top:0;"">
													<td align=""center"" valign=""top"">
														<table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
															<tr>
																<td align=""left"" class=""textContent"">
																	<h3 style=""color:#5F5F5F;line-height:125%;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;margin-top:0;margin-bottom:3px;text-align:left;"">Respuesta:</h3>
																	<div style=""text-align:left;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#5F5F5F;line-height:135%;"">" + Respuesta + @"</div>
														        </td>
															</tr>
														</table>
													</td>
												</tr>
											</table>
										</td>
							        </tr>
						        </table>
						        <table bgcolor=""#E1E1E1"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" id=""emailFooter"">
							        <tr>
								        <td align=""center"" valign=""top"">
									        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
										        <tr>
											        <td align=""center"" valign=""top"">
												        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
													        <tr>
														        <td align=""center"" valign=""top"" width=""500"" class=""flexibleContainerCell"">
															        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""100%"">
																        <tr>
																	        <td valign=""top"" bgcolor=""#E1E1E1"">
																		        <div style=""font-family:Helvetica,Arial,sans-serif;font-size:13px;color:#828282;text-align:center;line-height:120%;"">
																			        <div>Copyright &#169; 2016 <a href=""" + dominio + @""" target=""_blank"" style=""text-decoration:none;color:#828282;""><span style=""color:#828282;"">CSL</span></a>. All&nbsp;rights&nbsp;reserved.</div>
																			        <div>Creativa Softline <a href=""" + creativa + @""" target=""""_blank"""" style=""text-decoration:none;color:#828282;""><span style=""color:#828282;"">comunicarse</span></a>.</div>
																		        </div>
																	        </td>
																        </tr>
															        </table>
														        </td>
													        </tr>
												        </table>
											        </td>
										        </tr>
									        </table>
								        </td>
							        </tr>
						        </table>
					        </td>
				        </tr>
			        </table>
		        </center>
	        </body>
        </html>
            ";
            return html;
        }

        public static string GenerarHtmlResetContraseña(string usuario, string password)
        {
            string dominio = "http://www.creativasoftlineapps.com/ofelianarvaez";
            string login = "/Admin/Account/";
            string creativa = "creativasoftline.com";
            int año = DateTime.Now.Year;
            string html = @"
            <!DOCTYPE html>
	        <html>
	        <head>
		        <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
		        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
		        <meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1"">
		        <meta name=""format-detection"" content=""telephone=no"" />
		        <title>Colegio Ofelia Narváez Rincón</title>
		        <style type=""text/css"">
			        html { background-color:#E1E1E1; margin:0; padding:0; }
			        body, #bodyTable, #bodyCell, #bodyCell{height:100% !important; margin:0; padding:0; width:100% !important;font-family:Helvetica, Arial, ""Lucida Grande"", sans-serif;}
			        table{border-collapse:collapse;}
			        table[id=bodyTable] {width:100%!important;margin:auto;max-width:500px!important;color:#b6b24c;font-weight:normal;}
			        img, a img{border:0; outline:none; text-decoration:none;height:auto; line-height:100%;}
			        a {text-decoration:none !important;border-bottom: 1px solid;}
			        h1, h2, h3, h4, h5, h6{color:#5F5F5F; font-weight:normal; font-family:Helvetica; font-size:20px; line-height:125%; text-align:Left; letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:10px;margin-left:0;padding-top:0;padding-bottom:0;padding-left:0;padding-right:0;}
			        .ReadMsgBody{width:100%;} .ExternalClass{width:100%;} /* Force Hotmail/Outlook.com to display emails at full width. */
			        .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div{line-height:100%;} /* Force Hotmail/Outlook.com to display line heights normally. */
			        table, td{mso-table-lspace:0pt; mso-table-rspace:0pt;} /* Remove spacing between tables in Outlook 2007 and up. */
			        #outlook a{padding:0;} /* Force Outlook 2007 and up to provide a ""view in browser"" message. */
			        img{-ms-interpolation-mode: bicubic;display:block;outline:none; text-decoration:none;} /* Force IE to smoothly render resized images. */
			        body, table, td, p, a, li, blockquote{-ms-text-size-adjust:100%; -webkit-text-size-adjust:100%; font-weight:normal!important;} /* Prevent Windows- and Webkit-based mobile platforms from changing declared text sizes. */
			        .ExternalClass td[class=""ecxflexibleContainerBox""] h3 {padding-top: 10px !important;} /* Force hotmail to push 2-grid sub headers down */

			        h1{display:block;font-size:26px;font-style:normal;font-weight:normal;line-height:100%;}
			        h2{display:block;font-size:20px;font-style:normal;font-weight:normal;line-height:120%;}
			        h3{display:block;font-size:17px;font-style:normal;font-weight:normal;line-height:110%;}
			        h4{display:block;font-size:18px;font-style:italic;font-weight:normal;line-height:100%;}
			        .flexibleImage{height:auto;}
			        .linkRemoveBorder{border-bottom:0 !important;}
			        table[class=flexibleContainerCellDivider] {padding-bottom:0 !important;padding-top:0 !important;}

			        body, #bodyTable{background-color:#E1E1E1;}
			        #emailHeader{background-color:#b6b24c;}
			        #emailBody{background-color:#FFFFFF;}
			        #emailFooter{background-color:#E1E1E1;}
			        .nestedContainer{background-color:#F8F8F8; border:1px solid #CCCCCC;}
			        .emailButton{background-color:#205478; border-collapse:separate;}
			        .buttonContent{color:#FFFFFF; font-family:Helvetica; font-size:18px; font-weight:bold; line-height:100%; padding:15px; text-align:center;}
			        .buttonContent a{color:#FFFFFF; display:block; text-decoration:none!important; border:0!important;}
			        .emailCalendar{background-color:#FFFFFF; border:1px solid #CCCCCC;}
			        .emailCalendarMonth{background-color:#205478; color:#FFFFFF; font-family:Helvetica, Arial, sans-serif; font-size:16px; font-weight:bold; padding-top:10px; padding-bottom:10px; text-align:center;}
			        .emailCalendarDay{color:#205478; font-family:Helvetica, Arial, sans-serif; font-size:60px; font-weight:bold; line-height:100%; padding-top:20px; padding-bottom:20px; text-align:center;}
			        .imageContentText {margin-top: 10px;line-height:0;}
			        .imageContentText a {line-height:0;}
			        #invisibleIntroduction {display:none !important;} /* Removing the introduction text from the view */
			        span[class=ios-color-hack] a {color:#275100!important;text-decoration:none!important;}
			        span[class=ios-color-hack2] a {color:#205478!important;text-decoration:none!important;}
			        span[class=ios-color-hack3] a {color:#8B8B8B!important;text-decoration:none!important;}
			        .a[href^=""tel""], a[href^=""sms""] {text-decoration:none!important;color:#606060!important;pointer-events:none!important;cursor:default!important;}
			        .mobile_link a[href^=""tel""], .mobile_link a[href^=""sms""] {text-decoration:none!important;color:#606060!important;pointer-events:auto!important;cursor:default!important;}

			        @media only screen and (max-width: 480px){
				        body{width:100% !important; min-width:100% !important;}
				        /*td[class=""textContent""], td[class=""flexibleContainerCell""] { width: 100%; padding-left: 10px !important; padding-right: 10px !important; }*/
				        table[id=""emailHeader""],
				        table[id=""emailBody""],
				        table[id=""emailFooter""],
				        table[class=""flexibleContainer""],
				        td[class=""flexibleContainerCell""] {width:100% !important;}
				        td[class=""flexibleContainerBox""], td[class=""flexibleContainerBox""] table {display: block;width: 100%;text-align: left;}
				        td[class=""imageContent""] img {height:auto !important; width:100% !important; max-width:100% !important; }
				        img[class=""flexibleImage""]{height:auto !important; width:100% !important;max-width:100% !important;}
				        img[class=""flexibleImageSmall""]{height:auto !important; width:auto !important;}
				        table[class=""emailButton""]{width:100% !important;}
				        td[class=""buttonContent""]{padding:0 !important;}
				        td[class=""buttonContent""] a{padding:15px !important;}
			        }
			        @media only screen and (-webkit-device-pixel-ratio:.75){
			        }
			        @media only screen and (-webkit-device-pixel-ratio:1){
			        }
			        @media only screen and (-webkit-device-pixel-ratio:1.5){
			        }
			        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
			        }
		        </style>
		        <!--[if mso 12]><style type=""text/css"">.flexibleContainer{display:block !important; width:100% !important;}</style><![endif]-->
		        <!--[if mso 14]><style type=""text/css"">.flexibleContainer{display:block !important; width:100% !important;}</style><![endif]-->
	        </head>
	        <body bgcolor=""#E1E1E1"" leftmargin=""0"" marginwidth=""0"" topmargin=""0"" marginheight=""0"" offset=""0"">
		        <center style=""background-color:#E1E1E1;"">
			        <table border=""0"" cellpadding=""0"" cellspacing=""0"" height=""100%"" width=""100%"" id=""bodyTable"" style=""table-layout: fixed;max-width:100% !important;width: 100% !important;min-width: 100% !important;"">
				        <tr>
					        <td align=""center"" valign=""top"" id=""bodyCell"">
						        <table bgcolor=""#FFFFFF""  border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" id=""emailBody"">
							        <tr>
								        <td align=""center"" valign=""top"">
									        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""color:#FFFFFF;"" bgcolor=""#3498db"">
										        <tr>
											        <td align=""center"" valign=""top"">
												        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
													        <tr>
														        <td align=""center"" valign=""top"" width=""500"" class=""flexibleContainerCell"">
															        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""100%"">
																        <tr>
																	        <td align=""center"" valign=""top"" class=""textContent"">
																		        <h1 style=""color:#FFFFFF;line-height:100%;font-family:Helvetica,Arial,sans-serif;font-size:35px;font-weight:normal;margin-bottom:5px;text-align:center;"">Colegio Ofelia Narváez Rincón</h1>
																		        <h2 style=""text-align:center;font-weight:normal;font-family:Helvetica,Arial,sans-serif;font-size:23px;margin-bottom:10px;color:#205478;line-height:135%;"">Colegio Ofelia Narvaez Rincón</h2>
																		        <div style=""text-align:center;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#FFFFFF;line-height:135%;"">Contraseña reseteada este es usuario y contraseña</div>
																	        </td>
																        </tr>
															        </table>
														        </td>
													        </tr>
												        </table>
											        </td>
										        </tr>
									        </table>
								        </td>
							        </tr>
							        <tr mc:hideable>
								        <td align=""center"" valign=""top"">
									        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
										        <tr>
											        <td align=""center"" valign=""top"">
												        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
													        <tr>
														        <td valign=""top"" width=""500"" class=""flexibleContainerCell"">
															        <table align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
																        <tr>
																	        <td align=""left"" valign=""top"" class=""flexibleContainerBox"">
																		        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""210"" style=""max-width: 100%;"">
																			        <tr>
																				        <td align=""left"" class=""textContent"">
																					        <h3 style=""color:#5F5F5F;line-height:125%;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;margin-top:0;margin-bottom:3px;text-align:left;"">Usuario:</h3>
																					        <div style=""text-align:left;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#5F5F5F;line-height:135%;"">" + usuario + @"</div>
																				        </td>
																			        </tr>
																		        </table>
																	        </td>
																	        <td align=""right"" valign=""middle"" class=""flexibleContainerBox"">
																		        <table class=""flexibleContainerBoxNext"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""210"" style=""max-width: 100%;"">
																			        <tr>
																				        <td align=""left"" class=""textContent"">
																					        <h3 style=""color:#5F5F5F;line-height:125%;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;margin-top:0;margin-bottom:3px;text-align:left;"">Contraseña:</h3>
																					        <div style=""text-align:left;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#5F5F5F;line-height:135%;"">" + password + @"</div>
																				        </td>
																			        </tr>
																		        </table>
																	        </td>
																        </tr>
															        </table>
															        <br><br>
														        </td>
													        </tr>
												        </table>
											        </td>
										        </tr>
									        </table>
								        </td>
							        </tr>
							        <tr>
								        <td align=""center"" valign=""top"">
									        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
										        <tr style=""padding-top:0;"">
											        <td align=""center"" valign=""top"">
												        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
													        <tr>
														        <td style=""padding-top:0;"" align=""center"" valign=""top"" width=""500"" class=""flexibleContainerCell"">
															        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""50%"" class=""emailButton"" style=""background-color: #b6b24c;"">
																        <tr>
																	        <td align=""center"" valign=""middle"" class=""buttonContent"" style=""padding-top:15px;padding-bottom:15px;padding-right:15px;padding-left:15px;"">
																		        <a style=""color:#FFFFFF;text-decoration:none;font-family:Helvetica,Arial,sans-serif;font-size:20px;line-height:135%;"" href=""" + dominio + login + @""" target=""_blank"">Ingresar</a>
																	        </td>
																        </tr>
															        </table>
														        </td>
													        </tr>
												        </table>
											        </td>
										        </tr>
									        </table>
								        </td>
							        </tr>
						        </table>
						        <table bgcolor=""#E1E1E1"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" id=""emailFooter"">
							        <tr>
								        <td align=""center"" valign=""top"">
									        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
										        <tr>
											        <td align=""center"" valign=""top"">
												        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
													        <tr>
														        <td align=""center"" valign=""top"" width=""500"" class=""flexibleContainerCell"">
															        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""100%"">
																        <tr>
																	        <td valign=""top"" bgcolor=""#E1E1E1"">
																		        <div style=""font-family:Helvetica,Arial,sans-serif;font-size:13px;color:#828282;text-align:center;line-height:120%;"">
																			        <div>Copyright &#169; """ + año + @""" <a href=""" + dominio + @""" target=""_blank"" style=""text-decoration:none;color:#828282;""><span style=""color:#828282;"">CSL</span></a>. All&nbsp;rights&nbsp;reserved.</div>
																			        <div>Creativa Softline <a href=""" + creativa + @""" target=""""_blank"""" style=""text-decoration:none;color:#828282;""><span style=""color:#828282;"">comunicarse</span></a>.</div>
																		        </div>
																	        </td>
																        </tr>
															        </table>
														        </td>
													        </tr>
												        </table>
											        </td>
										        </tr>
									        </table>
								        </td>
							        </tr>
						        </table>
					        </td>
				        </tr>
			        </table>
		        </center>
	        </body>
        </html>
            ";
            return html;
        }

        public static string GenerarHtmlCorreos()
        {
            string dominio = "http://viajesItzaa.com.mx.negox.com";
            string link = "/Admin/Account/";
            string creativa = "creativasoftline.com";
            string html = @"
            <!DOCTYPE html>
	        <html>
	        <head>
		        <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
		        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
		        <meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1"">
		        <meta name=""format-detection"" content=""telephone=no"" />
		        <title>Colegio Ofelia Narváez Rincón</title>
		        <style type=""text/css"">
			        html { background-color:#E1E1E1; margin:0; padding:0; }
			        body, #bodyTable, #bodyCell, #bodyCell{height:100% !important; margin:0; padding:0; width:100% !important;font-family:Helvetica, Arial, ""Lucida Grande"", sans-serif;}
			        table{border-collapse:collapse;}
			        table[id=bodyTable] {width:100%!important;margin:auto;max-width:500px!important;color:#b6b24c;font-weight:normal;}
			        img, a img{border:0; outline:none; text-decoration:none;height:auto; line-height:100%;}
			        a {text-decoration:none !important;border-bottom: 1px solid;}
			        h1, h2, h3, h4, h5, h6{color:#5F5F5F; font-weight:normal; font-family:Helvetica; font-size:20px; line-height:125%; text-align:Left; letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:10px;margin-left:0;padding-top:0;padding-bottom:0;padding-left:0;padding-right:0;}
			        .ReadMsgBody{width:100%;} .ExternalClass{width:100%;} /* Force Hotmail/Outlook.com to display emails at full width. */
			        .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div{line-height:100%;} /* Force Hotmail/Outlook.com to display line heights normally. */
			        table, td{mso-table-lspace:0pt; mso-table-rspace:0pt;} /* Remove spacing between tables in Outlook 2007 and up. */
			        #outlook a{padding:0;} /* Force Outlook 2007 and up to provide a ""view in browser"" message. */
			        img{-ms-interpolation-mode: bicubic;display:block;outline:none; text-decoration:none;} /* Force IE to smoothly render resized images. */
			        body, table, td, p, a, li, blockquote{-ms-text-size-adjust:100%; -webkit-text-size-adjust:100%; font-weight:normal!important;} /* Prevent Windows- and Webkit-based mobile platforms from changing declared text sizes. */
			        .ExternalClass td[class=""ecxflexibleContainerBox""] h3 {padding-top: 10px !important;} /* Force hotmail to push 2-grid sub headers down */

			        h1{display:block;font-size:26px;font-style:normal;font-weight:normal;line-height:100%;}
			        h2{display:block;font-size:20px;font-style:normal;font-weight:normal;line-height:120%;}
			        h3{display:block;font-size:17px;font-style:normal;font-weight:normal;line-height:110%;}
			        h4{display:block;font-size:18px;font-style:italic;font-weight:normal;line-height:100%;}
			        .flexibleImage{height:auto;}
			        .linkRemoveBorder{border-bottom:0 !important;}
			        table[class=flexibleContainerCellDivider] {padding-bottom:0 !important;padding-top:0 !important;}

			        body, #bodyTable{background-color:#E1E1E1;}
			        #emailHeader{background-color:#b6b24c;}
			        #emailBody{background-color:#FFFFFF;}
			        #emailFooter{background-color:#E1E1E1;}
			        .nestedContainer{background-color:#F8F8F8; border:1px solid #CCCCCC;}
			        .emailButton{background-color:#205478; border-collapse:separate;}
			        .buttonContent{color:#FFFFFF; font-family:Helvetica; font-size:18px; font-weight:bold; line-height:100%; padding:15px; text-align:center;}
			        .buttonContent a{color:#FFFFFF; display:block; text-decoration:none!important; border:0!important;}
			        .emailCalendar{background-color:#FFFFFF; border:1px solid #CCCCCC;}
			        .emailCalendarMonth{background-color:#205478; color:#FFFFFF; font-family:Helvetica, Arial, sans-serif; font-size:16px; font-weight:bold; padding-top:10px; padding-bottom:10px; text-align:center;}
			        .emailCalendarDay{color:#205478; font-family:Helvetica, Arial, sans-serif; font-size:60px; font-weight:bold; line-height:100%; padding-top:20px; padding-bottom:20px; text-align:center;}
			        .imageContentText {margin-top: 10px;line-height:0;}
			        .imageContentText a {line-height:0;}
			        #invisibleIntroduction {display:none !important;} /* Removing the introduction text from the view */
			        span[class=ios-color-hack] a {color:#275100!important;text-decoration:none!important;}
			        span[class=ios-color-hack2] a {color:#205478!important;text-decoration:none!important;}
			        span[class=ios-color-hack3] a {color:#8B8B8B!important;text-decoration:none!important;}
			        .a[href^=""tel""], a[href^=""sms""] {text-decoration:none!important;color:#606060!important;pointer-events:none!important;cursor:default!important;}
			        .mobile_link a[href^=""tel""], .mobile_link a[href^=""sms""] {text-decoration:none!important;color:#606060!important;pointer-events:auto!important;cursor:default!important;}

			        @media only screen and (max-width: 480px){
				        body{width:100% !important; min-width:100% !important;}
				        /*td[class=""textContent""], td[class=""flexibleContainerCell""] { width: 100%; padding-left: 10px !important; padding-right: 10px !important; }*/
				        table[id=""emailHeader""],
				        table[id=""emailBody""],
				        table[id=""emailFooter""],
				        table[class=""flexibleContainer""],
				        td[class=""flexibleContainerCell""] {width:100% !important;}
				        td[class=""flexibleContainerBox""], td[class=""flexibleContainerBox""] table {display: block;width: 100%;text-align: left;}
				        td[class=""imageContent""] img {height:auto !important; width:100% !important; max-width:100% !important; }
				        img[class=""flexibleImage""]{height:auto !important; width:100% !important;max-width:100% !important;}
				        img[class=""flexibleImageSmall""]{height:auto !important; width:auto !important;}
				        table[class=""emailButton""]{width:100% !important;}
				        td[class=""buttonContent""]{padding:0 !important;}
				        td[class=""buttonContent""] a{padding:15px !important;}
			        }
			        @media only screen and (-webkit-device-pixel-ratio:.75){
			        }
			        @media only screen and (-webkit-device-pixel-ratio:1){
			        }
			        @media only screen and (-webkit-device-pixel-ratio:1.5){
			        }
			        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
			        }
		        </style>
		        <!--[if mso 12]><style type=""text/css"">.flexibleContainer{display:block !important; width:100% !important;}</style><![endif]-->
		        <!--[if mso 14]><style type=""text/css"">.flexibleContainer{display:block !important; width:100% !important;}</style><![endif]-->
	        </head>
	        <body bgcolor=""#E1E1E1"" leftmargin=""0"" marginwidth=""0"" topmargin=""0"" marginheight=""0"" offset=""0"">
		        <center style=""background-color:#E1E1E1;"">
			        <table border=""0"" cellpadding=""0"" cellspacing=""0"" height=""100%"" width=""100%"" id=""bodyTable"" style=""table-layout: fixed;max-width:100% !important;width: 100% !important;min-width: 100% !important;"">
				        <tr>
					        <td align=""center"" valign=""top"" id=""bodyCell"">
						        <table bgcolor=""#FFFFFF""  border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" id=""emailBody"">
							        <tr>
								        <td align=""center"" valign=""top"">
									        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""color:#FFFFFF;"" bgcolor=""#3498db"">
										        <tr>
											        <td align=""center"" valign=""top"">
												        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
													        <tr>
														        <td align=""center"" valign=""top"" width=""500"" class=""flexibleContainerCell"">
															        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""100%"">
																        <tr>
																	        <td align=""center"" valign=""top"" class=""textContent"">
																		        <h1 style=""color:#FFFFFF;line-height:100%;font-family:Helvetica,Arial,sans-serif;font-size:35px;font-weight:normal;margin-bottom:5px;text-align:center;"">Colegio Ofelia Narváez Rincón</h1>
																		        <h2 style=""text-align:center;font-weight:normal;font-family:Helvetica,Arial,sans-serif;font-size:23px;margin-bottom:10px;color:#205478;line-height:135%;"">Colegio Ofelia Narváez Rincón, A.C.</h2>
																		        <div style=""text-align:center;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#FFFFFF;line-height:135%;"">Registro</div>
																	        </td>
																        </tr>
															        </table>
														        </td>
													        </tr>
												        </table>
											        </td>
										        </tr>
									        </table>
								        </td>
							        </tr>
							        <tr mc:hideable>
								        <td align=""center"" valign=""top"">
									        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
										        <tr>
											        <td align=""center"" valign=""top"">
												        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
													        <tr>
														        <td valign=""top"" width=""500"" class=""flexibleContainerCell"">
															        <table align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
																        <tr>
																	        <td align=""left"" valign=""top"" class=""flexibleContainerBox"">
																		        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""210"" style=""max-width: 100%;"">
																			        <tr>
																				        <td align=""left"" class=""textContent"">
																					        <h3 style=""color:#5F5F5F;line-height:125%;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;margin-top:0;margin-bottom:3px;text-align:left;"">Usuario:</h3>
																					        <div style=""text-align:left;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#5F5F5F;line-height:135%;"">" + "" + @"</div>
																				        </td>
																			        </tr>
																		        </table>
																	        </td>
																	        <td align=""right"" valign=""middle"" class=""flexibleContainerBox"">
																		        <table class=""flexibleContainerBoxNext"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""210"" style=""max-width: 100%;"">
																			        <tr>
																				        <td align=""left"" class=""textContent"">
																					        <h3 style=""color:#5F5F5F;line-height:125%;font-family:Helvetica,Arial,sans-serif;font-size:20px;font-weight:normal;margin-top:0;margin-bottom:3px;text-align:left;"">Contraseña:</h3>
																					        <div style=""text-align:left;font-family:Helvetica,Arial,sans-serif;font-size:15px;margin-bottom:0;color:#5F5F5F;line-height:135%;"">" + "" + @"</div>
																				        </td>
																			        </tr>
																		        </table>
																	        </td>
																        </tr>
															        </table>
															        <br><br>
														        </td>
													        </tr>
												        </table>
											        </td>
										        </tr>
									        </table>
								        </td>
							        </tr>
							        <tr>
								        <td align=""center"" valign=""top"">
									        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
										        <tr style=""padding-top:0;"">
											        <td align=""center"" valign=""top"">
												        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
													        <tr>
														        <td style=""padding-top:0;"" align=""center"" valign=""top"" width=""500"" class=""flexibleContainerCell"">
															        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""50%"" class=""emailButton"" style=""background-color: #b6b24c;"">
																        <tr>
																	        <td align=""center"" valign=""middle"" class=""buttonContent"" style=""padding-top:15px;padding-bottom:15px;padding-right:15px;padding-left:15px;"">
																		        <a style=""color:#FFFFFF;text-decoration:none;font-family:Helvetica,Arial,sans-serif;font-size:20px;line-height:135%;"" href=""" + dominio + link + @""" target=""_blank"">Ir</a>
																	        </td>
																        </tr>
															        </table>
														        </td>
													        </tr>
												        </table>
											        </td>
										        </tr>
									        </table>
								        </td>
							        </tr>
						        </table>
						        <table bgcolor=""#E1E1E1"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" id=""emailFooter"">
							        <tr>
								        <td align=""center"" valign=""top"">
									        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
										        <tr>
											        <td align=""center"" valign=""top"">
												        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""500"" class=""flexibleContainer"">
													        <tr>
														        <td align=""center"" valign=""top"" width=""500"" class=""flexibleContainerCell"">
															        <table border=""0"" cellpadding=""30"" cellspacing=""0"" width=""100%"">
																        <tr>
																	        <td valign=""top"" bgcolor=""#E1E1E1"">
																		        <div style=""font-family:Helvetica,Arial,sans-serif;font-size:13px;color:#828282;text-align:center;line-height:120%;"">
																			        <div>Copyright &#169; 2016 <a href=""" + dominio + @""" target=""_blank"" style=""text-decoration:none;color:#828282;""><span style=""color:#828282;"">CSL</span></a>. All&nbsp;rights&nbsp;reserved.</div>
																			        <div>Creativa Softline <a href=""" + creativa + @""" target=""""_blank"""" style=""text-decoration:none;color:#828282;""><span style=""color:#828282;"">comunicarse</span></a>.</div>
																		        </div>
																	        </td>
																        </tr>
															        </table>
														        </td>
													        </tr>
												        </table>
											        </td>
										        </tr>
									        </table>
								        </td>
							        </tr>
						        </table>
					        </td>
				        </tr>
			        </table>
		        </center>
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
                    Parametros.Append(FirstParam ? "" : "&");
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