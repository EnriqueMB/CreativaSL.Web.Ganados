var Transferir = function () {
    "use strict";
    var tblGanadoCorral, tblGanadoTransferir;
    var IDSucursal;
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
            IDSucursal = $("#IdSucursal").val();
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
                    //"Id_sucursal": 1
                },
                "url": "/Admin/CatGanado/DatatableGanadoActual/",
                "type": "POST",
                "datatype": "json",
                "dataSrc": ''
            },
            "columns": [
                { "data": "id_ganado" },
                { "data": "numArete" },
                { "data": "corral" },
                { "data": "Sucursal" },
                { "data": "genero" },
                {
                    "data": "pesoInicial",
                    "render": $.fn.dataTable.render.number(',', '.', 0, '', ' KG.')
                }
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ]
        });

        tblGanadoTransferir = $('#tblGanadoTransferir').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "columns": [
                { "data": "id_ganado" },
                { "data": "numArete" },
                { "data": "corral" },
                { "data": "Sucursal" },
                { "data": "genero" },
                {
                    "data": "pesoInicial",
                    "render": $.fn.dataTable.render.number(',', '.', 0, '', ' KG.')
                }
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ]
        });
    }

    var initFuncionesGanado = function () {

        //Seleccionar filas 
        $('#tblGanadoCorral tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });
        $('#tblGanadoTransferir tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });
        $('#seleccionarCorral').click(function () {
            var nodos = tblGanadoCorral.rows({ page: 'all', search: 'applied' }).nodes();
            for (var i = 0; i < nodos.length; i++) {
                $(nodos[i]).toggleClass("selected");
            }
        });
        $('#seleccionarTransferir').click(function () {
            var nodos = tblGanadoTransferir.rows({ page: 'all', search: 'applied' }).nodes();
            for (var i = 0; i < nodos.length; i++) {
                $(nodos[i]).toggleClass("selected");
            }
        });

        //Pasar y regresar filas en las tablas
        $('#enviar').click(function () {
            var rows = tblGanadoCorral.rows('.selected').data();

            for (var i = 0; i < rows.length; i++) {
                var d = rows[i];

                //lo agrego a la tabla jaula para su envio
                tblGanadoTransferir.row.add({
                    "id_ganado": d.id_ganado,
                    "numArete": d.numArete,
                    "corral": d.corral,
                    "Sucursal": d.Sucursal,
                    "genero": d.genero,
                    "pesoInicial": d.pesoInicial
                }).draw();
            }
            tblGanadoCorral.rows('.selected').remove().draw(false);
        });

        $('#regresar').click(function () {
            var rows = tblGanadoTransferir.rows('.selected').data();
            for (var i = 0; i < rows.length; i++) {
                var d = rows[i];
                tblGanadoCorral.row.add({
                    "id_ganado": d.id_ganado,
                    "numArete": d.numArete,
                    "corral": d.corral,
                    "Sucursal": d.Sucursal,
                    "genero": d.genero,
                    "pesoInicial": d.pesoInicial
                }).draw();
            }
            tblGanadoTransferir.rows('.selected').remove().draw(false);
        });

        $('#guardar').on('click', function () {
            //datos de las tablas

            var error = Validar();

            if (error == 0) {
                //var tblGanado = tblGanadoJaula.rows().data();
                var tblGanado = tblGanadoTransferir.rows().data();
                console.log(tblGanado);
                var ganados = [];

                for (var i = 0; i < tblGanado.length; i++) {
                    var id_ganado = tblGanado[i].id_ganado;
                    //dtTable.row(this).data()[3]
                    
                    var ganado =
                    {
                        Id_ganado: id_ganado,
                    };
                    ganados.push(ganado);
                }

                var Datos = {
                    ListaGanadosParaVender: ganados,
                    IDSucursal: $("#IdSucursal").val(),
                    IDCorral: $("#IdCorral").val()
                };

                $.ajax({
                    dataType: 'json',
                    type: 'POST',
                    url: '/Admin/CatGanado/Transferir/',
                    data: Datos,
                    success: function (response) {
                        if (response == "Correcto") {
                            window.location.href = '/Admin/CatGanado/Index';
                        }
                    },
                    error: function (request, status, error) {
                        window.location.href = '/Admin/CatGanado/Index';
                    }
                });
            }
        });

    };

    function Validar() {
        var error = 0;
        var ganado = tblGanadoTransferir.rows().data();
        IDSucursal = $("#IdSucursal").val();
        if (ganado.length == 0) {
            $("#tblGanadoTransferir").addClass("errorTableCSL");
            $("#tblGanadoTransferir").removeClass("okCSLGanado");
            $("#validation_summary").find("dd[for='Ganado']").addClass('help-block valid').text('-Debe de seleccionar un ganado para transferir');
            error = 1;
        }
        else {
            $("#tblGanadoTransferir").addClass("okCSLGanado");
            $("#tblGanadoTransferir").removeClass("errorTableCSL");
            $("#validation_summary").find("dd[for='Ganado']").addClass('help-block valid').text('');
        }
        if (IDSucursal == "") {
            $("#IdSucursal").addClass("errorTableCSL");
            $("#IdSucursal").removeClass("okCSLGanado");
            $("#validation_summary").find("dd[for='Sucursal']").addClass('help-block valid').text('-Debe de seleccionar una sucursal de tranferencia');
            error = 1;
        }
        else {
            $("#IdSucursal").addClass("okCSLGanado");
            $("#IdSucursal").removeClass("errorTableCSL");
            $("#validation_summary").find("dd[for='Sucursal']").addClass('help-block valid').text('');
        }
        return error;
    }

    return {
        //main function to initiate template pages
        init: function () {
            runValidator1();
            runEvents();
            initDataTables();
            initFuncionesGanado();
        }
    };
}();