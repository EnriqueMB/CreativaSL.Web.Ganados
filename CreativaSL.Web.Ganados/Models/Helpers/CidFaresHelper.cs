using System;
using System.IO;
using System.Web;
using System.Web.Hosting;
using CreativaSL.Web.Ganados.Models.System;

namespace CreativaSL.Web.Ganados.Models.Helpers
{
    public static class CidFaresHelper
    {
        public static void DeleteFilesWithOutExtensionFromServer(UploadImageToServerModel uploadImageToServer)
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
        public static void UploadImageToServer(UploadImageToServerModel uploadImageToServer)
        {
            try
            {
                if (uploadImageToServer.FileBase != null && uploadImageToServer.FileBase.ContentLength > 0)
                {
                    uploadImageToServer.FileName =
                        GetNewFileNameFromHttpPostedFileBase(uploadImageToServer.FileBase, uploadImageToServer.FileName);

                    uploadImageToServer.UrlComplete = SaveHttpPostedFileBaseToServer(uploadImageToServer.FileBase, uploadImageToServer.BaseDir,
                        uploadImageToServer.FileName);
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

        private static string GetNewFileNameFromHttpPostedFileBase(HttpPostedFileBase fileBase, string fileName)
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

        private static string SaveHttpPostedFileBaseToServer(HttpPostedFileBase fileBase, string baseDir, string fileName)
        {
            var folderPath = HostingEnvironment.MapPath(baseDir);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (fileBase == null || fileBase.ContentLength == 0)
            {
                throw new Exception("File base null or empty");
            }

            var urlComplete = baseDir + fileName;

            if (Path.GetExtension(fileBase.FileName)?.ToLower() == ".heic")
            {
                var stream = fileBase.InputStream;
                var bmp = Auxiliar.ProcessFile(stream);
                bmp.Save(HostingEnvironment.MapPath(urlComplete));
            }
            else
            {
                fileBase.SaveAs(HostingEnvironment.MapPath(urlComplete));
            }

            return urlComplete;
        }
    }
}