using Newtonsoft.Json;
using System;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using System.Collections.Generic;

namespace CreativaSL.Web.Ganados.Models
{
    public static class Auxiliar
    {
        public static string ToJSON(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }
        public static string ToJSON(this object obj, int recursionDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer
            {
                RecursionLimit = recursionDepth
            };
            return serializer.Serialize(obj);
        }
        public static string ImageToBase64(HttpPostedFileBase image)
        {
            var fileContent = image;
            string base64 = null;
            if (fileContent != null && fileContent.ContentLength > 0)
            {
                //Obtengo el stream
                var stream = fileContent.InputStream;
                //Genero un array de bytes
                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(stream))
                {
                    fileData = binaryReader.ReadBytes(fileContent.ContentLength);
                }
                //Realizo la convercion a base 64
                return base64 = Convert.ToBase64String(fileData);
            }
            else
                return string.Empty;
        }
        public static string SetDefaultImage()
        {
            string image = "iVBORw0KGgoAAAANSUhEUgAAAQYAAADCCAYAAACrO2wqAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyJpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMy1jMDExIDY2LjE0NTY2MSwgMjAxMi8wMi8wNi0xNDo1NjoyNyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNiAoV2luZG93cykiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6MjQ4RDAyOTMxODREMTFFNzhGNDlDRTgwOURCQURFMDMiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6MjQ4RDAyOTQxODREMTFFNzhGNDlDRTgwOURCQURFMDMiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDoyNDhEMDI5MTE4NEQxMUU3OEY0OUNFODA5REJBREUwMyIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDoyNDhEMDI5MjE4NEQxMUU3OEY0OUNFODA5REJBREUwMyIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/PpJ5JNAAABOeSURBVHja7J0JlGRV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";
            return image;
        }
        public static string SqlReaderToJson(SqlDataReader rdr)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter jsonWriter = new JsonTextWriter(sw))
            {
                jsonWriter.WriteStartArray();

                while (rdr.Read())
                {
                    jsonWriter.WriteStartObject();

                    int fields = rdr.FieldCount;

                    for (int i = 0; i < fields; i++)
                    {
                        jsonWriter.WritePropertyName(rdr.GetName(i));
                        jsonWriter.WriteValue(rdr[i]);
                    }

                    jsonWriter.WriteEndObject();
                }

                jsonWriter.WriteEndArray();

                return sw.ToString();
            }
        }
        public static string ObtenerExtensionImagenBase64(string imagen64)
        {
            string extension = string.Empty;
            int position = 0;

            position = imagen64.IndexOf("iVBOR");
            if (position == 0)
                return extension = "image/png";

            position = imagen64.IndexOf("/9j/4");
            if (position == 0)
                return extension = "image/jpeg";

            //bmp de 256 colores
            position = imagen64.IndexOf("Qk3");
            if (position == 0)
                return extension = "image/bmp";

            //bmp de monocromatico colores
            position = imagen64.IndexOf("Qk2");
            if (position == 0)
                return extension = "image/bmp";

            //bmp de 16 colores
            position = imagen64.IndexOf("Qk1");
            if (position == 0)
                return extension = "image/bmp";

            return extension;
        }
        public static IEnumerable<ErrorModelState> AllErrors(this ModelStateDictionary modelState)
        {
            var result = from ms in modelState
                         where ms.Value.Errors.Any()
                         let fieldKey = ms.Key
                         let errors = ms.Value.Errors
                         from error in errors
                         select new ErrorModelState(fieldKey, error.ErrorMessage);

            return result;
        }
    }
}
