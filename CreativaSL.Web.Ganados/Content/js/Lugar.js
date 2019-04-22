var Lugar = function () {
    "use strict";
    // Funcion para validar registrar
    var runValidator1 = function () {
        var form1 = $('#form-lugar');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        $('#form-lugar').validate({
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

                //ListSucursal: { required: true },
                nombrePropietario: { required: true, texto: true, maxlength: 120 },
               // apellidoPaterno: { required: true, texto: true, maxlength: 80 },
                //apellidoMaterno: { texto: true, maxlength: 80 },
                descripcion: { required: true, texto: true, maxlength: 100 },
                //ejido: { texto: true, maxlength: 100 },
                //observaciones: { required: true, texto: true },
                id_municipio: { CMBINT: true },
                id_estadoCodigo: { required: true },
                id_pais: { required: true },
                id_sucursal: { required: true },
                Direccion: {maxlength: 300}

            },
            messages: {

                //ListSucursal: { required: "Seleccione una sucursal." },
                nombrePropietario: { required: "Ingrese nombre del Propietario.",texto:"Ingrese un nombre valido", maxlength: "El campo nombre admite máximo 120 caracteres." },
            //apellidoPaterno: { required: "Ingrese apellido paterno del propietario.", texto: "Ingrese un nombre valido", maxlength: "El campo  admite máximo 80 caracteres." },
            //apellidoMaterno: { texto: "Ingrese un nombre valido", maxlength: "El campo  admite máximo 80 caracteres." },
            descripcion: { required: "Ingrese nombre del lugar.", texto: "Ingrese un nombre valido", maxlength: "El campo  admite máximo 100 caracteres." },
            //ejido: { texto: "Ingrese un nombre de ejido valido", maxlength: "El campo  admite máximo 100 caracteres." },
            //observaciones: { required: "Ingrese las observaciones del lugar.", texto: "Ingrese las observaciones del lugar" },
            id_municipio: { CMBINT: "Seleccione un municipio." },
            id_estadoCodigo: { required: "Ingrese un estado válido." },
            id_pais: { required: "Ingrese un pais válido." },
            id_sucursal: { required: "Ingrese una sucursal válida." },
            Direccion: { maxlength: jQuery.validator.format("Dirección tiene un máximo de caracteres: {0}") }
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