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
    var LoadValidationCreate = function () {
        var form1 = $('#frmCreateCompra');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        $('#frmCreateCompra').validate({ // initialize the plugin
            //debug: true,
            errorElement: "span", // contain the error msg in a span tag
            errorClass: 'text-danger',
            errorLabelContainer: $("#validation_summary"),
            errorPlacement: function (error, element) { // render error placement for each input type
                if (element.attr("type") == "radio" || element.attr("type") == "checkbox") { // for chosen elements, need to insert the error after the chosen container
                    error.insertAfter($(element).closest('.form-group').children('div').children().last());
                } else if (element.attr("name") == "dd" || element.attr("name") == "mm" || element.attr("name") == "yyyy") {
                    error.insertAfter($(element).closest('.form-group').children('div'));
                } else if (element.attr("type") == "text") {
                    error.insertAfter($(element).closest('.input-group').children('div'));
                } else {
                    error.insertAfter(element);
                    // for other inputs, just perform default behavior
                }
            },
            ignore: "",
            rules: {
                IDProveedor: {
                    required: true
                },
                "Sucursal.IDSucursal": {
                    required: true
                },
                GanadosPactadoMachos: {
                    digits: true
                },
                GanadosPactadoHembras: {
                    digits: true
                },
                FechaHoraProgramada: {
                    required: true,
                    fecha: true
                },
                "Flete.kmInicialVehiculo": {
                    digits: true
                },
                GuiaTransito: {
                    maxlength: 15
                },
                CertZoosanitario: {
                    maxlength: 15
                },
                CertTuberculosis: {
                    maxlength: 15
                },
                CertBrucelosis: {
                    maxlength: 15
                }
            },
            messages: {
                IDProveedor: {
                    required: "-Seleccione un Proveedor"
                },
                "Sucursal.IDSucursal": {
                    required: "-Seleccione una Sucursal"
                },
                GanadosPactadoMachos: {
                    digits: "-El campo: Ganados Pactado Machos, debe ser igual o mayo que 0 (solo números enteros)."
                },
                GanadosPactadoHembras: {
                    digits: "-El campo: Ganados Pactado Hembras, debe ser igual o mayo que 0 (solo números enteros)."
                },
                FechaHoraProgramada: {
                    required: "-Seleccione una Fecha para la compra a realizar",
                    date: "-Debe ser una fecha con formado dd/mm/aaaa"
                },
                "Flete.kmInicialVehiculo": {
                    digits: "-El campo: Kilómetraje Inicial, debe ser igual o mayo que 0 (solo números enteros)."
                },
                GuiaTransito: {
                    maxlength: jQuery.validator.format("-El campo: Guía de Transito debe ser igual o menor que {0} carácteres.")
                },
                CertZoosanitario: {
                    maxlength: jQuery.validator.format("-El campo: Cert. Zoosanitario debe ser igual o menor que {0} carácteres.")
                },
                CertTuberculosis: {
                    maxlength: jQuery.validator.format("-El campo: Cert. Tuberculosis debe ser igual o menor que {0} carácteres.")
                },
                CertBrucelosis: {
                    maxlength: jQuery.validator.format("-El campo: Cert. Brucelosis debe ser igual o menor que {0} carácteres.")
                }
            },
            invalidHandler: function (event, validator) {
                successHandler1.hide();
                errorHandler1.show();
            },
            highlight: function (element) {
                $(element).closest('.help-block').removeClass('valid');
                $(element).closest('.controlError').removeClass('has-success').addClass('has-error').find('.symbol').removeClass('ok').addClass('required');
            },
            unhighlight: function (element) {
                $(element).closest('.controlError').removeClass('has-error');
            },
            success: function (label, element) {
                label.addClass('help-block valid');
                label.removeClass('color');
                $(element).closest('.controlError').removeClass('has-error').addClass('has-success').find('.symbol').removeClass('required').addClass('ok');
            },
            submitHandler: function (form) {
                successHandler1.show();
                errorHandler1.hide();
                form.submit();
            }
        });
    };
    var LoadItems = function () {
        $('#FechaHoraProgramada').datepicker({
            format: 'dd/mm/yyyy',
            language: 'es'
        });

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
    };
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
                window.alert('No se pudo cargar la ubicación, verifique sus coordenas en el catálogo de Lugares, estatus: ' + status);
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
            LoadItems();
            InitMap();
            LoadValidationCreate();
            LoadTableGanado(idCompra);
            LoadTableMovimientos(idCompra);
            LoadTableEvento(idCompra);
        }
    };
}();