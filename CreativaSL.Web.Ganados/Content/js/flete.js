var Flete = function () {
    "use strict"
    var lugarOrigen = document.getElementById('Trayecto_LugarOrigen_id_lugar');
    var lugarDestino = document.getElementById('Trayecto_LugarDestino_id_lugar');
    var tableImpuesto, map, directionsDisplay, directionsService;
    var tableDocumentos, tableProductoGanado, tableProductoGeneral, tblModalGanadoActual;

    var InitMap = function () {
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
    };
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
    var RunEventsCobro= function () {
        $("#precioFlete").on("keyup keydown", function (e) {
            var value = $(this).val();
            if (!isNaN(value)) {
                $("#TotalFlete").val(value);
            }
        });
    }
    var RunEventsDocumento = function (imagen) {
        $('#ImagenPost').fileinput({
            theme: 'fa',
            language: 'es',
            showRemove: false,
            showClose: false,
            showUpload: false,
            uploadUrl: "#",
            autoReplace: true,
            overwriteInitial: true,
            showUploadedThumbs: false,
            maxFileCount: 1,
            initialPreview: [
                '<img class="file-preview-image" style="width: auto; height: auto; max-width: 100%; max-height: 100%;" src="data:image/png;base64,' + imagen + '" />'
            ],
            initialPreviewConfig: [
                { caption: 'Imagen del documento' }
            ],
            initialPreviewShowDelete: false,
            showRemove: false,
            showClose: false,
            layoutTemplates: { actionDelete: '' },
            allowedFileExtensions: ["png"],
            required: true
        })
    }
    var RunEventsFleteimpuesto = function () {
        var option = $("#TipoFactor_Clave").val();
        
        TipoFactor(option);

        $("#TipoFactor_Clave").on("change", function () {
            var option = $(this).val();
            TipoFactor(option);
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
    var LoadValidation_AC_Cobro = function () {
        var form1 = $('#frm_AC_Cobro');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({ // initialize the plugin
            //debug: true,
            errorElement: "li",
            errorClass: 'text-danger',
            errorLabelContainer: $("#validation_summary_AC_Cobro"),
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
                precioFlete: {
                    required: true,
                    decimal: true
                },
                CondicionPago: {
                    required: true
                },
                "MetodoPago.Clave": {
                    required: true
                },
                "FormaPago.Clave": {
                    required: true,
                    min: 1
                }
            },
            messages: {
                precioFlete: {
                    required: "-Introduzca un precio del flete.",
                    decimal: "-El precio del flete debe ser solo números."
                },
                CondicionPago: {
                    required: "-Introduzca una condición de pago."
                },
                "MetodoPago.Clave": {
                    required: "-Seleccion un método de pago."
                },
                "FormaPago.Clave": {
                    required: "-Seleccione una forma de pago.",
                    min: "-Seleccione una forma de pago."
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
                AC_Cobro();
            }
        });
    };
    var LoadValidation_AC_Documento = function () {
        var form1 = $('#frm_AC_Documentos');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({ // initialize the plugin
            //debug: true,
            errorElement: "li",
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
                },
                ImagenPost: {
                    validarImgEdit2: true, validarImgEdit2: ["FlagImg"],
                    formatoPNG:true
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
    var LoadValidationFleteImpuesto = function () {
        var form1 = $('#frm_AC_FleteImpuesto');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({ // initialize the plugin
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
    var LoadTableImpuesto = function () {
        var IDFlete = $("#id_flete").val();

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
                {
                    "data": "base",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
                { "data": "tasaCuota" },
                {
                    "data": "importe",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
                {
                    "data": null,
                    "render": function (data, type, full) {

                        return "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-idflete='" + full["IDFlete"] + "' data-id='" + full["IDFleteImpuesto"] + "' class='btn btn-yellow tooltips btn-sm editImpuesto' title='Editar'  data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                            "<a data-hrefa='/Admin/FleteImpuesto/DEL_FleteImpuesto/' title='Eliminar' data-id='" + full["IDFleteImpuesto"] + "' class='btn btn-danger tooltips btn-sm deleteImpuesto' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
                            "</div>" +
                            "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                            "<div class='btn-group'>" +
                            "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                            "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                            "</a>" +
                            "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                            "<li>" +
                            "<a data-idflete='" + full["IDFlete"] + "' class='editImpuesto' data-id='" + full["IDFleteImpuesto"] + "'  role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
                            "</a>" +
                            "</li>" +
                            "<li>" +
                            "<a data-hrefa='/Admin/FleteImpuesto/DEL_FleteImpuesto/' class='deleteImpuesto' role='menuitem' tabindex='-1' data-id='" + full["IDFleteImpuesto"] + "'>" +
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
                    var IDFlete = $(this).data("idflete");
                    var IDFleteImpuesto = $(this).data("id");

                    ModalImpuesto(IDFlete, IDFleteImpuesto);
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
                            data: { IDFleteImpuesto: row },
                            type: 'POST',
                            dataType: 'json',
                            success: function (result) {
                                if (result.Success) {
                                    box.find(".mb-control-yes").prop('onclick', null).off('click');
                                    Mensaje("Impuesto eliminado con éxito.", "1");
                                    //Recogo los valores
                                    var json = JSON.parse(result.Mensaje);
                                    $("#TotalFlete").val(json.totalFlete);
                                    $("#TotalImpuestoTrasladado").val(json.totalImpuestoTrasladados);
                                    $("#TotalImpuestoRetenido").val(json.totalImpuestoRetenido);
                                    $("#ModalImpuesto").modal('hide');
                                    tableImpuesto.ajax.reload();
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

        $("#btnAddImpuesto").on("click", function () {
            ModalImpuesto(IDFlete, 0);
        });
    };
    var LoadTableDocumentos = function () {
        var IDFlete = $("#id_flete").val();

        tableDocumentos = $('#tblDocumentos').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "IDFlete": IDFlete
                },
                "url": "/Admin/Flete/TableJsonDocumentos/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "descripcion" },
                { "data": "clave" },
                { "data":  null,
                    "render": function (data, type, full) {
                        return "<img class='file-preview-image' style='width: 150px; height: 150px;' src='data:image/png;base64," + full["imagen"] + "' />";
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

                    ModalDocumento(IDFlete, IDDocumento);
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

        $("#btnAddDocumento").on("click", function () {
            ModalDocumento(IDFlete, 0);
        });
    };
    var LoadTableProductos = function () {
        var IDFlete = $("#id_flete").val();

        tableProductoGanado = $('#tblProductoGanado').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "IDFlete": IDFlete
                },
                "url": "/Admin/Flete/TableJsonProductoGanadoXIDFlete/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "numArete" },
                { "data": "genero" },
                { "data": "pesoAproximado" },
                {
                    "data": null,
                    "render": function (data, type, full) {

                        return "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-hrefa='/Admin/Flete/C_DEL_ProductoGanado/' title='Eliminar' data-id='" + full["id_fleteProductoGanado"] + "' class='btn btn-danger tooltips btn-sm deleteProductoGanado' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
                            "</div>" +
                            "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                            "<div class='btn-group'>" +
                            "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                            "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                            "</a>" +
                            "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                            "<li>" +
                            "<a data-hrefa='/Admin/Flete/C_DEL_ProductoGanado/' class='deleteProductoGanado' role='menuitem' tabindex='-1' data-id='" + full["id_fleteProductoGanado"] + "'>" +
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
                $(".deleteProductoGanado").on("click", function () {
                    var url = $(this).attr('data-hrefa');
                    //id del ganado seleccionado
                    var idGanado = $(this).attr('data-id');
                    var box = $("#mb-deleteProductoGanado");

                    var idFlete = $("#id_flete").val();
                    var formdata = new FormData();
                    var listaGanado = new Array();
                    listaGanado.unshift(idGanado);
                    formdata.append("ganados", listaGanado);
                    formdata.append("opcion", 2);
                    formdata.append("idFlete", idFlete);

                    box.addClass("open");
                    box.find(".mb-control-yes").on("click", function () {
                        box.removeClass("open");
                        $.ajax({
                            url: url,
                            data: formdata,
                            type: 'POST',
                            processData: false,
                            contentType: false,
                            success: function (result) {
                                if (result.Success) {
                                    box.find(".mb-control-yes").prop('onclick', null).off('click');
                                    Mensaje("Producto eliminado con éxito.", "1");
                                    $("#ModalProducto").modal('hide');
                                    tableProductoGanado.ajax.reload();
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


        $("#btnAddProductoGanado").on("click", function () {
            ModalProducto(1);
        });
    };
   //Funcion Modal
    function ModalImpuesto(IDFlete, IDFleteImpuesto) {
        $.ajax({
            url: '/Admin/FleteImpuesto/ModalFleteImpuesto/',
            type: "POST",
            data: { IDFlete: IDFlete, IDFleteImpuesto: IDFleteImpuesto },
            success: function (data) {
                $('#ContenidoModalImpuesto').html(data);
                $('#ModalImpuesto').modal({ backdrop: 'static', keyboard: false });
                LoadValidationFleteImpuesto();
                RunEventsFleteimpuesto();
            }
        });
    }
    function ModalDocumento(IDFlete, IDDocumento) {
        $.ajax({
            url: '/Admin/Flete/ModalDocumento/',
            type: "POST",
            data: { IDFlete: IDFlete, IDDocumento: IDDocumento },
            success: function (data) {
                $('#ContenidoModalDocumento').html(data);
                $('#ModalDocumento').modal({ backdrop: 'static', keyboard: false });
                var imagen = $("#MostrarImagen").val();

                LoadValidation_AC_Documento();
                RunEventsDocumento(imagen);
            }
        });
    }
    function ModalProducto(opcion) {
        var url;
        if (opcion == 1)
            url = '/Admin/Flete/ModalProductoGanado/';
        else
            url = '/Admin/Flete/ModalProductoGeneral/';

        $.ajax({
            url: url,
            type: "POST",
            data: { },
            success: function (data) {
                $('#ContenidoModalProducto').html(data);
                $('#ModalProducto').modal({ backdrop: 'static', keyboard: false });
                if (opcion == 1) {
                    LoadTableGanadoActual();
                    RunEventProducto();
                }
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
                    Mensaje("Datos guardados con éxito.", "1");
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
    function AC_Cobro() {
        var form = $("#frm_AC_Cobro")[0];
        var formData = new FormData(form);

        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/Flete/A_Cobro/',
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
                    $("#TotalFlete").val(json.totalFlete);
                    $("#TotalImpuestoTrasladado").val(json.totalImpuestoTrasladados);
                    $("#TotalImpuestoRetenido").val(json.totalImpuestoRetenido);
                }
                else
                    Mensaje(response.Mensaje, "2");
            }
        });
    }
    function AC_Documento() {
        var form = $("#frm_AC_Documentos")[0];
        var formData = new FormData(form);

        $.ajax({
            type: 'POST',
            data: formData,
            url: '/Admin/Flete/AC_Documento/',
            contentType: false,
            processData: false,
            cache: false,
            error: function (response) {
                Mensaje(response.Mensaje, "2");
            },
            success: function (response) {
                if (response.Success) {
                    Mensaje("Datos guardados con éxito.", "1");
                    //var json = JSON.parse(response.Mensaje);
                    $("#ModalDocumento").modal('hide');
                    tableDocumentos.ajax.reload();
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
                    Mensaje("Datos guardados con éxito.", "1");
                    //Recogo los valores
                    var json = JSON.parse(response.Mensaje);
                    $("#TotalFlete").val(json.totalFlete);
                    $("#TotalImpuestoTrasladado").val(json.totalImpuestoTrasladados);
                    $("#TotalImpuestoRetenido").val(json.totalImpuestoRetenido);
                    $("#ModalImpuesto").modal('hide');
                    tableImpuesto.ajax.reload();
                }
                else
                    Mensaje(response.Mensaje, "2");
            }
        });
    }
    function TipoFactor(option) {
        var TasaCuota = $("#TasaCuota");
        var Importe = $("#Importe");

        if (option == 3) {
            TasaCuota.attr('readonly', true);
            TasaCuota.rules("remove", "required TasaCuotaSAT");
            TasaCuota.closest(".controlError").removeClass("has-success has-error");
            $("#validation_summary_AC_FleteImpuesto").find("dd[for='TasaCuota']").addClass('help-block valid').text('');
            TasaCuota.val("0");
            Importe.val("0");
        }
        else {
            TasaCuota.attr('readonly', false);
            TasaCuota.rules("add", { required: true, TasaCuotaSAT: true });
        }
    }
    function RunEventProducto() {
        //Seleccionar filas 
        $('#tblModalGanadoActual tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });
        $('#guardarProductoGanado').click(function () {
            var rows = tblModalGanadoActual.rows('.selected').data();
            var listaGanado = new Array();
            var idFlete = $("#id_flete").val();

            for (var i = 0; i < rows.length; i++) {
                var d = rows[i];
                listaGanado.unshift(d.id_ganado);
            }
            if (listaGanado.length > 0) {
                var formdata = new FormData();
                formdata.append('opcion', 1);
                formdata.append('ganados', listaGanado);
                formdata.append('idFlete', idFlete);

                $.ajax({
                    url: '/Admin/Flete/C_DEL_ProductoGanado/',
                    type: "POST",
                    processData: false,
                    contentType: false,
                    data: formdata,
                    error: function (result) {
                        Mensaje(result.Mensaje, "2");
                    },
                    success: function (result) {
                        Mensaje(result.Mensaje, "1");
                        $("#ModalProducto").modal('hide');
                        tableProductoGanado.ajax.reload();
                    }
                });
            }
        });
    }
    function LoadTableGanadoActual() {
        tblModalGanadoActual = $('#tblModalGanadoActual').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                   
                },
                "url": "/Admin/Flete/TableJsonGanadoActual/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                {
                    "data": "id_ganado",
                    "visible": false,
                    "searchable": false
                },
                { "data": "numArete" },
                { "data": "genero" },
                { "data": "pesoPagado" },
            ]
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
            //Hay un trayecto desbloqueamos el cobro
            document.getElementById("tabCobro").dataset.toggle = "tab";
            $('#tabCobro').data('toggle', "tab")
            $("#liCobro").removeClass('disabled').addClass('pestaña');
            //Documentos
            document.getElementById("tabDocumentacion").dataset.toggle = "tab";
            $('#tabDocumentacion').data('toggle', "tab")
            $("#liDocumentacion").removeClass('disabled').addClass('pestaña');
            //Productos
            document.getElementById("tabProducto").dataset.toggle = "tab";
            $('#tabProducto').data('toggle', "tab")
            $("#liProducto").removeClass('disabled').addClass('pestaña');
        }
    }
    return {
        init: function () {
            InitMap();
            DesbloquearTabs();
            RunEventsGeneral();
            RunEventsCobro();
            LoadValidation_AC_Cliente();
            LoadValidation_AC_Trayecto();
            LoadValidation_AC_Cobro();

            LoadTableImpuesto();
            LoadTableDocumentos();
            LoadTableProductos();
        }
    };
}();