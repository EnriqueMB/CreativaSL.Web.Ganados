﻿var Servicios = function () {
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
                IDSucursal: { required: true },
                IDProveedor: { required: true },
                Fecha: { required: true },
                FechaProxima: { required: true }
            },
            messages: {
                IDSucursal: { required: "Seleccione una sucursal." },
                IDProveedor: { required: "Seleccione un proveedor." },
                Fecha: { required: "Ingrese la fecha del servicio." },
                FechaProxima: { required: "Ingrese la fecha próxima del servicio." }
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


    var runCombos = function () {

        $('#IDSucursal').on('change', function (event) {
            $("#IDProveedor option").remove();
            var IdSucursal = $(this).val();
            getCatProveedores(IdSucursal);
            //$(this).trigger("focusout");
        });
        
        function getCatProveedores(IdSucursal) {
            $.ajax({
                url: "/Admin/Mantenimiento/ObtenerComboProveedores",
                data: { IDSucursal: IdSucursal },
                async: false,
                dataType: "json",
                type: "POST",
                error: function () {
                    Mensaje("Ocurrió un error al cargar el combo", "2");
                },
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        $("#IDProveedor").append('<option value="' + result[i].IDProveedor + '">' + result[i].NombreRazonSocial + '</option>');
                    }
                    $('#IDProveedor.select').selectpicker('refresh');
                }
            });
        }

    };


    var runDatePicker = function () {
        $('#Fecha').datepicker({
            format: 'dd/mm/yyyy'
        });
        $('#FechaProxima').datepicker({
            format: 'dd/mm/yyyy'
        });
    };
    return {
        //main function to initiate template pages
        init: function () {
            runValidator1();
            runCombos();
            runDatePicker();
        }
    };
}();