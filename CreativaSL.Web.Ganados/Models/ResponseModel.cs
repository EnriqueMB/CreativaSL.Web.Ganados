using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ResponseModel
    {
        public dynamic Result { get; set; }
        public bool Response { get; set; }
        public string Message { get; set; }
        public string Href { get; set; }
        public string Function { get; set; }

        public ResponseModel()
        {
            this.Response = false;
            this.Message = "Ha ocurrido un error.";
        }

        public void SetResponse(bool r, string m = "")
        {
            this.Response = r;
            this.Message = m;

            if (!r && m == "")
                this.Message = "Ha ocurrido un error.";
        }

    }
}