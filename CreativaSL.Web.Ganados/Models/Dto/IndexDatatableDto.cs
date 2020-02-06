using System.Collections.Generic;
using Newtonsoft.Json;

namespace CreativaSL.Web.Ganados.Models.Dto
{
    public class IndexDatatableDto
    {
        [JsonProperty("draw")]
        public int Draw { get; set; }
        [JsonProperty("recordsTotal")]
        public int RecordsTotal { get; set; }
        [JsonProperty("recordsFiltered")]
        public int RecordsFiltered { get; set; }

        [JsonProperty("data")]
        public List<object> Data { get; set; }
    }
}