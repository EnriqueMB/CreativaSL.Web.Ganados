var VentaEventoRecepcion = function () {
    "use strict"
    var Id_venta = $("#Id_venta").val();
    var Id_recepcionOrigenVenta2 =  $("#RecepcionOrigen_Id_recepcionOrigenVenta");
    var tblEventos;
   
    /*INICIA RECEPCION*/
    var initFuncionesRecepcion = function () {
        $('.Hora24hrs').timepicker({
            minuteStep: 1,
            showMeridian: false
        });

        $("#btnAddEvento").on("click", function () {
            window.location.href = '/Admin/Venta/VentaEvento?IDVenta=' + Id_venta + '&Id_eventoVenta=0';
        });

        tblEventos = $('#tblEventos').DataTable({
            "language": {
                "url": "../../Content/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id_venta": Id_venta
                },
                "url": "/Admin/Venta/DatatableEventos/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "id_eventoVenta" },
                { "data": "descripcion" },
                { "data": "cantidad" },
                { "data": "lugar" },
                { "data": "fechaDeteccion",
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
                            fecha : data;
                    }
                },
                {
                    "data": "horaDeteccion",
                    "render": function (data, type, row) {
                        if (data === null)
                            hhmm = "Sin hora";
                        else {
                            var dateSplit = data.split(':');
                            var hora = dateSplit[0];
                            var minuto = dateSplit[1];
                            var hhmm = hora + ':' + minuto
                        }

                        return type === "display" || type === "filter" ?
                            hhmm : data;
                    }
                },
                {
                    "data": null,
                    "render": function (data, type, full) {

                        return "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" + full["id_eventoVenta"] + "' class='btn btn-yellow tooltips btn-sm edit' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                            "<a data-id='" + full["id_eventoVenta"] + "' data-hrefa='/Admin/Venta/Del_Evento/' title='Eliminar'  class='btn btn-danger tooltips btn-sm delete' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
                            "</div>" +
                            "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                            "<div class='btn-group'>" +
                            "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                            "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                            "</a>" +
                            "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                            "<li>" +
                            "<a data-id='" + full["id_eventoVenta"] + "' class='edit' role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
                            "</a>" +
                            "</li>" +
                            "<li>" +
                            "<a data-id='" + full["id_eventoVenta"] + "' data-hrefa='/Admin/Venta/Del_Evento/' class='delete' role='menuitem' tabindex='-1' >" +
                            "<i class='fa fa-trash-o'></i> Eliminar" +
                            "</a>" +
                            "</li>" +
                            "</ul>" +
                            "</div>" +
                            "</div>";
                    }
                }
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ],
            "drawCallback": function (settings) {
                $(".edit").on("click", function () {
                    var Id_eventoVenta = $(this).data("id");
                    window.location.href = '/Admin/Venta/VentaEvento?IDVenta=' + Id_venta + '&Id_eventoVenta=' + Id_eventoVenta;
                });
                $(".delete").on("click", function () {
                    var url = $(this).attr('data-hrefa');
                    var Id_eventoVenta = $(this).attr('data-id');
                    var box = $("#mb-del-evento");
                    box.addClass("open");
                    box.find(".mb-control-yes").on("click", function () {
                        box.removeClass("open");
                        $.ajax({
                            url: url,
                            data: { IDVenta: Id_venta, Id_eventoVenta: Id_eventoVenta },
                            type: 'POST',
                            dataType: 'json',
                            success: function (result) {
                                if (result.Success) {
                                    box.find(".mb-control-yes").prop('onclick', null).off('click');
                                    Mensaje(json.Mensaje, "1");

                                    tblEventos.ajax.reload();
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

        $(document).on('submit', 'form#frmRecepcion', function (e) {
            console.log("entro en el submit");
            e.preventDefault();
            var form = $('form#frmRecepcion')[0];
            var formData = new FormData(form);

            $.ajax({
                type: 'POST',
                data: formData,
                url: '/Admin/Venta/VentaEventoRecepcion/',
                contentType: false,
                processData: false,
                cache: false,
                success: function (response) {
                    if (response.Success) {
                        var json = JSON.parse(response.Mensaje);
                        Mensaje(json.Mensaje, "1");
                        Id_recepcionOrigenVenta2.val(json.Id_recepcionOrigenVenta);
                    } else {
                        Mensaje(response.Mensaje, "2");
                    }
                },
                error: function (response) {
                    Mensaje(response.Mensaje, "2");
                }
            });
        });
    }
    /*TERMINA RECEPCION*/

    return {
        init: function () {
            initFuncionesRecepcion();
        }
    };
}();