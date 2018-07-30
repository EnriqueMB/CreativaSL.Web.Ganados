var VentaTransaccion = function () {
    "use strict"
    //datatables
    var tbl_detallesDocumentoPorCobrar, tbl_detallesDocumentoPorCobrarCobros;
        
    //otros
    var Id_venta = $("#Id_venta").val();
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
                    "Id_documentoCobrar": Id_documentoPorCobrar, "Id_venta": Id_venta
                },
                "url": "/Admin/Venta/DatatableDetallesDocumentoPorCobrarVenta/",
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
                    "data": "total",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
                {
                    "data": null,
                    "render": function (data, type, full) {
                        var esSistema = String(full["esSistema"]);
                        var opcionSistema = "";
                        var opcionSistemaMin = "";

                        if (esSistema.localeCompare("false") == 0) {
                            opcionSistema =
                            "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='btn btn-yellow tooltips btn-sm editDetalle' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                            "<a data-hrefa='/Admin/Venta/Del_ProductoServicio/' title='Eliminar' data-id='" + full["id_detalleDoctoCobrar"] + "' class='btn btn-danger tooltips btn-sm deleteDetalle' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>";
                            opcionSistemaMin =
                            "<li>" +
                            "<a data-id='" + full["id_detalleDoctoCobrar"] + "' class='editDetalle' role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
                            "</a>" +
                            "</li>" +
                            "<li>" +
                            "<a data-hrefa='/Venta/Flete/Del_ProductoServicio/' class='deleteDetalle' role='menuitem' tabindex='-1' data-id='" + full["id_detalleDoctoCobrar"] + "'>" +
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
                    window.location.href = '/Admin/Venta/Edit_FleteProductoServicio?&Id_venta=' + Id_flete + '&Id_documentoPorCobrar=' + Id_documentoPorCobrar + '&Id_detalleDocumento=' + Id_detalleDoctoCobrar;
                });
                $(".impuestos").on("click", function () {
                    var Id_detalleDoctoCobrar = $(this).data("id");
                    window.location.href = '/Admin/Venta/VentaImpuestoProductoServicio?&Id_1=' + Id_venta + '&Id_2=' + Id_detalleDoctoCobrar;
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
                            data: { Id_detalleDoctoCobrar: id_detalle, Id_documentoCobrar: Id_documentoPorCobrar, Id_servicio: Id_venta },
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


        tbl_detallesDocumentoPorCobrarCobros = $('#tbl_detallesDocumentoPorCobrarCobros').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id_documentoCobrar": Id_documentoPorCobrar, "Id_venta": Id_venta
                },
                "url": "/Admin/Venta/DatatableDetallesDocumentoPorCobrarFleteCobros/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "formaPago" },
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
                    "data": "monto",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
                { "data": "observacion" },
                {
                    "data": null,
                    "render": function (data, type, full) {

                        var menu = "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" + full["id_documentoPorCobrarDetallePagos"] + "' class='btn btn-yellow tooltips btn-sm editCobro' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                            "<a data-hrefa='/Admin/Venta/DelComprobante/' title='Eliminar' data-id='" + full["id_documentoPorCobrarDetallePagos"] + "' class='btn btn-danger tooltips btn-sm deleteCobro' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
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
                            "<a data-hrefa='/Admin/Venta/DelComprobante/' class='deleteCobro' role='menuitem' tabindex='-1' data-id='" + full["id_documentoPorCobrarDetallePagos"] + "'>" +
                            "<i class='fa fa-trash-o'></i> Eliminar" +
                            "</a>" +
                            "</li>"+
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
                    window.location.href = '/Admin/Venta/VentaCobro?&id_1=' + Id_venta + '&id_2=' + Id_detalleDoctoCobrarPago;
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
        $("#btnDetalles").on("click", function () {
            window.location.href = '/Admin/Venta/VentaProductoServicio?Id_documentoPorCobrar=' + Id_documentoPorCobrar + '&Id_venta=' + Id_venta + '&Id_detalleDocumento=';
        });

        $("#btnAddCobro").on("click", function () {
            window.location.href = '/Admin/Venta/VentaCobro?id_1=' + Id_venta + '&id_2=';
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