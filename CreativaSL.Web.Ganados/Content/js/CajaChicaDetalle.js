var CajaChica = function () {
    "use strict";
    // Funcion para validar registrar
    var runValidator1 = function () {
        var form1 = $('#form-dg');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);
        $.validator.addMethod("validarImagen", function () {
            if (document.getElementById("FotoCheque").value === '') {
                if ((document.getElementById("FotoCheque").value === ''))
                    return false;
                else
                    return true;
            }
            else
                return true;
        }, 'Debe seleccionar una imagen.');

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
                }
            },
            ignore: "",
            rules: {
                IdConcepto: { required: true, CMBINT: true },
                Concepto: { required: true },
                Salida: { required: true, decimal: true },
                Recibe: { required: true },
                IdFormaPago: { required: true, CMBINT: true },
                FolioCheque: { required: true }
                //FotoCheque: { validarImagen: true }
            },
            messages: {
                IdConcepto: { required: "Seleccione una categoría.", CMBINT: "Seleccione una categoría." },
                Concepto: { required: "Ingrese el concepto." },
                Salida: { required: "Ingrese el monto.", decimal: "Ingrese un dato numérico válido." },
                Recibe: { required: "Ingrese el nombre de quien recibe el dinero." },
                IdFormaPago: { required: "Seleccione una forma de pago.", CMBINT: "Seleccione una forma de pago." },
                FolioCheque: { required: "Ingrese un folio" }
                //FotoCheque: { validarImagen: "Seleccione una imagen de comprobante ó cheque" }
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