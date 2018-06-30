var CompraAgendar = function () {
    "use strict"
    //google maps
    var map, directionsDisplay, directionsService;
    //datatables
    var tableDocumentos;
    //otros
    var opcionServer = $("#TipoFlete").val();
    var Validation_summary_flete = $("#validation_summary_flete");
    var IDEmpresa = $("#IDEmpresa");
    var IDSucursal = $("#IDSucursal");
    var IDChofer = $("#IDChofer");
    var IDVehiculo = $("#IDVehiculo");
    var Flete_kmInicialVehiculo = $("#Flete_kmInicialVehiculo");
    var DocumentosPorCobrarDetallePagos_Monto = $("#DocumentosPorCobrarDetallePagos_Monto");
    var Flete_Id_metodoPago = $("#Flete_Id_metodoPago");
    var DocumentosPorCobrarDetallePagos_Id_formaPago = $("#DocumentosPorCobrarDetallePagos_Id_formaPago");
    var Trayecto_id_lugarOrigen = $("#Trayecto_id_lugarOrigen");
    var Trayecto_id_lugarDestino = $("#Trayecto_id_lugarDestino");
    var Bancarizado = $("#DocumentosPorCobrarDetallePagos_Bancarizado");
    var DocumentosPorCobrarDetallePagos_FolioIFE = $("#DocumentosPorCobrarDetallePagos_FolioIFE");
    var DocumentosPorCobrarDetallePagos_NumeroAutorizacion = $("#DocumentosPorCobrarDetallePagos_NumeroAutorizacion");
    var DocumentosPorCobrarDetallePagos_HttpImagen = $("#DocumentosPorCobrarDetallePagos_HttpImagen");
    var DocumentosPorCobrarDetallePagos_Id_cuentaBancariaOrdenante = $("#DocumentosPorCobrarDetallePagos_Id_cuentaBancariaOrdenante");
    var DocumentosPorCobrarDetallePagos_Id_cuentaBancariaBeneficiante = $("#DocumentosPorCobrarDetallePagos_Id_cuentaBancariaBeneficiante");

    /*INICIA PROVEEDOR*/
    var RunEventsProveedor = function () {
        $("#IDProveedor").on("change", function () {
            var IDProveedor = $(this).val();
            GetLugaresProveedorXIDProveedor(IDProveedor);
        });
        $('#FechaHoraProgramada').datepicker({
            format: 'dd/mm/yyyy',
            language: 'es'
        });
        $('#tabFlete').on("click", function () {
            CalculateAndDisplayRoute(directionsService, directionsDisplay, map);
        });
    }
    var LoadValidationProveedor = function () {
        var form1 = $('#frmProveedor');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({ // initialize the plugin
            //debug: true,
            errorElement: "dd",
            errorClass: 'text-danger',
            errorLabelContainer: $("#validation_summary_proveedor"),
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
                IDSucursal: {
                    required: true
                },
                IDPLugarProveedor: {
                    required: true
                },
                GanadosPactadoMachos: {
                    required: true,
                    digits: true
                },
                GanadosPactadoHembras: {
                    required: true,
                    digits: true
                },
                FechaHoraProgramada: {
                    required: true,
                    fecha: true
                }
            },
            messages: {
                IDProveedor: {
                    required: "-Seleccione un Proveedor"
                },
                IDSucursal: {
                    required: "-Seleccione una Sucursal"
                },
                IDPLugarProveedor: {
                    required: "-Seleccione un lugar del proveedor"
                },
                GanadosPactadoMachos: {
                    required: "-Seleccione una cantidad de ganado machos",
                    digits: "-El campo: Ganados Pactado Machos, debe ser igual o mayor que 0 (solo números enteros)."
                },
                GanadosPactadoHembras: {
                    required: "-Seleccione una cantidad de ganado hembras",
                    digits: "-El campo: Ganados Pactado Hembras, debe ser igual o mayor que 0 (solo números enteros)."
                },
                FechaHoraProgramada: {
                    required: "-Seleccione una Fecha para la compra a realizar",
                    date: "-Debe ser una fecha con formado dd/mm/aaaa"
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
                AC_Proveedor();
            }
        });
    };
    function AC_Proveedor() {
        var form = $("#frmProveedor")[0];
        var formData = new FormData(form);
        $("body").css("cursor", "progress");
        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/Compra/AC_Proveedor/',
            contentType: false,
            processData: false,
            cache: false,
            success: function (response) {
                $("body").css("cursor", "default");
                var json = JSON.parse(response.Mensaje);
                if (response.Success) {
                    Mensaje(json.Mensaje, "1");
                    $('input[name=IDCompra]').val(json.IDCompra);

                    $("#Folio").text("Folio de la compra: " + json.Folio);
                    DesbloquearTabs();
                }
                else
                    Mensaje(json.Mensaje, "2");
            }
        });
    }
    /*TERMINA PROVEEDOR*/

    /*INICIA FLETE*/
    var RunEventsFlete = function () {
        $("#Empresa_IDEmpresa").on("change", function () {
            var IDEmpresa = $(this).val();
            GetChoferesXIDEmpresa(IDEmpresa);
            GetVehiculosXIDEmpresa(IDEmpresa);
            GetJaulasXIDEmpresa(IDEmpresa);
            GetRemolquesXIDEmpresa(IDEmpresa);
            GetLugaresXIDEmpresa(IDEmpresa);
        });
        $("#TipoFlete").on("change", function () {
            var opcion = $(this).val();
            ToggleDivTipoFlete(opcion);
        });

        $("#DocumentosPorCobrarDetallePagos_Id_formaPago").on("change", function () {
            var opcion = $(this).find(":selected").data("bancarizado");
            ToggleDivBancarizado(opcion);
        });
        

    }
    var LoadValidationFlete = function () {
        var form1 = $('#frmFlete');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({ // initialize the plugin
            debug: true,
            errorElement: "dd",
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
                IDEmpresa: { required: true },
                IDSucursal: { required: true },
                IDChofer: { required: true },
                IDVehiculo: { required: true },
                "Flete.kmInicialVehiculo": { required: true, digits: true },
                "Flete.precioFlete":{ required:true, min: 0 },
                TipoFlete: { required: true },
                "Flete.Id_metodoPago": { required:true },
                "Flete.Id_formaPago": { min: 1 },
                "Trayecto.id_lugarOrigen": { required: true },
                "Trayecto.id_lugarDestino": { required: true },
                //bancarizado
                "DocumentosPorCobrarDetallePagos.FolioIFE": { required: true },
                "DocumentosPorCobrarDetallePagos.NumeroAutorizacion": { required: true },
                "DocumentosPorCobrarDetallePagos.HttpImagen": { ImagenRequerida: true },
                "DocumentosPorCobrarDetallePagos.Id_cuentaBancariaOrdenante": { required: true },
                "DocumentosPorCobrarDetallePagos.Id_cuentaBancariaBeneficiante": { required: true }
            },
            messages: {
                IDEmpresa: {  required: "Por favor, seleccione una línea fletera." },
                IDSucursal: { required: "Por favor, seleccione una sucursal." },
                IDChofer: { required: "Por favor, seleccione un chofer." },
                IDVehiculo: { required: "Por favor, seleccione un vehículo." },
                "Flete.kmInicialVehiculo": { required: "Por favor, ingrese el kilómetraje inicial.", digits: "Por favor, ingrese un número entero mayor o igual a 0 (cero). " },
                "Flete.precioFlete": { required: "Por favor, ingrese un precio del flete, puede ser 0 (cero).", min: "Por favor, ingrese un precio del flete, debe ser mayor o igual a 0 (cero)." },
                TipoFlete: { required: "Por favor, seleccione un tipo de flete." },
                "Flete.Id_metodoPago": { required: "Por favor, seleccione un metodo de pago." },
                "Flete.Id_formaPago": { min: "Por favor, seleccione una forma de pago." },
                "Trayecto.id_lugarOrigen": { required: "Por favor, seleccione un lugar de origen." },
                "Trayecto.id_lugarDestino": { required: "Por favor, seleccione un lugar destino." },
                //bancarizado
                "DocumentosPorCobrarDetallePagos.FolioIFE": { required: "Por favor, escriba el número de folio del INE." },
                "DocumentosPorCobrarDetallePagos.NumeroAutorizacion": { required: "Por favor, escriba el número de autorización." },
                "DocumentosPorCobrarDetallePagos.HttpImagen": { ImagenRequerida: "Por favor, seleccione una imagen del comprobante del cobro." },
                "DocumentosPorCobrarDetallePagos.Id_cuentaBancariaOrdenante": { required: "Por favor, seleccione una cuenta bancaria de la empresa." },
                "DocumentosPorCobrarDetallePagos.Id_cuentaBancariaBeneficiante": { required: "Por favor, seleccione una cuenta bancaria del cliente." }
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
    function AC_Flete() {
        var form = $("#frmFlete")[0];
        var formData = new FormData(form);
        var IDSucursal = $("#IDSucursal").val();
        formData.append("IDSucursal", IDSucursal);

        $("body").css("cursor", "progress");
        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/Compra/AC_Flete/',
            contentType: false,
            processData: false,
            cache: false,
            success: function (response) {
                $("body").css("cursor", "default");
                var json = JSON.parse(response.Mensaje);
                if (response.Success) {
                    Mensaje(json.Mensaje, "1");
                    $('input[name=IDFlete]').val(json.IDFlete);
                    $('input[name=Id_documentoPorCobrar]').val(json.Id_documentoPorCobrar);
                    DesbloquearTabs();
                    LoadTableDocumentos();
                }
                else
                    Mensaje(json.Mensaje, "2");
            }
        });
    }
    function GetLugaresProveedorXIDProveedor(IDProveedor) {
        $.ajax({
            url: '/Admin/Compra/GetLugaresProveedorXIDProveedor/',
            type: "POST",
            dataType: 'json',
            data: { IDProveedor: IDProveedor },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {

                $("#IDPLugarProveedor option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#IDPLugarProveedor").append('<option value="' + result[i].id_lugar + '">' + result[i].descripcion + '</option>');
                }
                $('#IDPLugarProveedor.select').selectpicker('refresh');

                $("#Trayecto_id_lugarDestino option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Trayecto_id_lugarDestino").append('<option value="' + result[i].id_lugar + '" data-latitud="' + result[i].latitud + '" data-longitud="' + result[i].longitud + '">' + result[i].descripcion + '</option>');
                }
            }
        });
    }
    function GetVehiculosXIDEmpresa(IDEmpresa) {
        $.ajax({
            url: '/Admin/Compra/GetVehiculosXIDEmpresa/',
            type: "POST",
            dataType: 'json',
            data: { IDEmpresa: IDEmpresa },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {
                $('#IDVehiculo').empty();
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
                        $("#IDVehiculo").append(option);
                        //Creamos un group nuevo
                        option = '<optgroup label="' + result[i].Modelo + '"><option value="' + result[i].IDVehiculo + '">' + result[i].nombreMarca + '</option>';
                        optgroup = result[i].Modelo;
                    }
                }
                //Anexamos el último valor
                option += '</optgroup>';
                $("#IDVehiculo").append(option);
                $('#IDVehiculo.select').selectpicker('refresh');
            }
        });
    }
    function GetChoferesXIDEmpresa(IDEmpresa) {
        $.ajax({
            url: '/Admin/Compra/GetChoferesXIDEmpresa/',
            type: "POST",
            dataType: 'json',
            data: { IDEmpresa: IDEmpresa },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {

                $("#IDChofer option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#IDChofer").append('<option value="' + result[i].IDChofer + '">' + result[i].Nombre + '</option>');
                }
                $('#IDChofer.select').selectpicker('refresh');
            }
        });
    }
    function GetJaulasXIDEmpresa(IDEmpresa) {
        $.ajax({
            url: '/Admin/Compra/GetJaulasXIDEmpresa/',
            type: "POST",
            dataType: 'json',
            data: { IDEmpresa: IDEmpresa },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {

                $("#IDJaula option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#IDJaula").append('<option value="' + result[i].IDJaula + '">' + result[i].Matricula + '</option>');
                }
                $('#IDJaula.select').selectpicker('refresh');
            }
        });
    }
    function GetRemolquesXIDEmpresa(IDEmpresa) {
        $.ajax({
            url: '/Admin/Compra/GetRemolquesXIDEmpresa/',
            type: "POST",
            dataType: 'json',
            data: { IDEmpresa: IDEmpresa },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {

                $("#IDRemolque option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#IDRemolque").append('<option value="' + result[i].IDRemolque + '">' + result[i].placa + '</option>');
                }
                $('#IDRemolque.select').selectpicker('refresh');
            }
        });
    }
    function GetLugaresXIDEmpresa(IDEmpresa) {
        $.ajax({
            url: '/Admin/Compra/GetLugaresXIDEmpresa/',
            type: "POST",
            dataType: 'json',
            data: { IDEmpresa: IDEmpresa },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {
                $("#Trayecto_id_lugarOrigen option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Trayecto_id_lugarOrigen").append('<option value="' + result[i].id_lugar + '" data-latitud="' + result[i].latitud + '" data-longitud="' + result[i].longitud + '">' + result[i].descripcion + '</option>');
                }
            }
        });
    }
    var InitMap = function () {
        directionsDisplay = new google.maps.DirectionsRenderer;
        directionsService = new google.maps.DirectionsService;
        map = new google.maps.Map(document.getElementById('map'), {
            zoom: 10,
            center: { lat: 17.6063149, lng: -93.204288 }
        });
        directionsDisplay.setMap(map);

        var onChangeHandler = function () {
            CalculateAndDisplayRoute(directionsService, directionsDisplay);
        };
        document.getElementById("Trayecto_id_lugarOrigen").addEventListener('change', onChangeHandler);
        document.getElementById("Trayecto_id_lugarDestino").addEventListener('change', onChangeHandler);

        CalculateAndDisplayRoute(directionsService, directionsDisplay);
        
    };
    function CalculateAndDisplayRoute(directionsService, directionsDisplay) {
        var selectIndexInicio = document.getElementById('Trayecto_id_lugarOrigen').selectedIndex;
        var optionInicio = document.getElementById('Trayecto_id_lugarOrigen').options.item(selectIndexInicio);
        var latitudInicial = optionInicio.dataset.latitud.replace(",", ".");
        var longInicial = optionInicio.dataset.longitud.replace(",", ".");

        var selectIndexFinal = document.getElementById('Trayecto_id_lugarDestino').selectedIndex;
        var optionFinal = document.getElementById('Trayecto_id_lugarDestino').options.item(selectIndexFinal);
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

    function ToggleDivTipoFlete(opcion) {
        if (opcion == 1) {
            AgregarValidaciones();
            $('#divNoAplicaFlete').show(1000);
        }
        else if (opcion == 2  ||  opcion === '') {
            QuitarValidaciones();
            $('#divNoAplicaFlete').hide(1000);
        }
    }
    function ToggleDivBancarizado(opcion) {
        if (opcion == 1) {
            AgregarValidacionesBancarizado();
            Bancarizado.value = true;
            $('#divBancarizado').show(1000);
        }
        else {
            QuitarValidacionesBancarizadas();
            Bancarizado.value = false;
            $('#divBancarizado').hide(1000);
        }
    }
    function AgregarValidacionesBancarizado() {
        DocumentosPorCobrarDetallePagos_Id_cuentaBancariaBeneficiante.rules("add", { required: true });
        DocumentosPorCobrarDetallePagos_Id_cuentaBancariaOrdenante.rules("add", { required: true });
        DocumentosPorCobrarDetallePagos_HttpImagen.rules("add", { ImagenRequerida: true });
        DocumentosPorCobrarDetallePagos_FolioIFE.rules("add", { required: true });
        DocumentosPorCobrarDetallePagos_NumeroAutorizacion.rules("add", { required: true });
    }
    function QuitarValidacionesBancarizadas() {
        DocumentosPorCobrarDetallePagos_Id_cuentaBancariaBeneficiante.rules("remove", "required");
        DocumentosPorCobrarDetallePagos_Id_cuentaBancariaOrdenante.rules("remove", "required");
        DocumentosPorCobrarDetallePagos_HttpImagen.rules("remove", "ImagenRequerida");
        DocumentosPorCobrarDetallePagos_FolioIFE.rules("remove", "required");
        DocumentosPorCobrarDetallePagos_NumeroAutorizacion.rules("remove", "required");

        DocumentosPorCobrarDetallePagos_Id_cuentaBancariaBeneficiante.closest(".controlError").removeClass("has-success has-error");
        DocumentosPorCobrarDetallePagos_Id_cuentaBancariaOrdenante.closest(".controlError").removeClass("has-success has-error");
        DocumentosPorCobrarDetallePagos_HttpImagen.closest(".controlError").removeClass("has-success has-error");
        DocumentosPorCobrarDetallePagos_FolioIFE.closest(".controlError").removeClass("has-success has-error");
        DocumentosPorCobrarDetallePagos_NumeroAutorizacion.closest(".controlError").removeClass("has-success has-error");

        $("#validation_summary").find("dd[for='DocumentosPorCobrarDetallePagos_FolioIFE']").addClass('help-block valid').text('');
        $("#validation_summary").find("dd[for='DocumentosPorCobrarDetallePagos_NumeroAutorizacion']").addClass('help-block valid').text('');
        $("#validation_summary").find("dd[for='DocumentosPorCobrarDetallePagos_HttpImagen']").addClass('help-block valid').text('');
        $("#validation_summary").find("dd[for='DocumentosPorCobrarDetallePagos_Id_cuentaBancariaOrdenante']").addClass('help-block valid').text('');
        $("#validation_summary").find("dd[for='DocumentosPorCobrarDetallePagos_Id_cuentaBancariaBeneficiante']").addClass('help-block valid').text('');
    }
    function AgregarValidaciones() {
        IDEmpresa.rules("add", { required: true });
        IDSucursal.rules("add", { required: true });
        IDChofer.rules("add", { required: true });
        IDVehiculo.rules("add", { required: true });
        Flete_kmInicialVehiculo.rules("add", { required: true });
        DocumentosPorCobrarDetallePagos_Monto.rules("add", { required: true });
        Flete_Id_metodoPago.rules("add", { required: true });
        DocumentosPorCobrarDetallePagos_Id_formaPago.rules("add", { required: true, min: 0 });
        Trayecto_id_lugarOrigen.rules("add", { required: true, min: 0 });
        Trayecto_id_lugarDestino.rules("add", { required: true });
    }
    function QuitarValidaciones() {
        IDEmpresa.rules("remove", "required");
        IDSucursal.rules("remove", "required");
        IDChofer.rules("remove", "required");
        IDVehiculo.rules("remove", "required");
        Flete_kmInicialVehiculo.rules("remove", "required digits");
        DocumentosPorCobrarDetallePagos_Monto.rules("remove", "required min");
        Flete_Id_metodoPago.rules("remove", "required");
        DocumentosPorCobrarDetallePagos_Id_formaPago.rules("remove", "min");
        Trayecto_id_lugarOrigen.rules("remove", "required");
        Trayecto_id_lugarDestino.rules("remove", "required");

        IDEmpresa.closest(".controlError").removeClass("has-success has-error");
        IDSucursal.closest(".controlError").removeClass("has-success has-error");
        IDChofer.closest(".controlError").removeClass("has-success has-error");
        IDVehiculo.closest(".controlError").removeClass("has-success has-error");
        Flete_kmInicialVehiculo.closest(".controlError").removeClass("has-success has-error");
        DocumentosPorCobrarDetallePagos_Monto.closest(".controlError").removeClass("has-success has-error");
        Flete_Id_metodoPago.closest(".controlError").removeClass("has-success has-error");
        DocumentosPorCobrarDetallePagos_Id_formaPago.closest(".controlError").removeClass("has-success has-error");
        Trayecto_id_lugarOrigen.closest(".controlError").removeClass("has-success has-error");
        Trayecto_id_lugarDestino.closest(".controlError").removeClass("has-success has-error");

        Validation_summary_flete.find("dd[for='IDEmpresa']").addClass('help-block valid').text('');
        Validation_summary_flete.find("dd[for='IDSucursal']").addClass('help-block valid').text('');
        Validation_summary_flete.find("dd[for='IDChofer']").addClass('help-block valid').text('');
        Validation_summary_flete.find("dd[for='IDVehiculo']").addClass('help-block valid').text('');
        Validation_summary_flete.find("dd[for='Flete_kmInicialVehiculo']").addClass('help-block valid').text('');
        Validation_summary_flete.find("dd[for='DocumentosPorCobrarDetallePagos_Monto']").addClass('help-block valid').text('');
        Validation_summary_flete.find("dd[for='Flete_Id_metodoPago']").addClass('help-block valid').text('');
        Validation_summary_flete.find("dd[for='DocumentosPorCobrarDetallePagos_Id_formaPago']").addClass('help-block valid').text('');
        Validation_summary_flete.find("dd[for='Trayecto_id_lugarOrigen']").addClass('help-block valid').text('');
        Validation_summary_flete.find("dd[for='Trayecto_id_lugarDestino']").addClass('help-block valid').text('');
    }

    /*TERMINA FLETE*/

    /*INICIA DOCUMENTOS*/
    var LoadTableDocumentos = function () {
        var IDCompra = $("#IDCompra").val();

        if (!$.fn.DataTable.isDataTable('#tblDocumentos')) {
            tableDocumentos = $('#tblDocumentos').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "IDCompra": IDCompra
                },
                "url": "/Admin/Compra/TableJsonDocumentos/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "descripcion" },
                { "data": "clave" },
                {
                    "data": null,
                    "render": function (data, type, full) {
                        var imagen64 = full["imagen"];
                        var img = "";
                        if (imagen64 != null) {

                            var extension = "";

                            var position = imagen64.indexOf("iVBOR");
                            //imagen png
                            if (position != -1)
                                extension = "image/png";

                            position = imagen64.indexOf("/9j/4");
                            if (position != -1)
                                extension = "image/jpeg";
                            //bmp de 256 colores
                            position = imagen64.indexOf("Qk3");
                            if (position == 0)
                                extension = "image/bmp";

                            //bmp de monocromatico colores
                            position = imagen64.indexOf("Qk2");
                            if (position == 0)
                                extension = "image/bmp";

                            //bmp de 16 colores
                            position = imagen64.indexOf("Qk1");
                            if (position == 0)
                                extension = "image/bmp";

                            img = "<img class='file-preview-image' style='width: 150px; height: 150px;' src='data:" + extension + ";base64," + full["imagen"] + "' />";
                        }
                        else {
                            img = "<img class='file-preview-image' style='width: 150px; height: 150px;' src='/Content/img/GrupoOcampo.png' />";
                        }
                        return img;
                    }
                },
                {
                    "data": null,
                    "render": function (data, type, full) {
                        
                        return "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" + full["id_documento"] + "' class='btn btn-yellow tooltips btn-sm editDocumento' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                            "<a data-hrefa='/Admin/Flete/DEL_Documento/' title='Eliminar' data-id='" + full["id_documento"] + "' class='btn btn-danger tooltips btn-sm deleteDocumento' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
                            "</div>" +
                            "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                            "<div class='btn-group'>" +
                            "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                            "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                            "</a>" +
                            "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                            "<li>" +
                            "<a data-id='" + full["id_documento"] + "' class='editDocumento' role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
                            "</a>" +
                            "</li>" +
                            "<li>" +
                            "<a data-hrefa='/Admin/Flete/DEL_Documento/' class='deleteDocumento' role='menuitem' tabindex='-1' data-id='" + full["id_documento"] + "'>" +
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
                $(".editDocumento").on("click", function () {
                    var IDDocumento = $(this).data("id");

                    ModalDocumento(IDCompra, IDDocumento);
                });
                $(".deleteDocumento").on("click", function () {
                    var url = $(this).attr('data-hrefa');
                    var row = $(this).attr('data-id');
                    var box = $("#mb-deleteDocumento");
                    box.addClass("open");
                    box.find(".mb-control-yes").on("click", function () {
                        box.removeClass("open");
                        $.ajax({
                            url: url,
                            data: { IDDocumento: row },
                            type: 'POST',
                            dataType: 'json',
                            success: function (result) {
                                if (result.Success) {
                                    box.find(".mb-control-yes").prop('onclick', null).off('click');
                                    Mensaje("Documento eliminado con éxito.", "1");
                                    $("#ModalDocumento").modal('hide');
                                    tableDocumentos.ajax.reload();
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
        }
        

        $("#btnAddDocumento").on("click", function () {
            var IDCompra = $("#IDCompra").val();
            ModalDocumento(IDCompra, 0);
        });
    };
    function ModalDocumento(IDCompra, IDDocumento) {
        $("body").css("cursor", "progress");
        $.ajax({
            url: '/Admin/Compra/ModalDocumento/',
            type: "POST",
            data: { IDCompra: IDCompra, IDDocumento: IDDocumento },
            success: function (data) {
                $("body").css("cursor", "default");
                $('#ContenidoModalDocumento').html(data);
                $('#ModalDocumento').modal({ backdrop: 'static', keyboard: false });
                
                LoadValidation_AC_Documento();
                RunEventsDocumento();
            }
        });
    }
    var RunEventsDocumento = function () {
        var Imagen = document.getElementById("MostrarImagen").value;
        var ExtensionImagen = document.getElementById("ExtensionImagenBase64").value;
        $('#ImagenPost').fileinput({
            theme: 'fa',
            language: 'es',
            showUpload: false,
            uploadUrl: "#",
            autoReplace: true,
            overwriteInitial: true,
            showUploadedThumbs: false,
            maxFileCount: 1,
            initialPreview: [
                '<img class="file-preview-image" style="width: auto; height: auto; max-width: 100%; max-height: 100%;" src="data:image/png;base64,' + Imagen + '" />'
            ],
            initialPreviewConfig: [
                { caption: 'Imagen del documento' }
            ],
            initialPreviewShowDelete: false,
            showRemove: true,
            showClose: true,
            layoutTemplates: { actionDelete: '' },
            allowedFileExtensions: ["png", 'jpg', 'bmp', 'jpeg'],
            required: true
        })
        $('#ImagenPost').on('fileclear', function (event) {
            document.getElementById("MostrarImagen").value = "";
        });
    }
    var LoadValidation_AC_Documento = function () {
        var form1 = $('#frm_AC_Documentos');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({ // initialize the plugin
            //debug: true,
            errorElement: "dd",
            errorClass: 'text-danger',
            errorLabelContainer: $("#validation_summary_AC_FleteDocumentos"),
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
                IDTipoDocumento: {
                    required: true,
                    min: 1
                }
            },
            messages: {
                IDTipoDocumento: {
                    required: "-Seleccione un tipo de documento.",
                    min: "-Seleccione un tipo de documento."
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
                AC_Documento();
            }
        });
    };
    function AC_Documento() {
        var form = $("#frm_AC_Documentos")[0];
        var formData = new FormData(form);

        var arrayKeys = new Array();
        var arrayValues = new Array();

        for (var key of formData.keys()) {
            arrayKeys.push("Documento." + key);
        }
        for (var value of formData.values()) {
            arrayValues.push(value);
        }
        for (var i = 0; i < arrayKeys.length; i++) {
            formData.append(key[i], value[i]);
        }

        $("body").css("cursor", "progress");
        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/Compra/AC_Documento/',
            contentType: false,
            processData: false,
            cache: false,
            success: function (response) {
                $("body").css("cursor", "default");
                if (response.Success) {
                    Mensaje("Datos guardados con éxito.", "1");
                    $("#ModalDocumento").modal('hide');
                    tableDocumentos.ajax.reload();
                }
                else
                    Mensaje(response.Mensaje, "2");
            }
        });
    }
    /*TERMINA DOCUMENTOS*/

    /*INICIA FUNCIONES MIXTAS*/
    function DesbloquearTabs() {
        //Solo el primer tab esta desbloqueado
        var flete = $("#IDFlete");
        var compra = $("#IDCompra");

        if (compra.val().length == 36) {
            document.getElementById("tabFlete").dataset.toggle = "tab";
            $('#tabFlete').data('toggle', "tab")
            $("#liFlete").removeClass('disabled').addClass('pestaña');
        }
        if (flete.val().length == 36) {
            //Hay un flete desbloqueamos los demas tabs
            document.getElementById("tabDocumentos").dataset.toggle = "tab";
            $('#tabDocumentos').data('toggle', "tab")
            $("#liDocumentos").removeClass('disabled').addClass('pestaña');
            LoadTableDocumentos();
            
        }
    }

    // or even this one if we want the earlier event
    $("a[href='#flete']").on('show.bs.tab', function (e) {
        ToggleDivTipoFlete(opcionServer);
        ToggleDivBancarizado(Bancarizado.val());
    });

    /*TERMINA FUNCIONES MIXTAS*/
    return {
        init: function () {
            DesbloquearTabs();
            LoadValidationProveedor();
            RunEventsProveedor();
            InitMap();
            LoadValidationFlete();
            RunEventsFlete();
        }
    };
}();