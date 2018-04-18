var Flete = function () {
    "use strict"

    var InitMap = function (option) {
        if (option == 2)
        {
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
            document.getElementById("LugarOrigen_id_lugar").addEventListener('change', onChangeHandler);
            document.getElementById("LugarDestino_id_lugar").addEventListener('change', onChangeHandler);

            CalculateAndDisplayRoute(directionsService, directionsDisplay);
        }
    };
    //Eventos
    var RunEventsLineaFletera = function () {
        $("#Empresa_IDEmpresa").on("change", function () {
            var IDEmpresa = $(this).val();
            GetChoferesXIDEmpresa(IDEmpresa);
            GetVehiculosXIDEmpresa(IDEmpresa);
            GetJaulasXIDEmpresa(IDEmpresa);
            GetRemolquesXIDEmpresa(IDEmpresa);
        });
        $("#Cliente_IDCliente").on("change", function () {
            var rfc = $(this).find(":selected").data("rfc");
            $("#Cliente_RFC").val(rfc);
        });
        $("#Remitente_IDCliente").on("change", function () {
            var IDRemitente = $(this).val();
            GetLugarXIDRemitente(IDRemitente);
            $("#LugarOrigen_Direccion").val('');
            $("#LugarOrigen_descripcion").val('');
        });
        $("#Destinatario_IDCliente").on("change", function () {
            var IDDestino = $(this).val();
            GetLugarXIDDestino(IDDestino);
            $("#LugarDestino_Direccion").val('');
            $("#LugarDestino_descripcion").val('');
        });
        $("#LugarOrigen_id_lugar").on("change", function () {
            var direccion = $(this).find(":selected").data("direccion");
            var descripcion = $(this).find(":selected").text();
            $("#LugarOrigen_Direccion").val(direccion);
            $("#LugarOrigen_descripcion").val(descripcion);
        });
        $("#LugarDestino_id_lugar").on("change", function () {
            var direccion = $(this).find(":selected").data("direccion");
            var descripcion = $(this).find(":selected").text();
            $("#LugarDestino_Direccion").val(direccion);
            $("#LugarDestino_descripcion").val(descripcion);
        });
    }
    //Validaciones
    var LoadValidationFlete = function () {
        var form1 = $('#frmFlete');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        $('#frmFlete').validate({ // initialize the plugin
            //debug: true,
            errorElement: "li",
            errorClass: 'text-danger',
            errorLabelContainer: $("#validation_summary_flete"),
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
                IDEmpresa: {
                    required: true
                },
                IDSucursal: {
                    required: true
                },
                IDChofer: {
                    required: true,
                },
                IDVehiculo: {
                    required: true,
                },
                "Flete.kmInicialVehiculo": {
                    required: true,
                    digits: true
                },
                IDCostoFlete: {
                    min: 1
                }
            },
            messages: {
                IDEmpresa: {
                    required: "-Seleccione una línea fletera."
                },
                IDSucursal: {
                    required: "-Seleccione una sucursal."
                },
                IDChofer: {
                    required: "-Seleccione un chofer."
                },
                IDVehiculo: {
                    required: "-Seleccione un vehículo."
                },
                "Flete.kmInicialVehiculo": {
                    required: "-Ingrese el kilómetraje inicial.",
                    digits: "Ingrese un número entero mayor o igual a 0 (cero). "
                },
                IDCostoFlete: {
                    min: "-Seleccione un tipo de costo del flete."
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
                AC_Flete();
            }
        });
    };
    //Funciones
    function AC_Flete() {
        var form = $("#frmFlete")[0];
        var formData = new FormData(form);

        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/Compra/AC_Flete/',
            contentType: false,
            processData: false,
            cache: false,
            error: function (response) {
                Mensaje(response.Mensaje, "2");
            },
            success: function (response) {
                if (response.Success) {
                    Mensaje("Registro guardado con éxito.", "1");
                    $("#IDFlete").val = response.Mensaje;
                }
                else
                    Mensaje(response.Mensaje, "2");
            }
        });
    }
    function CalculateAndDisplayRoute(directionsService, directionsDisplay) {
        var selectIndexInicio = document.getElementById('LugarOrigen_id_lugar').selectedIndex;
        var optionInicio = document.getElementById('LugarOrigen_id_lugar').options.item(selectIndexInicio);
        var latitudInicial = optionInicio.dataset.latitud.replace(",", ".");
        var longInicial = optionInicio.dataset.longitud.replace(",", ".");

        var selectIndexFinal = document.getElementById('LugarDestino_id_lugar').selectedIndex;
        var optionFinal = document.getElementById('LugarDestino_id_lugar').options.item(selectIndexFinal);
        var latitudFinal = optionFinal.dataset.latitud.replace(",", ".");
        var longFinal = optionFinal.dataset.longitud.replace(",", ".");

        var inicio = new google.maps.LatLng(latitudInicial, longInicial);
        var final = new google.maps.LatLng(latitudFinal, longFinal);

        if ((latitudInicial != 0 && longInicial != 0) && (latitudFinal != 0 && longFinal != 0)) {
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
        else {

            if ((optionInicio.text != "-- Seleccione --") && (optionFinal.text != "-- Seleccione --"))
                window.alert('No se pudo cargar la ubicación, verifique sus coordenas en el catálogo de Lugares');
        }
    }
    function GetChoferesXIDEmpresa(IDEmpresa) {
        $.ajax({
            url: '/Admin/Flete/GetChoferesXIDEmpresa/',
            type: "POST",
            dataType: 'json',
            data: { IDEmpresa: IDEmpresa },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {

                $("#Chofer_IDChofer option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Chofer_IDChofer").append('<option value="' + result[i].IDChofer + '">' + result[i].Nombre + '</option>');
                }
                $('#Chofer_IDChofer.select').selectpicker('refresh');
            }
        });
    }
    function GetVehiculosXIDEmpresa(IDEmpresa) {
        $.ajax({
            url: '/Admin/Flete/GetVehiculosXIDEmpresa/',
            type: "POST",
            dataType: 'json',
            data: { IDEmpresa: IDEmpresa },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {
                $('#Vehiculo_IDVehiculo').empty();
                var optgroup = result[0].Modelo;
                var option = '<optgroup label="' + result[0].Modelo + '"><option value="' + result[0].IDVehiculo + '">' + result[0].nombreMarca + '</option>';

                for (var i = 1; i < result.length; i++) {
                    if (optgroup == result[i].Modelo) {
                        option += '<option value="' + result[i].IDVehiculo + '">' + result[i].nombreMarca + '</option>';
                    }
                    else {
                        //Cerramos el grupo
                        option += '</optgroup>';
                        //Anexamos al select
                        $("#Vehiculo_IDVehiculo").append(option);
                        //Creamos un group nuevo
                        option = '<optgroup label="' + result[i].Modelo + '"><option value="' + result[i].IDVehiculo + '">' + result[i].nombreMarca + '</option>';
                        optgroup = result[i].Modelo;
                    }
                }
                //Anexamos el último valor
                option += '</optgroup>';
                $("#Vehiculo_IDVehiculo").append(option);
                $('#Vehiculo_IDVehiculo.select').selectpicker('refresh');
            }
        });
    }
    function GetJaulasXIDEmpresa(IDEmpresa) {
        $.ajax({
            url: '/Admin/Flete/GetJaulasXIDEmpresa/',
            type: "POST",
            dataType: 'json',
            data: { IDEmpresa: IDEmpresa },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {

                $("#Jaula_IDJaula option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Jaula_IDJaula").append('<option value="' + result[i].IDJaula + '">' + result[i].Matricula + '</option>');
                }
                $('#Jaula_IDJaula.select').selectpicker('refresh');
            }
        });
    }
    function GetRemolquesXIDEmpresa(IDEmpresa) {
        $.ajax({
            url: '/Admin/Flete/GetRemolquesXIDEmpresa/',
            type: "POST",
            dataType: 'json',
            data: { IDEmpresa: IDEmpresa },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {

                $("#Remolque_IDRemolque option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Remolque_IDRemolque").append('<option value="' + result[i].IDRemolque + '">' + result[i].placa + '</option>');
                }
                $('#Remolque_IDRemolque.select').selectpicker('refresh');
            }
        });
    }
    function GetLugarXIDRemitente(IDRemitente) {
        $.ajax({
            url: '/Admin/Flete/GetLugarXIDRemitente/',
            type: "POST",
            dataType: 'json',
            data: { IDRemitente: IDRemitente },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {

                $("#LugarOrigen_id_lugar option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#LugarOrigen_id_lugar").append('<option value="' + result[i].id_lugar + '" data-latitud="' + result[i].latitud + '" data-longitud="' + result[i].longitud + '" data-direccion="' + result[i].Direccion + '">' + result[i].descripcion + '</option>');
                }
            }
        });
    }
    function GetLugarXIDDestino(IDDestino) {
        $.ajax({
            url: '/Admin/Flete/GetLugarXIDDestino/',
            type: "POST",
            dataType: 'json',
            data: { IDDestino: IDDestino },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {

                $("#LugarDestino_id_lugar option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#LugarDestino_id_lugar").append('<option value="' + result[i].id_lugar + '" data-latitud="' + result[i].latitud + '" data-longitud="' + result[i].longitud + '" data-direccion="' + result[i].Direccion + '">' + result[i].descripcion + '</option>');
                }
            }
        });
    }

    return {
        init: function (option) {
            InitMap(option);
            RunEventsLineaFletera();
            LoadValidationFlete();
        }
    };
}();