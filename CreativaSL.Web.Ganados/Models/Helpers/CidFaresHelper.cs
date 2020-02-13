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
        public static void DeleteFilesWithOutExtensionFromServer(UploadFileToServerModel uploadImageToServer)
        {
            try
            {
                var path = HostingEnvironment.MapPath(uploadImageToServer.BaseDir);
                var files = Directory.GetFiles(path, uploadImageToServer.FileName + ".*");
                foreach (var f in files)
                {
                    File.Delete(f);
                }
                uploadImageToServer.Success = true;
            }
            catch (Exception ex)
            {
                uploadImageToServer.Exception = ex;
                uploadImageToServer.Success = false;
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

                    uploadImageToServer.UrlComplete = SaveFileToServer(uploadImageToServer.FileBase, uploadImageToServer.BaseDir,
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
            for(var x = 0; x < folders.Length; x++)
            {
                var currentFolder = string.Empty;
                for (var y = x; y < folders.Length; y++)
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

        private static string SaveFileToServer(HttpPostedFileBase fileBase, string baseDir, string fileName, int calidad)
        {
            if (fileBase == null || fileBase.ContentLength == 0)
            {
                throw new Exception("File base null or empty");
            }

            if (!CreateFolder(baseDir))
            {
                throw new Exception("Directorio no válido.");
            }

            var urlComplete = baseDir + fileName;
            var stream = fileBase.InputStream;

            if (IsFileImage(fileBase.FileName))
            {
                if (Path.GetExtension(fileBase.FileName)?.ToLower() == ".heic")
                {
                    var img = (Image)Auxiliar.ProcessFile(stream);
                    var bmp = new Bitmap(VaryQualityLevel((Image)img.Clone(), calidad));
                    bmp.Save(HostingEnvironment.MapPath(urlComplete));
                }
                else
                {
                    var bmp = new Bitmap(stream);
                    bmp.Save(HostingEnvironment.MapPath(urlComplete));
                }
            }
            else
            {
                fileBase.SaveAs(HostingEnvironment.MapPath(urlComplete));
            }

            return urlComplete;
        }

        private static bool IsFileImage(string fileName)
        {
            var extensionsImages = new[]
            {
                ".png", ".jpg", ".jpge", ".bmp"
            };

            return extensionsImages.Any(extensionImage => Path.GetExtension(fileName).ToLower().Equals(extensionImage));
        }
        private static Image VaryQualityLevel(Image image, long value)
        {
            using (var bmp = new Bitmap(image))
            {
                var jpgEncoder = GetEncoder(GetImageFormat(image));
                var myEncoder = Encoder.Quality;
                var myEncoderParameters = new EncoderParameters(1);

                var myEncoderParameter = new EncoderParameter(myEncoder, value);
                myEncoderParameters.Param[0] = myEncoderParameter;
                var ms = new MemoryStream();
                bmp.Save(ms, jpgEncoder, myEncoderParameters);
                return Image.FromStream(ms);
            }
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        private static ImageFormat GetImageFormat(Image img)
        {
            using (img)
            {
                return img.RawFormat;
            }
        }
    }
}