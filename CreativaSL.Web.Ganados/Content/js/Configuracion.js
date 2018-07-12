var Configuracion = function () {
    "use strict";
    // Funcion para validar registrar
    var runValidator1 = function () {
        var form1 = $('#form-dg');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        $('#form-dg').validate({
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
                textoTicket1: { required: true, texto: true },
                textoTicket2: { required: true },
                textoTicket3: { required: true },
                pagosDiasFestivos: { required: true,  maxlength: 20 },
                pagosDiasVacaciones: { required: true, maxlength: 20 },
                pagosDiasDomingos: { required: true, maxlength: 20 },
                retardosFaltas: { required: true, number: true }
            },
            messages: {
                textoTicket1: { required: "Ingrese un texto para ticket uno.", texto: "Ingrese un texto para ticket uno válido." },
                textoTicket2: { required: "Ingrese un texto para ticket dos.", texto: "Ingrese un texto para ticket dos válido." },
                textoTicket3: { required: "Ingrese un texto para ticket tres.", texto: "Ingrese un texto para ticket tres válido." },
                pagosDiasFestivos: { required: "Ingrese una cantidad para días festivos", maxlength: "El pago de días festivos admite máximo 20 caracteres" },
                pagosDiasVacaciones: { required: "Ingrese una cantidad para días vacaciones", maxlength: "El pago de días festivos admite máximo 20 caracteres" },
                pagosDiasDomingos: { required: "Ingrese una cantidad para días Domingo", maxlength: "El pago de días festivos admite máximo 20 caracteres" },
                retardosFaltas: { required: "Ingrese un número de retardos faltas",  number: "asdds "}
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
    return {
        //main function to initiate template pages
        init: function () {
            runValidator1();
        }
    };
}();
