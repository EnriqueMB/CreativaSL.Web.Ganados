var Compra = function () {
    "use strict"
    var tableGanado;

    var InitMap = function (option) {
        if (option == 2){
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

            CalculateAndDisplayRoute(directionsService, directionsDisplay);
        }
    };
    var LoadItems = function () {
        $('#FechaHoraProgramada').datepicker({
            format: 'dd/mm/yyyy',
            language: 'es'
        });

    };
    //Eventos
    var RunEventsLineaFletera = function () {
        $("#IDEmpresa").on("change", function () {
            var IDEmpresa = $(this).val();
            GetChoferesXIDEmpresa(IDEmpresa);
            GetVehiculosXIDEmpresa(IDEmpresa);
            GetJaulasXIDEmpresa(IDEmpresa);
            GetRemolquesXIDEmpresa(IDEmpresa);
        });
    }
    var RunEventsFechaProgramada = function () {
        $("#IDProveedor").on("change", function () {
            var IDProveedor = $(this).val();
            GetLugaresProveedorXIDProveedor(IDProveedor);
        });
    }
    var RunEventsGanado = function (IDProveedor, NombreProveedor) {
        $("#btnListadoPrecio").on("click", function () {
            ModalListadoPrecios(IDProveedor, NombreProveedor);
        });
    }
    //Validaciones
    var LoadValidationProveedor = function () {
        var form1 = $('#frmCreateCompra');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        $('#frmProveedor').validate({ // initialize the plugin
            //debug: true,
            errorElement: "li",
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
                form.submit();
            }
        });
    };
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
                    min:1
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
    var LoadValidationDocumentos = function () {
        var form1 = $('#frmDocumentos');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        $('#frmDocumentos').validate({ // initialize the plugin
            errorElement: "li",
            errorClass: 'text-danger',
            errorLabelContainer: $("#validation_summary_documentos"),
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
                GuiaTransito: {
                    required: true,
                    maxlength: 15
                },
                CertZoosanitario: {
                    maxlength: 15
                },
                CertBrucelosis: {
                    maxlength: 15
                },
                CertTuberculosis: {
                    maxlength: 15
                }
            },
            messages: {
                GuiaTransito: {
                    required: "-Introduzca un número de gruia.",
                    maxlength: jQuery.validator.format("-Guia Transito tiene una longitud máxima de caracteres: {0}")
                },
                CertZoosanitario: {
                    maxlength: jQuery.validator.format("-Cert. Zoosanitario tiene una longitud máxima de caracteres: {0}")
                },
                CertBrucelosis: {
                    maxlength: jQuery.validator.format("-Cert. Brucelosis tiene una longitud máxima de caracteres: {0}")
                },
                CertTuberculosis: {
                    maxlength: jQuery.validator.format("-Cert. Tuberculosis tiene una longitud máxima de caracteres: {0}")
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
                A_Documento();
            }
        });
    };
    var LoadValidationGanado = function () {
        var form1 = $('#frmGanado');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        $('#frmGanado').validate({ // initialize the plugin
            debug: true,
            errorElement: "li",
            errorClass: 'text-danger',
            errorLabelContainer: $("#validation_summary_ganado"),
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
                "Ganado.numArete": {
                    required: true,
                    maxlength: 15
                   
                },
                "CompraGanado.PesoInicial": {
                    required: true,
                    min: 1,
                    digits: true
                },
                "CompraGanado.TotalPagado": {
                    group: true, group: ["CompraGanado_TotalPagado", "CompraGanado_TotalSugerido", "Algunos de estos 2 campos: Total Pagado o Total Sugerido deben ser mayores a 0."]
                },
                "CompraGanado.TotalSugerido": {
                    group: true, group: ["CompraGanado_TotalPagado", "CompraGanado_TotalSugerido", "Algunos de estos 2 campos: Total Pagado o Total Sugerido deben ser mayores a 0."]
                }
            },
            messages: {
                "Ganado.numArete": {
                    required: "-Introduzca un número de arete.",
                    maxlength: jQuery.validator.format("-Número de arete debe tener una longitud máxima de {0} caracteres.")
                },
                "CompraGanado.PesoInicial": {
                    required: "-Introduzca un peso inicial.",
                    min: "-Peso Inicial debe ser mayor a 0.",
                    digits: "-Peso Inicial: Solo numeros enteros."
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
                AC_Ganado();
            }
        });
    };
    //Tablas
    var LoadTableGanado = function (IDCompra, jsonPreciosPeso, Merma) {
        tableGanado = $('#GanadoXCompraGanado').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "IDCompra": IDCompra
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
                    ModalGanado(idGanado, jsonPreciosPeso, Merma, IDCompra);
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
            ModalGanado(0, jsonPreciosPeso, Merma, IDCompra);
        });
    };
    //Modales
    function ModalListadoPrecios(IDProveedor, NombreProveedor) {
        $.ajax({
            url: '/Admin/Compra/ModalListadoPrecios/',
            type: "POST",
            data: { IDProveedor: IDProveedor, NombreProveedor: NombreProveedor},
            success: function (data) {
                $('#ContenidoModalListadoPrecios').html(data);
                $('#ModalListadoPrecios').modal({ backdrop: 'static', keyboard: false });
            }
        });
    }
    function ModalGanado(IDGanado, jsonPreciosPeso, MermaSucursal, IDCompra) {
        $.ajax({
            url: '/Admin/Compra/ModalGanado/',
            type: "POST",
            data: { IDGanado: IDGanado, Merma: MermaSucursal, IDCompra: IDCompra },
            success: function (data) {
                $('#ContenidoModalGanado').html(data);
                $('#ModalGanado').modal({ backdrop: 'static', keyboard: false });
                LoadValidationGanado();
                EventsModalGanado(jsonPreciosPeso, MermaSucursal);
            }
        });
    }
    //Funciones
    function A_Documento() {
        var form = $("#frmDocumentos")[0];
        var formData = new FormData(form);

        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/Compra/A_Documentos/',
            contentType: false,
            processData: false,
            cache: false,
            error: function (response) {
                Mensaje(response.Mensaje, "2");
            },
            success: function (response) {
                if (response.Success) {
                    Mensaje("Registro guardado con éxito.", "1");
                }
                else
                    Mensaje(response.Mensaje, "2");
            }
        });
    }
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
    function AC_Ganado() {
        var form = $("#frmGanado")[0];
        var formData = new FormData(form);

        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/Compra/AC_Ganado/',
            contentType: false,
            processData: false,
            cache: false,
            error: function (response) {
                Mensaje(response.Mensaje, "2");
            },
            success: function (response) {
                if (response.Success) {
                    Mensaje(response.Mensaje, "1");
                    tableGanado.ajax.reload();
                }
                else
                    Mensaje(response.Mensaje, "2");
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
    function CalculateAndDisplayRoute(directionsService, directionsDisplay) {
        var selectIndexInicio = document.getElementById('Trayecto.id_lugarOrigen').selectedIndex;
        var optionInicio = document.getElementById('Trayecto.id_lugarOrigen').options.item(selectIndexInicio);
        var latitudInicial = optionInicio.dataset.latitud.replace(",", ".");
        var longInicial = optionInicio.dataset.longitud.replace(",", ".");

        var selectIndexFinal = document.getElementById('Trayecto.id_lugarDestino').selectedIndex;
        var optionFinal = document.getElementById('Trayecto.id_lugarDestino').options.item(selectIndexFinal);
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
    //Calculo de precio y peso de ganado
    function EventsModalGanado(jsonPrecioPeso, MermaSucursal) {
        var pesoInicial  = $("#CompraGanado_PesoInicial").val();
        var pesoFinal    = $("#CompraGanado_PesoFinal").val();
        var pesoPagado   = $("#CompraGanado_PesoPagado").val();
        var kiloPagado   = $("#CompraGanado_PrecioKilo").val();

        $("#Ganado_genero").on("change", function () {
            if ($('#Ganado_Repeso').is(":checked")) {
                calculosSugeridos(true, jsonPrecioPeso, MermaSucursal, pesoInicial, pesoFinal);
                calculoPesoKiloPagado();
            } else {
                calculosSugeridos(false, jsonPrecioPeso, MermaSucursal, pesoInicial, pesoFinal);
                calculoPesoKiloPagado();
            }
        });
        $('#Ganado_Repeso').on("change", function () {
            $('.Esconder').toggle(1000);
            if ($('#Ganado_Repeso').is(":checked")) {
                calculosSugeridos(true, jsonPrecioPeso, MermaSucursal, pesoInicial, pesoFinal);
                calculoPesoKiloPagado();
            } else {
                calculosSugeridos(false, jsonPrecioPeso, MermaSucursal, pesoInicial, pesoFinal);
                calculoPesoKiloPagado();
            }
        });
        $("#CompraGanado_PesoInicial").on("keyup keydown", function (e) {
            $(this).val($(this).val().replace(/[^\d].+/, ""));

            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57) && (e.which < 96 || e.which > 105)) {
                e.preventDefault();
            }
            else {
                    pesoInicial = $(this).val();
                if ($('#Ganado_Repeso').is(":checked")) {
                    calculosSugeridos(true, jsonPrecioPeso, MermaSucursal, pesoInicial, pesoFinal);
                    calculoPesoKiloPagado();
                }
                else{
                    calculosSugeridos(false, jsonPrecioPeso, MermaSucursal, pesoInicial, pesoFinal);
                    calculoPesoKiloPagado();
                }
            }
        });
        $("#CompraGanado_PesoFinal").on("keyup keydown", function (e) {
            $(this).val($(this).val().replace(/[^\d].+/, ""));

            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57) && (e.which < 96 || e.which > 105)) {
                e.preventDefault();
            }
            else {
                    pesoFinal = $(this).val();
                if ($('#Ganado_Repeso').is(":checked")) {
                    calculosSugeridos(true, jsonPrecioPeso, MermaSucursal, pesoInicial, pesoFinal);
                    calculoPesoKiloPagado();
                }
                else {

                    calculosSugeridos(false, jsonPrecioPeso, MermaSucursal, pesoInicial, pesoFinal);
                    calculoPesoKiloPagado();
                }
            }
        });
        $("#CompraGanado_PesoPagado").on("keyup keydown", function (e) {
            $(this).val($(this).val().replace(/[^\d].+/, ""));

            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57) && (e.which < 96 || e.which > 105)) {
                e.preventDefault();
            }
            else {
                calculoPesoKiloPagado();
            }
        });
        $("#CompraGanado_PrecioKilo").on("keyup keydown", function (e) {
            $(this).val($(this).val().replace(/[^0-9\.]/g, ''));

            if (e.which != 8 && e.which != 0 && ($(this).val().indexOf('.') != -1) && (e.which < 48 || e.which > 57) && ((e.which < 96 || e.which > 105))) {
                e.preventDefault();
            }
            else {
                calculoPesoKiloPagado();
            }
        });
    }
    function calculoPesoKiloPagado() {
        var CompraGanado_PrecioKilo = parseFloat($("#CompraGanado_PrecioKilo").val().trim());
        var CompraGanado_PesoPagado = parseFloat($("#CompraGanado_PesoPagado").val().trim());

        if (CompraGanado_PrecioKilo == 0 && CompraGanado_PesoPagado == 0) {
            //console.log("tiene 0 ambos");
            $("#CompraGanado_TotalPagado").val(0);
        } else if (isNaN(CompraGanado_PrecioKilo) && isNaN(CompraGanado_PesoPagado)) {
            //console.log("ambos NaN");
            $("#CompraGanado_TotalPagado").val(0);
        }
        else if (CompraGanado_PrecioKilo > 0 && CompraGanado_PesoPagado > 0) {
            //console.log("tiene valores diferentes a 0, tienen prioridad así que pasan directo");
            $("#CompraGanado_TotalPagado").val(CompraGanado_PrecioKilo * CompraGanado_PesoPagado);
        }
        else if (CompraGanado_PesoPagado > 0 && (CompraGanado_PrecioKilo == 0 || isNaN(CompraGanado_PrecioKilo))) {
            //console.log("Validamos Peso pagado");
            var CompraGanado_PrecioSugeridoXkilo = $("#CompraGanado_PrecioSugeridoXkilo").val();
            $("#CompraGanado_TotalPagado").val(CompraGanado_PesoPagado * CompraGanado_PrecioSugeridoXkilo);
        }
        else if (CompraGanado_PrecioKilo > 0 && (CompraGanado_PesoPagado == 0 || isNaN(CompraGanado_PesoPagado))) {
            //console.log("Validamos kilo pagado");
            var CompraGanado_PesoSugerido = $("#CompraGanado_PesoSugerido").val();
            $("#CompraGanado_TotalPagado").val(CompraGanado_PrecioKilo * CompraGanado_PesoSugerido);
        }
    }
    function calculoKiloPagado() {
        var CompraGanado_PrecioKilo = $("#CompraGanado_PrecioKilo").val().trim();
        var CompraGanado_PesoPagado = $("#CompraGanado_PesoPagado").val().trim();

        //Checamos primero el pesoPagado (introducido por el usuario), tiene mayor prioridad
        if ((CompraGanado_PesoPagado.length == 0 || CompraGanado_PesoPagado == undefined || CompraGanado_PesoPagado == 0) && (CompraGanado_PrecioKilo.length == 0 || CompraGanado_PrecioKilo == undefined || CompraGanado_PrecioKilo == 0)) {
            console.log("CompraGanado_PesoPagado: " + CompraGanado_PesoPagado);

            //Si no tiene algún peso por el usuario, checamos en peso sugerido (sistema)
            var CompraGanado_PesoSugerido = $("#CompraGanado_PesoSugerido").val().trim();
            console.log("PesoSugerido: " + CompraGanado_PesoSugerido);
            //Si tampoco tiene algún dato, ponemos en total pagado 0
            if (CompraGanado_PesoSugerido.length == 0 || CompraGanado_PesoSugerido == undefined) {
                $("#CompraGanado_TotalPagado").val(0);
            }
            else {

                if (CompraGanado_PesoPagado.length == 0 || CompraGanado_PesoPagado == undefined) {
                    $("#CompraGanado_PesoPagado").val(0);
                    $("#CompraGanado_TotalPagado").val(0);
                }
                else {
                    $("#CompraGanado_TotalPagado").val(CompraGanado_PesoSugerido * CompraGanado_PesoPagado);
                }
            }
        }
        else {
            //Si tiene algún precio 
            console.log("***************************************");
            console.log("calculoKiloPagado");
            console.log("PrecioKilo: " + CompraGanado_PrecioKilo);
            console.log("PrecioPagado: " + CompraGanado_PesoPagado);
            console.log("***************************************");
            $("#CompraGanado_TotalPagado").val(CompraGanado_PrecioKilo * CompraGanado_PesoPagado);
        }
       
    }
    function calculosSugeridos(checked, jsonPrecioPeso, MermaSucursal, pesoInicial, pesoFinal) {
        var inputDiferenciaPeso = $("#CompraGanado_DiferenciaPeso");
        var inputMermaObtenida = $("#CompraGanado_Merma");
        var inputPesoSugerido = $("#CompraGanado_PesoSugerido");
        var inputPrecioSugerido = $("#CompraGanado_PrecioSugeridoXkilo");
        var inputTotalSugerido = $("#CompraGanado_TotalSugerido");
        var selectGenero = $("#Ganado_genero").val();
        
        var MermaObtenida, PesoSugerido, PrecioSugerido;

        if (checked) {
            inputDiferenciaPeso.val(diferenciaPeso(pesoInicial, pesoFinal));
            MermaObtenida = mermaGenerada(pesoInicial, pesoFinal);
            inputMermaObtenida.val(MermaObtenida);
            PesoSugerido = pesoSugerido(pesoInicial, pesoFinal, MermaSucursal, MermaObtenida);
            inputPesoSugerido.val(PesoSugerido);
            PrecioSugerido = precioSugerido(jsonPrecioPeso, PesoSugerido, selectGenero)
            inputPrecioSugerido.val(PrecioSugerido);
            inputTotalSugerido.val(PrecioSugerido * PesoSugerido);
        }
        else {
            inputPesoSugerido.val(pesoInicial);
            PesoSugerido = precioSugerido(jsonPrecioPeso, pesoInicial, selectGenero);
            inputPrecioSugerido.val(PesoSugerido);
            inputTotalSugerido.val(pesoInicial * PesoSugerido); 
        }
        
    }
    function precioSugerido(jsonPrecioPeso, PesoSugerido, selectGenero) {
        var selectGenero = (selectGenero == "True") ? true : false;
        for (var item in jsonPrecioPeso) {
            if (jsonPrecioPeso[item].esMacho == selectGenero) {
                if ((jsonPrecioPeso[item].pesoMinimo <= PesoSugerido) && (PesoSugerido <= jsonPrecioPeso[item].pesoMaximo)) {
                    return jsonPrecioPeso[item].precio;
                }
            }
        }
        return 0;
    }
    function pesoSugerido(pesoInicial, pesoFinal, MermaSucursal, MermaObtenida) {
        if (MermaObtenida > MermaSucursal) {
            var peso = pesoInicial - (pesoInicial * (MermaSucursal / 100));
            return peso;
        }
        else {
            return pesoInicial;
        }
    }
    function mermaGenerada(pesoInicial, pesoFinal) {
        var mermaGenerada = (((pesoFinal * 100) / pesoInicial) - 100)*(-1);
        mermaGenerada = Math.abs(mermaGenerada.toFixed(2));
        return mermaGenerada;
    }
    function diferenciaPeso(pesoInicial, pesoFinal) {
       return (pesoFinal - pesoInicial);
    }
   
    
   
    var LoadTableMovimientos = function (idCompra) {
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

    function ModalCobro(idDocCobrar) {
        $.ajax({
            url: "ModalCobro",
            type: "POST",
            data: { idDocCobrar: idDocCobrar },
            success: function (data) {
                $("#ContenidoModalCobro").html(data);
                $("#ModalCobro").modal({ backdrop: "static", keyboard: false });

            }
        })
    }
    function ModalPago(idDocPagar) {
        $.ajax({
            url: "ModalPago",
            type: "POST",
            data: { idDocPagar: idDocPagar },
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
        init: function (option, IDCompra, PrecioPeso, merma, IDProveedor, NombreProveedor) {
            LoadItems();
            LoadValidationProveedor();
            InitMap(option);
            
            LoadValidationFlete();
            LoadValidationDocumentos();
            RunEventsLineaFletera();
            RunEventsFechaProgramada();
            RunEventsGanado(IDProveedor, NombreProveedor);


            LoadTableGanado(IDCompra, PrecioPeso, merma);
            LoadTableMovimientos(IDCompra);
            LoadTableEvento(IDCompra);
        }
    };
}();