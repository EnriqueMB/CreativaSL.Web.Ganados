var Transferir = function () {
    "use strict";
    var tblGanadoCorral, tblGanadoJaula;
    // Funcion para validar registrar
    var runValidator1 = function () {
        var form1 = $('#form-dg');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);
        
        $('#form-dg').validate({
            debug: true,
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
                IdSucursal: { required: true },
                IdCorral: { CMBINT: true }
            },
            messages: {
                IdSucursal: { required: "Seleccione una sucursal" },
                IdCorral: { CMBINT: "Seleccione un corral"}
            },
            invalidHandler: function (event, validator) { //display error alert on form submit
                successHandler1.hide();
                errorHandler1.show();
                //$("#validation_summary").text(validator.showErrors());
            },
            highlight: function (element) {
                $(element).closest('.help-block').removeClass('valid');
                // display OK icon
                $(element).closest('.controlError').removeClass('has-success').addClass('has-error').find('.symbol').removeClass('ok').addClass('required');
                // add the Bootstrap error class to the control group
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.controlError').removeClass('has-error');
                // set error class to the control group
            },
            success: function (label, element) {
                label.addClass('help-block valid');
                label.removeClass('color');
                // mark the current input as valid and display OK icon
                $(element).closest('.controlError').removeClass('has-error').addClass('has-success').find('.symbol').removeClass('required').addClass('ok');
            },
            submitHandler: function (form) {
                successHandler1.show();
                errorHandler1.hide();
                form.submit();
            }
        });
    };
   
    var runEvents = function () {
        $("#IdSucursal").on("change", function () {
            var IDSucursal = $("#IdSucursal").val();
            GetCorrarXIDSucursal(IDSucursal);
        });
    }
    function GetCorrarXIDSucursal(IDSucursal) {
        $.ajax({
            url: "/Admin/CatGanado/ObtenerCorralXIdSucursal/",
            data: { IDSucursal: IDSucursal },
            async: false,
            dataType: "json",
            type: "POST",
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {
                $("#IdCorral option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#IdCorral").append('<option value="' + result[i].Id_corral + '">' + result[i].Descripcion + '</option>');
                }
                $('#IdCorral.select').selectpicker('refresh');
            }
        });
    }

    var initDataTables = function () {
        tblGanadoCorral = $('#tblGanadoCorral').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    "Id_sucursal": 1
                },
                "url": "/Admin/CatGanado/DatatableGanadoActual/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "id_ganado" },
                { "data": "id_detalle" },
                { "data": "numArete" },
                { "data": "corral" },
                { "data": "genero" },
                {
                    "data": "merma",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '', ' %')
                },
                {
                    "data": "pesoInicial",
                    "render": $.fn.dataTable.render.number(',', '.', 0, '', ' KG.')
                },
                {
                    "data": "precioKilo",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$')
                },
                {
                    "data": "precioCompra",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$')
                },
                { "data": "precioCompra" }
            ],
            "columnDefs": [
                {
                    "targets": [0, 1, 5, 7, 9],
                    "visible": false,
                    "searchable": false
                }
            ]
        });
    }
    return {
        //main function to initiate template pages
        init: function () {
            runValidator1();
            runEvents();
            initDataTables();
        }
    };
}();