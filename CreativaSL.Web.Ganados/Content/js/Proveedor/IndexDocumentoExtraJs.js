var Index = function () {
    "use strict";
    var idProveedor = $("#IdProveedor").val();
    var tblIndex;

    var runElements = function () {
        
        $("#btnNuevo").on("click",
            function () {
                window.location.href = '/Admin/CatProveedor/CreateDocumentoExtra?idProveedor=' + idProveedor;
            });
    };

    var runDataTable = function() {
        tblIndex = $('#tblDocumentosExtras').DataTable({
            "processing": true,
            "serverSide": true,
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "url": "/Admin/CatProveedor/JsonIndexDocumentosExtras/",
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
                { "data": "UrlArchivo" },
                {
                    "data": null,
                    "render": function (data, type, full) {

                        return "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" +
                            full["IdDocumentacionExtra"] +
                            "' class='btn btn-yellow tooltips btn-sm editDocumento' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                            "<a data-hrefa='/Admin/Venta/DEL_Documento/' title='Eliminar' data-id='" +
                            full["IdDocumentacionExtra"] +
                            "' class='btn btn-danger tooltips btn-sm deleteDocumento' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
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
                            "<a data-hrefa='/Admin/Venta/DEL_Documento/' class='deleteDocumento' role='menuitem' tabindex='-1' data-id='" +
                            full["IdDocumentacionExtra"] +
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
                $(".editDocumento").on("click",
                    function () {
                        var IDDocumento = $(this).data("id");
                        window.location.href = '/Admin/Venta/VentaDocumento?Id_venta=' +
                            Id_venta +
                            '&IDDocumento=' +
                            IDDocumento;
                    });

                $(".deleteDocumento").on("click",
                    function () {
                        var url = $(this).attr('data-hrefa');
                        var row = $(this).attr('data-id');
                        var box = $("#mb-deleteDocumento");
                        box.addClass("open");
                        box.find(".mb-control-yes").on("click",
                            function () {
                                box.removeClass("open");
                                $.ajax({
                                    url: url,
                                    data: { IDDocumento: row, Id_servicio: Id_venta },
                                    type: 'POST',
                                    dataType: 'json',
                                    success: function (result) {
                                        if (result.Success) {
                                            box.find(".mb-control-yes").prop('onclick', null).off('click');
                                            Mensaje(result.Mensaje, "1");
                                            tblDocumentos.ajax.reload();
                                        } else
                                            location.reload();
                                    },
                                    error: function (result) {
                                        location.reload();
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
