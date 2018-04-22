﻿var Flete = function () {
    "use strict"
    var lugarOrigen = document.getElementById('Trayecto_LugarOrigen_id_lugar');
    var lugarDestino = document.getElementById('Trayecto_LugarDestino_id_lugar');
    var tableImpuesto, map, directionsDisplay, directionsService;

    var InitMap = function () {
        google.maps.event.addDomListener(window, "load", gmap);
    };
    function gmap() {
        
        directionsService = new google.maps.DirectionsService;
        map = new google.maps.Map(document.getElementById('map'), {
            zoom: 10,
            center: { lat: 17.6063149, lng: -93.204288 },
            mapTypeId: google.maps.MapTypeId.ROADMAP
        });
        directionsDisplay = new google.maps.DirectionsRenderer({
            map: map
        });
        var onChangeHandler = function () {
            CalculateAndDisplayRoute(directionsService, directionsDisplay, map);
        };
        lugarOrigen.addEventListener('change', onChangeHandler);
        lugarDestino.addEventListener('change', onChangeHandler);
    }
    //Eventos
    var RunEventsGeneral = function () {
        $('.cslTablist').on("click", function () {
            var id = $(this).attr('id');
            //Si es el tab de trayecto, muestra el trayecto seleccionado
            //Se debe de accionar el evento al momento que el div este visible para que se obtenga 
            //El tamaño correcto del div, si no marca width 0 (agarra del css el valor dado por defecto, pero 
            //Si se pone en porcentaje lo agarra como pixel = 100% = 100px)
            //y el zoom 0 al momento de mostrar
            if (id == "tabTrayecto") {
                CalculateAndDisplayRoute(directionsService, directionsDisplay, map);
            }
            var data = $(this).data("toggle");
            if (data != "tab") {
                Mensaje("Debe guardar sus cambios para continuar.", 2);
            }
        });
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
    var RunEventsFleteimpuesto = function () {
        $("#TipoFactor_Clave").on("change", function () {
            var option = $(this).val();
            var TasaCuota = $("#TasaCuota");
            var Importe = $("#Importe");
           
            //Excento
            if(option == 3)
            {
                TasaCuota.attr('readonly', true);
                TasaCuota.rules("remove", "required TasaCuotaSAT");
                TasaCuota.closest(".controlError").removeClass("has-success has-error");
                $("#validation_summary_AC_FleteImpuesto").find("dd[for='TasaCuota']").addClass('help-block valid').text('');
                TasaCuota.val("0");
                Importe.val("0");
            }
            else
            {
                TasaCuota.attr('readonly', false);
                TasaCuota.rules("add", { required: true, TasaCuotaSAT: true });
            }
        });
        $("#Base").on("keyup keydown", function (e) {
            ObtenerImporte();
        }); 
        $("#TasaCuota").on("keyup keydown", function (e) {
            ObtenerImporte();
        });
    }
    //Validaciones
    var LoadValidation_AC_Cliente = function () {
        var form1 = $('#Frm_AC_Cliente');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({ // initialize the plugin
            //debug: true,
            errorElement: "li",
            errorClass: 'text-danger',
            errorLabelContainer: $("#validation_summary_AC_Cliente"),
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
                "Empresa.IDEmpresa": {
                    required: true
                },
                "Cliente.IDCliente": {
                    required: true
                },
                "Chofer_IDChofer": {
                    required: true
                },
                "Vehiculo.IDVehiculo": {
                    required: true
                },
                kmInicialVehiculo: {
                    digits: true
                },
                FechaTentativaEntrega: {
                    required: true
                }
            },
            messages: {
                "Empresa.IDEmpresa": {
                    required: "-Seleccione una línea fletera."
                },
                "Cliente.IDCliente": {
                    required: "-Seleccione un cliente."
                },
                "Chofer_IDChofer": {
                    required: "-Seleccione un chofer."
                },
                "Vehiculo.IDVehiculo": {
                    required: "-Seleccione un vehículo."
                },
                kmInicialVehiculo: {
                    digits: "Ingrese un número entero mayor o igual a 0 (cero). "
                },
                FechaTentativaEntrega: {
                    required: "-Seleccione una fecha tentativa de entrega."
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
                AC_Cliente();
            }
        });
    };
    var LoadValidation_AC_Trayecto = function () {
        var form1 = $('#frm_AC_Trayecto');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({ // initialize the plugin
            //debug: true,
            errorElement: "li",
            errorClass: 'text-danger',
            errorLabelContainer: $("#validation_summary_AC_Trayecto"),
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
                "Trayecto.Remitente.IDCliente": {
                    required: true
                },
                "Trayecto.LugarOrigen.Direccion": {
                    required: true
                },
                "Trayecto.LugarOrigen.descripcion": {
                    required: true
                },
                "Trayecto.Destinatario.IDCliente": {
                    required: true
                },
                "Trayecto.LugarDestino.Direccion": {
                    required: true
                },
                "Trayecto.LugarDestino.descripcion": {
                    required: true
                }
            },
            messages: {
                "Trayecto.Remitente.IDCliente": {
                    required: "-Seleccione un cliente de origen"
                },
                "Trayecto.LugarOrigen.Direccion": {
                    required: "-La dirección del cliente de origen es necesario, seleccione un lugar de origen del mapa."
                },
                "Trayecto.LugarOrigen.descripcion": {
                    required: "-La descripción del cliente de origen es necesario, seleccione un lugar de origen del mapa."
                },
                "Trayecto.Destinatario.IDCliente": {
                    required: "-Seleccione un cliente destino."
                },
                "Trayecto.LugarDestino.Direccion": {
                    required: "-La dirección del cliente de destino es necesario, seleccione un lugar de origen del mapa."
                },
                "Trayecto.LugarDestino.descripcion": {
                    required: "-La descripción del cliente de destino es necesario, seleccione un lugar de origen del mapa."
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
                AC_Trayecto();
            }
        });
    };
    var LoadValidationFleteImpuesto = function () {
        var form1 = $('#frm_AC_FleteImpuesto');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        $('#frm_AC_FleteImpuesto').validate({ // initialize the plugin
            //debug: true,
            errorElement: "dd",
            errorClass: 'text-danger',
            errorLabelContainer: $("#validation_summary_AC_FleteImpuesto"),
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
                "TipoImpuesto.Clave": {
                    min: 1
                },
                Base: {
                    required: true,
                    BaseSAT: true
                },
                "Impuesto.Clave": {
                    min: 1,
                },
                "TipoFactor.Clave": {
                    min: 1,
                },
                "TasaCuota": {
                    required: true,
                    TasaCuotaSAT: true
                },
                Importe: {
                    required: true
                }
            },
            messages: {
                "TipoImpuesto.Clave": {
                    min: "Seleccione un tipo de impuesto."
                },
                Base: {
                    required: "Ingrese una cantidad base."
                },
                "Impuesto.Clave": {
                    min: "Seleccione un Impuesto."
                },
                "TipoFactor.Clave": {
                    min: "Seleccione un Tipo o Factor."
                },
                "TasaCuota": {
                    required: "Ingrese una tasa o cuota."
                },
                Importe: {
                    required: "Ingrese un importe."
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
                AC_FleteImpuesto();
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
                RunEventsFleteimpuesto();
                LoadValidationFleteImpuesto();
            }
        });
    }
    //Funciones
    function AC_Cliente() {
        var form = $("#Frm_AC_Cliente")[0];
        var formData = new FormData(form);

        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/Flete/AC_Cliente/',
            contentType: false,
            processData: false,
            cache: false,
            error: function (response) {
                Mensaje(response.Mensaje, "2");
            },
            success: function (response) {
                if (response.Success) {
                    Mensaje("Datos guardados con éxito.", "1");
                    //Recogo los valores
                    var json = JSON.parse(response.Mensaje);
                    $("#id_flete").val = json.id_flete;
                    $("#Folio").text("Folio del flete: " + json.folio);
                    //Habilitamos el tab cliente
                    document.getElementById("tabTrayecto").dataset.toggle = "tab";
                    $('#tabTrayecto').data('toggle', "tab")
                    $("#liTrayecto").removeClass('disabled').addClass('pestaña');
                }
                else
                    Mensaje(response.Mensaje, "2");
            }
        });
    }
    function AC_Trayecto() {
        var form = $("#frm_AC_Trayecto")[0];
        var formData = new FormData(form);

        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/Flete/AC_Trayecto/',
            contentType: false,
            processData: false,
            cache: false,
            error: function (response) {
                Mensaje(response.Mensaje, "2");
            },
            success: function (response) {
                if (response.Success) {
                    Mensaje("Trayecto creado con éxito.", "1");
                    //Recogo los valores
                    $("#Trayecto_id_trayecto").val = response.Mensaje;
                    //Habilitamos el tab movimientos
                    document.getElementById("tabCobro").dataset.toggle = "tab";
                    $('#tabCobro').data('toggle', "tab")
                    $("#liCobro").removeClass('disabled').addClass('pestaña');
                }
                else
                    Mensaje(response.Mensaje, "2");
            }
        });
    }
    function AC_FleteImpuesto() {
        var form = $("#frm_AC_FleteImpuesto")[0];
        var formData = new FormData(form);

        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/FleteImpuesto/AC_FleteImpuesto/',
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
    function ObtenerImporte() {
        var Base = $('#Base').val();
        var TasaCuota = $('#TasaCuota').val();
        var Importe = $("#Importe");
        
        if (isNaN(Base) || isNaN(TasaCuota)) {
            Importe.val("0");
        }
        else {
            var total = Base * TasaCuota;
            Importe.val(total.toFixed(2));
        }
    }
    function CalculateAndDisplayRoute(directionsService, directionsDisplay, map) {
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
//Aunque no lo utilizo, puede servir en otro momento, aunque ya no se utiliza en la nueva version de googleMap
    /**
* Returns the zoom level at which the given rectangular region fits in the map view. 
* The zoom level is computed for the currently selected map type. 
* @param {google.maps.Map} map
* @param {google.maps.LatLngBounds} bounds 
* @return {Number} zoom level
**/
    function getZoomByBounds(map, bounds) {
        var MAX_ZOOM = map.mapTypes.get(map.getMapTypeId()).maxZoom || 22;
        var MIN_ZOOM = map.mapTypes.get(map.getMapTypeId()).minZoom || 0;
        console.log("MAX_ZOOM: " + MAX_ZOOM);
        console.log("MIN_ZOOM: " + MIN_ZOOM);

        var ne = map.getProjection().fromLatLngToPoint(bounds.getNorthEast());
        var sw = map.getProjection().fromLatLngToPoint(bounds.getSouthWest());
        console.log("ne: " + ne);
        console.log("sw " + sw);

        console.log("GetDiv.width(): " + $(map.getDiv()).width());
        console.log("GetDiv.height(): " + $(map.getDiv()).height());

        var worldCoordWidth = Math.abs(ne.x - sw.x);
        var worldCoordHeight = Math.abs(ne.y - sw.y);
        console.log("worldCoordWidth " + worldCoordWidth);
        console.log("worldCoordHeight " + worldCoordHeight);

        //Fit padding in pixels 
        var FIT_PAD = 40;

        for (var zoom = MAX_ZOOM; zoom >= MIN_ZOOM; --zoom) {
            if (worldCoordWidth * (1 << zoom) + 2 * FIT_PAD < $(map.getDiv()).width() &&
                worldCoordHeight * (1 << zoom) + 2 * FIT_PAD < $(map.getDiv()).height())
                return zoom;
        }
        return 0;
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
    function DesbloquearTabs() {
        //Solo el primer tab esta desbloqueado
        var key1 = $("#id_flete");
        var key2 = $("#Trayecto_id_trayecto");
        if (key1.val().length == 36) {
            //Hay un flete desbloqueamos el trayecto
            document.getElementById("tabTrayecto").dataset.toggle = "tab";
            $('#tabTrayecto').data('toggle', "tab")
            $("#liTrayecto").removeClass('disabled').addClass('pestaña');
        }
        if (key2.val().length == 36) {
            //Hay un flete desbloqueamos el cobro
            document.getElementById("tabCobro").dataset.toggle = "tab";
            $('#tabCobro').data('toggle', "tab")
            $("#liCobro").removeClass('disabled').addClass('pestaña');
        }
    }
    return {
        init: function (IDFlete) {
            InitMap();
            RunEventsGeneral();
            DesbloquearTabs();
            LoadValidation_AC_Cliente();
            LoadValidation_AC_Trayecto();

            LoadTableImpuesto(IDFlete);
        }
    };
}();