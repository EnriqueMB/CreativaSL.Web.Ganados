var DocumentoExtra = function () {
    "use strict";
    var archivo = document.getElementById("Archivo").defaultValue;

    var runPlugins = function () {
        $('input[name="Archivo"]').fileinput({
            language: 'es',
            showUpload: false,
            uploadUrl: "#",
            autoReplace: true,
            overwriteInitial: true,
            showUploadedThumbs: false,
            maxFileCount: 1,
            initialPreview: [
                function () {
                    var img = "";
                    if (archivo) {
                        img = '<img class="file-preview-image" style="width: auto; height: auto; max-width: 100%; max-height: 100%;" src="' +
                            archivo +
                            '">';
                    }
                    else {
                        img = '<img src="/Content/img/GrupoOcampo.png" alt="Logo" style="width: auto; height: auto; max-width: 100%; max-height: 100%;">';
                    }
                    return img;
                }
            ],
            initialPreviewShowDelete: false,
            showRemove: false,
            showClose: false,
            showZoom: true,
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
            , fileActionSettings: {
                showRemove: false
            }
        });
    };
    var runValidator = function () {
        var form = $('#form');
        var errorHandler1 = $('.errorHandler', form);
        var successHandler1 = $('.successHandler', form);

        form.validate({ // initialize the plugin
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
                IdTipoDocumentacionExtra: { required: true, min: 1 },
                Archivo: { ImagenRequerida: ["Archivo"] }
            },
            messages: {
                IdTipoDocumentacionExtra: { required: "Por favor, seleccione un tipo de documento.", min: "Por favor, seleccione un tipo de documento." }
                , Archivo: { ImagenRequerida: "Por favor seleccione una imagen." }
            },
            invalidHandler: function (event, validator) { //display error alert on form submit
                successHandler1.hide();
                errorHandler1.show();
                //$("#validation_summary").text(validator.showErrors());
            },
            highlight: function (element) {
                $(element).closest('.help-block').removeClass('valid');
                // display OK icon
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error').find('.symbol').removeClass('ok').addClass('required');
                // add the Bootstrap error class to the control group
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.form-group').removeClass('has-error');
                // set error class to the control group
            },
            success: function (label, element) {
                label.addClass('help-block valid');
                label.removeClass('color');
                // mark the current input as valid and display OK icon
                $(element).closest('.form-group').removeClass('has-error').addClass('has-success').find('.symbol').removeClass('required').addClass('ok');
            },
            submitHandler: function (form) {
                successHandler1.show();
                errorHandler1.hide();
                form.submit();
            }
        });
    };

    return {
        init: function () {
            runPlugins();
            runValidator();
        }
    };
}();