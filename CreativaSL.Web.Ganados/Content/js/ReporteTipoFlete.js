var TipoFlete = function () {
    "use strict";
    var runValidator1 = function () {
        var form1 = $('#form-SMV');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        $('#form-SMV').validate({
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
                    // for other inputs, just perform default behavior
                }
            },
            ignore: "",
            rules: {
                id_sucursal: { required: true },
                id_tipoFlete: { required: true },
                FechaInicio: { required: true },
                FechaFin: { required: true }
            },
            messages: {
                id_sucursal: { required: "Seleccione una sucursal." },
                id_tipoFlete: { required: "Seleccione un tipo de flete." },
                FechaInicio: { required: "Ingrese la fecha inicio del reporte." },
                FechaFin: { required: "Ingrese la fecha fin del reporte." }
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
                //this.submit();
            }
        });
    };
    


    var runDatePicker = function () {
        $('#FechaInicio').datepicker({
            format: 'dd/mm/yyyy'
        });
        $('#FechaFin').datepicker({
            format: 'dd/mm/yyyy'
        });
    };
    return {
        //main function to initiate template pages
        init: function () {
            runValidator1();
            runDatePicker();
        }
    };
}();