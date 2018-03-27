var Empleado = function () {
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
                Nombre: { required: true, texto: true, maxlength: 70 },
                ApellidoPat: { required: true, texto: true, maxlength: 50 },
                IDSucursalActual: { required: true },
                IDGrupoSanguineo: { CMBINT: true },
                IDPuesto: { CMBINT: true },
                IDCategoriaPuesto: { required: true},
                Telefono: { telefono: true },
                DirCalle: { direccion: true, maxlength: 90 },
                DirColonia: { direccion: true, maxlength: 90 }
            },
            messages: {
                Nombre: { required: "Ingrese el nombre del empleado.", texto: "Ingrese un nombre valido.", maxlength: "El campo nombre admite máximo 70 caracteres." },
                ApellidoPat: { required: "Ingrese el apellido paterno.", texto: "Ingrese un apellido paterno válido.", maxlength: "El campo apellido paterno admite máximo 50 caracteres." },
                IDSucursalActual: { required: "Seleccione una sucursal." },
                IDGrupoSanguineo: { CMBINT: "Seleccione un grupo sanguineo." },
                IDPuesto: { CMBINT: "Seleccione un puesto." },
                IDCategoriaPuesto: { required: "Seleccione una categoria del puesto"},
                Telefono: { telefono: "Ingrese un número de teléfono válido." },
                DirCalle: { direccion: "Ingrese una calle valida", maxlength: "El campo calle admite máximo 90 caracteres." },
                DirColonia: { direccion: "Ingrese una colonia valida", maxlength: "El campo colonia admite máximo 50 caracteres." }
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
        $("#IDPuesto").change(function () {
            $("#IDCategoriaPuesto option").remove();
            getDatosRegimen($("#IDPuesto").val());
        });
        function getDatosRegimen(IDPuesto) {

            $.ajax({
                url: "/Admin/CatEmpleado/ObtenerComboCategoriaPuesto",
                data: { IDP: IDPuesto },
                async: false,
                dataType: "json",
                type: "POST",
                error: function () {
                    Mensaje("Ocurrió un error al cargar el combo", "1");
                },
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        $("#IDCategoriaPuesto").append('<option value="' + result[i].id_categoria + '">' + result[i].descripcion + '</option>');
                    }
                    $('#IDCategoriaPuesto.select').selectpicker('refresh');
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