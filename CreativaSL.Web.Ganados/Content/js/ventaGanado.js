var VentaGanado = function () {
    "use strict"
    var Id_venta = $("#Id_venta").val();
    var tblGanadoCorral;

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
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ]
        });
       

        tblJaula = $('#jaula').DataTable({
            fixedHeader: {
                header: true,
                footer: true,
                select: true
            },
            "language": {
                "url": "/Content/assets/json/Spanish.json"
            },
            responsive: true
        });
    };
    /*TERMINA GANADO*/





       return {
        init: function () {
            initDataTables();
        }
    };
}();