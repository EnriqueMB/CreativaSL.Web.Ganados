var Archivo = function () {
    "use strict";
    var RunFileInput = function () {
        $('#Archivo').fileinput({
            theme: 'fa',
            language: 'es',
            maxFileCount: 1,
            overwriteInitial: true,
            showUploadedThumbs: false,
            initialPreview: [
                '<img class="file-preview-image"  style=" width: auto !important; height: auto; max-width: 100%; max-height: 100%;" src="/Content/img/GrupoOcampo.png" />'
            ],
            initialPreviewConfig: [
                { caption: 'Archivo' }
            ],
            initialPreviewShowDelete: false,
            showRemove: false,
            showClose: false,
            showUpload: false,
            layoutTemplates: { actionDelete: '' },

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

    var Validaciones = function () {
        var form1 = $('#frm_agregarArchivo');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);
        $('#frm_agregarArchivo').validate({ // initialize the plugin
            debug: true,
            errorElement: "dd", // contain the error msg in a span tag
            errorClass: 'help-block color',
            errorLabelContainer: $("#validation_summary"),
            errorPlacement: function (error, element) { // render error placement for each input type
                if (element.attr("type") === "radio" || element.attr("type") === "checkbox") { // for chosen elements, need to insert the error after the chosen container
                    error.insertAfter($(element).closest('.form-group').children('div').children().last());
                } else if (element.attr("name") === "dd" || element.attr("name") === "mm" || element.attr("name") === "yyyy") {
                    error.insertAfter($(element).closest('.form-group').children('div'));
                } else if (element.attr("type") === "text") {
                    error.insertAfter($(element).closest('.input-group').children('div'));
                } else {
                    error.insertAfter(element);
                    // for other inputs, just perform default behavior
                }
            },
            ignore: "",
            rules: {
                Descripcion: {
                    required: true,
                    minlength: 5,
                    maxlength: 150
                },
                Archivo: {
                    LimitSize: ["Archivo", "2"]
                }
            },
            messages: {
                Descripcion: {
                    required: "Por favor, escriba la descripción de la imagen.",
                    minlength: jQuery.validator.format("El campo: descripción de la imagen tiene un mínimo de caracteres: {0}"),
                    maxlength: jQuery.validator.format("El campo: descripción de la imagen tiene un máximo de caracteres: {0}")
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
                form.submit();
            }
        });
    };

    return {
        init: function () {
            Validaciones();
            RunFileInput();
        }
    };
}();