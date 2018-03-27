function LoadTableGanado(idCompra) {

    window.TableGanado = $('#GanadoXCompraGanado').DataTable({
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
                        "<a href='javascript:void(0)' onclick='ModalGanado(\"" + full["id_ganado"] +"\")' title='Editar' class='btn btn-yellow tooltips btn-sm' data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                        "<a title='Eliminar' data-id='" + full["id_ganado"] + "' class='btn btn-danger tooltips btn-sm' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
                        "</div>" +
                        "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                        "<div class='btn-group'>" +
                        "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                        "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                        "</a>" +
                        "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                        "<li>" +
                        "<a class='classname' onclick='ModalGanado(\"" + full["id_ganado"] +"\")'  role='menuitem' tabindex='-1'>" +
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
        ]
    });

    $("#btnAddGanado").on("click", function () {
        ModalGanado(0);
    });
}

function LoadTablePago(idCompra) {
    $("#btnAddPago").on("click", function () {
        ModalPago(0);
    });
}
function LoadTableVale(idProveedor) {
    $("#btnAddVale").on("click", function () {
        ModalVale(0);
    });
}
function ModalVale(idProveedor) {

    $.ajax({
        url: 'ModalPago',
        data: { idProveedor: idProveedor },
        success: function (data) {
            $('#ContenidoModalVale').html(data);
            $('#ModalVale').modal({ backdrop: 'static', keyboard: false });

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
function ModalGanado(idGanado) {
   
    $.ajax({
        url: 'ModalGanado',
        type:"POST",
        data: { idGanado: idGanado },
        success: function (data) {
            $('#ContenidoModalGanado').html(data);
            $('#ModalGanado').modal({ backdrop: 'static', keyboard: false });
            window.TableGanado.row.add({
                "id_ganado": "A004C8A0-CDF9-4FCC-B2ED-8E99CB777B1C",
                "numArete": "2332",
                "genero": "Hembra",
                "pesoInicial": "345.43",
                "pesoFinal": "1232",
                "diferenciaPeso": "5421",
                "merma": "2",
                "pesoPagado": "23",
                "precioKilo": "2212",
                "totalPagado": "23322"
            }).draw();
        }
    })
}

function ModalVale(id) {

    $.ajax({
        url: 'ModalGanado',
        data: { idGanado: id },
        success: function (data) {
            $('#ContenidoModalGanado').html(data);
            $('#ModalGanado').modal({ backdrop: 'static', keyboard: false });
            window.TableGanado.row.add({
                "id_ganado": "A004C8A0-CDF9-4FCC-B2ED-8E99CB777B1C",
                "numArete": "2332",
                "genero": "Hembra",
                "pesoInicial": "345.43",
                "pesoFinal": "1232",
                "diferenciaPeso": "5421",
                "merma": "2",
                "pesoPagado": "23",
                "precioKilo": "2212",
                "totalPagado": "23322"
            }).draw();
        }
    })
}


function isUndefined(value) {
    // Obtain `undefined` value that's
    // guaranteed to not have been re-assigned
    var undefined = void (0);
    return value === undefined;
}

// INICIA Funciones para googleMaps
function InitMap() {
    window.directionsDisplay = new google.maps.DirectionsRenderer;
    window.directionsService = new google.maps.DirectionsService;
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 10,
        center: { lat: 17.6063149, lng: -93.204288 }
    });
    window.directionsDisplay.setMap(map);

    var onChangeHandler = function () {
        CalculateAndDisplayRoute(window.directionsService, window.directionsDisplay);
    };
    document.getElementById("Trayecto.id_lugarOrigen").addEventListener('change', onChangeHandler);
    document.getElementById("Trayecto.id_lugarDestino").addEventListener('change', onChangeHandler);
}
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

    window.directionsService.route({
        origin: inicio,
        destination: final,
        travelMode: 'DRIVING'
    }, function (response, status) {
        if (status === 'OK') {
            window.directionsDisplay.setDirections(response);
        } else {
            window.alert('Directions request failed due to ' + status);
        }
    });
}
 // TERMINA Funciones para googleMaps