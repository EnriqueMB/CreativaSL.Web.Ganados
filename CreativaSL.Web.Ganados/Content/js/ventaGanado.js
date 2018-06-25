var VentaGanado = function () {
    "use strict"
    var Id_venta = $("#Id_venta").val();
    var tblGanadoCorral, tblGanadoJaula;

    /*INICIA GANADO*/
    var initDataTables = function () {
        tblGanadoCorral = $('#tblGanadoCorral').DataTable({
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                },
                "url": "/Admin/Venta/DatatableGanadoActual/",
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
                { "data": "merma" },
                {
                    "data": "pesoPagado",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
                { "data": "precioKilo",
                "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
                {
                    "data": "subtotal",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
            ],
            "columnDefs": [
                {
                    "targets": [0, 1],
                    "visible": false,
                    "searchable": false
                }
            ]
        });
      
        tblGanadoJaula = $('#tblGanadoJaula').DataTable({
            fixedHeader: {
                header: true,
                footer: true,
                select: true
            },
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true,
            "ajax": {
                "data": {
                    Id_venta: Id_venta
                },
                "url": "/Admin/Venta/DatatableGanadoParaVenta/",
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
                { "data": "merma" },
                {
                    "data": "pesoPagado",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
                {
                    "data": "precioKilo",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
                {
                    "data": "subtotal",
                    "render": $.fn.dataTable.render.number(',', '.', 2, '$'),
                },
            ],
            "columnDefs": [
                {
                    "targets": [0, 1],
                    "visible": false,
                    "searchable": false
                }
            ]
        });
    };

    var initFuncionesGanado = function () {
        //Seleccionar filas 
        $('#tblGanadoCorral tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });
        $('#tblGanadoJaula tbody').on('click', 'tr', function () {
            $(this).toggleClass('selected');
        });
        $('#seleccionarCorral').click(function () {
            var nodos = tblGanadoCorral.rows({ page: 'all', search: 'applied' }).nodes();
            for (var i = 0 ; i < nodos.length ; i++) {
                $(nodos[i]).toggleClass("selected");
            }
        });
        $('#seleccionarJaula').click(function () {
            var nodos = tblGanadoJaula.rows({ page: 'all', search: 'applied' }).nodes();
            for (var i = 0 ; i < nodos.length ; i++) {
                $(nodos[i]).toggleClass("selected");
            }
        });

        $(document).on('submit', 'form#frm_venta', function (e) {
            e.preventDefault();

            //datos de las tablas
            var ganado = tblGanadoJaula.rows().data();
            var objGanado, cantidad = 0;
            var listaGanado = new Array();

            if (ganado.length == 0) {
                
                $("#tblGanadoJaula").addClass("errorTableCSL");
                $("#validation_summary").append("");
                $("#validation_summary").append("<dd style='color: #ff004d !important; '>-Debe de seleccionar un ganado para su venta</dd>");
            }
            else {
                
                $("#validation_summary").append("");
                for (var i = 0 ; i < ganado.length ; i++) {
                    listaGanado.unshift(ganado[i].id_ganado);
                    cantidad = i + 1;
                }
            }
        });
        
        //Pasar y regresar filas en las tablas
        $('#enviar').click(function () {
            var rows = tblGanadoCorral.rows('.selected').data();

            for (var i = 0; i < rows.length; i++) {
                var d = rows[i];

                tblGanadoJaula.row.add({
                    "id_ganado": d.id_ganado,
                    "id_detalle": d.id_detalle,
                    "numArete": d.numArete,                 
                    "corral": d.corral,
                    "genero": d.genero,                     
                    "merma": d.merma,
                    "pesoPagado": d.pesoPagado,
                    "precioKilo": d.precioKilo,
                    "subtotal": d.subtotal
                }).draw();
            }
            tblGanadoCorral.row('.selected').remove().draw(false);
        });

        $('#regresar').click(function () {
            var rows = tblGanadoJaula.rows('.selected').data();

            for (var i = 0; i < rows.length; i++) {
                var d = rows[i];

                tblGanadoCorral.row.add({
                    "id_ganado": d.id_ganado,
                    "id_detalle": d.id_detalle,
                    "numArete": d.numArete,
                    "corral": d.corral,
                    "genero": d.genero,
                    "merma": d.merma,
                    "pesoPagado": d.pesoPagado,
                    "precioKilo": d.precioKilo,
                    "subtotal": d.subtotal
                }).draw();
            }
            tblGanadoJaula.row('.selected').remove().draw(false);
        });

        //$('#enviar').click(function () {
        //    cGanadoM = 0, cGanadoH = 0, kGanadoM = 0, kGanadoH = 0;
        //    var rows = tblCorral.rows('.selected').data();

        //    for (var i = 0; i < rows.length; i++) {
        //        var d = rows[i];

        //        tblJaula.row.add([
        //            d[0], //Dueño
        //            d[1], //Genero
        //            d[2]  //Peso
        //        ]).draw();

        //        if (d[1] == "MACHO") {
        //            cGanadoM += 1;
        //            kGanadoM += parseInt(d[2]);
        //        }
        //        else {
        //            cGanadoH += 1;
        //            kGanadoH += parseInt(d[2]);
        //        }
        //    }
        //    sumarGenero(cGanadoM, cGanadoH);
        //    sumarPesoGanado(kGanadoM, kGanadoH);
        //    tblCorral.row('.selected').remove().draw(false);
        //});
        //$('#regresar').click(function () {
        //    cGanadoM = 0, cGanadoH = 0, kGanadoM = 0, kGanadoH = 0;

        //    var rows = tblJaula.rows('.selected').data();

        //    for (var i = 0; i < rows.length; i++) {
        //        var d = rows[i];

        //        tblCorral.row.add([
        //            d[0], //Dueño
        //            d[1], //Genero
        //            d[2]  //Peso
        //        ]).draw();
        //        if (d[1] == "MACHO") {
        //            cGanadoM += 1;
        //            kGanadoM += parseInt(d[2]);
        //        }
        //        else {
        //            cGanadoH += 1;
        //            kGanadoH += parseInt(d[2]);
        //        }
        //    }
        //    restarGenero(cGanadoM, cGanadoH);
        //    restarPesoGanado(kGanadoM, kGanadoH);
        //    tblJaula.row('.selected').remove().draw(false);
        //});
    };
    
    /*TERMINA GANADO*/





       return {
           init: function () {
               initDataTables();
               initFuncionesGanado();
        }
    };
}();