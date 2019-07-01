var Prestamos = function () {
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
                FechaPrestamo: { required: true },
                Comentario: { texto: true }
            },
            messages: {
                IDSucursal: { required: "Seleccione un sucursal." },
                IDAlmacen: { required: "Seleccione una bodega." },
                FechaSalida: { required: "Ingrese una fecha." },
                Comentario: { texto: "Ingrese un texto válido para comentarios adicionales." }
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
            $("#IDEmpleado option").remove();
            var IdSucursal = $(this).val();
            getCatAlmacen(IdSucursal);
            getCatEmpleados(IdSucursal);
        });

        function getCatAlmacen(IdSucursal) {
            $.ajax({
                url: "/Admin/Prestamos/ObtenerAlmacenesXIDSucursal",
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
        function getCatEmpleados(IdSucursal) {
            $.ajax({
                url: "/Admin/Prestamos/ObtenerEmpleadosXIDSucursal",
                data: { IDSucursal: IdSucursal },
                async: false,
                dataType: "json",
                type: "POST",
                error: function () {
                    Mensaje("Ocurrió un error al cargar el combo", "2");
                },
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        $("#IDEmpleado").append('<option value="' + result[i].IDEmpleado + '">' + result[i].NombreCompleto + '</option>');
                    }
                    $('#IDEmpleado.select').selectpicker('refresh');
                }
            });
        }
    };

    //var uiDatatable = function () {
    //    if ($(".datatable2").length > 0) {
    //        $(".datatable2").dataTable({
    //            "order": [],
    //            "language": {
    //                "url": "/Content/assets/json/Spanish.json"
    //            },
    //            responsive: true
    //        });
    //        $(".datatable2").on('page.dt', function () {
    //            onresize(100);
    //        });
    //    }
    //};//END Datatable

    return {
        //main function to initiate template pages
        init: function () {
            runValidator();
            runElements();
            runCombos();
            //uiDatatable();
        }
    };
}();