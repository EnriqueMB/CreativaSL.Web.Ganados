var jaula = function () {
    "use strict";
    var tblVentas, tblJaula;
    var UrlVentas = "/Admin/Jaulas/ObtenerVentas";
    var tblVentasData = function () {
        $.ajax({
            url: UrlVentas,            
            type: "GET",
            dataType: "json",
            success: function (result) {
                tblVentas.clear().draw();
                tblJaula.clear().draw();

                for (var i = 0; i < result.length; i++) {
                    tblVentas.row.add({
                        "Id_venta": result[i].Id_venta,
                        "Folio": result[i].Folio,
                        "Sucursal": result[i].Sucursal,
                        "CantTotal": result[i].CantTotal,
                        "PesoTotal": result[i].PesoTotal
                    }).draw();
                }
            },
            error: function () {
                Mensaje(
                    "Error al tratar de obtener las ventas, intentelo más tarde o contacte con soporte técnico",
                    "2");
            }
        });
    };
    var initDataTables = function () {
        tblVentas = $('#tblVentas').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            "columns": [
                { "data": "Id_venta" },
                { "data": "Folio" },
                { "data": "Sucursal" },
                { "data": "CantTotal" },
                {"data":"PesoTotal"}
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ]
        });

        tblJaula = $('#tblJaula').DataTable({
            fixedHeader: {
                header: true,
                footer: true,
                select: true
            },
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            "columns": [
                { "data": "Id_venta" },
                { "data": "Folio" },
                { "data": "Sucursal" },
                { "data": "CantTotal" },
                { "data": "PesoTotal" }
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
    var initFuncionesJaula = function () {
        $('#tblVentas tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });
        $('#tblJaula tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });

        $('#seleccionarTodaLaTblVentas').click(function () {
            var nodos = tblVentas.rows({ page: 'all', search: 'applied' }).nodes();
            for (var i = 0; i < nodos.length; i++) {
                $(nodos[i]).toggleClass("selected");
            }
        });

        $('#seleccionarTodaLaTblJaula').click(function () {
            var nodos = tblJaula.rows({ page: 'all', search: 'applied' }).nodes();
            for (var i = 0; i < nodos.length; i++) {
                $(nodos[i]).toggleClass("selected");
            }
        });

        $('#guardarJaula').on('click', function () {
            //datos de las tablas

            var error = Validar();
            if (!error) {

                var rows = tblVentas.rows().data();

                var ventas = [];

                for (var i = 0; i < rows.length; i++) {
                    var row = rows[i];

                    var Id_venta = row.Id_venta;

                    ventas.push(Id_venta);
                }
                

                post(UrlReporteCliente, { ventas: ventas, id_jaula: id_jaula });
            }
        });

        //Pasar y regresar filas en las tablas
        $('#enviar').click(function () {
            var rows = tblVentas.rows('.selected').data();

            for (var i = 0; i < rows.length; i++) {
                var row = rows[i];

                //lo agrego a la tabla jaula para su envio
                tblJaula.row.add({                    
                    "Id_venta": row.Id_venta,
                    "Folio": row.Folio,
                    "Sucursal": row.Sucursal,
                    "CantTotal": row.CantTotal,
                    "PesoTotal": row.PesoTotal
                }).draw();

            }
            tblVentas.rows('.selected').remove().draw();
        });

        $('#regresar').click(function () {
            var rows = tblJaula.rows('.selected').data();
            for (var i = 0; i < rows.length; i++) {
                var row = rows[i];

                tblVentas.row.add({
                    "Id_venta": row.Id_venta,
                    "Folio": row.Folio,
                    "Sucursal": row.Sucursal,
                    "CantTotal": row.CantTotal,
                    "PesoTotal": row.PesoTotal
                }).draw();
            }
            tblJaula.rows('.selected').remove().draw();
        });
    };
    return {
        init: function () {
            tblVentasData();
            initDataTables();
            initFuncionesJaula();
        }
    };
}();