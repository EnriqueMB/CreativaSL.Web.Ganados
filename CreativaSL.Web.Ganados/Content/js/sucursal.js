﻿var Sucursal = function () {
    "use strict"
     
    var Validaciones = function () {
        var form1 = $('#frmSucursal');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);
        $('#frmSucursal').validate({ // initialize the plugin
            //debug: true,
            errorElement: "span", // contain the error msg in a span tag
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
               }
            },
            ignore: "",
            rules: {
                NombreSucursalMatriz: {
                    minlength: 5,
                    maxlength: 300
                },
                NombreSucursal: {
                    required: true,
                    minlength: 5,
                    maxlength: 300
                },
                Direccion: {
                    required: true,
                    minlength: 5,
                    maxlength: 300
                },
                MermaPredeterminada: {
                    required: true,
                    digits: true
                }
            },
            messages: {
                NombreSucursalMatriz: {
                    minlength: jQuery.validator.format("-El campo: Nombre Empresa,  debe tener un mínimo de caracteres de: {0}"),
                    maxlength: jQuery.validator.format("-El campo: Nombre Empresa,  debe tener un máximo de caracteres de: {0}")
                },
                NombreSucursal: {
                    required: "-El campo: Nombre Sucursal, es requerido",
                    minlength: jQuery.validator.format("-El campo: Nombre Sucursal,  debe tener un mínimo de caracteres de: {0}"),
                    maxlength: jQuery.validator.format("-El campo: Nombre Sucursal,  debe tener un máximo de caracteres de: {0}")
                },
                Direccion: {
                    required: "-El campo: Dirección, es requerido",
                    minlength: jQuery.validator.format("-El campo: Dirección,  debe tener un mínimo de caracteres de: {0}"),
                    maxlength: jQuery.validator.format("-El campo: Dirección,  debe tener un máximo de caracteres de: {0}")
                },
                MermaPredeterminada: {
                    required: "-El campo: Merma predeterminada, es requerido",
                    digits: "-El campo: Merma predeterminada, debe ser igual o mayor que 0 (solo números enteros)."
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
    }
     
    return {
        init: function () {
            Validaciones();
        }
    };
}();