var NominaEmpleado = function () {
    "use strict";
    // Funcion para validar registrar
    var runValidator1 = function () {
        var form1 = $('#form-search');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        $('#form-search').validate({
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
                FechaInicio: { required: true },
                FechaFin: { required: true }

            },
            messages: {

                IDSucursal: { required: "Seleccione una sucursal." },
                FechaInicio: { required: "Seleccione una fecha inicio periodo." },
                FechaFin: { required: "Seleccione una fecha fin periodo." }
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
    getDatostablaEmpleado($("#IDSucursal").val());
        $('#IDSucursal').on('change', function (event) {
            $("#IDTabla  tr").remove();
            getDatostablaEmpleado($("#IDSucursal").val());
        });
         
        // $("#IDPuesto").trigger('change');
        //$("#IDPuesto").change(function () {
        //    $("#IDCategoriaPuesto option").remove();
        //    getDatosRegimen($("#IDPuesto").val());
        //});
        function getDatostablaEmpleado(IDSucursal) {
            $.ajax({
                url: "/Admin/Nomina/DatostablaEmpleado",
                data: { IDS: IDSucursal },
                async: false,
                dataType: "json",
                type: "POST",
                error: function () {
                    Mensaje("Ocurrió un error al cargar la tabla", "2");
                },
                success: function (result) {
                    var Numero = 0;
                    for (var i = 0; i < result.length; i++) {
                        Numero = i;
                        $("#IDTabla").append('<tr><td><input checked="checked" id="ListaEmpleados_' + Numero + '.AbrirCaja" name="ListaEmpleados[' + Numero + '].AbrirCaja" type="checkbox" value="true" /><input name="ListaEmpleados[' + Numero + '].AbrirCaja" type="hidden" value="false" /> </td>' +
                            ' <td>' + result[i].CodigoUsuario + '</td>' +
                            '<td>' + result[i].NombreEmpleado + '</td>' +
                            '<td>' + result[i].Puesto + '</td>' +
                            ' <td>' + result[i].CategoriaPuesto + '</td>' +
                            '<td>' + result[i].Sueldo + '</td>' +
                            '<td>' + result[i].Percepciones + '</td>' +
                            '<td>' + result[i].Deducciones + '</td>' +
                            '<td style="display:none"><input id="ListaEmpleados_' + Numero + '.IDEmpleado" name="ListaEmpleados[' + Numero + '].IDEmpleado" type="text" value="' + result[i].IDEmpleado + '" /></td></tr>');
                    }
                    //$('#IDCategoriaPuesto.select').selectpicker('refresh');
                }
            });
        }
    };

    var runDatePicker = function () {
        $('#FechaInicio').datepicker({
            format: 'dd/mm/yyyy',
            //startDate: '-0d',
            autoclose: true
        });
        $('#FechaFin').datepicker({
            format: 'dd/mm/yyyy',
            //startDate: '0d',
            autoclose: true
        });
        //$('#FechaFin').datepicker('setDaysOfWeekDisabled', [1,6]);
        $('#FechaFin').datepicker().datepicker('setDate', '+2d');
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