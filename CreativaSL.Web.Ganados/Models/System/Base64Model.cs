using System.Drawing;

namespace CreativaSL.Web.Ganados.Models.System
{
    public class Base64Model
    {
        public bool Success { get; set; }
        public string StringBase64 { get; set; }      
        public Bitmap BitmapBase64 { get; set; }
    }
}