var Empresa = function () {
    var TblCuentasBancarias;
    var TblArchivos;
    "use strict";
    var RunFileInput = function (LogoRFC, LogoEmpresa){
        $('#LogoEmpresaHttp').fileinput({
            theme: 'fa',
            language: 'es',
            showUpload: false,
            uploadUrl: "#",
            autoReplace: true,
            overwriteInitial: true,
            showUploadedThumbs: false,
            maxFileCount: 1,
            initialPreview: [
                '<img class="file-preview-image" style="width: auto; height: auto; max-width: 100%; max-height: 100%;" src="' + LogoEmpresa + '" />'
            ],
            initialPreviewConfig: [
                { caption: 'Logo de la empresa' }
            ],
            initialPreviewShowDelete: false,
            showRemove: false,
            showClose: false,
            layoutTemplates: { actionDelete: '' },
            allowedFileExtensions: ["png", "jpg", "jpeg", "bmp", 'heic'],
            required: true,

            previewFileIcon: '<i class="fa fa-file"></i>',
            preferIconicPreview: true, // this will force thumbnails to display icons for following file extensions
            previewFileIconSettings: { // configure your icon file extensions
                'heic': '<i class="fa fa-file-text text-primary"></i>'
            },
            previewFileExtSettings: { // configure the logic for determining icon file extensions
                'heic': function (ext) {
                    return ext.match(/(heic)$/i);
                }
            }

        });
        $('#LogoRFCHttp').fileinput({
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
                '<img class="file-preview-image" style="width: auto; height: auto; max-width: 100%; max-height: 100%;" src="' + LogoRFC + '" />'
            ],
            initialPreviewConfig: [
                { caption: 'Imagen del R.F.C.' }
            ],
            initialPreviewShowDelete: false,
            layoutTemplates: { actionDelete: '' },
            allowedFileExtensions: ["png", "jpg", "jpeg", "bmp", "heic"],
            required: true,

            previewFileIcon: '<i class="fa fa-file"></i>',
            preferIconicPreview: true, // this will force thumbnails to display icons for following file extensions
            previewFileIconSettings: { // configure your icon file extensions
                'heic': '<i class="fa fa-file-text text-primary"></i>'
            },
            previewFileExtSettings: { // configure the logic for determining icon file extensions
                'heic': function (ext) {
                    return ext.match(/(heic)$/i);
                }
            }
        });
    };
    var LoadTableCuentasBancarias = function (IDEmpresa) {

        TblCuentasBancarias = $('#TblCuentasBancarias').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "IDEmpresa": IDEmpresa
                },
                "url": "/Admin/CatEmpresa/LoadTableCuentasBancarias/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ""
            },
            "columns": [
                {
                    "data": null,
                    "render": function (data, type, full) {
                        console.log(full);
                        return "<img style='width: 100px; height: 100px; max-width: 100 %; max-height: 100 %;' src='" + full["ImgBanco"] + "' />";
                    }
                },
                { "data": "NomBanco" },
                { "data": "Titular" },
                { "data": "NumTarjeta" },
                { "data": "NumCuenta" },
                { "data": "ClabeInter" },
                {
                    "data": null,
                    "render": function (data, type, full) {

                        return "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a data-id='" + full["IDDatosBan"] + "' title='Editar' class='btn btn-yellow tooltips btn-sm editCuentaBancaria' data-placement='top' data-original-title='Edit'><i class='fa fa-edit'></i></a>" +
                            "<a title='Eliminar' data-id='" + full["IDDatosBan"] + "' data-hrefa='/Admin/CatEmpresa/DeleteCuentaBancaria/" + full["IDDatosBan"] + "' class='btn btn-danger tooltips btn-sm deleteCuentaBancaria' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
                            "</div>" +
                            "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                            "<div class='btn-group'>" +
                            "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                            "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                            "</a>" +
                            "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +
                            "<li>" +
                            "<a  data-id='" + full["IDDatosBan"] + "' class='editCuentaBancaria' role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-edit'></i> Editar" +
                            "</a>" +
                            "</li>" +
                            "<li>" +
                            "<a class='deleteCuentaBancaria' role='menuitem' tabindex='-1'  data-id='" + full["IDDatosBan"] + "' data-hrefa='/Admin/CatEmpresa/DeleteCuentaBancaria/" + full["IDDatosBan"] + "'>" +
                            "<i class='fa fa-trash-o'></i> Eliminar" +
                            "</a>" +
                            "</li>" +
                            "</ul>" +
                            "</div>" +
                            "</div>";
                    }
                }
            ],
            //Para agregar algún evento a la tabla se puede poner aquí
            "drawCallback": function (settings) {
                $(".editCuentaBancaria").on("click", function () {
                    var IDCuentaBancaria = $(this).data("id")
                    ModalCuentaBancaria(IDCuentaBancaria, IDEmpresa);
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

    };
    var LoadTableArchivos = function (IDEmpresa) {
        TblArchivos = $('#TblArchivos').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "IDEmpresa": IDEmpresa
                },
                "url": "/Admin/CatEmpresa/LoadTableArchivos/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "nombreArchivo" },
                { "data": "descripcion" },
                {
                    "data": null,
                    "render": function (data, type, full) {

                        return "<div class='visible-md visible-lg hidden-sm hidden-xs'>" +
                            "<a href='/Admin/CatEmpresa/DescargarArchivo?nombreArchivoServer=" + full["urlArchivo"] +"&nombreArchivo=" + full["nombreArchivo"] + "' target='_blanck' title='Descargar' class='btn btn-success tooltips btn-sm descargarArchivo' data-placement='top' data-original-title='Descargar'><i class='fa fa-cloud-download'></i></a>" +
                            "<a title='Eliminar' data-hrefa='/Admin/CatEmpresa/EliminarArchivo?nombreArchivoServer=" + full["urlArchivo"] + "&id=" + full["id"] + "' class='btn btn-danger tooltips btn-sm deleteArchivo' data-placement='top' data-original-title='Eliminar'><i class='fa fa-trash-o'></i></a>" +
                            "</div>" +
                            "<div class='visible-xs visible-sm hidden-md hidden-lg'>" +
                            "<div class='btn-group'>" +
                            "<a class='btn btn-danger dropdown-toggle btn-sm' data-toggle='dropdown' href='#'" +
                            "<i class='fa fa-cog'></i> <span class='caret'></span>" +
                            "</a>" +
                            "<ul role='menu' class='dropdown-menu pull-right dropdown-dark'>" +

                            "<li>" +
                            "<a  href='/Admin/CatEmpresa/DescargarArchivo?nombreArchivoServer=" + full["urlArchivo"] + "&nombreArchivo=" + full["nombreArchivo"] + "' target='_blanck' class='descargarArchivo' role='menuitem' tabindex='-1'>" +
                            "<i class='fa fa-cloud-download'></i> Descargar" +
                            "</a>" +
                            "</li>" +
                           
                            "<li>" +
                            "<a class='deleteArchivo' role='menuitem' tabindex='-1'  data-hrefa='/Admin/CatEmpresa/EliminarArchivo?nombreArchivoServer=" + full["urlArchivo"] + "&id=" + full["id"] + "'>" +
                            "<i class='fa fa-trash-o'></i> Eliminar" +
                            "</a>" +
                            "</li>" +

                            "</ul>" +
                            "</div>" +
                            "</div>";
                    }
                }
            ],
            //Para agregar algún evento a la tabla se puede poner aquí
            "drawCallback": function (settings) {
                $(".deleteArchivo").on("click", function () {
                    var url = $(this).attr('data-hrefa');
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
                                    TblArchivos.ajax.reload();
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

    };
    var LoadModal = function (IDEmpresa) {
        $("#btnCrearCuentaBancaria").on("click", function () {
            ModalCuentaBancaria(0, IDEmpresa);
        });
    };
    var Validaciones = function () {
        var form1 = $('#frmEditEmpresa');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);
        $('#frmEditEmpresa').validate({ // initialize the plugin
            //debug: true,
            errorElement: "dd", // contain the error msg in a span tag
            errorClass: 'help-block color',
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
                RazonFiscal: {
                    required: true,
                    minlength: 5,
                    maxlength: 150
                },
                DireccionFiscal: {
                    required: true,
                    minlength: 10,
                    maxlength: 150
                },
                RFC: {
                    required: true,
                    minlength: 10,
                    maxlength: 15,
                    rfc: true
                },
                NumTelefonico1: {
                    required: true,
                    minlength: 7,
                    maxlength: 15
                },
                NumTelefonico2: {
                    maxlength: 15
                },
                Email: {
                    required: true,
                    email: true,
                    minlength: 5,
                    maxlength: 50
                },
                HorarioAtencion: {
                    minlength: 5,
                    maxlength: 150
                },
                Representante: {
                    minlength: 10,
                    maxlength: 100
                },
                LogoEmpresaHttp: {
                    ImagenRequerida: ["LogoEmpresa"]
                },
                LogoRFCHttp: {
                    ImagenRequerida: ["ImagBDRFC"]
                },
                PSGEmpresa: {
                    required: true
                },
                Rubro: {
                    required: true
                }
            },
            messages: {
                RazonFiscal: {
                    required: "Por favor, escriba la razón fiscal.",
                    minlength: jQuery.validator.format("Razón Fiscal mínimo de caracteres: {0}"),
                    maxlength: jQuery.validator.format("Razón Fiscal máximo de caracteres: {0}")
                },
                DireccionFiscal: {
                    required: "Por favor, escriba la dirección fiscal.",
                    minlength: jQuery.validator.format("Direccion Fiscal, mínimo de caracteres: {0}"),
                    maxlength: jQuery.validator.format("Direccion Fiscal, máximo de caracteres: {0}")
                },
                RFC: {
                    required: "Por favor, escriba el R.F.C.",
                    minlength: jQuery.validator.format("R.F.C., mínimo de caracteres: {0}"),
                    maxlength: jQuery.validator.format("R.F.C., máximo de caracteres: {0}"),
                    rfc: "R.F.C. no válido"
                },
                NumTelefonico1: {
                    required: "Por favor, escriba un número telefónico.",
                    minlength: jQuery.validator.format("Núm. Telefónico 1, mínimo de caracteres: {0}"),
                    maxlength: jQuery.validator.format("Núm. Telefónico 1, máximo de caracteres: {0}")
                },
                NumTelefonico2: {
                    maxlength: jQuery.validator.format("Núm. Telefónico 2, máximo de caracteres: {0}")
                },
                Email: {
                    required: "Por favor, escriba un email.",
                    email: "Formato de correo no válido",
                    minlength: jQuery.validator.format("Email, mínimo de caracteres: {0}"),
                    maxlength: jQuery.validator.format("Email, máximo de caracteres: {0}")
                },
                HorarioAtencion: {
                    minlength: jQuery.validator.format("Horario de Atención, Mínimo de caracteres: {0}"),
                    maxlength: jQuery.validator.format("Horario de Atención, Máximo de caracteres: {0}")
                },
                Representante: {
                    minlength: jQuery.validator.format("Representante, Mínimo de caracteres: {0}"),
                    maxlength: jQuery.validator.format("Representante, Máximo de caracteres: {0}")
                },
                PSGEmpresa: {
                    required: "Por favor, escriba el P.S.G."
                },
                Rubro: {
                    required: "Por favor, escriba el rubro."
                }
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
                SaveEmpresa();
            }
        });
    };
     //Funciones
    function ModalCuentaBancaria(IDCuentaBancaria, IDEmpresa) {
        $.ajax({
            url: '/Admin/CatEmpresa/ModalCuentaBancaria/',
            type: "POST",
            dataType: 'HTML',
            data: { IDCuentaBancaria: IDCuentaBancaria, IDCliente: IDEmpresa },
            success: function (data) {
                $('#ContenidoModalCuentasBancaria').html(data);
                $('#ModalCuentasBancaria').modal({ backdrop: 'static', keyboard: false });
                //Activamos las validaciones por parte del server
                ValidarModalCuentaBancaria();
            }
        });

    }
    function SaveEmpresa() {
        var data = $("form#frmEditEmpresa")[0];
        var fileData = new FormData(data);

        $.ajax({
            type: 'POST',
            data: fileData,
            url: '/Admin/CatEmpresa/SaveEmpresa/',
            contentType: false,
            processData: false,
            cache: false,
            success: function (response) {
                if (response.Success)
                    Mensaje(response.Mensaje, "1");
                else
                    Mensaje(response.Mensaje, "2");
            },
            failure: function (response) {
                Mensaje(response.Mensaje, "2");
            },
            error: function (response) {
                Mensaje(response.Mensaje, "2");

            }
        });


    };
    function SaveCuentaBancaria(){
        var form = $("#frmModalCuentaBancaria")[0];

        var fileData = new FormData(form);
        $.ajax({
            type: 'POST',
            data: fileData,
            url: '/Admin/CatEmpresa/InsertUpdateCuentaBancaria/',
            contentType: false,
            processData: false,
            cache: false,
            success: function (response) {
                $('#ModalCuentasBancaria').modal('hide');

                if (response.Success) {
                    Mensaje(response.Mensaje, "1");
                    TblCuentasBancarias.ajax.reload();
                }
                else
                    Mensaje(response.Mensaje, "2");
            },
            failure: function (response) {
                Mensaje(response.Mensaje, "2");
            },
            error: function (response) {
                Mensaje(response.Mensaje, "2");
            }
        });
        
    }
    function ValidarModalCuentaBancaria() {
        var form1 = $('#frmModalCuentaBancaria');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);
        $('#frmModalCuentaBancaria').validate({ // initialize the plugin
            debug: true,
            errorElement: "span", // contain the error msg in a span tag
            errorClass: 'help-block color',
            errorLabelContainer: $("#validation_summary_cuentaBancaria"),
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
                "CuentaBancaria.Titular": {
                        required: true,
                        minlength: 10,
                        maxlength: 300
                },
                "Banco.IDBanco": {
                    required: true
                },
                "CuentaBancaria.NumCuenta": {
                    cuenta: true,
                    maxlength: 20
                },
                "CuentaBancaria.NumTarjeta": {
                    maxlength: 20
                },
                "CuentaBancaria.Clabe": {
                    clabe: true,
                    maxlength: 18
                }
            },
            messages: {
                "CuentaBancaria.Titular": {
                    required: "Nombre del Titular es un campo requerido",
                    minlength: jQuery.validator.format("Nombre del Titular, mínimo de caracteres: {0}"),
                    maxlength: jQuery.validator.format("Nombre del Titular, máximo de caracteres: {0}")
                },
                "Banco.IDBanco": {
                    required: "El Banco es requerido"
                },
                "CuentaBancaria.NumCuenta": {
                    cuenta: "Digite una Cuenta Bancaria válida",
                    maxlength: jQuery.validator.format("Cuenta Bancaria, máximo de caracteres: {0}")
                },
                "CuentaBancaria.NumTarjeta": {
                    maxlength: jQuery.validator.format("Número de Tarjeta, máximo de caracteres: {0}")
                },
                "CuentaBancaria.Clabe": {
                    clabe: "Ingrese una clabe interbancaria válida.",
                    maxlength: jQuery.validator.format("Clabe interbancaria, máximo de caracteres: {0}")
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
                SaveCuentaBancaria();
            }
        });
    }
    return {
        init: function (LogoRFC, LogoEmpresa, IDEmpresa) {
            Validaciones();
            RunFileInput(LogoRFC, LogoEmpresa);
            LoadTableCuentasBancarias(IDEmpresa);
            LoadModal(IDEmpresa);
            LoadTableArchivos(IDEmpresa);
        }
    };
}();