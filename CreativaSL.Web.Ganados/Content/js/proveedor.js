var Proveedor = function () {
    "use strict";
    // Funcion para validar registrar
    var runValidator1 = function () {
        var form1 = $('#form-dg');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);
        
        $('#form-dg').validate({
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
                    // for other inputs, just perform default behavior
                }
            },
            ignore: "",
            rules: {
                IDSucursal: { required: true },
                IDTipoProveedor: { CMBINT: true },
                NombreRazonSocial: { required: true, texto: true, maxlength: 300 },
                ImgINE:
                {
                    ImagenRequerida: ["ImgINE"]
                }
                , ImgManifestacionFierro:
                {
                    ImagenRequerida: ["ImgManifestacionFierro"]
                }
                , FotoPerfil:
                {
                    ImagenNoRequerida_ValidarExtension: ["FotoPerfil"]
                }
            },
            messages: {
                IDSucursal:
                {
                     required: "Seleccione una sucursal."
                }
                , IDTipoProveedor:
                {
                     CMBINT: "Seleccione un tipo proveedor válido."
                }
                , NombreRazonSocial:
                {
                    required: "Ingrese el nombre o Razón social.",
                    texto: "Ingrese un nombre o razón social válido.",
                    maxlength: "El campo nombre o razón social admite máximo 300 caracteres."
                }
                , ImgINE:
                {
                    ImagenRequerida:
                        "Seleccione una imagén válida del ine, formatos válidos: PNG, JPG, JPEG, HEIC y BMP."
                }
                , ImgManifestacionFierro:
                {
                    ImagenRequerida:
                        "Seleccione una imagén válida de manifestación de fierro, formatos válidos: PNG, JPG, JPEG, HEIC y BMP."
                }
                , FotoPerfil:
                {
                    ImagenNoRequerida_ValidarExtension:
                    "Seleccione una imagen válida para la foto de perfil, formatos válidos: PNG, JPG, JPEG, HEIC y BMP."
                }
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
            }
        });
    };

    var runDatePicker = function ()
    {
        $('#FechaIngreso').datepicker({
            format: 'dd/mm/yyyy'
        });
    };

    var runImagenes = function ()
    {
        
    };

    var runCombos = function () {
        var isChecked = $("#EsEmpresa").prop("checked");
        if (isChecked == true) {
            $('#Genero').css('display', 'block')
        }
        else {
            $('#Genero').css('display', 'none')
        }
        $('input').on('ifChanged', function (event) {
            console.log("Entre")
            var esPersonaFisica = $(this).prop('checked');
            if (esPersonaFisica == true) {
                $('#Genero').css('display', 'block')
            }
            else {
                $('#Genero').css('display', 'none')
            }
        });
    };

    return {
        //main function to initiate template pages
        init: function () {
            runValidator1();
            runDatePicker();
            runImagenes();
            runCombos();
        }
    };
}();