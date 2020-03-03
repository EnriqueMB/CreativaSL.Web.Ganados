var ConfiguracionReporteCliente = function () {
    "use strict";
    var tblClientesNoSeleccionados, tblClientesSeleccionados;
    var UrlClientes = "/Admin/CatCliente/ObtenerClienteXSucursal/";
    var UrlReporteCliente = "/Admin/CatCliente/GenerarReporteClientes/";

    var initEvents = function () {
        $('.datepicker').datepicker({
            format: 'dd/mm/yyyy',
            language: 'es'
        });

        $('input[type="checkbox"]').on("change", function () {
            var sucursales = [];
            $('input[type="checkbox"]').each(function () {
                if (this.checked) {
                    var idSucursal = $(this).attr("data-idsucursal");
                    sucursales.push(idSucursal);
                }

            });

            $.ajax({
                url: UrlClientes,
                data: { sucursales: sucursales },
                type: "POST",
                dataType: "json",
                success: function (result) {
                    tblClientesNoSeleccionados.clear().draw();
                    tblClientesSeleccionados.clear().draw();

                    for (var i = 0; i < result.length; i++) {
                        tblClientesNoSeleccionados.row.add({
                            "IdCliente": result[i].IdCliente,
                            "Sucursal": result[i].Sucursal,
                            "Cliente": result[i].Cliente
                        }).draw();
                    }
                },
                error: function () {
                    Mensaje(
                        "Error al tratar de obtener los clientes, intentelo más tarde o contacte con soporte técnico",
                        "2");
                }
            });

        });
    };

    var initDataTables = function () {

        tblClientesNoSeleccionados = $('#tblClientesNoSeleccionados').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            "columns": [
                { "data": "IdCliente" },
                { "data": "Sucursal" },
                { "data": "Cliente" },
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ]
        });

        tblClientesSeleccionados = $('#tblClientesSeleccionados').DataTable({
            fixedHeader: {
                header: true,
                footer: true,
                select: true
            },
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            "columns": [
                { "data": "IdCliente" },
                { "data": "Sucursal" },
                { "data": "Cliente" },
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ]
        });
    };

    function post(path, params, method = 'post') {

        // The rest of this code assumes you are not using a library.
        // It can be made less wordy if you use one.
        var form = document.createElement('form');
        form.method = method;
        form.action = path;

        for (const key in params) {
            if (params.hasOwnProperty(key)) {
                var hiddenField = document.createElement('input');
                hiddenField.type = 'hidden';
                hiddenField.name = key;
                hiddenField.value = params[key];

                form.appendChild(hiddenField);
            }
        }

        document.body.appendChild(form);
        form.submit();
    }

    var initFuncionesGanado = function () {
        //Seleccionar filas 
        $('#tblClientesNoSeleccionados tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });
        $('#tblClientesSeleccionados tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });

        $('#seleccionarTodaLaTblClientesNoSeleccionados').click(function () {
            var nodos = tblClientesNoSeleccionados.rows({ page: 'all', search: 'applied' }).nodes();
            for (var i = 0; i < nodos.length; i++) {
                $(nodos[i]).toggleClass("selected");
            }
        });

        $('#seleccionarTodaLaTblClientesSeleccionados').click(function () {
            var nodos = tblClientesSeleccionados.rows({ page: 'all', search: 'applied' }).nodes();
            for (var i = 0; i < nodos.length; i++) {
                $(nodos[i]).toggleClass("selected");
            }
        });

        $('#generarReporte').on('click', function () {
            //datos de las tablas

            var error = Validar();
            if (!error) {

                var rows = tblClientesSeleccionados.rows().data();

                var clientes = [];

                for (var i = 0; i < rows.length; i++) {
                    var row = rows[i];

                    var idCliente = row.IdCliente;

                    clientes.push(idCliente);
                }
                var fechaInicio = $("#FechaInicio").val().trim();
                var fechaFin = $("#FechaFin").val().trim();

                post(UrlReporteCliente, { clientes: clientes, fechaInicio: fechaInicio, fechaFin: fechaFin });
            }
        });

        //Pasar y regresar filas en las tablas
        $('#enviar').click(function () {
            var rows = tblClientesNoSeleccionados.rows('.selected').data();

            for (var i = 0; i < rows.length; i++) {
                var row = rows[i];

                //lo agrego a la tabla jaula para su envio
                tblClientesSeleccionados.row.add({
                    "IdCliente": row.IdCliente,
                    "Sucursal": row.Sucursal,
                    "Cliente": row.Cliente
                }).draw();

            }
            tblClientesNoSeleccionados.rows('.selected').remove().draw();
        });

        $('#regresar').click(function () {
            var rows = tblClientesSeleccionados.rows('.selected').data();
            for (var i = 0; i < rows.length; i++) {
                var row = rows[i];

                tblClientesNoSeleccionados.row.add({
                    "IdCliente": row.IdCliente,
                    "Sucursal": row.Sucursal,
                    "Cliente": row.Cliente
                }).draw();
            }
            tblClientesSeleccionados.rows('.selected').remove().draw();
        });
    };


    function Validar() {
        var error = false;
        var clientesSeleccionados = tblClientesSeleccionados.rows().data();

        if (clientesSeleccionados.length === 0) {
            $("#tblClientesSeleccionados").addClass("errorCID");
            $("#tblClientesSeleccionados").removeClass("successCID");
            $("#validation_summary").find("dd[for='ErrorProveedor']").addClass('help-block valid').text('-Debe de seleccionar por lo menos un cliente.');
            error = true;
        }
        else {
            $("#tblClientesSeleccionados").addClass("successCID");
            $("#tblClientesSeleccionados").removeClass("errorCID");
            $("#validation_summary").find("dd[for='ErrorProveedor']").addClass('help-block valid').text('');
        }

        var fechaInicio = $("#FechaInicio").val().trim();
        if (fechaInicio) {
            $("#FechaInicio").addClass("successCID");
            $("#FechaInicio").removeClass("errorCID");
            $("#validation_summary").find("dd[for='ErrorFechaInicio']").addClass('help-block valid').text('');
        }
        else {
            $("#FechaInicio").addClass("errorCID");
            $("#FechaInicio").removeClass("successCID");
            $("#validation_summary").find("dd[for='ErrorFechaInicio']").addClass('help-block valid').text('-Debe de seleccionar una fecha de inicio.');
            error = true;
        }

        var fechaFin = $("#FechaFin").val().trim();
        if (fechaFin) {
            $("#FechaFin").addClass("successCID");
            $("#FechaFin").removeClass("errorCID");
            $("#validation_summary").find("dd[for='ErrorFechaFin']").addClass('help-block valid').text('');
        }
        else {
            $("#FechaFin").addClass("errorCID");
            $("#FechaFin").removeClass("successCID");
            $("#validation_summary").find("dd[for='ErrorFechaFin']").addClass('help-block valid').text('-Debe de seleccionar una fecha fin.');
            error = true;
        }

        return error;
    }

    return {
        init: function () {
            initEvents();
            initDataTables();
            initFuncionesGanado();
        }
    };
}();