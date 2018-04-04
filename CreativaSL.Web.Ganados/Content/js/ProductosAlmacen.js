var ProductosAlmacen = function () {
   
    "use strict";
    // Funcion para validar registrar
    var runValidator1 = function () {
        var form1 = $('#form-pa');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);
        $.validator.addMethod("validarImagen2", function () {
            if (document.getElementById("imagen2").value === '') {
                if ((document.getElementById("imagen2").value === ''))
                    return false;
                else
                    return true;
            }
            else
                return true;
        }, 'Debe seleccionar una imagen.');
    
        $('#form-pa').validate({
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
                
            IDTipoCodigo: {CMBINT: true }, //{ nombre: true, maxlenght: 300 },
                IDUnidadMedida: { CMBINT: true },
                Clave: { required: true, texto: true, maxlength: 20 },
                Nombre: { required: true, nombre: true, maxlength: 300 },
                Descripcion: { required: true, descripcion: true },
                UltimoCosto: { required: true },
                imagen2: { validarImagen2: true },
               
            },
            messages: {
                
                IDTipoCodigo: {  CMBINT: "Seleccione un tipo de código." }, // { nombre: "Ingrese un nombre de contacto válido." , maxlenght:   }
                IDUnidadMedida: { CMBINT: "Seleccione una unidad de médida." },
                Clave: { required: "Se necesita de una clave del producto", texto: "Ingrese un formato válido ", maxlength: "Solo se admite un máximo de 20 caracteres" },
                Nombre: { required: "Se necesita de un nombre del producto", nombre: "Ingrese un formato válido ", maxlength: "Solo se admite un máximo de 300 caracteres" },
                Descripcion:{ required: "Se necesita de una clave del producto", descripcion: "Ingrese un formato válido "},
                UltimoCosto: { required: "Se necesita de un nombre del producto" },
                imagen2: { validarImagen2: "Seleccione una imagén válida del ine" },
               

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