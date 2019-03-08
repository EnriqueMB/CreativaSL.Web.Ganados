var VentaFlete = function () {
    "use strict"
    var Id_venta = $("#Id_venta").val();
    var FechaEmbarque = $("#Flete_FechaEmbarque");
    var HoraEmbarque = $("#Flete_HoraEmbarque");
    var FechaSalida = $("#Flete_FechaSalida");
    var HoraSalida = $("#Flete_HoraSalida");
    var Id_empresa = $("#Flete_Id_empresa");
    var Id_chofer = $("#Flete_id_chofer");
    var Id_vehiculo = $("#Flete_id_vehiculo");
    var KmInicialVehiculo = $("#Flete_kmInicialVehiculo");
    var PrecioFlete = $("#Flete_precioFlete");                
    var ListaLugarOrigen = $("#Flete_Trayecto_id_lugarOrigen");
    var ListaLugarDestino = $("#Flete_Trayecto_id_lugarDestino");
    var Validation_summary = $("#validation_summary");
    var opcionFlete = $("#CobrarFlete").val();
    var FechaTentativaFlete = $("#Flete_FechaTentativaEntrega");
    var FormaPago = $("#Flete_FormaPago_Clave");
    var MetodoPago = $("#Flete_MetodoPago_Clave");
                
    /*INICIA FLETE*/
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
        document.getElementById("Flete_Trayecto_id_lugarOrigen").addEventListener('change', onChangeHandler);
        document.getElementById("Flete_Trayecto_id_lugarDestino").addEventListener('change', onChangeHandler);

        CalculateAndDisplayRoute(directionsService, directionsDisplay);
        
    };
    var Eventos = function (){
        $("#Id_cliente").on("change", function () {
            var Id_cliente = $(this).val();
            GetListadoLugaresCliente(Id_cliente);
        });


        $("#Id_sucursal").on("change", function () {
            var Id_sucursal = $(this).val();
            var Id_empresa = $("#Flete_Id_empresa").val();

            GetClientesXIDSucursal(Id_sucursal);
            GetChoferesXIDEmpresa(Id_empresa, Id_sucursal);
            GetVehiculosXIDEmpresa(Id_empresa, Id_sucursal);
            GetLugaresXIDEmpresa(Id_empresa); //checar si se carga mejor por sucursal
        });

        $("#Flete_Id_empresa").on("change", function () {
            var Id_empresa = $(this).val();
            var Id_sucursal = $("#Id_sucursal").val();

            GetChoferesXIDEmpresa(Id_empresa, Id_sucursal);
            GetVehiculosXIDEmpresa(Id_empresa, Id_sucursal);
            GetLugaresXIDEmpresa(Id_empresa);
        });

        $("#CobrarFlete").on("change", function () {
            var opcion = $(this).val();
            ToggleDivFlete(opcion,1);
        });
    }
    var Validaciones = function () {
        //Seleccionar filas 
        var form1 = $('#frm_venta');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({ // initialize the plugin
            // debug: true,
            errorElement: "dd", // contain the error msg in a span tag
            errorClass: 'help-block text-danger',
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
                Id_sucursal: { required: true },
                Id_cliente: { required: true },
                //NombreVenta: { required: true },
                CobrarFlete: {required: true },
                "Flete.FechaEmbarque": { required: true },
                "Flete.HoraEmbarque": { required: true },
                "Flete.FechaSalida": { required: true },
                "Flete.HoraSalida": { required: true },
                "Flete.Id_empresa": { required: true },
                "Flete.id_chofer": { required: true },
                "Flete.id_vehiculo": { required: true },
                //"Flete.kmInicialVehiculo": { required: true, min: 0 },
                "Flete.precioFlete": { required: true, min: 0 },
                "Flete.Trayecto.id_lugarOrigen": { required: true },
                "Flete.Trayecto.id_lugarDestino": { required: true },
                //"Flete.MetodoPago.Clave": { required: true },
                //"Flete.FormaPago.Clave": {min: 1},
                "Flete.FechaTentativaEntrega": { required: true },
                TipoVenta: { required: true }
            },
            messages: {
                Id_sucursal: { required: "Por favor, seleccione una sucursal." },
                Id_cliente: { required: "Por favor, seleccione un cliente." },
                //NombreVenta: { required: "Por favor, ingrese el nombre para la venta." },
                CobrarFlete: { required: "Por favor, seleccione un tipo del flete." },
                "Flete.FechaEmbarque": { required: "Por favor, seleccione una fecha del embarque." },
                "Flete.HoraEmbarque": { required: "Por favor, seleccione una hora del embarque." },
                "Flete.FechaSalida": { required: "Por favor, seleccione una fecha de salida." },
                "Flete.HoraSalida": { required: "Por favor, seleccione una hora de salida." },
                "Flete.Id_empresa": { required: "Por favor, seleccione una línea fletera." },
                "Flete.id_chofer": { required: "Por favor, seleccione, un chofer. " },
                "Flete.id_vehiculo": { required: "Por favor, seleccione un vehículo." },
                //"Flete.kmInicialVehiculo": { required: "Por favor, ingrese el kilometraje inicial, puede ser 0.", min: "El kilometraje inicial debe ser mayor o igual a 0." },
                "Flete.precioFlete": { required: "Por favor, ingrese un precio del flete, puede ser 0.", min: "El precio del flete debe ser mayor o igual a 0." },
                "Flete.Trayecto.id_lugarOrigen": { required: "Por favor, seleccione un lugar de origen." },
                "Flete.Trayecto.id_lugarDestino": { required: "Por favor, seleccione un lugar de destino." },
                //"Flete.MetodoPago.Clave": { required: "Por favor, seleccioe un método de pago." },
                //"Flete.FormaPago.Clave": { min: "Por favor, seleccione una forma de pago." },
                "Flete.FechaTentativaEntrega": { required: "Por favor, seleccione una fecha tentativa del flete." },
                TipoVenta: { required: "Por favor, seleccione un tipo de venta." }
            },
            invalidHandler: function (event, validator) { //display error alert on form submit
                successHandler1.hide();
                errorHandler1.show();
                //$("#validation_summary").text(validator.showErrors());
            },
            highlight: function (element) {
                $(element).closest('.help-block').removeClass('valid');
                // display OK icon
                $(element).closest('.controlError').removeClass('has-success').addClass('has-error').find('.symbol').removeClass('ok').addClass('required');
                // add the Bootstrap error class to the control group
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.controlError').removeClass('has-error');
                // set error class to the control group
            },
            success: function (label, element) {
                label.addClass('help-block valid');
                label.removeClass('color');
                // mark the current input as valid and display OK icon
                $(element).closest('.controlError').removeClass('has-error').addClass('has-success').find('.symbol').removeClass('required').addClass('ok');
            },
            submitHandler: function (form) {
                successHandler1.show();
                errorHandler1.hide();
                form.submit();
            }
        });
    }
    function CalculateAndDisplayRoute(directionsService, directionsDisplay) {
        var selectIndexInicio = document.getElementById('Flete_Trayecto_id_lugarOrigen').selectedIndex;
        var optionInicio = document.getElementById('Flete_Trayecto_id_lugarOrigen').options.item(selectIndexInicio);
        var latitudInicial = optionInicio.dataset.latitud.replace(",", ".");
        var longInicial = optionInicio.dataset.longitud.replace(",", ".");

        var selectIndexFinal = document.getElementById('Flete_Trayecto_id_lugarDestino').selectedIndex;
        var optionFinal = document.getElementById('Flete_Trayecto_id_lugarDestino').options.item(selectIndexFinal);
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
    function GetListadoLugaresCliente(Id_cliente) {
        $.ajax({
            url: '/Admin/Venta/GetListadoLugaresCliente/',
            type: "POST",
            dataType: 'json',
            data: { Id_cliente: Id_cliente },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {

                $("#Flete_Trayecto_id_lugarDestino option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Flete_Trayecto_id_lugarDestino").append('<option value="' + result[i].id_lugar + '" data-latitud="' + result[i].latitud + '" data-longitud="' + result[i].longitud + '">' + result[i].descripcion + '</option>');
                }
            }
        });
    }
    function GetEmpresas(opcion_flete) {
        $.ajax({
            url: '/Admin/Venta/GetEmpresas/',
            type: "POST",
            dataType: 'json',
            data: { opcion: opcion_flete },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {

                $("#Flete_Id_empresa option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Flete_Id_empresa").append('<option value="' + result[i].IDEmpresa + '">' + result[i].RazonFiscal + '</option>');
                }
                $('#Flete_Id_empresa.select').selectpicker('refresh');

                var Id_sucursal = $("#Id_sucursal").val();
                var Id_empresa = $("#Flete_Id_empresa").val();

                GetChoferesXIDEmpresa(Id_empresa, Id_sucursal);
                GetVehiculosXIDEmpresa(Id_empresa, Id_sucursal);
                GetLugaresXIDEmpresa(Id_empresa);

            }
        });
    }
    function GetClientesXIDSucursal(Id_sucursal) {
        $.ajax({
            url: '/Admin/Venta/GetClientesXIdSucursal/',
            type: "POST",
            dataType: 'json',
            data: { Id_sucursal: Id_sucursal },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {

                $("#Id_cliente option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Id_cliente").append('<option value="' + result[i].IDCliente + '">' + result[i].NombreRazonSocial + '</option>');
                }
                $('#Id_cliente.select').selectpicker('refresh');



            }
        });
    }
    function GetChoferesXIDEmpresa(Id_empresa, Id_sucursal) {
        $.ajax({
            url: '/Admin/Venta/GetChoferesXIDEmpresa/',
            type: "POST",
            dataType: 'json',
            data: { IDEmpresa: Id_empresa, Id_sucursal: Id_sucursal },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {

                $("#Flete_id_chofer option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Flete_id_chofer").append('<option value="' + result[i].IDChofer + '">' + result[i].Nombre + '</option>');
                }
                $('#Flete_id_chofer.select').selectpicker('refresh');


                
            }
        });
    }
    function GetVehiculosXIDEmpresa(Id_empresa, Id_sucursal) {
        $.ajax({
            url: '/Admin/Venta/GetVehiculosXIDEmpresa/',
            type: "POST",
            dataType: 'json',
            data: { IDEmpresa: Id_empresa, Id_sucursal: Id_sucursal },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {
                $('#Flete_id_vehiculo').empty();
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
                        $("#Flete_id_vehiculo").append(option);
                        //Creamos un group nuevo
                        option = '<optgroup label="' + result[i].Modelo + '"><option value="' + result[i].IDVehiculo + '">' + result[i].nombreMarca + '</option>';
                        optgroup = result[i].Modelo;
                    }
                }
                //Anexamos el último valor
                option += '</optgroup>';
                $("#Flete_id_vehiculo").append(option);
                $('#Flete_id_vehiculo.select').selectpicker('refresh');
            }
        });
    }
    function GetLugaresXIDEmpresa(Id_empresa) {
        $.ajax({
            url: '/Admin/Venta/GetListadoLugaresEmpresa/',
            type: "POST",
            dataType: 'json',
            data: { IDEmpresa: Id_empresa },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {
                
                $("#Flete_Trayecto_id_lugarOrigen option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Flete_Trayecto_id_lugarOrigen").append('<option value="' + result[i].id_lugar + '" data-latitud="' + result[i].latitud + '" data-longitud="' + result[i].longitud + '">' + result[i].descripcion + '</option>');
                }
            }
        });
    }
    function ToggleDivFlete(opcion, recargarDatosServer) {

        if(recargarDatosServer == 1)
            GetEmpresas(opcion);

        if (opcion == 1 || opcion == 2) {
            $('#divFlete').show(1000);
            AgregarValidaciones();
        }
        
        else if(opcion == 0 || opcion == 3 || opcion === ''){
            $('#divFlete').hide(1000);
            QuitarValidaciones();
        }

    }
    function AgregarValidaciones() {
        FechaEmbarque.rules("add", { required: true });
        HoraEmbarque.rules("add", { required: true });
        FechaSalida.rules("add", { required: true });
        HoraSalida.rules("add", { required: true });
        Id_empresa.rules("add", { required: true });
        Id_chofer.rules("add", { required: true });
        Id_vehiculo.rules("add", { required: true });
        //KmInicialVehiculo.rules("add", { required: true, min: 0 });
        PrecioFlete.rules("add", { required: true, min: 0 });
        ListaLugarOrigen.rules("add", { required: true });
        ListaLugarDestino.rules("add", { required: true });
        FechaTentativaFlete.rules("add", { required: true });
        //FormaPago.rules("add", { min: 1 });
        //MetodoPago.rules("add", { required: true });

    }
    function QuitarValidaciones() {
        FechaEmbarque.rules("remove", "required");
        HoraEmbarque.rules("remove", "required");
        FechaSalida.rules("remove", "required");
        HoraSalida.rules("remove", "required");
        Id_empresa.rules("remove", "required");
        Id_chofer.rules("remove", "required");
        Id_vehiculo.rules("remove", "required");
        //KmInicialVehiculo.rules("remove", "required min");
        PrecioFlete.rules("remove", "required min");
        ListaLugarOrigen.rules("remove", "required");
        ListaLugarDestino.rules("remove", "required");
        FechaTentativaFlete.rules("remove", "required");
        //FormaPago.rules("remove", "min");
       // MetodoPago.rules("remove", "required" );

        FechaEmbarque.closest(".controlError").removeClass("has-success has-error");
        HoraEmbarque.closest(".controlError").removeClass("has-success has-error");
        FechaSalida.closest(".controlError").removeClass("has-success has-error");
        HoraSalida.closest(".controlError").removeClass("has-success has-error");
        Id_empresa.closest(".controlError").removeClass("has-success has-error");
        Id_chofer.closest(".controlError").removeClass("has-success has-error");
        Id_vehiculo.closest(".controlError").removeClass("has-success has-error");
        //KmInicialVehiculo.closest(".controlError").removeClass("has-success has-error");
        PrecioFlete.closest(".controlError").removeClass("has-success has-error");
        ListaLugarOrigen.closest(".controlError").removeClass("has-success has-error");
        ListaLugarDestino.closest(".controlError").removeClass("has-success has-error");
        FechaTentativaFlete.closest(".controlError").removeClass("has-success has-error");
        //FormaPago.closest(".controlError").removeClass("has-success has-error");
        //MetodoPago.closest(".controlError").removeClass("has-success has-error");

        Validation_summary.find("dd[for='Flete_Id_empresa']").addClass('help-block valid').text('');
        Validation_summary.find("dd[for='Flete_id_chofer']").addClass('help-block valid').text('');
        Validation_summary.find("dd[for='Flete_id_vehiculo']").addClass('help-block valid').text('');
        Validation_summary.find("dd[for='Flete_Trayecto_ListaLugarOrigen']").addClass('help-block valid').text('');
        Validation_summary.find("dd[for='Flete_Trayecto_ListaLugarDestino']").addClass('help-block valid').text('');
        Validation_summary.find("dd[for='Flete_FechaEmbarque']").addClass('help-block valid').text('');
        Validation_summary.find("dd[for='Flete_HoraEmbarque']").addClass('help-block valid').text('');
        Validation_summary.find("dd[for='Flete_FechaSalida']").addClass('help-block valid').text('');
        //Validation_summary.find("dd[for='Flete_kmInicialVehiculo']").addClass('help-block valid').text('');
        Validation_summary.find("dd[for='Flete_precioFlete']").addClass('help-block valid').text('');
        Validation_summary.find("dd[for='Flete_FechaTentativaEntrega']").addClass('help-block valid').text('');
        //Validation_summary.find("dd[for='Flete.FormaPago.Clave']").addClass('help-block valid').text('');
        //Validation_summary.find("dd[for='Flete.MetodoPago.Clave']").addClass('help-block valid').text('');
    }

   
    /*TERMINA FLETE*/
        return {
            init: function (opcion) {
            $.ajax({
                url: "http:www.google.com",
                context: document.body,
                error: function (jqXHR, exception) {
                    console.log("offline");
                },
                success: function () {
                    console.log("online");
                    InitMap();
                }
            });
            
            Validaciones();
            Eventos();

            
            ToggleDivFlete(opcion,0);
        }
    };
}();