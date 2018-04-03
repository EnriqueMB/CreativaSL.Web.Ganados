var Compra = function () {
    var tableGanado;
    "use strict"

    var InitMap = function () {
        var directionsDisplay = new google.maps.DirectionsRenderer;
        var directionsService = new google.maps.DirectionsService;
        var map = new google.maps.Map(document.getElementById('map'), {
            zoom: 10,
            center: { lat: 17.6063149, lng: -93.204288 }
        });
        directionsDisplay.setMap(map);

        var onChangeHandler = function () {
            CalculateAndDisplayRoute(directionsService, directionsDisplay);
        };
        document.getElementById("Trayecto.id_lugarOrigen").addEventListener('change', onChangeHandler);
        document.getElementById("Trayecto.id_lugarDestino").addEventListener('change', onChangeHandler);

       //Inicializo la función
       CalculateAndDisplayRoute(directionsService, directionsDisplay);
    };
    var LoadTableGanado = function (idCompra) {

        tableGanado = $('#GanadoXCompraGanado').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "IDCompra": idCompra
                },
                "url": "TableJsonGanado",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "numArete" },
                { "data": "genero" },
                { "data": "pesoInicial" },
                { "data": "pesoFinal" },
                { "data": "diferenciaPeso" },
                { "data": "merma" },
                { "data": "pesoPagado" },
                { "data": "precioKilo" },
                { "data": "totalPagado" },
                {
                    "data": null,
                    "render": function (data, type, full) {

                        return "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" + full["id_ganado"] + "' class='btn btn-yellow tooltips btn-sm editGanado' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                            "<a title='Eliminar' data-id='" + full["id_ganado"] + "' class='btn btn-danger tooltips btn-sm' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
                            "</div>" +
                            "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                            "<div class='btn-group'>" +
                            "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                            "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                            "</a>" +
                            "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                            "<li>" +
                            "<a class='deleteGanado' data-id='" + full["id_ganado"] + "'  role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
                            "</a>" +
                            "</li>" +
                            "<li>" +
                            "<a role='menuitem' tabindex='-1' id='" + full["id_ganado"] + "'>" +
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
                $(".editGanado").on("click", function () {
                    var idGanado = $(this).data("id")
                    ModalGanado(idGanado);
                });
                $(".deleteCuentaBancaria").on("click", function () {
                    var url = $(this).attr('data-hrefa');
                    var row = $(this).attr('data-id');
                    var box = $("#mb-remove-row");
                    box.addClass("open");
                    box.find(".mb-control-yes").on("click", function () {
                        box.removeClass("open");
                        $.ajax({
                            url: url,
                            type: 'POST',
                            dataType: 'json',
                            success: function (result) {
                                if (result.Success) {
                                    box.find(".mb-control-yes").prop('onclick', null).off('click');
                                    Mensaje(result.Mensaje, "1");
                                    TblCuentasBancarias.ajax.reload();
                                }
                                else
                                    Mensaje(result.Mensaje, "2");
                            },
                            error: function () {
                                Mensaje(result.Mensaje, "2");
                            }
                        });
                    });
                });
            }
        });

        $("#btnAddGanado").on("click", function () {
            ModalGanado(0);
        });
    }
    var LoadTableMovimientos = function(idCompra) {
        $("#btnAddPago").on("click", function () {
            ModalPago(0);
        });
        $("#btnAddCobro").on("click", function () {
            ModalCobro(0);
        });
    }
    var LoadTableEvento = function (idCompra) {
        $("#btnAddEvento").on("click", function () {
            ModalEvento(0);
        });
    }
    //Funciones
    function CalculateAndDisplayRoute(directionsService, directionsDisplay) {
        var selectIndexInicio = document.getElementById('Trayecto.id_lugarOrigen').selectedIndex;
        var optionInicio = document.getElementById('Trayecto.id_lugarOrigen').options.item(selectIndexInicio);
        var latitudInicia = optionInicio.dataset.latitud.replace(",", ".");
        var longInicial = optionInicio.dataset.longitud.replace(",", ".");

        var selectIndexFinal = document.getElementById('Trayecto.id_lugarDestino').selectedIndex;
        var optionFinal = document.getElementById('Trayecto.id_lugarDestino').options.item(selectIndexFinal);
        var latitudFinal = optionFinal.dataset.latitud.replace(",", ".");
        var longFinal = optionFinal.dataset.longitud.replace(",", ".");

        var inicio = new google.maps.LatLng(latitudInicia, longInicial);
        var final = new google.maps.LatLng(latitudFinal, longFinal);

        directionsService.route({
            origin: inicio,
            destination: final,
            travelMode: 'DRIVING'
        }, function (response, status) {
            if (status === 'OK') {
                directionsDisplay.setDirections(response);
            } else {
                window.alert('Directions request failed due to ' + status);
            }
        });
    }
    function ModalGanado(idGanado) {
        $.ajax({
            url: 'ModalGanado',
            type: "POST",
            data: { idGanado: idGanado },
            success: function (data) {
                $('#ContenidoModalGanado').html(data);
                $('#ModalGanado').modal({ backdrop: 'static', keyboard: false });
                
                $('#Ganado_Repeso').change(function () {
                    $('.Esconder').toggle(1000);
                });

                //$("input").keyup(function () {
                //    var value = $(this).val();
                //    $("p").text(value);
                //}).keyup();

                //for (var i in json) {
                //    for (var j in json[i]) {
                //        if (json[i][j].hasOwnProperty('check')) {
                //            json[i][j].check = true;
                //        }
                //    }
                //}
            }
        });
    }
    function ModalCobro(idDocCobrar) {
        $.ajax({
            url: "ModalCobro/",
            type: "POST",
            data: { idDocCobrar: idDocCobrar },
            success: function (data) {
                $("#ContenidoModalCobro").html(data);
                $("#ModalCobro").modal({ backdrop: "static", keyboard: false });

            }
        })
    }
    function ModalPago(idCompra) {
        $.ajax({
            url: "ModalPago",
            type: "POST",
            data: { idCompra: idCompra },
            success: function (data) {
                $("#ContenidoModalPago").html(data);
                $("#ModalPago").modal({ backdrop: "static", keyboard: false });

            }
        })
    }
    function ModalEvento(idCompra) {
        $.ajax({
            url: "ModalEvento/",
            type: "POST",
            data: { idCompra: idCompra },
            success: function (data) {
                $("#ContenidoModalPago").html(data);
                $("#ModalPago").modal({ backdrop: "static", keyboard: false });

            }
        })
    }

    return {
        init: function (idCompra) {
            InitMap();
            LoadTableGanado(idCompra);
            LoadTableMovimientos(idCompra);
            LoadTableEvento(idCompra);
        }
    };
}();


function isUndefined(value) {
    // Obtain `undefined` value that's
    // guaranteed to not have been re-assigned
    var undefined = void (0);
    return value === undefined;
}

