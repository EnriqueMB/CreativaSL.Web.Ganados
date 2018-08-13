var VentaDocumento = function () {
    "use strict";
    // Funcion para validar registrar
    var tableDocumentos;

    var runValidator1 = function () {
        var form1 = $('#frm_documento');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({ // initialize the plugin
            //debug: true,
            errorElement: "dd",
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
                IDTipoDocumento: { required: true, min: 1 },
                ImagenPost: { ImagenRequerida: true, ImagenRequerida: ["ImagenServer"] }
            },
            messages: { 
                IDTipoDocumento: { required: "Por favor, seleccione un tipo de documento.", min: "Por favor, seleccione un tipo de documento." }
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
                form.submit()
            }
        });
    };
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
                '<img class="file-preview-image" style="width: auto; height: auto; max-width: 100%; max-height: 100%;" src="data:'+ ExtensionImagen +';base64,' + Imagen + '" />'
            ],
            initialPreviewConfig: [
                { caption: 'Imagen del documento' }
            ],
            initialPreviewShowDelete: false,
            showRemove: false,
            showClose: false,
            layoutTemplates: { actionDelete: '' },
            allowedFileExtensions: ["png", 'jpg', 'bmp', 'jpeg'],
            required: true
        })
    }
    return {
        init: function () {
            runValidator1();
            RunEventsDocumento();
        }
    };
}();