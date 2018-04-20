var Entrada = function () {
    "use strict";
    // Funcion para validar registrar
    var runValidator = function () {
        var form1 = $('#form-ed');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        $.validator.addMethod("cMaxValue", function (value, element) {
            var DataMaxValue = $(element).data('max');
            var MaxValue = $.isNumeric(DataMaxValue) ? DataMaxValue : 0;
            value = $.isNumeric(value) ? value : 0;
            return this.optional(element) || (value <= MaxValue);
        }, "Verifique la cantidad. No puede ser mayor a la cantidad disponible.");

        $.validator.addMethod("cMinValue", function (value, element, params) {            
            value = $.isNumeric(value) ? value : 0;
            var minValue = $.isNumeric(params[0]) ? params[0] : 0;
            return this.optional(element) || (value >= minValue);
        }, "Verifique la cantidad. Debe ser un número mayor o igual a {0}.");

        $.validator.addMethod("cRequired", $.validator.methods.required, "Cantidad de entrada es requerido.");
        $.validator.addMethod("cDecimal", $.validator.methods.decimal, "Ingrese un dato válido para cantidad de entrada.");
        $.validator.addClassRules("decimalVal", { cDecimal: true, cRequired: true, cMaxValue: true, cMinValue : 0 });

        $('#form-ed').validate({
            errorElement: "span", // contain the error msg in a span tag
            errorClass: 'help-block color',
            //errorLabelContainer: $("#validation_summary"),
            errorPlacement: function (error, element) { // render error placement for each input type
                if (element.attr("type") == "radio" || element.attr("type") == "checkbox") { // for chosen elements, need to insert the error after the chosen container
                    error.insertAfter($(element).closest('.form-group').children('div').children().last());
                } else if (element.attr("name") == "dd" || element.attr("name") == "mm" || element.attr("name") == "yyyy") {
                    error.insertAfter($(element).closest('.form-group').children('div'));
                } else if (element.attr("type") == "text") {
                    error.insertAfter($(element).closest('.form-group').children('div'));
                } else {
                    error.insertAfter(element);
                    // for other inputs, just perform default behavior
                }
            },
            ignore: "",
            invalidHandler: function (event, validator) { //display error alert on form submit
                successHandler1.hide();
                errorHandler1.show();
                //$("#validation_summary").text(validator.showErrors());
            },
            highlight: function (element) {
                $(element).closest('.help-block').removeClass('valid');
                // display OK icon
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error').find('.form-control-feedback').removeClass('glyphicon-ok').addClass('glyphicon-remove');
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
                $(element).closest('.form-group').removeClass('has-error').addClass('has-success').find('.form-control-feedback').removeClass('glyphicon-remove').addClass('glyphicon-ok');
            },
            submitHandler: function (form) {
                successHandler1.show();
                errorHandler1.hide();
                form.submit();
                //this.submit();
            }
        });
    };

    var runElements = function () {

        $('input.cantidad').on('change', function () {
            var value = $(this).val();
            if(!$.isNumeric(value))
            {
                $(this).val('');
            }
        });
        //$('input.cantidad').mask('##0.000', { reverse: true });

        //$("input.cantidad").mask("##0.000", { reverse:true, clearIfNotMatch: true });

        //$.mask.definitions['~'] = '[+-]';
        //$("input.cantidad").mask("~9.99 ~9.99 999");

    };

    return {
        //main function to initiate template pages
        init: function () {
            runValidator();
            runElements();
        }
    };
}();