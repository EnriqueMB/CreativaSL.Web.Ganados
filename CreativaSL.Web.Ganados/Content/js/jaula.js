var Jaula = function () {
    "use strict";
    // Funcion para validar registrar
    var runValidator1 = function () {
        var form1 = $('#frmJaula');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);
        
        $('#frmJaula').validate({
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
                ListaEmpresas: { required: true },
                Matricula: { required: true, maxlength: 15 },
            },
            messages: {
                ListaEmpresas: { required: "Seleccione una empresa" },
                Matricula: { required: "Ingrese la matrícula de la jaula.", maxlength: "El campo nombre admite máximo 15 caracteres." }
            },
            invalidHandler: function (event, validator) { //display error alert on form submit
                successHandler1.hide();
                errorHandler1.show();
            },
            highlight: function (element) {
                $(element).closest('.help-block').removeClass('valid');
                // display OK icon
                $(element).closest('.classError').removeClass('has-success').addClass('has-error').find('.symbol').removeClass('ok').addClass('required');
                // add the Bootstrap error class to the control group
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.classError').removeClass('has-error');
                // set error class to the control group
            },
            success: function (label, element) {
                label.addClass('help-block valid');
                label.removeClass('color');
                // mark the current input as valid and display OK icon
                $(element).closest('.classError').removeClass('has-error').addClass('has-success').find('.symbol').removeClass('required').addClass('ok');
            },
            submitHandler: function (form) {
                successHandler1.show();
                errorHandler1.hide();
                form.submit();
            }
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
            url: "/Admin/CatJaula/ObtenerSucursalesXIDEmpresa/",
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
            runEvents();
        }
    };
}();