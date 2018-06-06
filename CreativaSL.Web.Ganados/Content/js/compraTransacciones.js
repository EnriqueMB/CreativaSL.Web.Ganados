var CompraTransaccion = function () {
    "use strict"
    //datatables
    var tblDocumentoPorCobrarDetalles,
        tbl_articulosServiciosCobro, tbl_articulosServiciosCobroFlete,
        tbl_documentosPorCobrarDetallesPagos, tbl_documentosPorCobrarDetallesPagosFlete;
    //otros
    var IDCompra = $("#IDCompra").val();
    var Id_documentoPorCobrar = $("#Id_documentoPorCobrar").val();
    var Id_documentoPorCobrarFlete = $("#DocumentoPorCobrar_DocumentoPorCobraFlete_Id_documentoCobrar").val();
    //1 por ser una compra
    //2 flete
    var TipoServicio = 1;
    var TipoServicioFlete = 2;

    /*INICIA COBROS*/
    var Load_tbl_articulosServiciosCobro = function () {
        function format(d) {
            return '<table class="table" cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
                '<tr>' +
                '<td>Impuesto retenido:</td>' +
                '<td> $' + d.impuestos_retenidos.toFixed(2) + '</td>' +
                '</tr>' +
                '<tr>' +
                '<td>Impuesto trasladado:</td>' +
                '<td> $' + d.impuestos_trasladados.toFixed(2) + '</td>' +
                '</tr>' +
                '</table>';
        }

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
                {
                    "class": "details-control",
                    "orderable": false,
                    "data": null,
                    "defaultContent": "",
                    "width": "5%"
                },
                { "data": "descripcion" },
                { "data": "cantidad" },
                {
                    "data": "precioUnitario",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
                {
                    "data": "impuestos",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
                {
                    "data": "subtotal",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
                {
                    "data": null,
                    "render": function (data, type, full) {
                        var opcionSistema = "";
                        if (full["esSistema"] != true) {
                            opcionSistema = "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='btn btn-yellow tooltips btn-sm editDocumento' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>";
                        }

                        var menu = "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='btn btn-yellow tooltips btn-sm impuestos' title='Impuestos'  data-placement='top' data-original-title='Impuestos'><i class='fa fa-edit'></i></a>" +
                            opcionSistema +
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
                        return menu;
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

         // Add event listener for opening and closing details
        $('#tbl_articulosServiciosCobro tbody').on('click', 'td.details-control', function () {
            var tr = $(this).closest('tr');
            var row = tbl_articulosServiciosCobro.row(tr);

            if (row.child.isShown()) {
                // This row is already open - close it
                row.child.hide();
                tr.removeClass('shown');
            }
            else {
                // Open this row
                row.child(format(row.data())).show();
                tr.addClass('shown');
            }
        });


    };
    var Load_tbl_articulosServiciosCobroFlete = function () {
        function format(d) {
            return '<table class="table" cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
                '<tr>' +
                '<td>Impuesto retenido:</td>' +
                '<td> $' + d.impuestos_retenidos.toFixed(2) + '</td>' +
                '</tr>' +
                '<tr>' +
                '<td>Impuesto trasladado:</td>' +
                '<td> $' + d.impuestos_trasladados.toFixed(2) + '</td>' +
                '</tr>' +
                '</table>';
        }

        tbl_articulosServiciosCobroFlete = $('#tbl_articulosServiciosCobroFlete').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id_documentoCobrar": Id_documentoPorCobrarFlete
                },
                "url": "/Admin/DocumentoXCobrar/JsonDocumentosDetallesCompra/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                {
                    "class": "details-control",
                    "orderable": false,
                    "data": null,
                    "defaultContent": "",
                    "width": "5%"
                },
                { "data": "descripcion" },
                { "data": "cantidad" },
                {
                    "data": "precioUnitario",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
                {
                    "data": "impuestos",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
                {
                    "data": "subtotal",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
                {
                    "data": null,
                    "render": function (data, type, full) {
                        //var opcionSistema = "";
                        //if (full["esSistema"] != true) {
                        //    opcionSistema = "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='btn btn-yellow tooltips btn-sm editDocumento' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>";
                        //}
                        var menu = "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='btn btn-yellow tooltips btn-sm editDetalle' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>"+
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
                    return menu;
                    }
                }
            ],
            "drawCallback": function (settings) {
                $(".editDetalle").on("click", function () {
                    var Id_documentoPorCobrarDetalle = $(this).data("id");
                    window.location.href = '/Admin/DocumentoXCobrar/EditProductoServicio?Id_documentoPorCobrarDetallePagos=' + Id_documentoPorCobrarDetalle + '&TipoServicio=' + TipoServicioFlete + '&Id_documentoPorCobrar=' + Id_documentoPorCobrarFlete;
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

        // Add event listener for opening and closing details
        $('#tbl_articulosServiciosCobroFlete tbody').on('click', 'td.details-control', function () {
            var tr = $(this).closest('tr');
            var row = tbl_articulosServiciosCobroFlete.row(tr);

            if (row.child.isShown()) {
                // This row is already open - close it
                row.child.hide();
                tr.removeClass('shown');
            }
            else {
                // Open this row
                row.child(format(row.data())).show();
                tr.addClass('shown');
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
                            "<a data-hrefa='/Admin/DocumentoXCobrar/DelComprobante/' title='Eliminar' data-id='" + full["id_documentoPorCobrarDetallePagos"] + "' class='btn btn-danger tooltips btn-sm delComprobante' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
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
                            "<a data-hrefa='/Admin/DocumentoXCobrar/DelComprobante/' class='delComprobante' role='menuitem' tabindex='-1' data-id='" + full["id_documentoPorCobrarDetallePagos"] + "'>" +
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
                    var Id_documentoPorCobrarDetalle = $(this).data("id");
                    window.location.href = '/Admin/DocumentoXCobrar/EditComprobante?Id_documentoPorCobrarDetallePagos=' + Id_documentoPorCobrarDetalle + '&TipoServicio=' + TipoServicio + '&Id_documentoPorCobrar=' + Id_documentoPorCobrar;
                });
                $(".delComprobante").on("click", function () {
                    var url = $(this).attr('data-hrefa');
                    var id_pago = $(this).attr('data-id');
                    var box = $("#mb-delComprobante");
                    box.addClass("open");
                    box.find(".mb-control-yes").on("click", function () {
                        box.removeClass("open");
                        $.ajax({
                            url: url,
                            data: { Id_documentoPorCobrarDetallePagos: id_pago, Id_documentoPorCobrar: Id_documentoPorCobrar },
                            type: 'POST',
                            dataType: 'json',
                            success: function (result) {
                                if (result.Success) {
                                    box.find(".mb-control-yes").prop('onclick', null).off('click');
                                    Mensaje(result.Mensaje, "1");
                                    $("#Modal").modal('hide');
                                    tbl_documentosPorCobrarDetallesPagos.ajax.reload();
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
    var Load_tbl_documentosPorCobrarDetallesPagosFlete = function () {
        tbl_documentosPorCobrarDetallesPagosFlete = $('#tbl_documentosPorCobrarDetallesPagosFlete').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id_documentoPorCobrar": Id_documentoPorCobrarFlete, "TipoServicio": TipoServicioFlete
                },
                "url": "/Admin/DocumentoXCobrar/JsonDocumentosDetallesCompraPagos/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
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
                            "<a data-id='" + full["id_documentoPorCobrarDetallePagos"] + "' class='btn btn-yellow tooltips btn-sm editCobroFlete' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                            "<a data-hrefa='/Admin/DocumentoXCobrar/DelComprobante/' title='Eliminar' data-id='" + full["id_documentoPorCobrarDetallePagos"] + "' class='btn btn-danger tooltips btn-sm delComprobanteFlete' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
                            "</div>" +
                            "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                            "<div class='btn-group'>" +
                            "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                            "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                            "</a>" +
                            "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                            "<li>" +
                            "<a data-id='" + full["id_documentoPorCobrarDetallePagos"] + "' class='editCobroFlete' role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
                            "</a>" +
                            "</li>" +
                            "<li>" +
                            "<a data-hrefa='/Admin/DocumentoXCobrar/DelComprobante/' class='delComprobanteFlete' role='menuitem' tabindex='-1' data-id='" + full["id_documentoPorCobrarDetallePagos"] + "'>" +
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
                $(".editCobroFlete").on("click", function () {
                    var Id_documentoPorCobrar = $(this).data("id");
                    window.location.href = '/Admin/DocumentoXCobrar/EditComprobante?Id_documentoPorCobrarDetallePagos=' + Id_documentoPorCobrar + '&TipoServicio=' + TipoServicioFlete + '&Id_documentoPorCobrar=' + Id_documentoPorCobrarFlete;
                });
                $(".delComprobanteFlete").on("click", function () {
                    var url = $(this).attr('data-hrefa');
                    var id_pago = $(this).attr('data-id');
                    var box = $("#mb-delComprobante");
                    console.log(url);
                    console.log(id_pago);
                    console.log(box);
                    box.addClass("open");
                    box.find(".mb-control-yes").on("click", function () {
                        box.removeClass("open");
                        $.ajax({
                            url: url,
                            data: { Id_documentoPorCobrarDetallePagos: id_pago, Id_documentoPorCobrar: Id_documentoPorCobrarFlete },
                            type: 'POST',
                            dataType: 'json',
                            success: function (result) {
                                if (result.Success) {
                                    box.find(".mb-control-yes").prop('onclick', null).off('click');
                                    Mensaje(result.Mensaje, "1");
                                    $("#Modal").modal('hide');
                                    location.reload();
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
            window.location.href = '/Admin/DocumentoXCobrar/AddProductoServicio?&TipoServicio=' + TipoServicio + '&Id_documentoPorCobrar=' + Id_documentoPorCobrar + '&Id_redireccionar=' + IDCompra;
        });
        $("#btnAddCobroComprobante").on("click", function () {
            window.location.href = '/Admin/DocumentoXCobrar/AddComprobante?Id_documentoPorCobrar=' + Id_documentoPorCobrar + '&TipoServicio=' + TipoServicio;
        });
    }
    var EventosCobroFlete = function () {
        $("#btnAddCobroArticuloServicio").on("click", function () {

        });
        $("#btnAddCobroComprobanteFlete").on("click", function () {
            window.location.href = '/Admin/DocumentoXCobrar/AddComprobante?Id_documentoPorCobrar=' + Id_documentoPorCobrarFlete + '&TipoServicio=' + TipoServicioFlete;
        });
    }


    /*TERMINA COBROS*/
    return {
        init: function () {
            Load_tbl_articulosServiciosCobro();
            Load_tbl_articulosServiciosCobroFlete();
            EventosCobro();
            EventosCobroFlete();
            Load_tbl_documentosPorCobrarDetallesPagos();
            Load_tbl_documentosPorCobrarDetallesPagosFlete();
        }
    };
}();