var Clientes = function () {
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
                } else if (element.attr("type") == "text" ) {
                    error.insertAfter($(element).closest('.input-group').children('div'));
                } else {
                    error.insertAfter(element);
                    // for other inputs, just perform default behavior
                }
            },
            ignore: "",
            rules: {
                IDSucursal: { required: true },
                NombreRazonSocial: { required: true, texto: true, maxlength: 300 },
                IDRegimenFiscal: { required: true },
                RFC: { required: true, rfc: true },
                Direccion: { direccion: true, maxlength: 300 },
                FechaIngreso: { required: true },
                NombreResponsable: { required: true, nombre: true, maxlength: 300, minlength: 4}, //{ nombre: true, maxlenght: 300 },
                Celular: { telefono: true },
                Telefono: { telefono: true },
                TipoCliente: { min: 1 }
                //CorreoElectronico: { required: true, email: true },              
                //PSGCliente: {required: true}
            },
            messages: {
                IDSucursal: { required: "Seleccione una sucursal." },
                NombreRazonSocial: { required: "Ingrese el nombre o Razón social.", texto: "Ingrese un nombre o razón social válido.", maxlength: "El campo nombre o razón social admite máximo 300 caracteres." },
                IDRegimenFiscal: { required: "Seleccione un régimen fiscal." },
                RFC: { required: "Ingrese el RFC del cliente.", rfc: "Ingrese un RFC válido." },
                Direccion: { direccion: "Ingrese un dirección válida.", maxlength: "El campo domicilio fiscal admite máximo 300 caracteres." },
                FechaIngreso: { required: "Ingrese la fecha de inicio de relación." },
                NombreResponsable: { required: "Ingrese el Nombre del Contacto", nombre: "Ingrese un nombre de contacto válido.", maxlength: "El campo nombre de contacto admite máximo 300 caracteres.", minlength: "El campo nombre de contacto admite minimo 4 caracteres."  }, // { nombre: "Ingrese un nombre de contacto válido." , maxlenght:   }
                Celular: { telefono: "Ingrese un número de celular válido." },
                Telefono: { telefono: "Ingrese un número de teléfono válido." },
                TipoCliente: { min: "Ingrese un tipo de cliente." }
                //CorreoElectronico: { required: "Ingrese el correo electrónico del cliente.", email: "Ingrese un correo electrónico válido." },
                //PSGCliente: { required: "Ingrese el P.S.G. del cliente." }
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

    var runDatePicker = function ()
    {
        $('#FechaIngreso').datepicker({
            format: 'dd/mm/yyyy'
        });
    };

    var runCombos = function () {

        $('input').on('ifChanged', function (event) {
            $("#IDRegimenFiscal option").remove();
            var esPersonaFisica = $(this).prop('checked');
            getDatosRegimen(esPersonaFisica);            
        });
        function getDatosRegimen(esPersonaFisica) {
            $.ajax({
                url: "/Admin/CatCliente/ObtenerRegimenFiscalXBoolEsPersonaFisica",
                data: { band: esPersonaFisica },
                async: false,
                dataType: "json",
                type: "POST",
                error: function () {
                    Mensaje("Ocurrió un error al cargar el combo", "1");
                },
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        $("#IDRegimenFiscal").append('<option value="' + result[i].Clave + '">' + result[i].Descripcion + '</option>');
                    }
                    $('#IDRegimenFiscal.select').selectpicker('refresh');
                }
            });

            
        }
    };

    return {
        //main function to initiate template pages
        init: function () {
            runValidator1();
            runDatePicker();
            runCombos();
        }
    };
}();