var FleteTransaccion = function () {
    "use strict"
    //datatables
    var tbl_detallesDocumentoPorCobrar;
        
    //otros
    var Id_flete = $("#Id_flete").val();
    var Id_documentoPorCobrar = $("#DocumentosPorCobrar_Id_documentoCobrar").val();

    /*INICIA COBROS*/
    var Load_tablas = function () {
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

        tbl_detallesDocumentoPorCobrar = $('#tbl_detallesDocumentoPorCobrar').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id_documentoCobrar": Id_documentoPorCobrar, "Id_flete": Id_flete
                },
                "url": "/Admin/Flete/DatatableDetallesDocumentoPorCobrarFlete/",
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
                        var esSistema = String(full["esSistema"]);
                        var opcionSistema = "";
                        var opcionSistemaMin = "";

                        if (esSistema.localeCompare("false") == 0) {
                            opcionSistema = "<a data-hrefa='/Admin/Flete/Del_ProductoServicio/' title='Eliminar' data-id='" + full["id_detalleDoctoCobrar"] + "' class='btn btn-danger tooltips btn-sm deleteDetalle' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>"+
                                "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='btn btn-yellow tooltips btn-sm editDetalle' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" ;
                            opcionSistemaMin =
                            "<li>" +
                            "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='editDetalle' role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
                            "</a>" +
                            "</li>" +
                            "<li>" +
                            "<a data-hrefa='/Admin/Flete/Del_ProductoServicio/' class='deleteDetalle' role='menuitem' tabindex='-1' data-id='" + full["id_detalleDoctoCobrar"] + "'>" +
                            "<i class='fa fa-trash-o'></i> Eliminar" +
                            "</a>" +
                            "</li>";
                        }

                        var menu = "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='btn btn-green tooltips btn-sm impuestos' title='Impuestos'  data-placement='top' data-original-title='Impuestos'><i class='fa fa-money'></i></a>" +

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
                            "</li>"+
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
                    window.location.href = '/Admin/Flete/EditProductoServicio?&Id_detalleDoctoCobrar=' + Id_detalleDoctoCobrar + '&Id_redireccionar=' + IDCompra + '&TipoServicio=' + TipoServicio;
                });
                $(".impuestos").on("click", function () {
                    var Id_detalleDoctoCobrar = $(this).data("id");
                    window.location.href = '/Admin/Flete/AC_FleteImpuestoProductoServicio?&Id_1=' + Id_flete + '&Id_2=' + Id_detalleDoctoCobrar;
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
                            data: { Id_detalleDoctoCobrar: id_detalle, Id_documentoCobrar: Id_documentoPorCobrar, Id_servicio: Id_flete },
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

         // Add event listener for opening and closing details
        $('#tbl_detallesDocumentoPorCobrar tbody').on('click', 'td.details-control', function () {
            var tr = $(this).closest('tr');
            var row = tbl_detallesDocumentoPorCobrar.row(tr);

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

    //var Load_tbl_documentosPorCobrarDetallesPagos = function () {
    //    tbl_documentosPorCobrarDetallesPagos = $('#tbl_documentosPorCobrarDetallesPagos').DataTable({
    //        "language": {
    //            "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
    //        },
    //        responsive: true,
    //        "ajax": {
    //            "data": {
    //                "Id_documentoPorCobrar": Id_documentoPorCobrar , "TipoServicio" : TipoServicio
    //            },
    //            "url": "/Admin/DocumentoXCobrar/JsonDocumentosDetallesCompraPagos/",
    //            "type": "POST",
    //            "datatype": "json",
    //            "dataSrc": ''
    //        },
    //        "columns": [
    //            { "data": "descripcion" },
    //            {
    //                "data": "monto",
    //                "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
    //            },
    //            {
    //                "data": "fecha",
    //                "render": function (data, type, row) {
    //                    if (data === null)
    //                        fecha = "Sin fecha";
    //                    else {
    //                        var dateSplit = data.split('-');
    //                        var dia = dateSplit[2];
    //                        dia = dia.split('T');
    //                        dia = dia[0];
    //                        var mes = dateSplit[1];
    //                        var año = dateSplit[0];
    //                        var fecha = dia + '-' + mes + '-' + año;
    //                    }

    //                    return type === "display" || type === "filter" ?
    //                        fecha :
    //                        data;
    //                }
    //            },
    //            {
    //                "data": null,
    //                "render": function (data, type, full) {

    //                    return "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
    //                        "<a data-id='" + full["id_documentoPorCobrarDetallePagos"] + "' class='btn btn-yellow tooltips btn-sm editCobro' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
    //                        "<a data-hrefa='/Admin/DocumentoXCobrar/DelComprobante/' title='Eliminar' data-id='" + full["id_documentoPorCobrarDetallePagos"] + "' class='btn btn-danger tooltips btn-sm delComprobante' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
    //                        "</div>" +
    //                        "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
    //                        "<div class='btn-group'>" +
    //                        "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
    //                        "<i class='fa fa-cog'></i> <span class='caret'></span>" +
    //                        "</a>" +
    //                        "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
    //                        "<li>" +
    //                        "<a data-id='" + full["id_documentoPorCobrarDetallePagos"] + "' class='editCobro' role='menuitem' tabindex='-1'>" +
    //                        "<i class='fa fa-edit'></i> Editar" +
    //                        "</a>" +
    //                        "</li>" +
    //                        "<li>" +
    //                        "<a data-hrefa='/Admin/DocumentoXCobrar/DelComprobante/' class='delComprobante' role='menuitem' tabindex='-1' data-id='" + full["id_documentoPorCobrarDetallePagos"] + "'>" +
    //                        "<i class='fa fa-trash-o'></i> Eliminar" +
    //                        "</a>" +
    //                        "</li>" +
    //                        "</ul>" +
    //                        "</div>" +
    //                        "</div>";
    //                }
    //            }
    //        ],
    //        "drawCallback": function (settings) {
    //            $(".editCobro").on("click", function () {
    //                var Id_documentoPorCobrarDetalle = $(this).data("id");
    //                window.location.href = '/Admin/DocumentoXCobrar/EditComprobante?Id_documentoPorCobrarDetallePagos=' + Id_documentoPorCobrarDetalle + '&TipoServicio=' + TipoServicio + '&Id_documentoPorCobrar=' + Id_documentoPorCobrar;
    //            });
    //            $(".delComprobante").on("click", function () {
    //                var url = $(this).attr('data-hrefa');
    //                var id_pago = $(this).attr('data-id');
    //                var box = $("#mb-delComprobante");
    //                box.addClass("open");
    //                box.find(".mb-control-yes").on("click", function () {
    //                    box.removeClass("open");
    //                    $.ajax({
    //                        url: url,
    //                        data: { Id_documentoPorCobrarDetallePagos: id_pago, Id_documentoPorCobrar: Id_documentoPorCobrar },
    //                        type: 'POST',
    //                        dataType: 'json',
    //                        success: function (result) {
    //                            if (result.Success) {
    //                                box.find(".mb-control-yes").prop('onclick', null).off('click');
    //                                Mensaje(result.Mensaje, "1");
    //                                $("#Modal").modal('hide');
    //                                tbl_documentosPorCobrarDetallesPagos.ajax.reload();
    //                            }
    //                            else
    //                                Mensaje(result.Mensaje, "2");
    //                        },
    //                        error: function (result) {
    //                            Mensaje(result.Mensaje, "2");
    //                        }
    //                    });
    //                });
    //            });
    //        }
    //    });
    //};
  

    var EventosCobro = function () {
        $("#btnDetalles").on("click", function () {
            window.location.href = '/Admin/Flete/AC_FleteProductoServicio?Id_documentoPorCobrar=' + Id_documentoPorCobrar + '&Id_flete=' + Id_flete + '&Id_detalleDocumento=';
        });
        $("#btnAddCobroComprobante").on("click", function () {
            window.location.href = '/Admin/Flete/AddComprobante?Id_documentoPorCobrar=' + Id_documentoPorCobrar + '&TipoServicio=' + TipoServicio;
        });
    }
    
    /*TERMINA COBROS*/

    return {
        init: function () {
            Load_tablas();
            EventosCobro();
        }
    };
}();