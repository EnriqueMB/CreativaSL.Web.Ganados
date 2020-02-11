using System.Collections.Generic;
using CreativaSL.Web.Ganados.Models.Dto.CatModulos;

namespace CreativaSL.Web.Ganados.Models.Dto.CatTipoDocumentacionExtra
{
    public class CreateEditCatTipoDocumentacionExtraDto
    {
        public CatTipoDocumentacionExtraModel CatTipoDocumentacionExtra { get; set; }
        public List<SelectCatModuloDto> ListaModulosDto { get; set; }

    }
}