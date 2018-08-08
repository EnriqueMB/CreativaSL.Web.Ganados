var Vehiculo = function () {
    "use strict";
    // Funcion para validar registrar
    var runValidator1 = function () {
        var form1 = $('#form-vehiculo');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        $('#form-vehiculo').validate({
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
                IDEmpresa: { required: true },
                IDTipoVehiculo: { CMBINT: true },
                IDMarca: { CMBINT: true },
                Placas: { required: true, texto: true, maxlength: 10 },
                Modelo: { required: true, texto: true, maxlength: 30 },
                tarjetaCirculacion: { required: true, texto: true, maxlength: 30 },
                fechaIngreso: { required: true },
                Capacidad: { required: true, texto: true, maxlength: 30 },
                Color: { required: true, texto: true, maxlength: 30 },
                NoSerie: { required: true, texto: true, maxlength: 30 },
                Gps: { maxlength: 10},

            },
            messages: {
                
                IDEmpresa: { required: "Seleccione una empresa." },
                IDTipoVehiculo: { CMBINT: "Seleccione un tipo de vehículo." },
                IDMarca: { CMBINT: "Seleccione una marca de vehículo." },
                Placas: { required: "Ingrese la placa del vehículo.", placa: "Ingrese un formato valido (letras, números y guión(-)", maxlength: "El campo nombre admite máximo 10 caracteres." },
                Modelo: { required: "Ingrese el modelo del vehículo.", texto: "Ingrese un nombre valido.", maxlength: "El campo nombre admite máximo 30 caracteres." },
                tarjetaCirculacion: { required: "Ingrese la tarjeta de circulación.", tarjetaCirculacion: "Ingrese un nombre valido.", maxlength: "El campo nombre admite máximo 30 caracteres." },
                fechaIngreso: { required: " " },
                Capacidad: { required: "Ingrese la capacidad del vehículo.", texto: "Ingrese un nombre valido.", maxlength: "El campo nombre admite máximo 30 caracteres." },
                Color: { required: "Ingrese el color del vehiculo.", texto: "Ingrese un nombre valido.", maxlength: "El campo nombre admite máximo 30 caracteres." },
                NoSerie: { required: "Ingrese el numero de serie del vehículo.", texto: "Ingrese un nombre valido.", maxlength: "El campo nombre admite máximo 30 caracteres." },
                Gps: { maxlength: "El campo GPS admite máximo 10 caracteres." },
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
        $('#fechaIngreso').datepicker({
            format: 'dd/mm/yyyy'
        });
    };
    var runEvents = function () {
        $("#IDEmpresa").on("change", function () {
            var IDEmpresa = $("#IDEmpresa").val();
            GetSucursalesXIDEmpresa(IDEmpresa);
        });
    }
    function GetSucursalesXIDEmpresa(IDEmpresa) {
        $.ajax({
            url: "/Admin/CatVehiculo/ObtenerSucursalesXIDEmpresa/",
            data: { IDEmpresa: IDEmpresa },
            async: false,
            dataType: "json",
            type: "POST",
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {
                $("#IDSucursal option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#IDSucursal").append('<option value="' + result[i].IDSucursal + '">' + result[i].NombreSucursal + '</option>');
                }
                $('#IDSucursal.select').selectpicker('refresh');
            }
        });
    }

    return {
        //main function to initiate template pages
        init: function () {
            runValidator1();
            runDatePicker();
            runEvents();
        }
    };
}();