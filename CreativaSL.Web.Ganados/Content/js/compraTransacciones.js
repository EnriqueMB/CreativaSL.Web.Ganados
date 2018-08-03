var CompraTransaccion = function () {
    "use strict"
    //datatables
    var tblDocumentoPorCobrarDetalles,        tbl_articulosServiciosCobro,
        tbl_documentosPorCobrarDetallesPagos, tbl_articulosServiciosPagos;
    var tbl_documentoPorPagarDetalles,        tbl_documentosPorPagarDetallesPagos;
    //otros
    var IDCompra = $("#IDCompra").val();
    var Id_documentoPorCobrar = $("#Id_documentoPorCobrar").val();
    var Id_documentoPorPagar = $("#Id_documentoPorPagar").val();
    
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
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id_documentoCobrar": Id_documentoPorCobrar, "Id_servicio": IDCompra
                },
                "url": "/Admin/Compra/DatatableDocumentosPorCobrarDetalles/",
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
                        var opcionSistemaMin = "";

                        if (full["esSistema"] != true) {
                            opcionSistema = "<a data-hrefa='/Admin/Compra/Del_ProductoServicio/' title='Eliminar' data-id='" + full["id_detalleDoctoCobrar"] + "' class='btn btn-danger tooltips btn-sm deleteDetalle' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>";
                            opcionSistemaMin =
                            "<li>" +
                            "<a data-hrefa='/Admin/Compra/Del_ProductoServicio/' class='deleteDetalle' role='menuitem' tabindex='-1' data-id='" + full["id_detalleDoctoCobrar"] + "'>" +
                            "<i class='fa fa-trash-o'></i> Eliminar" +
                            "</a>" +
                            "</li>";
                        }

                        var menu = "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='btn btn-green tooltips btn-sm impuestosCobro' title='Impuestos'  data-placement='top' data-original-title='Impuestos'><i class='fa fa-money'></i></a>" +
                            "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='btn btn-yellow tooltips btn-sm editDetalle' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                            opcionSistema +
                            "</div>" +
                            "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                            "<div class='btn-group'>" +
                            "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                            "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                            "</a>" +
                            "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                            "<li>" +
                            "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='impuestosCobro' role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Impuestos" +
                            "</a>" +
                            "</li>" +
                            "<li>" +
                            "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='editDetalle' role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
                            "</a>" +
                            "</li>" +
                            opcionSistemaMin + 
                            "</ul>" +
                            "</div>" +
                            "</div>";
                        return menu;
                    }
                }
            ],
            "drawCallback": function (settings) {
                $(".impuestosCobro").on("click", function () {
                    var Id_detalleDoctoCobrar = $(this).data("id");
                    window.location.href = '/Admin/Compra/ImpuestosProductoServicioCompra?Id_1=' + IDCompra + '&Id_2=' + Id_detalleDoctoCobrar;
                });
                $(".editDetalle").on("click", function () {
                    var Id_detalleDoctoCobrar = $(this).data("id");
                    window.location.href = '/Admin/Compra/EditProductoServicioCompra?Id_compra=' + IDCompra + '&Id_documentoPorCobrar=' + Id_documentoPorCobrar + '&Id_detalleDocumento=' + Id_detalleDoctoCobrar;
                });
                $(".deleteDetalle").on("click", function () {
                    var url = $(this).attr('data-hrefa');
                    var id_detalle = $(this).attr('data-id');
                    var box = $("#mb-deleteDetalle");
                    box.addClass("open");
                    box.find(".mb-control-yes").on("click", function () {
                        box.removeClass("open");
                        $.ajax({
                            url: url,
                            data: { Id_detalleDoctoCobrar: id_detalle, Id_documentoCobrar: Id_documentoPorCobrar, Id_servicio: IDCompra },
                            type: 'POST',
                            dataType: 'json',
                            success: function (result) {
                                if (result.Success) {
                                    box.find(".mb-control-yes").prop('onclick', null).off('click');
                                }
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

    var Load_tbl_documentosPorCobrarDetallesPagos = function () {
        tbl_documentosPorCobrarDetallesPagos = $('#tbl_documentosPorCobrarDetallesPagos').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id_documentoPorCobrar": Id_documentoPorCobrar, "Id_compra": IDCompra
                },
                "url": "/Admin/Compra/DatatableDocumentosPorCobrarDetallesPagos/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "formaPago" },
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

                        return fecha;
                    }
                },
                {
                    "data": null,
                    "render": function (data, type, full) {

                        var menu = "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" + full["id_documentoPorCobrarDetallePagos"] + "' class='btn btn-yellow tooltips btn-sm editCobro' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                            "<a data-hrefa='/Admin/Compra/DelComprobante/' title='Eliminar' data-id='" + full["id_documentoPorCobrarDetallePagos"] + "' class='btn btn-danger tooltips btn-sm deleteCobro' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
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
                            "<a data-hrefa='/Admin/Compra/DelComprobante/' class='deleteCobro' role='menuitem' tabindex='-1' data-id='" + full["id_documentoPorCobrarDetallePagos"] + "'>" +
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
                $(".editCobro").on("click", function () {
                    var Id_detalleDoctoCobrarPago = $(this).data("id");
                    window.location.href = '/Admin/Compra/CobroCompra?id_1=' + IDCompra + '&id_2=' + Id_detalleDoctoCobrarPago;
                });

                $(".deleteCobro").on("click", function () {
                    var url = $(this).attr('data-hrefa');
                    var id_detalle = $(this).attr('data-id');
                    var box = $("#mb-delComprobante");
                    box.addClass("open");
                    box.find(".mb-control-yes").on("click", function () {
                        box.removeClass("open");
                        $.ajax({
                            url: url,
                            data: { Id_documentoPorCobrarDetallePagos: id_detalle, Id_documentoPorCobrar: Id_documentoPorCobrar },
                            type: 'POST',
                            dataType: 'json',
                            success: function (result) {
                                box.find(".mb-control-yes").prop('onclick', null).off('click');
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

    var EventosCobro = function () {
        $("#btnAddCobroArticuloServicio").on("click", function () {
            window.location.href = '/Admin/Compra/ProductoServicioCompra?Id_compra=' + IDCompra + '&Id_documentoPorCobrar=' + Id_documentoPorCobrar + '&Id_detalleDocumento=';
        });
        $("#btnAddCobroComprobante").on("click", function () {
            window.location.href = '/Admin/Compra/CobroCompra?id_1=' + IDCompra + '&id_2=';
        });
    }
    /*TERMINA COBROS*/

    /*INICIA PAGOS*/
    var Load_tbl_documentoPorPagarDetalles = function () {
        tbl_documentoPorPagarDetalles = $('#tbl_documentoPorPagarDetalles').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id_1": Id_documentoPorPagar, "Id_2": IDCompra
                },
                "url": "/Admin/Compra/DatatableDocumentosPorPagarDetalles/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": '',
                },
                "columns": [
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
                            var opcionSistema = "";
                            var opcionSistemaMin = "";

                            if (full["esSistema"] != true) {
                                opcionSistema = "<a data-hrefa='/Admin/Compra/Del_ProductoServicio/' title='Eliminar' data-id='" + full["id_detalleDoctoPagar"] + "' class='btn btn-danger tooltips btn-sm deleteDetalle' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>";
                                opcionSistemaMin =
                                "<li>" +
                                "<a data-hrefa='/Admin/Compra/Del_ProductoServicio/' class='deleteDetalle' role='menuitem' tabindex='-1' data-id='" + full["id_detalleDoctoPagar"] + "'>" +
                                "<i class='fa fa-trash-o'></i> Eliminar" +
                                "</a>" +
                                "</li>";
                            }

                            var menu = "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                                "<a data-id='" + full["id_detalleDoctoPagar"] + "' class='btn btn-green tooltips btn-sm impuestos' title='Impuestos'  data-placement='top' data-original-title='Impuestos'><i class='fa fa-money'></i></a>" +
                                "<a data-id='" + full["id_detalleDoctoPagar"] + "' class='btn btn-yellow tooltips btn-sm editDetalle' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                                opcionSistema +
                                "</div>" +
                                "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                                "<div class='btn-group'>" +
                                "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                                "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                                "</a>" +
                                "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                                "<li>" +
                                "<a data-id='" + full["id_detalleDoctoPagar"] + "' class='impuestoDetalle' role='menuitem' tabindex='-1'>" +
                                "<i class='fa fa-edit'></i> Editar" +
                                "</a>" +
                                "</li>" +
                                "<li>" +
                                "<a data-id='" + full["id_detalleDoctoPagar"] + "' class='editDetalle' role='menuitem' tabindex='-1'>" +
                                "<i class='fa fa-edit'></i> Editar" +
                                "</a>" +
                                "</li>" +
                                opcionSistemaMin +
                                "</ul>" +
                                "</div>" +
                                "</div>";
                            return menu;
                        }
                    }
                ],
                "drawCallback": function (settings) {
                    $(".editDetallePago").on("click", function () {
                        var Id_detalleDoctoCobrar = $(this).data("id");
                        window.location.href = '/Admin/Compra/Edit_ProductoServicio?Id_compra=' + IDCompra + '&Id_documentoPorCobrar=' + Id_documentoPorCobrar + '&Id_detalleDocumento=' + Id_detalleDoctoCobrar;
                    });
                    $(".deleteDetalle").on("click", function () {
                        var url = $(this).attr('data-hrefa');
                        var id_detalle = $(this).attr('data-id');
                        var box = $("#mb-deleteDetalle");
                        box.addClass("open");
                        box.find(".mb-control-yes").on("click", function () {
                            box.removeClass("open");
                            $.ajax({
                                url: url,
                                data: { Id_detalleDoctoCobrar: id_detalle, Id_documentoCobrar: Id_documentoPorCobrar, Id_servicio: IDCompra },
                                type: 'POST',
                                dataType: 'json',
                                success: function (result) {
                                    if (result.Success) {
                                        box.find(".mb-control-yes").prop('onclick', null).off('click');
                                    }
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
    
    var Load_tbl_documentosPorPagarDetallesPagos = function () {
        tbl_documentosPorPagarDetallesPagos = $('#tbl_documentosPorPagarDetallesPagos').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id_documentoPorPagar": Id_documentoPorPagar, "Id_compra": IDCompra
                },
                "url": "/Admin/Compra/DatatableDocumentosPorPagarDetallesPagos/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "formaPago" },
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

                        return fecha;
                    }
                },
                {
                    "data": null,
                    "render": function (data, type, full) {

                        var menu = "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" + full["id_documentoPorPagarDetallePagos"] + "' class='btn btn-yellow tooltips btn-sm editPago' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                            "<a data-hrefa='/Admin/Compra/DelComprobantePorPagar/' title='Eliminar' data-id='" + full["id_documentoPorPagarDetallePagos"] + "' class='btn btn-danger tooltips btn-sm deletePago' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
                            "</div>" +
                            "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                            "<div class='btn-group'>" +
                            "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                            "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                            "</a>" +
                            "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                            "<li>" +
                            "<a data-id='" + full["id_documentoPorPagarDetallePagos"] + "' class='editPago' role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
                            "</a>" +
                            "</li>" +
                            "<li>" +
                            "<a data-hrefa='/Admin/Compra/DelComprobantePorPagar/' class='deletePago' role='menuitem' tabindex='-1' data-id='" + full["id_documentoPorPagarDetallePagos"] + "'>" +
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
                $(".editPago").on("click", function () {
                    var Id_detalleDoctoPagarPago = $(this).data("id");
                    window.location.href = '/Admin/Compra/PagoCompra?id_1=' + IDCompra + '&id_2=' + Id_detalleDoctoPagarPago;
                });

                $(".deletePago").on("click", function () {
                    var url = $(this).attr('data-hrefa');
                    var id_detalle = $(this).attr('data-id');
                    var box = $("#mb-delComprobante");
                    box.addClass("open");
                    box.find(".mb-control-yes").on("click", function () {
                        box.removeClass("open");
                        $.ajax({
                            url: url,
                            data: { Id_documentoPorCobrarDetallePagos: id_detalle, Id_documentoPorCobrar: Id_documentoPorCobrar },
                            type: 'POST',
                            dataType: 'json',
                            success: function (result) {
                                box.find(".mb-control-yes").prop('onclick', null).off('click');
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

    var EventosPago = function () {
        $("#btnAddPagoArticuloServicio").on("click", function () {
            window.location.href = '/Admin/Compra/ProductoServicioCompraPago?Id_compra=' + IDCompra + '&Id_documentoPorCobrar=' + Id_documentoPorCobrar + '&Id_detalleDocumento=';
        });
        $("#btnAddPagoComprobantePago").on("click", function () {
            window.location.href = '/Admin/Compra/PagoCompra?id_1=' + IDCompra + '&id_2=';
        });
    }
    /*TERMINA PAGOS*/
    return {
        init: function () {
            Load_tbl_articulosServiciosCobro();
            Load_tbl_documentosPorCobrarDetallesPagos();
            EventosCobro();

            Load_tbl_documentoPorPagarDetalles();
            Load_tbl_documentosPorPagarDetallesPagos();
            EventosPago();
        }
    };
}();