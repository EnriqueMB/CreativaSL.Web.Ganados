var DocXCobrar = function () {
    "use strict";
    // Funcion para validar registrar
    var runValidator1 = function () {
        var form1 = $('#form-DocumentoCobrar');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        $('#form-DocumentoCobrar').validate({
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

                Id_sucursal: { required: true },
                Fecha: { required: true },
                Id_tipoDocumento: { CMBINT: true },
                 IDTProveedor: { CMBINT: true },
                IDProveedor:{required:true},
                Id_metodoPago: { required: true },
                Total: {
                    required: true,
                    min: 1
                }

            },
            messages: {


                Id_sucursal: {
                    required: "*Seleccione la sucursal"

                },
                Fecha: { required: "*Seleccione una fecha" },
                Id_tipoDocumento: { CMBINT: "*Seleccione un tipo de documento" },
                IDTProveedor: { CMBINT: "*Seleccione un tipo de proveedor" },
                IDProveedor: { required: "*Seleccione un proveedor" },
                Id_metodoPago: { required: "*Seleccione un tipo de proveedor" },
                Total: { required: "*Ingrese una cantidad", min: "*Ingrese numeros mayores a 1" }



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
        $('#Fecha').datepicker({
            format: 'yyyy/mm/dd'
        });
    };    
    var runCombos = function () {

        $('#IDTProveedor').on('change', function (event) {
            $("#IDProveedor option").remove();
            getDatosRegimen($("#IDTProveedor").val());
        });
        // $("#IDPuesto").trigger('change');
        //$("#IDPuesto").change(function () {
        //    $("#IDCategoriaPuesto option").remove();
        //    getDatosRegimen($("#IDPuesto").val());
        //});
        function getDatosRegimen(IDPuesto) {
            $.ajax({
                url: "/Admin/DocumentoXPagar/ObtenerProveedoresXID",
                data: { IDP: IDPuesto },
                async: false,
                dataType: "json",
                type: "POST",
                error: function () {
                    Mensaje("Ocurrió un error al cargar el combo", "1");
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
    return {
        //main function to initiate template pages
        init: function () {
            runValidator1();
            runCombos();
        }
    };
}();