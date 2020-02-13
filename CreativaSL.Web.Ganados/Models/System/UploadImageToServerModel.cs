using System;
using System.Web;

namespace CreativaSL.Web.Ganados.Models.System
{
    public class UploadFileToServerModel
    {
        public string BaseDir { get; set; }
        public HttpPostedFileBase FileBase { get; set; }
        public string FileName { get; set; }
        public Exception Exception { get; set; }
        public bool Success { get; set; }
        public long QualityImage { get; set; } = 50L;
        public string UrlRelative { get; set; }
    }
}