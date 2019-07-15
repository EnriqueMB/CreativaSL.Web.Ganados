var CompraAgendar = function () {
    "use strict";
    //google maps
    var map, directionsDisplay, directionsService;
    //datatables
    var tableDocumentos, tblGanadoProgramado;
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
    var nNodes, numeroFila = 1, listaFierros;
    var IDCompra = $("#IDCompra").val();
    var guardarIDs = new Array();
    var longitud_permitida_arete = 10;
    var Flete_kmInicial = $('#Flete_kmInicialVehiculo');
    var Monto = $('#DocumentosPorCobrarDetallePagos_Monto');

    var IMAGEN = 0, MENSAJE = 1, ARETE = 2, GENERO = 3, FIERRO1 = 4, FIERRO2 = 5, FIERRO3 = 6, BTN_ELIMINAR = 7, BTN_ELIMINAR_MIN = 8;


    /*INICIA PROVEEDOR*/
    var RunEventsProveedor = function () {
        //////INICIO -> SE AGREGA POR ACTUALIZACION 17/01/2019
        $("#IDSucursal").on("change", function () {
            var IDSucursal = $(this).val();
            var IDProveedor = $("#IDProveedor").val();
            GetProveedoresXIDSucursal(IDSucursal);
            GetLugaresProveedorXIDProveedor(IDProveedor);
            GetListadoCuentasBancariasProveedorXIDProveedor(IDProveedor);
        });
        //////FIN -> SE AGREGA POR ACTUALIZACION 17/01/2019

        $("#IDProveedor").on("change", function () {
            var IDProveedor = $(this).val();
            GetLugaresProveedorXIDProveedor(IDProveedor);
            GetListadoCuentasBancariasProveedorXIDProveedor(IDProveedor);
        });
        $('#FechaHoraProgramada').datepicker({
            format: 'dd/mm/yyyy',
            language: 'es'
        });
        $('#tabFlete').on("click", function () {
            CalculateAndDisplayRoute(directionsService, directionsDisplay, map);
        });
        //$('#Flete_kmInicialVehiculo').on({
        //    "focus": function (event) {
        //        $(event.target).select();
        //    },
        //    "keyup": function (event) {
        //        $(event.target).val(function (index, v) {
        //            var number = cpf(v);
        //            return number;
        //        });
        //    }
        //});
        $('#DocumentosPorCobrarDetallePagos_Monto').on({
            "focus": function (event) {
                $(event.target).select();
            },
            "keyup": function (event) {
                $(event.target).val(function (index, value) {
                    var number = cpf(value);
                    return number;
                });
            }
        });
        //$('#Flete_kmInicialVehiculo').val(cpf($('#Flete_kmInicialVehiculo').val()));
        $('#DocumentosPorCobrarDetallePagos_Monto').val(cpf($('#DocumentosPorCobrarDetallePagos_Monto').val()));
        $('.Hora24hrs').timepicker({
            minuteStep: 1,
            showMeridian: false
        });
    }
  
    function cpf(v) {
        v = v.replace(/([^0-9\.]+)/g, '');
        v = v.replace(/^[\.]/, '');
        v = v.replace(/[\.][\.]/g, '');
        v = v.replace(/\.(\d)(\d)(\d)/g, '.$1$2');
        v = v.replace(/\.(\d{1,2})\./g, '.$1');
        v = v.toString().split('').reverse().join('').replace(/(\d{3})/g, '$1,');
        v = v.split('').reverse().join('').replace(/^[\,]/, '');
        return v;
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
                IDProveedor: { required: true },
                IDSucursal: { required: true },
                IDPLugarProveedor: { required: true }
            },
            messages: {
                IDProveedor: {  required: "Por favor, seleccione un proveedor."  },
                IDSucursal: {   required: "Por favor, seleccione una Sucursal."  },
                IDPLugarProveedor: { required: "Por favor, seleccione un lugar del proveedor." }
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
        $('#btnSubmitProveedor').attr("disabled", true);
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
            },
            complete: function () {
                //Ajax request is finished, so we can enable
                //the button again.
                $('#btnSubmitProveedor').attr("disabled", false);
            }
        });
    }
    /*TERMINA PROVEEDOR*/

    /*INICIA FLETE*/
    var RunEventsFlete = function () {
        $("#IDEmpresa").on("change", function () {
            var IDEmpresa = $(this).val();
            var IDSucursal = $("#IDSucursal").val();

            GetChoferesXIDEmpresa(IDEmpresa, IDSucursal);
            GetVehiculosXIDEmpresa(IDEmpresa, IDSucursal);
            GetLugaresXIDEmpresa(IDEmpresa);

            //GetJaulasXIDEmpresa(IDEmpresa);
            //GetRemolquesXIDEmpresa(IDEmpresa);
            
        });
        $("#TipoFlete").on("change", function () {
            var opcion = $(this).val();
            GetEmpresaXTipoFlete(opcion);
            opcionServer = opcion;
            ToggleDivTipoFlete(opcion);

            if (navigator.onLine) {
                console.log("online");
                InitMap();
            }
            else {
                console.log("offline");
            }

            $("#IDChofer option").remove();
            $("#IDChofer").append('<option value="">-- Seleccione --</option>');
            $('#IDChofer.select').selectpicker('refresh');

            $('#IDVehiculo').empty();
            $("#IDVehiculo").append('<option value="">-- Seleccione --</option>');
            $('#IDVehiculo.select').selectpicker('refresh');

            $("#Trayecto_id_lugarOrigen option").remove();
            $("#Trayecto_id_lugarOrigen").append('<option value="" data-latitud="" data-longitud="">-- Seleccione --</option>');

        });

        //$("#DocumentosPorCobrarDetallePagos_Id_formaPago").on("change", function () {
        //    var opcion = $(this).find(":selected").data("bancarizado");
        //    Bancarizado.val(opcion);
        //    ToggleDivBancarizado(opcion);
        //});

        //$("#DocumentosPorCobrarDetallePagos_Id_cuentaBancariaBeneficiante").on("change", function () {
        //    var opcion = $('#DocumentosPorCobrarDetallePagos_Id_cuentaBancariaBeneficiante').find(":selected");

        //    $("#DocumentosPorCobrarDetallePagos_NombreBancoBeneficiante").val(opcion[0].dataset.banco);
        //    $("#DocumentosPorCobrarDetallePagos_NumCuentaBeneficiante").val(opcion[0].dataset.numcuenta);
        //    $("#DocumentosPorCobrarDetallePagos_NumClabeBeneficiante").val(opcion[0].dataset.clabe);
        //    $("#DocumentosPorCobrarDetallePagos_NumTarjetaBeneficiante").val(opcion[0].dataset.numtarjeta);
        //});

        //$("#DocumentosPorCobrarDetallePagos_Id_cuentaBancariaOrdenante").on("change", function () {
        //    var opcion = $('#DocumentosPorCobrarDetallePagos_Id_cuentaBancariaOrdenante').find(":selected");

        //    $("#DocumentosPorCobrarDetallePagos_NombreBancoOrdenante").val(opcion[0].dataset.banco);
        //    $("#DocumentosPorCobrarDetallePagos_NumCuentaOrdenante").val(opcion[0].dataset.numcuenta);
        //    $("#DocumentosPorCobrarDetallePagos_NumClabeOrdenante").val(opcion[0].dataset.clabe);
        //    $("#DocumentosPorCobrarDetallePagos_NumTarjetaOrdenante").val(opcion[0].dataset.numtarjeta);
        //});

        //var Imagen = document.getElementById("DocumentosPorCobrarDetallePagos_ImagenMostrar").value;
        //var ExtensionImagen = document.getElementById("DocumentosPorCobrarDetallePagos_ExtensionImagenBase64").value;
        //var ImagenServidor = document.getElementById("DocumentosPorCobrarDetallePagos_ImagenBase64").value;
        //if (ImagenServidor === null || ImagenServidor.length == 0 || ImagenServidor == '') {
        //    document.getElementById("DocumentosPorCobrarDetallePagos_HttpImagen").dataset.imgBD = "0";
        //}
        //else {
        //    document.getElementById("DocumentosPorCobrarDetallePagos_HttpImagen").dataset.imgbd = "1";
        //}


        //$('#DocumentosPorCobrarDetallePagos_HttpImagen').fileinput({
        //    theme: 'fa',
        //    language: 'es',
        //    showUpload: false,
        //    uploadUrl: "#",
        //    autoReplace: true,
        //    overwriteInitial: true,
        //    showUploadedThumbs: false,
        //    maxFileCount: 1,
        //    initialPreview: [
        //        '<img class="file-preview-image" style="width: auto; height: auto; max-width: 100%; max-height: 100%;" src="data:' + ExtensionImagen + ';base64,' + Imagen + '" />'
        //    ],
        //    initialPreviewConfig: [
        //        { caption: 'Imagen del recibo' }
        //    ],
        //    initialPreviewShowDelete: false,
        //    showRemove: false,
        //    showClose: false,
        //    layoutTemplates: { actionDelete: '' },
        //    allowedFileExtensions: ["png", "jpg", "jpeg",]
        //})
        //$('#DocumentosPorCobrarDetallePagos_HttpImagen').on('fileclear', function (event) {
        //    document.getElementById("DocumentosPorCobrarDetallePagos_ImagenMostrar").value = "";
        //});
    }
    var LoadValidationFlete = function () {
        var form1 = $('#frmFlete');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({ // initialize the plugin
            //debug: true,
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
                //"Flete.kmInicialVehiculo": { numeroConComas: true },
                "Flete.precioFlete": { numeroConComas:true },
                TipoFlete: { required: true },
                //"Flete.Id_metodoPago": { required:true },
                //"Flete.Id_formaPago": { min: 1 },
                "Trayecto.id_lugarOrigen": { required: true },
                "Trayecto.id_lugarDestino": { required: true },
               // "DocumentosPorCobrarDetallePagos.Monto": { min: 1, required: true} 
                //bancarizado
                //"DocumentosPorCobrarDetallePagos.FolioIFE": { required: true },
                //"DocumentosPorCobrarDetallePagos.NumeroAutorizacion": { required: true },
                //"DocumentosPorCobrarDetallePagos.HttpImagen": { ImagenRequerida: true },
                //"DocumentosPorCobrarDetallePagos.Id_cuentaBancariaOrdenante": { required: true },
                //"DocumentosPorCobrarDetallePagos.Id_cuentaBancariaBeneficiante": { required: true }
            },
            messages: {
                IDEmpresa: {  required: "Por favor, seleccione una línea fletera." },
                IDSucursal: { required: "Por favor, seleccione una sucursal." },
                IDChofer: { required: "Por favor, seleccione un chofer." },
                IDVehiculo: { required: "Por favor, seleccione un vehículo." },
                TipoFlete: { required: "Por favor, seleccione un tipo de flete." },
                //"Flete.Id_metodoPago": { required: "Por favor, seleccione un metodo de pago." },
                //"Flete.Id_formaPago": { min: "Por favor, seleccione una forma de pago." },
                "Trayecto.id_lugarOrigen": { required: "Por favor, seleccione un lugar de origen." },
                "Trayecto.id_lugarDestino": { required: "Por favor, seleccione un lugar destino." },
                //"DocumentosPorCobrarDetallePagos.Monto": { min: "Por favor, escriba una cantidad para el cobro del flete el cual debe ser mayor o igual a 1.", required: "Por favor, ingrese una cantidad para el cobro del flete el cual debe ser mayor o igual a 1." }
                //bancarizado
                //"DocumentosPorCobrarDetallePagos.FolioIFE": { required: "Por favor, escriba el número de folio del INE." },
                //"DocumentosPorCobrarDetallePagos.NumeroAutorizacion": { required: "Por favor, escriba el número de autorización." },
                //"DocumentosPorCobrarDetallePagos.HttpImagen": { ImagenRequerida: "Por favor, seleccione una imagen del comprobante del cobro." },
                //"DocumentosPorCobrarDetallePagos.Id_cuentaBancariaOrdenante": { required: "Por favor, seleccione una cuenta bancaria de la empresa." },
                //"DocumentosPorCobrarDetallePagos.Id_cuentaBancariaBeneficiante": { required: "Por favor, seleccione una cuenta bancaria del cliente." }
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
        $('#btnSubmitFlete').attr("disabled", true);
        var form = $("#frmFlete")[0];
        var formData = new FormData(form);
        var IDSucursal = $("#IDSucursal").val();
        formData.append("IDSucursal", IDSucursal);

        var monto = Monto.val();
        monto = Number.parseFloat(monto.replace(/,/g, ''));
        formData.set("DocumentosPorCobrarDetallePagos.Monto", monto);

        //var km = Flete_kmInicial.val();
        //km = Number.parseFloat(km.replace(/,/g, ''));
        //formData.set("Flete.kmInicialVehiculo", km);

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
                if (response.Success) {
                    var json = JSON.parse(response.Mensaje);
                    Mensaje(json.Mensaje, "1");
                    $('input[name=IDFlete]').val(json.IDFlete);
                }
                else {
                    window.location.href = '/Admin/Compra/Index/';
                    Mensaje(response.Mensaje, "2");
                }
            },
            complete: function () {
                //Ajax request is finished, so we can enable
                //the button again.
                $('#btnSubmitFlete').attr("disabled", false);
            }
        });
    }

    function GetProveedoresXIDSucursal(IDSucursal) {
        $.ajax({
            url: '/Admin/Compra/GetProveedoresXIDSucursal/',
            type: "POST",
            dataType: 'json',
            data: { IDSucursal: IDSucursal },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {

                $("#IDProveedor option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#IDProveedor").append('<option value="' + result[i].IDProveedor + '">' + result[i].NombreRazonSocial + '</option>');
                }
                $('#IDProveedor.select').selectpicker('refresh');
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
    function GetListadoCuentasBancariasProveedorXIDProveedor(IDProveedor) {
        $.ajax({
            url: '/Admin/Compra/GetListadoCuentasBancariasProveedorXIDProveedor/',
            type: "POST",
            dataType: 'json',
            data: { IDProveedor: IDProveedor },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {

                $("#DocumentosPorCobrarDetallePagos_Id_cuentaBancariaOrdenante option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#DocumentosPorCobrarDetallePagos_Id_cuentaBancariaOrdenante").append('<option value="' + result[i].IDDatosBancarios + '" data-titular="' + result[i].Titular + '" data-numcuenta="' + result[i].NumCuenta + '" data-numclabe="' + result[i].Clabe + '" data-numtarjeta="' + result[i].NumTarjeta + '" data-banco="' + result[i].Banco.Descripcion + '" data-idbanco="' + result[i].Banco.IDBanco + '">' + result[i].Titular + '</option>');
                }
                $('#DocumentosPorCobrarDetallePagos_Id_cuentaBancariaOrdenante.select').selectpicker('refresh');

                $("#DocumentosPorCobrarDetallePagos_NombreBancoOrdenante").val('');
                $("#DocumentosPorCobrarDetallePagos_NumCuentaOrdenante").val('');
                $("#DocumentosPorCobrarDetallePagos_NumClabeOrdenante").val('');
                $("#DocumentosPorCobrarDetallePagos_NumTarjetaOrdenante").val('');

            }
        });
    }
    function GetVehiculosXIDEmpresa(IDEmpresa, IDSucursal) {
        $.ajax({
            url: '/Admin/Compra/GetVehiculosXIDEmpresa/',
            type: "POST",
            dataType: 'json',
            data: { IDEmpresa: IDEmpresa, IDSucursal: IDSucursal },
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

    function GetChoferesXIDEmpresa(IDEmpresa, IDSucursal) {
        $.ajax({
            url: '/Admin/Compra/GetChoferesXIDEmpresa/',
            type: "POST",
            dataType: 'json',
            data: { IDEmpresa: IDEmpresa, IDSucursal: IDSucursal },
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
    function GetEmpresaXTipoFlete(TipoFlete) {

        $.ajax({
            url: '/Admin/Compra/GetEmpresaXTipoFlete/',
            type: "POST",
            dataType: 'json',
            data: { TipoFlete: TipoFlete },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "2");
            },
            success: function (result) {
                $("#IDEmpresa option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#IDEmpresa").append('<option value="' + result[i].IDEmpresa + '">' + result[i].RazonFiscal + '</option>');
                }
                $('#IDEmpresa.select').selectpicker('refresh');
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
            $("#DocumentosPorCobrarDetallePagos_fecha").datepicker("update");
            AgregarValidaciones();
            $('#divNoAplicaFlete').show(1000);
        }
        else if (opcion == 2  ||  opcion === '') {
            QuitarValidaciones();
            $('#divNoAplicaFlete').hide(1000);
        }
        else if (opcion == 3 || opcion === '') {
            $("#DocumentosPorCobrarDetallePagos_fecha").datepicker("update");
            AgregarValidaciones();
            $('#divNoAplicaFlete').show(1000);
        }
        
    }
    function ToggleDivBancarizado(opcion) {
        opcion = String(opcion);

        if (opcion.localeCompare("True") == 0 || opcion.localeCompare("1") == 0) {
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
        //DocumentosPorCobrarDetallePagos_Monto.rules("add", { required: true, min: 1 });
        //Flete_Id_metodoPago.rules("add", { required: true });
        //DocumentosPorCobrarDetallePagos_Id_formaPago.rules("add", { required: true, min: 0 });
        Trayecto_id_lugarOrigen.rules("add", { required: true });
        Trayecto_id_lugarDestino.rules("add", { required: true });
        //Flete_kmInicial.rules("add", { numeroConComas: true });
        Monto.rules("add", { numeroConComas: true });
    }
    function QuitarValidaciones() {
        IDEmpresa.rules("remove", "required");
        IDSucursal.rules("remove", "required");
        IDChofer.rules("remove", "required");
        IDVehiculo.rules("remove", "required");
        //DocumentosPorCobrarDetallePagos_Monto.rules("remove", "required min");
        //Flete_Id_metodoPago.rules("remove", "required");
        //DocumentosPorCobrarDetallePagos_Id_formaPago.rules("remove", "min");
        Trayecto_id_lugarOrigen.rules("remove", "required");
        Trayecto_id_lugarDestino.rules("remove", "required");
        //Flete_kmInicial.rules("remove", "numeroConComas");
        Monto.rules("remove", "numeroConComas");

        IDEmpresa.closest(".controlError").removeClass("has-success has-error");
        IDSucursal.closest(".controlError").removeClass("has-success has-error");
        IDChofer.closest(".controlError").removeClass("has-success has-error");
        IDVehiculo.closest(".controlError").removeClass("has-success has-error");
        DocumentosPorCobrarDetallePagos_Monto.closest(".controlError").removeClass("has-success has-error");
        //Flete_Id_metodoPago.closest(".controlError").removeClass("has-success has-error");
        //DocumentosPorCobrarDetallePagos_Id_formaPago.closest(".controlError").removeClass("has-success has-error");
        Trayecto_id_lugarOrigen.closest(".controlError").removeClass("has-success has-error");
        Trayecto_id_lugarDestino.closest(".controlError").removeClass("has-success has-error");
        //Flete_kmInicial.closest(".controlError").removeClass("has-success has-error");
        Monto.closest(".controlError").removeClass("has-success has-error");

        Validation_summary_flete.find("dd[for='IDEmpresa']").addClass('help-block valid').text('');
        Validation_summary_flete.find("dd[for='IDSucursal']").addClass('help-block valid').text('');
        Validation_summary_flete.find("dd[for='IDChofer']").addClass('help-block valid').text('');
        Validation_summary_flete.find("dd[for='IDVehiculo']").addClass('help-block valid').text('');
        Validation_summary_flete.find("dd[for='DocumentosPorCobrarDetallePagos_Monto']").addClass('help-block valid').text('');
        //Validation_summary_flete.find("dd[for='Flete_Id_metodoPago']").addClass('help-block valid').text('');
        //Validation_summary_flete.find("dd[for='DocumentosPorCobrarDetallePagos_Id_formaPago']").addClass('help-block valid').text('');
        Validation_summary_flete.find("dd[for='Trayecto_id_lugarOrigen']").addClass('help-block valid').text('');
        Validation_summary_flete.find("dd[for='Trayecto_id_lugarDestino']").addClass('help-block valid').text('');
        //Validation_summary_flete.find("dd[for='Flete_kmInicialVehiculo']").addClass('help-block valid').text('');
        Validation_summary_flete.find("dd[for='DocumentosPorCobrarDetallePagos_Monto']").addClass('help-block valid').text('');
    } 

    /*TERMINA FLETE*/

    /*INICIA DOCUMENTOS*/
    var LoadTableDocumentos = function () {
        var IDCompra = $("#IDCompra").val();

        if (!$.fn.DataTable.isDataTable('#tblDocumentos')) {
            tableDocumentos = $('#tblDocumentos').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
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

    
    /*INICIA FIERROS*/
    function RunEventsFierro() {
        $("#VistaPreviaFierro").on("click", function () {
            var id_fierro = $("#Fierro_IDFierro").val();
            
            document.getElementById('base64image').src = 'data:image/gif;base64,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q6z3q633rK33rK34rK34ra74rq/4sLH4srP4tLX4t7j4uLr4urv4u7z5u7z5vL75vr/5v8D5wML5w8T6xcb6x8j6yMn6ycr6y8z6y8z6zM36zM36zc77z9D70dL71NX71db719j72Nn72tv729z729z73Nz73d383d783t/83+D84OH84uP85eX85uf85+j96On96ur96+v96+v96+v96+v97Oz97Oz97Oz97Oz97e397e397e397u797+/98PD98PD98/P++Pj++vr+/Pz+/v7+/f3+/f3+/v7+/v7+/v7+/f3+/f3+/f3+/Pz+/Pz+/Pz+/f3+/f3+/v7+/v7+/v7+/v7+/v7+/v7//v7//v7//v7//v7///////////////////////8I/gD5CRxIsKDBgwgTKlzIsKHDhxAjSpxIsaLFixgzatzIsaPHjyBDihxJsqTJkyhTqlzJsqXLlzBjypxJs6bNmzhz6tzJs6fPn0CDCh1KtKjRo0iTKl3KtClMe1DtOc1pb5zVq1alTp1ZFatXrVtheh07LqxYsmPNfgQHjh1DtGkXFtu1Sy1Ddt6q6a3WzW1CuF4VFrtzpfCVRMXsHsy7dy+2b38BX024yrDlK48UD7TXrbFnbH4NSp58sJiTy5bveNPM2HPjbQhHW0UoB/XlRIrBud4N7mBXwGAJ7rKNuq7ab7tdrz44GmEi4pcz2UWe3DPC32SDE4QefXp165Hj/iLkblm6Wt3f9zaE2pC8YVmKt6Xny5Ew+TOaqX8PnbGYe/PxfQdZR6RwJ4dmArGDzW7Y9PbRLl7Y5kViCPLDTmeOecOfR80kEqFhd1BYYYLffAOOdiF5I0smmYi4FHLKVKMhUd68cskfl/TCEzvKDOOjj8ZgI1QoQChhpJF/LHcTO8n86OQwyWyYkzd4HGmlEngoWVM0Tz5pTDg8OVPklVZechM4XXbJDE9+kEkmMjY5k2aXA+LUi5tkOlITO8bM+aSQOYWC55VAaAnTN37+qZOgg1qp40yIJvqjoTW90qijNKEp6TDG8BTEpUbCSVOPkkbD0yWgImJTOJJ+Gean/o06c9M3fc5JKU6nDBrEo0s22SVsP+VKZpY8fcPMj8rU+VMvbRqJCK8+SSnUrSNWa+212Gar7bbcCoVoL7iE+wsyDipVDSaKxKEuII4k81Iy4cYbry7NKNWKHerm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8m1w8mxv8mtu8mpt8mdq8mRn8mJl8mBk8l9i8V9i8V9i8V5h8V5h8V1g8Vpe8Vhb8FVZ8FNW8FJV8FBU8E9T8E5S8E5S8E5S705S705S705S705S709S709T7lBU7VJV6lRX5lda3VxfzmNmvmttqnJ0jnx9goKCg4ODhISEhYWFhoaGh4eHiIiIkIqKpIyNuI6PzpCR3ZGS55GT7JGT8JCS85CS9JCS9ZCS9Y+R9Y+R9Y+R9Y+R9pCS9pGT9pGT9pOV9pSW9paY9pia9pqc9pud9p2f95+h95+h96Ci96Ci96Kk96Sl96ao96mq96ut962u+K6v+K6v+K+w+K+w+LCx+LGz+LO1+LW2+ba4+bi5+bq7+bu9+b2/+b7A+b7A+b/A+sHD+sTG+sXH+sfI+snK+srL+szN+s3O+s7P+87P+8/Q+9DR+9HS+9TV+9na+9zc/N7f/N/f/ODh/OLi/OPk/OTl/OXm/Obn/ejp/err/ezt/e3u/e7v/e/v/e/w/fDw/fHx/vLy/vLz/vPz/vT0/vX1/vb2/vr6/vv7/v39/v7+/v7+/v7+/v7+/v7+//////7+//7+//7+////////////////////////////////////////////////////////////////////CP4A7QkcSLCgwYMIEypcyLChw4cQI0qcSLGixYsYM2rcyLGjx48gQ4ocSbKkyZMoU6pcybKly5cwY8qcSbOmzZs4c+rcybOnz59AgwodSrSo0aNIkypdyrSpy3LhslWrli1cOac3uT1rxrXrM25YaV7rSrbrtbAxx5Vd22wc2pfU2Jal9vbjt2/hGEqTS1Yaw3LkyNVlGO6ZscPGluVNyLeswnLftkneJu7qYIPREGtGdhZhY7IJx00eTfnyQHLLNKtG5vbg564Iy3EjPfobO9OGVatWhjDuZ7oHI9MeLe7yN93Ivx2s9rraQXLDaQuuSw257mfPt/J9Nr2guOiki/5Tt64bIbfGYA+CDz+4OnnNCcXtLStNvPr1k+2j3fYevkJy4mTDjTjdIYTfZK3VlUx/iXEk3HrdmObeewlmVM6B+tWlzHvOdSQaeMqZZk84yCCHTIgekTMbbdxYJmI4qSGGzDMVelSOOCtK9o2LIgoUDjXUbFOgSOyMI05lTmWT2jLUDNljSuQYs8uUU/bSzJMrkSMM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+/78/P79/f7+/v7+/v7+/v///////////////////////////////////////////////////////////////////wj+AO8JHEiwoMGDCBMqXMiwocOHECNKnEixosWLGDNq3Mixo8ePIEOKHEmypMmTKFOqXMmypcuXMGPKnEmzps2bOHPq3Mmzp8+fQIMKHUq0qNGjSJMqXcq0qct02qQpUyZNWzqnN6cB+8W1KzBqWGk660q2q7OwMb+VXfvrG9qXydiWTfb2ozZt3hgOk0t2GMNw39zWXejNmK3DtoLlTci3rMJwzIxJNiYt3OCDyRBr1jWNceOuCbdNHm0M7GWB6IJpXq0LHMLPoA+GQ0Z6NDN1pw2vXv0L4THYxxAqq01a2mVtu5NrO6gMtrKD34jXFvx2WfLdxg6K28oXmLiD0qT+k7Y22Pr11QinNe58UPz48ufRI+y2t+ywbgndjyZft1p8zQuJ040003TznUL6TbZYXbz8lxhHkelH12XmxefaRuEkyN9lv8T3jEfYuPfcafd4o0tyuiz30Te01YaMZSSWqBpiuhhzIUjlSNOiZMzAGKNA3iyzTDXomKSON9ZY4+NS1gyzyzDOlEOUOt9oU402B+pUzi6qdNmlK8gIxY00z5RZZjW44VQOLl62qQouUvakDjVm1vkMNWnaJIybbrpCXU7pkGlnnSrWtA2ffPa2UzWDDhonTcEgyueGN4nT6KDY1FSOK5K6GWZO3FxqpzR5xmRNp57qFKqodWZpKqr+bZ6V0zestkrTobCq4gpP0dRa5qMycQmrMDxp4yulMn0Dq588pdMrq1fZZA2nksrKUzeiRuOqprcgOqJP2A6KJ0/W+OLlLsj2JA6jZVqzLU/ATvnjvPTWa++9+Oarb1LY6OJKKADP0suCSVHpzV3adPNNtC0BA/DDD5cSXFLibIPwxdr8mdIxEHcMsGlGoYPxyO+WNE4pHne861HdjIzxNu2oBE3KHhMsFFQuYxxvScLQ3DHIQ4mc88U3ntSLzxBPTBQ4Q1+scUlHIw2w0kMx3fRdT5PUs9ShAC2U0FcXbdLMXNscFM5X70zSyVKvbFTLTcO8EsdIex301SWb5HD+yhIrVXHOWaOETS7/AizLwEwZjHA33jC87+OQRy755JRXjm/elmMkDSyZdM6KcUGtkw46pKezDk/imNL56p379ZM6pMdeuk7SdML67Z/yBLvsvJ9uUze3B98Jfju1w/vx6MRc0yrBB8+L7sjz7ripzQvPU/THKy+TNNUHn/tNu2Mfu+/bd387MDqFLz465Mckju3md45+TuqLbxMu8XcOOk7rrD97TdbI3yua5T/t0YQX5hseT4wnvlLRRBylqF4pMGeT0SFvejURB/NY14n5AaV+6MDgTaTBi1XwAhkU3Ik61JEOdbRPKdTgBS+SsSSRfGMYsYjF/n5EDVI84of+P3wFN0SCjVHg4YhHvMQOL0MNSgDxiZT40EeGEQgkWjEQS3xLODzxxC4+woMb0YUVx4gHSZCIF170ot0sIg0ykjEWp9lEGrtICo5Ywo1jTMRlkjFHLy5kGsvABS6WwZ6E4JGMrnsLGvv4xIQsAxOEiKQkMbEMQx7SinBkECOBSAmE9EKSoJRkLxBySUwOZpGbHKBBtKGIUIZSEYUiiBFLiYdMvuUZm/zhGlHhSleiAjO0xMOE6vKKTdbxIJ3oZSg7gRBJlHIUpwmHD+dIiSEeRJmuRIg0AHFISwQuLLZIIyUSaRBshpIx3HwjvahRTCCSYo0DMScoFSKNOyJxFFlh/FGZ4EkQSMqTmQuRRjKGmbmBjEKehIBmQScyDYQWcqEReYU5VQnRifQiEa5MxCgrWhFwIAMXqEAFLpAhNo6a9KQoTalKV8rSlrr0pTCNqUxnStOa2vSmOM2pTnfK07AEBAAh+QQJBAD4ACwAAAAAjgCOAIcAAAABAQECAgIDAwMEBAQFBQUGBgYHBwcICAgJCQkKCgoLCwsMDAwNDQ0ODg4PDw8QEBARERESEhITExMUFBQVFRUWFhYXFxcYGBgZGRkaGhobGxscHBwdHR0eHh4fHx8gICAhISEiIiIjIyMkJCQlJSUmJiYnJycoKCgpKSkqKiorKyssLCwtLS0uLi4vLy8wMDAxMTEyMjIzMzM0NDQ1NTU2NjY3Nzc4ODg5OTk6Ojo7Ozs8PDw9PT0+Pj4/Pz9AQEBBQUFCQkJDQ0NERERFRUVGRkZHR0dISEhJSUlKSkpLS0tMTExNTU1OTk5PT09QUFBRUVFSUlJTU1NUVFRVVVVWVlZXV1dYWFhZWVlaWlpbW1tcXFxdXV1eXl5fX19gYGBhYWFiYmJjY2NkZGRlZWVmZmZnZ2doaGhpaWlqampra2tsbGxtbW1ubm5vb29wcHBxcXFycnJzc3N0dHR1dXV2dnZ3d3d4eHh5eXl6enqCd3iSc3Sfb3CuaWvCYWTSW17cV1rjU1foUlXrUFTtT1PuTlLvTlLvTlLvTlLvTlLwTlLwT1PwUFTwUVXwU1fwVFjxV1rxWV3xW1/xXWDxXmHxX2LxX2LyX2LyX2LyYGPyYWTyZGfyaGvya27ybG/ybXDyb3LzcXTzc3bzdXjzeHrzen3zfH/0foH0gIL0goT0hYf1h4r1i431jY/1jpD1jpD1jpD2j5H2kZP2lJb2lpj2mpz2m532nZ/2nqD2nqD3n6H3oaP3oqT3pKX3pqj3qar3q6z3ra74r7D4sbL4tLX4t7j5urv5vL35vb/5vsD5v8H6wcP6xMb6x8j6y8z6zc77zs/7z9D70NH70tL709T71tf72dr829v83d783t/83t/83t/84eL84+T85eb85+j96en97Oz97u798PD98PD+8fH+8/P+9PX+9/f++vr+/f3+/v7+/v7+/v7+/v7//////////v7///////////////////////////////////////////////////8I/gDxCRxIsKDBgwgTKlzIsKHDhxAjSpxIsaLFixgzatzIsaPHjyBDihxJsqTJkyhTqlzJsqXLlzBjypxJs6bNmzhz6tzJs6fPn0CDCh1KtKjRo0iTKl3KtKlLdd+sQYNm7Zs6pzexFRvGtWsxbFhpUutKtiu1sDHJlV07jBzal87YlnX29iM4cOIYKpNLVhnDc+Tc1l0ortmuw7uM5U3It6zCc9KWSV5m7dzgg88Qa/YFFmFjsgnBTR69rPNlfOqMaV7ty/LBz10RnmtGerS0q5cNr15NDGEz2M0QPqtN2tplcLuTgzsIDTa0g+SI1xb8Vlry3cENotvKtxi6g9ak/pPmNtj69dUIsTU2XVD8+PLn0SMUt7esssUH3Y8mX7dbfM0LoSOONdiI851C+k2G31vB/JcYR5HpR9dl5sXnmkbnJMjfZcTEV41H3rj3zGkCifNLcr8s9xE5zBDHzIWniaMaYr80A6NH6VjT4mTS3EiiONJI0w1uJKkjDjfc+IhUiMFQlg5R64zzDTbfKFlTOsHIoqWWtUwIFDjUBCkmNuvklI4vW6Ypiy9P9rTONWLGKc01Zd60jJpq1lIOT+qEKWec39wUDp54FsMTNn/+eSBNxhCKpzc6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2tv729z73Nz83N383t783+D84eL85OX86Oj96+v97Oz97Oz97e397e397u797+/98PD98fH98vL+8/P+8/T+9PT+9fX+9vb++fn++/v++/v+/Pz+/Pz+/Pz+/f3//f3//f3//f3//f3//v7//v7//v7//v7///////////////////////////////////////////////////////////////////////8I/gDtCRxIsKDBgwgTKlzIsKHDhxAjSpxIsaLFixgzatzIsaPHjyBDihxJsqTJkyhTqlzJsqXLlzBjypxJs6bNmzhz6tzJs6fPn0CDCh1KtKjRo0iTKl3KtKnLb82G7do1rNk3pzeLrUrFteuqYlhp9upKtmuvsDGllV2bShral7fYlr319mOzZs8YypJLVhZDa9Cg1WX4rFaow6Fa5U3It6xCa7tgSYYFzNrgg7kQay4FFmFjsgmZTR4Ni9jlgd5aaV5d6prnz1wRWptFevSuq5cNr16tCiEt2LQQ4qpN2tflZruTNzu4C/aug9CI1xZcl1fy3bUOatvKd5W2g76k/pPu/Nb69dUIizUmX1D8+MHmzyNO6GxvWVnOErofzT7sMfmaLaSNM8IU48x3Cu03WX6DoQJgYhxFtl9wl8V3nmsbWaNgf2+pIl8wHh3jHi6nCfQMKcmRstxH0NBW2yyWlWjPM6ohRkotGILEjS8uSrZLjDKayAsvx3hj0jfOFFMMkEst8xstwhg5VDjOHCPMMdXw5E0qmXTZJSe5CLXMLriUWaYw4eTkTSletplJKVLyFE4vZtaJSy9p3kSLm25ykuVO3pBpZ53H3OQMn3yuwlMwgw6ajU2wIMrnMjpV0+igHL7kDSeSuhlmTstcaucuecq0TKee6hSqqHX+aSqq/m0Ko5MzrLZK06GwZsIJT4LW+ihNqORKoU7H1IqLaTVVA6ufWvZ6aZw0LcOppLL21Iyou7hq05qI8gLUtYPiydMyq3iJCqVBVcNomcRo2xO0RJUa5Lz01mvvvfjmq69RzZziCSQAk7KKW0t9w8wxwSRsjDLwqvQKwBBDTMmnSD0zTMIYY8zgSrlE7DHAhR6VTcYkJ7xYSt5Q8rHHniBlTMkkDzOOSsOs/DHBRHkDc8lMmkSLzR6HTNTIO2e88UmrAB0xxUM5U3TGzKiUtNIAMy2U008nHHVKP1MN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ZmnyaWzyam3ybG/ybXDybXDybXDybXDzbnHzcHPzcnXzdXfzdnnzeHrzeXzze370fYD0f4H0gYT0hYj1iIr1i431jZD1j5H1kJL1kZP2k5X2lpj2mZv2m532nJ72nqD2n6H3oKL3o6X3p6j3qqv3rK34rq/4rq/4r7D4sLH4srP4tbb5uLr5u735vb/5vsD5v8H6wML6w8X6xsj6ycr6zM36zs/7z9D70NH70dL70tP71NX71tf72dr729z83d783t/83t/83+D84OH84eL84eL84uP84+T85eX85ub85uf85+f96Oj96er96+v97Oz97e397u797+/97+/+8PD+8PD+8vL+9PT+9fX+9vb++Pj++fn++vr+/f3+/f3+/f3+/f3+/v7+/v7//v7//v7//v7//v7//v7///////////////////////////////////////////////////////////////////////////////////8I/gDrCRxIsKDBgwgTKlzIsKHDhxAjSpxIsaLFixgzatzIsaPHjyBDihxJsqTJkyhTqlzJsqXLlzBjypxJs6bNmzhz6tzJs6fPn0CDCh1KtKjRo0iTKl3KtKlLbr9kqVIl6xc3pzdtXaLEteslW1hpuupKtqursDGblV1LqRnal6bYljX19mOwYMoYepJL1hNDaMry1l2orNSiw4s6CUbIt6xCaKg0Sdb0Ctrgg6kQa350K2Fjsgl9TR6tidblgdw6aV79SBrjz1wRQvNEejSqbqcNr159CaEo2KIQlqpN+tXlYLuTBzuoCraqg8qI116M1lXy3aUOWrPU2JK1g6+k/pMGW9f69dUIbTUmb1D8+MHmzyNOSGxvWU/EErofzR4tL/maLWQNMbLYQsx3Cu03mTCXVQJgYhxFtl8op8V3nmsbQaNgf3VhIt8sHuXiXnan1aMMJMlBstxHytBWmyeWlWiiaohBUgqGIGHziouSoRKjjAIp44orvFxVUjfC2GLLj0v9UsolpdSiDVHeBHPLK7e4tZM2l/jhpZeErCKULqWMYqaZr3iTkzaTfOmmH5NM2ZM3rZxp5yitqHlTKW++SUg0PGVT5p12dmaTMX32yQlPrxBKKI4zgZJon7/o1IyjhNZSkzaETPqmmDnpgumdpegp0y+efqqTqKPaqeWp/qm6qWlOwbTqKk2IxuoHITyZYquZkMbUZawk6nTLr7HYFE2sfwbqa6vZ3PRLp5POypMvo5ryqk3aSJKocT9hS2iePP2yyZeXVBpUM42aGcu2PclplKlA1mvvvfjmq+++/BoljCWMyCGwJJs8w9Q2uNSyysKy3IJgS6AILLHEeLSiVDCuLKyxxr201MrEIAvc8VHRbGzywiumpA0eIYPMCFKynGyyK/SaZEvLIRtclDUyn8yMSqbgDPLIRJXc88bqoqSJ0BNbXNQvR2+Mi0pLMy2w00RBHfXCU6cUtNVyED2U0VsnfdLNYOtMFM9br/KzyndYvQjMW9O80sdMi130/tYpqxRxy3dgfRTGPfPy0r+LSByJJmonhbDCDNvycL+UV2755ZhnrvnmPn3D+UfW6DLL6L5M/rlFyYyu+ui4eH56Ramvvnotpr/uUDe1yK47brZDFIzuuvvSO0S5Ay97tMMz9IzxuteevEHFMC+7888TFL30o9fievUIWYP96GZzf1Av3yMvPkK4S9/3+Qd9g4vxrbPPUDHFq67L9vIr9M0zwegSDPVGAUc3uvGNc5gEHNh4xjO2cS9waAMbEIQgN8Qhkm4gQxcYxGAwGCgjcGQjgiDMBv44go1eZPCEveDgZc7xQBC6sGYZicYJZ6gLYJSoGy7MITbAwZFt0JCG/o17ywd1CEJ5ZaR/Pzxh+MLyDSLmcCEDtIY1BriQJNIQG4PBoRNBmJBuLKMYYAzjMnh3ECvOMIhY0eIWsWG+gmQjjHAMYxsJYsYTotEpatyikQryDWTEMY7IGKFALlhHXdyxKU1c4w4PEo0//hFQBtFGIXVhRLRwY42VHMgzHBnHQwKjjsg4DQudmA0KHoSTf0TINkyYxGDwsESXzGE2YDgQVMYxIdNg5RntBY5YQlAbr0SILeGokG0gEYPIUOG9vvGNYCbki8M8JEG2oY1Mcm6T0cyfQboxzGKQUZsDmYYtpwHOg2TDj4CcYzkHIo5tWCMa0bDGNky5znra8574G8ynPvfJz376858ADahAB0rQghr0oAhNKEsCAgAh+QQJBADyACwAAAAAjgCOAIcAAAABAQECAgIDAwMEBAQFBQUGBgYHBwcICAgJCQkKCgoLCwsMDAwNDQ0ODg4PDw8QEBARERESEhITExMUFBQVFRUWFhYXFxcYGBgZGRkaGhobGxscHBwdHR0eHh4fHx8gICAhISEiIiIjIyMkJCQlJSUmJiYnJycoKCgpKSkqKiorKyssLCwtLS0uLi4vLy8wMDAxMTEyMjIzMzM0NDQ1NTU2NjY3Nzc4ODg5OTk6Ojo7Ozs8PDw9PT0+Pj4/Pz9AQEBBQUFCQkJDQ0NERERFRUVGRkZHR0dISEhJSUlKSkpLS0tMTExNTU1OTk5PT09QUFBRUVFSUlJTU1NUVFRVVVVWVlZXV1dYWFhiV1d1VlaMVFWlUVPCTlHUS0/fSk3lSEzqR0vtRkruRkrvRUnvRUnvRUnvRUnvRUnvRUnwRkrwSEvwSU3wTE/wTlLwUFPwUVXwU1bwVFfwVFfwVFfxVVjxVVjxVlnxWFvxW17xXWDxX2LxYWTxY2bxZGfxZGfxZGfxZWjyZmnyaGvyam3ybXDzb3LzcXTzc3bzdXjzdnn0eXz0foH0gIP0g4b1hYj1hon1iYz1i471jpD1kJL1kpT2lJb2lZf2l5n2mZv2nZ/3oaP3pKb3paf3pqj3p6n3qav4q634r7D4s7T4tbf5trf5t7j5urz5vb/5wMH5wcP6xMb6xsf6x8n6yMr6ysz6zM77z9H70tP709X71db71tf819j82dr829z83N383d783t/84OH84eL84uP84+T85eb85eb85eb85uf85uf95+f95+j96er97O398PD98vL98/P99PT99fX99vb+9vb+9/f+9/f++Pj++Pj++fn++fn++fn++vr++vr++vr++/v+/f3+/f3+/v7+/v7+/v7+/v7+/v7+/v7+/v7+/v7+/v7///////////////////////////////////////////////////////////////////////////////////////////////////////8I/gDlCRxIsKDBgwgTKlzIsKHDhxAjSpxIsaLFixgzatzIsaPHjyBDihxJsqTJkyhTqlzJsqXLlzBjypxJs6bNmzhz6tzJs6fPn0CDCh1KtKjRo0iTKl3KtKnLZ7NOgQJ1atYzpzdVMUrEtSujVVhpiupKtquosDGDlV2bKBjal5vYlt309uOtW70YTpJLlhLDY7x41WXYa1Ogw4Ek5U3It6zCY5weSX406tjgg6AQazYEFmFjsgljTR796NTlgc0kaV5tCJnnz1wRHptEejSnaKc1rd7tCCEm2JgQaqpNetTlW7uT38IMG9RB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+v77+/78/P79/f79/f79/f79/f79/f7+/v/+/v/+/v/+/v/+/v/+/v/+/v/9/f/9/f/9/f/9/f/9/f/9/f/9/f/9/f/9/f/9/f/+/v/+/v/+/v/+/v/+/v/+/v/+/v/+/v///////////////////////////////////////////////wj+APMJHEiwoMGDCBMqXMiwocOHECNKnEixosWLGDNq3Mixo8ePIEOKHEmypMmTKFOqXMmypcuXMGPKnEmzps2bOHPq3Mmzp8+fQIMKHUq0qNGjSJMqXcq0qctkrz5hwvTpVTKnN0kF6sO1ayBSWGlu6kq266awMXeVXdtnF9qXktiWlfT2I6xYuhgmkktWEcNeuXLVZahL0pzDcxLlTci3rMJelAhJJsSp1+CDmBBrzlOKceOuCVlNHk3o0+WByBJpXp3nF8LPoA/2QkR6NKWrlw2vXj0I4SPYjxBGqk367GBYu5PDwgwb00FcxGvjGrwp+W66BoVt5RtI2MFN0Un+g61b3fpqhKQajzcYXjx18+cRztpbNtGshO1Hr0e7Cr7mhcLM8gkps3inUH6TLTfYH/7NgQhHkyDYyGnlwefaRr0guF9dgsAHikentBfJaQLpkkdyeSjoES601YaIZSTmowsim0lyIUjEbNKiZJPAGGOJm2yyCjImJQMLKaT4uNQrkwgyiSjHELXMkZmQ4tZOxwhSxpZbsuFcUKdA0siYY2ayTE7H+MHlmmX4EWVPy1xC5pyNXHLmTZKwySYbvvBkjJh0zrmhTLjoqeeDO2USaKA3zvSIoXq+otMuiwb6IU3HsAEpm1/idEqldEJyp0yvbMqpTp+COueVpJq6pij+OsGi6qo0FepqGWvwBOisjcakpavY6UTKrI10YpMvrq7R505/zmrMTa+ssSmsPa0CKiSs2pSmocb+ZG2gdvL0CiJcCiJpULsoOmYn2fb0plGj/ijvvPTWa++9+OZ7FDn89stOU8eY8gklBHciioEtsdPvwvz+mxQsmBAsscSpJMzwxUlBNvHGlKiY0sUgI9UJxxtjEs1KIGNslDAkc7wYSgqnvPBRGrcsMSsqxSxzw0axYvPEnaWk884OE+XzzwQHDfPO/dKMNME4q8Q0v0ex/PTLKE1NjshIm7zS0CEjVXPLHgstc9FHQdwyKjBdjHZSx5QycMEH62v33Xjnrff+3nzr+/ZQJwM1zTHCFG7MNEL9UkqQm6DS603sFFP45IXHyxMtjGcuSuA3TUMM5aA74xPmmWfuyeMxVQP66sRUwxMynpQuO5E1GbP66rTr5IrssrNNk+q3g04MT7HzXnoxNE0T/Oqi56SL8bKj3pLyy1OOG06wQF+69Cyx83n1hV9/U/baB+kJ5zIRDr4wiOf0S/lBqmIT8NW/m9Mp8CNvEzLVt/568cZzBU4iF7xi/A0n0RiF8UaBvpqww3aUI4b4fAILAAapFA3sHDKMgQxnHLAn0dCFK0rhCu4VJRrEIAYyXFcSavwiMPaLUTR4cYsa1vAX1BCJkUbBQx6yIob+g4lGLmxIxFxMcCOK66ESSwFEtFRjF0SM4i30x5FdKPGKo6jYaYghRSlm0CLHwCIWBXMZXXQxirzgyCrEeMVTXAYZZ5TiQpJxjF704hhHLAgbsWhCpXAxjkRMyDFisYpCGjIWTRTIHq9Ixrf8EZC3aGRBfmHIShpSeotUoiTD8khAoi4arrCkJV3xRVhkkoebxEoyIFnDL+pClKLE2kCIccpRDG8wv4BkGg8yC1ha8j4HSUUmy+ZEGp4xFzk8iC9FiZC47XEVyTwNMLqYi0TmY5mWTEgvSiHGVF4mGrm0IS++OBBsVlIhx1hjD2FhzdMkIxnkJAghzQlMdKawbwdOuYU5V3ELfFYkGfvMoz8dsgtstmugEflFKEfZR4QypBrF6IUudNGLYrDQoRjNqEY3ytGOevSjIA2pSEdK0pKa9KQoTalKV8rSlroUnwEBACH5BAkEAPAALAAAAACOAI4AhwAAAAEBAQICAgMDAwQEBAUFBQYGBgcHBwgICAkJCQoKCgsLCwwMDA0NDQ4ODg8PDxAQEBERERISEhMTExQUFBUVFRYWFhcXFxgYGBkZGRoaGhsbGxwcHB0dHR4eHh8fHyAgICEhISIiIiMjIyQkJCUlJSYmJicnJygoKCkpKSoqKisrKywsLC0tLS4uLi8vLzAwMDExMTIyMjMzMzQ0NDU1NTY2Njc3Nzg4ODk5OTo6Ojs7Ozw8PD09PT4+Pj8/P0BAQEFBQUJCQkNDQ0REREVFRUZGRlFGRlxHR4ZGR6VFR7tERstDRtZCRt5CReNBRehBRetBRexBRe1BRe5BRe5BRe5BRe5BRe5BRe5BRe5BRe5BRe9BRe9BRe9CRu9DR+9FSe9GSu9JTPBLT/BOUfBQU/BQU/BRVPBRVPBRVPBRVPBRVPBRVPFSVfFUV/FWWfFYW/FZXPFcX/FeYfFgY/FhZPJiZfJjZvJmafJoa/JrbvJtcPJvcvJwc/NxdPNzdvN0d/N3evR6ffR9gPR/gfSAg/SBhPSBhPWChfWFiPWJi/WMjvWPkfWRk/aSlPaTlfaVl/aXmfaZm/acnvefofegoveipPeipPekpfenqfirrfiusPixsviys/mztPmztfm1tvm3uPm6u/m9vvm+wPm/wfnBwvrCxPrCxPrDxfrFxvrHyfrJy/rMzfrNzvrP0PrQ0frR0vvR0vvS0/vT1PvV1vvW1/vY2fva2/zd3fze3/zg4Pzh4vzi4/3j5P3k5f3l5v3m5/3o6P3r6/3t7v3v8P3x8f3y8v3y8v3z8/3z8/709P709f719f719f729v739/739/74+P75+f76+v77+/79/f7+/v7+/v7+/v7+/v7+/v7+/v7+/v7+/v7+/v/+/v///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////wj+AOEJHEiwoMGDCBMqXMiwocOHECNKnEixosWLGDNq3Mixo8ePIEOKHEmypMmTKFOqXMmypcuXMGPKnEmzps2bOHPq3Mmzp8+fQIMKHUq0qNGjSJMqXcq0qUtnrjhNmsTJlTOnN0XpscO1qx5RWGlm6kq2a6awMXmVXWuHF9qXj9iWffT2Iy1auhgSkkuWEMNfuXLVZajrUZvDbQblTci3rMJfkPxI9pPp1+CDkxBrnkOKceOuCVlNHu1n0+WBygZpXj1HGMLPoA/+EkR6NKSrlx2t3u0HoSLYihAuqk367GBau5PTwgx70sFcxGsLrpsp+W5HB4dt5atn2MFM0Un+hxpc3fpqhKIagz0YXjx58+cR3tpbltCthO1Hj6+7Cr7mhcPcwokot3inUH6TzXIZHv4lxlFk+SFyWnnwubbRLwjud9kf8HXikSntLXKaQLrIkZwcy32UC221CWLZiPDoohpicjhiIUjEZMKiZJC8CCOJmWSyijImOTNLKKH4uNQrkPQBSShEDhWNLKJcIsouPCnTBxdccklGJUKVsggiZJJ5STQ5KYNHl2xygUeUPEUjSZl0IiIJmjc90mabZATDUzFj1knnejXhsueeg/BkiaCC3jiTIofu+YpOuzAqKCc1KUNGpG1SolMplta5CJ4yvcJpp5+GWieWM5l6apf+Gt4Ei6p0siqToa9yMQZPitBKpqMx8ZErXTuJ4qtxNAXz6hh+7lRMr6oWc9MrY3Aaq06rhKqIrTapeagmQGUr6J08vTJIl3xMGtQui5KZCbc9wVkUqT/Wa++9+Oar7778GmXNM84oI/Az0VjDlDGibOLIwpl8YmBL0ggs8cTTKPWKJAtnnLEpLU0z8ccCG3yULxqXvLC6KXEDMsi4GZWJySVLIo1K1awMsshEDQOzydOh5LHNE+M8FMk7a7yKStEATbFRqxStMaEnJa20wBUX1bTTC0Nt0s9TCy0U0VgfnVLNUyvjdVA6Y+1IzyepPHXLRb3stMwrcW3z2V9jjbL+ShHbXDVSF+/MsUv/BjxwwQcnnHHDD/fr+OOQRy755JTri3dQMwMlzTC+dD5M5kAJU+Ull5QC7E3WCNP56p0jA5QtpMdOOiig2yQNMKznvoxPsMsueyanx2RN7sQDczlNy2Ti+/K718Q58blLuxMryy9fSk3TQF88T8pX7zsxNEmjPfHN45SL98sHD/H4uRujkyzo+64+S9bgzn7n7ucEf/ykZ1J7TMS4X+f+VxNh8I90qrBJ9u7XOJyQ4oDgs0kx2Gc8niQvfqzASeq0J4zj1UQan/DeJwg4E2s8b3XAyB9QZNG92ImChB8sxjCKsQwP6kQauWCFKFgxv6I8Qxj+wjCGDTdCDWHgAhfSq9czcjGLJjbRF3/7iDNe4YkqVlEVSTzNM2rhxC7WonwdEUYorEjGUGSxLtZgYhfX2MCM6IKMcPTE4C4jjDXacRbP4Egx4hhHXJzmFndcI9swogo+wnEUlzFGIO24kGUUYxe7oOFCDBnHHiqljovsYkKK4QpUePKTrjgjQSgJRz/WBZOZnEUtEPKLT7ryk0oaJSmtaMq3oDKTvThINFTxyleqgl4DoeIsPVFLtCwjlU3Mo0Fu0cte3scgwximJ9qIlV6kcpADkUUzXykLhJhilnt7SxoXWYsoEmSbvURIMcZoSFVQA0bAuGMtIngQdL4yIb12YGcp7fUMazoxF8pEiD1dqZBiFNKKrxDlj5axjIAmpJMD7eZCZEjNytVioKhYZeUqsgyMgnGjEsmFPbEJ0oj8gpe+jGVJJ1KNYeziFrfYxTCqsdKa2vSmOM2pTnfK05769KdADapQh0rUohr1qEhNqlKXutSAAAAh+QQJBAD1ACwAAAAAjgCOAIcAAAABAQECAgIDAwMEBAQFBQUGBgYHBwcICAgJCQkKCgoLCwsMDAwNDQ0ODg4PDw8QEBARERESEhITExMUFBQVFRUWFhYXFxcYGBgZGRkaGhobGxscHBwdHR0eHh4fHx8gICAhISEiIiIjIyMkJCQlJSUmJiYnJycoKCgpKSkqKiorKyssLCwtLS0uLi4vLy8wMDAxMTEyMjIzMzM0NDQ1NTU2NjY3Nzc4ODg5OTk6Ojo7Ozs8PDw9PT0+Pj4/Pz9AQEBBQUFCQkJDQ0NERERFRUVGRkZHR0dISEhJSUlKSkpoU1OXYGG2aWvLb3HZc3XkdnnqeHvueXzxeXzyeXzzeXzzeXvzeHvzeHvzeHvzeHvzeHvzeHvzd3rzd3rzd3rzd3rzd3rzdXjzcXTzbnHya27yZ2rxY2bxXF/xWFvwVFfwUlXwUFTwT1LwTFDwS0/wSU3wSEzwSEzwSk3wTFDwUFPwU1bwV1rxWl3xXmHxYGPxZGfxZmnxZ2ryaGvya23ybXDyb3LycnXzeHv0fYD0gYP0g4b0hYj0h4r1iYv1i471jpD1kJL1kpX1lZf2l5n2mJr2mZv2nZ/3oKL3pKb3pqj3qKr3qav4qqz4q634rK74sLL4srP4tLX4trj5uLn5ubr5urv5urv5u7z5vb75vr/5wMH5wsP5xMX5xcb6x8j6x8j6yMn6yMn6ycr6ycr6ycr6y8z6zM36zs/60NH70dL709T71db719j72Nn72Nn72Nn72dr72tv829z83N383t784OD84uL85OX85ub85+f85+j96er96er96uv97Oz97e797u/98PD98vL98/P99PT99fX99vb99vb+9/f++Pj++Pj++Pn++fn++fn++fn++fn++fn++fr++vr++/v++/z+/f3+/f7+/v7+/v7+/v7+/v7+/v7+/v7+/v7+/v7///////////////////////////////////////////////////////////////////////////////////8I/gDrCRxIsKDBgwgTKlzIsKHDhxAjSpxIsaLFixgzatzIsaPHjyBDihxJ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AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=='
            
            var formData = new FormData();
            formData.append('Id_1', id_fierro);

            $.ajax({
                type: 'POST',
                data: formData,
                url: '/Admin/Compra/GetFierroXIDFierro/',
                contentType: false,
                processData: false,
                cache: false,
                success: function (response) {
                    
                    if (response.Success) {
                        
                        var imagen = response.Mensaje;
                        var extension = "";
                        var position = imagen.indexOf("iVBOR");

                        if (position != -1)
                            extension = "image/png";

                        position = imagen.indexOf("/9j/4");
                        if (position != -1)
                            extension = "image/jpeg";
                        //bmp de 256 colores
                        position = imagen.indexOf("Qk3");
                        if (position == 0)
                            extension = "image/bmp";

                        //bmp de monocromatico colores
                        position = imagen.indexOf("Qk2");
                        if (position == 0)
                            extension = "image/bmp";

                        //bmp de 16 colores
                        position = imagen.indexOf("Qk1");
                        if (position == 0)
                            extension = "image/bmp";
                        
                        var imagenFinal = 'data:' + extension + ';base64,' + imagen;

                        document.getElementById('base64image').src = imagenFinal;
                    }
                    else
                        Mensaje(response.Mensaje, "2");
                }
            });
        });

        $('#btnAddRowGanado').on('click', function () {
            var numero = document.getElementById("inputGanado").value;
            if (!/^([0-9])*$/.test(numero)) {
                Mensaje("Por favor, ingrese un número.", 2);
            }
            else if (numero.length == 0) {
                Mensaje("Por favor, ingrese un número.", 2);
            }
            else {
                for (var i = 0; i < numero; i++) {
                    AgergarFilas(-1, false, "Sin registrar", '', "macho", '', '', '');
                    numeroFila++;
                }
            }
            //después de inserter muestra las nuevas filas
            tblGanadoProgramado.order([0, 'desc']).draw(false);
        });

        $("#tblGanadoProgramado tbody").on("click", ".deleteGanado", function (e) {

            var tr = $(this).parents('tr');
            var url = $(this).attr('data-hrefa');
            var id_ganado = $(this).attr('data-id');

            var box = $("#mb-delete-ganado");
            box.addClass("open");
            box.find(".mb-control-yes").on("click", function () {
                box.removeClass("open");
                if (id_ganado != -1) {
                    $.ajax({
                        url: url,
                        data: { IDCompra: IDCompra, IDGanado: id_ganado },
                        type: 'POST',
                        dataType: 'json',
                        success: function (result) {
                            if (result.Success) {
                                box.find(".mb-control-yes").prop('onclick', null).off('click');
                                Mensaje(result.Mensaje, "1");
                                tblGanadoProgramado.row(tr).remove().draw(false);
                            }
                            else
                                Mensaje(result.Mensaje, "2");
                        }
                    });
                }
                else {
                    tblGanado.row(tr).remove().draw(false);
                }

            });
        });

        $('#btnSaveRowGanado').on('click', function () {
            var flag = true;
            var NUM_ELEMENTOS_FILA = 9;

            guardarIDs = eliminarDuplicadosArray(guardarIDs);
           
            for (var index = 0; index < guardarIDs.length; index++) {
                var id_guardado = guardarIDs[index];
                //comparo en los nodos y avanzo en grupo de la cantidad de filas
                for (var i = 0; i < nNodes.length; i += NUM_ELEMENTOS_FILA) {
                    //obtengo el id de la fila
                    var id_fila = nNodes[i + 2].dataset.id;
                    //son iguales, valido 
                    if (id_guardado == id_fila) {
                        /*INICIA VALIDACION*/
                        //arete
                        if (nNodes[i + ARETE].value.length < longitud_permitida_arete || nNodes[i + ARETE].value === '' || nNodes[i + ARETE].value == null) {
                            nNodes[i + ARETE].classList.remove('okCSLGanado');
                            nNodes[i + ARETE].classList.add('errorCSLGanado');
                            flag = false;
                        }
                        else {
                            nNodes[i + ARETE].classList.remove('errorCSLGanado');
                            nNodes[i + ARETE].classList.add('okCSLGanado');
                        }
                        //fierro1, solo 1 fierro necesario
                        if (nNodes[i + FIERRO1].value.length == 0 || nNodes[i + FIERRO1].value == '' || nNodes[i + FIERRO1].value == null || nNodes[i + FIERRO1].value <= 0) {
                            nNodes[i + FIERRO1].classList.add('errorCSLGanado');
                            nNodes[i + FIERRO1].classList.remove('okCSLGanado');
                            flag = false;
                        }
                        else {
                            nNodes[i + FIERRO1].classList.add('okCSLGanado');
                            nNodes[i + FIERRO1].classList.remove('errorCSLGanado');
                        }

                        if (flag) {
                            nNodes[i].src = "/Content/img/tabla/loading.gif";
                            nNodes[i + MENSAJE].innerText = "Guardando";
                            var id_ganado = nNodes[i + ARETE].dataset.id;
                            var numArete = nNodes[i + ARETE].value;
                            var id_genero = nNodes[i + GENERO].value;
                            var id_fierro1 = nNodes[i + FIERRO1].selectedOptions[0].dataset.id;
                            var id_fierro2 = nNodes[i + FIERRO2].selectedOptions[0].dataset.id;
                            var id_fierro3 = nNodes[i + FIERRO3].selectedOptions[0].dataset.id;

                            $.ajax({
                                url: '/Admin/Compra/AC_GanadoProgramado/',
                                type: "POST",
                                data: {
                                    IDCompra: IDCompra, IDGanado: id_ganado, numArete: numArete, id_genero: id_genero,
                                    indiceActual: i, id_fierro1: id_fierro1, id_fierro2: id_fierro2, id_fierro3: id_fierro3
                                },
                                success: function (response) {
                                    var obj = JSON.parse(response.Mensaje);
                                    var indice = parseInt(obj.indiceActual);

                                    if (response.Success) {
                                        //imagen
                                        nNodes[indice].src = "/Content/img/tabla/ok.png";
                                        nNodes[indice].id = "img_" + obj.id_ganado;
                                        //label
                                        nNodes[indice + MENSAJE].id = "lbl_" + obj.id_ganado;
                                        nNodes[indice + MENSAJE].innerText = "Registrado";
                                        //numArete 
                                        nNodes[indice + ARETE].id = "arete_" + obj.id_ganado;
                                        nNodes[indice + ARETE].dataset.id = obj.id_ganado;
                                        nNodes[indice + ARETE].dataset.iddetalledocumento = obj.id_detalleDoctoCobrar;
                                        //genero
                                        nNodes[indice + GENERO].id = "genero_" + obj.id_ganado;
                                        //a (btn eliminar)
                                        nNodes[indice + BTN_ELIMINAR].id = "a_" + obj.id_ganado;
                                        nNodes[indice + BTN_ELIMINAR].dataset.id = obj.id_ganado;
                                        nNodes[indice + BTN_ELIMINAR].dataset.iddetalledocumento = obj.id_detalleDoctoCobrar
                                        //amin (btn eliminar min)
                                        nNodes[indice + BTN_ELIMINAR_MIN].id = "aMin_" + obj.id_ganado;
                                        nNodes[indice + BTN_ELIMINAR_MIN].dataset.id = obj.id_ganado;
                                        nNodes[indice + BTN_ELIMINAR_MIN].dataset.iddetalledocumento = obj.id_detalleDoctoCobrar
                                    }
                                    else {
                                        nNodes[indice].src = "/Content/img/tabla/cancel.png";
                                        nNodes[indice + MENSAJE].innerText = obj.Mensaje;
                                    }
                                }
                            });

                        }

                    }
                }
            }
        });

        $('#tblGanadoProgramado tbody').on('change', '.inputCSL', function (e) {
            var $td = $(this).parent();
            $td.find('input').attr('value', this.value);

            //obtenemos el valor de toda la fila
            var $tr = $(this).closest("tr");
            var fila = $tr.find('.cslElegido');

            var idx = fila[ARETE].dataset.id;
            tblGanadoProgramado.cell($td).invalidate('dom').draw(false);
            guardarIDs.push(idx);

            var id = this.id;
            var arete_ganado = "arete_";
            var buscar = id.indexOf(arete_ganado);

            if (buscar != -1) {
                var subtring = obtenerSubstring($(this).val());
                $(this).val(subtring);
            }
        });

        $("#tblGanado tbody").on('change', '.selectCSL', function (e) {
            var $td = $(this).parent();
            var value = this.value;

            $td.find('option').each(function (i, o) {
                if ($(o).val() == value) {
                    $(o).attr('selected', "selected");
                }
                else {
                    $(o).removeAttr('selected');
                }
            })

            //solo se le asigna el calculo al select de genero
            var id = this.id;
            var genero_ganado = "genero_";
            var buscar = id.indexOf(genero_ganado);

            if (buscar != -1) {
                calculosGanado(fila);
            }

            //actualizo datatables y guardo el nodo actualizado
            var idx = fila[ARETE].dataset.id;
            tblGanadoProgramado.cell($td).invalidate('dom').draw(false);
            guardarIDs.push(idx);
        });

        function eliminarDuplicadosArray(arr) {
            var i,
                len = arr.length,
                out = [],
                obj = {};

            for (i = 0; i < len; i++) {
                obj[arr[i]] = 0;
            }
            for (i in obj) {
                out.push(i);
            }
            return out;
        }
    }
    var LoadTableGanadoProgramado = function () {
        tblGanadoProgramado = $('#tblGanadoProgramado').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            "searching": false,
            "ordering": false,
            "autoWidth": false,
            columnDefs: [
                { "type": "html-input", "targets": [1, 2, 3, 4, 5, 6] }
            ]

        });

        //para que realice la busqueda por el valor de cada elemento
        $.fn.dataTableExt.ofnSearch['html-input'] = function (value) {
            return $(value).val();
        };

        $.ajax({
            url: '/Admin/Compra/DatatableGanadoProgramado/',
            type: "POST",
            data: { IDCompra: IDCompra },
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    AgergarFilas(
                        data[i].id_ganadoProgramado, true, "Registrado", data[i].numArete, data[i].genero,
                        data[i].id_fierro1, data[i].id_fierro2, data[i].id_fierro3);
                }
            }
        });
    }

    function AgergarFilas( id_fila, guardado, mensaje, numArete, genero, id_fierro1, id_fierro2, id_fierro3) {
        //columna, imagen y aviso
        var html_imagen = '';
        if (guardado)
            html_imagen = '<img id="img_' + id_fila + '" class="cslElegido"  src="/Content/img/tabla/ok.png" alt="" height="42" width="42"> <label id="lbl_' + id_fila + '" class="cslElegido" for="' + mensaje + '">' + mensaje + '</label>';
        else
            html_imagen = '<img id="img_' + id_fila + '" class="cslElegido" src="/Content/img/tabla/cancel.png" alt="" height="42" width="42"> <label id="lbl_' + id_fila + '" class="cslElegido" for="' + mensaje + '">' + mensaje + '</label>';
        //columna, arete
        var html_arete = '<input id="arete_' + id_fila + '" data-id="' + id_fila + '" class="form-control inputCSL cslElegido cslArete" type="text" maxlength="15" value="' + numArete + '" data-toggle="tooltip" data-placement="top" title="Por favor, escriba el número de arete.">';
        //columna, genero
        var html_macho = '<option value="MACHO">Macho</option>';
        var html_hembra = '<option value="HEMBRA">Hembra</option>';

        if (genero == "Macho" || genero == "MACHO") {
            html_macho = '<option value="Macho" selected>Macho</option>';
        }
        else if (genero == "Hembra" || genero == "HEMBRA") {
            html_hembra = '<option value="Hembra" selected>Hembra</option>';
        }
        var html_genero = '<select id="genero_' + id_fila + '"class="form-control selectCSL cslElegido" data-toggle="tooltip" data-placement="top" title="Por favor, seleccione el género del animal." >' +
            html_macho +
            html_hembra +
            '</select> ';

        //columna, fierro1
        var html_fierro1 = '<select id="fierro1_' + id_fila + '"class="form-control selectCSL cslElegido" data-toggle="tooltip" data-placement="top" title="Fierro 1 seleccionado." >';
        var opciones_fierro1 = '';

        for (var item in listaFierros) {
            var id_fierro_server = listaFierros[item].IDFierro;

            if (id_fierro1.localeCompare(id_fierro_server) == 0) {

                opciones_fierro1 += '<option value="' + listaFierros[item].IDFierro + '" data-id="' + listaFierros[item].IDFierro + '" selected>' + listaFierros[item].NombreFierro + '</option>';
            }
            else {
                opciones_fierro1 += '<option value="' + listaFierros[item].IDFierro + '" data-id="' + listaFierros[item].IDFierro + '">' + listaFierros[item].NombreFierro + '</option>';
            }
        }
        html_fierro1 += opciones_fierro1;
        html_fierro1 += '</select> ';

        //columna, fierro2
        var html_fierro2 = '<select id="fierro2_' + id_fila + '"class="form-control selectCSL cslElegido" data-toggle="tooltip" data-placement="top" title="Fierro 2 seleccionado." >';
        var opciones_fierro2 = '';

        for (var item in listaFierros) {
            var id_fierro_server = listaFierros[item].IDFierro;

            if (id_fierro2.localeCompare(id_fierro_server) == 0) {

                opciones_fierro2 += '<option value="' + listaFierros[item].NombreFierro + '" data-id="' + listaFierros[item].IDFierro + '" selected>' + listaFierros[item].NombreFierro + '</option>';
            }
            else {
                opciones_fierro2 += '<option value="' + listaFierros[item].NombreFierro + '" data-id="' + listaFierros[item].IDFierro + '">' + listaFierros[item].NombreFierro + '</option>';
            }
        }
        html_fierro2 += opciones_fierro2;
        html_fierro2 += '</select> ';

        //columna, fierro3
        var html_fierro3 = '<select id="fierro3_' + id_fila + '"class="form-control selectCSL cslElegido" data-toggle="tooltip" data-placement="top" title="Fierro 3 seleccionado." >';
        var opciones_fierro3 = '';

        for (var item in listaFierros) {
            var id_fierro_server = listaFierros[item].IDFierro;

            if (id_fierro3.localeCompare(id_fierro_server) == 0) {

                opciones_fierro3 += '<option value="' + listaFierros[item].NombreFierro + '" data-id="' + listaFierros[item].IDFierro + '" selected>' + listaFierros[item].NombreFierro + '</option>';
            }
            else {
                opciones_fierro3 += '<option value="' + listaFierros[item].NombreFierro + '" data-id="' + listaFierros[item].IDFierro + '">' + listaFierros[item].NombreFierro + '</option>';
            }
        }
        html_fierro3 += opciones_fierro3;
        html_fierro3 += '</select> ';

        tblGanadoProgramado.row.add([
            html_imagen,
            html_arete,
            html_genero,
            html_fierro1,
            html_fierro2,
            html_fierro3,
            ' <div class="visible-md visible-lg hidden-sm hidden-xs">' +
            '<a id="a_' + id_fila + '" data-hrefa="/Admin/Compra/DEL_GanadoProgramado/" title="Eliminar" data-id="' + id_fila + '" class="btn btn-danger tooltips btn-sm deleteGanado cslElegido" data-toggle="tooltip" data-placement="top" data-original-title="Eliminar"><i class="fa fa-trash-o"></i></a>' +
            '</div>' +
            '<div class="visible-xs visible-sm hidden-md hidden-lg">' +
            '<div class="btn-group">' +
            '<a class="btn btn-danger dropdown-toggle btn-sm" data-toggle="dropdown" href="#"' +
            '<i class="fa fa-cog"></i> <span class="caret"></span>' +
            '</a>' +
            '<ul role="menu" class="dropdown-menu pull-right dropdown-dark">' +
            '<li>' +
            '<a id="aMin_' + id_fila + '" data-hrefa="/Admin/Compra/DEL_GanadoProgramado/" data-id="' + id_fila + '" class="deleteGanado cslElegido" role="menuitem" tabindex="-1" data-id="">' +
            '<i class="fa fa-trash-o"></i> Eliminar' +
            '</a>' +
            '</li>' +
            '</ul>' +
            '</div>' +
            '</div>'
        ]).draw(false);

        nNodes = tblGanadoProgramado.rows().nodes().to$().find('.cslElegido');
    }
    function obtenerSubstring(valor) {
        var nuevo_value = valor;
        var longitud_input = valor.length;

        if (longitud_input > longitud_permitida_arete) {
            var diferencia = longitud_input - longitud_permitida_arete;
            nuevo_value = valor.substring(diferencia, (longitud_permitida_arete + diferencia));
        }

        return nuevo_value;
    }
    /*TERMINA FIERROS*/

    /*INICIA FUNCIONES MIXTAS*/
    function DesbloquearTabs() {
        //Solo el primer tab esta desbloqueado
        var flete = $("#IDFlete");
        var compra = $("#IDCompra");

        if (compra.val().length == 36) {
            document.getElementById("tabFlete").dataset.toggle = "tab";
            $('#tabFlete').data('toggle', "tab")
            $("#liFlete").removeClass('disabled').addClass('pestaña');

            //document.getElementById("tabFierro").dataset.toggle = "tab";
            //$('#tabFierro').data('toggle', "tab")
            //$("#liFierro").removeClass('disabled').addClass('pestaña');
        }
        //if (flete.val().length == 36) {
        //    //Hay un flete desbloqueamos los demas tabs
        //    document.getElementById("tabDocumentos").dataset.toggle = "tab";
        //    $('#tabDocumentos').data('toggle', "tab")
        //    $("#liDocumentos").removeClass('disabled').addClass('pestaña');
        //    LoadTableDocumentos();
            
        //}
    }

    $("a[href='#flete']").on('show.bs.tab', function (e) {
        ToggleDivTipoFlete(opcionServer);
        //ToggleDivBancarizado(Bancarizado.val());
    });

    /*TERMINA FUNCIONES MIXTAS*/
    return {
        init: function (listafierro) {
            //listaFierros = listafierro;
            DesbloquearTabs();
            LoadValidationProveedor();
            RunEventsProveedor();
            if (navigator.onLine) {
                console.log("online");
                InitMap();
            }
            else {
                console.log("offline");
            }
            LoadValidationFlete();
            RunEventsFlete();
            //LoadTableGanadoProgramado();
            //RunEventsFierro();
           
        }
    };
}();