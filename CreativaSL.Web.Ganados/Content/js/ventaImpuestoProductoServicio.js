var FleteImpuestoProductoServicio = function () {
    "use strict"
    var Id1 = $("#IDModulo").val();
    var Id2 = $("#Id_detalleDoctoCobrar").val();
    var tblImpuesto;

    var RunEvents = function () {
        $("#btnAddImpuesto").on("click", function () {
            window.location.href = '/Admin/Venta/Venta_ProductoServicio?Id1=' + Id1 + '&Id2=' + Id2 + '&Id3=';
        });

        tblImpuesto = $('#tblImpuesto').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id1": Id1, "Id2": Id2
                },
                "url": "/Admin/Venta/DatatableImpuestoXIdDocDetalle/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "TipoImpuesto" },
                { "data": "Impuesto" },
                { "data": "TipoFactor" },
                {
                    "data": "base",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
                { "data": "tasaCuota" },
                {
                    "data": "importe",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
                {
                    "data": null,
                    "render": function (data, type, full) {

                        return "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-idventa='" + Id1 + "' data-id='" + full["id_documentoCobrarDetalleImpuesto"] + "' class='btn btn-yellow tooltips btn-sm editImpuesto' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                            "<a data-hrefa='/Admin/Venta/DEL_Impuesto/' title='Eliminar' data-id='" + full["id_documentoCobrarDetalleImpuesto"] + "' class='btn btn-danger tooltips btn-sm deleteImpuesto' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
                            "</div>" +
                            "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                            "<div class='btn-group'>" +
                            "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                            "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                            "</a>" +
                            "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                            "<li>" +
                            "<a data-idventa='" + Id1 + "' class='editImpuesto' data-id='" + full["id_documentoCobrarDetalleImpuesto"] + "'  role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
                            "</a>" +
                            "</li>" +
                            "<li>" +
                            "<a data-hrefa='/Admin/Venta/DEL_Impuesto/' class='deleteImpuesto' role='menuitem' tabindex='-1' data-id='" + full["id_documentoCobrarDetalleImpuesto"] + "'>" +
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
                $(".editImpuesto").on("click", function () {
                    var IDImpuestoProducto = $(this).data("id");

                    window.location.href = '/Admin/Venta/Venta_ProductoServicio?Id1=' + Id1 + '&Id2=' + Id2 + '&Id3=' + IDImpuestoProducto;
                });

                $(".deleteImpuesto").on("click", function () {
                    var url = $(this).attr('data-hrefa');
                    var IDImpuesto = $(this).attr('data-id');
                    var box = $("#mb-delete-impuesto");
                    box.addClass("open");
                    box.find(".mb-control-yes").on("click", function () {
                        console.log("ajax");
                        box.removeClass("open");
                        $.ajax({
                            url: url,
                            data: { IDImpuesto: IDImpuesto, IDModulo: Id1 },
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

    }

    return {
        init: function () {
            RunEvents();
        }
    };
}();