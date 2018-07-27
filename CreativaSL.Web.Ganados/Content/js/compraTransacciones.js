var CompraTransaccion = function () {
    "use strict"
    //datatables
    var tblDocumentoPorCobrarDetalles,          tbl_articulosServiciosCobro,
        tbl_documentosPorCobrarDetallesPagos,   tbl_articulosServiciosPagos;
    //otros
    var IDCompra = $("#IDCompra").val();
    var Id_documentoPorCobrar = $("#Id_documentoPorCobrar").val();
    var Id_documentoPorPagar = $("#Id_documentoPorPagar").val();
    //1 por ser una compra
    var TipoServicio = 1;
    
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
                            "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='btn btn-green tooltips btn-sm impuestos' title='Impuestos'  data-placement='top' data-original-title='Impuestos'><i class='fa fa-money'></i></a>" +
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
                            "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='impuestoDetalle' role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
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
                $(".editDetalle").on("click", function () {
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

                        return type === "display" || type === "filter" ?
                            fecha :
                            data;
                    }
                },
            ],
          
        });
    };

    var EventosCobro = function () {
        $("#btnAddCobroArticuloServicio").on("click", function () {
            window.location.href = '/Admin/Compra/ProductoServicioCompra?Id_compra=' + IDCompra + '&Id_documentoPorCobrar=' + Id_documentoPorCobrar + '&Id_detalleDocumento=';
        });
        $("#btnAddCobroComprobante").on("click", function () {
            window.location.href = '/Admin/Compra/AddComprobante?Id_documentoPorCobrar=' + Id_documentoPorCobrar + '&TipoServicio=' + TipoServicio;
        });
    }
    /*TERMINA COBROS*/

    /*INICIA PAGOS*/
    var Load_tbl_articulosServiciosPagos = function () {

        tbl_articulosServiciosPagos = $('#tbl_articulosServiciosPagos').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id_documentoPagar": Id_documentoPorPagar
                },
                "url": "/Admin/DocumentoXPagar/JsonDocumentosDetallesCompra/",
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
                            opcionSistema = "<a data-hrefa='/Admin/DocumentoXCobrar/Del_ProductoServicio/' title='Eliminar' data-id='" + full["id_detalleDoctoCobrar"] + "' class='btn btn-danger tooltips btn-sm deleteDetalle' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>";
                            opcionSistemaMin =
                            "<li>" +
                            "<a data-hrefa='/Admin/DocumentoXCobrar/Del_ProductoServicio/' class='deleteDetalle' role='menuitem' tabindex='-1' data-id='" + full["id_detalleDoctoCobrar"] + "'>" +
                            "<i class='fa fa-trash-o'></i> Eliminar" +
                            "</a>" +
                            "</li>";
                        }

                        var menu = "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='btn btn-green tooltips btn-sm impuestos' title='Impuestos'  data-placement='top' data-original-title='Impuestos'><i class='fa fa-money'></i></a>" +
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
                            "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='impuestoDetalle' role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
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
                $(".editDetalle").on("click", function () {
                    var Id_detalleDoctoCobrar = $(this).data("id");
                    window.location.href = '/Admin/DocumentoXCobrar/EditProductoServicio?&Id_detalleDoctoCobrar=' + Id_detalleDoctoCobrar + '&Id_redireccionar=' + IDCompra + '&TipoServicio=' + TipoServicio;
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
                            data: { Id_detalleDoctoCobrar: id_detalle, Id_documentoCobrar: Id_documentoPorCobrar },
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
    /*TERMINA PAGOS*/
    return {
        init: function () {
            Load_tbl_articulosServiciosCobro();
            EventosCobro();
            Load_tbl_documentosPorCobrarDetallesPagos();
        }
    };
}();