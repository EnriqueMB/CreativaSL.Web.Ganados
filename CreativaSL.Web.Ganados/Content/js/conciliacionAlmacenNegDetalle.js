var ConciliacionDetalle = function () {
    "use strict";
    // Funcion para validar registrar
    var runValidator = function () {
        var form1 = $('#form-ea');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        $.validator.addMethod("cMenorQExistencia", function (value, element) {
            value = Number($.isNumeric(value) ? value : 0);
            var MaxValue = Number($('#Existencia').val());
            return this.optional(element) || (value > 0 && value <= MaxValue);
        }, "Verifique la cantidad. Debe ser menor o igual a la existencia, y mayor a 0.");

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
                IDProductoAlmacen: { required: true },
                IDUnidadProducto: { required: true },
                Cantidad: { required: true, decimal: true, cMenorQExistencia: true }
            },
            messages: {
                IDProductoAlmacen: { required: "Seleccione un producto." },
                IDUnidadProducto: { required: "Seleccione una unidad de medida." },
                Cantidad: { required: "Ingrese la cantidad.", decimal: "Ingrese un dato válido para cantidad." }
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

    var runCombos = function () {

        $('#IDProductoAlmacen').on('change', function (event) {
            $("#IDUnidadProducto option").remove();
            $("#Existencia").val('0.000');
            var IdProducto = $(this).val();
            getUnidadesXIDProducto(IdProducto);
        });

        $('#IDUnidadProducto').on('change', function (event) {
            var IdProducto = $('#IDProductoAlmacen').val();
            var IdUnidad = $(this).val();
            var IdConciliacion = $('#IDConciliacion').val();
            getExistenciaXIDProducto(IdProducto, IdUnidad, IdConciliacion);
        });

        function getUnidadesXIDProducto(IdProducto) {
            $.ajax({
                url: "/Admin/ConciliacionAlmacen/ObtenerUnidadesXIDProducto",
                data: { IDProducto: IdProducto },
                async: false,
                dataType: "json",
                type: "POST",
                error: function () {
                    Mensaje("Ocurrió un error al cargar el combo", "2");
                },
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        $("#IDUnidadProducto").append('<option value="' + result[i].id_unidadProducto + '">' + result[i].Descripcion + '</option>');
                    }
                    $('#IDUnidadProducto.select').selectpicker('refresh');
                }
            });
        }

        function getExistenciaXIDProducto(IdProducto, IdUnidad, IdConciliacion) {
            $.ajax({
                url: "/Admin/ConciliacionAlmacen/ObtenerExistenciaXIDProducto",
                data: { IDProducto: IdProducto, IDConciliacion: IdConciliacion, IDUnidad: IdUnidad },
                async: false,
                dataType: "json",
                type: "POST",
                error: function () {
                    Mensaje("Ocurrió un error al cargar el combo", "2");
                },
                success: function (result) {
                    console.log(result);
                    $("#Existencia").val(result);
                }
            });
        }
    };

    return {
        //main function to initiate template pages
        init: function () {
            runValidator();
            runCombos();
        }
    };
}();