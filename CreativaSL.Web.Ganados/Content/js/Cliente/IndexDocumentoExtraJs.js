var Index = function () {
    "use strict";
    var idCliente = $("#IdCliente").val();
    var tblIndex;

    var runElements = function () {

        $("#btnNuevo").on("click",
            function () {
                window.location.href = '/Admin/CatCliente/CreateDocumentoExtra?idCliente=' + idCliente;
            });
    };

    var runDataTable = function () {
        tblIndex = $('#tblDocumentosExtras').DataTable({
            "processing": true,
            "serverSide": true,
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "url": "/Admin/CatCliente/JsonIndexDocumentosExtras/",
                "data": {
                    "id": idCliente
                },
                "method": "POST",
                "dataType": "json",
                "dataSrc": function (json) {

                    if (json.data === null)
                        return [];
                    else
                        return json.data;
                }
            },
            "columns": [
                { "data": "NombreTipoDocumentacionExtra" },
                {
                    "data": null,
                    "render": function (data, type, row) {

                        var img =
                            '<img class="vista_previa" src = "/Content/img/GrupoOcampo.png" alt = "Logo" style = "width: 150px; height: 150px" >';
                        if (row["UrlArchivo"]) {
                            img = '<img class="vista_previa" src = "' + row["UrlArchivo"] + '" alt = "Logo" style = "width: 150px; height: 150px" >';
                        }
                        return img;
                    }
                },
                {
                    "data": null,
                    "render": function (data, type, full) {

                        return "<div class='visible-md visible-lg hidden-sm hidden-xs text-center'>" +
                            "<a href='/Admin/CatCliente/EditDocumentoExtra?idDocumentacionExtra=" +
                            full["IdDocumentacionExtra"] +
                            "&idCliente=" +
                            idCliente +
                            "' class='btn btn-yellow tooltips editDocumento' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                            "<a data-hrefa='/Admin/CatCliente/EliminarDocumentoExtra/' title='Eliminar' data-id='" +
                            full["IdDocumentacionExtra"] +
                            "' data-urlArchivo='" +
                            full["UrlArchivo"] +
                            "' class='btn btn-danger tooltips deleteDocumento' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
                            "</div>" +
                            "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                            "<div class='btn-group'>" +
                            "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                            "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                            "</a>" +
                            "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                            "<li>" +
                            "<a data-id='" +
                            full["IdDocumentacionExtra"] +
                            "' class='editDocumento' role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
                            "</a>" +
                            "</li>" +
                            "<li>" +
                            "<a data-hrefa='/Admin/CatCliente/EliminarDocumentoExtra/' class='deleteDocumento' role='menuitem' tabindex='-1' data-id='" +
                            full["IdDocumentacionExtra"] +
                            "' data-urlArchivo='" +
                            full["UrlArchivo"] +
                            "'>" +
                            "<i class='fa fa-trash-o'></i> Eliminar" +
                            "</a>" +
                            "</li>" +
                            "</ul>" +
                            "</div>" +
                            "</div>";
                    }
                }
            ],
            "drawCallback": function (settings) {

                $('img.vista_previa').on('click',
                    function () {
                        var src_image = $(this).attr('src');
                        $("#imagen_previa").attr("src", src_image);
                        $("#modalImagen").modal();
                    });

                $(".deleteDocumento").on("click",
                    function () {
                        var url = $(this).attr('data-hrefa');
                        var idDocumentacionExtra = $(this).attr('data-id');
                        var urlArchivo = $(this).attr('data-urlArchivo');

                        var box = $("#mb-deleteDocumento");
                        box.addClass("open");
                        box.find(".mb-control-yes").on("click",
                            function () {
                                box.removeClass("open");
                                $.ajax({
                                    url: url,
                                    data: { idDocumentacionExtra: idDocumentacionExtra, idCliente: idCliente, urlArchivo: urlArchivo },
                                    type: 'POST',
                                    dataType: 'json',
                                    success: function (result) {
                                        window.location.href = '/Admin/CatCliente/DocumentosExtras?id=' + idCliente;
                                    },
                                    error: function (result) {
                                        window.location.href = '/Admin/CatCliente/DocumentosExtras?id=' + idCliente;
                                    }
                                });
                            });
                    });
            }
        });
    };

    return {
        init: function () {
            runElements();
            runDataTable();
        }
    };
}();
