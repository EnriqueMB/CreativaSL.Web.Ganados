namespace CreativaSL.Web.Ganados.Models.Dto.Configuracion
{
    public class RegistrosDeTablaBase64ToUrlDto
    {
        private string _id;
        private string _imagen;

        public string Id
        {
            get { return _id; }
            set { _id = value.Trim(); }
        }

        public string Imagen
        {
            get { return _imagen; }
            set { _imagen = value.Trim(); }
        }
    }
}