var Chofer = function () {
    "use strict";
    // Funcion para validar registrar
    var runValidator1 = function () {
        var form1 = $('#form-chofer');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);
        
        $('#form-chofer').validate({
            errorElement: "span", // contain the error msg in a span tag
            errorClass: 'help-block color',
            errorLabelContainer: $("#validation_summary"),
            errorPlacement: function (error, element) { // render error placement for each input type
                if (element.attr("type") == "radio" || element.attr("type") == "checkbox") { // for chosen elements, need to insert the error after the chosen container
                    error.insertAfter($(element).closest('.form-group').children('div').children().last());
                } else if (element.attr("name") == "dd" || element.attr("name") == "mm" || element.attr("name") == "yyyy") {
                    error.insertAfter($(element).closest('.form-group').children('div'));
                } else if (element.attr("type") == "text" ) {
                    error.insertAfter($(element).closest('.input-group').children('div'));
                } else {
                    error.insertAfter(element);
                    // for other inputs, just perform default behavior
                }
            },
            ignore: "",
            rules: {
               
                //ListSucursal: { required: true },
                Nombre: { required: true, texto: true, maxlength: 80 },
                ApPaterno: { required: true, texto: true, maxlength: 70 },
                Ife: { required: true, ife: true, maxlength: 13 },
                NumSeguroSocial: { texto: true, maxlength: 30 },
                IDGenero: { CMBINT: true },
                idgruposanguineo: { CMBINT: true },
                FechaNacimiento: { required: true },
                FechaIngreso: { required: true },
                AvisoAccidente: { required: true },
                TelefonoAccidente: { required: true, telefono :true},

            },
            messages: {
                
                //ListSucursal: { required: "Seleccione una sucursal." },
                Nombre: { required: "Ingrese nombre del chofer.", placa: "Ingrese un formato valido (letras, números y guión(-)", maxlength: "El campo nombre admite máximo 80 caracteres." },
                ApPaterno: { required: "Ingrese apellido paterno del chofer.", texto: "Ingrese un nombre valido.", maxlength: "El campo nombre admite máximo 70 caracteres." },
                Ife: { required: "Ingrese el codigo de credencial de elector.", ife: "Ingrese un nombre valido.", maxlength: "El campo nombre admite máximo 13 caracteres." },
                NumSeguroSocial: { required: "Ingrese el número de seguro social.", texto: "Ingrese un nombre valido.", maxlength: "El campo nombre admite máximo 30 caracteres." },
                IDGenero: { CMBINT: "Seleccione un género." },
                idgruposanguineo: { CMBINT: "Seleccione un grupo sanguineo." },
                FechaNacimiento: { required: "Seleccione una fecha." },
                FechaIngreso: { required: "Seleccione una fecha." },
                AvisoAccidente: { required: "Ingrese a quien avisar en caso de accidente." },
                TelefonoAccidente: { required: "Ingrese teléfono en caso de accidente.", telefono:"Ingrese un número valido"},
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
        $('#vigencia').datepicker({
            format: 'dd/mm/yyyy'
        });
        $('#FechaNacimiento').datepicker({
            format: 'dd/mm/yyyy'
         });
         $('#FechaIngreso').datepicker({
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