var UPPProveedor = function () {
    "use strict";
    // Funcion para validar registrar
    var runValidator1 = function () {
        var form1 = $('#form-dg');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);
        $.validator.addMethod("validarImagen", function () {
            if (document.getElementById("Imagen").value === '') {
                if ((document.getElementById("Imagen").value === ''))
                    {return false;}
                else
                    {return true;}
            }
            else
                return true;
        }, 'Debe seleccionar una imagen.');

        $('#form-dg').validate({
            //debug:true,
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
                UPP: { required: true },
                nombrePredio: { required: true, texto: true, maxlength: 200 },
                propietario: { required: true, texto: true },
                fechaAlta: { required: true },
                // Imagen: { validarImagen: true },
                ImagenHttp: { ImagenRequerida: true, ImagenRequerida: ["Imagen"] },
                id_municipio: { CMBINT: true },
                id_estadoCodigo: { required: true },
                id_pais: { required: true },
            },
            messages: {
                UPP: { required: "Ingrese el UPP." },
                nombrePredio: { required: "Ingrese el UPP.", texto: "Ingrese un nombre valido.", maxlength: "El nombre del predio admite máximo 200 caracteres."},

                propietario: { required: "Ingrese el nombre del propietario", texto: "Ingrese un nombre valido" },
                fechaAlta: { required: "Ingrese la fecha de alta." },

                //Imagen: { validarImagen: "Seleccione una imagén válida de la UPP" },
                id_municipio: { CMBINT: "Seleccione un municipio." },
                id_estadoCodigo: { required: "Ingrese un estado válido." },
                id_pais: { required: "Ingrese un pais válido." },

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

    var rumImagenes = function () {

    };

    return {
        //main function to initiate template pages
        init: function () {
            runValidator1();
            rumImagenes();
        }
    };
}();