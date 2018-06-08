var Conciliacion = function () {
    "use strict";
    // Funcion para validar registrar
    var runValidator = function () {
        var form1 = $('#form-ea');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);
        $('#form-ea').validate({
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
                IDAlmacen: { required: true },
                IDTipoConciliacion: { CMBINT: true },
                FechaConciliacion: { required: true },
                Comentarios: { texto: true }
            },
            messages: {
                IDSucursal: { required: "Seleccione un sucursal." },
                IDAlmacen: { required: "Seleccione una bodega." },
                IDTipoConciliacion: { CMBINT : "Seleccione un tipo de conciliación."},
                FechaConciliacion: { required: "Ingrese una fecha." },
                Comentarios: { texto: "Ingrese un texto válido para comentarios adicionales." }
            },
            invalidHandler: function (event, validator) { //display error alert on form submit
                successHandler1.hide();
                errorHandler1.show();
                //$("#validation_summary").text(validator.showErrors());
            },
            highlight: function (element) {
                $(element).closest('.help-block').removeClass('valid');
                // display OK icon
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error').find('.form-control-feedback').removeClass('glyphicon-ok').addClass('glyphicon-remove');
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
                $(element).closest('.form-group').removeClass('has-error').addClass('has-success').find('.form-control-feedback').removeClass('glyphicon-remove').addClass('glyphicon-ok');
            },
            submitHandler: function (form) {
                successHandler1.show();
                errorHandler1.hide();
                form.submit();
                //this.submit();
            }
        });
    };

    var runElements = function () {
        $('.datepicker').datepicker({
            format: 'dd/mm/yyyy'
        });
    };

    var runCombos = function () {

        $('#IDSucursal').on('change', function (event) {
            $("#IDAlmacen option").remove();
            var IdSucursal = $(this).val();
            getCatAlmacen(IdSucursal);
        });

        function getCatAlmacen(IdSucursal) {
            $.ajax({
                url: "/Admin/ConciliacionAlmacen/ObtenerAlmacenesXIDSucursal",
                data: { IDSucursal: IdSucursal },
                async: false,
                dataType: "json",
                type: "POST",
                error: function () {
                    Mensaje("Ocurrió un error al cargar el combo", "2");
                },
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        $("#IDAlmacen").append('<option value="' + result[i].IDAlmacen + '">' + result[i].Descripcion + '</option>');
                    }
                    $('#IDAlmacen.select').selectpicker('refresh');
                }
            });
        }
    };

    return {
        //main function to initiate template pages
        init: function () {
            runValidator();
            runElements();
            runCombos();
        }
    };
}();