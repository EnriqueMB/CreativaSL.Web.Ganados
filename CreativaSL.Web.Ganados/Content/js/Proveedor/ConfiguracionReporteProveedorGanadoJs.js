var ConfiguracionReporteProveedorGanado = function () {
    "use strict";
    var tblProveedoresNoSeleecionados, tblProveedoresSeleccionados;
    var UrlProveedores = "/Admin/CatProveedor/ObtenerProveedoresXSucursal/";
    var UrlReporteProveedorGanado = "/Admin/CatProveedor/GenerarReporteProveedoresGanado/";

    var initEvents = function() {
        $('input[type="checkbox"]').on("change", function () {
            var sucursales = [];
            $('input[type="checkbox"]').each(function () {
                if (this.checked) {
                    var idSucursal = $(this).attr("data-idsucursal");
                    sucursales.push(idSucursal);
                }
                    
            });

            $.ajax({
                url: UrlProveedores,
                data: { sucursales: sucursales },
                type: "POST",
                dataType: "json",
                success: function(result) {
                    tblProveedoresNoSeleecionados.clear().draw();
                    tblProveedoresSeleccionados.clear().draw();

                    for (var i = 0; i < result.length; i++) {
                        tblProveedoresNoSeleecionados.row.add({
                            "IdProveedor": result[i].IdProveedor,
                            "Sucursal": result[i].Sucursal,
                            "Proveedor": result[i].Proveedor
                        }).draw();
                    }
                },
                error: function() {
                    Mensaje(
                        "Error al tratar de obtener los proveedores, intentelo más tarde o contacte con soporte técnico",
                        "2");
                }
            });

        });
    };
    
    var initDataTables = function () {

        tblProveedoresNoSeleecionados = $('#tblProveedoresNoSeleecionados').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            "columns": [
                { "data": "IdProveedor" },
                { "data": "Sucursal" },
                { "data": "Proveedor" },
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ]
        });

        tblProveedoresSeleccionados = $('#tblProveedoresSeleccionados').DataTable({
            fixedHeader: {
                header: true,
                footer: true,
                select: true
            },
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            "columns": [
                { "data": "IdProveedor" },
                { "data": "Sucursal" },
                { "data": "Proveedor" },
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

    var initFuncionesGanado = function ()
    {
        //Seleccionar filas 
        $('#tblProveedoresNoSeleecionados tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });
        $('#tblProveedoresSeleccionados tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });

        $('#seleccionarTodaLaTblProveedoresNoSeleccionados').click(function () {
            var nodos = tblProveedoresNoSeleecionados.rows({ page: 'all', search: 'applied' }).nodes();
            for (var i = 0; i < nodos.length; i++) {
                $(nodos[i]).toggleClass("selected");
            }
        });

        $('#seleccionarTodaLaTblProveedoresSeleccionados').click(function () {
            var nodos = tblProveedoresSeleccionados.rows({ page: 'all', search: 'applied' }).nodes();
            for (var i = 0; i < nodos.length; i++) {
                $(nodos[i]).toggleClass("selected");
            }
        });
        
        $('#generarReporte').on('click', function () {
            //datos de las tablas

            var error = Validar();
            if (!error) {

                var rows = tblProveedoresSeleccionados.rows().data();

                var proveedores = [];

                for (var i = 0; i < rows.length; i++) {
                    var row = rows[i];

                    var idProveedor = row.IdProveedor;

                    proveedores.push(idProveedor);
                }

                //var xhr = new XMLHttpRequest();
                //xhr.open("POST", UrlReporteProveedorGanado, true);
                //xhr.setRequestHeader('Content-Type', 'application/json');
                //xhr.send(JSON.stringify({
                //    proveedores: proveedores
                //}));

                post(UrlReporteProveedorGanado, { proveedores: proveedores });
            }
        });

        //Pasar y regresar filas en las tablas
        $('#enviar').click(function () {
            var rows = tblProveedoresNoSeleecionados.rows('.selected').data();

            for (var i = 0; i < rows.length; i++) {
                var row = rows[i];

                //lo agrego a la tabla jaula para su envio
                tblProveedoresSeleccionados.row.add({
                    "IdProveedor": row.IdProveedor,
                    "Sucursal": row.Sucursal,
                    "Proveedor": row.Proveedor
                }).draw();

            }
            tblProveedoresNoSeleecionados.rows('.selected').remove().draw();
        });

        $('#regresar').click(function () {
            var rows = tblProveedoresSeleccionados.rows('.selected').data();
            for (var i = 0; i < rows.length; i++) {
                var row = rows[i];

                tblProveedoresNoSeleecionados.row.add({
                    "IdProveedor": row.IdProveedor,
                    "Sucursal": row.Sucursal,
                    "Proveedor": row.Proveedor
                }).draw();
            }
            tblProveedoresSeleccionados.rows('.selected').remove().draw();
        });
    };


    function Validar() {
        var error = true;
        var proveedoresSeleccionados = tblProveedoresSeleccionados.rows().data();

        if (proveedoresSeleccionados.length === 0) {
            $("#tblProveedoresSeleccionados").addClass("errorCID");
            $("#tblProveedoresSeleccionados").removeClass("successCID");
            $("#validation_summary").find("dd[for='ErrorProveedor']").addClass('help-block valid').text('-Debe de seleccionar por lo menos un proveedor.');
        }
        else {
            $("#tblProveedoresSeleccionados").addClass("successCID");
            $("#tblProveedoresSeleccionados").removeClass("errorCID");
            $("#validation_summary").find("dd[for='ErrorProveedor']").addClass('help-block valid').text('');
            error = false;
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