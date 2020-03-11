using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using CreativaSL.Web.Ganados.Models.System;

namespace CreativaSL.Web.Ganados.Models.Helpers
{
    public static class CidFaresHelper
    {
        public static void DeleteFilesWithOutExtensionFromServer(UploadFileToServerModel uploadFileToServer)
        {
            try
            {
                var path = HostingEnvironment.MapPath(uploadFileToServer.BaseDir);
                var files = Directory.GetFiles(path, uploadFileToServer.FileName + ".*");
                foreach (var f in files)
                {
                    File.Delete(f);
                }
                uploadFileToServer.Success = true;
            }
            catch (Exception ex)
            {
                uploadFileToServer.Exception = ex;
                uploadFileToServer.Success = false;
            }
        }

        public static void DeleteFileFromServer(UploadFileToServerModel uploadFileToServer)
        {
            try
            {
                var urlRelative = uploadFileToServer.BaseDir + uploadFileToServer.FileName;
                var path = HostingEnvironment.MapPath(urlRelative);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                uploadFileToServer.Success = true;
            }
            catch (Exception ex)
            {
                uploadFileToServer.Exception = ex;
                uploadFileToServer.Success = false;
            }
        }

        public static void UploadFileToServer(UploadFileToServerModel uploadImageToServer)
        {
            try
            {
                if (uploadImageToServer.FileBase != null && uploadImageToServer.FileBase.ContentLength > 0)
                {
                    uploadImageToServer.FileName =
                        GetNewImageNameFromHttpPostedFileBase(uploadImageToServer.FileBase, uploadImageToServer.FileName);

                    uploadImageToServer.UrlRelative = SaveFileToServer(uploadImageToServer.FileBase, uploadImageToServer.BaseDir,
                        uploadImageToServer.FileName, uploadImageToServer.QualityImage);
                }
                else
                {
                    throw new Exception("Verifique su archivo");
                }
                uploadImageToServer.Success = true;
            }
            catch (Exception e)
            {
                uploadImageToServer.Exception = e;
                uploadImageToServer.Success = false;
            }
        }

        public static UploadBase64ToServerModel UploadBase64ToServer(string stringBase64, string baseDir)
        {
            var uploadBase64ToServer = new UploadBase64ToServerModel
            {
                BaseDir = baseDir
                ,
                StringBase64 = stringBase64.Replace(baseDir, string.Empty)
            };

            try
            {
                GetBitmapFromBase64(uploadBase64ToServer);

                if (uploadBase64ToServer.Success)
                {
                    uploadBase64ToServer.FileName = Guid.NewGuid().ToString().ToUpper() +
                                                    ObtenerExtensionImagenBase64(uploadBase64ToServer.StringBase64);

                    uploadBase64ToServer.UrlRelative = SaveBitmapToServer(uploadBase64ToServer.BitmapBase,
                        uploadBase64ToServer.BaseDir, uploadBase64ToServer.FileName, uploadBase64ToServer.QualityImage);
                }
                else
                {
                    throw new Exception("No se pudo hacer la conversion string base 64 a bitmap.");
                }
                uploadBase64ToServer.Success = true;
            }
            catch (Exception e)
            {
                uploadBase64ToServer.Exception = e;
                uploadBase64ToServer.Success = false;
            }

            return uploadBase64ToServer;
        }

        private static void GetBitmapFromBase64(UploadBase64ToServerModel uploadBase64ToServer)
        {
            try
            {
                byte[] byteBuffer = Convert.FromBase64String(
                    uploadBase64ToServer.StringBase64);
                MemoryStream memoryStream = new MemoryStream(byteBuffer);

                memoryStream.Position = 0;

                uploadBase64ToServer.BitmapBase = new Bitmap(memoryStream);

                memoryStream.Close();
                memoryStream = null;
                byteBuffer = null;

                uploadBase64ToServer.Success = true;
            }
            catch (Exception)
            {
                uploadBase64ToServer.Success = false;
            }
        }

