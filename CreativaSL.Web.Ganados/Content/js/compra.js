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
                    ModalGanado(idGanado, jsonPreciosPeso, Merma);
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
            ModalGanado(0, jsonPreciosPeso, Merma);
        });
    };
    //Modales
    function ModalGanado(IDGanado, jsonPreciosPeso, MermaSucursal) {
        $.ajax({
            url: 'ModalGanado',
            type: "POST",
            data: { IDGanado: IDGanado, Merma: MermaSucursal },
            success: function (data) {
                $('#ContenidoModalGanado').html(data);
                $('#ModalGanado').modal({ backdrop: 'static', keyboard: false });
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

    function EventsModalGanado(jsonPrecioPeso, MermaSucursal) {
        var MermaSucursal = MermaSucursal;
        var jsonPrecioPeso = jsonPrecioPeso;

        var inputDiferenciaPeso = $("#CompraGanado_DiferenciaPeso");
        var inputMermaObtenida = $("#CompraGanado_Merma");
        var inputPesoSugerido = $("#CompraGanado_PesoSugerido");
        var inputPrecioSugerido = $("#CompraGanado_PrecioSugeridoXkilo");
        var selectGenero = $("#Ganado_genero").val();

        var pesoInicial = $("#CompraGanado_PesoInicial").val();
        var pesoFinal = $("#CompraGanado_PesoFinal").val();
        var MermaObtenida = inputMermaObtenida.val();
        var PesoSugerido = inputPesoSugerido.val();
        var PrecioSugerido = inputPrecioSugerido.val();

        $('#Ganado_Repeso').change(function () {
            $('.Esconder').toggle(1000);
            if ($('#Ganado_Repeso').is(":checked")) {
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
                        inputDiferenciaPeso.val(diferenciaPeso(pesoInicial, pesoFinal));
                        MermaObtenida = mermaGenerada(pesoInicial, pesoFinal);
                        inputMermaObtenida.val(MermaObtenida);
                        console.log(pesoInicial);
                        console.log(pesoFinal);
                        console.log(MermaSucursal);
                        console.log(MermaObtenida);
                        PesoSugerido = pesoSugerido(pesoInicial, pesoFinal, MermaSucursal, MermaObtenida);
                        inputPesoSugerido.val(PesoSugerido);

                        //PrecioSugerido = precioSugerido(jsonPrecioPeso, PesoSugerido, selectGenero);
                }
                else{
                     
                    console.log("directo");
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
                    inputDiferenciaPeso.val(diferenciaPeso(pesoInicial, pesoFinal));
                    MermaObtenida = mermaGenerada(pesoInicial, pesoFinal);
                    inputMermaObtenida.val(MermaObtenida);

                    PesoSugerido = pesoSugerido(pesoInicial, pesoFinal, MermaSucursal, MermaObtenida);
                    inputPesoSugerido.val(PesoSugerido);
                }
                else {

                    console.log("directo");
                }
            }
        });

    }

    function pesoSugerido(pesoInicial, pesoFinal, MermaSucursal, MermaObtenida) {
        if (MermaObtenida > MermaSucursal) {
            console.log((pesoInicial * (MermaSucursal / 100)));
            console.log(pesoInicial);
            var peso = pesoInicial - (pesoInicial * (MermaSucursal / 100));
            return peso;
        }
        else {
            return pesoInicial;
        }
    }
    function mermaGenerada(pesoInicial, pesoFinal) {
        var mermaGenerada = (((pesoFinal * 100) / pesoInicial) - 100)*(-1);
            mermaGenerada = mermaGenerada.toFixed(2);
        return mermaGenerada;
    }
    function diferenciaPeso(pesoInicial, pesoFinal) {
       return (pesoFinal - pesoInicial);
    }
    function calcularCompraGanado() {
        var repeso = $('#Ganado_Repeso').is(":checked");

        var inputGenero = $("#Ganado_genero");
        var inputPesoInicial = $("#CompraGanado_PesoInicial");
        var inputFinal = $("CompraGanado_PesoFinal");
        var genero = $("#Ganado_genero").val;
        var pesoInicial = $("#CompraGanado_PesoInicial").val;
        var pesoFinal = $("CompraGanado_PesoFinal").val;
        var mermaPermitida = $("Sucursal_MermaPredeterminada").val;
        
        if (repeso) {
            
        }
        else {

        }

        //    $("input").keyup(function () {

        //        var pesoInicial = document.getElementById("CompraGanado_PesoInicial").value;
        //        var val = this.value;
        //        pesoInicial.replace(/\D|\-/, '');

        //}).keyup();
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
        init: function (option, IDCompra, PrecioPeso, merma) {
            LoadItems();
            LoadValidationProveedor();
            InitMap(option);
            
            LoadValidationFlete();
            LoadValidationDocumentos();
            RunEventsLineaFletera();
           
            LoadTableGanado(IDCompra, PrecioPeso, merma);
            LoadTableMovimientos(IDCompra);
            LoadTableEvento(IDCompra);
        }
    };
}();