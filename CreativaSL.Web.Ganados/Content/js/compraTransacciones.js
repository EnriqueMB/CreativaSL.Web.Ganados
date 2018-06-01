var CompraTransaccion = function () {
    "use strict"
    //datatables
    var tblDocumentoPorCobrarDetalles, tbl_articulosServiciosCobro, tbl_documentosPorCobrarDetallesPagos;
    //otros
    var IDCompra = $("#IDCompra").val();
    var Id_documentoPorCobrar = $("#Id_documentoPorCobrar").val();
    //1 por ser una compra
    var TipoServicio = 1;

    /*INICIA COBROS*/
    var Load_tbl_articulosServiciosCobro = function () {
        tbl_articulosServiciosCobro = $('#tbl_articulosServiciosCobro').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id_documentoCobrar": Id_documentoPorCobrar
                },
                "url": "/Admin/DocumentoXCobrar/JsonDocumentosDetallesCompra/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "asignadoA" },
                { "data": "descripcion" },
                { "data": "cantidad" },
                {
                    "data": "precioUnitario",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
                {
                    "data": "subtotal",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
                {
                    "data": null,
                    "render": function (data, type, full) {

                        return "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='btn btn-yellow tooltips btn-sm editDocumento' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                            "<a data-hrefa='/Admin/DocumentoXCobrar/DEL_DocumentoDetalleCompra/' title='Eliminar' data-id='" + full["id_detalleDoctoCobrar"] + "' class='btn btn-danger tooltips btn-sm deleteDocumento' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
                            "</div>" +
                            "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                            "<div class='btn-group'>" +
                            "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                            "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                            "</a>" +
                            "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                            "<li>" +
                            "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='editDocumento' role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
                            "</a>" +
                            "</li>" +
                            "<li>" +
                            "<a data-hrefa='/Admin/DocumentoXCobrar/DEL_DocumentoDetalleCompra/' class='deleteDocumento' role='menuitem' tabindex='-1' data-id='" + full["id_detalleDoctoCobrar"] + "'>" +
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
                $(".editDocumento").on("click", function () {
                    var IDDocumento = $(this).data("id");

                    ModalDocumento(IDFlete, IDDocumento);
                });
                $(".deleteDocumento").on("click", function () {
                    var url = $(this).attr('data-hrefa');
                    var row = $(this).attr('data-id');
                    var box = $("#mb-deleteDocumento");
                    box.addClass("open");
                    box.find(".mb-control-yes").on("click", function () {
                        box.removeClass("open");
                        $.ajax({
                            url: url,
                            data: { IDDocumento: row },
                            type: 'POST',
                            dataType: 'json',
                            success: function (result) {
                                if (result.Success) {
                                    box.find(".mb-control-yes").prop('onclick', null).off('click');
                                    Mensaje("Documento eliminado con éxito.", "1");
                                    $("#ModalDocumento").modal('hide');
                                    tableDocumentos.ajax.reload();
                                }
                                else
                                    Mensaje(result.Mensaje, "2");
                            },
                            error: function (result) {
                                Mensaje(result.Mensaje, "2");
                            }
                        });
                    });
                });
            }
        });
    };
    var Load_tbl_documentosPorCobrarDetallesPagos = function () {
        tbl_documentosPorCobrarDetallesPagos = $('#tbl_documentosPorCobrarDetallesPagos').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id_documentoPorCobrar": Id_documentoPorCobrar , "TipoServicio" : TipoServicio
                },
                "url": "/Admin/DocumentoXCobrar/JsonDocumentosDetallesCompraPagos/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "asignadoA" },
                { "data": "descripcion" },
                {
                    "data": "monto",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
                {
                    "data": "fecha",
                    "render": function (data, type, row) {
                        if (data === null)
                            fecha = "Sin fecha";
                        else {
                            var dateSplit = data.split('-');
                            var dia = dateSplit[2];
                            dia = dia.split('T');
                            dia = dia[0];
                            var mes = dateSplit[1];
                            var año = dateSplit[0];
                            var fecha = dia + '-' + mes + '-' + año;
                        }

                        return type === "display" || type === "filter" ?
                            fecha :
                            data;
                    }
                },
                {
                    "data": null,
                    "render": function (data, type, full) {

                        return "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" + full["id_documentoPorCobrarDetallePagos"] + "' class='btn btn-yellow tooltips btn-sm editCobro' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                            "<a data-hrefa='/Admin/DocumentoXCobrar/DEL_DocumentoDetalleCompra/' title='Eliminar' data-id='" + full["id_documentoPorCobrarDetallePagos"] + "' class='btn btn-danger tooltips btn-sm deleteDocumento' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
                            "</div>" +
                            "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                            "<div class='btn-group'>" +
                            "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                            "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                            "</a>" +
                            "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                            "<li>" +
                            "<a data-id='" + full["id_documentoPorCobrarDetallePagos"] + "' class='editCobro' role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
                            "</a>" +
                            "</li>" +
                            "<li>" +
                            "<a data-hrefa='/Admin/DocumentoXCobrar/DEL_DocumentoDetalleCompra/' class='deleteDocumento' role='menuitem' tabindex='-1' data-id='" + full["id_documentoPorCobrarDetallePagos"] + "'>" +
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
                $(".editCobro").on("click", function () {
                    var Id_documentoPorCobrar = $(this).data("id");
                    window.location.href = '/Admin/DocumentoXCobrar/EditComprobante?Id_documentoPorCobrarDetallePagos=' + Id_documentoPorCobrar + '&TipoServicio=' + TipoServicio;
                });
                $(".deleteDocumento").on("click", function () {
                    var url = $(this).attr('data-hrefa');
                    var row = $(this).attr('data-id');
                    var box = $("#mb-deleteDocumento");
                    box.addClass("open");
                    box.find(".mb-control-yes").on("click", function () {
                        box.removeClass("open");
                        $.ajax({
                            url: url,
                            data: { IDDocumento: row },
                            type: 'POST',
                            dataType: 'json',
                            success: function (result) {
                                if (result.Success) {
                                    box.find(".mb-control-yes").prop('onclick', null).off('click');
                                    Mensaje("Documento eliminado con éxito.", "1");
                                    $("#ModalDocumento").modal('hide');
                                    tableDocumentos.ajax.reload();
                                }
                                else
                                    Mensaje(result.Mensaje, "2");
                            },
                            error: function (result) {
                                Mensaje(result.Mensaje, "2");
                            }
                        });
                    });
                });
            }
        });
    };

    var EventosCobro = function () {
        $("#btnAddCobroArticuloServicio").on("click", function () {
           
        });
        $("#btnAddCobroComprobante").on("click", function () {
            window.location.href = '/Admin/DocumentoXCobrar/AddComprobante?Id_documentoPorCobrar=' + Id_documentoPorCobrar + '&TipoServicio=' + TipoServicio;
        });
    }

    /*TERMINA COBROS*/
    return {
        init: function () {
            Load_tbl_articulosServiciosCobro();
            EventosCobro();
            Load_tbl_documentosPorCobrarDetallesPagos();
        }
    };
}();