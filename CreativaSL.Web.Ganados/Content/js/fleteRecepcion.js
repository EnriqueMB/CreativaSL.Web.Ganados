var FleteRecepcion = function () {
    "use strict"
    var IDFlete = document.getElementById("id_flete").value;
    var tblEventos;

    /*TABLAS*/
    var LoadTableTiposEventos = function () {
        tblEventos = $('#tblEventos').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "IDFlete": IDFlete
                },
                "url": "/Admin/Flete/TableJsonFleteEventos/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "descripcion" },
                { "data": "cantidad" },
                { "data": "lugar" },
                {
                    "data": "fechaDeteccion",
                    "render": function (data, type, row) {
                        if (data === null)
                            fecha = "Sin dato";
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
                { "data": "horaDeteccion" },
                {
                    "data": null,
                    "render": function (data, type, full) {

                        return "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" + full["id_evento"] + "' class='btn btn-yellow tooltips btn-sm edit' title='Editar'  data-placement='top' data-original-title='Editar'><i class='fa fa-edit'></i></a>" +
                            "<a data-hrefa='/Admin/Flete/DEL_Evento/' title='Eliminar' data-id='" + full["id_evento"] + "' class='btn btn-danger tooltips btn-sm delete' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
                            "</div>" +
                            "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                            "<div class='btn-group'>" +
                            "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                            "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                            "</a>" +
                            "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                            "<li>" +
                            "<a data-id='" + full["id_evento"] + "' class='edit' data-id='" + full["id_evento"] + "'  role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
                            "</a>" +
                            "</li>" +
                            "<li>" +
                            "<a data-hrefa='/Admin/Flete/DEL_Evento/' class='delete' role='menuitem' tabindex='-1' data-id='" + full["id_evento"] + "'>" +
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
                $(".edit").on("click", function () {
                    //var IDFlete = $(this).data("idflete");
                    var id_flete = IDFlete;
                    var id_evento = $(this).data("id");
                    console.log(id_flete);
                    console.log(id_evento);

                    //ModalImpuesto(IDFlete, IDFleteImpuesto);
                });
                $(".delete").on("click", function () {
                    var url = $(this).attr('data-hrefa');
                    var row = $(this).attr('data-id');
                    var box = $("#mb-remove-row");
                    box.addClass("open");
                    //box.find(".mb-control-yes").on("click", function () {
                    //    box.removeClass("open");
                    //    $.ajax({
                    //        url: url,
                    //        data: { IDFleteImpuesto: row },
                    //        type: 'POST',
                    //        dataType: 'json',
                    //        success: function (result) {
                    //            if (result.Success) {
                    //                box.find(".mb-control-yes").prop('onclick', null).off('click');
                    //                Mensaje("Impuesto eliminado con éxito.", "1");
                    //                //Recogo los valores
                    //                var json = JSON.parse(result.Mensaje);
                    //                $("#TotalFlete").val(json.totalFlete);
                    //                $("#TotalImpuestoTrasladado").val(json.totalImpuestoTrasladados);
                    //                $("#TotalImpuestoRetenido").val(json.totalImpuestoRetenido);
                    //                $("#ModalImpuesto").modal('hide');
                    //                tableImpuesto.ajax.reload();
                    //            }
                    //            else
                    //                Mensaje(result.Mensaje, "2");
                    //        },
                    //        error: function (result) {
                    //            Mensaje(result.Mensaje, "2");
                    //        }
                    //    });
                    //});
                });
            }
        });
    }   

    /*EVENTOS*/
    var RunEventsEvento = function () {
        $("#btnAddEvento").on("click", function () {
            ModalEvento(IDFlete, 0);
        });
    }
    /*FUNCIONES MODALES*/
    function ModalEvento(IDFlete, IDEvento) {
        $.ajax({
            url: '/Admin/Flete/ModalEvento/',
            type: "POST",
            data: { IDFlete: IDFlete, IDEvento: IDEvento },
            success: function (data) {
                $('#ContenidoModalEvento').html(data);
                $('#ModalEvento').modal({ backdrop: 'static', keyboard: false });
                //LoadValidationFleteImpuesto();
                //RunEventsFleteimpuesto();
            }
        });
    }
    /*FUNCIONES VALIDACIONES*/

    return {
        init: function (ID_Flete) {
            LoadTableTiposEventos();
            RunEventsEvento();
        }
    };
}();