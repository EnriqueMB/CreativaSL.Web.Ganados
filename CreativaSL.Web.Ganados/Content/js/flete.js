var Flete = function () {
    "use strict"
    var lugarOrigen = document.getElementById('Trayecto_LugarOrigen_id_lugar');
    var lugarDestino = document.getElementById('Trayecto_LugarDestino_id_lugar');
    var tableImpuesto;

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
            lugarOrigen.addEventListener('change', onChangeHandler);
            lugarDestino.addEventListener('change', onChangeHandler);

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
        $("#Trayecto_Remitente_IDCliente").on("change", function () {
            var IDRemitente = $(this).val();
            GetLugarXIDRemitente(IDRemitente);
            $("#LugarOrigen_Direccion").val('');
            $("#LugarOrigen_descripcion").val('');
        });
        $("#Trayecto_Destinatario_IDCliente").on("change", function () {
            var IDDestino = $(this).val();
            GetLugarXIDDestino(IDDestino);
            $("#LugarDestino_Direccion").val('');
            $("#LugarDestino_descripcion").val('');
        });
        $("#Trayecto_LugarOrigen_id_lugar").on("change", function () {
            var direccion = $(this).find(":selected").data("direccion");
            var descripcion = $(this).find(":selected").text();
            $("#Trayecto_LugarOrigen_Direccion").val(direccion);
            $("#Trayecto_LugarOrigen_descripcion").val(descripcion);
        });
        $("#Trayecto_LugarDestino_id_lugar").on("change", function () {
            var direccion = $(this).find(":selected").data("direccion");
            var descripcion = $(this).find(":selected").text();
            $("#Trayecto_LugarDestino_Direccion").val(direccion);
            $("#Trayecto_LugarDestino_descripcion").val(descripcion);
        });
    }
    //Validaciones
    var LoadValidationFlete = function () {
        var form1 = $('#Frm_AC_Cliente');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({ // initialize the plugin
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
    //Tablas
    var LoadTableImpuesto = function (IDFlete) {
        tableImpuesto = $('#tblImpuesto').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "IDFlete": IDFlete
                },
                "url": "/Admin/FleteImpuesto/TableJsonFleteImpuesto/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "TipoImpuesto" },
                { "data": "Impuesto" },
                { "data": "TipoFactor" },
                { "data": "base" },
                { "data": "tasaCuota" },
                { "data": "importe" },
                {
                    "data": null,
                    "render": function (data, type, full) {

                        return "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" + full["IDFleteImpuesto"] + "' class='btn btn-yellow tooltips btn-sm editImpuesto' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                            "<a title='Eliminar' data-id='" + full["IDFleteImpuesto"] + "' class='btn btn-danger tooltips btn-sm deleteImpuesto' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
                            "</div>" +
                            "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                            "<div class='btn-group'>" +
                            "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                            "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                            "</a>" +
                            "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                            "<li>" +
                            "<a class='editImpuesto' data-id='" + full["IDFleteImpuesto"] + "'  role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
                            "</a>" +
                            "</li>" +
                            "<li>" +
                            "<a role='menuitem' tabindex='-1' id='" + full["IDFleteImpuesto"] + "'>" +
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
                    var IDFleteImpuesto = $(this).data("id")
                    ModalImpuesto(IDFleteImpuesto);
                });
                $(".deleteImpuesto").on("click", function () {
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
                                    tableImpuesto.ajax.reload();
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

        $("#btnAddImpuesto").on("click", function () {
            ModalImpuesto(0);
        });
    };
   //Funcion Modal
    function ModalImpuesto(IDFleteImpuesto) {
        $.ajax({
            url: '/Admin/FleteImpuesto/ModalFleteImpuesto/',
            type: "POST",
            data: { IDFleteImpuesto: IDFleteImpuesto },
            success: function (data) {
                $('#ContenidoModalImpuesto').html(data);
                $('#ModalImpuesto').modal({ backdrop: 'static', keyboard: false });
            }
        });
    }
    //Funciones
    function AC_Flete() {
        var form = $("#Frm_AC_Cliente")[0];
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
        var selectIndexInicio   = lugarOrigen.selectedIndex;
        var optionInicio        = lugarOrigen.options.item(selectIndexInicio);
        var latitudInicial      = optionInicio.dataset.latitud.replace(",", ".");
        var longInicial         = optionInicio.dataset.longitud.replace(",", ".");

        var selectIndexFinal    = lugarDestino.selectedIndex;
        var optionFinal         = lugarDestino.options.item(selectIndexFinal);
        var latitudFinal        = optionFinal.dataset.latitud.replace(",", ".");
        var longFinal           = optionFinal.dataset.longitud.replace(",", ".");

        var inicio  = new google.maps.LatLng(latitudInicial, longInicial);
        var final   = new google.maps.LatLng(latitudFinal, longFinal);

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

                $("#Trayecto_LugarOrigen_id_lugar option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Trayecto_LugarOrigen_id_lugar").append('<option value="' + result[i].id_lugar + '" data-latitud="' + result[i].latitud + '" data-longitud="' + result[i].longitud + '" data-direccion="' + result[i].Direccion + '">' + result[i].descripcion + '</option>');
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

                $("#Trayecto_LugarDestino_id_lugar option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Trayecto_LugarDestino_id_lugar").append('<option value="' + result[i].id_lugar + '" data-latitud="' + result[i].latitud + '" data-longitud="' + result[i].longitud + '" data-direccion="' + result[i].Direccion + '">' + result[i].descripcion + '</option>');
                }
            }
        });
    }

    return {
        init: function (option, IDFlete) {
            InitMap(option);
            RunEventsLineaFletera();
            LoadValidationFlete();
            LoadTableImpuesto(IDFlete);
        }
    };
}();