        private static string GetNewImageNameFromHttpPostedFileBase(HttpPostedFileBase fileBase, string fileName)
        {
            if (fileBase == null || fileBase.ContentLength == 0)
            {
                throw new Exception("File base null or empty");
            }

            if (Path.GetExtension(fileBase.FileName)?.ToLower() == ".heic")
            {
                fileName += ".png";
            }
            else
            {
                fileName += Path.GetExtension(fileBase.FileName);
            }

            return fileName;
        }

        private static bool CreateFolder(string baseDir)
        {
            if (Directory.Exists(HostingEnvironment.MapPath(baseDir)))
            {
                return true;
            }

            var folders = baseDir.Split('/');
            for (var x = 0; x < folders.Length; x++)
            {
                var currentFolder = string.Empty;
                for (var y = 0; y <= x; y++)
                {
                    currentFolder += "/" + folders[y];
                }

                var folderPath = HostingEnvironment.MapPath(currentFolder);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
            }

            return true;
        }

        private static string SaveFileToServer(HttpPostedFileBase fileBase, string baseDir, string fileName, long calidad)
        {
            if (fileBase == null || fileBase.ContentLength == 0)
            {
                throw new Exception("File base null or empty");
            }

            if (!CreateFolder(baseDir))
            {
                throw new Exception("Directorio no válido.");
            }

            var urlRelative = HostingEnvironment.MapPath(baseDir + fileName);
            var stream = fileBase.InputStream;
            Bitmap bmp = null;

            if (IsFileImage(fileBase.FileName))
            {
                if (Path.GetExtension(fileBase.FileName)?.ToLower() == ".heic")
                {
                    bmp = Auxiliar.ProcessFile(stream);
                }
                else
                {
                    var ms = new MemoryStream();
                    fileBase.InputStream.CopyTo(ms);
                    bmp = new Bitmap(ms);
                }

                VaryQualityLevel(bmp, calidad, urlRelative);
            }
            else
            {
                fileBase.SaveAs(urlRelative);
            }

            urlRelative = baseDir + fileName;

            return urlRelative;
        }

        private static string SaveBitmapToServer(Bitmap bitmap, string baseDir, string fileName, long calidad)
        {
            if (!CreateFolder(baseDir))
            {
                throw new Exception("Directorio no válido.");
            }

            var urlRelative = HostingEnvironment.MapPath(baseDir + fileName);

            VaryQualityLevel(bitmap, calidad, urlRelative);

            urlRelative = baseDir + fileName;

            return urlRelative;
        }

        private static bool IsFileImage(string fileName)
        {
            var extensionsImages = new[]
            {
                ".png", ".jpg", ".jpge", ".bmp", ".heic"
            };

            return extensionsImages.Any(extensionImage => Path.GetExtension(fileName).ToLower().Equals(extensionImage));
        }

        private static void VaryQualityLevel(Image image, long value, string url)
        {
            using (var bmp = new Bitmap(image))
            {
                ImageCodecInfo jpgEncoder = null;

                var imageQualitysParameter =
                    new EncoderParameter(Encoder.Quality, value);

                var codecParameter = new EncoderParameters(1);
                codecParameter.Param[0] = imageQualitysParameter;

                var allCodecs = ImageCodecInfo.GetImageEncoders();
                for (int i = 0; i < allCodecs.Length; i++)
                {
                    if (allCodecs[i].MimeType == "image/jpeg")
                    {
                        jpgEncoder = allCodecs[i];
                        break;
                    }
                }

                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                    g.DrawImage(image, 0, 0, image.Width, image.Height);
                }

                bmp.Save(url, jpgEncoder, codecParameter);
            }
        }

        private static string ObtenerExtensionImagenBase64(string imagen64)
        {
            var extension = string.Empty;
            var position = 0;

            position = imagen64.IndexOf("iVBOR");
            if (position == 0)
                return extension = ".png";

            position = imagen64.IndexOf("/9j/4");
            if (position == 0)
                return extension = ".jpeg";

            //bmp de 256 colores
            position = imagen64.IndexOf("Qk3");
            if (position == 0)
                return extension = ".bmp";

            //bmp de monocromatico colores
            position = imagen64.IndexOf("Qk2");
            if (position == 0)
                return extension = ".bmp";

            //bmp de 16 colores
            position = imagen64.IndexOf("Qk1");
            if (position == 0)
                return extension = ".bmp";

            return extension;
        }
    }
}