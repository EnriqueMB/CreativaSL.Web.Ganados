using System.Drawing;
using System;

namespace CreativaSL.Web.Ganados.Models.System
{
    public class UploadBase64ToServerModel
    {
        public string BaseDir { get; set; }
        public Bitmap BitmapBase { get; set; }
        public string FileName { get; set; }
        public Exception Exception { get; set; }
        public bool Success { get; set; }
        public long QualityImage { get; set; } = 50L;
        public string UrlRelative { get; set; }
        public string StringBase64 { get; set; }
    }
}